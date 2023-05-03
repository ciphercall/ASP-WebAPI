<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ItemDiscountEntry.aspx.cs" Inherits="DynamicMenu.Stock.UI.ItemDiscountEntry" %>

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
            Search_GetCompletionListSubName();
            $("#txtEffectFR,#txtEffectTo").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+10" });
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

        function Search_GetCompletionListSubName() {
            $("#<%=txtSUBCATNM.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListSubName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtSUBCATNM.ClientID %>").val() + "'}",
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
                        <h1>Discount Entry</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                        <!-- logout option button -->
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemDiscountEntry.aspx", "INSERTR") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                                </li>
                                <% } %>

                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemDiscountEntry.aspx", "DELETER") == true)
                                   { %>
                                <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                                </li>
                                <% } %>
                            </ul>
                        </div>
                        <!-- end logout option -->


                    </div>
                    <!-- content header end -->

                    <asp:Label ID="lblItemID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblSUBid" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMinItem" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMaxItem" runat="server" Visible="False"></asp:Label>

                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2">Category Name :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSUBCATNM" runat="server" TabIndex="1"
                                    OnTextChanged="txtSUBCATNM_TextChanged" AutoPostBack="True"
                                    CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Discount (%) :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDiscount" runat="server" TabIndex="2" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2">Effect From :</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEffectFR" ClientIDMode="Static" TabIndex="3"
                                    runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Effect to:</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEffectTo" ClientIDMode="Static" TabIndex="4"
                                    runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row text-center">
                            <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000"></asp:Label>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemDiscountEntry.aspx", "INSERTR") == true)
                                   { %>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="5"
                                    OnClick="btnSubmit_Click" CssClass="form-control input-sm btn-primary" />
                                <% } %>
                            </div>
                            <div class="col-md-5"></div>
                        </div>

                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                            <asp:GridView ID="gv_Discount" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both"
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="12px"
                                OnRowDeleting="gv_Discount_RowDeleting" ShowFooter="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Barcode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBarcode" runat="server" Text='<%# Eval("ITEMCD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox" runat="server" Text='<%# Eval("ITEMCD") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEM NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ITEMNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("ITEMNM") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DISCOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("DISCRT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("DISCRT") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EFFECT FROM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEffectDateFrom" runat="server" Text='<%# Eval("EFDT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("EFDT") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EFFECT TO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEffectDateTo" runat="server" Text='<%# Eval("ETDT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("ETDT") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Stock/UI/ItemDiscountEntry.aspx", "DELETER") == true)
                                               { %>
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CssClass="" Text="Edit" runat="server"
                                                ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                                TabIndex="2" />
                                            <% } %>
                                        </ItemTemplate>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
