<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptTransferMemo.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptTransferMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <style type="text/css">
        #btnPrint
        {
            font-weight: 700;
        }
        
        .style2
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: xx-small;
            width: 1050px;
        }
        .style3
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: small;
            width: 1050px;
        }
        .style4
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            width: 98px;
        }
        .style5
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            font-size: medium;
            width: 1050px;
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
            width: 210px;
        }
        
        .style11
        {
            width: 381px;
        }
        
        .style13
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: medium;
            width: 98px;
        }
        .style14
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: xx-small;
            width: 140px;
        }
        .style15
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: small;
            width: 140px;
        }
        
        .style19
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        
        .style20
        {
            width: 118px;
        }
        
        .style25
        {
            width: 223px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style26
        {
            width: 95px;
        }
        .style27
        {
            width: 135px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style28
        {
            width: 144px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style29
        {
            width: 155px;
            font-family: Arial, Helvetica, sans-serif;
        }
        
        .style30
        {
            width: 420px;
        }
        
        .style31
        {
            width: 141px;
        }
        .style33
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Times New Roman", Times, serif;
            font-size: small;
            width: 1050px;
        }
        
        .style34
        {
            font-family: Calibri;
            font-size: medium;
        }
        
    </style>

</head>
<body style="font-size: medium">
    <form id="form1" runat="server">
    <div>
        <div>
            <div>
                <table style="width:100%;">
                    <tr>
                        <td style="text-align: center; font-size: xx-large; font-weight: 700" 
                            class="style4">
                            &nbsp;</td>
                        <td style="text-align: center; font-size: x-large; font-weight: 700" 
                            class="style2">
                    <asp:Label ID="lblCompNM" runat="server" 
                                style="font-family: Calibri; font-size: 25px; font-weight: 700"></asp:Label>
                        </td>
                        <td style="text-align: center; font-size: x-large; font-weight: 700" 
                            class="style14">
                        <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                    </tr>
                    <tr>
                        <td style="text-align: center" class="style4">
                            &nbsp;</td>
                        <td class="style33">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 11px"></asp:Label>
                        </td>
                        <td style="text-align: center" class="style14">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style13" 
                        
                            
                            style="text-align: center; font-weight: 700; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                        <td class="style33" 
                        
                            
                            style="text-align: center; ">
                            TELEPHONE :
                    <asp:Label ID="lblContact" runat="server" 
                        style="font-family: Calibri; font-size: 11px"></asp:Label>
                        </td>
                        <td class="style15" 
                        
                            
                            
                            style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style13" 
                        
                            
                            style="text-align: center; font-weight: 700; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                        <td class="style3" 
                        
                            
                            style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                        <td class="style15" 
                        
                            
                            
                            
                            style="text-align: right; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style13" 
                        
                            
                            style="text-align: center; font-weight: 700; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                        <td class="style5" 
                        
                            
                            
                            style="text-align: center; ">
                            <strong>DELIVERY CHALLAN - TRANSFER</strong></td>
                        <td class="style15" 
                        
                            
                            
                            
                            
                            style="text-align: center; font-family: Calibri">
                    <asp:Label ID="lblTime" runat="server" 
                                
                                style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style = "width:98%; margin: 0% 1% 0% 1%; height: 1px; background: #000000;">
                </div>
              </div>
              <table style="width:100%; margin-top: 5px; font-family: Calibri;">
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td class="style31">
                                Date</td>
                            <td class="style8">
                                :</td>
                            <td class="style11">
                                <asp:Label ID="lblInVDT" runat="server" CssClass="style34"></asp:Label>
                            </td>
                            <td class="style10">
                                Invoice No <strong>:&nbsp; </strong>
                                <asp:Label ID="lblInVNo" runat="server" CssClass="style34"></asp:Label>
                            </td>
                            <td style="text-align: right" class="style26">
                                &nbsp;</td>
                            <td style="text-align: right" class="style30">
                                Sales Memo No <strong>:</strong>
                                <asp:Label ID="lblSalesMemoNo" runat="server" CssClass="style34"></asp:Label>
&nbsp; </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td class="style31">
                                Transfer From&nbsp;</td>
                            <td class="style8">
                                :</td>
                            <td class="style11">
                                <asp:Label ID="lblTransferFrom" runat="server" 
                                    
                                    
                                    style="font-weight: 700; " CssClass="style34"></asp:Label>
                            </td>
                            <td class="style10">
                                &nbsp;</td>
                            <td class="style26">
                                &nbsp;</td>
                            <td class="style30">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td class="style31">
                                Transfer To</td>
                            <td class="style8">
                                :</td>
                            <td class="style11">
                                <asp:Label ID="lblTransferTo" runat="server" CssClass="style34"></asp:Label>
                            </td>
                            <td class="style10">
                                &nbsp;</td>
                            <td class="style26">
                                &nbsp;</td>
                            <td class="style30">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <div style = "width:98%; margin: 1% 1% 0% 1%;"> 
                        
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            onrowdatabound="GridView1_RowDataBound" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="SL" HeaderText="SL">
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEMNM" HeaderText="Item Particulars" >
                                <HeaderStyle Width="550px" />
                                <ItemStyle Width="550px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Carton Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCarton" runat="server" Text='<%#Eval("CQTY") %>'></asp:Label>
                                        &nbsp;<asp:Label ID="lblCross" runat="server" Text="X"></asp:Label>
                                        &nbsp;<asp:Label ID="lblCartonQty" runat="server" Text='<%#Eval("CPQTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="180px" Wrap="True" />
                                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="QTY" HeaderText="Qty">
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RATE" HeaderText="Rate">
                                <HeaderStyle Width="120px" />
                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Amount">
                                <HeaderStyle Width="200px" />
                                <ItemStyle HorizontalAlign="Right" Width="200px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                            <RowStyle Font-Names="Calibri" Font-Size="14px" />
                        </asp:GridView>
                        
                        <div style="width: 100%; font-family: Calibri;">
                            <table style="width: 100%;">
                                <tr>
                                    <td style = "width: 5.9%;">
                                        <div style="width: 100%;">
                                        </div>    
                                    </td>
                                    <td style = "width: 38%;">
                                        <div style = "width: 100%;">
                                            <p style = "text-align: right; font-weight: bold;">Total :</p>
                                        </div>
                                    </td>
                                    <td style="width: 5%;">
                                        <div style="width:100%; text-align: center;">
                                            <asp:Label ID="lblTotCQTy" runat="server" style = "font-weight:bold; text-align:center;"></asp:Label>    
                                        </div>
                                    </td>
                                    <td style="width: 7%;">
                                        <div style="width:100%;">
                                        <div style="width:100%; text-align: center;">
                                        </div>
                                        </div>
                                    </td>
                                    <td style="width: 7%;">
                                        <div style="width:100%; text-align:right;">
                                            <asp:Label ID="lblTotQTy" runat="server" style="font-weight:bold; text-align:right;"></asp:Label>    
                                        </div>
                                    </td>
                                    <td style="width: 10%;">
                                        <div style="width:100%;">
                                        <div style="width:100%; text-align: center;">
                                        </div>
                                        </div>
                                    </td>
                                    <td style="width: 16%;">
                                        <div style="width:100%; text-align:right;">
                                            <asp:Label ID="lblTotAmount" runat="server" style="font-weight:bold; text-align:right;"></asp:Label>    
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    
                    <table style = "width:94%; margin: 1% 3% 0% 3%; font-family: Calibri;">
                        <tr>
                        <td class="style19">
                        
                            In Words :&nbsp;
                            <asp:Label ID="lblInWords" runat="server" style="font-family: Calibri"></asp:Label>
                        
                        </td>
                        <td>
                        
                        </td>
                        </tr>    
                    </table>
                    <table style = "width:98%; margin: 5% 1% 0% 1%; font-family: Calibri;">
                       <tr>
                            <td class="style26" style="text-align: right">
                                &nbsp;</td>
                            <td class="style27" style="text-align: center; border-top: 1px solid #CCCCCC;">
                                Prepared By</td>
                            <td class="style20" style="text-align: right">
                                &nbsp;</td>
                            <td class="style28" style="text-align: center; border-top: 1px solid #CCCCCC;">
                                Received By</td>
                            <td class="style20" style="text-align: center">
                                &nbsp;</td>
                            <td class="style29" style="text-align: center; border-top: 1px solid #CCCCCC;">
                                Checked By</td>
                            <td style="text-align: right" class="style20">
                                &nbsp;</td>
                            <td style="text-align: center; border-top: 1px solid #CCCCCC;" class="style25">
                                Authorized By</td>
                            <td style="text-align: center">
                                &nbsp;</td>
                       </tr> 
                    </table>
        </div>
    </div>
    </form>
</body>
</html>
