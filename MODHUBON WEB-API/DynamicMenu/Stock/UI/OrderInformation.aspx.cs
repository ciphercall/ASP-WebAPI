using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.Functions;
using DynamicMenu.LogData;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class OrderInformation : System.Web.UI.Page
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
                const string formLink = "/Stock/UI/OrderInformation.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission == true)
                {
                    if (!IsPostBack)
                    {
                        DateTime date = dbFunctions.Timezone(DateTime.Now);
                        txtOrderDate.Text = date.ToString("dd-MM-yyyy");
                        txtBillDate.Text = date.AddDays(1).ToString("dd-MM-yyyy");
                        txtTransactionNo.Text = StockTransaction.OrderTransactionNo(date.Year.ToString());
                        ShowGrid();
                        txtPartyName.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

        protected void ShowGrid()
        {
            DateTime date = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string transDate = date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            iob.TransNo2 = btnEdit.Text == "Edit Record" ? txtTransactionNo.Text : ddlTransactionNo.Text;

            SqlCommand cmd = new SqlCommand(@"SELECT        STK_TRANS.COMPID, STK_TRANS.TRANSTP, STK_TRANS.TRANSDT, STK_TRANS.TRANSYY, STK_TRANS.TRANSNO, STK_TRANS.PSID, STK_TRANS.PSSL, STK_TRANS.BILLDT, STK_TRANS.STOREFR, 
            STK_TRANS.STORETO, STK_TRANS.TRANSSL, STK_TRANS.ITEMID, STK_TRANS.QTY, STK_TRANS.RATE, STK_TRANS.AMOUNT, STK_TRANS.REMARKS, STK_ITEM.ITEMNM,  STK_ITEM.MUNIT
            FROM STK_TRANS INNER JOIN
            STK_ITEM ON STK_TRANS.ITEMID = STK_ITEM.ITEMID
            WHERE (STK_TRANS.TRANSNO = '" + iob.TransNo2 + "') AND (STK_TRANS.TRANSDT = '" + transDate +
            "') AND (STK_TRANS.TRANSTP = 'IORD')", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Sub.DataSource = ds;
                gv_Sub.DataBind();
                TextBox txtItemNameFooter = (TextBox)gv_Sub.FooterRow.FindControl("txtItemNameFooter");
                txtItemNameFooter.Focus();
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
                TextBox txtItemNameFooter = (TextBox)gv_Sub.FooterRow.FindControl("txtItemNameFooter");
                txtItemNameFooter.Focus();
            }
            var data = dbFunctions.DataReaderAdd(@"SELECT ISNULL(SUM(QTY),0) AS QTY, ISNULL(SUM(AMOUNT),0) AS AMOUNT FROM STK_TRANS 
                WHERE (TRANSTP = 'IORD') AND (TRANSDT = '" + transDate + "') AND (TRANSNO = '" + iob.TransNo2 + "')");
            if (data.Count > 0)
            {
                lblTotalQty.Text = data[0];
                lblTotalAmount.Text = data[1];
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }

        protected void gv_Sub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                iob.UserID = HttpContext.Current.Session["USERID"].ToString();
                iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.ITime = dbFunctions.Timezone(DateTime.Now);

                var lblItemCodeFooter = (TextBox)gv_Sub.FooterRow.FindControl("lblItemCodeFooter");
                var txtItemNameFooter = (TextBox)gv_Sub.FooterRow.FindControl("txtItemNameFooter");
                var txtQtyFooter = (TextBox)gv_Sub.FooterRow.FindControl("txtQtyFooter");
                var txtRateFooter = (TextBox)gv_Sub.FooterRow.FindControl("txtRateFooter");
                var txtRemarksFooter = (TextBox)gv_Sub.FooterRow.FindControl("txtRemarksFooter");

                if (e.CommandName.Equals("Add"))
                {
                    if (lblItemCodeFooter.Text == "")
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Select Item Name";
                        txtItemNameFooter.Focus();
                    }
                    else if (txtPartyCode.Text == "")
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Select Party Name";
                        txtPartyName.Text = "";
                        txtPartyName.Focus();
                    }
                    else
                    {
                        lblGridMSG.Visible = false;
                        lblMSG.Visible = false;

                        iob.TrDt = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.BillDate = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                        iob.MonthYear = iob.TrDt.Year.ToString();
                        iob.CompanyId = "101";
                        iob.ItemID = lblItemCodeFooter.Text;
                        iob.Qty = Convert.ToDecimal(txtQtyFooter.Text);
                        iob.Rate = Convert.ToDecimal(txtRateFooter.Text);
                        iob.Amount = iob.Qty * iob.Rate;
                        iob.TransTP = "IORD";
                        iob.PartyId = txtPartyCode.Text;
                        iob.PartySerial = StockTransaction.PartySerial(iob.TrDt.Year.ToString(), iob.PartyId);
                        iob.Remarks = txtRemarks.Text;
                        iob.RemarksDetails = txtRemarksFooter.Text;
                        string s = "";
                        if (btnEdit.Text == "Edit Record")
                        {
                            string counter =
                                dbFunctions.StringData(@"SELECT COUNT(TRANSNO) FROM STK_TRANS WHERE TRANSTP='IORD' AND TRANSNO='" +
                                                       txtTransactionNo.Text +
                                                       "' AND USERID=" + iob.UserID + "");
                            if (Convert.ToInt16(counter) > 0)
                            {
                                iob.TransNo = Convert.ToInt64(txtTransactionNo.Text);
                                iob.TransSL = dbFunctions.StringData(@"SELECT ISNULL(MAX(TRANSSL),0)+1  FROM STK_TRANS
                            WHERE TRANSTP='IORD' AND TRANSNO='" + iob.TransNo + "'");
                                s = dob.InsertOrder(iob);
                            }
                            else
                            {
                                iob.TransNo =
                                    Convert.ToInt64(StockTransaction.OrderTransactionNo(iob.TrDt.Year.ToString()));
                                txtTransactionNo.Text = iob.TransNo.ToString();
                                iob.TransSL = "1";
                                var t = dob.InsertOrderMaster(iob);
                                if (t == "")
                                    s = dob.InsertOrder(iob);
                            }
                        }
                        else
                        {
                            iob.TransNo = Convert.ToInt64(ddlTransactionNo.Text);
                            iob.TransSL = dbFunctions.StringData(@"SELECT ISNULL(MAX(TRANSSL),0)+1  FROM STK_TRANS 
                            WHERE TRANSNO='" + iob.TransNo + "'");
                            s = dob.InsertOrder(iob);
                        }

                        if (s == "")
                        {
                            txtPartyName.ReadOnly = true;
                            txtBillDate.ReadOnly = true;
                            txtRemarks.ReadOnly = true;
                            ShowGrid();
                        }
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
                ShowGrid();
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
                ShowGrid();

                TextBox txtQtyEdit = (TextBox)gv_Sub.Rows[e.NewEditIndex].FindControl("txtQtyEdit");
                txtQtyEdit.Focus();
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
                iob.TrDt = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                var lblCOMPID = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblCOMPID");
                var lblItemid = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblItemid");
                var lblItemSl = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblItemSl");

                iob.MonthYear = iob.TrDt.Year.ToString();
                iob.ItemID = lblItemid.Text;
                iob.TransSL = lblItemSl.Text;
                iob.CompanyId = lblCOMPID.Text;
                iob.TransTP = "IORD";

                iob.TransNo = btnEdit.Text == "Edit Record"
                    ? Convert.ToInt64(txtTransactionNo.Text)
                    : Convert.ToInt64(ddlTransactionNo.Text);

                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+TRANSTP+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+CONVERT(NVARCHAR(50),TRANSYY,103)+'  '+TRANSNO+'  '+
                        ISNULL(PSID,'(NULL)')+'  '+ISNULL(PSSL,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),BILLDT,103),'(NULL)')+'  '+ISNULL(STOREFR,'(NULL)')+'  '+
                        ISNULL(STORETO,'(NULL)')+'  '+TRANSSL+'  '+ISNULL(ITEMID,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),QTY,103),'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),RATE,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),AMOUNT,103),'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+
                        ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                        ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                        ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_TRANS WHERE
                        TRANSTP='IORD' AND TRANSYY='" + iob.MonthYear + "' AND TRANSNO=" + iob.TransNo + " AND TRANSSL=" + iob.TransSL + "");
                    string logid = "DELETE";
                    string tableid = "STK_TRANS";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception)
                {

                }
                var result = dob.DeleteOrder(iob);
                var count = dbFunctions.StringData(@"SELECT COUNT(*) FROM STK_TRANS WHERE
                        TRANSTP='IORD' AND TRANSYY='" + iob.MonthYear + "' AND TRANSNO='" + iob.TransNo + "'");

                if (count == "0")
                {
                    try
                    {
                        // logdata add start //
                        string lotileng = HttpContext.Current.Session["Location"].ToString();
                        string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+TRANSTP+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+CONVERT(NVARCHAR(50),TRANSYY,103)+'  '+TRANSNO+'  '+ISNULL(PSID,'(NULL)')+'  '+
                        ISNULL(PSSL,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),BILLDT,103),'(NULL)')+'  '+ISNULL(STOREFR,'(NULL)')+'  '+ISNULL(STORETO,'(NULL)')+'  '+
                        ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+
                        ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                        ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_TRANSMST WHERE
                        TRANSTP='IORD' AND TRANSYY='" + iob.MonthYear + "' AND TRANSNO=" + iob.TransNo + "");
                        string logid = "DELETE";
                        string tableid = "STK_TRANSMST";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                    catch (Exception)
                    {

                    }
                    dob.DeleteOrderMaster(iob);
                    if (btnEdit.Text == "Add Record")
                    {
                        dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSDT='" + iob.TrDt.ToString("yyyy-MM-dd") + "'");
                        ddlTransactionNo.Focus();
                    }
                    Refresh();
                }
                if (result == "")
                {
                    ShowGrid();
                }
            }

        }

        protected void gv_Sub_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var txtQtyEdit = (TextBox)gv_Sub.Rows[e.RowIndex].FindControl("txtQtyEdit");
            var txtRateEdit = (TextBox)gv_Sub.Rows[e.RowIndex].FindControl("txtRateEdit");
            var lblItemSlEdit = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblItemSlEdit");
            var lblItemidEdit = (Label)gv_Sub.Rows[e.RowIndex].FindControl("lblItemidEdit");
            var txtRemarksEdit = (TextBox)gv_Sub.Rows[e.RowIndex].FindControl("txtRemarksEdit");

            if (txtPartyCode.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Party Name";
                txtPartyName.Text = "";
                txtPartyName.Focus();
            }
            if (btnEdit.Text == "Add Record" && ddlTransactionNo.Text == "--SELECT--")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Transaction No";
                ddlTransactionNo.Text = "";
                ddlTransactionNo.Focus();
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

                    iob.TrDt = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.MonthYear = iob.TrDt.Year.ToString();
                    iob.CompanyId = "101";
                    iob.ItemID = lblItemidEdit.Text;
                    iob.TransSL = lblItemSlEdit.Text;
                    iob.TransTP = "IORD";
                    iob.TransNo = btnEdit.Text == "Edit Record" ? Convert.ToInt64(txtTransactionNo.Text) : Convert.ToInt64(ddlTransactionNo.Text);


                    iob.Qty = Convert.ToDecimal(txtQtyEdit.Text);
                    iob.Rate = Convert.ToDecimal(txtRateEdit.Text);
                    iob.Amount = iob.Qty * iob.Rate;
                    iob.RemarksDetails = txtRemarksEdit.Text;

                    try
                    {
                        // logdata add start //
                        string lotileng = HttpContext.Current.Session["Location"].ToString();
                        string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+TRANSTP+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+CONVERT(NVARCHAR(50),TRANSYY,103)+'  '+TRANSNO+'  '+
                        ISNULL(PSID,'(NULL)')+'  '+ISNULL(PSSL,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),BILLDT,103),'(NULL)')+'  '+ISNULL(STOREFR,'(NULL)')+'  '+
                        ISNULL(STORETO,'(NULL)')+'  '+TRANSSL+'  '+ISNULL(ITEMID,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),QTY,103),'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),RATE,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),AMOUNT,103),'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+
                        ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                        ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                        ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_TRANS WHERE
                        TRANSTP='IORD' AND TRANSYY='" + iob.MonthYear + "' AND TRANSNO=" + iob.TransNo + " AND TRANSSL=" + iob.TransSL + "");
                        string logid = "UPDATE";
                        string tableid = "STK_TRANS";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                    catch (Exception)
                    {

                    }

                    var s = dob.UpdateOrder(iob);
                    if (s == "")
                    {
                        gv_Sub.EditIndex = -1;
                        ShowGrid();
                    }
                }
            }
        }

        protected void Refresh()
        {
            txtPartyName.Text = "";
            txtPartyCode.Text = "";
            txtRemarks.Text = "";
            lblTotalAmount.Text = "0.00";
            lblTotalQty.Text = "0.00";
        }

        protected void btnComplete_OnClick(object sender, EventArgs e)
        {
            DateTime date = dbFunctions.Timezone(DateTime.Now);
            txtOrderDate.Text = date.ToString("dd-MM-yyyy");
            txtBillDate.Text = date.AddDays(1).ToString("dd-MM-yyyy");

            if (btnEdit.Text == "Edit Record")
            {
                txtTransactionNo.Text = StockTransaction.OrderTransactionNo(date.Year.ToString());
                ShowGrid();
                txtBillDate.ReadOnly = false;
                txtOrderDate.ReadOnly = false;
                txtPartyName.ReadOnly = false;
                txtRemarks.ReadOnly = false;
                Refresh();
                txtPartyName.Focus();
            }
            else
            {
                string transDate = date.ToString("yyyy-MM-dd");
                dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSDT='" + transDate + "'");
                ShowGrid();
                ddlTransactionNo.Focus();
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit Record")
            {
                DateTime date = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string transDate = date.ToString("yyyy-MM-dd");

                dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSDT='" + transDate + "'");
                ddlTransactionNo.Visible = true;
                txtTransactionNo.Visible = false;
                btnEdit.Text = "Add Record";
                Refresh();
                ShowGrid();
                txtBillDate.ReadOnly = true;
                txtPartyName.ReadOnly = true;
                txtRemarks.ReadOnly = true;
                ddlTransactionNo.Focus();
            }
            else
            {
                btnEdit.Text = "Edit Record";
                Refresh();
                ShowGrid();
                txtBillDate.ReadOnly = false;
                txtPartyName.ReadOnly = false;
                txtRemarks.ReadOnly = false;

                ddlTransactionNo.Visible = false;
                txtTransactionNo.Visible = true;
                txtPartyName.Focus();
            }
        }

        protected void ddlTransactionNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string transDate = date.ToString("yyyy-MM-dd");
            if (ddlTransactionNo.Text == "--SELECT--")
            {
                dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT TRANSNO FROM STK_TRANSMST WHERE TRANSDT='" + transDate + "'");
                lblMSG.Visible = true;
                lblMSG.Text = "Select Transaction No";
                ddlTransactionNo.Focus();
            }
            else
            {
                var data = dbFunctions.DataReaderAdd(@"SELECT STK_TRANSMST.PSID, REPLACE(CONVERT(NVARCHAR,STK_TRANSMST.BILLDT,103),'/','-') BILLDT, 
                STK_TRANSMST.REMARKS, STK_PARTY.PARTYNM, REPLACE(CONVERT(NVARCHAR,STK_TRANSMST.TRANSDT,103),'/','-') TRANSDT 
                FROM STK_TRANSMST INNER JOIN 
                STK_PARTY ON STK_TRANSMST.COMPID = STK_PARTY.COMPID AND STK_TRANSMST.PSID = STK_PARTY.PARTYID  
                WHERE (STK_TRANSMST.TRANSTP = 'IORD') AND (STK_TRANSMST.TRANSDT = '" + transDate +
                "') AND (STK_TRANSMST.TRANSNO = '" + ddlTransactionNo.Text + "')");
                if (data.Count > 0)
                {
                    txtPartyCode.Text = data[0];
                    txtBillDate.Text = data[1];
                    txtRemarks.Text = data[2];
                    txtPartyName.Text = data[3];
                    txtOrderDate.Text = data[4];
                }
                ShowGrid();
            }
        }

        protected void txtOrderDate_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Parse(txtOrderDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string transDate = date.ToString("yyyy-MM-dd");

                if (btnEdit.Text == "Edit Record")
                {
                    txtTransactionNo.Text = StockTransaction.OrderTransactionNo(date.Year.ToString());
                    txtPartyName.Focus();
                }
                else
                {
                    dbFunctions.dropDownAddWithSelect(ddlTransactionNo, @"SELECT DISTINCT TRANSNO FROM STK_TRANS WHERE TRANSTP='IORD' AND TRANSDT='" + transDate + "'");
                    ddlTransactionNo.Focus();
                }
                ShowGrid();
            }
            catch (Exception)
            {
                //ignore
            }

        }

    
    }
}