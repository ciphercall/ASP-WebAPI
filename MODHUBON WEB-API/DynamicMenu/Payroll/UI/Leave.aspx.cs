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
    public partial class Leave : System.Web.UI.Page
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
                const string formLink = "/Payroll/UI/Leave.aspx";
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
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_LEAVE", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_LEAVE.DataSource = ds;
                gv_LEAVE.DataBind();
                TextBox txtLEAVENMFooter = (TextBox)gv_LEAVE.FooterRow.FindControl("txtLEAVENMFooter");
                txtLEAVENMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_LEAVE.DataSource = ds;
                gv_LEAVE.DataBind();
                int columncount = gv_LEAVE.Rows[0].Cells.Count;
                gv_LEAVE.Rows[0].Cells.Clear();
                gv_LEAVE.Rows[0].Cells.Add(new TableCell());
                gv_LEAVE.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_LEAVE.Rows[0].Visible = false;
                TextBox txtLEAVENMFooter = (TextBox)gv_LEAVE.FooterRow.FindControl("txtLEAVENMFooter");
                txtLEAVENMFooter.Focus();
            }
        }
        private void LEAVEID()
        {
            string CMPID = int.Parse("101").ToString();
            string LEAVEID = "";
            dbFunctions.lblAdd("SELECT MAX(LEAVEID) FROM HR_LEAVE  WHERE COMPID='" + CMPID + "'", lblLEAVEID);

            if (lblLEAVEID.Text == "")
            {
                LEAVEID = CMPID + "01";
            }
            else
            {
                string Substr = lblLEAVEID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    LEAVEID = CMPID + "0" + subint;
                }
                else if (subint < 100)
                {
                    LEAVEID = CMPID + subint;
                }
            }
            iob.LEAVEID = int.Parse(LEAVEID);
        }
        protected void gv_LEAVE_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    var lblLEAVEID = (Label)gv_LEAVE.FooterRow.FindControl("lblLEAVEID");
                    var txtLEAVENMFooter = (TextBox)gv_LEAVE.FooterRow.FindControl("txtLEAVENMFooter");
                    var txtREMARKSFooter = (TextBox)gv_LEAVE.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtLEAVENMFooter.Text == "")
                        {
                            txtLEAVENMFooter.Focus();
                        }
                        else
                        {
                            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            iob.UserID = int.Parse(Session["USERID"].ToString());
                            LEAVEID();
                            iob.LEAVENM = txtLEAVENMFooter.Text;
                            iob.Ltude = txtLotiLongTude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_LEAVE(iob);
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

        protected void gv_LEAVE_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                LEAVEID();
                e.Row.Cells[0].Text = iob.LEAVEID.ToString();
            }
        }

        protected void gv_LEAVE_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_LEAVE.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_LEAVE_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Label lblLEAVEID = (Label)gv_LEAVE.Rows[e.RowIndex].FindControl("lblLEAVEID");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_LEAVE WHERE LEAVEID = '" + lblLEAVEID.Text + "'", conn);
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

        protected void gv_LEAVE_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_LEAVE.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtLEAVENMEdit = (TextBox)gv_LEAVE.Rows[e.NewEditIndex].FindControl("txtLEAVENMEdit");
                txtLEAVENMEdit.Focus();
            }
        }

        protected void gv_LEAVE_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                var lblLEAVEID = (Label)gv_LEAVE.Rows[e.RowIndex].FindControl("lblLEAVEID");
                var txtLEAVENMEdit = (TextBox)gv_LEAVE.Rows[e.RowIndex].FindControl("txtLEAVENMEdit");
                var txtREMARKSEdit = (TextBox)gv_LEAVE.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (txtLEAVENMEdit.Text == "")
                    txtLEAVENMEdit.Focus();
                else
                {
                    iob.LEAVEID = int.Parse(lblLEAVEID.Text);
                    iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    iob.UserID = int.Parse(Session["USERID"].ToString());
                    iob.LEAVENM = txtLEAVENMEdit.Text;
                    iob.UPDLtude = txtLotiLongTude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_LEAVE(iob);
                    gv_LEAVE.EditIndex = -1;
                    gridShow();
                }



            }
        }
    }
}