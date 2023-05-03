<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="orderToSaleProcess.aspx.cs" Inherits="DynamicMenu.Stock.UI.orderToSaleProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/js/function.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID%>").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+100" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Order To Sale Process</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


           
                <!-- end logout option -->

                <asp:Label ID="lblPCheckDate" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTransSLItem" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblItemRate" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblItemUnit" runat="server" Visible="False"></asp:Label>


            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Date <strong>:</strong></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-sm" AutoPostBack="True"
                              TabIndex="1"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>

                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnProcess" runat="server" CssClass="form-control btn-primary"
                          OnClick="btnProcess_OnClick"  Font-Bold="True" Font-Italic="True" Text="Process"
                            TabIndex="2" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                  
                    <asp:GridView ID="gvSale" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="GridView3" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gridMultiple" runat="server">
                    </asp:GridView>
                </div>
            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
