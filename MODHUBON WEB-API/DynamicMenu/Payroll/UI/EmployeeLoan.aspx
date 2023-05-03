<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="EmployeeLoan.aspx.cs" Inherits="DynamicMenu.Payroll.UI.EmployeeLoan" %>
<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            BindControlEvents();
        });
        function BindControlEvents() {
            $("#txtFdt,#txtTdt,#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
           
            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
           
        }
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#Image1')
                        .attr('src', e.target.result)
                        .width(100)
                        .height(100);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) { }
            else { return false; }
        }
        <%--function Search_GetCompletionListGetCompletionListeEmployeeName() {
            $("#<%=txtEmpNM.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListeEmployeeName",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtEmpNM.ClientID%>").val() + "'}",
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
        }--%>
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
                <h1>Employee Loan Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeLoan.aspx", "INSERTR") == true)
                            { %>
                        <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeLoan.aspx", "UPDATER") == true)
                            { %>
                        <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeLoan.aspx", "DELETER") == true)
                            { %>
                        <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                        </li>
                        <% } %>
                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSTID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class" style="height: 800px; max-height: 1200px">
                <div class="row"></div>
                <div class="row form-class3px">
                    <%--<div class="col-md-2">Date : <span class="red-color">*</span> :&nbsp; </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" CssClass="form-control input-sm" TabIndex="1" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control input-sm" AutoPostBack="True" Enabled="False" OnTextChanged="txtEmpNM_TextChanged" TabIndex="1" Visible="False"></asp:TextBox>
                    </div>--%>
                    <div class="col-md-10"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnEdit" runat="server" CssClass="form-control input-sm btn-primary" OnClick="btnEdit_Click" Text="Edit" />
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDate" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" TabIndex="2" MaxLength="100" AutoPostBack="True" OnTextChanged="txtDate_OnTextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Month/Year</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtMonthYear" runat="server" CssClass="form-control input-sm" ReadOnly="True" TabIndex="3" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Serial :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTransNO" runat="server" TabIndex="4" Visible="True" CssClass="form-control input-sm" MaxLength="100" oNfocus="blur()"></asp:TextBox>
                        <asp:DropDownList ID="ddlTransNo" runat="server" Visible="False" TabIndex="4" CssClass="form-control input-sm" MaxLength="100" AutoPostBack="True" OnTextChanged="ddlTransNo_OnTextChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">Employee Name</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlEmployeeNM" runat="server" TabIndex="5" Visible="True" CssClass="form-control input-sm" MaxLength="100"></asp:DropDownList>
                        
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Loan Amount: <span class="red-color">*</span> :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtLoanAM" runat="server" Text="0" CssClass="form-control input-sm" TabIndex="6" MaxLength="11"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Deduction Amount: </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDedAM" runat="server" Text="0" CssClass="form-control input-sm" TabIndex="7" MaxLength="50"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">From Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFdt" ClientIDMode="Static" runat="server" TabIndex="8" CssClass="form-control input-sm" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        To Date:<%--<span class="red-color">*</span>--%>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTdt" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" TabIndex="9" MaxLength="50"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Remarks :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRemarks" runat="server" TabIndex="10" CssClass="form-control input-sm" MaxLength="25"></asp:TextBox>
                    </div>
                    
                </div>
               
                
                
                <div class="row form-class3px  text-center">
                        <asp:Label ID="lblMSG" runat="server" Font-Bold="True" ForeColor="#009933" Visible="False"></asp:Label>
                </div>

                <div class="row form-class3px">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeLoan.aspx", "INSERTR"))
                            { %>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="form-control input-sm btn-primary" TabIndex="26" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" Width="120px" />
                        <% } %>
                    </div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeLoan.aspx", "UPDATER"))
                            { %>
                        <asp:Button ID="btnUpdate" runat="server" CssClass="form-control input-sm btn-primary" TabIndex="27" OnClick="btnUpdate_Click" Text="Update" Visible="False" Width="120px" />
                        <% } %>
                    </div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/EmployeeLoan.aspx", "DELETER"))
                            { %>
                        <asp:Button ID="btnDelete" runat="server" CssClass="form-control input-sm btn-primary" TabIndex="25" OnClick="btnDelete_Click" OnClientClick="return confMSG()" Text="Delete" Visible="False" Width="120px" />
                        <% } %>
                    </div>
                    
                </div>


                <asp:TextBox ID="txtCRDNO" runat="server" CssClass="form-control input-sm" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtCRDISUDT" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" Visible="False"></asp:TextBox>



            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
