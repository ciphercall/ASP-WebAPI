<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="DetailsOpProfitLoss.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.DetailsOpProfitLoss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#txtFromDate").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtToDate").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#txtToDate").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtFromDate").datepicker("option", "maxDate", selectedDate);
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Detailed Operating Profit & Loss</h1>
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
                        <asp:DropDownList ID="ddlStore" runat="server" TabIndex="1" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="2" CssClass="form-control input-sm"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="3" CssClass="form-control input-sm"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Text="Submit"
                            CssClass="form-control input-sm btn-primary" TabIndex="4" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>

                <asp:Label ID="lblStID" runat="server" Visible="False"></asp:Label>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
