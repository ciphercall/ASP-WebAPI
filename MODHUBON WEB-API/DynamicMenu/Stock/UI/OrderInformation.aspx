<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="OrderInformation.aspx.cs" Inherits="DynamicMenu.Stock.UI.OrderInformation"%>

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
            GetCompletionListPartyNameWithId();
            GetCompletionListItemNameWithId();
            $("#txtOrderDate,#txtBillDate").datepicker({ dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-10:+2" });

           <%-- $("#txtOrderDate").change(function () {
                var date = $("#txtOrderDate").val();
                var edittype = $("#<%=btnEdit.ClientID %>").text();
                if (edittype === "Edit Record") {
                    var year = date.substring(6, 11);
                    if (year.length === 4) {
                        $.ajax({
                            url: '../../search.asmx/GetOrderInvoiceNo',
                            type: "POST",
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            data: "{ 'year' : '" + year + "'}",
                            success: function (data) {
                                $("#<%=txtTransactionNo.ClientID %>").val(data.d);
                                $("#<%=txtPartyName.ClientID %>").focus();
                            },
                            error: function (result) {
                                $("#<%=txtTransactionNo.ClientID %>").val("");
                                $("#<%=txtBillDate.ClientID %>").focus();
                            }
                        });
                    } else {
                        $("#<%=txtTransactionNo.ClientID %>").val("");
                        $("#<%=txtOrderDate.ClientID %>").focus();
                    }
                } else {
                    if (date.length === 10) {
                        $.ajax({
                            url: '../../search.asmx/GetTransactionNo',
                            type: "POST",
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            data: "{ 'date' : '" + date + "'}",
                            success: function (data) {
                                var ddlCustomers =  $("#<%=ddlTransactionNo.ClientID %>");
                                ddlCustomers.empty().append('<option value="--SELECT--">--SELECT--</option>');
                                $.each(data.d, function () {
                                    ddlCustomers.append("<option value='" + this['value'] + "'>" + this['value'] + "</option>");
                                });
                                $("#<%=ddlTransactionNo.ClientID %>").focus();
                            },
                            error: function (result) {
                                $("#<%=ddlTransactionNo.ClientID %>").items.clear();
                                $("#<%=txtBillDate.ClientID %>").focus();
                            }
                        });
                    } else {
                        $("#<%=txtTransactionNo.ClientID %>").val("");
                        $("#<%=txtOrderDate.ClientID %>").focus();
                    }
                }
            });--%>
            $("#txtBillDate").change(function () {
                $("#<%=txtRemarks.ClientID %>").focus();
            });
            $("[id*=txtQtyFooter]").change(function () {
                var qty = $("[id*=txtQtyFooter]").val();
                var rate = $("[id*=txtRateFooter]").val();
                var amount = qty * rate;
                $("[id*=lblAmountFooter]").val(amount);
                $("[id*=txtRemarksFooter]").focus();
            });
            $("[id*=txtQtyEdit]").change(function () {
                var qty = $("[id*=txtQtyEdit]").val();
                var rate = $("[id*=txtRateEdit]").val();
                var amount = qty * rate;
                $("[id*=lblAmountEdit]").val(amount);
                $("[id*=txtRemarksEdit]").focus();
            });
            $("[id*=txtRateFooter]").change(function () {
                var qty = $("[id*=txtQtyFooter]").val();
                var rate = $("[id*=txtRateFooter]").val();
                var amount = qty * rate;
                $("[id*=lblAmountFooter]").val(amount);
                $("[id*=txtRemarksFooter]").focus();
            });
            $("[id*=txtRateEdit]").change(function () {
                var qty = $("[id*=txtQtyEdit]").val();
                var rate = $("[id*=txtRateEdit]").val();
                var amount = qty * rate;
                $("[id*=lblAmountEdit]").val(amount);
                $("[id*=txtRemarksEdit]").focus();
            });
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            }
            else {
                return false;
            }
        }

        function GetCompletionListPartyNameWithId() {
            $("#<%=txtPartyName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListPartyNameWithId",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("#<%=txtPartyName.ClientID %>").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    partyid: item.value
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
                select: function (event, ui) {
                    $("#<%=txtPartyCode.ClientID %>").val(ui.item.partyid);
                    $("#<%=txtBillDate.ClientID %>").focus();
                    return true;
                }
            });
        }
        function GetCompletionListItemNameWithId() {
            $("[id*=txtItemNameFooter]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListItemNameWithId",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("[id*=txtItemNameFooter]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.value,
                                    value: item.value,
                                    itemid: item.label1,
                                    unit: item.label2,
                                    rate: item.label3
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
                select: function (event, ui) {
                    $("[id*=lblUnitFooter]").text(ui.item.unit);
                    $("[id*=txtRateFooter]").val(ui.item.rate);
                    $("[id*=lblItemCodeFooter]").val(ui.item.itemid);
                    $("[id*=txtQtyFooter]").focus();
                    return true;
                }
            });
        }
    </script>
    <style>
        .ui-autocomplete {
            max-width: 350px;
            max-height: 250px;
            overflow: auto;
        }

        .noedit {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Order Information</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
                        <!-- logout option button -->
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Stock/UI/OrderInformation.aspx", "INSERTR"))
                                    { %>
                                <li>
                                    <asp:LinkButton runat="server" ID="btnEdit" CssClass="fa fa-plus" Text="Edit Record" OnClick="btnEdit_OnClick" />
                                </li>
                                <% } %>
                            </ul>
                        </div>
                        <!-- end logout option -->
                    </div>
                    <!-- content header end -->


                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2">Date & Invoice No</div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtOrderDate" AutoPostBack="True" OnTextChanged="txtOrderDate_OnTextChanged" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtTransactionNo" CssClass="form-control input-sm" ReadOnly="True"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="ddlTransactionNo" CssClass="form-control input-sm" Visible="False"
                                    OnSelectedIndexChanged="ddlTransactionNo_OnSelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">Party Name</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" TabIndex="1" ID="txtPartyName" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtPartyCode" Style="display: none" CssClass="form-control input-sm visible-false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2">Bill Date</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" TabIndex="2" ID="txtBillDate" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Remarks</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" TabIndex="3" ID="txtRemarks" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>


                        <div class="row form-class text-center">
                            <asp:Label ID="lblMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                Visible="False"></asp:Label>
                        </div>


                        <div class="row form-class">
                            <div class="col-md-12">
                                <div class="table  table-hover" style="border: 1px solid #ddd;">
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
                                            <asp:TemplateField HeaderText="Itemid" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemid" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblItemidEdit" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item sl" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemSl" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Label ID="lblItemSlEdit" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEMNM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtItemNameEdit" TabIndex="21" ReadOnly="True" runat="server" CssClass="form-control input-sm" Width="100%"
                                                        Text='<%# Eval("ITEMNM") %>'></asp:TextBox>
                                                    <asp:TextBox ID="lblItemCodeEdit" CssClass="form-control input-sm" Style="display: none" runat="server" Width="100%" Text='<%# Eval("ITEMID") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemNameFooter" TabIndex="11" runat="server" CssClass="form-control input-sm"  Width="100%"></asp:TextBox>
                                                    <asp:TextBox ID="lblItemCodeFooter" CssClass="form-control input-sm" Style="display: none" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyEdit" TabIndex="22" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("QTY") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQtyFooter" TabIndex="12" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("MUNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblUnitEdit" runat="server" Width="100%" Text='<%# Eval("MUNIT") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblUnitFooter" runat="server" Width="100%"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRateEdit" runat="server" CssClass="form-control input-sm" Width="100%" Text='<%# Eval("RATE") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRateFooter" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lblAmountEdit" ReadOnly="True" CssClass="form-control input-sm text-right noedit" runat="server" Width="100%" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="lblAmountFooter" ReadOnly="True" CssClass="form-control input-sm text-right noedit" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRemarksEdit" TabIndex="23" CssClass="form-control input-sm" runat="server" Width="100%" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRemarksFooter" TabIndex="13" CssClass="form-control input-sm" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" CssClass="" ImageUrl="~/Images/update.png"
                                                        ToolTip="Update" Height="20px" Width="20px" TabIndex="28" />
                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CssClass="" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                                        ToolTip="Cancel" Height="20px" Width="20px" TabIndex="29" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/OrderInformation.aspx", "UPDATER"))
                                                        { %>
                                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CssClass="" runat="server" ImageUrl="~/Images/Edit.png"
                                                        ToolTip="Edit" Height="20px" Width="20px" TabIndex="98" />
                                                    <% } %>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/OrderInformation.aspx", "DELETER"))
                                                        { %>
                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CssClass="" Text="Edit" runat="server"
                                                        ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                                        TabIndex="99" />
                                                    <% } %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/OrderInformation.aspx", "INSERTR"))
                                                        { %>
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" CssClass="" ImageUrl="~/Images/AddNewitem.png" TabIndex="19"
                                                        CommandName="Add" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton" />
                                                    <% } %>
                                                </FooterTemplate>
                                                <FooterStyle Width="10%" />
                                                <ItemStyle Width="10%" />

                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle Font-Names="Calibri" Font-Size="14px" />
                                        <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#D9EDF7" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    </asp:GridView>
                                </div>
                                <div class="row">
                                    <div class="col-md-8"></div>
                                    <div class="col-md-4">
                                        <table style="width: 100%" class="table">
                                            <tr>
                                                <td style="width: 50%; text-align: right">Total Qty</td>
                                                <td style="width: 10%; text-align: center">:</td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="lblTotalQty" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; text-align: right">Total Amount</td>
                                                <td style="width: 10%; text-align: center">:</td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="lblTotalAmount" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; text-align: right"></td>
                                                <td style="width: 10%; text-align: center"></td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Button runat="server" ID="btnComplete" Text="Complete Transaction" CssClass="form-control input-sm btn-warning" OnClick="btnComplete_OnClick" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="grid">
                                    <asp:Label ID="lblGridMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                        Visible="False"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>




                </div>
                <!-- Content End From here -->
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
