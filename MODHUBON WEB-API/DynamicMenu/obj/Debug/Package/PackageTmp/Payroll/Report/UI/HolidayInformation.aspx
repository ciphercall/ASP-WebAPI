<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="HolidayInformation.aspx.cs" Inherits="DynamicMenu.Payroll.Report.UI.HolidayInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            // $("#txtFrDT,#txtToDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+0" });
            BindControlEvents();
        });
        function BindControlEvents() {
            $(function () {
                $("#txtFrDT").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtToDT").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#txtToDT").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtFrDT").datepicker("option", "maxDate", selectedDate);
                    }
                });
            });
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }

    </script>
    <style>
        .ui-autocomplete {
            max-width: 350px;
            max-height: 250px;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Holiday Information</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFrDT" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtToDT" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class text-center">
                    <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                    <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" CssClass="btn-primary form-control input-sm" />
                    </div>
                    <div class="col-md-5"></div>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
