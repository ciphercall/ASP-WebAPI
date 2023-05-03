<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="AccountsRecST.aspx.cs" Inherits="DynamicMenu.Accounts.Report.UI.AccountsRecST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="/MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="/MenuCssJs/js/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>ACCOUNTS RECEIVABLE - TOTAL</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                    <!-- logout option button -->
                    <!-- end logout option -->
                    <asp:Label ID="lblStID" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblErrMsg" runat="server" ForeColor="#990000" Style="font-weight: 700"></asp:Label>
            </div>
            <!-- content header end -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        Date :
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtDate" runat="server" AutoPostBack="False"
                            ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-4"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="form-control input-sm btn-primary" Font-Bold="True" Text="Search" OnClick="btnSearch_Click"/>
                    </div>
                    <div class="col-md-4"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
