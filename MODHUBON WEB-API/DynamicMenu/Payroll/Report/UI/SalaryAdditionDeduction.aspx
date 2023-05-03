<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="SalaryAdditionDeduction.aspx.cs" Inherits="DynamicMenu.Payroll.Report.UI.SalaryAdditionDeduction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../MenuCssJs/bootstrap/bootstrap-datepicker.js"></script>
    <link href="../../../MenuCssJs/bootstrap/datepicker.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $("#txtDate").datepicker({
                format: "M-yy",
                startView: "months",
                minViewMode: "months",
                autoclose: "true"
            });
        });

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
                <h1>Employee Salary Sheet(Addition/Deduction)</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Branch :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Month-Year :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" runat="server" MaxLength="6"  TabIndex="2" CssClass="form-control input-sm text-uppercase" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class text-center">
                    <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Search" TabIndex="3" CssClass="btn-primary form-control input-sm" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
