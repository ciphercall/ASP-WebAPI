<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ClossingStock.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.ClossingStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Store Wise Closing / Minumum Stock & Value</h1>
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
                                <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">Date :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-sm"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Text="Closing Stock"
                                    CssClass="form-control input-sm btn-primary" OnClick="btnSearch_Click" />
                            </div>
                             <div class="col-md-2">
                                <asp:Button ID="btnMinmunStockSearch" runat="server" Font-Bold="True" Text="Minimum Stock"
                                    CssClass="form-control input-sm btn-primary" OnClick="btnMinmunStockSearch_Click" />
                            </div>
                            <div class="col-md-4"></div>
                        </div>

                        <asp:Label ID="lblStID" runat="server" Visible="False"></asp:Label>

                    </div>
                    <!-- Content End From here -->
                </div>
            </div>
</asp:Content>
