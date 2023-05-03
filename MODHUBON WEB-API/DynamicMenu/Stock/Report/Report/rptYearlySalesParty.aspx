<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptYearlySalesParty.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rptYearlySalesParty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>YEARLY SALES - PARTY</title>

    <script src="../../../MenuCssJs/js/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>

    <style media="print">
        .showHeader thead {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        #main {
            float: left;
            border: 1px solid #cccccc;
            width: 100%;
            padding-bottom: 40px;
        }

        #btnPrint {
            font-weight: 700;
        }

        .style1 {
            font-size: small;
        }

        .style2 {
            font-size: medium;
            font-family: Calibri;
        }

        .SubTotalRowStyle {
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

        .style3 {
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
                                Style="font-family: Calibri; font-size: 12px"></asp:Label>
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style3">
                            <strong style="font-size: medium">YEARLY SALES - PARTY</strong></td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style8">
                           <strong>  Item Name: 
                                <asp:Label ID="lblParty" runat="server" Text="lblPartyName"></asp:Label>
                            </strong>
                            
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style8" style="font-weight: bold">
                            <span class="style2">YEAR </span><strong><span class="style2">:&nbsp; 
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
         <br/>
                <div style="width: 96%; margin: 0% 2% 0% 2%;">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="GridView1_OnRowDataBound" Width="100%" ShowFooter="True">
                        <Columns>
                            <asp:BoundField HeaderText="Party Name">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                               <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                                                   
                            <asp:BoundField HeaderText="January">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="February">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="March">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="April">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="May">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="June">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="July">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="August">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="September">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="October">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="November">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="December">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Total">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle Font-Names="Calibri" Font-Size="14px" />
                        <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                    </asp:GridView>

                </div>
            </div>
    </div>
    </form>
</body>
</html>
