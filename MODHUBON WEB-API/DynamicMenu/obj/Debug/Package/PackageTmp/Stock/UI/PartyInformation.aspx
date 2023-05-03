<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="PartyInformation.aspx.cs" Inherits="DynamicMenu.Stock.UI.PartyInformation" %>

<%@ Import Namespace="DynamicMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/jquery-ui.js"></script>
    <script src="../../MenuCssJs/js/avro.min.js" type="text/javascript" charset="utf-8"></script>
    <script>
        $(document).ready(function () {
            //BindControlEvents();
        });

        $(function () {
            $('[id*=txtPartyNameBan],[id*=txtAddressBan]').avro();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Party Information Entry</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <% if (UserPermissionChecker.checkParmit("/Stock/UI/PartyInformation.aspx", "INSERTR"))
                            { %>
                        <li>
                            <asp:LinkButton runat="server" ID="LinkBAdd" CssClass="fa fa-plus" Text="Add record" OnClick="LinkBAdd_OnClick"/>
                        </li>
                        <% } %>
                        <% if (UserPermissionChecker.checkParmit("/Stock/UI/PartyInformation.aspx", "UPDATER"))
                            { %>
                        <li>
                            <asp:LinkButton runat="server" ID="updateRecord" CssClass="fa fa-plus" Text="Update record" OnClick="updateRecord_OnClick"/>
                        </li>
                        <% } %>
                    </ul>
                </div>
                <!-- end logout option -->
            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                 <div class="row form-class text-center">
                     <asp:Label runat="server" ID="lblMsg" Visible="False" CssClass="red-color"></asp:Label>
                 </div>
                <div class="row form-class">
                    <div class="col-md-4"></div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlPartyList" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddlPartyList_OnSelectedIndexChanged" CssClass="form-control input-sm" Visible="False"/>
                    </div>
                    <div class="col-md-4"></div>
                 </div>

                 <div class="row form-class">
                     <div class="col-md-2">Name (English)<span class="red-color">*</span></div>
                     <div class="col-md-4">
                         <asp:TextBox runat="server" ID="txtPartyNameEng"  required="true" MaxLength="100"  CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                     <div class="col-md-2">Name (Bengali)</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" ID="txtPartyNameBan" MaxLength="200"  CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                 </div>
                <div class="row form-class">
                     <div class="col-md-2">Address (English)<span class="red-color">*</span></div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" ID="txtAddressEng" MaxLength="100" CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                     <div class="col-md-2">Address (Bengali)</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" ID="txtAddressBan" MaxLength="290" CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                 </div>
                <div class="row form-class">
                     <div class="col-md-2">Mobile 1<span class="red-color">*</span></div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" title="Input a Valid Mobile Number." required="true" pattern="[016|017|018|019|015|011]+[0-9]{10}$" ID="txtMobile1" MaxLength="11" CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                     <div class="col-md-2">Mobile 2</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server"  title="Input a Valid Mobile Number." pattern="[016|017|018|019|015|011]+[0-9]{10}$" ID="txtMobile2" MaxLength="11" CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                 </div>
                <div class="row form-class">
                     <div class="col-md-2">Email</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server"  title="Input a Valid Email address." ID="txtEmail" type="email"  MaxLength="100"  CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                     <div class="col-md-2">Remarks</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" ID="txtRemarks"  MaxLength="100"  CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                 </div>
                <div class="row form-class">
                     <div class="col-md-2">Autorized Person</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" ID="txtAuthorName" MaxLength="50"  CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                     <div class="col-md-2">Contact No (AP)</div>
                     <div class="col-md-4">
                          <asp:TextBox runat="server" ID="txtAuthorContactNo" title="Input a Valid Mobile Number." pattern="[016|017|018|019|015|011]+[0-9]{10}$" MaxLength="11" CssClass="form-control input-sm"></asp:TextBox>
                     </div>
                 </div>
                <div class="row form-class">
                     <div class="col-md-2">Status<span class="red-color">*</span></div>
                     <div class="col-md-4">
                          <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-sm">
                              <asp:ListItem Value="A">Active</asp:ListItem>
                              <asp:ListItem Value="I">Inactive</asp:ListItem>
                          </asp:DropDownList>
                     </div>
                     <div class="col-md-2"></div>
                     <div class="col-md-2">
                         <asp:Button runat="server" ID="btnSave" CssClass="form-control input-sm btn-primary" Text="Save" OnClick="btnSave_OnClick"/>
                         <asp:Button runat="server" ID="btnUpdate" Visible="False" CssClass="form-control input-sm btn-warning" Text="Update" OnClick="btnUpdate_OnClick"/>
                     </div>
                     <div class="col-md-2"></div>
                 </div>
            </div>
            

        </div>
    </div>
</asp:Content>
