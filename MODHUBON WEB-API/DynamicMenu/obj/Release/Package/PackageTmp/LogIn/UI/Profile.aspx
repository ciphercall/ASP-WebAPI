<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="DynamicMenu.LogIn.UI.Profile" %>
<%@ Import Namespace="AlchemyAccounting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
     <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>Profile</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->

            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                
                <div class="row form-class">
                    <div class="col-md-3">
                        <div class="profile">
                            <p>
                            
                                <%
                                    string profilelink = dbFunctions.FbProfilePicture(Session["USERID"].ToString());
                                %>
                                <img src="<%=profilelink %>"/>
                            </p>
                            <h2><asp:Label runat="server" ID="lblUserName"></asp:Label></h2>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="row form-class">
                            <div class="col-md-4">
                                Company Name
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblCompanyName"></asp:Label>
                                </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">
                                Branch
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblBranch"></asp:Label>
                                </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">
                                Department
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblDepartment"></asp:Label>
                                </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">
                                Address
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblAddress"></asp:Label>
                                </div>
                        </div>
                        
                        <div class="row form-class">
                            <div class="col-md-4">
                                Mobile No
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblMobile"></asp:Label>
                                </div>
                        </div>
                         <div class="row form-class">
                            <div class="col-md-4">
                                Email
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                </div>
                        </div>
                        
                        <div class="row form-class">
                            <div class="col-md-4">
                                Login Time
                                </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblTimeFrom"></asp:Label> to <asp:Label runat="server" ID="lbltimeTo"></asp:Label>
                                </div>
                        </div>
                    </div>
                    
                    <div class="col-md-1">
                        </div>
                </div>
                
            </div>
            <!-- Content End From here -->
        </div>
    </div>
    
    <%--<script type="text/javascript">
        function getProfilePicture($fbUserName)
        {
            $url='http://graph.facebook.com/'.$fbUserName.'/picture?type=large';
            return $url;
        }

    </script>--%>
</asp:Content>
