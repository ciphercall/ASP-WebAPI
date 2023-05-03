<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="DynamicMenu.Payroll.UI.Member" %>

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
            $("#txtJoinDT,#txtCRDISUDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            $("#txtEmpDOB").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-70:+00" });
            Search_GetCompletionListGetCompletionListeEmployeeName();
            //$('.ui-autocomplete').click(function () {
            //    __doPostBack();
            //});
            $("#<%=txtEmpNM.ClientID%>").keydown(function (e) {
                if (e.which == 9 || e.which == 13)
                    window.__doPostBack();
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
       function Search_GetCompletionListGetCompletionListeEmployeeName() {
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
                <h1>Employee Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <%--<div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Member.aspx", "INSERTR") == true)
                            { %>
                        <li><a href="#"><i class="fa fa-plus"></i>Create</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Member.aspx", "UPDATER") == true)
                            { %>
                        <li><a href="#"><i class="fa fa-edit"></i>Update</a>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Member.aspx", "DELETER") == true)
                            { %>
                        <li><a href="#"><i class="fa fa-times"></i>Delete</a>
                        </li>
                        <% } %>
                    </ul>
                </div>--%>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class" style="height: 800px; max-height: 1200px">
                <div class="row"></div>
                <div class="row form-class3px">
                    <div class="col-md-2">Employee Name<span class="red-color">*</span> :&nbsp; </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpNM" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnTextChanged="txtEmpNM_TextChanged" TabIndex="1" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control input-sm" AutoPostBack="True" Enabled="False" OnTextChanged="txtEmpNM_TextChanged" TabIndex="1" Visible="False"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnEdit" runat="server" CssClass="form-control input-sm btn-primary" OnClick="btnEdit_Click" Text="Edit" />
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Guardian Name :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpGNM" runat="server" CssClass="form-control input-sm" TabIndex="2" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Mother Name :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpMNM" runat="server" CssClass="form-control input-sm" TabIndex="3" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Present Address :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpPreAddress" runat="server" TabIndex="4" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Permanent Address</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpPerAddress" runat="server" TabIndex="5" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Contact Number<span class="red-color">*</span> :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpCNO" runat="server" CssClass="form-control input-sm" TabIndex="6" MaxLength="11"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Email :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEMPEmail" runat="server" CssClass="form-control input-sm" TabIndex="7" MaxLength="50"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Date Of Birth :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpDOB" ClientIDMode="Static" runat="server" TabIndex="8" CssClass="form-control input-sm" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        Gender<span class="red-color">*</span> :
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlEmpGen" runat="server" TabIndex="9" CssClass="form-control input-sm">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>MALE</asp:ListItem>
                            <asp:ListItem>FEMALE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Voter ID No :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpVoterID" runat="server" TabIndex="10" CssClass="form-control input-sm" MaxLength="25"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Blood Group :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmpBld" runat="server" CssClass="form-control input-sm" TabIndex="11" MaxLength="5"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-12">
                        <h4><u>Reference Information 1 :</u></h4>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Name : </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef1NM" runat="server" TabIndex="12" CssClass="form-control input-sm" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Designation :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef1Desig" runat="server" TabIndex="13" CssClass="form-control input-sm" MaxLength="30"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Address :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef1Address" runat="server" TabIndex="14" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Contact No :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef1CNO" runat="server" TabIndex="15" CssClass="form-control input-sm" MaxLength="11"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-12">
                        <h4><u>Reference Information 2 :</u></h4>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Name :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef2NM" runat="server" TabIndex="16" CssClass="form-control input-sm" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Designation :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef2Desig" runat="server" TabIndex="17" CssClass="form-control input-sm" MaxLength="30"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Address :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef2Address" runat="server" TabIndex="17" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Contact No :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRef2CNO" runat="server" TabIndex="18" CssClass="form-control input-sm" MaxLength="11"></asp:TextBox>
                    </div>
                </div>
                <div class=" row form-class3px">
                    <div class="col-md-12">
                        <hr />
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Joining Date<span class="red-color">*</span> : </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtJoinDT" ClientIDMode="Static" runat="server" TabIndex="19" CssClass="form-control input-sm" MaxLength="12"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Department<span class="red-color">*</span> :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlDPT" runat="server" CssClass="form-control input-sm" TabIndex="20">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Member Type <span class="red-color">*</span> :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlEMPTP" runat="server" TabIndex="21" CssClass="form-control input-sm">
                            <asp:ListItem>--SELECT--</asp:ListItem>
                            <asp:ListItem>OFFICIALS</asp:ListItem>
                            <asp:ListItem>WORKERS</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">Member ID (Manual)<span class="red-color">*</span> :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEMPIDM" runat="server" CssClass="form-control input-sm" TabIndex="22" MaxLength="5"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Unit :</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlCostPId" CssClass="form-control input-sm" />
                       
                    </div>
                    <div class="col-md-2">Shift <span class="red-color">*</span>: </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlShift" runat="server" TabIndex="23" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2">Status <span class="red-color">*</span>: </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlSTATS" runat="server" TabIndex="24" CssClass="form-control input-sm">
                            <asp:ListItem>--SELECT--</asp:ListItem>
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>I</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">Photos :</div>
                    <div class="col-md-4">
                        <asp:FileUpload ID="FileUpload1"  onchange="readURL(this);" ClientIDMode="Static" TabIndex="25" CssClass="form-control input-sm" runat="server" accept='image/*' />
                    </div>
                </div>
                <div class="row form-class3px">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row form-class3px  text-center">
                        <asp:Label ID="lblMSG" runat="server" Font-Bold="True" ForeColor="#009933" Visible="False"></asp:Label>
                </div>

                <div class="row form-class3px">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Member.aspx", "INSERTR"))
                            { %>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="form-control input-sm btn-primary" TabIndex="26" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" Width="120px" />
                        <% } %>
                    </div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Member.aspx", "UPDATER"))
                            { %>
                        <asp:Button ID="btnUpdate" runat="server" CssClass="form-control input-sm btn-primary" TabIndex="27" OnClick="btnUpdate_Click" Text="Update" Visible="False" Width="120px" />
                        <% } %>
                    </div>
                    <div class="col-md-2">
                        <% if (UserPermissionChecker.checkParmit("/Payroll/UI/Member.aspx", "DELETER"))
                            { %>
                        <asp:Button ID="btnDelete" runat="server" CssClass="form-control input-sm btn-primary" TabIndex="25" OnClick="btnDelete_Click" OnClientClick="return confMSG()" Text="Delete" Visible="False" Width="120px" />
                        <% } %>
                    </div>
                    <div class="col-md-4">
                         <asp:Image ID="Image1" runat="server" ClientIDMode="Static" Height="100px" Width="100px" />
                        <asp:TextBox ID="txtPath" runat="server" ReadOnly="True" Visible="False" OnTextChanged="txtPath_TextChanged"></asp:TextBox>
                    </div>
                </div>


                <asp:TextBox ID="txtCRDNO" runat="server" CssClass="form-control input-sm" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtCRDISUDT" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" Visible="False"></asp:TextBox>



            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
