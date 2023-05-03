<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="CategoryEntry.aspx.cs" Inherits="DynamicMenu.Stock.UI.CategoryEntry" %>

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
            Search_GetCompletionListCategoryMasterName();

            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
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

        function Search_GetCompletionListCategoryMasterName() {
            $("#<%=txtCategoryNM.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListCategoryMasterName",
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
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Category Entry</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                        <!-- logout option button -->
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/CategoryEntry.aspx", "INSERTR") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/CategoryEntry.aspx", "UPDATER") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/CategoryEntry.aspx", "DELETER") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                                </li>
                                <% } %>
                            </ul>
                        </div>
                        <!-- end logout option -->
                    </div>
                    <!-- content header end -->

                    <asp:Label ID="lblIMaxItemID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMaxCatID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblSUBID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMSTID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblChkItemID" runat="server" Visible="False"></asp:Label>

                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">Category Name :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCategoryNM" runat="server" TabIndex="1" OnTextChanged="txtCategoryNM_TextChanged"
                                    CssClass="form-control input-sm" AutoPostBack="True"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Search" runat="server" TabIndex="2"
                                    Text="Search" OnClick="Search_Click" CssClass="form-control input-sm btn-primary" />
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div class="row text-center">
                            <asp:Label ID="lblMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                Visible="False"></asp:Label>
                        </div>


                        <div class="row form-class">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                                    <asp:GridView ID="gv_Sub" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                        BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                        OnRowCommand="gv_Sub_RowCommand" OnRowCancelingEdit="gv_Sub_RowCancelingEdit" OnRowEditing="gv_Sub_RowEditing"
                                        OnRowDeleting="gv_Sub_RowDeleting" OnRowUpdating="gv_Sub_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SUB ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSUBID" runat="server" Text='<%# Eval("SUBID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSUBIDEdit" runat="server" Text='<%# Eval("SUBID") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Category Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSUBNM" runat="server" Text='<%# Eval("SUBNM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSUBNMEdit" TabIndex="5" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("SUBNM") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSUBNMfooter" TabIndex="3" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("SUBNM") %>'></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="70%" />
                                                <ItemStyle HorizontalAlign="Center" Width="70%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" CssClass="" ImageUrl="~/Images/update.png"
                                                        ToolTip="Update" Height="20px" Width="20px" TabIndex="6" />
                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CssClass="" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                                        ToolTip="Cancel" Height="20px" Width="20px" TabIndex="7" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/CategoryEntry.aspx", "UPDATER") == true)
                                                       { %>
                                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CssClass="" runat="server" ImageUrl="~/Images/Edit.png"
                                                        ToolTip="Edit" Height="20px" Width="20px" TabIndex="8" />
                                                    <% } %>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/CategoryEntry.aspx", "DELETER") == true)
                                                       { %>
                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CssClass="" Text="Edit" runat="server"
                                                        ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                                        TabIndex="9" />
                                                    <% } %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/CategoryEntry.aspx", "INSERTR") == true)
                                                       { %>
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" CssClass="" ImageUrl="~/Images/AddNewitem.png" TabIndex="4"
                                                        CommandName="Add" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton" />
                                                    <% } %>
                                                </FooterTemplate>
                                                <FooterStyle Width="20%" />
                                                <ItemStyle Width="20%" />

                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle Font-Names="Calibri" Font-Size="14px" />
                                        <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#D9EDF7" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    </asp:GridView>
                                </div>
                                <div id="grid">
                                    <asp:Label ID="lblGridMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                        Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                    </div>




                </div>
                <!-- Content End From here -->
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
