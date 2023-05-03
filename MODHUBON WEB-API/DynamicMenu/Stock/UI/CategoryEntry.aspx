<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="CategoryEntry.aspx.cs" Inherits="DynamicMenu.Stock.UI.CategoryEntry" %>

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
            $('[id*=txtSUBNMBanfooter],[id*=txtSUBNMBanEdit]').avro();
        });
        function BindControlEvents() {
            //Search_GetCompletionListCategoryMasterName();

            //$('.ui-autocomplete').click(function () {
            //    __doPostBack();
            //});
            //$('.ui-autocomplete').select(function () {
            //    __doPostBack();
            //});
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            }
            else {
                return false;
            }
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


                       <%-- <!-- logout option button -->
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
                        <!-- end logout option -->--%>
                    </div>
                    <!-- content header end -->

                  
                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                       
                        <div class="row text-center">
                            <asp:Label ID="lblMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                Visible="False"></asp:Label>
                        </div>


                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                <div class="table table-responsive table-hover" style="border: 1px solid #ddd;">
                                    <asp:GridView ID="gv_Sub" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                        BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                        OnRowCommand="gv_Sub_RowCommand" OnRowCancelingEdit="gv_Sub_RowCancelingEdit" OnRowEditing="gv_Sub_RowEditing"
                                        OnRowDeleting="gv_Sub_RowDeleting" OnRowUpdating="gv_Sub_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Company Id" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCOMPID" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblCOMPIDEdit" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSUBID" runat="server" Text='<%# Eval("CATID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSUBIDEdit" runat="server" Text='<%# Eval("CATID") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name (English)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSUBNM" runat="server" Text='<%# Eval("CATNM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSUBNMEdit" TabIndex="5" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("CATNM") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSUBNMfooter" TabIndex="3" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="35%" />
                                                <ItemStyle HorizontalAlign="Left" Width="35%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name (Bengali)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSUBNMBan" runat="server" Text='<%# Eval("CATNMB") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSUBNMBanEdit" TabIndex="5" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("CATNMB") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSUBNMBanfooter" TabIndex="4" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("CATNMB") %>'></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="35%" />
                                                <ItemStyle HorizontalAlign="Left" Width="35%" />
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
                            <div class="col-md-2"></div>
                        </div>
                    </div>




                </div>
                <!-- Content End From here -->
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
