<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="AttendanceOtEmplyeeWise.aspx.cs" Inherits="DynamicMenu.Payroll.Report.UI.AttendanceOtEmplyeeWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js"></script>
    <script>
 
        $(document).ready(function () {
            //$("#txtFromDate,#txtToDate").datepicker({ dateFormat: "dd/m/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
            BindControlEvents();
           
        });
        function BindControlEvents() {
            $(function () {
                
                $("#txtFromDate").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtToDate").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#txtToDate").datepicker({
                    defaultDate: "",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-10:+10",
                    onClose: function (selectedDate) {
                        $("#txtFromDate").datepicker("option", "maxDate", selectedDate);
                    }
                });
            });
            GetCompletionListEmployeeName();
        }
        function GetCompletionListEmployeeName() {
            $("#<%=txtEmplyee.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../../search.asmx/GetEmployeeNameWithID",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: "{ 'txt' : '" + $("#<%=txtEmplyee.ClientID %>").val() + "'}",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.label,
                                    value: item.label,
                                    empno: item.value
                                };
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 1,
                select: function (event, ui) {
                    $("#<%=txtEmpID.ClientID %>").val(ui.item.empno);
                    $("#<%=txtFromDate.ClientID %>").focus();
                    return true;
                }
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
                <h1>ATTENDANCE & OT - EMPLOYEE WISE</h1>
            </div>
            <!-- content header end -->

            <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            <!-- Content Start From here -->
            <div class="form-class">
                <div class="row"></div>

                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">Employee Name :</div>
                    <div class="col-md-4">
                       <asp:TextBox runat="server" ID="txtEmpID"  Visible="false" ></asp:TextBox>  
                       <asp:TextBox runat="server" ID="txtEmplyee" CssClass="input-sm form-control"></asp:TextBox>  
                       </div>
                    <div class="col-md-4">*Name field must be blank for all employee</div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">From Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFromDate" runat="server" onfocus="blur()" TabIndex="3" CssClass="form-control input-sm text-uppercase" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">To Date :</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtToDate" runat="server" onfocus="blur()" TabIndex="4" CssClass="form-control input-sm text-uppercase" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row form-class text-center">
                    <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </div>
                <div class="row form-class">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="5" CssClass="btn-primary form-control input-sm" OnClick="btnSubmit_Click"/>
                    </div>
                    <div class="col-md-5"></div>
                </div>

            </div>
            <!-- Content End From here -->
        </div>
    </div>
</asp:Content>
