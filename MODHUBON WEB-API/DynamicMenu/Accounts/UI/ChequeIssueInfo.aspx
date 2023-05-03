<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ChequeIssueInfo.aspx.cs" Inherits="DynamicMenu.Accounts.UI.ChequeIssueInfo" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>

    <script>
        $(document).ready(function () {
            BindControlEvents();
          
        });
        function pageLoad() {
            GetIssueChequeNO();
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }
        function BindControlEvents() {
            $("#<%=txtTransDT.ClientID%>, [id*=txtChequeDt], [id*=txtChequeDtEdit]").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            Search_GetCompletionListLcBankName();
            GetIssueChequeNO();
            GetRcvPayHeadWithID();
            GetRcvPayHeadWithIDEdit();
        }

        
        function GetIssueChequeNO() {
            $("[id*=txtChequeNo],[id*=txtChequeNoEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetIssueChequeNO",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("[id*=txtChequeNo]").val() + "','BanckCD' : '" +  $("#<%=txtBankDEBITCD.ClientID %>").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    x: item.value
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
                    $("[id*=txtChequeNo]").val(ui.item.x);
                    $("[id*=txtChequeNo]").focus();
                  //  $("[id*=txtChequeNoEdit]").focus();
                    //pageLoad();
                    return true;
                }
            });
        }

        function Search_GetCompletionListLcBankName() {
            $("#<%=txtBankNm.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompaniesBankNameAndId",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("#<%=txtBankNm.ClientID %>").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    x: item.value
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
                    $("#<%=txtBankDEBITCD.ClientID%>").val(ui.item.x);
                    $("#<%=btnSubmit.ClientID%>").focus();
                    return true;
                }
            });
        }




        function GetRcvPayHeadWithID() {
            $("[id*=txtCreditNM]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetRcvPayHeadWithID",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("[id*=txtCreditNM]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    x: item.value
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
                    $("[id*=txtCreditCD]").val(ui.item.x);
                    $("[id*=txtAmount]").focus();

                    return true;
                }
            });
        }

        function GetRcvPayHeadWithIDEdit() {
            $("[id*=txtCreditNMEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetRcvPayHeadWithID",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'term' : '" + $("[id*=txtCreditNMEdit]").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    x: item.value
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
                    $("[id*=txtCreditCDEdit]").val(ui.item.x);
                    $("[id*=txtAmountEdit]").focus();

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>
            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>CHEQUE ISSUE INFORMATION</h1>
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <li><a href="#">
                                    <asp:LinkButton CssClass="fa fa-refresh" ID="btnRefresh" runat="server" Text="Refresh"></asp:LinkButton></a>
                                </li>
                            </ul>
                        </div>
                        <!-- end logout option -->


                    </div>
                    <!-- content header end -->
                    <div class="form-class text-center">
                        <asp:Label ID="lblVCount" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblcheck" runat="server" Visible="False"></asp:Label>
                        <%--<asp:TextBox ID="txtCreditCD" runat="server"></asp:TextBox>--%>
                        <asp:Label runat="server" ID="lblErrMsg" Visible="False" ForeColor="#990000" Style="font-weight: 700"></asp:Label>
                    </div>

                    <div class="form-class">
                        <div class="row form-class3px">
                            <div class="col-md-2">DATE</div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtTransDT" runat="server" ClientIDMode="Static" CssClass="form-control input-sm text-center" TabIndex="1" AutoPostBack="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2 text-left">Bank Name</div>
                            <div class="col-md-4">

                                <asp:TextBox ID="txtBankNm" runat="server" AutoPostBack="True" CssClass="form-control input-sm" ></asp:TextBox>
                                <asp:TextBox ID="txtBankDEBITCD" runat="server" AutoPostBack="False" CssClass="form-control input-sm" Style="display: none"></asp:TextBox>

                            </div>

                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="form-control btn-primary input-sm" Text="Submit" OnClick="btnSubmit_OnClick" />
                            </div>
                        </div>
                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" GridLines="Both"
                                BorderStyle="None" CellPadding="4" CssClass="Gridview text-center" Font-Size="11px" Width="100%" OnRowDataBound="gvDetails_RowDataBound"
                                OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting" OnRowCancelingEdit="gvDetails_RowCancelingEdit" OnRowEditing="gvDetails_RowEditing"
                                OnRowUpdating="gvDetails_RowUpdating" OnRowDeleted="gvDetails_RowDeleted" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" >
                                        <EditItemTemplate>
                                            <asp:Label ID="lblTransNoEdit" runat="server" TabIndex="62" Text='<%# Eval("TRANSNO") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTransNo" runat="server" CssClass="form-control input-sm" TabIndex="32" AutoPostBack="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransNo" runat="server" Text='<%# Eval("TRANSNO") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChequeNo" runat="server" Style="text-align: center" Text='<%# Eval(" CHEQUENO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtChequeNoEdit" CssClass="form-control input-sm" OnTextChanged="txtChequeNoEdit_OnTextChanged" AutoPostBack="True" Style="text-align: center" TabIndex="31" Text='<%# Eval(" CHEQUENO") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control input-sm" OnTextChanged="txtChequeNo_OnTextChanged" AutoPostBack="True"  TabIndex="11" Text='<%# Eval(" CHEQUENO") %>'></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="7" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChequeDt" runat="server" Style="text-align: center" Text='<%# Eval("CHEQUEDT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtChequeDt" runat="server" CssClass="form-control input-sm"  TabIndex="12" ClientIDMode="Static"></asp:TextBox>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtChequeDtEdit" runat="server" ClientIDMode="Static" TabIndex="32" CssClass="form-control input-sm" Style="text-align: center" Text='<%# Eval("CHEQUEDT") %>'></asp:TextBox>
                                        </EditItemTemplate>

                                        <FooterStyle CssClass="txtalign" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransMode" runat="server" Text='<%#Eval("TRANSMODE") %>' Width="100%" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlTransModeEdit" runat="server" Width="98%" CssClass="form-control input-sm" TabIndex="33">
                                                <asp:ListItem>CASH CHEQUE</asp:ListItem>
                                                <asp:ListItem>A/C PAYEE CHEQUE</asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:TextBox runat="server" ID="txtmodeedit" style="display: none"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlTransMode" runat="server" Width="98%" AutoPostBack="False" CssClass="form-control input-sm" TabIndex="13">
                                                <asp:ListItem>CASH CHEQUE</asp:ListItem>
                                                <asp:ListItem>A/C PAYEE CHEQUE</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox runat="server" ID="txtmode" style="display: none"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                        <ItemStyle HorizontalAlign="Center" Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank-Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChqBankBr" runat="server" Text='<%# Eval("CHQBANKBR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtChqBankBr" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" TabIndex="14"></asp:TextBox>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtChqBankBrEdit" runat="server" TabIndex="34" CssClass="form-control input-sm" Style="text-align: center" Text='<%# Eval("CHQBANKBR") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterStyle CssClass="txtalign" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditNM" runat="server" Text='<%# Eval("ACNM") %>'></asp:Label>
                                            <asp:Label ID="lblCreditCD" runat="server" Text='<%# Eval("CREDITCD") %>' Style="display: none"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCreditNM" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" TabIndex="14"></asp:TextBox>
                                            <asp:TextBox ID="txtCreditCD" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" Style="display: none" TabIndex="14"></asp:TextBox>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCreditNMEdit" runat="server" TabIndex="34" CssClass="form-control input-sm" Style="text-align: center" Text='<%# Eval("ACNM") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtCreditCDEdit" runat="server" TabIndex="34" CssClass="form-control input-sm" Style="text-align: center; display: none" Text='<%# Eval("DEBITCD") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterStyle CssClass="txtalign" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="23%" />
                                        <ItemStyle HorizontalAlign="Center" Width="23%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" Style="text-align: right" CssClass="form-control input-sm" TabIndex="15">.00</asp:TextBox>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAmountEdit" runat="server" TabIndex="35" CssClass="form-control input-sm" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                        </EditItemTemplate>

                                        <FooterStyle CssClass="txtalign" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" Width="8%" />
                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="form-control input-sm"
                                                TabIndex="36" Text='<%# Eval("REMARKS") %>' Style="text-align: left"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control input-sm"
                                                TabIndex="16" Style="text-align: left"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("STATUS") %>' Width="100%" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatusEdit" runat="server" Width="98%" CssClass="form-control input-sm" TabIndex="37"
                                                AutoPostBack="True">
                                                <asp:ListItem>ISSUED</asp:ListItem>
                                                 <asp:ListItem>HONORED</asp:ListItem>
                                              <%--  <asp:ListItem>RECEIVED</asp:ListItem>--%>
                                                <asp:ListItem>DISHONORED</asp:ListItem>
                                                <asp:ListItem>RETURNED</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox runat="server" ID="txtstatusedit" style="display: none"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="98%" AutoPostBack="False" CssClass="form-control input-sm" TabIndex="17">
                                                <asp:ListItem>ISSUED</asp:ListItem>
                                                 <asp:ListItem>HONORED</asp:ListItem>
                                               <%-- <asp:ListItem>RECEIVED</asp:ListItem>--%>
                                                <asp:ListItem>DISHONORED</asp:ListItem>
                                                <asp:ListItem>RETURNED</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/ACCOUNTS/UI/ChequeIssueInfo.aspx", "UPDATER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                                ImageUrl="~/Images/update.png" TabIndex="64" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                                ImageUrl="~/Images/Cancel.png" TabIndex="38" ToolTip="Cancel" Width="20px" />
                                            <% } %>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/ACCOUNTS/UI/ChequeIssueInfo.aspx", "INSERTR"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon"
                                                Height="25px" ImageUrl="~/Images/AddNewitem.png" TabIndex="18" ToolTip="Save &amp; Continue"
                                                ValidationGroup="validaiton" Width="25px" />                                            
                                            <% } %>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/ACCOUNTS/UI/ChequeIssueInfo.aspx", "UPDATER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                                ImageUrl="~/Images/Edit.png" TabIndex="100" ToolTip="Edit" Width="20px" />
                                            <% } %>
                                            <% if (UserPermissionChecker.checkParmit("/ACCOUNTS/UI/ChequeIssueInfo.aspx", "DELETER"))
                                                { %>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete"  Height="20px" 
                                                ImageUrl="~/Images/delete.png" OnClientClick="return confMSG()" TabIndex="101"
                                                ToolTip="Delete" Width="21px" />                                                                                      
                                             <asp:ImageButton ID="ImagebtnPPrint" runat="server"  CommandName="Print" CssClass="txtColor" Height="20px" 
                                                      CommandArgument="<%#((GridViewRow)Container).RowIndex%>"  ImageUrl="~/Images/print.png"  TabIndex="24" ToolTip="Save &amp; Print"  Width="20px" />
                                            <% } %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:TemplateField>
                                 <%--    <asp:TemplateField> 
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="print" PostBackUrl="../Report/Report/ChequeReceiveInfoVoucher.aspx?<%# Eval("TRANSNO") %>">Print</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EditRowStyle Font-Size="14px" />
                                <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#6299ad" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnSave_Print" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

