<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="MenuCreate.aspx.cs" Inherits="DynamicMenu.ASLCompany.UI.MenuCreate" %>

<%@ Import Namespace="DynamicMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Search_Module();
            Search_MenuName();
            $("#<%=lblMsg.ClientID%>, #<%=lblMsgMenu.ClientID%>,#<%=lblGridMSG.ClientID%>").fadeOut(20000);
            $("#<%=lblMsg.ClientID%>, #<%=lblMsgMenu.ClientID%>,#<%=lblGridMSG.ClientID%>").text = "";
            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
            $('.ui-autocomplete').select(function () {
                __doPostBack();
            });
        });
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }
        function Search_Module() {
            $("#<%=txtModuleName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListModuleName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtModuleName.ClientID %>").val() + "'}",
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
        function Search_MenuName() {
            $("[id*=txtMenuName],[id*=txtMenuNameEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListMenuName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtMenuName],[id*=txtMenuNameEdit]").val() + "'}",
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Menu Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <li><a href="#"><i class="fa fa-plus"></i>Add Menu</a>
                        </li>
                        <li><a href="#"><i class="fa fa-edit"></i>Edit Menu</a>
                        </li>
                        <li><a href="#"><i class="fa fa-times"></i>Delete Menu</a>
                        </li>

                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->


            <div class="form-class">

                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <strong>Module Name :</strong>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtModuleName" ClientIDMode="Static"
                            CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txtModuleName_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>


                <div class="row form-class">
                    <div class="col-md-12 text-center">
                        <strong>
                            <asp:Label runat="server" ID="lblMsg" ForeColor="red" Visible="False"></asp:Label></strong>
                    </div>
                </div>
                <div class="row form-class">
                    <div class="col-md-3"></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="form-control input-sm btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="row text-center">
                    <asp:Label runat="server" ID="lblMsgMenu" ForeColor="red"></asp:Label>
                </div>
               
                    <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                            BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                            OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                            OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Module Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("MODULEID") %>' Width="98%"
                                            Style="text-align: center" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblModuleIdEdit" runat="server" Text='<%#Eval("MODULEID") %>' Width="98%"
                                            Style="text-align: center" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuId" runat="server" Text='<%# Eval("MENUID") %>' Width="98%"
                                            Style="text-align: center" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblMenuIdEdit" runat="server" Text='<%#Eval("MENUID") %>' Width="98%"
                                            Style="text-align: center" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuType" runat="server" Text='<%# Eval("MENUTP") %>' Width="98%"
                                            Style="text-align: left" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlMenuTypeEdit" CssClass="form-control input-sm">
                                            <asp:ListItem Value="SELECT">--SELECT--</asp:ListItem>
                                            <asp:ListItem Value="F">FORM</asp:ListItem>
                                            <asp:ListItem Value="R">REPORT</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList runat="server" ID="ddlMenuType" CssClass="form-control input-sm">
                                            <asp:ListItem Value="SELECT">--SELECT--</asp:ListItem>
                                            <asp:ListItem Value="F">FORM</asp:ListItem>
                                            <asp:ListItem Value="R">REPORT</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuName" runat="server" Text='<%# Eval("MENUNM") %>' Width="98%"
                                            Style="text-align: left" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMenuNameEdit" runat="server" Text='<%#Eval("MENUNM") %>' ClientIDMode="Static" CssClass="form-control input-sm" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" />
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                    <ItemStyle HorizontalAlign="Left" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu Link">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuLink" runat="server" Text='<%# Eval("FLINK") %>' Width="98%"
                                            Style="text-align: left" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMenuLinkEdit" runat="server" Text='<%#Eval("FLINK") %>' CssClass="form-control input-sm" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMenuLink" runat="server" CssClass="form-control input-sm" T />
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                    <ItemStyle HorizontalAlign="Left" Width="40%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu Serial">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuSerial" runat="server" Text='<%# Eval("MENUSL") %>' Width="98%"
                                            Style="text-align: left" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMenuSerialEdit" runat="server" Text='<%#Eval("MENUSL") %>' CssClass="form-control input-sm" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMenuSerial" runat="server" CssClass="form-control input-sm" T />
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.png"
                                            ToolTip="Update" Height="20px" Width="20px" TabIndex="28" />
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                            ToolTip="Cancel" Height="20px" Width="20px" TabIndex="29" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.png"
                                            ToolTip="Edit" Height="20px" Width="20px" TabIndex="40" />
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                            ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                            TabIndex="41" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.png"
                                            CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton"
                                            TabIndex="9" />
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
</asp:Content>
