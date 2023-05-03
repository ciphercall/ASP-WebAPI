<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="SaleTransactionReceive.aspx.cs" Inherits="DynamicMenu.Stock.UI.SaleTransactionReceive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
<%--    <link href="../../MenuCssJs/bootstrap/datepicker.css" rel="stylesheet" />
    <link href="../../MenuCssJs/bootstrap/datepicker3.css" rel="stylesheet" />--%>
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
<%--    <script src="../../MenuCssJs/bootstrap/bootstrap-datepicker.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {
            $("#txtDate").datepicker({ dateFormat: "dd-mm-yy", changeMonth: true, changeYear: true, yearRange: "-100:+10" });
            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
            $('.ui-autocomplete').select(function () {
                __doPostBack();
            });
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
            } else {
                return false;
            }
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
                    <div id="">
                        <ul class="nav nav-tabs nav-justified">
                            <li role="presentation" class="active"><a href="SaleTransactionReceive.aspx">Receive</a></li>
                            <li role="presentation" class="grayBackground"><a href="SaleTransactionIssue.aspx">Issue</a></li>
                        </ul>
                    </div>
                    <!-- content header end -->


                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class3px">
                            <div class="col-md-2">Receive To :</div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" AutoPostBack="True" TabIndex="1" ID="ddlReceiveTo" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlReceiveTo_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">Date :</div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtDate" AutoPostBack="True" ClientIDMode="Static" CssClass="form-control input-sm" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnEdit" TabIndex="2" Text="EDIT" CssClass="btn-primary form-control input-sm" OnClick="btnEdit_Click" />
                            </div>
                        </div>
                        <div class="row form-class3px">
                            <div class="col-md-2">Invoice No :</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ReadOnly="True" ID="txtInvoiceNoReceive" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:DropDownList runat="server" Visible="False" AutoPostBack="True" ID="ddlInvoiceNoRcv" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlInvoiceNoRcv_SelectedIndexChanged" />
                            </div>
                            <div class="col-md-2">Memo No :</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server"  MaxLength="12" TabIndex="3" ID="txtMemoNo" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class3px">
                            <div class="col-md-2">Receive From :</div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" TabIndex="4" ID="ddlReceiveFrom" CssClass="form-control input-sm"></asp:DropDownList>
                              <%--  <asp:TextBox runat="server" ID="txtPartyName" TabIndex="4" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtPartyNameId" TabIndex="4" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>--%>
                            </div>
                            <div class="col-md-2">Remarks</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server"  MaxLength="100" TabIndex="5" ID="txtRemarks" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                   <%-- <script type="text/javascript">
                        $(document() {
                            $("#txtDate").datepicker(),
                            format: "dd-MM-yy",
                            autoclose:true
                        });
                    </script>--%>

                    <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                        <asp:GridView ID="gv_Receive" runat="server" BackColor="White" BorderStyle="None" CssClass="Gridview text-center" CellPadding="3"
                            CellSpacing="1" GridLines="Both" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowCancelingEdit="gv_Receive_RowCancelingEdit" OnRowCommand="gv_Receive_RowCommand"
                            OnRowDeleting="gv_Receive_RowDeleting" OnRowEditing="gv_Receive_RowEditing" OnRowUpdating="gv_Receive_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Serial">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblItemSLEdit_R" runat="server" Style="text-align: center" Text='<%# Eval("SL") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSl_R" runat="server" Style="text-align: center" Text='<%# Eval("SL") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Barcode">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtItCDREdit" runat="server"  MaxLength="9" ReadOnly="True" CssClass="form-control input-sm" Style="text-align: center" Text='<%#Eval("ITEMCD") %>'
                                            TabIndex="18" AutoPostBack="True"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtItCDR" runat="server"  MaxLength="9" CssClass="form-control input-sm" TabIndex="7"
                                            Font-Names="Calibri" Font-Size="12px" AutoPostBack="True"
                                            OnTextChanged="txtItCDR_TextChanged"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltCDR" runat="server" Style="text-align: center" Text='<%# Eval("ITEMCD") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Particulars">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtItemNMEdit_R" runat="server" ReadOnly="True" CssClass="form-control input-sm" TabIndex="19" Text='<%#Eval("ITEMNM") %>'
                                            AutoPostBack="True" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtItemNM_R" runat="server" ReadOnly="True" AutoPostBack="True" CssClass="form-control input-sm"
                                            TabIndex="8" />

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemNM_R" runat="server" Style="text-align: left" Text='<%# Eval("ITEMNM") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" Width="30%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item ID" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblItemIDEdit_R" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                            Width="120px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblItemID_R2" runat="server" CssClass="form-control input-sm" TabIndex="0" Width="120px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemID_R" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                            Width="120px" />
                                    </ItemTemplate>
                                    <ControlStyle Width="120px" />
                                    <FooterStyle HorizontalAlign="Center" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQtyEdit_R" runat="server" AutoPostBack="true" Style="text-align: right" CssClass="form-control input-sm"
                                            TabIndex="20" Text='<%#Eval("QTY") %>' OnTextChanged="txtQtyEdit_R_TextChanged"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtQty_R" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                            Style="text-align: right" TabIndex="9" OnTextChanged="txtQty_R_TextChanged"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty_R" runat="server" Style="text-align: right" Text='<%# Eval("QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Width="10%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRateEdit_R" runat="server" ReadOnly="True" Style="text-align: right" TabIndex="21" CssClass="form-control input-sm"
                                            Text='<%#Eval("RATE") %>' Width="110px" AutoPostBack="True" OnTextChanged="txtRateEdit_R_TextChanged"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtRate_R" runat="server" ReadOnly="True" CssClass="form-control input-sm" Style="text-align: right"
                                            TabIndex="10" AutoPostBack="True" OnTextChanged="txtRate_R_TextChanged"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate_R" runat="server" Style="text-align: right" Text='<%# Eval("RATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Width="15%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAmountEdit_R" runat="server" ReadOnly="True" CssClass="form-control input-sm" Style="text-align: right" Text='<%#Eval("AMOUNT") %>'
                                            Width="140px" TabIndex="22"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAmount_R" runat="server" ReadOnly="True" CssClass="form-control input-sm" Style="text-align: right"
                                            TabIndex="11">.00</asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount_R" runat="server" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Width="15%" />
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SL" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblTransSLEdit_R" runat="server" Text='<%# Eval("ITEMSL") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransSL_R" runat="server" Text='<%# Eval("ITEMSL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnRUpdate" runat="server" CommandName="Update" Height="20px"
                                            ImageUrl="~/Images/update.png" TabIndex="23" ToolTip="Update" Width="20px" />
                                        <asp:ImageButton
                                            ID="imgbtnRCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.png"
                                            TabIndex="24" ToolTip="Cancel" Width="20px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="imgbtnRAdd" runat="server" CommandName="SaveCon" CssClass=""
                                            Height="30px" ImageUrl="~/Images/AddNewitem.png" TabIndex="12" ToolTip="Save &amp; Continue"
                                            ValidationGroup="validaiton" Width="30px" />
                                        <asp:ImageButton ID="ImagebtnRComp" runat="server"
                                            CommandName="Complete" CssClass="" Height="30px" ImageUrl="~/Images/checkmark.png"
                                            TabIndex="13" ToolTip="Complete" ValidationGroup="validaiton" Width="30px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnREdit" runat="server" CommandName="Edit" Height="20px"
                                            ImageUrl="~/Images/Edit.png" TabIndex="15" ToolTip="Edit" Width="20px" />
                                        <asp:ImageButton
                                            ID="imgbtnRDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                            Height="20px" ImageUrl="~/Images/delete.png" TabIndex="16" ToolTip="Delete" Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle Font-Names="Calibri" Font-Size="14px" />
                            <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="13px" BackColor="#D9EDF7" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        </asp:GridView>
                        <table style="width: 100%; font-style: normal;">
                            <tr>
                                <td style="width: 40%">
                                    <asp:Label ID="lblRGridMSG" runat="server" Font-Bold="True" ForeColor="#CC3300" Visible="False"
                                        Font-Italic="True"></asp:Label>
                                </td>
                                <td style="width: 10%"></td>
                                <td style="width: 25%"></td>
                                <td style="width: 5%">
                                    <strong>Total </strong>
                                </td>

                                <td style="width: 10%">
                                    <asp:TextBox ID="txtRTotal" runat="server" ReadOnly="True" Style="text-align: right"
                                        TabIndex="17" Width="135px" CssClass="form-control input-sm">.00</asp:TextBox>
                                </td>
                                <td style="width: 1%"></td>
                                <td style="width: 10%"></td>
                            </tr>
                        </table>
                    </div>


                </div>
                <!-- Content End From here -->


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
