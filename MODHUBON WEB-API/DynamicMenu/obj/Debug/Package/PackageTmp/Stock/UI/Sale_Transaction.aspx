<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Sale_Transaction.aspx.cs" Inherits="DynamicMenu.Stock.UI.Sale_Transaction" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../Content/js/select2.full.min.js"></script>
    <link href="../../Content/css/select2.css" rel="stylesheet" />
    

    <%--  <script src="../../Content/select2-4.0.3/js/select2.full.min.js"></script>
    <link href="../../Content/select2-4.0.3/css/select2.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        $(document).ready(function () {
            BindControlEvents();
            $("#<%=txtTransDate.ClientID%>").datepicker({ dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-10:+2" });
        });

        function pageLoad() {
            $(".select").select2();
            $("#<%=txtPartyName.ClientID%>").focus();

             $("#<%=txtTransDate.ClientID%>").datepicker({ dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-10:+2" });

        }
        function BindControlEvents() {
            Search_GetVehicles_Number();
            Search_Driver_Name();
            Search_Asst_Name();

            $("[id*=txtQtyFooter]").focusout(function () {
                 $("[id*=txtperRTFooter]").focus();
            });       
              
<%--            $("#<%=txtPartyName.ClientID%>").select(function () {
                $("#<%=txtVehicalsNo.ClientID%>").focus();
            });
            $("#<%=txtVehicalsNo.ClientID%>").focusout(function () {
                $("#<%=txtdriverNM.ClientID%>").focus();
            });
            $("#<%=txtdriverNM.ClientID%>").focusout(function () {
                $("#<%=txtasstNM.ClientID%>").focus();
            });
            $("#<%=txtasstNM.ClientID%>").focusout(function () {
                $("#<%=txtRemarks.ClientID%>").focus();
            }); 
            $("#<%=txtRemarks.ClientID%>").focusout(function () {
                $("[id*=txtItemNameFooter]").focus();
            });--%>
        }
        
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            }
            else {
                return false;
            }
        }
           

        function Search_GetVehicles_Number() {
            $("#<%=txtVehicalsNo.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetVehiclesNumber",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtVehicalsNo.ClientID %>").val() + "'}",
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
                select: function (event, ui) {
              <%--      $("#<%=Search.ClientID %>").focus();--%>
                    return true;
                }
            });
        }
        function Search_Driver_Name() {
            $("#<%=txtdriverNM.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetDriverName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtdriverNM.ClientID %>").val() + "'}",
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
                select: function (event, ui) {
              <%--      $("#<%=Search.ClientID %>").focus();--%>
                    return true;
                }
            });
        }
        function Search_Asst_Name() {
            $("#<%=txtasstNM.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../../search.asmx/GetAsstName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtasstNM.ClientID %>").val() + "'}",
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
                select: function (event, ui) {
              <%--      $("#<%=Search.ClientID %>").focus();--%>
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

        .auto-style1 {
            width: 6%;
        }

        .auto-style2 {
            width: 34%;
        }

        .auto-style3 {
            width: 65%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate> 
            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                       
                        <h1 align="left">Sale Information                      

                        </h1>
                     
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
                        <!-- logout option button -->
                        <%--  <div class="col-md-12">--%>
                        <%--  <div class="col-md-6"></div>--%>
                        <div class="btn-group pull-right" id="editOption">
                              <div class="col-md-4" >
                                <asp:Button ID="btnDifference" runat="server" CssClass="form-control input-sm btn-primary" Font-Bold="True"
                                  OnClick="btnDifference_OnClick"  Text="DIFF(Q)"/>

                            </div>
                            <div class="col-md-4">
                                <div align="right">
                                    <asp:Button ID="btnSaleEdit" runat="server" CssClass="form-control input-sm btn-primary" Font-Bold="True"
                                        OnClick="btnSaleEdit_OnClick" Text="EDIT" />
                                </div>
                            </div>
                            <div class="col-md-4" align="right">
                                <asp:Button ID="btnprint" runat="server" CssClass="form-control input-sm btn-primary" Font-Bold="True"
                                    OnClick="btnprint_OnClick" Text="PRINT" />

                            </div>
                            
                            <%-- <div class="col-md-2" style="text-align: right">
                                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                        <i class="fa fa-cog"></i>
                                    </button>
                                    <ul class="dropdown-menu pull-right" style="" role="menu">
                                        <% if (UserPermissionChecker.checkParmit("/Stock/UI/Sale_Transaction.aspx", "INSERTR"))
                                            { %>
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnEdit" CssClass="fa fa-plus" Text="Edit Record" />
                                        </li>
                                        <% } %>
                                    </ul>
                                </div>--%>
                        </div>
                        <%--    </div>--%>
                        <!-- end logout option -->
                    </div>
                    <!-- content header end -->


                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-10"></div>
                            <%-- <div class="col-md-2" align="right">
                                <asp:Button ID="btnSaleEdit" runat="server" CssClass="form-control input-sm btn-primary" Font-Bold="True"
                                    OnClick="btnSaleEdit_OnClick" Text="EDIT" />
                            </div>--%>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2">Date & Invoice No</div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtTransDate" OnTextChanged="txtTransDate_OnTextChanged" AutoPostBack="True"
                                    CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtTransactionNo" CssClass="form-control input-sm" ReadOnly="True"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="ddlTransactionNo" CssClass="form-control input-sm" Visible="False"
                                    OnSelectedIndexChanged="ddlTransactionNo_OnSelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">Vehicle No</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" TabIndex="2" ID="txtVehicalsNo" CssClass="form-control input-sm"></asp:TextBox>
                                <%-- <asp:TextBox runat="server" ID="txtPartyCode" Style="display: none" CssClass="form-control input-sm visible-false"></asp:TextBox>--%>
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2">Party Name</div>
                            <div class="col-md-4">
                                <%--<asp:DropDownList runat="server"  ID="txtPartyName" CssClass="form-control select" >
                                   
                                </asp:DropDownList>--%>
                                <asp:DropDownList runat="server" ID="txtPartyName" OnSelectedIndexChanged="txtPartyName_OnSelectedIndexChanged"
                                TabIndex="1"   AutoPostBack="True" CssClass="form-control select">
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtPartyCode" Style="display: none" CssClass="form-control input-sm visible-false"></asp:TextBox>
                                <asp:Label runat="server" ID="lblpartyID" Visible="False"></asp:Label>
                            </div>
                            <div class="col-md-2">Driver Name</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtdriverNM" TabIndex="3" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>


                        <div class="row form-class">

                            <div class="col-md-2">Remarks</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtRemarks" TabIndex="5" CssClass="form-control input-sm"></asp:TextBox>
                            </div>

                            <div class="col-md-2">Assistant</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtasstNM" TabIndex="4" CssClass="form-control input-sm"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-10"></div>
                            <div class="col-md-2" align="right">
                                <asp:Button ID="btncomplete" runat="server" CssClass="form-control input-sm btn-primary" Font-Bold="True"
                                   Visible="False" OnClick="btnciomplete_OnClick" Text="COMPLETE" />
                            </div>
                        </div>
                        <div class="row form-class text-center">
                            <asp:Label ID="lblMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                Visible="False"></asp:Label>
                        </div> 
                        <div class="row form-class">
                            <div class="col-md-12">
                                <div class="table  table-hover" style="border: 1px solid #ddd;">
                                    <asp:GridView ID="gv_details" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="Both" ShowFooter="True"
                                        BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Width="100%" Font-Names="Calibri" Font-Size="11px"
                                         OnRowCommand="gv_details_OnRowCommand" OnRowEditing="gv_details_OnRowEditing"
                                        OnRowCancelingEdit="gv_details_OnRowCancelingEdit" OnRowDataBound="gv_details_OnRowDataBound"
                                        OnRowDeleting="gv_details_OnRowDeleting" OnRowUpdating="gv_details_OnRowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Company Id" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCOMPID" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblCOMPIDEdit" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
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
                                                    <asp:Label ID="lblItemid" runat="server" Text='<%# Eval("ITEMID") %>' Style="display: none"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="txtItemNameEdit" runat="server" AutoPostBack="True" TabIndex="17"
                                                        OnSelectedIndexChanged="txtItemNameEdit_OnSelectedIndexChanged" CssClass="form-control select" Width="100%">
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="lblitemidedit" Text='<%# Eval("ITEMID") %>' AutoPostBack="True" Style="display: none"></asp:Label>
                                                    <asp:TextBox ID="lblItemCodeEdit" CssClass="form-control input-sm" Style="display: none" runat="server"
                                                        Width="100%" Text='<%# Eval("ITEMID") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="txtItemNameFooter" runat="server" TabIndex="6" CssClass="form-control select" AutoPostBack="True"
                                                        OnSelectedIndexChanged="txtItemNameFooter_OnSelectedIndexChanged" ClientIDMode="Static" Width="100%">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="lblItemCodeFooter" CssClass="form-control input-sm" Style="display: none"
                                                        runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
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
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            
                                            
                                            
                                             <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSQty" runat="server" Text='<%# Eval("ORDERQTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSQtyEdit" runat="server"
                                                   CssClass="form-control input-sm" ReadOnly="True" Width="100%" Text='<%# Eval("ORDERQTY") %>'>0.00</asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSQtyFooter" runat="server" CssClass="form-control input-sm"
                                                      ReadOnly="True"  Width="100%" ClientIDMode="Static">0.00</asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            


                                            <asp:TemplateField HeaderText="Sale Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyEdit" runat="server" AutoPostBack="True"
                                                  TabIndex="19"  CssClass="form-control input-sm" Width="100%" OnTextChanged="txtQtyEdit_OnTextChanged" Text='<%# Eval("QTY") %>'>.00</asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQtyFooter" runat="server" CssClass="form-control input-sm" AutoPostBack="True"
                                                    TabIndex="8" OnTextChanged="txtQtyFooter_OnTextChanged" Width="100%">.00</asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            
                                               

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRateEdit" runat="server" CssClass="form-control input-sm" ReadOnly="True" Width="100%" Text='<%# Eval("RATE") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRateFooter" runat="server" CssClass="form-control input-sm" ReadOnly="True" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lblAmountEdit" ReadOnly="True" CssClass="form-control input-sm text-right noedit" runat="server"
                                                        Width="100%" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="lblAmountFooter" ReadOnly="True" CssClass="form-control input-sm text-right noedit" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPer" runat="server" Text='<%# Eval("RETRT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPerRTEdit" CssClass="form-control input-sm text-right noedit" AutoPostBack="True"
                                                      TabIndex="20"  OnTextChanged="txtPerRTEdit_OnTextChanged" runat="server" Width="100%" Text='<%# Eval("RETRT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtperRTFooter" runat="server" CssClass="form-control input-sm text-right noedit"
                                                    TabIndex="10"    OnTextChanged="txtperRTFooter_OnTextChanged" AutoPostBack="True" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                            </asp:TemplateField>
                                            
                                            
                                            
                                              <asp:TemplateField HeaderText="Order Date" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblORDERDT" runat="server" Text='<%# Eval("ORDERDT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtORDERDTEdit" CssClass="form-control input-sm text-right noedit" ReadOnly="True" runat="server" Width="100%" Text='<%# Eval("ORDERDT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtORDERDTFoter" CssClass="form-control input-sm text-right noedit" ReadOnly="True" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnetret" runat="server" Text='<%# Eval("NETRT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtnetretEdit" CssClass="form-control input-sm text-right noedit" ReadOnly="True" runat="server" Width="100%" Text='<%# Eval("NETRT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtnetRetFoter" CssClass="form-control input-sm text-right noedit" ReadOnly="True" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnetAmnt" runat="server" Text='<%# Eval("NETAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtnetAmntEdit" ReadOnly="True" AutoPostBack="True"
                                                        OnTextChanged="txtnetAmntEdit_OnTextChanged" CssClass="form-control input-sm text-right noedit" runat="server"
                                                        Width="100%" Text='<%# Eval("NETAMT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtnetamntFooter" ReadOnly="True" AutoPostBack="True"
                                                        OnTextChanged="txtnetamntFooter_OnTextChanged" CssClass="form-control input-sm text-right noedit"
                                                        runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRemarksEdit" CssClass="form-control input-sm" runat="server" Width="100%" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRemarksFooter" CssClass="form-control input-sm" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" CssClass="" ImageUrl="~/Images/update.png"
                                                       TabIndex="21" ToolTip="Update" Height="20px" Width="20px" />
                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CssClass="" CommandName="Cancel" ImageUrl="~/Images/Cancel.png"
                                                      TabIndex="22"  ToolTip="Cancel" Height="20px" Width="20px" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%--<% if (UserPermissionChecker.checkParmit("/Stock/UI/Sale_Transaction.aspx", "UPDATER"))
                                                        { %>--%>
                                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CssClass="" runat="server" ImageUrl="~/Images/Edit.png"
                                                    TabIndex="23"    ToolTip="Edit" Height="20px" Width="20px" />
                                                    <%--  <% } %>
                                                    <% if (UserPermissionChecker.checkParmit("/Stock/UI/Sale_Transaction.aspx", "DELETER"))
                                                        { %>--%>
                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CssClass="" Text="Edit" runat="server"
                                                     TabIndex="24"   ImageUrl="~/Images/delete.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()" />
                                                    <%--    <% } %>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%--    <% if (UserPermissionChecker.checkParmit("/Stock/UI/Sale_Transaction.aspx", "INSERTR"))
                                                        { %>--%>
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" CssClass="" ImageUrl="~/Images/AddNewitem.png"
                                                    TabIndex="12" CommandName="SaveCon" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton" />

                                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="txtColor" TabIndex="13"
                                                        ImageUrl="~/Images/Checkmark.png" CommandName="Complete" ToolTip="Complete" ValidationGroup="validaiton"
                                                        Width="20px" Height="20px" />

                                                    <%--   <% } %>--%>
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
                                    <div class="col-md-4"></div>
                                    <div class="col-md-4">
                                        <table style="width: 100%" class="table">
                                            <%-- <tr>
                                                <td style="width: 50%; text-align: right">Total Qty</td>
                                                <td style="width: 10%; text-align: center">:</td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="lblTotalQty" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="text-align: right" class="auto-style3">Total Amount</td>
                                                <td style="width: 10%; text-align: center">:</td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="lblamount" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--   <tr>
                                                <td style="width: 50%; text-align: right"></td>
                                                <td style="width: 10%; text-align: center"></td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Button runat="server" ID="btnComplete" Visible="False" Text="Complete Transaction" 
                                                CssClass="form-control input-sm btn-warning" />
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                    <div class="col-md-4">
                                        <table style="width: 100%" class="table">
                                            <%-- <tr>
                                                <td style="width: 50%; text-align: right">Total Qty</td>
                                                <td style="width: 10%; text-align: center">:</td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Label runat="server" ID="lblTotalQty" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="text-align: left" class="auto-style2">Total Bill Amount</td>
                                                <td style="text-align: center" class="auto-style1">:</td>
                                                <td style="width: 40%; text-align: left">
                                                    <asp:Label runat="server" ID="lblTotalNetAmount" Text="0.00"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--   <tr>
                                                <td style="width: 50%; text-align: right"></td>
                                                <td style="width: 10%; text-align: center"></td>
                                                <td style="width: 40%; text-align: right">
                                                    <asp:Button runat="server" ID="btnComplete" Visible="False" Text="Complete Transaction" 
                                                CssClass="form-control input-sm btn-warning" />
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </div>
                                <div id="grid">
                                    <asp:Label ID="lblGridMSG" runat="server" Font-Bold="True" ForeColor="#CC0000"
                                        Visible="False"></asp:Label>
                                </div>

                                <asp:Label ID="lblItemID" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblSMY" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblSMxNo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblTransSL" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblTransSLItem" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblTransSL_log" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblcheckMSTNO" runat="server" Style="display: none"></asp:Label>
                                <asp:Label ID="lbltransNomst" runat="server" Style="display: none"></asp:Label>
                            </div>
                        </div>
                    </div>




                </div>
                <!-- Content End From here -->
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
