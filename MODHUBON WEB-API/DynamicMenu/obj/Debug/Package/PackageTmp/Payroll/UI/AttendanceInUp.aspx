<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="AttendanceInUp.aspx.cs" Inherits="DynamicMenu.Payroll.UI.AttendanceInUp" %>
<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../MenuCssJs/ui-lightness/jquery.ui.theme.css" rel="stylesheet" />
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <link href="../../MenuCssJs/bootstrap-clock-picker/src/clockpicker.css" rel="stylesheet" />

    <link href="../../MenuCssJs/bootstrap-clock-picker/src/standalone.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/bootstrap-clock-picker/src/clockpicker.js"></script>
    <script>
        $(document).ready(function () {
            BindControlEvents();
        });

        function BindControlEvents() {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            $(".clockpicker").clockpicker({
                placement: 'left',
                align: 'left',
                donetext: 'Done'
            });
        }

        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
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
                        <h1>Attendance Daily</h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                        <!-- logout option button -->
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceInUp.aspx", "INSERTR") == true)
                                    { %>
                                <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceInUp.aspx", "UPDATER") == true)
                                    { %>
                                <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceInUp.aspx", "DELETER") == true)
                                    { %>
                                <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                                </li>
                                <% } %>
                            </ul>
                        </div>
                        <!-- end logout option -->


                    </div>
                    <!-- content header end -->

                    <asp:Label ID="lblLEAVEID" runat="server" Visible="False"></asp:Label>

                    <asp:Label ID="lblLate" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblIntiem" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblOTHOUR" runat="server" Visible="False"></asp:Label>

                    <!-- Content Start From here -->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class">
                            <div class="col-md-2">Unit :</div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1" CssClass="form-control input-sm" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">Date :</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control input-sm text-uppercase" MaxLength="6" 
                                  AutoPostBack="True"  TabIndex="2" ClientIDMode="Static" OnTextChanged="txtMnYear_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row text-center">
                            <asp:Label runat="server" ID="lblError" ForeColor="red" Visible="False"></asp:Label>

                        </div>
                        <div class="table table-responsive table-hover" style="border: 1px solid #ddd; border-radius: 5px;">
                            <asp:GridView ID="gvdetails" runat="server" BackColor="White" BorderStyle="None" CssClass="Gridview text-center" CellPadding="3"
                                CellSpacing="1" GridLines="Both" AutoGenerateColumns="False" ShowFooter="True" Font-Italic="False" Width="100%"
                                OnRowCommand="gvdetails_RowCommand" OnRowDataBound="gvdetails_RowDataBound"
                                OnRowCancelingEdit="gvdetails_RowCancelingEdit" OnRowDeleting="gvdetails_RowDeleting"
                                OnRowEditing="gvdetails_RowEditing" OnRowUpdating="gvdetails_RowUpdating">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblShiftIdEdit" runat="server"  Text='<%# Eval("SHIFTID") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftIdMy" runat="server" Text='<%# Eval("SHIFTID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblShiftIdFooter" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEmpIdEdit" runat="server"  Text='<%# Eval("EMPID") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("EMPID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblEmpIdFooter" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                    

                                    <asp:TemplateField HeaderText="Employee Name">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEmployeeNamedit"  TabIndex="50" runat="server" Text='<%# Eval("EMPNM") %>' Width="100%"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbEmployeeName" runat="server" Text='<%# Eval("EMPNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlEmployeeNameFooter" TabIndex="10"  runat="server" CssClass="form-control input-sm" 
                                                 OnSelectedIndexChanged="ddlEmployeeNameFooter_SelectedIndexChanged" AutoPostBack="True" Width="100%"></asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemStyle Width="20%" />
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblShiftEdit"  runat="server" Text='<%# Eval("SHIFTNM") %>' Width="100%"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbShift" runat="server" Text='<%# Eval("SHIFTNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblShiftFooter"  runat="server" Width="100%"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="15%"  HorizontalAlign="Center"/>
                                        <HeaderStyle Width="15%"  HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="In Time">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtIntimeEdit" TabIndex="51" CssClass="form-control input-sm clockpicker" runat="server" Text='<%# Eval("TIMEIN") %>' Width="100%" AutoPostBack="True" OnTextChanged="txtIntimeEdit_OnTextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtLatetimeEdit" CssClass="form-control input-sm clockpicker"  TabIndex="52" runat="server" Text='<%# Eval("LATEHR") %>' Width="100%" style="display: none"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("TIMEIN") %>'></asp:Label>
                                            <asp:Label ID="lblLatetime" runat="server" Text='<%# Eval("LATEHR") %>' style="display: none"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtIntimeFooter" TabIndex="11"  runat="server" CssClass="form-control input-sm clockpicker" Width="100%" AutoPostBack="True" OnTextChanged="txtIntimeFooter_OnTextChanged"></asp:TextBox>
                                       <asp:TextBox ID="txtLatetimeFooter" TabIndex="12"  runat="server" CssClass="form-control input-sm clockpicker" Width="100%" style="display: none"></asp:TextBox>
                                             </FooterTemplate>
                                        <ItemStyle Width="15%" />
                                        <HeaderStyle Width="15%" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Late Time" Visible="True">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLatetimeEdit" CssClass="form-control input-sm clockpicker"  TabIndex="52" runat="server" Text='<%# Eval("LATEHR") %>' Width="100%" style="display: none"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLatetime" runat="server" Text='<%# Eval("LATEHR") %>' style="display: none"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLatetimeFooter" TabIndex="12"  runat="server" CssClass="form-control input-sm clockpicker" Width="100%" style="display: none"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Width="15%" />
                                        <HeaderStyle Width="15%" />
                                    </asp:TemplateField>--%>

                                     <asp:TemplateField HeaderText="Out Time">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOuttimeEdit" CssClass="form-control input-sm clockpicker"  TabIndex="52" runat="server" Text='<%# Eval("TIMEOUT") %>' Width="100%" AutoPostBack="True" OnTextChanged="txtOuttimeEdit_OnTextChanged"></asp:TextBox>
                                       <asp:TextBox ID="txtOTTIMEEdit" CssClass="form-control input-sm clockpicker"  TabIndex="52" runat="server" Text='<%# Eval("OTHOUR") %>' Width="100%" style="display: none"></asp:TextBox>
                                             </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOuttime" runat="server" Text='<%# Eval("TIMEOUT") %>'></asp:Label>
                                             <asp:Label ID="lblOTTime" runat="server" Text='<%# Eval("OTHOUR") %>' style="display: none"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtOuttimeFooter" TabIndex="12"  runat="server" CssClass="form-control input-sm clockpicker" Width="100%" AutoPostBack="True" OnTextChanged="txtOuttimeFooter_OnTextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtOTHourFooter" TabIndex="12"  runat="server" CssClass="form-control input-sm clockpicker" Width="100%" style="display: none"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Width="15%" />
                                        <HeaderStyle Width="15%" />
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Over Time" Visible="True">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOTTIMEEdit" CssClass="form-control input-sm clockpicker"  TabIndex="52" runat="server" Text='<%# Eval("OTHOUR") %>' Width="100%" style="display: none"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOTTime" runat="server" Text='<%# Eval("OTHOUR") %>' style="display: none"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtOTHourFooter" TabIndex="12"  runat="server" CssClass="form-control input-sm clockpicker" Width="100%" style="display: none"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Width="15%" />
                                        <HeaderStyle Width="15%" />
                                    </asp:TemplateField>--%>
                                    

                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnPUpdate" runat="server" TabIndex="71" CommandName="Update" CssClass="" Height="16px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton ID="imgbtnPCancel" runat="server" TabIndex="71" CommandName="Cancel" Height="16px" ImageUrl="~/Images/Cancel.png" ToolTip="Cancel" Width="20px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceInUp.aspx", "INSERTR") == true)
                                                { %>
                                            <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="31" CommandName="Add" CssClass="" Height="16px" ImageUrl="~/Images/AddNew.png" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                            <% } %>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceInUp.aspx", "UPDATER") == true)
                                                { %>
                                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="16px" ImageUrl="~/Images/Edit.png" ToolTip="Edit" Width="20px" />
                                            <% } %>
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/AttendanceInUp.aspx", "DELETER") == true)
                                                { %>
                                            <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="16px" ImageUrl="~/Images/delete.png" OnClientClick="return confMSG()" ToolTip="Delete" Width="20px" />
                                            <% } %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle Font-Names="Calibri" Font-Size="10px" />
                                <HeaderStyle CssClass="text-center" HorizontalAlign="Center" Height="30px" Font-Size="11px" BackColor="#D9EDF7" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <RowStyle Font-Size="12px"></RowStyle>
                            </asp:GridView>
                        </div>

                    </div>
                    <!-- Content End From here -->
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
