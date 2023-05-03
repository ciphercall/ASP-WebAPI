using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Payroll.DataAccess;
using DynamicMenu.Payroll.Interface;

namespace DynamicMenu.Payroll.UI
{
    public partial class LeaveYear : Page
    {
        readonly PayrollDataAcces _dob = new PayrollDataAcces();
        readonly PayrollInterface _iob = new PayrollInterface();
        readonly SqlConnection _conn = new SqlConnection(dbFunctions.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                const string formLink = "/Payroll/UI/LeaveYear.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!Page.IsPostBack)
                    {
                        GridShow();
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
        private void GridShow()
        {

            _conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     HR_LEAVE.LEAVENM, HR_LEAVEYY.LEAVEID, CONVERT(nvarchar(10),HR_LEAVEYY.LEAVEDD,103) AS LEAVEDD, HR_LEAVEYY.REMARKS
FROM         HR_LEAVEYY INNER JOIN
                      HR_LEAVE ON HR_LEAVEYY.LEAVEID = HR_LEAVE.LEAVEID", _conn);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            _conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_LEAVEYY.DataSource = ds;
                gv_LEAVEYY.DataBind();
                TextBox txtLeavenmFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVENMFooter");
                txtLeavenmFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_LEAVEYY.DataSource = ds;
                gv_LEAVEYY.DataBind();
                int columncount = gv_LEAVEYY.Rows[0].Cells.Count;
                gv_LEAVEYY.Rows[0].Cells.Clear();
                gv_LEAVEYY.Rows[0].Cells.Add(new TableCell());
                gv_LEAVEYY.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_LEAVEYY.Rows[0].Visible = false;
                TextBox txtLeavenmFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVENMFooter");
                txtLeavenmFooter.Focus();
            }
        }

        protected void gv_LEAVEYY_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            _iob.Username = HttpContext.Current.Session["USERID"].ToString();
            _iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            _iob.ITime =dbFunctions.Timezone(DateTime.Now);
            try
            {
                if (Session["USERID"] == null)
                    Response.Redirect("~/Login/UI/Login.aspx");
                else
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                    Label lblLeaveidFooter = (Label)gv_LEAVEYY.FooterRow.FindControl("lblLEAVEIDFooter");
                    TextBox txtLeavenmFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVENMFooter");
                    TextBox txtLeaveddFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVEDDFooter");
                    TextBox txtRemarksFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (lblLeaveidFooter.Text == "")
                            txtLeavenmFooter.Focus();
                        else if (txtLeaveddFooter.Text == "")
                            txtLeaveddFooter.Focus();
                        else
                        {
                            //DateTime DATE = DateTime.Parse(txtLEAVEDDFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            _iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            _iob.UserID = int.Parse(Session["USERID"].ToString());
                            // LEAVEYYID();
                            _iob.LEAVEID = int.Parse(lblLeaveidFooter.Text);
                            _iob.LEAVEYR = int.Parse(ddlYR.Text);
                            _iob.LEAVEDD = int.Parse(txtLeaveddFooter.Text);
                            _iob.Ltude = txtLotiLongTude.Text;
                            _iob.Remarks = txtRemarksFooter.Text;
                            _dob.Insert_HR_LEAVEYY(_iob);
                            GridShow();
                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        protected void gv_LEAVEYY_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                _iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                _iob.UPDUsername = HttpContext.Current.Session["USERID"].ToString();
                _iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                _iob.UPDTime =dbFunctions.Timezone(DateTime.Now);
                // ReSharper disable once PossibleNullReferenceException
                var txtLotiLongTude = (TextBox)Master.FindControl("txtLotiLongTude");
                Label lblLeaveid = (Label)gv_LEAVEYY.Rows[e.RowIndex].FindControl("lblLEAVEID");
                TextBox txtLeaveddEdit = (TextBox)gv_LEAVEYY.Rows[e.RowIndex].FindControl("txtLEAVEDDEdit");
                TextBox txtLeavenmFooter = (TextBox)gv_LEAVEYY.Rows[e.RowIndex].FindControl("txtLEAVENMFooter");
                TextBox txtRemarksEdit = (TextBox)gv_LEAVEYY.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (lblLeaveid.Text == "")
                    txtLeavenmFooter.Focus();
                else if (txtLeaveddEdit.Text == "")
                    txtLeaveddEdit.Focus();
                else
                {
                    //   DateTime DATE = DateTime.Parse(txtLEAVEDDEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    _iob.LEAVEYR = int.Parse(ddlYR.Text);
                    _iob.LEAVEID = int.Parse(lblLeaveid.Text);
                    _iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    _iob.UserID = int.Parse(Session["USERID"].ToString());
                    _iob.LEAVEDD = int.Parse(txtLeaveddEdit.Text);
                    _iob.UPDLtude = txtLotiLongTude.Text;
                    _iob.Remarks = txtRemarksEdit.Text;
                    _dob.Update_HR_LEAVEYY(_iob);
                    gv_LEAVEYY.EditIndex = -1;
                    GridShow();
                }



            }
        }



        protected void gv_LEAVEYY_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_LEAVEYY.EditIndex = -1;
                GridShow();
            }
        }

