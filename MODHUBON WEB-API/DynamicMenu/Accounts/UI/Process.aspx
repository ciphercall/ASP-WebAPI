<%@ Page Title="Process" Language="C#" AutoEventWireup="true" MasterPageFile="~/Dynamic.Master" CodeBehind="Process.aspx.cs" Inherits="DynamicMenu.Accounts.UI.Process" %>

<%@ Import Namespace="DynamicMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/js/function.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID%>").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+100" });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Process</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Accounts/UI/Process.aspx", "INSERTR") == true)
                            { %>
                        <li><a href="Process.aspx"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Accounts/UI/Process.aspx", "UPDATER") == true)
                            { %>
                        <li><a href="Process.aspx"><i class="fa fa-edit"></i>Update</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Accounts/UI/Process.aspx", "DELETER") == true)
                            { %>
                        <li><a href="Process.aspx"><i class="fa fa-times"></i>Delete</a>
                        </li>
                        <% } %>
                    </ul>
                </div>
                <!-- end logout option -->
                <asp:Label ID="lblSerial_Mrec" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSerial_Jour" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSerial_BUY" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSerial_Mpay" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSerial_Cont" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSerial_SALE" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSlSale_Dis" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSerial_LC" runat="server" Visible="False"></asp:Label>


            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Date <strong>:</strong></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-sm" AutoPostBack="True"
                             OnTextChanged="txtDate_TextChanged" TabIndex="1"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>

                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnProcess" runat="server" CssClass="form-control btn-primary"
                            Font-Bold="True" Font-Italic="True" Text="Process"
                            OnClick="btnProcess_Click" TabIndex="2" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="GridView2" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="GridView3" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="GridView4" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gridLC" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gridSale_Ret" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gridPurchase_Ret" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gvSaleNew" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gvSaleLtCost" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gvBuyImport" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gridMultiple" runat="server">
                    </asp:GridView>
                     <asp:GridView ID="GridGLCheque" runat="server">
                    </asp:GridView>
                </div>
            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
