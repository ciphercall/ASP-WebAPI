<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="LeaveYear.aspx.cs" Inherits="DynamicMenu.Payroll.UI.LeaveYear" %>
<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $("#txtPFEFDTEdit,#txtPFEFDTFooter,#txtPFETDTEdit,#txtPFETDTFooter,#txtJOBEFDTEdit,#txtJOBEFDTFooter,#txtJOBETDTEdit,#txtJOBETDTFooter").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            Search_GetCompletionListGetCompletionListPostName();
            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
            $("[id*=txtLEAVENMFooter],[id*=txtLEAVENMEdit]").keydown(function (e) {
                if (e.which == 9 || e.which == 13)
                    window.__doPostBack();
            });
        });

        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }

        function Search_GetCompletionListGetCompletionListPostName() {
            $("[id*=txtLEAVENMFooter],[id*=txtLEAVENMEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListLeaveName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtLEAVENMFooter],[id*=txtLEAVENMEdit]").val() + "'}",
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
                <h1>Yearly Leave Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->

            </div>
            <!-- content header end -->
            
             <asp:Label ID="lblLEAVEYYID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2">Year :</div>
                    <div class="col-md-4">
                          <asp:DropDownList ID="ddlYR" runat="server" TabIndex="1" CssClass="form-control input-sm" Width="160px" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-5"></div>
                </div>
                
                <div>
                    <asp:GridView ID="gv_LEAVEYY" runat="server" AutoGenerateColumns="False"  Width="100%" OnRowCommand="gv_LEAVEYY_RowCommand" ShowFooter="True" OnRowCancelingEdit="gv_LEAVEYY_RowCancelingEdit" OnRowDeleting="gv_LEAVEYY_RowDeleting" OnRowEditing="gv_LEAVEYY_RowEditing" OnRowUpdating="gv_LEAVEYY_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Leave Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLEAVENM" runat="server" Text='<%# Eval("LEAVENM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLEAVENMEdit" ClientIDMode="Static" runat="server" AutoPostBack="true" TabIndex="10" CssClass="form-control input-sm" Text='<%# Eval("LEAVENM") %>' Width="100%" OnTextChanged="txtLEAVENMEdit_TextChanged"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox  ID="txtLEAVENMFooter"  TabIndex="1" AutoPostBack="true" runat="server" Width="98%"  CssClass="form-control input-sm" OnTextChanged="txtLEAVENMFooter_TextChanged" ></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLEAVEID" runat="server" Text='<%# Eval("LEAVEID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                               <asp:Label ID="lblLEAVEIDFooter"  TabIndex="2" runat="server"  />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Leave Days">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLEAVEDDEdit" runat="server" TabIndex="11" CssClass="form-control input-sm" Text='<%# Eval("LEAVEDD") %>' Width="100%" OnTextChanged="txtLEAVEDDEdit_TextChanged"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLEAVEDD" runat="server" Text='<%# Eval("LEAVEDD") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtLEAVEDDFooter" TabIndex="2" runat="server" CssClass="form-control input-sm" Width="100%" OnTextChanged="txtLEAVEDDFooter_TextChanged"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtREMARKSEdit" runat="server" TabIndex="12" CssClass="form-control input-sm" MaxLength="100" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtREMARKSFooter" runat="server" TabIndex="3" CssClass="form-control input-sm" MaxLength="100" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                             <HeaderStyle Width="15%" />
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtnPUpdate" TabIndex="13" runat="server" CommandName="Update" CssClass="" Height="20px" ImageUrl="~/Images/update.png"  ToolTip="Update" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPCancel" TabIndex="14" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.png"  ToolTip="Cancel" Width="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/LeaveYear.aspx", "INSERTR"))
                                                   { %>
                                                <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="4" CommandName="Add" CssClass="" Height="30px" ImageUrl="~/Images/AddNew.png"  ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                                <% } %>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/LeaveYear.aspx", "UPDATER"))
                                                   { %>
                                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png"  ToolTip="Edit" Width="20px" />
                                                <% } %>
                                                 <% if (UserPermissionChecker.checkParmit("/Payroll/UI/LeaveYear.aspx", "DELETER"))
                                                    { %>
                                                <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()"  ToolTip="Delete" Width="20px" />
                                                <% } %>
                                            </ItemTemplate>
                                             <HeaderStyle Width="4%" />
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#2aabd2" BorderColor="#333333" BorderWidth="2px" ForeColor="White" />
                                </asp:GridView>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
