<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RtpOrderSummaryPartyWise.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.RtpOrderSummaryPartyWise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
    <style type="text/css">
        
        .auto-style3 {
            width: 26px;
        }
        .auto-style4 {
            width: 1033px;
        }
        .auto-style1 {
            width: 200px;
        }

        .SubTotalRowStyle
        {
            border: solid 1px Black;
            font-size: 18px;
            font-weight: bold;
            text-align: right;
        }
        .SubTotalRowStyleAmount
        {
            border: solid 1px Black;
            font-size: 20px;
            font-weight: bold;
            text-align: right;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 20px;
            text-align: right;
            height: 35px;
        }
        .GroupHeaderStyle
        {
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
    <div>
            <div>
                <table style="width: 100%; font-weight: bold">
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1" colspan="3" >
                            <asp:Label ID="lblCompNM" runat="server"
                                Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1" colspan="3">
                            <asp:Label ID="lblAddress" runat="server"
                                Style="font-family: Calibri; font-size: 9px"></asp:Label>
                        </td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1" colspan="3">
                            <strong>ORDER SUMMARY - PARTY WISE
                               
                            </strong></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">
                            <strong>From Date</strong></td>
                        <td style="width: 1px"><strong>:</strong></td>
                        <td class="auto-style4"><asp:Label runat="server" ID="lblFromDate"></asp:Label></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">
                            <strong>To Date</strong></td>
                       <td style="width: 1px"><strong>:</strong></td>
                        <td class="auto-style4"><asp:Label runat="server" ID="lblToDate"></asp:Label></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">
                            <strong>PARTY NAME</strong></td>
                       <td style="width: 1px"><strong>:</strong></td>
                        <td class="auto-style4"><asp:Label runat="server" ID="lblParty"></asp:Label></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td  style="width: 1px">&nbsp;</td>
                        <td style="text-align: right" class="auto-style4">
                            <asp:Label ID="lblTime" runat="server"
                                Style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label>
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
                            <asp:BoundField HeaderText="Item Name (English)">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Width="20%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Item Name (Bengali)">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Qty">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                            </asp:BoundField>
                            
                             <asp:BoundField HeaderText="Rate">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
                                 <ItemStyle HorizontalAlign="Right" Width="20%" />
                            </asp:BoundField>
                            
                             <asp:BoundField HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                    </asp:GridView>
                   
                </div>
            </div>
        </div>
    </form>
</body>
</html>
