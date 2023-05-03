<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="EmployeeSalaryAddDeduct.aspx.cs" Inherits="DynamicMenu.Payroll.UI.EmployeeSalaryAddDeduct" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../MenuCssJs/bootstrap/bootstrap-datepicker.js"></script>
    <link href="../../MenuCssJs/bootstrap/datepicker.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            BindControlEvents();
        });

        function BindControlEvents() {
            $("#txtMnYear").datepicker({
                format: "M-yy",
                startView: "months",
                minViewMode: "months",
                autoclose:"true"
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
                        <h1>Addition / Deduction - Monthly </h1>
                        <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                        <!-- logout option button -->
                        <div class="btn-group pull-right" id="editOption">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                            </button>
                            <ul class="dropdown-menu pull-right" style="" role="menu">
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalaryAddDeduct.aspx", "INSERTR") == true)
                                    { %>
                                <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalaryAddDeduct.aspx", "UPDATER") == true)
                                    { %>
                                <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                                </li>
                                <% } %>
                                <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalaryAddDeduct.aspx", "DELETER") == true)
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
                            <div class="col-md-2">Month/Year :</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtMnYear" CssClass="form-control input-sm text-uppercase" MaxLength="6" 
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
                                            <asp:Label ID="lblCompanyIdEdit" runat="server"  Text='<%# Eval("COMPID") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblCompanyIdFooter" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblTransMyEdit" runat="server"  Text='<%# Eval("TRANSMY") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransMy" runat="server" Text='<%# Eval("TRANSMY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTransMyFooter" runat="server"></asp:Label>
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
                                            <asp:DropDownList ID="ddlEmployeeNameFooter" TabIndex="10"  runat="server" CssClass="form-control input-sm" Width="100%"></asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemStyle Width="20%" />
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>

                                   
                                     <asp:TemplateField HeaderText="Festible Bonus">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBONUSFEdit" runat="server" TabIndex="53" CssClass="form-control input-sm" Text='<%# Eval("BONUSF") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBONUSF" runat="server" Text='<%# Eval("BONUSF") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBONUSFFooter" TabIndex="13" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"  BackColor="#9ACD32"/>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Incentive">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtINCENTIVEEdit" runat="server" TabIndex="54" CssClass="form-control input-sm" Text='<%# Eval("INCENTIVE") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblINCENTIVE" runat="server" Text='<%# Eval("INCENTIVE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtINCENTIVEFooter" TabIndex="14" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"  BackColor="#9ACD32"/>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Conveyance" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCONVEYEdit" runat="server" TabIndex="56" CssClass="form-control input-sm" Text='<%# Eval("CONVEY") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONVEY" runat="server" Text='<%# Eval("CONVEY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCONVEYFooter" TabIndex="16" runat="server" CssClass="form-control input-sm"  Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" BackColor="#9ACD32"/>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Mobile" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMOBILEEdit" runat="server" TabIndex="57" CssClass="form-control input-sm" Text='<%# Eval("MOBILE") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMOBILE" runat="server" Text='<%# Eval("MOBILE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMOBILEFooter" TabIndex="17" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"  BackColor="#9ACD32"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Salary" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDUEADJEdit" runat="server" TabIndex="58" CssClass="form-control input-sm" Text='<%# Eval("DUEADJ") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDUEADJ" runat="server" Text='<%# Eval("DUEADJ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDUEADJFooter" TabIndex="18" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"  BackColor="#9ACD32"/>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Attendance Bonus">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBONUSPEdit" runat="server" TabIndex="58" CssClass="form-control input-sm" Text='<%# Eval("BONUSP") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBONUSP" runat="server" Text='<%# Eval("BONUSP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBONUSPFooter" TabIndex="18" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"  BackColor="#9ACD32"/>
                                    </asp:TemplateField> 
                                    
                                    <asp:TemplateField HeaderText="Fooding Addition">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFOODINGADDEdit" runat="server" TabIndex="58" CssClass="form-control input-sm" Text='<%# Eval("FOODINGADD") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFOODINGADD" runat="server" Text='<%# Eval("FOODINGADD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFOODINGADDFooter" TabIndex="18" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"  BackColor="#9ACD32"/>
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderText="Advance">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtADVANCEEdit" runat="server" TabIndex="60" CssClass="form-control input-sm" Text='<%# Eval("ADVANCE") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblADVANCE" runat="server" Text='<%# Eval("ADVANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtADVANCEFooter" TabIndex="19" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"   BackColor="#FFA07A"/>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Fooding">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFOODINGEdit" runat="server" TabIndex="63" CssClass="form-control input-sm" Text='<%# Eval("FOODING") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFOODING" runat="server" Text='<%# Eval("FOODING") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFOODINGFooter" TabIndex="20" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"   BackColor="#FFA07A"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fine">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFINEEdit" runat="server" TabIndex="64" CssClass="form-control input-sm" Text='<%# Eval("FINEADJ") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFINE" runat="server" Text='<%# Eval("FINEADJ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFINEFooter" TabIndex="21" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"   BackColor="#FFA07A"/>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Income Tax">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtITAXEdit" runat="server" TabIndex="64" CssClass="form-control input-sm" Text='<%# Eval("ITAX") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblITAX" runat="server" Text='<%# Eval("ITAX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtITAXFooter" TabIndex="21" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"   BackColor="#FFA07A"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Day Leave">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDayLeaveEdit" runat="server" TabIndex="65" CssClass="form-control input-sm" Text='<%# Eval("DAYLEAVE") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDayLeave" runat="server" Text='<%# Eval("DAYLEAVE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDayLeaveFooter" TabIndex="22" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"   BackColor="#86B404"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OT Hour">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOTHOUREdit" runat="server" TabIndex="60" CssClass="form-control input-sm" Text='<%# Eval("OTHOUR") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOTHOUR" runat="server" Text='<%# Eval("OTHOUR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtOTHOURFooter" TabIndex="23" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"   BackColor="#86B404"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRemarksEdit" runat="server" TabIndex="66" CssClass="form-control input-sm" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRemarksFooter" TabIndex="24" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"/>
                                    </asp:TemplateField>
                                    
                                    
                                    
                                    
                                    
                                    

                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnPUpdate" runat="server" TabIndex="71" CommandName="Update" CssClass="" Height="16px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton ID="imgbtnPCancel" runat="server" TabIndex="71" CommandName="Cancel" Height="16px" ImageUrl="~/Images/Cancel.png" ToolTip="Cancel" Width="20px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalaryAddDeduct.aspx", "INSERTR") == true)
                                                { %>
                                            <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="31" CommandName="Add" CssClass="" Height="16px" ImageUrl="~/Images/AddNew.png" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                            <% } %>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalaryAddDeduct.aspx", "UPDATER") == true)
                                                { %>
                                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="16px" ImageUrl="~/Images/Edit.png" ToolTip="Edit" Width="20px" />
                                            <% } %>
                                            <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeSalaryAddDeduct.aspx", "DELETER") == true)
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
                                <RowStyle Font-Size="10px"></RowStyle>
                            </asp:GridView>
                        </div>

                    </div>
                    <!-- Content End From here -->
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
