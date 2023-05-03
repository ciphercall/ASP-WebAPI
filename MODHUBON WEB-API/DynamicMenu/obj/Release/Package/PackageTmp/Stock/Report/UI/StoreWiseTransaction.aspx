<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="StoreWiseTransaction.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.StoreWiseTransaction" %>

<%@ Import Namespace="DynamicMenu" %>
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
                <h1>Store Wise Ttransaction</h1>
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
                        <asp:DropDownList runat="server" ID="ddlStore" CssClass="form-control input-sm" />
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Type :</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control input-sm">
                            <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="SALE">SALE</asp:ListItem>
                            <asp:ListItem Value="IRTS">RETURN</asp:ListItem>
                            <asp:ListItem Value="BUY">PURCHASE</asp:ListItem>
                            <asp:ListItem Value="IISS">ISSUE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From :</div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtFdt" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To :</div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtToDt" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row text-center">
                    <strong>
                        <asp:Label runat="server" ID="lblMsg" Visible="False" ForeColor="red"></asp:Label></strong>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="form-control input-sm btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
