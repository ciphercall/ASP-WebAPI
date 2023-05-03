<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptFactoryWiseTransSummary.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptFactoryWiseTransSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Summary</title>

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
            text-align: left;
            width: 933px;
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
        }
        .style3
        {
            font-family: Calibri;
        }
        .style9
        {
            text-align: left;
            font-family: Calibri;
            font-size: 16px;
            width: 933px;
        }
        .style8
        {
            text-align: left;
            width: 933px;
        }
        .style10
        {
            width: 19px;
        }
                        
        .subTotalStyle
        {
            font-family:Calibri;
            font-size:15px;
            font-weight:bold;
        }
        
        .footerStyle
        {
            
        }
        
        .style13
        {
            width: 13px;
        }
        .style14
        {
            width: 558px;
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
                <td class="style13" style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right" class="style14">
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
                <td class="style13">
                    &nbsp;</td>
                <td style="text-align: right" class="style14">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style9">
                    <strong>STORE WISE TRANSACTION SUMMARY - Issue
                    </strong></td>
                <td class="style13">
                    &nbsp;</td>
                <td style="text-align: right" class="style14">
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
                <td class="style13">
                    &nbsp;</td>
                <td style="text-align: right" class="style14">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 15px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style8">
                    <span class="style2">FROM </span> <strong><span class="style2">:&nbsp; 
                    </span> </strong>
                    <asp:Label ID="lblStoreFr" runat="server" CssClass="style2"></asp:Label>
                    <span class="style3">&nbsp;&nbsp;&nbsp; TO <strong>:&nbsp; </strong>
                    </span>
                    <asp:Label ID="lblStoreTo" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td class="style13">
                    &nbsp;</td>
                <td style="text-align: right" class="style14">
                    <asp:Label ID="Label2" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 15px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>

            <div style = "width:96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
            </div>

            <div class="showHeader" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" 
                    ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Item Name">
                        <HeaderStyle HorizontalAlign="Center" Width="50%" />
                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Quantity">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Rate">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>
    </div>
    </form>
</body>
</html>
