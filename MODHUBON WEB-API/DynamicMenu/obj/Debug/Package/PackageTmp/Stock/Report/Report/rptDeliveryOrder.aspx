<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptDeliveryOrder.aspx.cs"
    Inherits="AlchemyAccounting.Stock.Report.Report.rptDeliveryOrder" %>

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
        
        .style2
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            font-size: xx-small;
            width: 510px;
        }
        .style3
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue" , Arial, Helvetica, sans-serif;
            font-size: small;
            width: 510px;
        }
        .style4
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            width: 187px;
        }
        .style5
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue" , Arial, Helvetica, sans-serif;
            font-size: medium;
            width: 510px;
        }
        
        .style6
        {
            width: 4px;
        }
        .style8
        {
            width: 1px;
            font-weight: bold;
        }
        .style10
        {
            width: 326px;
            font-family: Calibri;
        }
        
        .style14
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue" , Arial, Helvetica, sans-serif;
            font-size: xx-small;
            width: 140px;
        }
        .style15
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue" , Arial, Helvetica, sans-serif;
            font-size: small;
            width: 140px;
        }
        
        .style1900
        {
            width: 100px;
        }
        
        .style20
        {
            width: 118px;
        }
        
        .style26
        {
            width: 95px;
        }
        .style27
        {
            width: 135px;
        }
        .style28
        {
            width: 144px;
        }
        .style29
        {
            width: 155px;
        }
        
        .style30
        {
            width: 420px;
        }
        
        .style31
        {
            width: 97px;
            font-family: Calibri;
        }
        .style34
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            font-size: 11px;
            width: 510px;
        }
        
        .style35
        {
            width: 524px;
        }
        .imgbox
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue" , Arial, Helvetica, sans-serif;
            width: 187px;
        }
    </style>
