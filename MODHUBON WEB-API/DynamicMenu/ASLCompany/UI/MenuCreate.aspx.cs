using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;

namespace DynamicMenu.ASLCompany.UI
{
    public partial class MenuCreate : System.Web.UI.Page
    {
        IFormatProvider dateormat = new System.Globalization.CultureInfo("fr-FR", true);

        DataAccess.ASLDataAccess dob = new DataAccess.ASLDataAccess();
        Interface.ASLInterface iob = new Interface.ASLInterface();
        SqlConnection con = new SqlConnection(dbFunctions.connection);
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
                    txtModuleName.Focus();
                    lblMsg.Visible = false;
                    lblMsgMenu.Visible = false;
                }
            }
        }

        public string ModuleNameAdd()
        {
            string checkResult = "";
            if (txtModuleName.Text == "")
            {
                checkResult = "false";
                lblMsg.Text = "Select Module Name.";
                lblMsg.Visible = true;
            }
            else
            {
                string datacheck = dbFunctions.StringData("SELECT MODULENM FROM ASL_MENUMST WHERE MODULENM='" + txtModuleName.Text + "'");
                if (datacheck == "")
                {
                    var txtIp = (TextBox)Master.FindControl("txtIp");
                    iob.IpAddressInsert = txtIp.Text;
                    iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
                    iob.UserPcInsert = dbFunctions.UserPc();
                    iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

                    iob.ModuleId = ModuleId();
                    iob.ModuleName = txtModuleName.Text.Trim();

                    dob.INSERT_ASL_MENUMST(iob);
                }
            }
            return checkResult;
        }

        public string ModuleId()
        {
            string moduleId = "";
            string maxDbModuleId = dbFunctions.StringData("SELECT MAX(MODULEID) AS MODULEID FROM ASL_MENUMST");
            if (maxDbModuleId == "")
                moduleId = "01";
            else
            {
                int maxid = Convert.ToInt16(maxDbModuleId);
                maxid++;
                if (maxid < 10)
                    moduleId = "0" + maxid;
                else
                    moduleId = maxid.ToString();
            }
            return moduleId;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtModuleName.Text != "")
            {
                string checkModuleNmFromDb =
                    dbFunctions.StringData("SELECT MODULENM FROM ASL_MENUMST WHERE MODULENM='" + txtModuleName.Text + "'");
                if (checkModuleNmFromDb == "")
                {
                    ModuleNameAdd();
                    lblMsg.Text = "Module Create Succesfully.";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Green;
                }
                else
                {
                    lblMsg.Text = "Module Already Created. Add menu Name.";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Green;
                }
                Session["ModuleId"] =
                   dbFunctions.StringData("SELECT MODULEID FROM ASL_MENUMST WHERE MODULENM='" + txtModuleName.Text + "'");

                GridShowForMenu();
            }
            else
            {
                lblMsg.Text = "Write a Module Name.";
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Red;
                txtModuleName.Focus();
            }
        }



        public string MenuId(string moduleid)
        {
            string menuid = "";
            string maxMunuIdFromDb = dbFunctions.StringData("SELECT MAX(MENUID) FROM ASL_MENU WHERE MODULEID='" + moduleid + "'");
            if (maxMunuIdFromDb == "")
                menuid = moduleid + "01";
            else
            {
                string menuidwithoutmodule = maxMunuIdFromDb.Substring(2, 2);
                int menuidconvert = Convert.ToInt16(menuidwithoutmodule);
                menuidconvert++;
                if (menuidconvert < 10)
                    menuid = moduleid + "0" + menuidconvert;
                else
                    menuid = moduleid + menuidconvert;
            }
            return menuid;
        }
        public void MenuNameAdd(string moduleid)
        {
            var txtIp = (TextBox)Master.FindControl("txtIp");
            iob.IpAddressInsert = txtIp.Text;
            iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
            iob.UserPcInsert = dbFunctions.UserPc();
            iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

            iob.ModuleId = moduleid;
            iob.MenuId = MenuId(moduleid);
            //iob.MenuName = txtMenuName.Text.Trim();
            //iob.MenuType = ddlMenuType.SelectedValue;
            //iob.MenuLink = txtMenuLink.Text;

            string s = dob.INSERT_ASL_MENU(iob);
            if (s == "")
            {
                MenuAddInAslRoleForAllUser(iob.ModuleId, iob.MenuId, iob.MenuType);
                // MenuActiveForAllCompanyAdmin(iob.ModuleId, iob.MenuId, iob.MenuType);
            }
        }

        public void MenuAddInAslRoleForAllUser(string moduleId, string menuId, string menutp)
        {
            var txtIp = (TextBox)Master.FindControl("txtIp");
            TextBox txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
            iob.LotiLengTudeInsert = txtLotiLongTude.Text;
            iob.IpAddressInsert = txtIp.Text;
            iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
            iob.UserPcInsert = dbFunctions.UserPc();
            iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);
            iob.ModuleId = moduleId;
            iob.MenuId = menuId;
            iob.MenuType = menutp;
            iob.Status = "I";
            iob.InsertRole = "I";
            iob.UpdateRole = "I";
            iob.DeleteRole = "I";
            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT SUBSTRING(CONVERT(NVARCHAR,USERID),1,3) AS CMPID, USERID 
