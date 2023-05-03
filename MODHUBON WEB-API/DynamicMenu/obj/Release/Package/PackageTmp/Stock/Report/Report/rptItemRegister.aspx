<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptItemRegister.aspx.cs"
    Inherits="AlchemyAccounting.Stock.Report.Report.rptItemRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
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
    <style media="print">
        .showHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        #btnPrint
        {
            font-weight: 700;
        }
        .style1
        {
            font-size: small;
        }
        .style2
        {
            font-size: 16px;
            font-family: Calibri;
        }
        
        .SubTotalRowStyle
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
            font-size: 16px;
            text-align: right;
            height: 25px;
            font-family: Calibri;
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
        }
        .style3
        {
            font-family: Calibri;
        }
        .style9
        {
            font-family: Calibri;
            font-size: 14pt;
        }
        .style10
        {
            width: 115px;
            font-family: Calibri;
            font-size: 16px;
        }
        .style11
        {
            width: 115px;
            font-family: Calibri;
            font-size: 15px;
        }
        .style12
        {
            width: 24px;
            text-align: center;
        }
        .style13
        {
            font-size: 16px;
            font-family: Calibri;
            width: 24px;
            text-align: center;
        }
        .style14
        {
            width: 538px;
        }
        .style15
        {
            width: 538px;
            font-family: Calibri;
            font-size: 16px;
        }
        .style16
        {
            width: 115px;
        }
        .style17
        {
            width: 383px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
             <asp:ScriptManager runat="server" ID="scriptmanger1">
       </asp:ScriptManager>
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" Style="font-family: Calibri; font-size: 20px;
                        font-weight: 700"></asp:Label>
                </td>
                <td class="style5" style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;
                </td>
                <td class="style9">
                    <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;
                </td>
                <td class="style9">
                    <strong>STORE WISE ITEM REGISTER</strong>
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;
                </td>
                <td class="style8">
                    <span class="style2">FROM </span><strong><span class="style2">:&nbsp; </span></strong>
                    <asp:Label ID="lblFDate" runat="server" CssClass="style2"></asp:Label>
                    <span class="style3">&nbsp;&nbsp;&nbsp; TO <strong>:&nbsp; </strong></span>
                    <asp:Label ID="lblTDate" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" Style="text-align: right; font-family: Calibri;
                        font-size: 15px;"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <div style="width: 96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
        </div>
        <table style="width: 96%; margin: 1% 2% 0% 2%;">
            <tr>
                <td class="style11">
                    STORE NAME
                </td>
                <td class="style12" style="font-family: Calibri; font-size: 16px">
                    :
                </td>
                <td class="style14">
                    <asp:Label ID="lblStNM" runat="server" Style="font-family: Calibri; font-size: 16px"></asp:Label>
                </td>
                <td class="style17">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style10">
                    ITEM NAME
                </td>
                <td class="style13">
                    :
                </td>
                <td class="style14">
                    <asp:Label ID="lblItNM" runat="server" Style="font-family: Calibri; font-size: 16px"></asp:Label>
                </td>
                <td class="style17">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style16">
                </td>
                <td>
                </td>
                <td style="text-align: right" class="style15">
                    &nbsp;
                </td>
                <td style="text-align: right" class="style17">
                    <strong>Opening Balance :</strong>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblOpenBalance" runat="server" Style="font-family: Calibri; font-size: 16px;
                        font-weight: 700"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="showHeader" style="width: 96%; margin: 0% 2% 0% 2%;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                Width="100%" ShowFooter="True">
                <Columns>
                    <asp:BoundField HeaderText="Date">
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle Width="8%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Particulars">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Store Name">
                        <HeaderStyle HorizontalAlign="Center" Width="27%" />
                        <ItemStyle HorizontalAlign="Center" Width="27%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Receive">
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Sale">
                        <HeaderStyle Width="6%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Exchange">
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Issue">
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Total">
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                    </asp:BoundField>
                   
                </Columns>
                <FooterStyle CssClass="GrandTotalRowStyle" />
                <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                <RowStyle Font-Size="14px" Font-Names="Calibri" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
