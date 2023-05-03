<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="SalaryProcess.aspx.cs" Inherits="DynamicMenu.Payroll.UI.SalaryProcess" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../MenuCssJs/bootstrap/bootstrap-datepicker.js"></script>
    <link href="../../MenuCssJs/bootstrap/datepicker.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {
            $("#txtMnYear").datepicker({ format: "M-yy",
                startView: "months",
                minViewMode: "months",
                autoclose:"true" });
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
                <h1>SALARY PROCESS</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblLEAVEID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-3"></div>
                    <div class="col-md-2">Month/Year :</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtMnYear" CssClass="form-control input-sm text-uppercase" MaxLength="6"
                            TabIndex="2" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row text-center">
                    <asp:Label runat="server" ID="lblError" ForeColor="red" Visible="False"></asp:Label>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/SalaryProcess.aspx", "INSERTR") == true)
                            { %>
                        <asp:Button runat="server" ID="btnSubmit" Text="Process" CssClass="form-control input-sm btn-primary" OnClick="btnSubmit_Click" />
                        <% } %>
                    </div>
                    <div class="col-md-5"></div>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
    
    <asp:GridView runat="server" ID="gdProcess" Visible="False"></asp:GridView>
</asp:Content>