        protected void gv_LEAVEYY_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    _iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    _iob.UserID = int.Parse(Session["USERID"].ToString());
                    Label lblLeaveid = (Label)gv_LEAVEYY.Rows[e.RowIndex].FindControl("lblLEAVEID");
                    _conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_LEAVEYY WHERE LEAVEID = '" + lblLeaveid.Text + "' AND LEAVEYY='" + ddlYR.Text + "' AND COMPID='" + _iob.CmpID + "'", _conn);
                    cmd.ExecuteNonQuery();
                    _conn.Close();
                    GridShow();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }

            }
        }

        protected void gv_LEAVEYY_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_LEAVEYY.EditIndex = e.NewEditIndex;
                GridShow();
                TextBox txtLeavenmEdit = (TextBox)gv_LEAVEYY.Rows[e.NewEditIndex].FindControl("txtLEAVENMEdit");
                txtLeavenmEdit.Focus();
            }
        }


        protected void ddlYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox txtLeavenmFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVENMFooter");
            txtLeavenmFooter.Focus();
        }

        protected void txtLEAVENMFooter_TextChanged(object sender, EventArgs e)
        {
            Label lblLeaveidFooter = (Label)gv_LEAVEYY.FooterRow.FindControl("lblLEAVEIDFooter");
            TextBox txtLeavenmFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVENMFooter");
            TextBox txtLeaveddFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVEDDFooter");
            if (txtLeavenmFooter.Text == "")
                txtLeavenmFooter.Focus();
            else
            {
                dbFunctions.lblAdd("SELECT LEAVEID FROM HR_LEAVE WHERE LEAVENM='" + txtLeavenmFooter.Text + "'", lblLeaveidFooter);
                txtLeaveddFooter.Focus();

            }

        }

        protected void txtLEAVENMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtLeavenmEdit = (TextBox)row.FindControl("txtLEAVENMEdit");
            TextBox txtLeaveddEdit = (TextBox)row.FindControl("txtLEAVEDDEdit");
            Label lblLeaveid = (Label)row.FindControl("lblLEAVEID");

            if (txtLeavenmEdit.Text == "")
                txtLeavenmEdit.Focus();
            else
            {
                dbFunctions.lblAdd("SELECT LEAVEID FROM HR_LEAVE WHERE LEAVENM='" + txtLeavenmEdit.Text + "'", lblLeaveid);
                txtLeaveddEdit.Focus();
            }
        }
        protected void txtLEAVEDDFooter_TextChanged(object sender, EventArgs e)
        {

            TextBox txtRemarksFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtREMARKSFooter");
            TextBox txtLeaveddFooter = (TextBox)gv_LEAVEYY.FooterRow.FindControl("txtLEAVEDDFooter");
            if (txtLeaveddFooter.Text == "")
                txtLeaveddFooter.Focus();
            else
                txtRemarksFooter.Focus();
        }

        protected void txtLEAVEDDEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtRemarksEdit = (TextBox)row.FindControl("txtREMARKSEdit");
            TextBox txtLeaveddEdit = (TextBox)row.FindControl("txtLEAVEDDEdit");
            if (txtLeaveddEdit.Text == "")
                txtLeaveddEdit.Focus();
            else
                txtRemarksEdit.Focus();
        }
    }
}