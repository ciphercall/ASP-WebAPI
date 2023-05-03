<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="CreateNotice.aspx.cs" Inherits="DynamicMenu.Stock.UI.CreateNotice" %>
<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            BindControlEvents();
        });
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            }
            else {
                return false;
            }
        }
        function BindControlEvents() {
            $("#txtMonthYear").datepicker({ dateFormat: "M-yy", changeMonth: true, changeYear: true, yearRange: "-2:+2" });
            $("[id*=txtEffectFromEdit],[id*=txtEffectFrom],[id*=txtEffectToEdit],[id*=txtEffectTo]").datepicker(
                { dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-2:+2" });
            $("#txtMonthYear").change(function() {
                window.__doPostBack();
            });
            $("#txtMonthYear").keypress(function () {
                $(this).val($.datepicker.formatDate('M-yy', new Date()));
            });
            $("[id*=txtEffectFromEdit],[id*=txtEffectFrom],[id*=txtEffectToEdit],[id*=txtEffectTo]").keypress(function() {
                $(this).val($.datepicker.formatDate('dd-mm-yy', new Date()));
            });
        };
    </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Notice Information</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
                        
                    </div>
                    <!-- content header end -->

                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row text-center">
                            <asp:Label runat="server" ID="lblErr" CssClass="red-color" Visible="False"></asp:Label>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4 text-right ">Party Name</div>
                            <div class="col-md-4">
                               <asp:TextBox runat="server" ID="txtMonthYear" CssClass="form-control input-sm"  MaxLength="8"
                                   OnTextChanged="txtMonthYear_OnTextChanged" AutoPostBack="True" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-4"></div>
                        </div>

                        <div class="table table-hover" style="border: 1px solid #ddd;">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                AllowPaging="True" 
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                                OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Year" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("NOTICEYY") %>'
                                                Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblYearEdit" runat="server" Text='<%#Eval("NOTICEYY") %>'
                                                Style="text-align: center" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="noticesl" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnoticesl" runat="server" Text='<%# Eval("NOTICESL") %>'
                                                Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblnoticeslEdit" runat="server" Text='<%#Eval("NOTICESL") %>'
                                                Style="text-align: center" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notice For">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoticeType" runat="server" Text='<%# Eval("NOTICETP") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlNoticeTypeEdit" runat="server" CssClass="form-control input-sm" TabIndex="26">
                                                <asp:ListItem Value="PARTY">PARTY</asp:ListItem>
                                               <%-- <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                                <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlNoticeType" runat="server" CssClass="form-control input-sm" TabIndex="6">
                                                <asp:ListItem Value="PARTY">PARTY</asp:ListItem>
                                              <%--  <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                                <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notice">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotice" runat="server" Text='<%# Eval("NOTICE") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNoticeEdit" runat="server" Text='<%#Eval("NOTICE") %>' MaxLength="100"
                                                CssClass="form-control input-sm" TabIndex="21" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNotice" runat="server" TabIndex="1"  MaxLength="100"
                                                CssClass="form-control input-sm" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Effect From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEffectFrom" runat="server" Text='<%# Eval("EFDT") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEffectFromEdit" runat="server" Text='<%#Eval("EFDT") %>' MaxLength="10"
                                                CssClass="form-control input-sm" TabIndex="22"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEffectFrom" runat="server" TabIndex="2" 
                                                CssClass="form-control input-sm" MaxLength="10"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Effect To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEffectTo" runat="server" Text='<%# Eval("ETDT") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEffectToEdit" runat="server" Text='<%#Eval("ETDT") %>'
                                                CssClass="form-control input-sm" TabIndex="23" MaxLength="10"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEffectTo" runat="server" TabIndex="3" CssClass="form-control input-sm" 
                                                MaxLength="10"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>'
                                                Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatusEdit" runat="server" CssClass="form-control input-sm" TabIndex="26">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="I">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-sm" TabIndex="6">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="I">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdate"  CommandName="Update" runat="server" formnovalidate="formnovalidate"  ImageUrl="~/Images/update.png"
                                                ToolTip="Update" Height="20px" Width="20px" TabIndex="28" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                                ToolTip="Cancel" Height="20px" Width="20px" TabIndex="29" formnovalidate="formnovalidate" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/CreateNotice.aspx", "UPDATER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.png"
                                                ToolTip="Edit" Height="20px" Width="20px" TabIndex="40" formnovalidate="formnovalidate" />
                                            <% } %>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/CreateNotice.aspx", "DELETER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                                ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                                TabIndex="41" />
                                            <% } %>
                                            <asp:ImageButton ID="btnPrint" runat="server" CommandName="print" ToolTip="Print" ImageUrl="../../Images/print.png"
                                                CssClass="glyphicon glyphicon-print" Height="20px" Width="20px" Visible="False" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/CreateNotice.aspx", "INSERTR"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.png" ValidationGroup="insert"
                                                CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new Record" 
                                                TabIndex="9" />
                                            <% } %>
                                        </FooterTemplate>
                                        <FooterStyle Width="10%" />
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
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


                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
