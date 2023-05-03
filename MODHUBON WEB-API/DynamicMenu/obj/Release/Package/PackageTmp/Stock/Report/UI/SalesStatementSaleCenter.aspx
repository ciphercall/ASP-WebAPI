<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="SalesStatementSaleCenter.aspx.cs" Inherits="DynamicMenu.Stock.Report.UI.SalesStatementSaleCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFdt,#txtToDt,#txtNUmDateFr,#txtNUmDateTo").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            Search_GetCompletionListMobileNumber();
        });

        function Search_GetCompletionListMobileNumber() {
            $("#<%=txtNumber.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../../search.asmx/GetItemNameMobileNumber",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtNumber.ClientID %>").val() + "'}",
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
                <h1>SALE STATEMENT-CATEGORY</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->
            </div>
            <!-- content header end -->


            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row text-center">Search By Store</div>
                        <div class="row form-class">
                            <div class="col-md-4">Store :</div>
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="ddlStore" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">From Date :</div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtFdt" ClientIDMode="Static" MaxLength="10" TabIndex="2" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">To Date :</div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtToDt" ClientIDMode="Static" MaxLength="10" TabIndex="3" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row text-center">
                            <strong>
                                <asp:Label runat="server" ID="lblmsg" ForeColor="red" Visible="False"></asp:Label></strong>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn-primary form-control input-sm" TabIndex="4" Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>


                    <div class="col-md-6">

                        <div class="row text-center">Search By Mobile</div>
                        <div class="row form-class">
                            <div class="col-md-4">Mobile :</div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtNumber" ClientIDMode="Static" MaxLength="11" TabIndex="11" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">From Date :</div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtNUmDateFr" ClientIDMode="Static" MaxLength="10" TabIndex="12" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4">To Date :</div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtNUmDateTo" ClientIDMode="Static" MaxLength="10" TabIndex="13" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row text-center">
                            <strong>
                                <asp:Label runat="server" ID="lblErrNumber" ForeColor="red" Visible="False"></asp:Label></strong>
                        </div>
                        <div class="row form-class">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:Button runat="server" ID="btnNumberSearch" CssClass="btn-primary form-control input-sm" TabIndex="14" Text="Submit" OnClick="btnNumberSearch_OnClick" />
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>
                </div>


            </div>
            <!-- Content End From here -->
        </div>
    </div>
    <asp:Label runat="server" ID="lblStoreID" Visible="False"></asp:Label>

</asp:Content>
