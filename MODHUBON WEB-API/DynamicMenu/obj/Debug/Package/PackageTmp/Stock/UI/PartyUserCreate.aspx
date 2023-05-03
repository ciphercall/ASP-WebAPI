<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="PartyUserCreate.aspx.cs" Inherits="DynamicMenu.Stock.UI.PartyUserCreate" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Item Information</h1>
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
                                <asp:DropDownList runat="server" ID="ddlPartyList" CssClass="form-control input-sm" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlPartyList_OnSelectedIndexChanged"/>
                            </div>
                            <div class="col-md-4"></div>
                        </div>

                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd;">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                AllowPaging="True" 
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                                OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="PsId" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPsId" runat="server" Text='<%# Eval("PSID") %>'
                                                Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPsIdEdit" runat="server" Text='<%#Eval("PSID") %>'
                                                Style="text-align: center" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserCode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserCode" runat="server" Text='<%# Eval("USERCD") %>'
                                                Style="text-align: center" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblUserCodeEdit" runat="server" Text='<%#Eval("USERCD") %>'
                                                Style="text-align: center" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USERNM") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUserNameEdit" runat="server" Text='<%#Eval("USERNM") %>' MaxLength="100"
                                                CssClass="form-control input-sm" TabIndex="21" required="true" ValidationGroup="update"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" required="true"  MaxLength="100"
                                                CssClass="form-control input-sm" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile1" runat="server" Text='<%# Eval("MOBNO1") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMobile1Edit" runat="server" Text='<%#Eval("MOBNO1") %>' MaxLength="11"
                                                CssClass="form-control input-sm" TabIndex="22" required="true" ValidationGroup="update"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMobile1" runat="server" TabIndex="2" required="true" 
                                                CssClass="form-control input-sm" ValidationGroup="insert" MaxLength="11"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile 2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile2" runat="server" Text='<%# Eval("MOBNO2") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMobile2Edit" runat="server" Text='<%#Eval("MOBNO2") %>'
                                                CssClass="form-control input-sm" TabIndex="23" MaxLength="11"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMobile2" runat="server" TabIndex="3" CssClass="form-control input-sm" 
                                                MaxLength="11"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Login Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("LOGINID") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLoginIdEdit" runat="server" Text='<%#Eval("LOGINID") %>' MaxLength="11"
                                                CssClass="form-control input-sm" TabIndex="24" required="true" ValidationGroup="update"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLoginId" runat="server" TabIndex="4" required="true" 
                                                CssClass="form-control input-sm" ValidationGroup="insert" MaxLength="11"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Password">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("LOGINPW") %>'
                                                Style="text-align: left" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPasswordEdit" runat="server" Text='<%#Eval("LOGINPW") %>' MaxLength="50"
                                                CssClass="form-control input-sm" TabIndex="25" required="true" ValidationGroup="update"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPassword" runat="server" TabIndex="5" required="true"  MaxLength="50"
                                                CssClass="form-control input-sm" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>'
                                                Style="text-align: left" />
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
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdate"  CommandName="Update" runat="server" formnovalidate="formnovalidate"  ImageUrl="~/Images/update.png"
                                                ToolTip="Update" Height="20px" Width="20px" TabIndex="28" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                                ToolTip="Cancel" Height="20px" Width="20px" TabIndex="29" formnovalidate="formnovalidate" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/PartyUserCreate.aspx", "UPDATER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.png"
                                                ToolTip="Edit" Height="20px" Width="20px" TabIndex="40" formnovalidate="formnovalidate" />
                                            <% } %>
                                           <%-- <% if (UserPermissionChecker.checkParmit("/Stock/UI/PartyUserCreate.aspx", "DELETER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                                ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                                TabIndex="41" />
                                            <% } %>--%>
                                            <asp:ImageButton ID="btnPrint" runat="server" CommandName="print" ToolTip="Print" ImageUrl="../../Images/print.png"
                                                CssClass="glyphicon glyphicon-print" Height="20px" Width="20px" Visible="False" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/PartyUserCreate.aspx", "INSERTR"))
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
