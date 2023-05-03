<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ItemInformation.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.ItemInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Item Information</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
                    </div>
                    <!-- content header end -->
                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        
                        <div class="row form-class">
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Show"
                                    CssClass="form-control input-sm btn-primary" OnClick="btnSearch_OnClick" />
                            </div>
                            <div class="col-md-5"></div>
                        </div>
                    </div>
                    <!-- Content End From here -->
                </div>
            </div>
</asp:Content>
