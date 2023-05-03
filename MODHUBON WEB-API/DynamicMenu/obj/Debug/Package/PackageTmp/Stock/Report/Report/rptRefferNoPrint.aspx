<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptRefferNoPrint.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rptRefferNoPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager runat="server" ID="scriptmanger1">
       </asp:ScriptManager>
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" 
                        style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                </td>
                <td class="style5" style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style3">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style3">
                    <strong style="font-size: 16px">REFER NUMBER REPORT</strong></td>
                <td class="style5">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    
                </td>
                <td class="style5">
                    &nbsp;</td>
                <td style="text-align: right; margin-left: 80px;">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 16px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <div style = "width:96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
            </div>
            <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td style="font-family: Calibri; font-size: 16px">
                        <strong>REFER NO : &nbsp;<asp:Label ID="lblItemName" runat="server"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div style = "width:96%; margin: 0% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" ShowFooter="False" Font-Bold="True" Font-Size="20px"
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField HeaderText="Item Name" >
                        <HeaderStyle HorizontalAlign="Center" Width="35%" />
                        <ItemStyle Width="35%" HorizontalAlign="Justify" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Barcode">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Buy Rate" >
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sale Rate" >
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Min Qty" >
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Names="Calibri" Font-Size="14px" />
                </asp:GridView>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
