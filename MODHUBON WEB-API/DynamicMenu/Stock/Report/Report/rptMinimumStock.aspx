<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptMinimumStock.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rptMinimumStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> :: ITEM SHORT LIST ::</title>
    
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
                font-size: small;
            }
            .style2
            {
                font-size: medium;
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
            padding-left:0%;
            
        }
            .style3
            {
                font-family: Calibri;
            }
      </style>

</head>
<body>
    <form id="form1" runat="server">
     <div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="style6">&nbsp;</td>
                        <td class="style1">
                            <asp:Label ID="lblCompNM" runat="server"
                                Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                        </td>
                        <td class="style5" style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style3">
                            <asp:Label ID="lblAddress" runat="server"
                                Style="font-family: Calibri; font-size: 9px"></asp:Label>
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style3">
                            <strong style="font-size: medium">ITEM SHORT LIST</strong></td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style8">
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style8">
                            <span class="style2">AS ON DATE </span><strong><span class="style2">:&nbsp; 
                            </span></strong>
                            <asp:Label ID="lblDate" runat="server" CssClass="style2"></asp:Label>
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">
                            <asp:Label ID="lblTime" runat="server"
                                Style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <div style="width: 96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
                </div>
                <table style="width: 96%; margin: 1% 2% 0% 2%;">
                    <tr>
                        <td style="font-family: Calibri; font-size: medium">
                            <strong>STORE NAME : &nbsp;<asp:Label ID="lblStoreNM" runat="server"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                </table>
                <div style="width: 96%; margin: 0% 2% 0% 2%;">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        OnRowCreated="GridView1_RowCreated" Font-Bold="True" Font-Size="20px"
                        OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                              <asp:BoundField HeaderText="SL">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle Width="5%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Book Name">
                                <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                <ItemStyle Width="30%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Author">
                                <HeaderStyle HorizontalAlign="Center" Width="22%" />
                                <ItemStyle Width="22%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                            </asp:BoundField>
                              <asp:BoundField HeaderText="Edition">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="Barcode">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Current Stock">
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Min. Stock Qty.">
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Short">
                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
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
