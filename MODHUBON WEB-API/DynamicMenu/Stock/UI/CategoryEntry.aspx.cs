using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogData;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class CategoryEntry : System.Web.UI.Page
    {
        StockDataAcces dob = new StockDataAcces();
        StockInterface iob = new StockInterface();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection con = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                const string formLink = "/Stock/UI/CategoryEntry.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        BindSubCatalogue();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }


        protected void BindSubCatalogue()
        {

            SqlConnection conn = new SqlConnection(dbFunctions.connection);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM STK_ITEMMST WHERE COMPID='101'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Sub.DataSource = ds;
                gv_Sub.DataBind();
                TextBox txtSUBNMfooter = (TextBox)gv_Sub.FooterRow.FindControl("txtSUBNMfooter");
                txtSUBNMfooter.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Sub.DataSource = ds;
                gv_Sub.DataBind();
                int columncount = gv_Sub.Rows[0].Cells.Count;
                gv_Sub.Rows[0].Cells.Clear();
                gv_Sub.Rows[0].Cells.Add(new TableCell());
                gv_Sub.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Sub.Rows[0].Cells[0].Text = "No Records Found";
                TextBox txtSUBNMfooter = (TextBox)gv_Sub.FooterRow.FindControl("txtSUBNMfooter");
                txtSUBNMfooter.Focus();
            }
        }

        private string Generate()
        {
            var maxid = dbFunctions.StringData(@"SELECT MAX(CATID) FROM STK_ITEMMST WHERE COMPID='101'");
            string maxcatid;
            string companyid = "101";
            string subcatid;
            if (maxid == "")
            {
                maxcatid = companyid + "01";
            }
            else
            {
                subcatid = maxid.Substring(3, 2);
                if (Convert.ToInt16(subcatid) < 9)
                {
                    int id = Convert.ToInt16(subcatid) + Convert.ToInt16(1);
                    maxcatid = companyid + "0" + id;
                }
                else if (Convert.ToInt16(subcatid) < 99)
                {
                    int id = Convert.ToInt16(subcatid) + Convert.ToInt16(1);
                    maxcatid = companyid + id;
                }
                else maxcatid = "";
            }
            return maxcatid;
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[0].Text = "101";
                e.Row.Cells[1].Text = Generate();
            }
        }




        protected void txtItemNMEdit_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gv_Sub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.UserID = HttpContext.Current.Session["USERID"].ToString();
            iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.ITime = dbFunctions.Timezone(DateTime.Now);
            
            TextBox txtSUBNMfooter = (TextBox)gv_Sub.FooterRow.FindControl("txtSUBNMfooter");
            TextBox txtSUBNMBanfooter = (TextBox)gv_Sub.FooterRow.FindControl("txtSUBNMBanfooter");

            if (e.CommandName.Equals("Add"))
            {
                if (txtSUBNMfooter.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Sub Category Required.";
                    txtSUBNMfooter.Focus();
                    lblMSG.Visible = false;
                }
                else
                {
                    if (Session["USERID"] == null)
                    {
                        Response.Redirect("~/Login/UI/Login.aspx");
                    }
                    else
                    {
                        lblGridMSG.Visible = false;
                        lblMSG.Visible = false;

                        iob.CompanyId = "101";
                        iob.SubID = Generate();
                        
                        iob.SubNM = txtSUBNMfooter.Text;
                        iob.SubNMBan = txtSUBNMBanfooter.Text;

                        dob.insert_STK_ITEMMST(iob);
                        BindSubCatalogue();
                    }
                }
            }
        }

        protected void gv_Sub_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gv_Sub.EditIndex = -1;
                BindSubCatalogue();
            }
        }

        protected void gv_Sub_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                gv_Sub.EditIndex = e.NewEditIndex;
                BindSubCatalogue();

                TextBox txtSUBNMEdit = (TextBox)gv_Sub.Rows[e.NewEditIndex].FindControl("txtSUBNMEdit");
                txtSUBNMEdit.Focus();
            }
        }

        protected void gv_Sub_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {

                SqlConnection conn = new SqlConnection(dbFunctions.connection);

                Label lblSUBID = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblSUBID");
                Label lblCOMPID = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblCOMPID");


                string countitem = dbFunctions.StringData(@"SELECT COUNT(*) FROM STK_ITEM WHERE CATID='" + lblSUBID.Text + "' AND COMPID='"+ lblCOMPID.Text + "'");

                if (countitem == "0")
                {
                    try
                    {
                        // logdata add start //
                        string lotileng = HttpContext.Current.Session["Location"].ToString();
                        string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),
                        CATID,103)+'  '+ISNULL(CATNM,'(NULL)')+'  '+ISNULL(CATNMB,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                        ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERPC,103),'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                        ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_ITEMMST 
                        WHERE CATID='" + lblSUBID.Text + "' AND COMPID='" + lblCOMPID.Text + "'");
                        string logid = "DELETE";
                        string tableid = "STK_ITEMMST";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                    catch (Exception)
                    {

                    }

                    int result = 0;
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete FROM STK_ITEMMST where CATID = '" + lblSUBID.Text + "' AND COMPID='" + lblCOMPID.Text + "'", conn);
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        BindSubCatalogue();
                    }
                }
                else
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "You don't delete the category. This category has " + countitem + " items";
                }
            }

        }

        protected void gv_Sub_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtSUBNMEdit = (TextBox)gv_Sub.Rows[e.RowIndex].FindControl("txtSUBNMEdit");
            TextBox txtSUBNMBanEdit = (TextBox)gv_Sub.Rows[e.RowIndex].FindControl("txtSUBNMBanEdit");

            if (txtSUBNMEdit.Text == "")
            {
                lblGridMSG.Visible = true;
                lblGridMSG.Text = "Input Sub Gategory Name";
                txtSUBNMEdit.Focus();
                lblMSG.Visible = false;
            }
            else
            {
                iob.UpdUserID = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UpdTime = dbFunctions.Timezone(DateTime.Now);
                if (Session["USERID"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    lblGridMSG.Visible = false;
                    lblMSG.Visible = false;
                    SqlConnection conn = new SqlConnection(dbFunctions.connection);
                    DateTime updTime = dbFunctions.Timezone(DateTime.Now);

                    Label lblSUBIDEdit = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblSUBIDEdit");
                    Label lblCOMPIDEdit = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblCOMPIDEdit");

                    // SqlCommand cmd = new SqlCommand("update STK_ITEMMST set SUBNM=@SUBNM where SUBID=@SUBID", conn);

                    iob.CompanyId = lblCOMPIDEdit.Text;
                    iob.SubID = lblSUBIDEdit.Text;
                    iob.SubNM = txtSUBNMEdit.Text;
                    iob.SubNMBan = txtSUBNMBanEdit.Text;

                    try
                    {
                        // logdata add start //
                        string lotileng = HttpContext.Current.Session["Location"].ToString();
                        string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),
                        CATID,103)+'  '+ISNULL(CATNM,'(NULL)')+'  '+ISNULL(CATNMB,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                        ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERPC,103),'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                        ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_ITEMMST 
                        WHERE CATID='" + lblSUBIDEdit.Text + "' AND COMPID='" + lblCOMPIDEdit.Text + "'");
                        string logid = "UPDATE";
                        string tableid = "STK_ITEMMST";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                    catch (Exception)
                    {

                    }

                    dob.update_STK_ITEMMST(iob);
                    gv_Sub.EditIndex = -1;
                    BindSubCatalogue();
                }
            }
        }

    }
}