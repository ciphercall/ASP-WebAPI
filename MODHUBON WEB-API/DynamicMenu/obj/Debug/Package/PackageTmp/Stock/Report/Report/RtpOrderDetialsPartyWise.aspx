<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RtpOrderDetialsPartyWise.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.RtpOrderDetialsPartyWise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#maincontectforcopyto").append($("#maincontectforcopy").html());
        });
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }

    </script>
    <style type="text/css">
        #main {
            width: 840px;
            /* to centre page on screen*/
            margin-left: auto;
            margin-right: auto;
        }

        .auto-style3 {
            width: 26px;
        }

        .auto-style4 {
            width: 1033px;
        }

        .auto-style1 {
            width: 500px;
        }

        .SubTotalRowStyle {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
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
            font-size: 20px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
        </div>
        <div id="main">
            <table style="width: 100%; font-weight: bold">
                <tr>
                    <td style="width: 50%; text-align: center">
                        <div id="maincontectforcopy">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" colspan="3">
                                        <asp:Label ID="lblCompNM" runat="server"
                                            Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                                    </td>
                                    <td style="text-align: right"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" colspan="3" style="">
                                        <asp:Label ID="lblAddress" runat="server"
                                            Style="font-family: Calibri; font-size: 9px"></asp:Label>
                                    </td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" colspan="3" style="font-size: 15px">
                                        <strong>ORDER DETIALS - PARTY WISE
                               
                                        </strong></td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" style="text-align: left; font-size: 12px;">From Date</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="lblFromDate"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" style="text-align: left; font-size: 12px;">To Date</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="lblToDate"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" style="text-align: left; font-size: 12px;">PARTY NAME</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="lblParty" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" style="text-align: left; font-size: 12px;">Print Date
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td style="text-align: left" class="auto-style4">
                                        <asp:Label ID="lblTime" runat="server"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <div style="width: 96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
                            </div>
                            <div style="width: 96%; margin: 0% 2% 0% 2%;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" Width="100%" Font-Bold="True" Font-Size="20px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Item Name (English)" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Item Name">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Remarks">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Qty">
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Rate">
                                            <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                            <ItemStyle HorizontalAlign="Right" Width="6%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Amount">
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                                </asp:GridView>

                            </div>
                        </div>
                    </td>
                    <td style="width: 50%; text-align: center">
                        <div id="maincontectforcopyto">
                            
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

