<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="AttendanceProcess.aspx.cs" Inherits="DynamicMenu.Payroll.UI.AttendanceProcess" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {
            $("#txtAttDate").datepicker({ dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
            return true;
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
                <h1>ATTENDANCE PROCESS</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblLEAVEID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row form-class">
                    <div class="col-md-4"></div>
                    <div class="col-md-1 text-right">Date </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtAttDate" CssClass="form-control input-sm text-uppercase text-center" MaxLength="6"
                            TabIndex="2" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row">

                    <div class="col-md-12">
                        <h4 class="text-center">Daily Attendance Process</h4>
                        <hr />
                        <div class="row text-center">
                            <asp:Label runat="server" ID="lblAttMsg" ForeColor="red" Visible="False"></asp:Label>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceProcess.aspx", "INSERTR"))
                                    { %>
                                <asp:Button runat="server" ID="btnAttProcess" Text="Att Process" CssClass="form-control input-sm btn-primary" OnClick="btnAttProcess_OnClick" />
                                <% } %>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>

                    <%--<div class="col-md-6">
                        <%--<h4 class="text-center">Daily Over Time Process</h4>
                        <hr />
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class="row text-center">
                            <asp:Label runat="server" ID="lblError" ForeColor="red" Visible="False"></asp:Label>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceProcess.aspx", "INSERTR"))
                                    { %>
                                <asp:Button runat="server" Visible="False" ID="btnSubmit" Text="OT Process" CssClass="form-control input-sm btn-primary" OnClick="btnSubmit_Click" />
                                <% } %>
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>--%>
                </div>


            </div>
            <!-- Content End From here -->
        </div>
    </div>

    <asp:GridView runat="server" ID="gdProcess" Visible="False"></asp:GridView>
</asp:Content>
