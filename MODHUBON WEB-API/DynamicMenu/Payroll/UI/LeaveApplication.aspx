<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="DynamicMenu.Payroll.UI.LeaveApplication" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>

    <script>
        $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {

            $("#<%=txtDate.ClientID%>").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            $("[id*=txtLvTOFooter],[id*=txtLvFRFooter]").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            $("[id*=txtLvFREdit],[id*=txtLvTOEdit]").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        }
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
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>
            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>LEAVE APPLICATION INFORMATION</h1>

                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/LeaveApplication.aspx", "UPDATER"))
                                    { %>
                                <li><a href="#">
                                    <asp:LinkButton CssClass="fa fa-edit" ID="btnEdit" runat="server" OnClick="btnEdit_OnClick" Text="Edit"></asp:LinkButton>
                                </a>
                                </li>
                                <% } %>
                                <li><a href="#">
                                    <asp:LinkButton CssClass="fa fa-refresh" ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_OnClick"></asp:LinkButton></a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!-- content header end -->
                    <div class="form-class text-center">
                        <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        <asp:Label ID="lblExpRT" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        <asp:Label ID="lblCatTp" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        <asp:Label ID="lblPartyID" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        <asp:Label ID="lblGridDataCount" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                    </div>
                    <asp:Label ID="lblSTID" runat="server" Visible="False"></asp:Label>
                    <asp:TextBox ID="lblLEAVEID" runat="server" Visible="False"></asp:TextBox>

                    <div class="form-class">
                        <div class="row form-class3px">
                            <div class="col-md-2">Date </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control input-sm" Style="text-align: center" AutoPostBack="True" OnTextChanged="txtDate_OnTextChanged"></asp:TextBox>

                            </div>
                            <div class="col-md-1">Month/Year</div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtMY" CssClass="form-control input-sm text-center" oNfocus="blur()"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Serial No</div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtTransNo" CssClass="form-control input-sm text-center" Visible="True" oNfocus="blur()"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="ddlTransNo" CssClass="form-control input-sm text-center" Visible="False" AutoPostBack="True" OnTextChanged="ddlTransNo_OnTextChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-class3px">
                            <div class="col-md-2">Employee Name</div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlEMID" CssClass="form-control input-sm text-center" AutoPostBack="True" OnTextChanged="ddlEMID_OnTextChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">Remarks</div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control input-sm"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row form-class3px">
                            <div class="col-md-4"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnUpdateLeaveBY" runat="server" Text="Update Leave By" CssClass="form-control btn-primary input-sm" Visible="True" OnClick="btnUpdateLeaveBY_OnClick" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="50" CssClass="form-control input-sm btn-primary" Visible="True" OnClick="btnPrint_OnClick" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnDeleteMaster" runat="server" Text="Master Delete" TabIndex="50" CssClass="form-control input-sm btn-danger" Visible="False" />
                            </div>
                        </div>
                        <hr />

                        <%--grid view--%>
                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" GridLines="Both"
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Size="11px" OnRowCommand="gvDetails_OnRowCommand" OnRowDeleting="gvDetails_OnRowDeleting"
                                OnRowEditing="gvDetails_OnRowEditing" OnRowCancelingEdit="gvDetails_OnRowCancelingEdit" OnRowUpdating="gvDetails_OnRowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Leave ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLbID" runat="server" Text='<%# Eval("LEAVEID") %>' Style="display: none"></asp:Label>
                                            <asp:Label ID="lblLvNM" runat="server" Style="text-align: center" Text='<%# Eval("LEAVENM") %>'></asp:Label>

                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblLvIDEdit" runat="server" Text='<%# Eval("LEAVEID") %>' Style="display: none"></asp:Label>
                                            <asp:DropDownList ID="ddlLvNMEdit" runat="server" Style="text-align: center" CssClass="form-control input-sm"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblLvIDFooter" runat="server" Style="display: none"></asp:Label>
                                            <asp:DropDownList ID="ddlLvIDFooter" runat="server" TabIndex="1" CssClass="form-control input-sm"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLvFR" runat="server" Text='<%# Eval("LEAVEFR") %>' CssClass="form-control input-sm" Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLvFREdit" runat="server" CssClass="form-control input-sm" TabIndex="1"
                                                Text='<%# Eval("LEAVEFR") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLvFRFooter" runat="server" TabIndex="2" CssClass="form-control input-sm" Style="text-align: center"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave To">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLvTOEdit" runat="server" CssClass="form-control input-sm" Text='<%# Eval("LEAVETO") %>' Style="text-align: center" AutoPostBack="True" OnTextChanged="txtLvTOEdit_OnTextChanged"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLvTOFooter" runat="server" TabIndex="3" CssClass="form-control input-sm" Style="text-align: center" AutoPostBack="True" OnTextChanged="txtLvTOFooter_OnTextChanged"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLvTO" runat="server" Text='<%# Eval("LEAVETO") %>' CssClass="form-control input-sm" Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Days">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLvDays" runat="server" CssClass="form-control input-sm" TabIndex="62" Text='<%# Eval("LEAVEDAYS") %>' Style="text-align: center" oNfocus="blur()"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLvDaysFooter" runat="server" Style="text-align: center" CssClass="form-control input-sm" TabIndex="4" ClientIDMode="Static" oNfocus="blur()">0</asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLvDay" runat="server" Text='<%# Eval("LEAVEDAYS") %>' CssClass="form-control input-sm" Style="text-align: center" oNfocus="blur()"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtReasonEdit" runat="server" CssClass="form-control input-sm" TabIndex="63"
                                                Text='<%# Eval("REASON") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtReasonFooter" runat="server" TabIndex="5" CssClass="form-control input-sm"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                                ImageUrl="~/Images/update.png" TabIndex="65" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                                ImageUrl="~/Images/Cancel.png" TabIndex="66" ToolTip="Cancel" Width="20px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon"
                                                Height="20px" ImageUrl="~/Images/AddNew.png" TabIndex="34" ToolTip="Save &amp; Continue"
                                                ValidationGroup="validaiton" Width="20px" />
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete"
                                                Height="20px" ImageUrl="~/Images/checkmark.png" TabIndex="35" ToolTip="Complete"
                                                ValidationGroup="validaiton" Width="20px" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                                ImageUrl="~/Images/Edit.png" TabIndex="100" ToolTip="Edit" Width="20px" />
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/LeaveApplication.aspx", "DELETER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                                ImageUrl="~/Images/delete.png" OnClientClick="return confMSG()" TabIndex="101"
                                                ToolTip="Delete" Width="20px" />
                                            <% } %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                        <FooterStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle Font-Size="14px" />
                                <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#c79d6a" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
