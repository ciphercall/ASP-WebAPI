<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="OrderTimeChange.aspx.cs" Inherits="DynamicMenu.Stock.UI.OrderTimeChange" %>

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
            $("#<%=txtStartTime.ClientID%>").keypress(function () {
                $(this).val("");
                $(this).val("10:00");
            });
            $("#<%=txtEndTime.ClientID%>").keypress(function() {
                $(this).val("22:00");
            });
            $('.clockpicker').clockpicker({
                placement: 'bottom',
                align: 'left',
                donetext: 'Done'
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div class="col-md-10 pull-right" id="mainContentBox">
                <div id="contentBox">
                    <div id="contentHeaderBox">
                        <h1>Change Order Time Period</h1>

                    </div>
                    <!-- content header end -->

                    <!-- Content Write From Here-->
                    <div class="form-class">
                        <div class="row"></div>
                        <div class="row form-class text-center">
                            <asp:Label runat="server" ID="lblErr" Visible="False" Font-Bold="True" ForeColor="red"></asp:Label>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">

                                <div class="row form-class">
                                    <div class="col-md-2 text-left"><strong>Start Time</strong></div>
                                    <div class="col-md-4">
                                        <div class="input-group input-group-sm clockpicker">
                                            <asp:TextBox ID="txtStartTime" class="form-control" value="10:00" MaxLength="5" aria-describedby="sizing-addon3" runat="server"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon3">
                                                <samp class="glyphicon glyphicon-time"></samp>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2 text-left"><strong>End Time</strong></div>
                                    <div class="col-md-4">
                                        <div class="input-group input-group-sm clockpicker">
                                            <asp:TextBox ID="txtEndTime" class="form-control" value="22:00" MaxLength="5" aria-describedby="sizing-addon4" runat="server"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon4">
                                                <samp class="glyphicon glyphicon-time"></samp>
                                            </span>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-4">
                                        <% if (UserPermissionChecker.checkParmit("/Stock/UI/OrderTimeChange.aspx", "UPDATER"))
                                            { %>
                                        <asp:Button runat="server" ID="btnUpdate" CssClass="form-control input-sm btn-primary" Text="Update" OnClick="btnUpdate_OnClick" />
                                        <% } %>
                                    </div>
                                    <div class="col-md-4"></div>
                                </div>

                            </div>
                            <div class="col-md-2"></div>
                        </div>

                    </div>
                    <!-- Content Write From Here-->
                </div>
                <!-- content box end here -->
            </div>
            <!-- main content end here -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
