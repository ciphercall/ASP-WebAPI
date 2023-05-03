<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="UserCreate.aspx.cs" Inherits="DynamicMenu.ASLCompany.UI.UserCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="../../MenuCssJs/ui-lightness/jquery.ui.theme.css" rel="stylesheet" />
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <link href="../../MenuCssJs/bootstrap-clock-picker/src/clockpicker.css" rel="stylesheet" />

    <link href="../../MenuCssJs/bootstrap-clock-picker/src/standalone.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/bootstrap-clock-picker/src/clockpicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            BindControlEvents();
        });

        function BindControlEvents() {
            Search_CompanyName();
            $("#<%=lblMsg.ClientID%>").fadeOut(10000);
            $('.ui-autocomplete').click(function () {
                __doPostBack();
            });
            $('.ui-autocomplete').select(function () {
                __doPostBack();
            });
            $('.clockpicker').clockpicker({
                placement: 'top',
                align: 'left',
                donetext: 'Done'
            });
        }

        function Search_CompanyName() {
            $("#<%=txtCompanyName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../search.asmx/GetCompletionListCompany",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtCompanyName.ClientID %>").val() + "'}",
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
     <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Create User</h1>

                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <li>
                        <asp:LinkButton runat="server" ID="LinkBAdd" CssClass="fa fa-plus" Text="Add record" OnClick="LinkBAdd_Click"/>
                        </li>
                        <li><%--<a href="#"><i class="fa fa-edit"></i>Edit record</a>--%>
                            <asp:LinkButton runat="server" ID="linkBEdit" CssClass="fa fa-edit" Text="Edit record" OnClick="linkBEdit_Click"/>
                        </li>
                    </ul>
                </div>
                <!-- end logout option -->

            </div>
            <!-- content header end -->

            <!-- Content Write From Here-->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"><strong>Comapny Name</strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtCompanyName" AutoPostBack="True" MaxLength="100" CssClass="form-control auto-complete" ClientIDMode="Static" OnTextChanged="txtCompanyName_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Branch</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"/>
                    </div>
                </div>
                <div class="text-center">
                    <strong>
                        <asp:Label runat="server" ForeColor="red" ID="lblMsg" Visible="False"></asp:Label></strong>
                </div>


                <div class="row form-class">
                    <div class="col-md-2 text-left"><strong>User Name<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtUserName" MaxLength="100" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:DropDownList runat="server" Visible="False" ID="ddlUserName" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"/>
                    </div>
                    <div class="col-md-2 text-left"><strong>Department Name<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtDepartmentName" MaxLength="50" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2 text-left"><strong>Operation Type<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">

                        <asp:DropDownList runat="server" ID="ddlOpType" CssClass="form-control input-sm">
                            <asp:ListItem Value="COMPADMIN">Company Admin</asp:ListItem>
                            <asp:ListItem Value="USERADMIN">User Admin</asp:ListItem>
                            <asp:ListItem Value="USER">User</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-2 text-left"><strong>User Address<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtUserAdd" MaxLength="100" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2 text-left"><strong>Mobile No.<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtMobileNo" MaxLength="11" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left"><strong>Email Address<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtEmailId" MaxLength="50" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2 text-left"><strong>Login By<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlLogInBy" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlLogInBy_SelectedIndexChanged">
                            <asp:ListItem Value="">--Select Login By--</asp:ListItem>
                            <asp:ListItem Value="EMAIL">Email</asp:ListItem>
                            <asp:ListItem Value="MOBNO">Mobile</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 text-left"><strong>Login ID<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtlogInId" MaxLength="50" ReadOnly="True" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2 text-left"><strong>Password<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtPassword" MaxLength="20" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left"><strong>Status<span class="red-color">*</span></strong></div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-sm">
                            <asp:ListItem Value="">--Select Status--</asp:ListItem>
                            <asp:ListItem Value="A">Active</asp:ListItem>
                            <asp:ListItem Value="I">Inactive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2 text-left"><strong>Start Time</strong></div>
                    <div class="col-md-4">
                        <div class="input-group input-group-sm clockpicker">
                            <asp:TextBox ID="txtStartTime" class="form-control" value="10:00" MaxLength="10" aria-describedby="sizing-addon3" runat="server"></asp:TextBox>
                            <span class="input-group-addon" id="sizing-addon3">
                                <samp class="glyphicon glyphicon-time"></samp>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2 text-left"><strong>End Time</strong></div>
                    <div class="col-md-4">
                        <div class="input-group input-group-sm clockpicker">
                            <asp:TextBox ID="txtEndTime" class="form-control" value="18:00" MaxLength="10" aria-describedby="sizing-addon4" runat="server"></asp:TextBox>
                            <span class="input-group-addon" id="sizing-addon4">
                                <samp class="glyphicon glyphicon-time"></samp>
                            </span>
                        </div>
                    </div>

                   
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="form-control input-sm btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button runat="server" ID="btnUpdate" Visible="False" CssClass="form-control input-sm btn-primary" Text="Update" OnClick="btnUpdate_Click" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
            </div>
            <!-- Content Write From Here-->
        </div>
        <!-- content box end here -->
    </div>
    <!-- main content end here -->
    <asp:Label runat="server" ID="lblCompanyId" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblBranchCd" Visible="False"></asp:Label>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
