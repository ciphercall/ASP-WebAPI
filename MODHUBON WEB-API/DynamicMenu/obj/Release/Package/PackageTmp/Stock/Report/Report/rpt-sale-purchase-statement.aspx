<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-sale-purchase-statement.aspx.cs"
    Inherits="AlchemyAccounting.Stock.Report.Report.rpt_sale_purchase_statement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            window.print();
        }
    </script>
    <style type="text/css" media="print">
        .ShowHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 14px "Calibri";
        }
        *
        {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }
        .page
        {
            width: 21cm;
            padding: .5cm;
            margin: 1cm auto;
        }
        .subpage
        {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }
        
        @page
        {
            size: A4;
            margin: 0;
        }
        @media print
        {
            .page
            {
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
        .gridHeadStyle
        {
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
                    <td style="width: 10%">
                    </td>
                    <td style="width: 80%; text-align: center">
                        &nbsp;
                        <asp:Label runat="server" ID="lblCompanyNM" Style="font-size: 20px; font-weight: 700"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                            style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit;
                            text-align: right" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 80%; text-align: center">
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <asp:Label ID="lblStore" runat="server" Font-Names="Calibri" Font-Size="14px" 
                            Font-Bold="False" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: center; font-size: 12px">
                        <asp:Label ID="lblType" runat="server" Font-Names="Calibri" Font-Size="14px" Font-Bold="True"></asp:Label>
                        &nbsp;<strong style="font-size: 14px">STATEMENT</strong>
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%; font-family: Calibri: 14px; text-align: left">
                        From :
                        <asp:Label ID="lblFdt" runat="server" Font-Names="Calibri" Font-Size="14px" 
                            Font-Bold="False"></asp:Label>
                        &nbsp; To :
                        <asp:Label ID="lblTdt" runat="server" Font-Names="Calibri" Font-Size="14px" 
                            Font-Bold="False"></asp:Label>
                        </td>
                    <td style="width: 50%; text-align: right">
                        <strong><span>Print Date :</span></strong>
                        <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"
                            ></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin-top: 1%; font-family: Calibri; font-size: 14px">
            <asp:GridView ID="gvRep" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                Font-Size="14px" Width="100%" onrowcreated="gvRep_RowCreated" 
                onrowdatabound="gvRep_RowDataBound" ShowFooter="True" 
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField HeaderText="Item Particulars">
                        <HeaderStyle HorizontalAlign="Center" Width="36%" />
                        <ItemStyle HorizontalAlign="Left" Width="36%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Unit">
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QTY">
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Rate">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="13%" />
                        <ItemStyle HorizontalAlign="Right" Width="13%" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle CssClass="gridHeadStyle" Font-Size="15px" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
