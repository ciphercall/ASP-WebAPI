<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="ItemRegister.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.ItemRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFrom,#txtTo").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>ITEM REGISTER</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Store :</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlStore" runat="server"
                            OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                            AutoPostBack="True" TabIndex="1" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Barcode</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtItemNM" runat="server" MaxLength="9" CssClass="form-control input-sm" AutoPostBack="True"
                            OnTextChanged="txtItemNM_TextChanged" TabIndex="2"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFrom" runat="server" AutoPostBack="True" CssClass="form-control input-sm"
                            ClientIDMode="Static" OnTextChanged="txtFrom_TextChanged" TabIndex="3"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTo" runat="server" AutoPostBack="True" CssClass="form-control input-sm"
                            ClientIDMode="Static" OnTextChanged="txtTo_TextChanged" TabIndex="4"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row text-center">
                    <strong>
                        <asp:Label ID="lblGridMsg" runat="server" Visible="False" ForeColor="#CC0000"
                            Style="font-weight: 700"></asp:Label></strong>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-control input-sm btn-primary"
                           OnClick="btnSearch_Click" TabIndex="5" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
            </div>
            <!-- Content End From here -->
        </div>
    </div>
    <asp:Label ID="lblItemCD" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblITCD" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblStoreID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblItemNm" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblItemID" runat="server" Visible="False"></asp:Label>
</asp:Content>
