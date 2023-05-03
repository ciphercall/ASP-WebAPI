<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="PartyWisePurchaseDetails.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.PartyWisePurchaseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFdt,#txtToDt").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>TRANSACTION STATEMENT DETAILS</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Store :</div>
                    <div class="col-md-4">
                         <asp:DropDownList runat="server" ID="ddlStore" CssClass="form-control"/>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Type :</div>
                    <div class="col-md-4">
                         <asp:DropDownList runat="server" ID="ddlParty" CssClass="form-control">
                         </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtFdt" ClientIDMode="Static" MaxLength="10" TabIndex="2" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To Date :</div>
                    <div class="col-md-4">
                         <asp:TextBox runat="server" ID="txtToDt" ClientIDMode="Static" MaxLength="10" TabIndex="3" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row text-center">
                    <strong><asp:Label runat="server" ID="lblmsg" ForeColor="red" Visible="False"></asp:Label></strong>
                   <asp:Label runat="server" ID="Label1" ForeColor="red" Visible="False"></asp:Label>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                         <asp:Button runat="server" ID="btnSubmit" CssClass="btn-primary form-control input-sm" TabIndex="4" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
                
            </div>
            <!-- Content End From here -->
        </div>
    </div>
    <asp:Label runat="server" ID="lblStoreID" Visible="False"></asp:Label>
</asp:Content>
