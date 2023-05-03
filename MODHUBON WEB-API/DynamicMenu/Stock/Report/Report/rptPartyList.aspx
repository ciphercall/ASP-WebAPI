<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptPartyList.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rptPartyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Party Information</title>

    <script src="../../../MenuCssJs/js/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../../MenuCssJs/js/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
        $(document).ready(function () {
            $('#lblcodeEx').hover(function () {

            });
        });
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
        a>div { display: none; }

        .hover_img .a {
            position: inherit;
        }

            .hover_img .a span {
                position: absolute;
                display: none;
                z-index: 99;
            }

            .hover_img .a:hover span {
                display: block;
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
                            <strong>Party Details</strong></td>
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
                        <td style="text-align: right">
                            <asp:Label ID="lblTime" runat="server"
                                Style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <div style="width: 96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
                </div>
                <div style="width: 96%; margin: 0% 2% 0% 2%; font-weight: bold; font-size: 20px ">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Size="22px"
                        OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="Party Code">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Party Name (English)">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Party Name (Bengali)">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Address (English)">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Address (Bengali)">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mobile 1">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mobile 2">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Email">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="AP Name">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="AP Contact">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Remarks">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                        <RowStyle Font-Names="Calibri" Font-Size="11px" />
                    </asp:GridView>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
