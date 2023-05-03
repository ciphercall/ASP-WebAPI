<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="SalesSummarizedStatement.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.SalesSummarizedStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFrom.ClientID%>,#<%=txtTo.ClientID%>").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>SALES SUMMARIZED STATEMENT</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control input-sm"
                            TabIndex="3"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTo" runat="server"  CssClass="form-control input-sm"
                             TabIndex="4"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row text-center">
                    <strong>
                        <asp:Label ID="lblGridMsg" runat="server" Visible="False" ForeColor="#CC0000"
                            Style="font-weight: 700"></asp:Label></strong>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div> 
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-control input-sm btn-primary"
                          OnClick="btnSearch_OnClick"  TabIndex="5" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
