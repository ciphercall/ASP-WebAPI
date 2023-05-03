<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptPartySumStatement.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptPartySumStatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

        <script type ="text/javascript">
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

    <style type ="text/css">
    #btnPrint
        {
            font-weight: 700;
        }
            .style1
            {
                font-size: small;
            text-align: center;
            width: 821px;
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
            font-family:Calibri;
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
            padding-left: 6%;
        }
        .style3
        {
            font-family: Calibri;
        }
        .style9
        {
            text-align: center;
            font-family: Calibri;
            font-size: 16px;
            width: 821px;
        }
        .style8
        {
            text-align: center;
            width: 821px;
        }
        .style10
        {
            width: 227px;
        }
        .style11
        {
            width: 6px;
        }
        .style12
        {
            width: 242px;
        }
      </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" 
                        style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                </td>
                <td class="style11" style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right" class="style12">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td class="style11">
                    &nbsp;</td>
                <td style="text-align: right" class="style12">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style9">
                    <strong>PARTY WISE SUMMARIZED
                    <asp:Label ID="lblType" runat="server"></asp:Label>
&nbsp;STATEMENT</strong></td>
                <td class="style11">
                    &nbsp;</td>
                <td style="text-align: right" class="style12">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style8">
                    <span class="style2">FROM </span> <strong><span class="style2">:&nbsp; 
                    </span> </strong>
                    <asp:Label ID="lblFDate" runat="server" CssClass="style2"></asp:Label>
                    <span class="style3">&nbsp;&nbsp;&nbsp; TO <strong>:&nbsp; </strong>
                    </span>
                    <asp:Label ID="lblTDate" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td class="style11">
                    &nbsp;</td>
                <td style="text-align: right" class="style12">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 15px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <div style = "width:96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
            </div>

            <table style = "width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td style="font-family: Calibri; font-size: 16px; font-weight: 700">
                        
                    &nbsp;<strong><asp:Label ID="lblTypeHD" runat="server"></asp:Label>
                        </strong>&nbsp;Name :
                        <asp:Label ID="lblPSName" runat="server"></asp:Label>
                        
                    </td>
                </tr>
            </table>
            <div class="showHeader" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" ShowFooter="True" 
                    ShowHeaderWhenEmpty="True" onrowcreated="GridView1_RowCreated">
                    <Columns>
                        <asp:BoundField HeaderText="Item Name">
                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                        <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridRowStyle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Carton Qty">
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Rate" >
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="14px" />
                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

            </div>
    </div>
    </form>
</body>
</html>
