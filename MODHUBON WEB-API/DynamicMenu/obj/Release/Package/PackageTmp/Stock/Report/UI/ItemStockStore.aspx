<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ItemStockStore.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.ItemStockStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script type="text/javascript">
       $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {
            $("#txtDate").datepicker({ dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-100:+10" });
            Search_GetCompletionListSupplierName();
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
        }

        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            } else {
                return false;
            }
        }
        function Search_GetCompletionListSupplierName() {
            $("#<%=txtItemNM.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../../search.asmx/GetItemNameItemIdJSON",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("#<%=txtItemNM.ClientID %>").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    x: item.value
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
                select: function (event, ui) {
                    $("#<%=txtitemId.ClientID%>").val(ui.item.x);
                    return true;
                },
            });
            }

    </script>
    <style>
        .ui-autocomplete {
            max-width: 450px;
            max-height: 250px;
            overflow: auto;
        }
        .displaynone {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Item Stock</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Item Name :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtItemNM" runat="server"  CssClass="form-control input-sm"
                        TabIndex="1"></asp:TextBox>
                    
                          <asp:TextBox ID="txtitemId" runat="server" CssClass="form-control input-sm  displaynone" ></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True"  CssClass="form-control input-sm"
                        ClientIDMode="Static"  TabIndex="2"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row text-center">
                     <strong> <asp:Label ID="lblGridMsg" runat="server" Visible="False" ForeColor="#CC0000"
                            Style="font-weight: 700"></asp:Label></strong>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="form-control input-sm btn-primary" OnClick="btnSearch_Click" TabIndex="3" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
            </div>
            <!-- Content End From here -->
        </div>
    </div>
     <asp:Label ID="lblItemCD" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblITCD" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblStoreID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblItemNm" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblItemID" runat="server" Visible="False"></asp:Label>
</asp:Content>
