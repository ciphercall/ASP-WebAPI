<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptAccountsRecST.aspx.cs" Inherits="DynamicMenu.Accounts.Report.Report.rptAccountsRecST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report-ACCOUNTS RECEIVABLE- TOTAL</title>
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            window.print();
        }
    </script>
    <style type="text/css" media="print">
        .ShowHeader thead {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 14px "Calibri";
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            padding: .5cm;
            margin: 1cm auto;
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always; /* here always for subpage */
            }
        }

        .gridHeadStyle {
            height: 30px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div style="float: left; width: 100%; border-bottom: 1px solid #000">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%"></td>
                        <td style="width: 80%; text-align: center">&nbsp;
                        <asp:Label runat="server" ID="lblCompanyNM" Style="font-size: 20px; font-weight: 700"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                                style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit; text-align: right" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%"></td>
                        <td style="width: 80%; text-align: center">
                            <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                        </td>
                        <td style="width: 10%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%"></td>
                        <td style="text-align: center; font-size: 12px">&nbsp;<strong style="font-size: 14px">Accounts Receivable -Total</strong>
                        </td>
                        <td style="width: 10%"></td>
                    </tr>
                    <tr style="display: none">
                        <td style="width: 10%">Store Name:</td>
                        <td style="text-align: left; font-size: 12px">&nbsp;<asp:Label ID="lblStNM" runat="server"
                            Style="font-family: Calibri; font-size: 16px"></asp:Label></td>
                        <td style="width: 10%">&nbsp;</td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; font-family: Calibri; font-size: 14px; text-align: left">Up To :
                        <asp:Label ID="lblFdt" runat="server" Font-Names="Calibri" Font-Size="14px"
                            Font-Bold="False"></asp:Label>
                            &nbsp;
                        <asp:Label ID="lblTdt" runat="server" Font-Names="Calibri" Font-Size="14px" style="display: none"
                            Font-Bold="False"></asp:Label>
                        </td>
                        <td style="width: 50%; text-align: right">
                            <strong><span>Print Date :</span></strong>
                            <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left; width: 100%; margin-top: 1%; font-family: Calibri; font-size: 14px">
                <asp:GridView ID="gvRep" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                    Font-Size="14px" Width="100%" OnRowDataBound="gvRep_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True">
                    <Columns>
                         <asp:BoundField HeaderText="SL.">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Party Name">
                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ledger Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="LC Rcv. Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                        </asp:BoundField>

                    </Columns>
                    <FooterStyle CssClass="gridHeadStyle" Font-Size="15px" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
