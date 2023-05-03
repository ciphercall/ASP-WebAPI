<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="YearlyLeaveReport.aspx.cs" Inherits="DynamicMenu.Payroll.Report.UI.YearlyLeaveReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Yearly Leave Information</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Year :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlYR" runat="server" CssClass="input-sm form-control">
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
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" CssClass="btn-primary input-sm form-control" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
                
            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
