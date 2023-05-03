<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ItemNameEntry.aspx.cs" Inherits="DynamicMenu.Stock.UI.ItemNameEntry" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {
            Search_GetCompletionListSubCategoryName();
            Search_GetCompletionListItemNameWithOutItemCode();
            Search_GetCompletionListBrandName();
            Search_GetCompletionListProductUnitMeasurement();
            Search_GetCompletionListItemAuthorName();
            Search_GetCompletionListItemPublicationName();
            $('.ui-autocomplete').select(function () {
                __doPostBack();
            });
          
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            }
            else {
                return false;
            }
        }

        function Search_GetCompletionListSubCategoryName() {
            $("#<%=txtCategoryNM.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListSubCategoryName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtCategoryNM.ClientID %>").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
            });
        }
        function Search_GetCompletionListItemNameWithOutItemCode() {
            $("[id*=txtItemNM],[id*=txtItemNMEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListItemNameWithOutItemCode",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtItemNM],[id*=txtItemNMEdit]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
            });
        }

        function Search_GetCompletionListBrandName() {
            $("[id*=txtBrandEdit],[id*=txtBrand]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListBrandName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtBrandEdit],[id*=txtBrand]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
            });
        }
        function Search_GetCompletionListProductUnitMeasurement() {
            $("[id*=txtUnitEdit],[id*=txtUnit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListProductUnitMeasurement",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtUnitEdit],[id*=txtUnit]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
            });
        }
        function Search_GetCompletionListItemAuthorName() {
            $("[id*=txtAuthor],[id*=txtAuthorEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListItemAuthorName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtAuthor],[id*=txtAuthorEdit]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
            });
        }
        function Search_GetCompletionListItemPublicationName() {
            $("[id*=txtPubYear],[id*=txtPubYearEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListItemPublicationName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtPubYear],[id*=txtPubYearEdit]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    value: item
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
            });
        }
    </script>
    <style>
        .ui-autocomplete {
            max-width: 350px;
            max-height: 250px;
            overflow: auto;
        }
         td > a {
            font-size: 16px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Item Information</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                        <!-- logout option button -->
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemNameEntry.aspx", "INSERTR") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemNameEntry.aspx", "UPDATER") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemNameEntry.aspx", "DELETER") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                                </li>
                                <% } %>
                            </ul>
                        </div>
                        <!-- end logout option -->


                    </div>
                    <!-- content header end -->

                    <asp:Label ID="lblChkItemID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblSUBID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMaxCatID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblIMaxItemID" runat="server" Visible="False"></asp:Label>

                    <asp:Button ID="Search" runat="server" Font-Bold="True" Font-Italic="True" TabIndex="2"
                        Text="Search" CssClass="form-control input-sm" OnClick="Search_Click" Visible="False" />

                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                Sub Category Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCategoryNM" runat="server" TabIndex="1" OnTextChanged="txtCategoryNM_TextChanged"
                                    CssClass="form-control input-sm" AutoPostBack="True"></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPrint" runat="server" CssClass="form-control input-sm" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class">

                            <div class="col-md-12 text-center">
                                
                            </div>

                        </div>

                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                AllowPaging="True" PageSize="13" OnSorting="gridViewAslRole_Sorting" OnPageIndexChanging="gridViewAslRole_PageIndexChanging" 
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                                OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="ItemID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemID" runat="server" Text='<%# Eval("ITEMID") %>'
                                                Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblItemID" runat="server" Text='<%#Eval("ITEMID") %>'
                                                Style="text-align: center" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Barcode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCD" runat="server" Text='<%# Eval("ITEMCD") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemCD" ReadOnly="true" runat="server" TabIndex="3" CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemNM" runat="server" Text='<%# Eval("ITEMNM") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemNMEdit" runat="server" Text='<%#Eval("ITEMNM") %>'
                                                CssClass="form-control input-sm" TabIndex="21" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemNM" runat="server" TabIndex="2" CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("BRAND") %>' Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBrandEdit" runat="server" Text='<%#Eval("BRAND") %>' Style="text-align: right"
                                                CssClass="form-control input-sm" TabIndex="22" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBrand" runat="server" TabIndex="3" Style="text-align: left" CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UNIT") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnitEdit" runat="server" Text='<%#Eval("UNIT") %>' Style="text-align: right"
                                                CssClass="form-control input-sm" TabIndex="23" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" TabIndex="4" Style="text-align: left"
                                                CssClass="form-control input-sm" Text="PCS"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity Per Carton" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPack" runat="server" Text='<%#Eval("PQTY") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPackEdit" runat="server" Text='<%#Eval("PQTY") %>' Style="text-align: right"
                                                CssClass="form-control input-sm" TabIndex="24" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPack" runat="server" TabIndex="5" Style="text-align: left"
                                                CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Author">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAuthor" runat="server" Text='<%#Eval("AUTHOR") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAuthorEdit" runat="server" Text='<%#Eval("AUTHOR") %>' 
                                                CssClass="form-control input-sm" TabIndex="24" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAuthor" runat="server" TabIndex="5" Style="text-align: left"
                                                CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edition">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("VERSION") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVersionEdit" runat="server" Text='<%#Eval("VERSION") %>' Style="text-align: right"
                                                CssClass="form-control input-sm" TabIndex="24" TextMode="Number" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtVersion" runat="server" TabIndex="5" Style="text-align: left"
                                                CssClass="form-control input-sm" TextMode="Number" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Publication Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPubYear" runat="server" Text='<%#Eval("PUBNM") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPubYearEdit" runat="server" Text='<%#Eval("PUBNM") %>' MaxLength="100" Style="text-align: right"
                                                CssClass="form-control input-sm" TabIndex="24" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPubYear" runat="server" TabIndex="5" Style="text-align: left" MaxLength="100"
                                                CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Returnable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReturnable" runat="server" Text='<%#Eval("RETTP") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlRetunableEdit" TabIndex="24" CssClass="form-control input-sm">
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList runat="server" ID="ddlRetunable" TabIndex="5" CssClass="form-control input-sm">
                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Buy Rate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBuyRTEdit" runat="server" Style="text-align: right" Text='<%#Eval("BUYRT") %>'
                                                CssClass="form-control input-sm" TabIndex="25"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBuyRT" runat="server" Style="text-align: right" TabIndex="6"
                                                CssClass="form-control input-sm"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBuyRT" runat="server" Style="text-align: right"
                                                Text='<%#Eval("BUYRT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle Width="8%" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Rate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSaleRTEdit" runat="server" Style="text-align: right"
                                                Text='<%#Eval("SALERT") %>' TabIndex="26" CssClass="form-control input-sm"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSaleRT" runat="server" Style="text-align: right"
                                                TabIndex="7" CssClass="form-control input-sm"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleRT" runat="server" Style="text-align: right" Text='<%#Eval("SALERT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commission Rate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCommissionRTEdit" runat="server" Style="text-align: right"
                                                Text='<%#Eval("RETRT") %>' TabIndex="27" CssClass="form-control input-sm"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCommissionRT" runat="server" TabIndex="8" Style="text-align: right"
                                                CssClass="form-control input-sm"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommissionRT" runat="server" Style="text-align: right" Text='<%#Eval("RETRT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min. Qty">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMinsQtyEdit" runat="server" Width="80px" Style="text-align: right"
                                                Text='<%#Eval("MINSQTY") %>' TabIndex="27" CssClass="form-control input-sm"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMinsQty" runat="server" Style="text-align: right" Text="3"
                                                TabIndex="8" CssClass="form-control input-sm"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMisQty" runat="server" Style="text-align: right" Text='<%#Eval("MINSQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.png"
                                                ToolTip="Update" Height="20px" Width="20px" TabIndex="28" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                                ToolTip="Cancel" Height="20px" Width="20px" TabIndex="29" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemNameEntry.aspx", "UPDATER") == true)
                                               { %>
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.png"
                                                ToolTip="Edit" Height="20px" Width="20px" TabIndex="40" />
                                            <% } %>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemNameEntry.aspx", "DELETER") == true)
                                               { %>
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                                ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                                TabIndex="41" />
                                            <% } %>
                                          <%--  <form id="form1" method="post" action="http://completeke.com/student/barcode" target="_blank">
                                                <asp:TextBox ID="id" runat="server"
                                                Text='<%#Eval("ITEMCD") %>'></asp:TextBox>
                                                <asp:TextBox ID="name" runat="server"
                                                Text='<%#Eval("ITEMNM") %>'></asp:TextBox>
                                                <asp:TextBox ID="price" runat="server"
                                                Text='<%#Eval("SALERT") %>'></asp:TextBox>   </form>--%>
                                            <asp:ImageButton ID="btnPrint" runat="server" CommandName="print" ToolTip="Print" ImageUrl="../../Images/print.png"
                                            CssClass="glyphicon glyphicon-print" Height="20px" Width="20px" />
                                             
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemNameEntry.aspx", "INSERTR") == true)
                                               { %>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.png" 
                                                CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton"
                                                TabIndex="9" />
                                            <% } %>
                                        </FooterTemplate>
                                        <FooterStyle Width="5%" />
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle Font-Names="Calibri" Font-Size="14px" />
                                <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#D9EDF7" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            </asp:GridView>
                            <div class="text-center">
                                <asp:Label ID="lblGridMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                            </div>

                        </div>

                    </div>
                    <!-- Content End From here -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
