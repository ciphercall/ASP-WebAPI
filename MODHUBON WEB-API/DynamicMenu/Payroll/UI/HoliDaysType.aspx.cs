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
    public partial class HoliDaysType : System.Web.UI.Page
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
                const string formLink = "/Payroll/UI/HoliDaysType.aspx";
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
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_HDAYTP", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_HDAYTP.DataSource = ds;
                gv_HDAYTP.DataBind();
                TextBox txtHDAYTPNMFooter = (TextBox)gv_HDAYTP.FooterRow.FindControl("txtHDAYTPNMFooter");
                txtHDAYTPNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_HDAYTP.DataSource = ds;
                gv_HDAYTP.DataBind();
                int columncount = gv_HDAYTP.Rows[0].Cells.Count;
                gv_HDAYTP.Rows[0].Cells.Clear();
                gv_HDAYTP.Rows[0].Cells.Add(new TableCell());
                gv_HDAYTP.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_HDAYTP.Rows[0].Visible = false;
                TextBox txtHDAYTPNMFooter = (TextBox)gv_HDAYTP.FooterRow.FindControl("txtHDAYTPNMFooter");
                txtHDAYTPNMFooter.Focus();
            }
        }
        private void HDAYTPID()
        {
            string CMPID = int.Parse("101").ToString();
            string HDAYTPID = "";
            dbFunctions.lblAdd("SELECT MAX(HDAYID) FROM HR_HDAYTP  WHERE COMPID='" + CMPID + "'", lblHDAYTPID);

            if (lblHDAYTPID.Text == "")
            {
                HDAYTPID = CMPID + "01";
            }
            else
            {
                string Substr = lblHDAYTPID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    HDAYTPID = CMPID + "0" + subint;
                }
                else if (subint < 100)
                {
                    HDAYTPID = CMPID + subint;
                }
            }
            iob.HDAYID = int.Parse(HDAYTPID);
        }
        protected void gv_HDAYTP_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    TextBox txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                    Label lblHDAYTPID = (Label)gv_HDAYTP.FooterRow.FindControl("lblHDAYTPID");
                    TextBox txtHDAYTPNMFooter = (TextBox)gv_HDAYTP.FooterRow.FindControl("txtHDAYTPNMFooter");
                    TextBox txtREMARKSFooter = (TextBox)gv_HDAYTP.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtHDAYTPNMFooter.Text == "")
                        {
                            txtHDAYTPNMFooter.Focus();
                        }
                        else
                        {
                            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            iob.UserID = int.Parse(Session["USERID"].ToString());
                            HDAYTPID();
                            iob.HDAYNM = txtHDAYTPNMFooter.Text;
                            iob.Ltude = txtLotiLongTude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_HDAYTP(iob);
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

        protected void gv_HDAYTP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                HDAYTPID();
                e.Row.Cells[0].Text = iob.HDAYID.ToString();
            }
        }

        protected void gv_HDAYTP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_HDAYTP.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_HDAYTP_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Label lblHDAYTPID = (Label)gv_HDAYTP.Rows[e.RowIndex].FindControl("lblHDAYTPID");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_HDAYTP WHERE HDAYID = '" + lblHDAYTPID.Text + "'", conn);
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

        protected void gv_HDAYTP_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_HDAYTP.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtHDAYTPNMEdit = (TextBox)gv_HDAYTP.Rows[e.NewEditIndex].FindControl("txtHDAYTPNMEdit");
                txtHDAYTPNMEdit.Focus();
            }
        }

        protected void gv_HDAYTP_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UPDUsername = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDTime =dbFunctions.Timezone(DateTime.Now);

                TextBox txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                Label lblHDAYTPID = (Label)gv_HDAYTP.Rows[e.RowIndex].FindControl("lblHDAYTPID");
                TextBox txtHDAYTPNMEdit = (TextBox)gv_HDAYTP.Rows[e.RowIndex].FindControl("txtHDAYTPNMEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_HDAYTP.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (txtHDAYTPNMEdit.Text == "")
                    txtHDAYTPNMEdit.Focus();
                else
                {
                    iob.HDAYID = int.Parse(lblHDAYTPID.Text);
                    iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    iob.UserID = int.Parse(Session["USERID"].ToString());
                    iob.HDAYNM = txtHDAYTPNMEdit.Text;
                    iob.UPDLtude = txtLotiLongTude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_HDAYTP(iob);
                    gv_HDAYTP.EditIndex = -1;
                    gridShow();
                }



            }
        }
    }
}