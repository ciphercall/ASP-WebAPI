<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptSalesSummarizedStatement.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rptSalesSummarizedStatement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
    
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
        #main
        {
            float:left;
            border: 1px solid #cccccc;
            width: 100%;
            padding-bottom:40px;
        }
        #btnPrint
        {
            font-weight: 700;
        }
            .style1
            {
                font-size: 16pt;
                font-weight: 700;
                font-family: Calibri;
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
            float: left;
            margin-left: 150px;
        }
            .style3
            {
                font-family: Calibri;
            }
            #print
            {
                font-family: Calibri;
                font-size: 16px;
                font-weight: 700;
            }
      </style>
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <div align="center">
            <table style="width: 80%; align-content: center">
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
                    <strong style="font-size: 16px">SALES SUMMARIZED STATEMENT</strong></td>
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
                    <strong>From: <asp:Label runat="server" ID="lblDateFrom"></asp:Label>&nbsp;&nbsp;To: 
                        <asp:Label runat="server" ID="lblDateTo"></asp:Label>
                    </strong>
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
            <div style = "width:80%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
            </div>
            <br/> 
            <br/>
          <%--  <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td style="font-family: Calibri; font-size: 16px">
                        <strong>ITEM NAME : &nbsp;<asp:Label ID="lblItemName" runat="server"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>--%>
            <div style = "width:80%; margin: 0% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    OnRowDataBound="GridView1_OnRowDataBound" Width="100%" ShowFooter="True" 
                    ShowHeaderWhenEmpty="True" Font-Bold="True" Font-Size="20px">
                    <Columns>
                        <asp:BoundField HeaderText="Party Name" >
                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sold" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Return-Good" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Return-Bad" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Return-Shira" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Net Sale" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
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
