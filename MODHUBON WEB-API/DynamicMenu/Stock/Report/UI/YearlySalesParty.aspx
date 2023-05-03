<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="YearlySalesParty.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.YearlySalesParty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="/MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="/MenuCssJs/js/jquery-ui.js"></script>
    <link href="/MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
          <script src="../../../Content/js/select2.full.min.js"></script>
    <link href="../../../Content/css/select2.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function (e) {
            BindControlEvents();

        });
        function BindControlEvents() {
            $("#<%=txtFrom.ClientID%>").datepicker({
                changeMonth: false,
                changeYear: true,
                showButtonPanel: true,
                yearRange: '2010:2030',
                dateFormat: 'yy',
                onClose: function (dateText, inst) {
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, 0, 1));
                }
            });
         //   Search_GetCompletionListPartyName();
        };
        function pageLoad() {
            $(".select").select2();
        }

    </script>
    <style type="text/css">
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
                <h1>YEARLY SALES - PARTY</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->

                <!-- logout option button -->

                <!-- end logout option -->
                <asp:Label ID="lblPartyCD" runat="server" Visible="False"></asp:Label>
            </div>
            <!-- content header end -->
            <div class="form-class">
                <div class="row"></div>
              
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        Item Name :
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlPartyList" runat="server" CssClass="form-control select" ></asp:DropDownList>
                        <asp:TextBox ID="txtPartyCode" style="display: none" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>

                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        Year :
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFrom" runat="server"  CssClass="form-control input-sm text-center" ></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-4"></div>
                      <div class="col-md-2">
                        <asp:Button ID="btnqty" runat="server" Text="Qty" Font-Bold="True"
                            Font-Italic="False" CssClass="form-control input-sm btn-primary" OnClick="btnqty_OnClick" TabIndex="5" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnanount" runat="server" Text="Amount" Font-Bold="True"
                            Font-Italic="False" CssClass="form-control input-sm btn-primary" OnClick="btnanount_OnClick" TabIndex="5" />
                    </div>
                   
                    <div class="col-md-5"></div>
                </div>

                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <asp:Label ID="lblErrMsg" runat="server" Font-Bold="False" ForeColor="#990000"
                            Visible="False"></asp:Label>
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
