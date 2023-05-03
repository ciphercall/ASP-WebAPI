<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="HoliDays.aspx.cs" Inherits="DynamicMenu.Payroll.UI.HoliDays" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $("#txtTRANSDTEdit,#txtTRANSDTFooter").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-00:+10" });
            Search_GetCompletionListGetCompletionListHoliDaysName();
            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
            $("[id*=txtHDAYNMEdit],[id*=txtHDAYNMFooter]").keydown(function (e) {
                if (e.which == 9 || e.which == 13)
                    window.__doPostBack();
            });
        });
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }
        function Search_GetCompletionListGetCompletionListHoliDaysName() {
            $("[id*=txtHDAYNMEdit],[id*=txtHDAYNMFooter]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListHoliDaysName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtHDAYNMEdit],[id*=txtHDAYNMFooter]").val() + "'}",
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
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Holiday Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/HoliDays.aspx", "INSERTR") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                       
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/HoliDays.aspx", "DELETER") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                        </li>
                        <% } %>
                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <asp:Label ID="lblHDAYID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2">Year :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlYR" runat="server" TabIndex="1" CssClass="form-control input-sm" Width="162px" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-4"></div>
                </div>
                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">

                <asp:GridView ID="gv_HDAY" runat="server" BackColor="White" BorderStyle="None" CssClass="Gridview text-center" CellPadding="3"
                    CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" ShowFooter="True" Font-Italic="False" Width="100%"
                    OnRowCancelingEdit="gv_HDAY_RowCancelingEdit"
                    OnRowCommand="gv_HDAY_RowCommand" OnRowDeleting="gv_HDAY_RowDeleting" OnRowEditing="gv_HDAY_RowEditing"
                    OnRowUpdating="gv_HDAY_RowUpdating" OnRowDataBound="gv_HDAY_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTRANSDTEdit" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control input-sm" TabIndex="10" Text='<%# Eval("TRANSDT") %>' Width="100%" OnTextChanged="txtTRANSDTEdit_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTRANSDT" runat="server" Text='<%# Eval("TRANSDT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtTRANSDTFooter" TabIndex="2" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control input-sm" OnTextChanged="txtTRANSDTFooter_TextChanged" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle Width="12%" HorizontalAlign="Center" />
                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Holiday">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHDAYNMEdit" runat="server" AutoPostBack="true" CssClass="form-control input-sm" TabIndex="10" Text='<%# Eval("HDAYNM") %>' Width="100%" OnTextChanged="txtHDAYNMTEdit_TextChanged"></asp:TextBox>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblHDAYNMNM" runat="server" Text='<%# Eval("HDAYNM") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtHDAYNMFooter" runat="server" AutoPostBack="true" CssClass="form-control input-sm" OnTextChanged="txtHDAYNMFooter_TextChanged" TabIndex="3" Width="100%"></asp:TextBox>

                            </FooterTemplate>
                            <HeaderStyle Width="20%" HorizontalAlign="Center" />
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblHDAYID" runat="server" Text='<%# Eval("HDAYID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblHDAYIDEdit" runat="server" Text='<%# Eval("HDAYID") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblHDAYIDFooter" runat="server" />
                            </FooterTemplate>
                            <HeaderStyle Width="6%" />
                            <ItemStyle Width="6%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:DropDownList ID="txtSTATUSEdit" runat="server" CssClass="form-control input-sm" TabIndex="11" Text='<%# Eval("STATUS") %>' Width="100%">
                                    <asp:ListItem>OPEN</asp:ListItem>
                                    <asp:ListItem>CLOSE</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="txtSTATUSFooter" runat="server" CssClass="form-control input-sm" TabIndex="4" Width="100%">
                                    <asp:ListItem>OPEN</asp:ListItem>
                                    <asp:ListItem>CLOSE</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                            <HeaderStyle Width="20%" HorizontalAlign="Center" />
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtREMARKSEdit" runat="server" CssClass="form-control input-sm" MaxLength="100" TabIndex="12" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtREMARKSFooter" runat="server" CssClass="form-control input-sm" MaxLength="100" TabIndex="5" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnPUpdate" runat="server" CommandName="Update" CssClass="" Height="20px" ImageUrl="~/Images/update.png" TabIndex="13" ToolTip="Update" Width="20px" />
                                <asp:ImageButton ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.png" TabIndex="14" ToolTip="Cancel" Width="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/HoliDays.aspx", "INSERTR") == true)
                                   { %>
                                <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="Add" CssClass="" Height="30px" ImageUrl="~/Images/AddNew.png" TabIndex="6" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                <% } %>
                            </FooterTemplate>
                            <ItemTemplate>
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/HoliDays.aspx", "DELETER") == true)
                                   { %>
                                <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" ToolTip="Delete" Width="20px" />
                                <% } %>
                            </ItemTemplate>
                            <HeaderStyle Width="6%" />
                            <ItemStyle Width="6%" />
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
