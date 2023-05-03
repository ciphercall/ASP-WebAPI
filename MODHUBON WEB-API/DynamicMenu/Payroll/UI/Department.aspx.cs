using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class Department : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        PayrollDataAcces dob = new PayrollDataAcces();
        PayrollInterface iob = new PayrollInterface();
        SqlConnection conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/Department.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        gridShow();
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }

        private void gridShow()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_DEPT", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_DEPT.DataSource = ds;
                gv_DEPT.DataBind();
                TextBox txtDEPTNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTNMFooter");
                txtDEPTNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_DEPT.DataSource = ds;
                gv_DEPT.DataBind();
                int columncount = gv_DEPT.Rows[0].Cells.Count;
                gv_DEPT.Rows[0].Cells.Clear();
                gv_DEPT.Rows[0].Cells.Add(new TableCell());
                gv_DEPT.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_DEPT.Rows[0].Visible = false;
                TextBox txtDEPTNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTNMFooter");
                txtDEPTNMFooter.Focus();
            }
        }
        private void DEPTID()
        {
            string CMPID = int.Parse("101").ToString();
            string DEPTID = "";
            dbFunctions.lblAdd("SELECT MAX(DEPTID) FROM HR_DEPT WHERE COMPID='" + CMPID + "'", lblDEPTID);
            if (lblDEPTID.Text == "")
            {
                DEPTID = CMPID + "01";
            }
            else
            {
                string Substr = lblDEPTID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    DEPTID = CMPID + "0" + subint;
                }
                else if (subint < 100)
                {
                    DEPTID = CMPID + subint;
                }
            }
            iob.DEPTID = int.Parse(DEPTID);
        }
        protected void gv_DEPT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.Username = HttpContext.Current.Session["USERID"].ToString();
            iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.ITime =dbFunctions.Timezone(DateTime.Now);
            try
            {
                if (Session["USERID"] == null)
                    Response.Redirect("~/Login/UI/Login.aspx");
                else
                {
                    var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                    var lblDEPTID = (Label)gv_DEPT.FooterRow.FindControl("lblDEPTID");
                    var txtDEPTNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTNMFooter");
                    var txtREMARKSFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtDEPTNMFooter.Text == "")
                        {
                            txtDEPTNMFooter.Focus();
                        }
                        else
                        {
                            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            iob.UserID = int.Parse(Session["USERID"].ToString());
                            DEPTID();
                            iob.DEPTNM = txtDEPTNMFooter.Text;
                            iob.Ltude = txtLotiLongTude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_DEPT(iob);
                            gridShow();
                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gv_DEPT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DEPTID();
                e.Row.Cells[0].Text = iob.DEPTID.ToString();
            }
        }

        protected void gv_DEPT_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_DEPT.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_DEPT_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Label lblDEPTID = (Label)gv_DEPT.Rows[e.RowIndex].FindControl("lblDEPTID");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_DEPT WHERE DEPTID = '" + lblDEPTID.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    gridShow();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }

            }
        }

        protected void gv_DEPT_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_DEPT.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtDEPTNMEdit = (TextBox)gv_DEPT.Rows[e.NewEditIndex].FindControl("txtDEPTNMEdit");
                txtDEPTNMEdit.Focus();
            }
        }

        protected void gv_DEPT_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UPDUsername = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDTime =dbFunctions.Timezone(DateTime.Now);
                TextBox txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                Label lblDEPTID = (Label)gv_DEPT.Rows[e.RowIndex].FindControl("lblDEPTID");
                TextBox txtDEPTNMEdit = (TextBox)gv_DEPT.Rows[e.RowIndex].FindControl("txtDEPTNMEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_DEPT.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (txtDEPTNMEdit.Text == "")
                    txtDEPTNMEdit.Focus();
                else
                {
                    iob.DEPTID = int.Parse(lblDEPTID.Text);
                    iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    iob.UserID = int.Parse(Session["USERID"].ToString());
                    iob.DEPTNM = txtDEPTNMEdit.Text;
                    iob.UPDLtude = txtLotiLongTude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_DEPT(iob);
                    gv_DEPT.EditIndex = -1;
                    gridShow();
                }



            }
        }
    }
}