</head>
<body style="font-size: medium">
    <form id="form1" runat="server">
    <div>
        <%--<table style="width: 100%;">
            <tr>
                <td style="text-align: right;">
                        
            </tr>
        </table>--%>
        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
            style="float: right; height: 20px;" />
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <div style="height: 1050px;">
                    <div>
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center; font-size: xx-large; font-weight: 700" class="style4"
                                    rowspan="5">
                                    <img src="../../../Images/logo.png" alt="logo" style="float: left; width: 187px;
                                        height: 100px;" />
                                    <%--<div style="width: 100%; float:left; margin-top: 0px;">
                                <table>
                                    <tr>
                                        <td style="font-family: Calibri; font-size: 10pt" class="style36">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                </div>--%>
                                </td>
                                <td style="text-align: center; font-size: 25px; font-family: Calibri; font-weight: bold;"
                                    class="style2">
                                    <%#Eval("COMPNM")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="style34" style="font-family: Calibri; font-size: 11px; font-weight: bold">
                                    <%#Eval("ADDRESS")%>
                                </td>
                                <td style="text-align: center" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style34" style="text-align: center; font-family: Calibri; font-size: 11px;
                                    font-weight: bold">
                                    TELEPHONE :
                                    <%#Eval("CONTACTNO")%>
                                </td>
                                <td class="style15" style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style3" style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    &nbsp;
                                </td>
                                <td class="style15" style="text-align: right; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    <strong style="font-family: Calibri">DELIVERY ORDER</strong>
                                </td>
                                <td class="style15" style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    <asp:Label ID="lblTime" runat="server" Style="text-align: center; font-family: Arial, Helvetica, sans-serif;
                                        font-size: 10px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 98%; margin: 0% 1% 0% 1%; height: 1px; background: #000000;">
                        </div>
                    </div>
                    <div>
                        <div style="background-image: url(../../../Images/watermark-marco-do.png); background-repeat: no-repeat;
                            background-position: center;">
                            <table style="width: 100%; margin-top: 5px; font-size: small;">
                                <tr>
                                    <td class="style6">
                                        &nbsp;
                                    </td>
                                    <td class="style31">
                                        Date
                                    </td>
                                    <td class="style8">
                                        :
                                    </td>
                                    <td class="style35" style="font-family: Calibri; font-size: medium">
                                        <%#Eval("TRANSDT")%>
                                    </td>
                                    <td class="style10" style="font-family: Calibri; font-size: medium">
                                        Sales Memo No <strong>:&nbsp; </strong>
                                        <%#Eval("TRANSNO")%>
                                    </td>
                                    <td style="text-align: right" class="style26">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right; font-family: Calibri;" class="style30">
                                        <asp:Label ID="lblSalesMemoNo" runat="server" Style="font-family: Calibri; font-size: medium"
                                            Visible="False"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        &nbsp;
                                    </td>
                                    <td class="style31">
                                        Sales To
                                    </td>
                                    <td class="style8">
                                        :
                                    </td>
                                    <td class="style35" style="font-weight: 700; font-family: Calibri; font-size: medium;">
                                        <%#Eval("ACCOUNTNM")%>
                                    </td>
                                    <td class="style10">
                                        &nbsp;
                                    </td>
                                    <td class="style26">
                                        &nbsp;
                                    </td>
                                    <td class="style30">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        &nbsp;
                                    </td>
                                    <td class="style31">
                                        Address
                                    </td>
                                    <td class="style8">
                                        :
                                    </td>
                                    <td class="style35">
                                        <%--<asp:Label ID="lblSaleToAdd" runat="server" 
                                    style="font-family: Calibri; font-size: 10px;"></asp:Label>--%>
                                    </td>
                                    <td class="style10">
                                        &nbsp;
                                    </td>
                                    <td class="style26">
                                        &nbsp;
                                    </td>
                                    <td class="style30">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        &nbsp;
                                    </td>
                                    <td class="style31">
                                        Sale From
                                    </td>
                                    <td class="style8">
                                        :
                                    </td>
                                    <td class="style35" style="font-weight: 700; font-family: Calibri; font-size: medium;">
                                        <asp:Label ID="lblStoreFr" runat="server" Text='<%#Eval("STORENM")%>'></asp:Label>
                                        <asp:Label ID="lblStoreID" runat="server" Text='<%#Eval("STOREFR")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        &nbsp;
                                    </td>
                                    <td class="style26">
                                        &nbsp;
                                    </td>
                                    <td class="style30">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <div style="width: 98%; margin: 1% 1% 0% 1%;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Bold="True" Font-Size="20px"
                                    ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="SL" DataField="SL">
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Item Particulars" DataField="ITEMNM">
                                            <HeaderStyle Width="50%" HorizontalAlign="Center" />
                                            <ItemStyle Width="50%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Carton Qty" DataField="CQTY" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Pieces" DataField="CPQTY">
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Total" DataField="QTY">
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                                    <RowStyle Font-Names="Calibri" Font-Size="14px" />
                                </asp:GridView>
                            </div>
                            <table style="width: 94%; margin: 1% 3% 0% 3%;">
                                <tr>
                                    <td class="style19" style="font-family: Calibri">
                                        In Words :&nbsp;
                                        <asp:Label ID="lblInWords" runat="server" Style="font-family: Calibri; font-weight: bold"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 98%; margin: 5% 1% 0% 1%; font-family: Calibri;">
                                <tr>
                                    <td class="style1900">
                                    </td>
                                    <td class="style20" style="text-align: center; border-top: 1px solid #CCCCCC;">
                                        Prepared By
                                    </td>
                                    <td class="style20">
                                    </td>
                                    <td class="style28" style="text-align: center; border-top: 1px solid #CCCCCC;">
                                        Received By
                                    </td>
                                    <td class="style20">
                                    </td>
                                    <td style="text-align: center; border-top: 1px solid #CCCCCC;" class="style20">
                                        Checked By
                                    </td>
                                    <td class="style20">
                                    </td>
                                    <td style="text-align: center; border-top: 1px solid #CCCCCC;" class="style20">
                                        Authorized By
                                    </td>
                                    <td class="style20">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
