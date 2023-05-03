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
    public partial class Post : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_POST", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_post.DataSource = ds;
                gv_post.DataBind();
                TextBox txtPOSTNMFooter = (TextBox)gv_post.FooterRow.FindControl("txtPOSTNMFooter");
                txtPOSTNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_post.DataSource = ds;
                gv_post.DataBind();
                int columncount = gv_post.Rows[0].Cells.Count;
                gv_post.Rows[0].Cells.Clear();
                gv_post.Rows[0].Cells.Add(new TableCell());
                gv_post.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_post.Rows[0].Visible = false;
                TextBox txtPOSTNMFooter = (TextBox)gv_post.FooterRow.FindControl("txtPOSTNMFooter");
                txtPOSTNMFooter.Focus();
            }
        }
        private void POstID()
        {

            dbFunctions.lblAdd("SELECT MAX(POSTID) FROM HR_POST", lblPostID);
            string CMPID = int.Parse("101").ToString();
            string postID = "";
            if (lblPostID.Text == "")
            {
                postID = CMPID + "01";
            }
            else
            {
                string Substr = lblPostID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    postID = CMPID + "0" + subint;
                }
                else if (subint < 100)
                {
                    postID = CMPID + subint;
                }
            }
            iob.PostID = int.Parse(postID);
        }
        protected void gv_post_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblPOSTID = (Label)gv_post.FooterRow.FindControl("lblPOSTID");
                    TextBox txtPOSTNMFooter = (TextBox)gv_post.FooterRow.FindControl("txtPOSTNMFooter");
                    TextBox txtREMARKSFooter = (TextBox)gv_post.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtPOSTNMFooter.Text == "")
                        {
                            txtPOSTNMFooter.Focus();
                        }
                        else
                        {
                            iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                            iob.UserID = int.Parse(Session["USERID"].ToString());
                            POstID();
                            iob.PostNM = txtPOSTNMFooter.Text;
                            iob.Ltude = txtLotiLongTude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_POST(iob);
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

        protected void gv_post_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                POstID();
                e.Row.Cells[0].Text = iob.PostID.ToString();
            }
        }

        protected void gv_post_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_post.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_post_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                try
                {
                    Label lblPOSTID = (Label)gv_post.Rows[e.RowIndex].FindControl("lblPOSTID");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_POST WHERE POSTID = '" + lblPOSTID.Text + "'", conn);
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

        protected void gv_post_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                gv_post.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtPOSTNMEdit = (TextBox)gv_post.Rows[e.NewEditIndex].FindControl("txtPOSTNMEdit");
                txtPOSTNMEdit.Focus();
            }
        }

        protected void gv_post_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                Label lblPOSTID = (Label)gv_post.Rows[e.RowIndex].FindControl("lblPOSTID");
                TextBox txtPOSTNMEdit = (TextBox)gv_post.Rows[e.RowIndex].FindControl("txtPOSTNMEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_post.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (txtPOSTNMEdit.Text == "")
                    txtPOSTNMEdit.Focus();
                else
                {
                    iob.PostID = int.Parse(lblPOSTID.Text);
                    iob.CmpID = int.Parse(Session["COMPANYID"].ToString());
                    iob.UserID = int.Parse(Session["USERID"].ToString());
                    iob.PostNM = txtPOSTNMEdit.Text;
                    iob.UPDLtude = txtLotiLongTude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_POST(iob);
                    gv_post.EditIndex = -1;
                    gridShow();
                }



            }
        }
    }
}