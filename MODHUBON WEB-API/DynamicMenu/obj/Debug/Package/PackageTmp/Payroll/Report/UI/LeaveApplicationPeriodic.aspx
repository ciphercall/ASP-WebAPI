<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationPeriodic.aspx.cs" Inherits="DynamicMenu.Payroll.Report.UI.LeaveApplicationPeriodic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../MenuCssJs/bootstrap/bootstrap-datepicker.js"></script>
    <link href="../../../MenuCssJs/bootstrap/datepicker.css" rel="stylesheet" />
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            BindControlEvents();
           
        });

        function BindControlEvents() {
            $(function () {

                $("#txtFDate").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtFDate").datepicker("option", "maxDate", selectedDate);
                    }
                });
                $("#txtTDate").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtFDate").datepicker("option", "maxDate", selectedDate);
                    }
                });
            });
            
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
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Leave Application Periodic</h1>
                    </div>
                    <!-- content header end -->

                    <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">Employee Name :</div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlEmployeeNM" runat="server" TabIndex="1" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">From Date :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFDate" runat="server" TabIndex="2" CssClass="form-control input-sm text-uppercase" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">To date :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtTDate" runat="server" MaxLength="6" TabIndex="3" CssClass="form-control input-sm text-uppercase" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row form-class text-center">
                            <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Search" TabIndex="4" CssClass="btn-primary form-control input-sm" OnClick="btnSubmit_OnClick"/>
                            </div>
                            <div class="col-md-5"></div>
                        </div>

                    </div>
                    <!-- Content End From here -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
