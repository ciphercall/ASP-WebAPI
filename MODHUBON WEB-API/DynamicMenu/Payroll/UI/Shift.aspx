<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Shift.aspx.cs" Inherits="DynamicMenu.Payroll.UI.Shift" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-lightness/jquery.ui.theme.css" rel="stylesheet" />
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <link href="../../MenuCssJs/bootstrap-clock-picker/src/clockpicker.css" rel="stylesheet" />

    <link href="../../MenuCssJs/bootstrap-clock-picker/src/standalone.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/bootstrap-clock-picker/src/clockpicker.js"></script>
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
                <h1>Shift Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Shift.aspx", "INSERTR") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Shift.aspx", "UPDATER") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Shift.aspx", "DELETER") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                        </li>
                        <% } %>
                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <asp:Label ID="lblSHIFTID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                    <asp:GridView ID="gv_SHIFT" runat="server" BackColor="White" BorderStyle="None" CssClass="Gridview text-center" CellPadding="3"
                        CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" ShowFooter="True" Font-Italic="False" Width="100%"
                        OnRowCommand="gv_SHIFT_RowCommand" OnRowDataBound="gv_SHIFT_RowDataBound"
                        OnRowCancelingEdit="gv_SHIFT_RowCancelingEdit" OnRowDeleting="gv_SHIFT_RowDeleting"
                        OnRowEditing="gv_SHIFT_RowEditing" OnRowUpdating="gv_SHIFT_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblSHIFTID" runat="server" Text='<%# Eval("SHIFTID") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtItemCD" ReadOnly="true" runat="server" CssClass="form-control input-sm" />
                                </FooterTemplate>
                                <HeaderStyle Width="6%" />
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Shift Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSHIFTNMEdit" runat="server" TabIndex="10" CssClass="form-control input-sm" Text='<%# Eval("SHIFTNM") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSHIFTNM" runat="server" Text='<%# Eval("SHIFTNM") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtSHIFTNMFooter" TabIndex="1" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="16%" />
                                <ItemStyle Width="16%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time From">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTIMEFREdit" runat="server" TabIndex="10" CssClass="form-control clockpicker input-sm" Text='<%# Eval("TIMEFR") %>' Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTIMEFR" runat="server" Text='<%# Eval("TIMEFR") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTIMEFRFooter" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="form-control clockpicker input-sm" Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </FooterTemplate>
                                <HeaderStyle Width="8%" />
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time To">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTIMETOEdit" runat="server" TabIndex="10" CssClass="form-control clockpicker input-sm" Text='<%# Eval("TIMETO") %>' Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTIMETO" runat="server" Text='<%# Eval("TIMETO") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTIMETOFooter" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="form-control input-sm clockpicker" Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </FooterTemplate>
                                <HeaderStyle Width="8%" />
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            
                            
                            
                            

                            <asp:TemplateField HeaderText="Late From">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLATEFREdit" runat="server" TabIndex="10" CssClass="form-control clockpicker input-sm" Text='<%# Eval("LATEFR") %>' Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLATEFR" runat="server" Text='<%# Eval("LATEFR") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtLATEFRFooter" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="form-control input-sm clockpicker" Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </FooterTemplate>
                                <HeaderStyle Width="8%" />
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="OT From">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOTFROMEdit" runat="server" TabIndex="10" CssClass="form-control clockpicker input-sm" Text='<%# Eval("OTFROM") %>' Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOTFROM" runat="server" Text='<%# Eval("OTFROM") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOTFROMFooter" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="form-control input-sm clockpicker" Width="100%"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('.clockpicker').clockpicker();
                                        });
                                    </script>
                                </FooterTemplate>
                                <HeaderStyle Width="8%" />
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtREMARKSEdit" runat="server" TabIndex="11" CssClass="form-control input-sm" MaxLength="100" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtREMARKSFooter" runat="server" TabIndex="2" CssClass="form-control input-sm" MaxLength="100" Width="100%"></asp:TextBox>

                                </FooterTemplate>
                                <HeaderStyle Width="25%" />
                                <ItemStyle Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnPUpdate" TabIndex="12" runat="server" CommandName="Update" CssClass="" Height="20px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                    <asp:ImageButton ID="imgbtnPCancel" TabIndex="13" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/checkmark.png" ToolTip="Cancel" Width="20px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="3" CommandName="Add" CssClass="" Height="30px" ImageUrl="~/Images/AddNew.png" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" />
                                    <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" TabIndex="11" ToolTip="Delete" Width="20px" />
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