FROM ASL_USERCO WHERE SUBSTRING(CONVERT(NVARCHAR,USERID),1,3) !='100' AND SUBSTRING(CONVERT(NVARCHAR,USERID),4,2)='01'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                iob.CompanyId = Convert.ToInt16(dr["CMPID"].ToString());
                iob.CompanyUserId = Convert.ToInt16(dr["USERID"].ToString());
                dob.INSERT_ASL_ROLE(iob);
            }
            dr.Close();
            con.Close();
        }

        public void MenuActiveForAllCompanyAdmin(string moduleId, string menuId, string menutp)
        {
            TextBox txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
            iob.LotiLengTudeUpdate = txtLotiLongTude.Text;
            iob.IpAddressUpdate = dbFunctions.IpAddress();
            iob.UserIdUpdate = Convert.ToInt64(Session["USERID"].ToString());
            iob.InTimeUpdate = dbFunctions.Timezone(DateTime.Now);
            iob.ModuleId = moduleId;
            iob.MenuId = menuId;
            iob.MenuType = menutp;
            iob.Status = "A";
            iob.InsertRole = "A";
            iob.UpdateRole = "A";
            iob.DeleteRole = "A";

            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT COMPID, CONVERT(NVARCHAR,COMPID)+'01' AS USERID 
                FROM ASL_COMPANY WHERE COMPID!='100'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                iob.CompanyId = Convert.ToInt16(dr["COMPID"].ToString());
                iob.CompanyUserId = Convert.ToInt16(dr["USERID"].ToString());
                dob.UPDATE_ASL_ROLE(iob);
            }
            dr.Close();
            con.Close();
        }

        protected void txtModuleName_TextChanged(object sender, EventArgs e)
        {
            string checkModuleNmFromDb =
                   dbFunctions.StringData("SELECT MODULENM FROM ASL_MENUMST WHERE MODULENM='" + txtModuleName.Text + "'");
            if (checkModuleNmFromDb == "")
            {
                lblMsg.Text = "Module name not present.";
                lblMsg.Visible = true;
            }
            else
            {
                Session["ModuleId"] =
                   dbFunctions.StringData("SELECT MODULEID FROM ASL_MENUMST WHERE MODULENM='" + txtModuleName.Text + "'");
                lblMsg.Visible = false;
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var txtIp = (TextBox)Master.FindControl("txtIp");
            iob.IpAddressInsert = txtIp.Text;
            iob.UserIdInsert = Convert.ToInt64(Session["USERID"].ToString());
            iob.UserPcInsert = dbFunctions.UserPc();
            iob.InTimeInsert = dbFunctions.Timezone(DateTime.Now);

            var ddlMenuType = (DropDownList)gvDetails.FooterRow.FindControl("ddlMenuType");
            var txtMenuName = (TextBox)gvDetails.FooterRow.FindControl("txtMenuName");
            var txtMenuLink = (TextBox)gvDetails.FooterRow.FindControl("txtMenuLink");
            var txtMenuSerial = (TextBox)gvDetails.FooterRow.FindControl("txtMenuSerial");

            if (e.CommandName.Equals("AddNew"))
            {
                if (ddlMenuType.SelectedValue == "SELECT")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Select from type.";
                    ddlMenuType.Focus();
                }
                else if (txtMenuName.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Insert menu name.";
                    txtMenuName.Focus();
                }
                else if (txtMenuLink.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Insert menu link.";
                    txtMenuLink.Focus();
                }
                else if (txtMenuSerial.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Insert menu Serial.";
                    txtMenuSerial.Focus();
                }
                else
                {
                    string moduleId = Session["ModuleId"].ToString();
                    iob.ModuleId = moduleId;
                    iob.MenuId = MenuId(iob.ModuleId);
                    iob.MenuName = txtMenuName.Text.Trim();
                    iob.MenuType = ddlMenuType.SelectedValue;
                    iob.MenuLink = txtMenuLink.Text;
                    iob.MenuSerial = Convert.ToInt16(txtMenuSerial.Text);
                    string s = dob.INSERT_ASL_MENU(iob);
                    if (s == "")
                    {
                        MenuAddInAslRoleForAllUser(iob.ModuleId, iob.MenuId, iob.MenuType);
                    }
                    GridShowForMenu();

                }
            }

        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }


        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gvDetails.EditIndex = e.NewEditIndex;
                GridShowForMenu();
                var lblModuleIdEdit = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblModuleIdEdit");
                var lblMenuIdEdit = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblMenuIdEdit");
                var ddlMenuTypeEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlMenuTypeEdit");

                ddlMenuTypeEdit.SelectedValue = dbFunctions.StringData(@"SELECT MENUTP FROM ASL_MENU 
                WHERE MODULEID=" + lblModuleIdEdit.Text + " AND MENUID=" + lblMenuIdEdit.Text + "");
                ddlMenuTypeEdit.Focus();
            }
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                iob.UserIdUpdate = Convert.ToInt64(Session["USERID"].ToString());
                iob.IpAddressUpdate = HttpContext.Current.Session["IpAddress"].ToString();
                iob.InTimeUpdate = dbFunctions.Timezone(DateTime.Now);

                var lblModuleIdEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblModuleIdEdit");
                var lblMenuIdEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblMenuIdEdit");

                var ddlMenuTypeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlMenuTypeEdit");
                var txtMenuNameEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMenuNameEdit");
                var txtMenuLinkEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMenuLinkEdit");
                var txtMenuSerialEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMenuSerialEdit");


                if (ddlMenuTypeEdit.SelectedValue == "SELECT")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Select menu type.";
                    ddlMenuTypeEdit.Focus();
                }
                else if (txtMenuNameEdit.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Insert menu name.";
                    txtMenuNameEdit.Focus();
                }
                else if (txtMenuLinkEdit.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Insert menu link.";
                    txtMenuLinkEdit.Focus();
                }
                else if (txtMenuSerialEdit.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Insert menu serial.";
                    txtMenuSerialEdit.Focus();
                }
                else
                {
                    iob.ModuleId = lblModuleIdEdit.Text;
                    iob.MenuId = lblMenuIdEdit.Text;

                    iob.MenuType = ddlMenuTypeEdit.SelectedValue;
                    iob.MenuName = txtMenuNameEdit.Text;
                    iob.MenuLink = txtMenuLinkEdit.Text;
                    iob.MenuSerial = Convert.ToInt16(txtMenuSerialEdit.Text);

                    string s = dob.UPDATE_ASL_MENU(iob);
                    if (s == "")
                    {
                        lblGridMSG.Visible = true;
                        lblGridMSG.Text = "Menu change succesfully.";
                        gvDetails.EditIndex = -1;
                        GridShowForMenu();
                    }
                    else
                    {
                        lblGridMSG.Visible = true;
                        lblGridMSG.Text = "No menu change. please check the required fields";
                    }

                }
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gvDetails.EditIndex = -1;
                GridShowForMenu();
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                var lblModuleId = (Label) gvDetails.Rows[e.RowIndex].FindControl("lblModuleId");
                var lblMenuId = (Label) gvDetails.Rows[e.RowIndex].FindControl("lblMenuId");

                string menuLeft =
                    dbFunctions.StringData("SELECT COUNT(*) CNT FROM ASL_MENU WHERE MODULEID=" + lblModuleId.Text + "");
                iob.MenuId = lblMenuId.Text;
                iob.ModuleId = lblModuleId.Text;
                string s = "";
                if (menuLeft == "0")
                {
                    s = dob.DELETE_ASL_MENU(iob);
                    s = dob.DELETE_ASL_MENUMST(iob);
                }
                else
                {
                    s = dob.DELETE_ASL_MENU(iob);
                }
                if (s == "")
                {
                    gvDetails.EditIndex = -1;
                    GridShowForMenu();
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Deleted Succesfully.";
                }
            }
        }

        protected void GridShowForMenu()
        {

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            string moduleId = Session["ModuleId"].ToString();
            SqlCommand cmd = new SqlCommand("SELECT MODULEID, MENUID, MENUTP, MENUNM, FLINK, MENUSL  FROM ASL_MENU WHERE MODULEID=" + moduleId + "", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();

                var ddlMenuType = (DropDownList)gvDetails.FooterRow.FindControl("ddlMenuType");
                ddlMenuType.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No menu found";
                var ddlMenuType = (DropDownList)gvDetails.FooterRow.FindControl("ddlMenuType");
                ddlMenuType.Focus();
            }

        }
    }
}
