using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting;
using DynamicMenu.LogData;
using DynamicMenu.Stock.DataAccess;
using DynamicMenu.Stock.Interface;

namespace DynamicMenu.Stock.UI
{
    public partial class ItemNameEntry : System.Web.UI.Page
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
                const string formLink = "/Stock/UI/ItemNameEntry.aspx";
                bool permission = UserPermissionChecker.checkParmit(formLink, "STATUS");
                if (permission)
                {
                    if (!IsPostBack)
                    {
                        txtCategoryNM.Focus();
                    }
                }
                else
                {
                    Response.Redirect("../../DashBoard/UI/Default.aspx");
                }
            }
        }

        protected void txtCategoryNM_TextChanged(object sender, EventArgs e)
        {
            lblSUBID.Text = "";
            lblMaxCatID.Text = "";
            //dbFunctions.lblAdd(@"select CATID from STK_ITEMMST where CATNM='" + txtCategoryNM.Text + "'", lblCatID);
            if (txtCategoryNM.Text == "")
            {
                gvDetails.Visible = false;
                txtCategoryNM.Focus();
                txtPrint.Visible = false;
            }
            else
            {
                gvDetails.Visible = true;
                dbFunctions.lblAdd(@"select CATID from STK_ITEMMST where CATNM='" + txtCategoryNM.Text + "'", lblSUBID);
                GenerateID(lblSUBID.Text);
                GridShow();
                txtPrint.Visible = false;
                txtPrint.Text = "";
            }
        }
        protected void GridShow()
        {

            SqlConnection conn = new SqlConnection(dbFunctions.connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ITEMID,ITEMNM,ITEMNMB,MUNIT,BUYRT,SALRT,IMAGEPATH,(CASE STATUS WHEN 'A' THEN 'ACTIVE' WHEN 'I' THEN 'INACTIVE' ELSE '' END) STATUS from STK_ITEM  where CATID='" + lblSUBID.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                lblGridMSG.Visible = false;
                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                txtItemNM.Focus();
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
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                lblGridMSG.Visible = false;
                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                txtItemNM.Focus();
            }

        }
        private string GenerateID(string catid)
        {
            var maxid = dbFunctions.StringData(@"SELECT MAX(ITEMID) FROM STK_ITEM WHERE CATID='" + catid + "'");
            string maxItemid;
            if (maxid == "")
            {
                maxItemid = catid + "001";
            }
            else
            {
                var subcatid = maxid.Substring(5, 3);
                if (Convert.ToInt16(subcatid) < 9)
                {
                    int id = Convert.ToInt16(subcatid) + Convert.ToInt16(1);
                    maxItemid = catid + "00" + id;
                }
                else if (Convert.ToInt16(subcatid) < 99)
                {
                    int id = Convert.ToInt16(subcatid) + Convert.ToInt16(1);
                    maxItemid = catid + "0" + id;
                }
                else if (Convert.ToInt16(subcatid) < 999)
                {
                    int id = Convert.ToInt16(subcatid) + Convert.ToInt16(1);
                    maxItemid = catid + id;
                }
                else maxItemid = "";
            }
            return maxItemid;
        }
        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.UserID = HttpContext.Current.Session["USERID"].ToString();
            iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
            iob.UserPC = HttpContext.Current.Session["PCName"].ToString();
            iob.ITime = dbFunctions.Timezone(DateTime.Now);


            TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
            TextBox txtItemNMBan = (TextBox)gvDetails.FooterRow.FindControl("txtItemNMBan");
            TextBox txtUnit = (TextBox)gvDetails.FooterRow.FindControl("txtUnit");
            TextBox txtBuyRT = (TextBox)gvDetails.FooterRow.FindControl("txtBuyRT");
            TextBox txtSaleRT = (TextBox)gvDetails.FooterRow.FindControl("txtSaleRT");
            DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");

            var ImageFileUploadFooter = (FileUpload)gvDetails.FooterRow.FindControl("ImageFileUploadFooter");

            if (e.CommandName.Equals("AddNew"))
            {
                if (txtItemNM.Text == "")
                {
                    lblGridMSG.Visible = true;
                    lblGridMSG.Text = "Item Name Required .";
                    txtItemNM.Focus();                 
                }

                
                else
                {
                    if (Session["USERID"] == null)
                    {
                        Response.Redirect("~/Login/UI/Login.aspx");
                    }
                    else
                    {


                        if (ddlStatus.Text == "ACTIVE")
                        {
                            iob.Status = "A";
                        }
                       else
                        {
                            iob.Status = "I";
                        }
                       

                        lblGridMSG.Visible = false;
                        if (txtBuyRT.Text == "")
                            txtBuyRT.Text = "0";

                        if (txtSaleRT.Text == "")
                            txtSaleRT.Text = "0";

                        iob.CompanyId = "101";
                        iob.ItemID = GenerateID(lblSUBID.Text);
                        iob.SubID = lblSUBID.Text;
                        iob.ItemNM = txtItemNM.Text;
                        iob.ItemNMBangla = txtItemNMBan.Text;
                        iob.UnitTP = txtUnit.Text;
                        iob.BuyRT = txtBuyRT.Text;
                        iob.SaleRT = txtSaleRT.Text;
                        //iob.Status = ddlStatus.Text;


                        string count = dbFunctions.StringData(@"SELECT COUNT(*) AS CNT FROM STK_ITEM 
                    WHERE ITEMNM='" + iob.ItemNM + "' AND MUNIT='" + iob.UnitTP + "' AND ITEMNMB='" + iob.ItemNMBangla + "' ");
                        if (count == "0")
                        {
                            lblGridMSG.Visible = false;

                            if (ImageFileUploadFooter.HasFile)
                                iob.ImagePath = ImageUpload(ImageFileUploadFooter, iob.ItemID);
                            else iob.ImagePath = "";

                            dob.insert_STK_ITEM(iob);
                            GridShow();
                        }
                        else
                        {
                            lblGridMSG.Text = "Same item already added.";
                            lblGridMSG.Visible = true;
                            txtItemNM.Focus();
                        }
                    }
                }
            }

            if (e.CommandName.Equals("print"))
            {
                GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                index = oItem.RowIndex;
                Label itemcd = (Label)gvDetails.Rows[index].Cells[1].FindControl("lblItemCD");
                Label itemname = (Label)gvDetails.Rows[index].Cells[2].FindControl("lblItemNM");
                Label itemprice = (Label)gvDetails.Rows[index].Cells[11].FindControl("lblSaleRT");

                string url = "../../Barcode/Barcode.aspx?id=" + itemcd.Text + "&name=" + itemname.Text + "&price=" + itemprice.Text + "";

                //   Response.Redirect(url);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);

                ScriptManager.RegisterStartupScript(this,
               this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            }
        }
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, pairs);
            }
            return response;
        }
        public int index { get; set; }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                //TextBox txtItemCD = (TextBox)gvDetails.FooterRow.FindControl("txtItemCD");
                e.Row.Cells[0].Text = GenerateID(lblSUBID.Text);
                //txtItemCD.Text= iob.ItemID;
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
                GridShow();




                Label lblItemID = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblItemID");
                dbFunctions.lblAdd($@"select (CASE STATUS WHEN 'A' THEN 'ACTIVE' WHEN 'I' THEN 'INACTIVE' ELSE 'ACTIVE' END) STATUS from STK_ITEM where ITEMID='{lblItemID.Text}'", LBLSTS);

                DropDownList ddlStatusEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlStatusEdit");
                ddlStatusEdit.Text = LBLSTS.Text;



                TextBox txtItemNMEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtItemNMEdit");
                txtItemNMEdit.Focus();

             

               
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
                iob.UpdUserID = HttpContext.Current.Session["USERID"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();
                iob.UPDUserPC = HttpContext.Current.Session["PCName"].ToString();
                iob.UpdTime = dbFunctions.Timezone(DateTime.Now);

                Label lblItemID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemID");
                FileUpload ImageFileUploadEdit = (FileUpload)gvDetails.Rows[e.RowIndex].FindControl("ImageFileUploadEdit");

                TextBox txtItemNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtItemNMEdit");
                TextBox txtItemNMBanEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtItemNMBanEdit");
                TextBox txtUnitEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtUnitEdit");
                DropDownList ddlStatusEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlStatusEdit");

                TextBox txtBuyRTEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBuyRTEdit");
                if (txtBuyRTEdit.Text == "")
                    txtBuyRTEdit.Text = "0";

                TextBox txtSaleRTEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtSaleRTEdit");
                if (txtSaleRTEdit.Text == "")
                    txtSaleRTEdit.Text = "0";

                if (ddlStatusEdit.Text== "ACTIVE")
                {
                    iob.Status = "A";
                }
                else 
                {
                    iob.Status = "I";


                }
                iob.SubID = lblSUBID.Text;
                iob.ItemID = lblItemID.Text;

                iob.ItemNM = txtItemNMEdit.Text;
                iob.ItemNMBangla = txtItemNMBanEdit.Text;
                iob.UnitTP = txtUnitEdit.Text;
                iob.BuyRT = txtBuyRTEdit.Text;
                iob.SaleRT = txtSaleRTEdit.Text;
              //  iob.Status = ddlStatusEdit.Text;

                try
                {
                    // logdata add start //
                    string lotileng = HttpContext.Current.Session["Location"].ToString();
                    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                    string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),CATID,103)+'  '+
                    CONVERT(NVARCHAR(50),ITEMID,103)+'  '+ISNULL(ITEMNM,'(NULL)')+'  '+ISNULL(ITEMNMB,'(NULL)')+'  '+
                    ISNULL(MUNIT,'(NULL)')+'  '+CONVERT(NVARCHAR(50),BUYRT,103)+'  '+CONVERT(NVARCHAR(50),SALRT,103)+'  '+
                    ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+
                    ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERPC,103),'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                    ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_ITEM
                    WHERE CATID = '" + iob.SubID + "' and ITEMID = '" + iob.ItemID + "'");
                    string logid = "UPDATE";
                    string tableid = "STK_ITEM";
                    LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                    // logdata add end //
                }
                catch (Exception)
                {

                }
                if (ImageFileUploadEdit.HasFile)
                {
                    iob.ImagePath = ImageUpload(ImageFileUploadEdit, iob.ItemID);
                    dob.update_STK_ITEM(iob);
                }
                else
                {
                    dob.update_STK_ITEMWithOutImage(iob);
                }
                gvDetails.EditIndex = -1;
                GridShow();

            }
        }
        public string ImageUpload(FileUpload fd, string fileNmunique)
        {
            string img = "";
            try
            {
                if (fd.HasFile)
                {
                    string fileName = fd.FileName;
                    string exten = Path.GetExtension(fileName);

                    if (fd.FileBytes.Length > 1024000)
                    {
                        lblGridMSG.Visible = true;
                        lblGridMSG.Text = "Upload Image to large! file must be less then 1024kb";
                    }
                    else
                    {
                        lblGridMSG.Visible = false;

                        //here we have to restrict file type            
                        if (exten != null)
                        {
                            exten = exten.ToLower();
                            var acceptedFileTypes = new string[4];
                            acceptedFileTypes[0] = ".jpg";
                            acceptedFileTypes[1] = ".jpeg";
                            acceptedFileTypes[2] = ".gif";
                            acceptedFileTypes[3] = ".png";
                            bool acceptFile = false;
                            for (int i = 0; i <= 3; i++)
                            {
                                if (exten == acceptedFileTypes[i])
                                {
                                    acceptFile = true;
                                }
                            }
                            if (!acceptFile)
                            {
                                lblGridMSG.Visible = true;
                                lblGridMSG.Text = "Upload Image is not a permitted file type!";
                                fd.Focus();
                            }
                            else
                            {
                                try
                                {
                                    var file = new FileInfo("~/ProductImage/" + fileNmunique + exten);
                                    file.Delete();
                                }
                                catch (Exception)
                                {
                                    //ignore
                                }

                                //upload the file onto the server                   
                                fd.SaveAs(Server.MapPath("~/ProductImage/" + fileNmunique + exten));
                                img = "~/ProductImage/" + fileNmunique + exten;
                            }
                        }
                    }
                }
                else
                    img = "";
            }
            catch (Exception)
            {
                img = "";
            }


            return img;
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
                GridShow();
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
                SqlConnection conn = new SqlConnection(dbFunctions.connection);

                //Label lblCatGID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCatGID");
                Label lblItemID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemID");

                dbFunctions.lblAdd(@"select ITEMID from STK_TRANS where ITEMID = '" + lblItemID.Text + "'", lblChkItemID);

                int result = 0;

                if (lblChkItemID.Text == "")
                {
                    try
                    {
                        // logdata add start //
                        string lotileng = HttpContext.Current.Session["Location"].ToString();
                        string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        string logdata = dbFunctions.StringData(@"SELECT CONVERT(NVARCHAR(50),COMPID,103)+'  '+CONVERT(NVARCHAR(50),CATID,103)+'  '+
                    CONVERT(NVARCHAR(50),ITEMID,103)+'  '+ISNULL(ITEMNM,'(NULL)')+'  '+ISNULL(ITEMNMB,'(NULL)')+'  '+
                    ISNULL(MUNIT,'(NULL)')+'  '+CONVERT(NVARCHAR(50),BUYRT,103)+'  '+CONVERT(NVARCHAR(50),SALRT,103)+'  '+
                    ISNULL(USERPC,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),USERID,103),'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+
                    ISNULL(INSLTUDE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERPC,103),'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),UPDINTIME,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDUSERID,103),'(NULL)')+'  '+
                    ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(UPDLTUDE,'(NULL)') FROM STK_ITEM
                    where CATID = '" + lblSUBID.Text + "' and ITEMID = '" + lblItemID.Text + "'");
                        string logid = "DELETE";
                        string tableid = "STK_ITEM";
                        LogDataFunction.InsertLogData(lotileng, logid, tableid, logdata, ipAddress);
                        // logdata add end //
                    }
                    catch (Exception)
                    {

                    }
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete FROM STK_ITEM where CATID = '" + lblSUBID.Text + "' and ITEMID = '" + lblItemID.Text + "'", conn);
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }

                else
                {
                    Response.Write("<script>alert('This Item has a Transaction.');</script>");
                }

                if (result == 1)
                {
                    GridShow();
                }
            }
        }
        protected void gridViewAslRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                gvDetails.PageIndex = e.NewPageIndex;
                gvDetails.DataBind();
                GridShow();
                lblGridMSG.Visible = false;
            }
        }


        protected void gridViewAslRole_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (Session["USERID"] == null)
            {
                Response.Redirect("~/LogIn/UI/LogIn.aspx");
            }
            else
            {
                DataTable dataTable = gvDetails.DataSource as DataTable;

                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                    gvDetails.DataSource = dataView;
                    gvDetails.DataBind();
                    GridShow();
                }
            }
        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }
    }
}