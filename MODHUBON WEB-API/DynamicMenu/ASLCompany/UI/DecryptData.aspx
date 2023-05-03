<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="DecryptData.aspx.cs" Inherits="DynamicMenu.ASLCompany.UI.DecryptData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Create Company </h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <li><a href="#"><i class="fa fa-plus"></i>Add record</a>
                        </li>
                        <li><a href="#"><i class="fa fa-edit"></i>Edit record</a>
                        </li>
                        <li><a href="#"><i class="fa fa-times"></i>Delete record</a>
                        </li>

                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <asp:UpdatePanel runat="server" ID="upd1">
                <ContentTemplate>


                    <div class="form-class">
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">Encrypt Data</div>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtText" TabIndex="1" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnEncrypt" Text="Encrypt" TabIndex="2" CssClass="form-control input-sm btn-primary" OnClick="btnEncrypt_Click" />

                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnSubmit" Text="Decrypt" TabIndex="3" CssClass="form-control input-sm btn-primary" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">Decrypt Text</div>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" TextMode="MultiLine" Height="100px" ID="txtPlainText" ReadOnly="True" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Content Write From Here-->

    </div>
    <!-- main content end here -->

</asp:Content>
