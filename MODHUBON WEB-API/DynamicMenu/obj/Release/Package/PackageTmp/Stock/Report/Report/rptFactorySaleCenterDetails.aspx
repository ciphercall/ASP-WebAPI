<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptFactorySaleCenterDetails.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptFactorySaleCenterDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Details</title>
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

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

        .SubTotalRowStyle
        {
            border: solid 1px Black;
            
            font-weight: bold;
            text-align: center;
        }
        .SubTotalRowStyleAmount
        {
            border: solid 1px Black;
            
            font-weight: bold;
            text-align: right;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 18px;
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
        .GridRowStyle
        {
            padding-left:10%;
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div>
                <table style="width: 100%;">
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
                            <strong>Store Wise Transection Details - Issue</strong></td>
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
                        <td class="auto-style1" style="width: 200px">
                            <strong>Store From</strong></td>
                        <td style="width: 1px"><strong>:</strong></td>
                        <td class="auto-style4"><asp:Label runat="server" ID="lblStoreFr"></asp:Label></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">
                            <strong>Store To</strong></td>
                       <td style="width: 1px"><strong>:</strong></td>
                        <td class="auto-style4"><asp:Label runat="server" ID="lblStoreTo"></asp:Label></td>
                        <td style="text-align: right; width: 400px"> <asp:Label ID="lblTime" runat="server"
                                Style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label></td>
                        <td style="text-align: right">&nbsp; <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label></td>
                    </tr>
                    
                </table>
                <div style="width: 96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
                </div>
                <div style="width: 96%; margin: 0% 2% 0% 2%;">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        OnRowCreated="GridView1_RowCreated" ShowFooter="True"
                        OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="Date & Memo">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle Width="5%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Item Name">
                                <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Quantity">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Rate">
                                <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Right" Width="20%" />
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
