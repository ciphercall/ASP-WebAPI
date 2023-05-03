<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-sales-statement-sc.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rpt_sales_statement_sc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sale Statement Sales Center</title>
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js" type="text/javascript"></script>
    <link href="../../../MenuCssJs/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
        }

        .auto-style3 {
            width: 26px;
        }

        .auto-style4 {
            width: 1033px;
        }

        .auto-style1 {
            width: 100px;
        }

        .SubTotalRowStyle {
            border: solid 1px Black;
            font-weight: bold;
            text-align: center;
        }

        .SubTotalRowStyleAmount {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
        }

        .GrandTotalRowStyle {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 18px;
            text-align: right;
            height: 35px;
        }

        .GroupHeaderStyle {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            font-weight: bold;
            height: 30px;
        }

        .GridRowStyle {
            padding-left: 10%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="width: 96%; margin: 0% 2% 0% 2%;">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblCompNM" runat="server"
                                Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                        </td>
                        <td style="width: 45%; text-align: right">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                    </tr>
                    <tr>
                        <td colspan="2"><strong>SALES STATEMENT STORE TRANSECTION</strong>
                        </td>
                        <td style="width: 7%;"><strong>Store Name:</strong></td>
                        <td style="width: 20%">
                            <asp:Label runat="server" ID="lblStoreName"></asp:Label></td>
                        <td style="width: 45%"></td>
                    </tr>

                    <tr>
                        <td style="width: 8%"><strong>From Date :</strong></td>
                        <td style="width: 20%">
                            <asp:Label runat="server" ID="lblFromDate"></asp:Label></td>
                        <td style="width: 7%;"><strong>To Date :</strong></td>
                        <td style="width: 20%">
                            <asp:Label runat="server" ID="lblToDate"></asp:Label></td>
                        <td style="width: 45%; text-align: right">
                            <asp:Label ID="lblTime" runat="server"
                                Style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label></td>
                    </tr>

                </table>
                <%-- <asp:Label ID="lblAddress" runat="server" Visible="False"
                Style="font-family: Calibri; font-size: 9px"></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>--%>
            </div>



            <div style="width: 96%; margin: 0% 2% 0% 2%;">
                <u>
                    <h3>Sale &nbsp;&nbsp;&nbsp; :</h3>
                </u>
                <div>
                    <table style="width: 100%; font-size: 14px; font-weight: 500; background: #ddd">
                        <tr>
                            <td style="width: 30%; text-align: center">Item Name</td>
                            <td style="width: 5%; text-align: center">OTY</td>
                            <td style="width: 5%; text-align: center">RATE</td>
                            <td style="width: 7%; text-align: center">Amount</td>
                            <td style="width: 7%; text-align: center">Total Amount</td>
                            <td style="width: 5%; text-align: center">Discount</td>
                            <td style="width: 7%; text-align: center">Net Amount</td>
                            <td style="width: 7%; text-align: center">Due Amount</td>
                            <td style="width: 10%; text-align: center">Collected Amount</td>

                        </tr>
                    </table>
                </div>
                <div class="nav navbar-fixed-top">
                    <table style="width: 100%; background: #ddd" id="gridhead">
                        <tr>
                            <td style="width: 30%; text-align: center">Item Name</td>
                            <td style="width: 5%; text-align: center">OTY</td>
                            <td style="width: 5%; text-align: center">RATE</td>
                            <td style="width: 7%; text-align: center">Amount</td>
                            <td style="width: 7%; text-align: center">Total Amount</td>
                            <td style="width: 5%; text-align: center">Discount</td>
                            <td style="width: 7%; text-align: center">Net Amount</td>
                            <td style="width: 7%; text-align: center">Due Amount</td>
                            <td style="width: 10%; text-align: center">Collected Amount</td>
                        </tr>
                    </table>
                </div>
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <div style="border: double; border-width: 1px; border-color: black;">
                            <p style="padding-left: 20px;">
                                <asp:Label runat="server" ID="lblText" Text="Date :" Style="font-weight: bold"></asp:Label>
                                <asp:Label runat="server" ID="lbldate" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>
                            </p>
                        </div>
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <ItemTemplate>
                                <div style="border: double; border-width: 1px; border-color: black;">
                                    <span style="padding-left: 20px;">Invoice No : &nbsp; 
                                        <asp:Label runat="server" Visible="True" ID="lbldate1" Text='<%#Eval("TRANSNO") %>' Style="font-weight: bold"></asp:Label> 
                                        &nbsp;&nbsp;&nbsp;&nbsp;Time: 
                                        <asp:Label runat="server" Visible="True" ID="lblTime" Text='<%#Eval("INTIME") %>' Style="font-weight: bold"></asp:Label>
                                    </span>
                                    <asp:Label runat="server" Visible="false" ID="lblTransDT" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>
                                </div>
                                <asp:GridView ID="gv_Trans" runat="server" Width="100%" ShowHeader="False" ShowFooter="true" AutoGenerateColumns="False" Font-Size="11pt" Style="margin-bottom: 0px" OnRowDataBound="gv_Trans_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Item Name">
                                            <HeaderStyle Width="30%" />
                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OTY">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="RATE">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Amount">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Total Amount">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Discount">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Net Amount">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Due Amount">
                                            <HeaderStyle Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Collected Amount">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:BoundField>


                                    </Columns>
                                    <HeaderStyle Font-Size="11pt" />
                                </asp:GridView>

                            </ItemTemplate>
                        </asp:Repeater>
                        <table style="width: 100%; border: 2px solid #000000">
                            <tr>
                                <td style="width: 30%; text-align: right; border-right: 2px solid #000000"><strong>Sub Grand Total :</strong></td>
                                <td style="width: 5%; text-align: center"><strong>
                                    <asp:Label runat="server" ID="lblSubTotQty"></asp:Label></strong></td>
                                <td style="width: 5%"></td>
                                <td style="width: 7%; border-right: 2px solid #000000"></td>
                                <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblSubTotAmount"></asp:Label></strong></td>

                                <td style="width: 5%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblSubDiscount"></asp:Label></strong></td>
                                <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblSubNet"></asp:Label></strong></td>

                                <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblSubDue"></asp:Label></strong></td>
                                <td style="width: 10%; text-align: right"><strong>
                                    <asp:Label runat="server" ID="lblSubRec"></asp:Label></strong></td>
                            </tr>
                        </table>
                    </ItemTemplate>

                </asp:Repeater>



                <div style="padding-top: 20px">
                    <table style="width: 100%; border: 2px solid #000000">
                        <tr>

                            <td style="width: 30%; text-align: right; border-right: 2px solid #000000"><strong>Sale Grand Total :</strong></td>
                            <td style="width: 5%; text-align: center"><strong>
                                <asp:Label runat="server" ID="lblTotQty"></asp:Label></strong></td>
                            <td style="width: 5%"></td>
                            <td style="width: 7%; border-right: 2px solid #000000"></td>
                            <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                <asp:Label runat="server" ID="lblTotAmount"></asp:Label></strong></td>

                            <td style="width: 5%; text-align: right; border-right: 2px solid #000000"><strong>
                                <asp:Label runat="server" ID="lblDiscount"></asp:Label></strong></td>
                            <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                <asp:Label runat="server" ID="lblNet"></asp:Label></strong></td>

                            <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                <asp:Label runat="server" ID="lblDue"></asp:Label></strong></td>
                            <td style="width: 10%; text-align: right"><strong>
                                <asp:Label runat="server" ID="lblRec"></asp:Label></strong></td>

                        </tr>
                    </table>
                </div>
            </div>




            <%--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--
            Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--
            Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--
            Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange--Exchange----%>





            <div style="padding-top: 30px; padding-bottom: 10px">



                <div style="width: 96%; margin: 0% 2% 0% 2%;">
                    <u>
                        <h3>Return &nbsp;&nbsp;&nbsp; :</h3>
                    </u>

                    <div>
                        <table style="width: 100%; font-size: 14px; font-weight: 500; background: #ddd">
                            <tr>
                                <td style="width: 30%; text-align: center">Item Name</td>
                                <td style="width: 5%; text-align: center">OTY</td>
                                <td style="width: 5%; text-align: center">RATE</td>
                                <td style="width: 7%; text-align: center">Amount</td>
                                <td style="width: 7%; text-align: center">Total Amount</td>
                                <td style="width: 5%; text-align: center">Discount</td>
                                <td style="width: 7%; text-align: center">Deduction Amount</td>
                                <td style="width: 7%; text-align: center">Due Amount</td>
                                <td style="width: 10%; text-align: center">Refund Amount</td>

                            </tr>
                        </table>
                    </div>


                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                        <ItemTemplate>
                            <div style="border: double; border-width: 1px; border-color: black;">
                                <p style="padding-left: 20px;">
                                    <asp:Label runat="server" ID="lblText" Text="Date :" Style="font-weight: bold"></asp:Label>
                                    <asp:Label runat="server" ID="lbldate" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>
                                </p>
                            </div>
                            <asp:Repeater ID="Repeater4" runat="server" OnItemDataBound="Repeater4_ItemDataBound">
                                <ItemTemplate>
                                    <div style="border: double; border-width: 1px; border-color: black;">
                                        <span style="padding-left: 20px;">Invoice No : &nbsp; 
                                        <asp:Label runat="server" Visible="True" ID="lbldate1" Text='<%#Eval("TRANSNO") %>' Style="font-weight: bold"></asp:Label></span>
                                        <asp:Label runat="server" Visible="false" ID="lblTransDT" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>
                                    </div>
                                    <asp:GridView ID="gv_TransEx" runat="server" Width="100%" ShowHeader="False" ShowFooter="true" AutoGenerateColumns="False"
                                       Font-Bold="True" Font-Size="20px" Style="margin-bottom: 0px" OnRowDataBound="gv_Trans_RowDataBoundEx">
                                        <Columns>
                                            <asp:BoundField HeaderText="Item Name">
                                                <HeaderStyle Width="30%" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="OTY">
                                                <HeaderStyle Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="RATE">
                                                <HeaderStyle Width="5%" />
                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Total Amount">
                                                <HeaderStyle Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Discount">
                                                <HeaderStyle Width="5%" />
                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Net Amount">
                                                <HeaderStyle Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Due Amount">
                                                <HeaderStyle Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Collected Amount">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:BoundField>


                                        </Columns>
                                        <HeaderStyle Font-Size="11pt" />
                                    </asp:GridView>

                                </ItemTemplate>
                            </asp:Repeater>
                            <table style="width: 100%; border: 2px solid #000000">
                                <tr>
                                    <td style="width: 30%; text-align: right; border-right: 2px solid #000000"><strong>Sub Grand Total :</strong></td>
                                    <td style="width: 5%; text-align: center"><strong>
                                        <asp:Label runat="server" ID="lblSubTotQty"></asp:Label></strong></td>
                                    <td style="width: 5%"></td>
                                    <td style="width: 7%; border-right: 2px solid #000000"></td>
                                    <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                        <asp:Label runat="server" ID="lblSubTotAmount"></asp:Label></strong></td>

                                    <td style="width: 5%; text-align: right; border-right: 2px solid #000000"><strong>
                                        <asp:Label runat="server" ID="lblSubDiscount"></asp:Label></strong></td>
                                    <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                        <asp:Label runat="server" ID="lblSubNet"></asp:Label></strong></td>

                                    <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                        <asp:Label runat="server" ID="lblSubDue"></asp:Label></strong></td>
                                    <td style="width: 10%; text-align: right"><strong>
                                        <asp:Label runat="server" ID="lblSubRec"></asp:Label></strong></td>
                                </tr>
                            </table>
                        </ItemTemplate>

                    </asp:Repeater>



                    <div style="padding-top: 20px;">
                        <table style="width: 100%; border: 2px solid #000000;">
                            <tr>

                                <td style="width: 30%; text-align: right; border-right: 2px solid #000000"><strong>Return Grand Total :</strong></td>
                                <td style="width: 5%; text-align: center"><strong>
                                    <asp:Label runat="server" ID="lblTotQtyEx"></asp:Label></strong></td>
                                <td style="width: 5%"></td>
                                <td style="width: 7%; border-right: 2px solid #000000"></td>
                                <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblTotAmountEx"></asp:Label></strong></td>

                                <td style="width: 5%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblDiscountEx"></asp:Label></strong></td>
                                <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblNetEx"></asp:Label></strong></td>

                                <td style="width: 7%; text-align: right; border-right: 2px solid #000000"><strong>
                                    <asp:Label runat="server" ID="lblDueEx"></asp:Label></strong></td>
                                <td style="width: 10%; text-align: right"><strong>
                                    <asp:Label runat="server" ID="lblRecEx"></asp:Label></strong></td>

                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%; border: 3px; background: burlywood;">

                            <tr>
                                <td style="width: 15%"></td>
                                <td style="width: 60%; text-align: right"><strong>Total Amount: (Sale-Return)</strong></td>
                                <td style="width: 5%; text-align: center"><strong>:</strong></td>
                                <td style="width: 20%; text-align: right"><strong>
                                    <asp:Label runat="server" ID="lblTotalNetAmount"></asp:Label></strong></td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>




            <%--Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------
            Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------
            Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------
            Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------
            Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------
            Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due------Due--------%>




            <%--<div style="padding-top: 30px; padding-bottom: 10px">


                <div style="width: 96%; margin: 0% 2% 0% 2%;">
                    <u>
                        <h3>Due Collection &nbsp;&nbsp;&nbsp; : </h3>
                    </u>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        OnRowCreated="GridView1_RowCreated" ShowFooter="True"
                        OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>

                            <asp:BoundField HeaderText="Invoice Date">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" CssClass="" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Due Invoice">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Width="20%" CssClass="" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Advance Date">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Width="20%" CssClass="" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Advance Invoice">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Width="20%" CssClass="" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Net Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" CssClass="" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Due Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" CssClass="" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Receive Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                    </asp:GridView>

                </div>
            </div>--%>

            <div style="width: 96%; margin: 0% 2% 0% 2%;">
                <table style="width: 100%; border: 3px; background: burlywood">
                    <tr>
                        <td style="width: 15%"></td>
                        <td style="width: 60%; text-align: right"><strong>Sale  Amount</strong></td>
                        <td style="width: 5%; text-align: center"><strong>:</strong></td>
                        <td style="width: 20%; text-align: right"><strong>
                            <asp:Label runat="server" ID="lblTotalSaleAmount"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td style="width: 15%"></td>
                        <td style="width: 60%; text-align: right"><strong>Return Amount</strong></td>
                        <td style="width: 5%; text-align: center"><strong>:</strong></td>
                        <td style="width: 20%; text-align: right"><strong>
                            <asp:Label runat="server" ID="lblTotalReturnAmount"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td style="width: 15%"></td>
                        <td style="width: 60%; text-align: right"><strong>Discount Amount</strong></td>
                        <td style="width: 5%; text-align: center"><strong>:</strong></td>
                        <td style="width: 20%; text-align: right"><strong>
                            <asp:Label runat="server" ID="lblTotalDiscountAmount"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td style="width: 15%"></td>
                        <td style="width: 60%; text-align: right"><strong>Deduction Amount</strong></td>
                        <td style="width: 5%; text-align: center"><strong>:</strong></td>
                        <td style="width: 20%; text-align: right"><strong>
                            <asp:Label runat="server" ID="lblTotalDeductionAmount"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td style="width: 15%"></td>
                        <td style="width: 60%; text-align: right"><strong>Net Value </strong></td>
                        <td style="width: 5%; text-align: center"><strong>:</strong></td>
                        <td style="width: 20%; text-align: right"><strong>
                            <asp:Label runat="server" ID="lblTotalNetValue"></asp:Label></strong></td>
                    </tr>
                </table>
            </div>
        </div>

        <script type="text/javascript">
            $(document).ready(function () {
                $('#gridhead').hide();
                $(window).scroll(function (event) {
                    var scroll = $(window).scrollTop();
                    if (scroll > 100) {
                        $('#gridhead').show();
                    }
                    else
                        $('#gridhead').hide();
                });
            });
        </script>
    </form>
</body>
</html>
