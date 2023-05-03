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
    public partial class HoliDays : System.Web.UI.Page
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
                const string formLink = "/Payroll/UI/HoliDays.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!Page.IsPostBack)
                    {
                        gridShow();
                        string yr =dbFunctions.Timezone(DateTime.Now).ToString("yyyy");
                        int i, m;
                        int a = int.Parse(yr);
                        m = a + 5;
                        for (i = a - 5; i <= m; i++)
                        {
                            ddlYR.Items.Add(i.ToString());
                        }
                        ddlYR.Text =dbFunctions.Timezone(DateTime.Now).ToString("yyyy");
                        ddlYR.Focus();
                        
                    }
                }
                else
                {
                    Response.Redirect("../../DeshBoard/UI/Default.aspx");
                }
            }
        }
        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionHoliNM(string prefixText, int count, string contextKey)
        //{

        //    SqlConnection conn = new SqlConnection(dbFunctions.connection);
        //    SqlCommand cmd = new SqlCommand("SELECT HDAYNM FROM HR_HDAYTP WHERE HDAYNM like '" + prefixText + "%'", conn);
        //    SqlDataReader oReader;
        //    conn.Open();
        //    List<String> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (oReader.Read())
        //        CompletionSet.Add(oReader["HDAYNM"].ToString());
        //    return CompletionSet.ToArray();
        //}

        private void gridShow()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT CONVERT(NVARCHAR(10),HR_HOLIDAYS.TRANSDT,103) AS TRANSDT, HR_HDAYTP.HDAYNM, HR_HOLIDAYS.HDAYID, HR_HOLIDAYS.STATUS, HR_HOLIDAYS.REMARKS
                                                FROM   HR_HDAYTP INNER JOIN
                      HR_HOLIDAYS ON HR_HDAYTP.HDAYID = HR_HOLIDAYS.HDAYID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_HDAY.DataSource = ds;
                gv_HDAY.DataBind();
                TextBox txtTRANSDTFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtTRANSDTFooter");
                txtTRANSDTFooter.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_HDAY.DataSource = ds;
                gv_HDAY.DataBind();
                int columncount = gv_HDAY.Rows[0].Cells.Count;
                gv_HDAY.Rows[0].Cells.Clear();
                gv_HDAY.Rows[0].Cells.Add(new TableCell());
                gv_HDAY.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_HDAY.Rows[0].Visible = false;
                TextBox txtTRANSDTFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtTRANSDTFooter");
                txtTRANSDTFooter.Focus();
            }
        }

        protected void gv_HDAY_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    if (e.CommandName.Equals("Add"))
                    {
                        TextBox txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                        Label lblHDAYIDFooter = (Label)gv_HDAY.FooterRow.FindControl("lblHDAYIDFooter");
                        TextBox txtTRANSDTFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtTRANSDTFooter");
                        TextBox txtHDAYNMFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtHDAYNMFooter");
                        DropDownList txtSTATUSFooter = (DropDownList)gv_HDAY.FooterRow.FindControl("txtSTATUSFooter");
                        TextBox txtREMARKSFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtREMARKSFooter");

                        if (txtTRANSDTFooter.Text == "")
                            txtTRANSDTFooter.Focus();
                        else if (txtHDAYNMFooter.Text == "")
                            txtHDAYNMFooter.Focus();
                        else
                        {

                            iob.TransYr = int.Parse(ddlYR.Text);
                            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            iob.UserID = int.Parse(Session["USERID"].ToString());
                            iob.TransDT = DateTime.Parse(txtTRANSDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.HDAYID = int.Parse(lblHDAYIDFooter.Text);
                            iob.Status = txtSTATUSFooter.Text;
                            iob.Ltude = txtLotiLongTude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_HDAY(iob);
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
        protected void gv_HDAY_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                Label lblHDAYIDEdit = (Label)gv_HDAY.Rows[e.RowIndex].FindControl("lblHDAYIDEdit");
                TextBox txtHDAYNMEdit = (TextBox)gv_HDAY.Rows[e.RowIndex].FindControl("txtHDAYNMEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_HDAY.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                DropDownList txtSTATUSEdit = (DropDownList)gv_HDAY.Rows[e.RowIndex].FindControl("txtSTATUSEdit");
                TextBox txtTRANSDTEdit = (TextBox)gv_HDAY.Rows[e.RowIndex].FindControl("txtTRANSDTEdit");
                if (txtTRANSDTEdit.Text == "")
                    txtTRANSDTEdit.Focus();
                else if (txtHDAYNMEdit.Text == "")
                    txtHDAYNMEdit.Focus();
                else
                {
                    DateTime Date = DateTime.Parse(txtTRANSDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.TransYr = int.Parse(ddlYR.Text);
                    iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    iob.UserID = int.Parse(Session["USERID"].ToString());
                    iob.TransDT = Date;
                    iob.Status = txtSTATUSEdit.Text;
                    iob.HDAYID = int.Parse(lblHDAYIDEdit.Text);
                    iob.UPDLtude = txtLotiLongTude.Text;;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_HDAY(iob);
                    gv_HDAY.EditIndex = -1;
                    gridShow();
                }
            }
        }

        protected void gv_HDAY_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_HDAY.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_HDAY_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Label lblHDAYID = (Label)gv_HDAY.Rows[e.RowIndex].FindControl("lblHDAYID");
                    Label lblTRANSDT = (Label)gv_HDAY.Rows[e.RowIndex].FindControl("lblTRANSDT");
                    DateTime td =dbFunctions.Timezone(DateTime.Parse(lblTRANSDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                    string date = td.ToString("yyyy-MM-dd");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_HOLIDAYS WHERE HDAYID = '" + lblHDAYID.Text + "' AND TRANSDT='" + date + "'", conn);
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
        protected void gv_HDAY_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_HDAY.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtTRANSDTEdit = (TextBox)gv_HDAY.Rows[e.NewEditIndex].FindControl("txtTRANSDTEdit");
                DropDownList txtSTATUSEdit = (DropDownList)gv_HDAY.Rows[e.NewEditIndex].FindControl("txtSTATUSEdit");
                string com = Session["CMPID"].ToString();
                string date = DateTime.Parse(txtTRANSDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");
                Label lblStatus = new Label();
                dbFunctions.lblAdd("Select STATUS from HR_HOLIDAYS where COMPID='" + com + "' and TRANSDT='" + date + "'", lblStatus);

                txtSTATUSEdit.Text = lblStatus.Text;


                txtTRANSDTEdit.Focus();
            }
        }
        protected void txtHDAYNMFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtHDAYNMFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtHDAYNMFooter");
            DropDownList txtSTATUSFooter = (DropDownList)gv_HDAY.FooterRow.FindControl("txtSTATUSFooter");
            Label lblHDAYIDFooter = (Label)gv_HDAY.FooterRow.FindControl("lblHDAYIDFooter");
            if (txtHDAYNMFooter.Text == "")
            {
                txtHDAYNMFooter.Focus();
            }
            else
            {
                dbFunctions.lblAdd("SELECT HDAYID FROM HR_HDAYTP WHERE HDAYNM='" + txtHDAYNMFooter.Text + "'", lblHDAYIDFooter);
                txtSTATUSFooter.Focus();
            }

        }
        protected void txtHDAYNMTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtTRANSDTEdit = (TextBox)row.FindControl("txtTRANSDTEdit");
            TextBox txtHDAYNMEdit = (TextBox)row.FindControl("txtHDAYNMEdit");
            DropDownList txtSTATUSEdit = (DropDownList)row.FindControl("txtSTATUSEdit");
            if (txtHDAYNMEdit.Text == "")
            {
                txtHDAYNMEdit.Focus();
            }
            else
            {
                txtSTATUSEdit.Focus();
            }
        }
        protected void txtTRANSDTFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtTRANSDTFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtTRANSDTFooter");
            TextBox txtHDAYNMFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtHDAYNMFooter");
            if (txtTRANSDTFooter.Text == "")
            {
                txtTRANSDTFooter.Focus();
            }
            else
            {
                txtHDAYNMFooter.Focus();
            }
        }
        protected void txtTRANSDTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtTRANSDTEdit = (TextBox)row.FindControl("txtTRANSDTEdit");
            TextBox txtHDAYNMEdit = (TextBox)row.FindControl("txtHDAYNMEdit");
            DropDownList txtSTATUSEdit = (DropDownList)row.FindControl("txtSTATUSEdit");
            Label lblHDAYID = (Label)row.FindControl("lblHDAYID");
            if (txtTRANSDTEdit.Text == "")
            {
                txtTRANSDTEdit.Focus();
            }
            else
            {
                dbFunctions.lblAdd("SELECT HDAYID FROM HR_HDAYTP WHERE HDAYNM='" + txtHDAYNMEdit.Text + "'", lblHDAYID);
                txtSTATUSEdit.Focus();
            }
        }
        protected void ddlYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox txtTRANSDTFooter = (TextBox)gv_HDAY.FooterRow.FindControl("txtTRANSDTFooter");
            txtTRANSDTFooter.Focus();
        }

        protected void gv_HDAY_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}