using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using DynamicMenu.ASLCompany.DataAccess;
using DynamicMenu.ASLCompany.Interface;
using DynamicMenu.LogData;

namespace DynamicMenu.ASLCompany.UI
{
    public partial class MenuRole : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection con = new SqlConnection(dbFunctions.connection);
        ASLDataAccess dob = new ASLDataAccess();
        ASLInterface iob = new ASLInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null)
                {
                    Response.Redirect("~/LogIn/UI/LogIn.aspx");
                }
                else
                {
                    //Session["Companyidgrid"] = null;
                    string userId = Session["USERID"].ToString();
                    txtUserName.Focus();
                    lblMsg.Visible = false;

                    if (Session["USERTYPE"].ToString() == "SUPERADMIN")
                    {
                        lblUserlabel.Visible = false;
                        txtUserName.Visible = false;
                        lblUserlabel1.Visible = true;
                        txtComapnyAdminName.Visible = true;
                        dbFunctions.dropDownAddWithSelect(ddlModuleName, @"SELECT MODULENM FROM ASL_MENUMST 
                        WHERE MODULEID NOT IN ('01','02') ORDER BY MODULENM");
                    }
                    else
                    {
                        dbFunctions.dropDownAddWithSelect(ddlModuleName, @"SELECT DISTINCT ASL_MENUMST.MODULENM
                    FROM ASL_USERCO INNER JOIN
                    ASL_ROLE ON ASL_USERCO.COMPID = ASL_ROLE.COMPID INNER JOIN
                    ASL_MENU ON ASL_ROLE.MODULEID = ASL_MENU.MODULEID INNER JOIN
                    ASL_MENUMST ON ASL_ROLE.MODULEID = ASL_MENUMST.MODULEID
                    WHERE ASL_ROLE.STATUS='A' AND ASL_ROLE.USERID='" + userId + "' ORDER BY ASL_MENUMST.MODULENM");
                        lblUserlabel.Visible = true;
                        txtUserName.Visible = true;
                        lblUserlabel1.Visible = false;
                        txtComapnyAdminName.Visible = false;
                    }
                }
            }
        }

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Focus();
                lblMsg.Text = "Select User Name.";
                lblMsg.Visible = true;
                txtUserName.Text = "";
            }
            else
            {
                string companyid = HttpContext.Current.Session["COMPANYID"].ToString();
                lblCompanyUserId.Text = "";
                dbFunctions.lblAdd(@"SELECT ASL_USERCO.USERID FROM ASL_USERCO 
                WHERE ASL_USERCO.USERNM='" + txtUserName.Text + "' AND ASL_USERCO.COMPID='" + companyid + "'", lblCompanyUserId);
                lblMsg.Visible = false;
                Session["Companyidgrid"] = null;
                Session["Companyidgrid"] = companyid;
            }
        }

        protected void txtComapnyAdminName_TextChanged(object sender, EventArgs e)
        {
            if (txtComapnyAdminName.Text == "")
            {
                txtComapnyAdminName.Focus();
                lblMsg.Text = "Select Company Name.";
                lblMsg.Visible = true;
                txtComapnyAdminName.Text = "";
            }
            else
            {
                //string companyid = HttpContext.Current.Session["COMPANYID"].ToString();
                lblCompanyUserId.Text = "";
                dbFunctions.lblAdd(@"SELECT CONVERT(NVARCHAR,COMPID)+'01' AS USERID FROM ASL_COMPANY 
                    WHERE COMPNM='" + txtComapnyAdminName.Text + "'", lblCompanyUserId);
                Session["Companyidgrid"] = null;
                Session["Companyidgrid"] = dbFunctions.StringData(@"SELECT COMPID AS USERID FROM ASL_COMPANY 
                    WHERE COMPNM='" + txtComapnyAdminName.Text + "'");
                lblMsg.Visible = false;
            }
        }

        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModuleName.Text == "--SELECT--")
            {
                lblMsg.Text = "Select Module Name.";
                lblMsg.Visible = true;
            }
            else
            {
                string companyid = HttpContext.Current.Session["COMPANYID"].ToString();
                lblModuleId.Text = "";
                dbFunctions.lblAdd(@"SELECT MODULEID FROM ASL_MENUMST WHERE MODULENM='" + ddlModuleName.Text + "'", lblModuleId);

            }
        }

        public string FieldCheck()
        {
            string checkResult = "";
            if (lblCompanyUserId.Text == "")
            {
                checkResult = "Select user name.";
                txtUserName.Text = "";
                txtComapnyAdminName.Text = "";
                txtUserName.Focus();
            }
            else if (ddlModuleName.Text == "--SELECT--" || lblModuleId.Text == "")
            {
                checkResult = "Select module name.";
                ddlModuleName.SelectedIndex = -1;
                ddlModuleName.Focus();
            }
            else if (ddlMenuType.SelectedValue == "S")
            {
                checkResult = "Select form type.";
                ddlMenuType.SelectedIndex = -1;
                ddlMenuType.Focus();
            }
            else
            {
                checkResult = "true";
            }
            return checkResult;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                if (FieldCheck() == "true")
                {
                    ShowGridForAslRole();
                    lblMsg.Visible = false;
                }
                else
                {
                    lblMsg.Text = FieldCheck();
                    lblMsg.Visible = true;
                }

            }
        }
        protected void ShowGridForAslRole()
        {
            string companyID = "";
            if (Session["Companyidgrid"] != null)
                companyID = HttpContext.Current.Session["Companyidgrid"].ToString();

            string userid = lblCompanyUserId.Text;
            string moduleid = lblModuleId.Text;
            string menuTp = ddlMenuType.SelectedValue;

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT ASL_MENU.MENUNM, ASL_MENUMST.MODULENM, 
                CASE(ASL_ROLE.STATUS) WHEN 'A' THEN 'ACTIVE' ELSE 'INACTIVE' END AS STATUS, 
                CASE(ASL_ROLE.INSERTR) WHEN 'A' THEN 'ACTIVE' ELSE 'INACTIVE' END AS INSERTR, 
                CASE(ASL_ROLE.UPDATER) WHEN 'A' THEN 'ACTIVE' ELSE 'INACTIVE' END AS UPDATER, 
                CASE(ASL_ROLE.DELETER) WHEN 'A' THEN 'ACTIVE' ELSE 'INACTIVE' END AS DELETER, 
                ASL_ROLE.COMPID, ASL_ROLE.USERID, ASL_ROLE.MODULEID, ASL_ROLE.MENUID
                FROM ASL_ROLE INNER JOIN
                ASL_MENUMST ON ASL_ROLE.MODULEID = ASL_MENUMST.MODULEID INNER JOIN
                ASL_MENU ON ASL_ROLE.MODULEID = ASL_MENU.MODULEID AND ASL_ROLE.MENUID = ASL_MENU.MENUID
                WHERE (ASL_ROLE.COMPID = @COMPID) AND (ASL_ROLE.USERID = @USERID AND ASL_ROLE.MENUTP=@MENUTP  
                AND ASL_ROLE.MODULEID=@MODULEID )", conn);
            cmd.Parameters.AddWithValue("@COMPID", companyID);
            cmd.Parameters.AddWithValue("@USERID", userid);
            cmd.Parameters.AddWithValue("@MENUTP", menuTp);
            cmd.Parameters.AddWithValue("@MODULEID", moduleid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridViewAslRole.DataSource = ds;
                gridViewAslRole.DataBind();
                //TextBox txtZONENM = (TextBox)gridViewAslRole.FooterRow.FindControl("txtCountryNM");
                //txtZONENM.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gridViewAslRole.DataSource = ds;
                gridViewAslRole.DataBind();
                int columncount = gridViewAslRole.Rows[0].Cells.Count;
                gridViewAslRole.Rows[0].Cells.Clear();
                gridViewAslRole.Rows[0].Cells.Add(new TableCell());
                gridViewAslRole.Rows[0].Cells[0].ColumnSpan = columncount;
                gridViewAslRole.Rows[0].Cells[0].Text = "No Records Found";
                //TextBox txtZONENM = (TextBox)gridViewAslRole.FooterRow.FindControl("txtCountryNM");
                //txtZONENM.Focus();
            }
        }

        protected void gridViewAslRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                var lblCompanyIdEdit = (Label)gridViewAslRole.Rows[e.RowIndex].FindControl("lblCompanyIdEdit");
                var lblUserIdEdit = (Label)gridViewAslRole.Rows[e.RowIndex].FindControl("lblUserIdEdit");
                var lblModuleIdEdit = (Label)gridViewAslRole.Rows[e.RowIndex].FindControl("lblModuleIdEdit");
                var lblMenuIdEdit = (Label)gridViewAslRole.Rows[e.RowIndex].FindControl("lblMenuIdEdit");

                var ddlStatusEdit = (DropDownList)gridViewAslRole.Rows[e.RowIndex].FindControl("ddlStatusEdit");
                var ddlInsertEdit = (DropDownList)gridViewAslRole.Rows[e.RowIndex].FindControl("ddlInsertEdit");
                var ddlUpdateEdit = (DropDownList)gridViewAslRole.Rows[e.RowIndex].FindControl("ddlUpdateEdit");
                var ddlDeleteEdit = (DropDownList)gridViewAslRole.Rows[e.RowIndex].FindControl("ddlDeleteEdit");
                var txtIp = (TextBox)Master.FindControl("txtIp");
                var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                iob.LotiLengTudeUpdate = txtLotiLongTude.Text;
                iob.IpAddressUpdate = txtIp.Text;
                iob.UserIdUpdate = Convert.ToInt64(Session["USERID"].ToString());
                iob.InTimeUpdate = dbFunctions.Timezone(DateTime.Now);

                iob.LotiLengTudeInsert = txtLotiLongTude.Text;
                iob.IpAddressInsert = txtIp.Text;
                iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
                iob.UserPcInsert = dbFunctions.UserPc();
                iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

                iob.CompanyId = Convert.ToInt16(lblCompanyIdEdit.Text);
                iob.CompanyUserId = Convert.ToInt16(lblUserIdEdit.Text);
                iob.ModuleId = lblModuleIdEdit.Text;
                iob.MenuId = lblMenuIdEdit.Text;
                iob.MenuType = ddlMenuType.SelectedValue;

                iob.Status = ddlStatusEdit.SelectedValue;
                iob.InsertRole = ddlInsertEdit.SelectedValue;
                iob.UpdateRole = ddlUpdateEdit.SelectedValue;
                iob.DeleteRole = ddlDeleteEdit.SelectedValue;

                string s = dob.UPDATE_ASL_ROLE(iob);

                if (s == "")
                {
                    string cmpuserid = iob.CompanyUserId.ToString();
                    string cmpadminid = iob.CompanyId + "01";

                    if (cmpuserid == cmpadminid && Session["USERTYPE"].ToString() == "SUPERADMIN" && iob.Status == "I")
                    {
                        iob.CompanyUserId = Convert.ToInt64(cmpadminid);
                        dob.DELETE_ASL_ROLE(iob);
                    }
                    else if (cmpuserid == cmpadminid && Session["USERTYPE"].ToString() == "SUPERADMIN" && iob.Status == "A")
                    {
                        iob.Status = "I";
                        iob.InsertRole = "I";
                        iob.UpdateRole = "I";
                        iob.DeleteRole = "I";

                        con.Open();
                        
                        SqlCommand cmd = new SqlCommand(@"SELECT COMPID, USERID  FROM ASL_USERCO 
                        WHERE COMPID=@COMPID AND USERID!=@USERID", con);
                        cmd.Parameters.AddWithValue("@COMPID", iob.CompanyId);
                        cmd.Parameters.AddWithValue("@USERID", cmpadminid);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            iob.CompanyId = Convert.ToInt16(dr["COMPID"].ToString());
                            iob.CompanyUserId = Convert.ToInt16(dr["USERID"].ToString());
                            dob.INSERT_ASL_ROLE(iob);
                        }
                        dr.Close();
                        con.Close();
                    }
                    // logdata add start //
                    string lotileng = iob.LotiLengTudeUpdate;
                    string logdata = @"Company Id: " + iob.CompanyId + ", User Id: " + iob.CompanyUserId + ", Module Id: " +
                    iob.ModuleId + ", Menu Id: " + iob.MenuId + ", Menu Type: " + iob.MenuType + ", Status: " + iob.Status +
                    ", Insert Role: " + iob.InsertRole + ", Update Role: " + iob.UpdateRole + ", Delete Role: " + iob.DeleteRole;
                    string logid = "UPDATE";
                    string tableid = "ASL_ROLE";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, txtIp.Text);
                    // logdata add start //

                    lblMsg.Text = "Updated succesfully.";
                    lblMsg.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Updated failed.";
                    lblMsg.Visible = true;
                }
                gridViewAslRole.EditIndex = -1;
                ShowGridForAslRole();
            }
        }

        protected void gridViewAslRole_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                gridViewAslRole.EditIndex = e.NewEditIndex;
                ShowGridForAslRole();
                var lblCompanyIdEdit = (Label)gridViewAslRole.Rows[e.NewEditIndex].FindControl("lblCompanyIdEdit");
                var lblUserIdEdit = (Label)gridViewAslRole.Rows[e.NewEditIndex].FindControl("lblUserIdEdit");
                var lblModuleIdEdit = (Label)gridViewAslRole.Rows[e.NewEditIndex].FindControl("lblModuleIdEdit");
                var lblMenuIdEdit = (Label)gridViewAslRole.Rows[e.NewEditIndex].FindControl("lblMenuIdEdit");

                var ddlStatusEdit = (DropDownList)gridViewAslRole.Rows[e.NewEditIndex].FindControl("ddlStatusEdit");
                var ddlInsertEdit = (DropDownList)gridViewAslRole.Rows[e.NewEditIndex].FindControl("ddlInsertEdit");
                var ddlUpdateEdit = (DropDownList)gridViewAslRole.Rows[e.NewEditIndex].FindControl("ddlUpdateEdit");
                var ddlDeleteEdit = (DropDownList)gridViewAslRole.Rows[e.NewEditIndex].FindControl("ddlDeleteEdit");

                string status = "";
                string insert = "";
                string update = "";
                string delete = "";
                con.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT ASL_ROLE.STATUS, ASL_ROLE.INSERTR, ASL_ROLE.UPDATER, ASL_ROLE.DELETER
                FROM ASL_ROLE INNER JOIN ASL_MENUMST ON ASL_ROLE.MODULEID = ASL_MENUMST.MODULEID INNER JOIN
                ASL_MENU ON ASL_ROLE.MODULEID = ASL_MENU.MODULEID AND ASL_ROLE.MENUID = ASL_MENU.MENUID
                WHERE ASL_ROLE.COMPID=@COMPID AND  ASL_ROLE.USERID=@USERID AND ASL_ROLE.MODULEID=@MODULEID 
                AND ASL_ROLE.MENUID=@MENUID ", con);
                cmd.Parameters.AddWithValue("@COMPID", lblCompanyIdEdit.Text);
                cmd.Parameters.AddWithValue("@USERID", lblUserIdEdit.Text);
                cmd.Parameters.AddWithValue("@MODULEID", lblModuleIdEdit.Text);
                cmd.Parameters.AddWithValue("@MENUID", lblMenuIdEdit.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    status = dr["STATUS"].ToString();
                    insert = dr["INSERTR"].ToString();
                    update = dr["UPDATER"].ToString();
                    delete = dr["DELETER"].ToString();
                }
                dr.Close();
                con.Close();

                ddlStatusEdit.Text = status;
                ddlInsertEdit.Text = insert;
                ddlUpdateEdit.Text = update;
                ddlDeleteEdit.Text = delete;
                lblMsg.Visible = false;
                ddlStatusEdit.Focus();
            }
        }
        protected void gridViewAslRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                gridViewAslRole.EditIndex = -1;
                ShowGridForAslRole();
                lblMsg.Visible = false;
            }
        }

        protected void gridViewAslRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                gridViewAslRole.PageIndex = e.NewPageIndex;
                gridViewAslRole.DataBind();
                ShowGridForAslRole();
                lblMsg.Visible = false;
            }
        }


        protected void gridViewAslRole_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                DataTable dataTable = gridViewAslRole.DataSource as DataTable;

                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                    gridViewAslRole.DataSource = dataView;
                    gridViewAslRole.DataBind();
                    ShowGridForAslRole();
                }
            }
        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

    }
}