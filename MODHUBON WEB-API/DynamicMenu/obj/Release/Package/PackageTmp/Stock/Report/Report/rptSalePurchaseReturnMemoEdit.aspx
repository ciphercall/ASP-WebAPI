<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptSalePurchaseReturnMemoEdit.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptSalePurchaseReturnMemoEdit" %>

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
            font-family: Calibri;
            font-size: 18pt;
            width: 637px;
        }
        .style3
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: small;
            width: 637px;
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
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: medium;
            width: 637px;
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
            font-family: Calibri;
        }
        
        .style11
        {
            width: 549px;
        }
        
        .style13
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: medium;
            width: 187px;
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
            font-family: Calibri;
        }
        
        .style20
        {
            width: 118px;
            font-family: Calibri;
        }
        
        .style25
        {
            width: 223px;
            font-family: Calibri;
        }
        .style26
        {
            width: 95px;
        }
        .style27
        {
            width: 135px;
            font-family: Calibri;
        }
        .style28
        {
            width: 144px;
            font-family: Calibri;
        }
        .style29
        {
            width: 155px;
            font-family: Calibri;
        }
        
        .style30
        {
            width: 420px;
        }
        
        .style33
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            font-size: small;
            width: 637px;
        }
        
        .style34
        {
            font-family: Calibri;
        }
        
        .footerCqty
        {
            padding-right: 30px;
            text-align:center;
            font-family:Calibri;
        }
        .style35
        {
            width: 102px;
            font-family: Calibri;
        }
        .style36
        {
            width: 182px;
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
                        <td style="text-align: center; font-size: 10pt; font-weight: 700" 
                            class="style4" rowspan="3">
                                <img src="../../../Images/logo.jpg" alt="logo" style="float:left; width:187px; height: 53px; margin-top: -18px;"/>
                                <table>
                                    <tr>
                                        <td class="style36" style="text-align: center;">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        <td style="text-align: center; font-weight: 700" 
                            class="style2">
                    <asp:Label ID="lblCompNM" runat="server" 
                                style="font-family: Calibri; font-size: 25px; font-weight: 700"></asp:Label>
                        </td>
                        <td style="text-align: center; font-size: x-large; font-weight: 700" 
                            class="style14">
                        <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                    </tr>
                    <tr>
                        <td class="style33">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 11px"></asp:Label>
                        </td>
                        <td style="text-align: center" class="style14">
                            &nbsp;</td>
                    </tr>
                    <tr>
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
                        
                            
                            
                            style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            <asp:Label ID="lblType" runat="server" 
                                style="font-family: Calibri; font-weight: 700"></asp:Label>
                            <strong style="font-family: Calibri">&nbsp;RETURN MEMO</strong></td>
                        <td class="style15" 
                        
                            
                            
                            
                            
                            style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                    <asp:Label ID="lblTime" runat="server" 
                                
                                style="text-align: center; font-family: Calibri; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style = "width:98%; margin: 0% 1% 0% 1%; height: 1px; background: #000000;">
                </div>
              </div>
              <table style="width:100%; margin-top: 5px;">
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td class="style35">
                                Date</td>
                            <td class="style8">
                                :</td>
                            <td class="style11">
                                <asp:Label ID="lblInVDT" runat="server" 
                                    style="font-family: Calibri; font-size: medium"></asp:Label>
                            </td>
                            <td class="style10">
                                Invoice No <strong>:&nbsp; </strong>
                                <asp:Label ID="lblInVNo" runat="server" 
                                    style="font-family: Calibri; font-size: medium"></asp:Label>
                            </td>
                            <td style="text-align: right" class="style26">
                                &nbsp;</td>
                            <td style="text-align: right; font-family: Calibri;" class="style30">
                                Sales Memo No <strong>:</strong>
                                <asp:Label ID="lblSalesMemoNo" runat="server" 
                                    style="font-family: Calibri; font-size: medium"></asp:Label>
&nbsp; </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td class="style35">
                                <asp:Label ID="lblMode" runat="server"></asp:Label>
                            </td>
                            <td class="style8">
                                :</td>
                            <td class="style11">
                                <asp:Label ID="lblSalesTo" runat="server" 
                                    style="font-weight: 700; font-family: Calibri; font-size: medium;"></asp:Label>
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
                            <td class="style35">
                                Address</td>
                            <td class="style8">
                                :</td>
                            <td class="style11">
                                <asp:Label ID="lblSaleToAdd" runat="server" 
                                    style="font-family: Calibri; font-size: 10px"></asp:Label>
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
                            onrowdatabound="GridView1_RowDataBound" Width="100%" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField DataField="SL" HeaderText="SL">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEMNM" HeaderText="Item Particulars" >
                                <HeaderStyle Width="25%" />
                                <ItemStyle Width="25%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Carton Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCarton" runat="server" Text='<%#Eval("CQTY") %>'></asp:Label>
                                        &nbsp;<asp:Label ID="lblCross" runat="server" Text="X"></asp:Label>
                                        &nbsp;<asp:Label ID="lblCartonQty" runat="server" Text='<%#Eval("CPQTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="15%" Wrap="True" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="QTY" HeaderText="Qty">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Rate">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Amount">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Discount Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Net Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                            <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                            <RowStyle Font-Names="Calibri" Font-Size="14px" />
                        </asp:GridView>
                        
                        <div style=" margin-top:1%; width: 100%; font-family: Calibri;">
                            <table style="width: 100%;">
                                <tr>
                                    <td style = "width: 5.9%;">
                                        <div style="width: 100%;">
                                        </div>    
                                    </td>
                                    <td style = "width: 38%;">
                                        <div style = "width: 100%;">
                                        </div>
                                    </td>
                                    <td style="width: 5%;">
                                        <div style="width:100%; text-align: center;">
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
                                        </div>
                                    </td>
                                    <td style="width: 23%;">
                                        <div style="width:100%;">
                                        <div style="width:100%; text-align: right; font-weight: bold; font-family: Calibri;">
                                            Total Discount :
                                        </div>
                                        </div>
                                    </td>
                                    <td style="width: 16%;">
                                        <div style="width:100%; text-align:right;">
                                            <asp:Label ID="lblGrossDiscount" runat="server" 
                                                style = "font-weight:bold; text-align:center;"></asp:Label>    
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <table style="width: 100%;">
                                <tr>
                                    <td style = "width: 5.9%;">
                                        <div style="width: 100%;">
                                        </div>    
                                    </td>
                                    <td style = "width: 38%;">
                                        <div style = "width: 100%;">
                                            <p style = "text-align: right; font-weight: bold;">&nbsp;</p>
                                        </div>
                                    </td>
                                    <td style="width: 5%;">
                                        <div style="width:100%; text-align: center;">
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
                                        </div>
                                    </td>
                                    <td style="width: 23%;">
                                        <div style="width:100%;">
                                        <div style="width:100%; text-align: right; font-weight: bold; font-family: Calibri;">
                                            Total Amount : 
                                        </div>
                                        </div>
                                    </td>
                                    <td style="width: 16%;">
                                        <div style="width:100%; text-align:right;">
                                            <asp:Label ID="lblNetAmount" runat="server" 
                                                style="font-weight:bold; text-align:right;"></asp:Label>    
                                        </div>
                                    </td>
                                </tr>
                            </table>

                        </div>

                    </div>
                   
                    <table style = "width:94%; margin: 1% 3% 0% 3%;">
                        <tr>
                        <td class="style19">
                        
                            In Words :&nbsp;
                            <asp:Label ID="lblInWords" runat="server"></asp:Label>
                        
                        </td>
                        <td>
                        
                        </td>
                        </tr>    
                    </table>
                    <table style = "width:98%; margin: 5% 1% 0% 1%;">
                       <tr>
                            <td class="style34" style="text-align: right">
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
                            <td style="text-align: center" class="style19">
                                &nbsp;</td>
                       </tr> 
                        <%--<tr>
                            <td class="style34" style="text-align: right">
                                &nbsp;</td>
                            <td class="style27">
                                &nbsp;</td>
                            <td class="style20" style="text-align: right">
                                &nbsp;</td>
                            <td class="style28">
                                &nbsp;</td>
                            <td class="style20" style="text-align: center">
                                &nbsp;</td>
                            <td class="style29">
                                &nbsp;</td>
                            <td style="text-align: right" class="style20">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: center" class="style19">
                                &nbsp;</td>
                       </tr> 
                       <tr>
                            <td class="style34" style="text-align: right">
                                &nbsp;</td>
                            <td class="style27">
                                &nbsp;</td>
                            <td class="style20" style="text-align: right">
                                &nbsp;</td>
                            <td class="style28">
                                &nbsp;</td>
                            <td class="style20" style="text-align: center">
                                &nbsp;</td>
                            <td class="style29">
                                &nbsp;</td>
                            <td style="text-align: right" class="style20">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: center" class="style19">
                                &nbsp;</td>
                       </tr> 
                       <tr>
                            <td class="style34" style="text-align: left;" colspan="9">
                                <p style="text-align: left; margin-left: 10px; " class="style37">
                                    
                                    ** N.B: GOODS ONCE SOLD WILL NOT BE RETURNED OR EXCHANGED.</p>
                            </td>
                       </tr> --%>
                       </table>
        &nbsp;&nbsp;&nbsp;
        </div>
    </div>
    </form>
    <p>
&nbsp;&nbsp;&nbsp;
    </p>
</body>
</html>
