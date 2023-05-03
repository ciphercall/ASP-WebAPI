<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptDeliveryOrderEdit.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptDeliveryOrderEdit" %>

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
            width: 751px;
        }
        .style3
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: small;
            width: 751px;
        }
        .style4
        {
        text-align: center;
        color: rgb(34, 34, 34);
        font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
        width: 187px;
    }
        .style5
        {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: medium;
            width: 751px;
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
        width: 530px;
    }
        
        .style13
        {
        text-align: center;
        color: rgb(34, 34, 34);
        font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
        font-size: 10pt;
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
        
        .style20
        {
            width: 118px;
        }
        
        .style25
        {
            width: 223px;
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
        width: 114px;
        font-family: Calibri;
    }
        .style34
    {
        text-align: center;
        color: rgb(34, 34, 34);
        font-family: Calibri;
        font-size: small;
        width: 751px;
    }
        
    .style35
    {
        width: 186px;
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
                            class="style4" rowspan="3">
                                <img src="../../../Images/logo.jpg" alt="logo" style="float:left; width:187px; height: 53px;"/>
                            <table>
                                    <tr>
                                        <td class="style35" style="font-family: Calibri; font-size: 10pt">
                                            FOR NEXT GENERATION</td>
                                    </tr>
                                </table>
                            </td>
                        <td style="text-align: center; font-size: 18pt; font-weight: 700" 
                            class="style2">
                    <asp:Label ID="lblCompNM" runat="server" 
                                style="font-family: Calibri; font-size: 25px; font-weight: 700"></asp:Label>
                        </td>
                        <td style="text-align: center; font-size: x-large; font-weight: 700" 
                            class="style14">
                        <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                    </tr>
                    <tr>
                        <td class="style34">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 11px"></asp:Label>
                        </td>
                        <td style="text-align: center" class="style14">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style34" 
                        
                            
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
                        
                            
                            style="text-align: center; font-weight: 700; font-family: Calibri">
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
                            <strong style="font-family: Calibri">DELIVERY ORDER</strong></td>
                        <td class="style15" 
                        
                            
                            
                            
                            
                            style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                    <asp:Label ID="lblTime" runat="server" 
                                
                                style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: small;"></asp:Label>
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
                            <td class="style31">
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
                            <td class="style31">
                                Sales To </td>
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
                            <td class="style31">
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
                            onrowdatabound="GridView1_RowDataBound" Width="100%" ShowFooter="True" Font-Bold="True" Font-Size="20px"
                            ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField HeaderText="SL">
                                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Item Particulars" >
                                <HeaderStyle Width="50%" HorizontalAlign="Center" />
                                <ItemStyle Width="50%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Carton Qty">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Pieces">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Total">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                            <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                            <RowStyle Font-Names="Calibri" Font-Size="14px" />
                        </asp:GridView>
                        
                    </div>
                   
                    <table style = "width:98%; margin: 10% 1% 0% 1%; font-family: Calibri;">
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
