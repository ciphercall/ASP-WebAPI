<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ItemNameEntry.aspx.cs" Inherits="DynamicMenu.Stock.UI.ItemNameEntry" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/js/avro.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            BindControlEvents();
        });
        
        $(function () {
            $('[id*=txtItemNMBan],[id*=txtItemNMBanEdit]').avro();
        });
        function BindControlEvents() {
            Search_GetCompletionListSubCategoryName();
            Search_GetCompletionListItemNameWithOutItemCode();
            Search_GetCompletionListProductUnitMeasurement();
            $('.ui-autocomplete').select(function () {
                __doPostBack();
            });
            $('.ui-autocomplete').click(function () {
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
   <%-- <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>--%>

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
                    <asp:Label ID="LBLSTS" runat="server" style="display:none"></asp:Label>


                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                Category Name
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

                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd;">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                AllowPaging="True" PageSize="13" OnSorting="gridViewAslRole_Sorting" OnPageIndexChanging="gridViewAslRole_PageIndexChanging" 
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                                OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="ItemID">
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
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name (English)">
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
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name (Bengali)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemNMBan" runat="server" Text='<%# Eval("ITEMNMB") %>'
                                                Style="text-align: left" autocomplete="flase"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemNMBanEdit" runat="server" Text='<%#Eval("ITEMNMB") %>'
                                                CssClass="form-control input-sm" TabIndex="21" autocomplete="flase"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemNMBan" runat="server" TabIndex="2" CssClass="form-control input-sm" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("MUNIT") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnitEdit" runat="server" Text='<%#Eval("MUNIT") %>' Style="text-align: right"
                                                CssClass="form-control input-sm" TabIndex="23" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" TabIndex="4" Style="text-align: left"
                                                CssClass="form-control input-sm" Text="KG"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
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
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle Width="7%" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Rate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSaleRTEdit" runat="server" Style="text-align: right"
                                                Text='<%#Eval("SALRT") %>' TabIndex="26" CssClass="form-control input-sm"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSaleRT" runat="server" Style="text-align: right"
                                                TabIndex="7" CssClass="form-control input-sm"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleRT" runat="server" Style="text-align: right" Text='<%#Eval("SALRT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Right" Width="7%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Image">
                                        <EditItemTemplate>
                                            <asp:FileUpload ID="ImageFileUploadEdit" CssClass="form-control input-sm" runat="server" TabIndex="36"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:FileUpload ID="ImageFileUploadFooter" CssClass="form-control input-sm" runat="server" TabIndex="14"/>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imagePath" runat="server" Height="40px" ImageUrl='<%#Eval("IMAGEPATH") %>' Width="40px" />

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="17%" />
                                        <ItemStyle HorizontalAlign="Center" Width="17%" />
                                    </asp:TemplateField>
                                    
                                 <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlStatusEdit" TabIndex="26" CssClass="form-control input-sm" AutoPostBack="False">
                                                    <asp:ListItem >ACTIVE</asp:ListItem>
                                                    <asp:ListItem >INACTIVE</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlStatus" TabIndex="27" CssClass="form-control input-sm">
                                                    <asp:ListItem >ACTIVE</asp:ListItem>
                                                    <asp:ListItem >INACTIVE</asp:ListItem>
                                                </asp:DropDownList>    </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                            <ItemStyle HorizontalAlign="Center" Width="12%" />
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
                                            CssClass="glyphicon glyphicon-print" Height="20px" Width="20px" Visible="False"/>
                                             
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
   <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
