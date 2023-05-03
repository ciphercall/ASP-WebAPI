<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="DynamicMenu.Payroll.UI.Leave" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
          
        });
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
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
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Leave Basic Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Leave.aspx", "INSERTR") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Leave.aspx", "UPDATER") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Leave.aspx", "DELETER") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                        </li>
                        <% } %>
                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <asp:Label ID="lblLEAVEID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>

                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                    <asp:GridView ID="gv_LEAVE" runat="server" BackColor="White" BorderStyle="None" CssClass="Gridview text-center" CellPadding="3"
                        CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" ShowFooter="True" Font-Italic="False" Width="100%"
                        OnRowCommand="gv_LEAVE_RowCommand" OnRowDataBound="gv_LEAVE_RowDataBound"
                        OnRowCancelingEdit="gv_LEAVE_RowCancelingEdit" OnRowDeleting="gv_LEAVE_RowDeleting"
                        OnRowEditing="gv_LEAVE_RowEditing" OnRowUpdating="gv_LEAVE_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblLEAVEID" runat="server" Text='<%# Eval("LEAVEID") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtItemCD" ReadOnly="true" runat="server" Width="98%" CssClass="form-control" />
                                </FooterTemplate>
                                <HeaderStyle Width="6%" />
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Leave Particulars">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLEAVENMEdit" runat="server" TabIndex="10" CssClass="form-control" Text='<%# Eval("LEAVENM") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLEAVENM" runat="server" Text='<%# Eval("LEAVENM") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtLEAVENMFooter" TabIndex="1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtREMARKSEdit" runat="server" TabIndex="11" CssClass="form-control" MaxLength="100" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtREMARKSFooter" runat="server" TabIndex="2" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="30%" />
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnPUpdate" TabIndex="12" runat="server" CommandName="Update" CssClass="" Height="20px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                    <asp:ImageButton ID="imgbtnPCancel" TabIndex="13" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.png" ToolTip="Cancel" Width="20px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                     <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Leave.aspx", "INSERTR") == true)
                                        { %>
                                    <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="3" CommandName="Add" CssClass="" Height="30px" ImageUrl="~/Images/AddNew.png" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                    <% } %>
                                </FooterTemplate>
                                <ItemTemplate>
                                     <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Leave.aspx", "UPDATER") == true)
                                        { %>
                                    <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png" TabIndex="20" ToolTip="Edit" Width="20px" />
                                    <% } %>
                                     <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Leave.aspx", "DELETER") == true)
                                        { %>
                                    <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" TabIndex="21" ToolTip="Delete" Width="20px" />
                                    <% } %>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                                <ItemStyle Width="4%" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle Font-Names="Calibri" Font-Size="14px" />
                        <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#D9EDF7" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:GridView>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
