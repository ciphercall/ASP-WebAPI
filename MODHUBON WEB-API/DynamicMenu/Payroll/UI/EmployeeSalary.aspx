<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="EmployeeSalary.aspx.cs" Inherits="DynamicMenu.Payroll.UI.EmployeeSalary" %>

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
          $("[id*=txtPOSTNMFooter],[id*=txtPOSTNMEdit]").keydown(function (e) {
                if (e.which == 9 || e.which == 13)
                    window.__doPostBack();
            });
        });
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }
        function Search_GetCompletionListGetCompletionListPostName() {
            $("[id*=txtPOSTNMFooter],[id*=txtPOSTNMEdit]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListPostName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("[id*=txtPOSTNMFooter],[id*=txtPOSTNMEdit]").val() + "'}",
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
                <h1>Salary Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalary.aspx", "INSERTR") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalary.aspx", "UPDATER") == true)
                           { %>
                        <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalary.aspx", "DELETER") == true)
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
            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2">Employee :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" Width="100%">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-4"></div>
                </div>
                
                <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                <asp:GridView ID="gv_Salary" runat="server" BackColor="White" BorderStyle="None" CssClass="Gridview text-center" CellPadding="3"
                    CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" ShowFooter="True" Font-Italic="False" Width="100%"
                    OnRowCommand="gv_Salary_RowCommand" OnRowCancelingEdit="gv_Salary_RowCancelingEdit" OnRowDeleting="gv_Salary_RowDeleting" OnRowEditing="gv_Salary_RowEditing" OnRowUpdating="gv_Salary_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Post Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPOSTNMEdit" AutoPostBack="true" ReadOnly="True" TabIndex="14" runat="server" CssClass="form-control input-sm" Text='<%# Eval("POSTNM") %>' Width="100%" OnTextChanged="txtPOSTNMEdit_TextChanged"></asp:TextBox>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPOSTNM" runat="server" Text='<%# Eval("POSTNM") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPOSTNMFooter" TabIndex="1" AutoPostBack="true" runat="server" CssClass="form-control input-sm" OnTextChanged="txtPOSTNMFooter_TextChanged" Width="100%"></asp:TextBox>

                            </FooterTemplate>
                            <ItemStyle Width="10%" />
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="lblPOSTIDEdit" runat="server" TabIndex="15" Text='<%# Eval("POSTID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPOSTID" runat="server" Text='<%# Eval("POSTID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblPOSTIDFooter" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>

                                <asp:DropDownList ID="ddlStatsEdit" runat="server" TabIndex="16" CssClass="form-control input-sm" Text='<%# Eval("SALSTATUS") %>' Width="100%">

                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                    <asp:ListItem>STANDARD</asp:ListItem>
                                    <asp:ListItem>INCREMENT</asp:ListItem>
                                    <asp:ListItem>PROMOTION</asp:ListItem>
                                    <asp:ListItem>DEMOTION</asp:ListItem>
                                    <asp:ListItem>SUSPEND</asp:ListItem>

                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("SALSTATUS") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlStatsFooter" TabIndex="2" runat="server" CssClass="form-control input-sm" Width="100%">
                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                    <asp:ListItem>STANDARD</asp:ListItem>
                                    <asp:ListItem>INCREMENT</asp:ListItem>
                                    <asp:ListItem>PROMOTION</asp:ListItem>
                                    <asp:ListItem>DEMOTION</asp:ListItem>
                                    <asp:ListItem>SUSPEND</asp:ListItem>

                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemStyle Width="8%" />
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salary(Basic)">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBASICSALEdit" runat="server" TabIndex="17" CssClass="form-control input-sm" Text='<%# Eval("BASICSAL") %>' Width="100%">0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBASICSAL" runat="server" Text='<%# Eval("BASICSAL") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtBASICSALFooter" TabIndex="3" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="House Rent">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHOUSERENTEdit" runat="server" TabIndex="18" CssClass="form-control input-sm" Text='<%# Eval("HOUSERENT") %>' Width="100%">0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblHOUSERENT" runat="server" Text='<%# Eval("HOUSERENT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtHOUSERENTFooter" TabIndex="4" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="4%" />
                            <ItemStyle HorizontalAlign="Right" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Medical">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMEDICALEdit" CssClass="form-control input-sm" TabIndex="19" runat="server" Text='<%# Eval("MEDICAL") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMEDICAL" runat="server" Text='<%# Eval("MEDICAL") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtMEDICALFooter" TabIndex="5" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="4%" />
                            <ItemStyle HorizontalAlign="Right" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transport" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTRANSPORTEdit" CssClass="form-control input-sm" TabIndex="20" runat="server" Text='<%# Eval("TRANSPORT") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTRANSPORT" runat="server" Text='<%# Eval("TRANSPORT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtTRANSPORTFooter" TabIndex="6" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conveyance">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtConveyEdit" CssClass="form-control input-sm" TabIndex="21" runat="server" Text='<%# Eval("CONVEY") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblConvey" runat="server" Text='<%# Eval("CONVEY") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtCONVEYFooter" TabIndex="7" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Others">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRSTAMPEdit" CssClass="form-control input-sm" TabIndex="21" runat="server" Text='<%# Eval("RSTAMP") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRSTAMP" runat="server" Text='<%# Eval("RSTAMP") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtRSTAMPFooter" TabIndex="7" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>

                        

                         <asp:TemplateField HeaderText="OT Rate">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOTRTHREdit" CssClass="form-control input-sm" TabIndex="21" runat="server" Text='<%# Eval("OTRTHOUR") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOTRTHR" runat="server" Text='<%# Eval("OTRTHOUR") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtOTRTHRFooter" TabIndex="7" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Present Rate">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPBONUSRTEdit" CssClass="form-control input-sm" TabIndex="21" runat="server" Text='<%# Eval("PBONUSRT") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPBONUSRT" runat="server" Text='<%# Eval("PBONUSRT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPBONUSRTFooter" TabIndex="7" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OTR Day" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOTRTDAYEdit" CssClass="form-control input-sm" TabIndex="21" runat="server" Text='<%# Eval("OTRTDAY") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOTRTDAY" runat="server" Text='<%# Eval("OTRTDAY") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtOTRTDAYFooter" TabIndex="7" runat="server" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PF. Rate" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPFRATEEdit" CssClass="form-control input-sm" TabIndex="22" runat="server" Text='<%# Eval("PFRATE") %>'>0.00</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPFRATE" runat="server" Text='<%# Eval("PFRATE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPFRATEFooter" runat="server" TabIndex="8" CssClass="form-control input-sm" Width="100%">0.00</asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PF From" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPFEFDTEdit" ClientIDMode="Static" TabIndex="23" CssClass="form-control input-sm" runat="server" Text='<%# Eval("PFEFDT") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPFEFDT" runat="server" Text='<%# Eval("PFEFDT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPFEFDTFooter" ClientIDMode="Static" TabIndex="9" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PF To" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPFETDTEdit" ClientIDMode="Static" TabIndex="24" CssClass="form-control input-sm" runat="server" Text='<%# Eval("PFETDT") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPFETDT" runat="server" Text='<%# Eval("PFETDT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPFETDTFooter" ClientIDMode="Static" TabIndex="10" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="3%" />
                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job From">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJOBEFDTEdit" ClientIDMode="Static" TabIndex="25" runat="server" CssClass="form-control input-sm" Text='<%# Eval("JOBEFDT") %>' OnTextChanged="txtJOBEFDTEdit_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblJOBEFDT" runat="server" Text='<%# Eval("JOBEFDT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtJOBEFDTFooter" ClientIDMode="Static" TabIndex="11" runat="server" CssClass="form-control input-sm" Width="100%" OnTextChanged="txtJOBEFDTFooter_TextChanged"></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="4%" />
                            <ItemStyle HorizontalAlign="Right" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job To">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJOBETDTEdit" ClientIDMode="Static" TabIndex="26" CssClass="form-control input-sm" runat="server" Text='<%# Eval("JOBETDT") %>' OnTextChanged="txtJOBETDTEdit_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblJOBETDT" runat="server" Text='<%# Eval("JOBETDT") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtJOBETDTFooter" ClientIDMode="Static" TabIndex="12" AutoPostBack="true" runat="server" CssClass="form-control input-sm" Width="100%" OnTextChanged="txtJOBETDTFooter_TextChanged"></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="4%" />
                            <ItemStyle HorizontalAlign="Right" Width="4%" />
                            <HeaderStyle />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnPUpdate" runat="server" TabIndex="27" CommandName="Update" CssClass="" Height="16px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                <asp:ImageButton ID="imgbtnPCancel" runat="server" TabIndex="28" CommandName="Cancel" Height="16px" ImageUrl="~/Images/Cancel.png" ToolTip="Cancel" Width="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                  <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalary.aspx", "INSERTR") != true)
                                     { %>
                                <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="13" CommandName="Add" CssClass="" Height="16px" ImageUrl="~/Images/AddNewitem.png" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                <% } %>
                            </FooterTemplate>
                            <ItemTemplate>
                                  <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalary.aspx", "UPDATER") != true)
                                     { %>
                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="16px" ImageUrl="~/Images/Edit.png" ToolTip="Edit" Width="20px" />
                                <% } %>
                                  <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalary.aspx", "DELETER") != true)
                                     { %>
                                <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="16px" ImageUrl="~/Images/delete.png" OnClientClick="return confMSG()" ToolTip="Delete" Width="20px" />
                                <% } %>
                            </ItemTemplate>
                            <HeaderStyle Width="4%" />
                            <ItemStyle Width="4%" />
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
