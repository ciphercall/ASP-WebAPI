<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="OverTimeStatementDaily.aspx.cs" Inherits="DynamicMenu.Payroll.Report.UI.OverTimeStatementDaily" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/m/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
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
                <h1>OVER TIME STATEMENT - DAILY</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" runat="server" onfocus="blur()" TabIndex="1" CssClass="form-control input-sm text-uppercase" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                 <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Shift :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlShift" runat="server" TabIndex="2" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Branch :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class text-center">
                    <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Search" TabIndex="4" CssClass="btn-primary form-control input-sm" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
