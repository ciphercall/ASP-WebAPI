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
    public partial class Shift : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_SHIFT", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_SHIFT.DataSource = ds;
                gv_SHIFT.DataBind();
                TextBox txtSHIFTNMFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtSHIFTNMFooter");
                txtSHIFTNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_SHIFT.DataSource = ds;
                gv_SHIFT.DataBind();
                int columncount = gv_SHIFT.Rows[0].Cells.Count;
                gv_SHIFT.Rows[0].Cells.Clear();
                gv_SHIFT.Rows[0].Cells.Add(new TableCell());
                gv_SHIFT.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_SHIFT.Rows[0].Visible = false;
                TextBox txtSHIFTNMFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtSHIFTNMFooter");
                txtSHIFTNMFooter.Focus();
            }
        }
        private void SHIFTID()
        {

            dbFunctions.lblAdd("SELECT MAX(SHIFTID) FROM HR_SHIFT", lblSHIFTID);
            string SHIFTID = "";
            if (lblSHIFTID.Text == "")
            {
                SHIFTID = Session["COMPANYID"].ToString() + "01";
            }
            else
            {
                string Substr = lblSHIFTID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    SHIFTID = Session["COMPANYID"].ToString() + "0" + subint;
                }
                else if (subint < 100)
                {
                    SHIFTID = Session["COMPANYID"].ToString() + subint;
                }
            }
            iob.SHIFTID = int.Parse(SHIFTID);
        }
        protected void gv_SHIFT_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblSHIFTID = (Label)gv_SHIFT.FooterRow.FindControl("lblSHIFTID");
                    TextBox txtSHIFTNMFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtSHIFTNMFooter");
                    TextBox txtREMARKSFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtREMARKSFooter");
                    TextBox txtTIMEFRFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtTIMEFRFooter");
                    TextBox txtTIMETOFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtTIMETOFooter");
                    TextBox txtLATEFRFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtLATEFRFooter");
                    TextBox txtOTFROMFooter = (TextBox)gv_SHIFT.FooterRow.FindControl("txtOTFROMFooter");

                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtSHIFTNMFooter.Text == "")
                        {
                            txtSHIFTNMFooter.Focus();
                        }
                        else
                        {
                            TimeSpan TimeFR = TimeSpan.Parse(txtTIMEFRFooter.Text);
                            TimeSpan TimeTO = TimeSpan.Parse(txtTIMETOFooter.Text);

                            TimeSpan LateFR = TimeSpan.Parse(txtLATEFRFooter.Text);
                            TimeSpan OTFrom = TimeSpan.Parse(txtOTFROMFooter.Text);
                            iob.LateFR = LateFR;
                            iob.OTFrom = OTFrom;

                            iob.TimeFR = TimeFR;
                            iob.TimeTO = TimeTO;
                            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            iob.UserID = int.Parse(Session["USERID"].ToString());
                            SHIFTID();
                            iob.SHIFTNM = txtSHIFTNMFooter.Text;
                            iob.Ltude = txtLotiLongTude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_SHIFT(iob);
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
        protected void gv_SHIFT_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                Label lblSHIFTID = (Label)gv_SHIFT.Rows[e.RowIndex].FindControl("lblSHIFTID");
                TextBox txtSHIFTNMEdit = (TextBox)gv_SHIFT.Rows[e.RowIndex].FindControl("txtSHIFTNMEdit");
                TextBox txtTIMEFREdit = (TextBox)gv_SHIFT.Rows[e.RowIndex].FindControl("txtTIMEFREdit");
                TextBox txtTIMETOEdit = (TextBox)gv_SHIFT.Rows[e.RowIndex].FindControl("txtTIMETOEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_SHIFT.Rows[e.RowIndex].FindControl("txtREMARKSEdit");

                TextBox txtLATEFREdit = (TextBox)gv_SHIFT.Rows[e.RowIndex].FindControl("txtLATEFREdit");
                TextBox txtOTFROMEdit = (TextBox)gv_SHIFT.Rows[e.RowIndex].FindControl("txtOTFROMEdit");
                if (txtSHIFTNMEdit.Text == "")
                    txtSHIFTNMEdit.Focus();
                else
                {
                    TimeSpan TimeFR = TimeSpan.Parse(txtTIMEFREdit.Text);
                    TimeSpan TimeTO = TimeSpan.Parse(txtTIMETOEdit.Text);

                    TimeSpan LateFR = TimeSpan.Parse(txtLATEFREdit.Text);
                    TimeSpan OTFrom = TimeSpan.Parse(txtOTFROMEdit.Text);
                    iob.LateFR = LateFR;
                    iob.OTFrom = OTFrom;

                    iob.TimeFR = TimeFR;
                    iob.TimeTO = TimeTO;
                    iob.SHIFTID = int.Parse(lblSHIFTID.Text);
                    iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    iob.UserID = int.Parse(Session["USERID"].ToString());
                    iob.SHIFTNM = txtSHIFTNMEdit.Text;
                    iob.UPDLtude = txtLotiLongTude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_SHIFT(iob);
                    gv_SHIFT.EditIndex = -1;
                    gridShow();
                }



            }
        }

        protected void gv_SHIFT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                SHIFTID();
                e.Row.Cells[0].Text = iob.SHIFTID.ToString();
            }
        }

        protected void gv_SHIFT_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_SHIFT.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_SHIFT_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Label lblSHIFTID = (Label)gv_SHIFT.Rows[e.RowIndex].FindControl("lblSHIFTID");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_SHIFT WHERE SHIFTID = '" + lblSHIFTID.Text + "'", conn);
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

        protected void gv_SHIFT_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_SHIFT.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtSHIFTNMEdit = (TextBox)gv_SHIFT.Rows[e.NewEditIndex].FindControl("txtSHIFTNMEdit");
                txtSHIFTNMEdit.Focus();
            }
        }
    }
}