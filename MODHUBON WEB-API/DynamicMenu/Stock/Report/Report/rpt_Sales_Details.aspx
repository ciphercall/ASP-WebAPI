<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_Sales_Details.aspx.cs" Inherits="DynamicMenu.Stock.Report.Report.rpt_Sales_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#maincontectforcopyto").append($("#maincontectforcopy").html());
        });
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }

    </script>
    <style type="text/css">
        #main {
            width: 940px;
            /* to centre page on screen*/
            margin-left: auto;
            margin-right: auto;
        }

        .auto-style3 {
            width: 26px;
        }

        .auto-style4 {
            width: 980px;
        }

        .auto-style1 {
            width: 500px;
        }

        .SubTotalRowStyle {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
        }

        .SubTotalRowStyleAmount {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
        }

        .GrandTotalRowStyle {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 20px;
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

        .auto-style6 {
            width: 872px;
        }

        .auto-style7 {
            width: 582px;
        }

        .auto-style8 {
            width: 919px;
        }

        .auto-style9 {
            width: 30px;
        }

        .auto-style10 {
            height: 25px;
        }
        .auto-style11 {
            width: 1px;
            height: 25px;
        }
        .auto-style17 {
            width: 838px;
        }
        .auto-style18 {
            height: 25px;
            width: 838px;
        }
        
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
        </div>
        <div id="main">
            <table style="width: 100%; font-weight: bold">
                <tr>
                    <td style="width: 50%; text-align: center">
                        <div id="maincontectforcopy">
                            <table style="width: 100%;">
                                <tr style="width: 100%">
                                    <td class="auto-style3">&nbsp;</td>
                                    <td  colspan="8">
                                        <asp:Label ID="lblCompNM" runat="server"
                                            Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                                    </td>
                                    <td style="text-align: right" class="auto-style7"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style1" colspan="8">
                                        <asp:Label ID="lblAddress" runat="server"
                                            Style="font-family: Calibri; font-size:12px"></asp:Label>
                                    </td>
                                    <td style="text-align: right" class="auto-style7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                  
                                    <td style="text-align: center" class="auto-style7" colspan="8">বিল</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>
                                <%--  <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style5" style="text-align: left; font-size: 12px;">From Date</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="lblFromDate"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                </tr>--%>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style17" style="text-align: left; font-size: 12px;">তারিখ</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style8" style="text-align: left" colspan="2">
                                        <asp:Label runat="server" ID="lblToDate"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label>&nbsp;</td>




                                    <%--<td class="auto-style6" style="text-align: right; font-size: 12px;"></td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>--%>

                                       <td class="auto-style6" style="text-align: right; font-size: 12px;">বিল নং</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style8" style="text-align: left" colspan="2">
                                        <asp:Label runat="server" ID="lblMemoNo"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label>&nbsp;</td>





                                </tr>
                                <tr style="width: 100%">
                                    <td >&nbsp;</td>
                                    <td  style="text-align: left; font-size: 12px;" class="auto-style17">পরিবেশক</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td style="text-align: left" colspan="4" >
                                        <asp:Label runat="server" ID="lblParty" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>
                                    <%-- <td style="text-align: right" class="auto-style7">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>--%>
                                </tr>
                                    <tr style="width: 100%">
                                    <td >&nbsp;</td>
                                    <td  style="text-align: left; font-size: 12px;" class="auto-style17">ঠিকানা</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td style="text-align: left" colspan="4" >
                                        <asp:Label runat="server" ID="lblAddrs" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>
                                    <%-- <td style="text-align: right" class="auto-style7">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>--%>
                                </tr>
                                
                                  <tr>
                                    <td class="auto-style10" ></td>

                                    <td style="text-align: left; font-size: 12px;" class="auto-style18">গাড়ী নং</td>
                                    <td class="auto-style11"><strong>:</strong></td>
                                    <td style="text-align: left" colspan="2" class="auto-style8" >
                                        <asp:Label runat="server" ID="lblvehilceNO"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label>&nbsp;</td>


                                 <%--   <td class="auto-style6" style="text-align: left; font-size: 12px; display: none">Print Date
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 1px; display: none"><strong>:</strong></td>
                                    <td style="text-align: left" class="auto-style4">
                                        <asp:Label ID="lblTime" runat="server"
                                            Style="text-align: left; font-family: Calibri; font-size: 12px; display: none"></asp:Label>
                                    </td>--%>
                                      
                                    <td class="auto-style6" style="text-align: right; font-size: 12px; width: 100px">ফোন নং</td>
                                  <td class="auto-style9"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="lblMobno" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>


                                  
                                </tr>

                                <asp:Label runat="server" ID="Label1" Visible="False"/>
                                <tr>
                                    <td class="auto-style3">&nbsp;</td>
                                    <td class="auto-style9" style="text-align: left; font-size: 12px;">ড্রাইভার</td>
                                    <td style="width: 1px"><strong>:</strong></td>
                                    <td class="auto-style8" style="text-align: left" colspan="2">
                                        <asp:Label runat="server" ID="lblDriverNM" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>









                                    <%-- <td style="text-align: right" class="auto-style7">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>--%>
                                    <td class="auto-style6" style="text-align: right; font-size: 12px;">স্কট</td>
                                    <td class="auto-style9"><strong>:</strong></td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label runat="server" ID="lblasstNM" Style="text-align: left; font-family: Calibri; font-size: 12px;"></asp:Label></td>

                                </tr>
                              
                            </table>
                            <asp:Label runat="server" ID="lblPartyNm" Style="display: none"></asp:Label>
                            <div style="width: 96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
                            </div>
                            <div style="width: 96%; margin: 0% 2% 0% 2%;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="False"
                                    OnRowDataBound="GridView1_OnRowDataBound" OnRowCreated="GridView1_OnRowCreated" Width="100%" Font-Bold="True" Font-Size="20px">
                                    <Columns>
                                        <asp:BoundField HeaderText="ক্রম">
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                        </asp:BoundField>

                                        <asp:BoundField HeaderText="Item Name (English)" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="বিবরণ">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:BoundField>
                                        <%-- <asp:BoundField HeaderText="Remarks" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField HeaderText="পরিমান">
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="দর">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>

                                        <asp:BoundField HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                            <ItemStyle HorizontalAlign="Right" Width="3%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="মূল্য">
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                                </asp:GridView>

                                <table align="right" width="100%" style="font-size: 14px">
                                     <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">মোট বিক্রয় :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="0.00" ID="lbltotalS" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                    <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">যোগঃ প্রারম্ভিক জের :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="0.00" ID="lblopamnt" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                    <tr align="right">

                                        <td style="width: 75%; text-align: right; font-size: 12px;">যোগঃ পরিবহন ব্যয় :
                                        </td>

                                        <td>
                                            <asp:Label runat="server" Text="0.00" ID="lblcost" Style="text-align: right"></asp:Label></td>
                                    </tr>

                                    <tr align="right">


                                        <td style="width: 75%; text-align: right; font-size: 12px;">মোট প্রাপ্য :
                                        </td>
                                        <td><strong>
                                            <asp:Label runat="server" ID="lblgross" Text="0.00" Style="text-align: right"></asp:Label></strong></td>
                                    </tr>
                                </table>
                         
                              
                                <h5 align="left" style="font-weight: bold" >ফেরত পণ্যের বিবরণ :</h5>
                                <%--     2nd Grig --%>

                                <div style="width: 96%; margin: 0% 2% 0% 2%;">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="False"
                                        OnRowDataBound="GridView2_OnRowDataBound" Width="100%" Font-Bold="True" Font-Size="20px">
                                        <Columns>
                                            <asp:BoundField HeaderText="#SL">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Item Name (English)" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Item Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Remarks" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="Qty">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <%--        <asp:BoundField HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>--%>

                                            <asp:BoundField HeaderText="Rate">
                                                <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                                <ItemStyle HorizontalAlign="Right" Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                                    </asp:GridView>

                                </div>
                                
                                  <table align="right" width="100%" style="font-size: 14px">
                                      <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">বাদঃ মোট ফেরত :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblTotal" Text="0.00" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                    <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">বাদঃ আমদানী :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblrcvamnt" Text="0.00" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                    <tr align="right">

                                        <td style="width: 75%; text-align: right; font-size: 12px;">ক.  নেট প্রাপ্য (পণ্য বিক্রয় বাবদ) :
                                        </td>

                                        <td>
                                            <asp:Label runat="server" ID="lblnetamnt" Text="0.00" Style="text-align: right"></asp:Label></td>
                                    </tr>

                                   
                                </table>
                                                              

                                <h5 align="left" style="font-weight: bold">ফেরত যোগ্য পণ্য(সরবরাহ) :</h5>

                                <div style="width: 96%; margin: 0% 2% 0% 2%;">
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="False"
                                        OnRowDataBound="GridView3_OnRowDataBound" Width="100%" Font-Bold="True" Font-Size="20px">
                                        <Columns>
                                            <asp:BoundField HeaderText="#SL">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Item Name (English)" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Item Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Remarks" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="Qty">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <%--        <asp:BoundField HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>--%>

                                            <asp:BoundField HeaderText="Rate">
                                                <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                                <ItemStyle HorizontalAlign="Right" Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                                    </asp:GridView>

                                </div>
                                <table align="right" width="100%" style="font-size: 14px">
                                      <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">মোট সরবরাহ :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lbltotalOp" Text="0.00" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                    <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">যোগঃ প্রারম্ভিক জের :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblopAmountR" Text="0.00" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                 <%--   <tr align="right">

                                        <td style="width: 75%; text-align: right; font-size: 12px;">Net Receive :
                                        </td>

                                        <td>
                                            <asp:Label runat="server" ID="Label3" Style="text-align: right"></asp:Label></td>
                                    </tr>--%>

                                   
                                </table>
                            
                                <%--  END 3RD GRID--%>

                                <h5 align="left" style="font-weight: bold;padding-bottom: 0px">ফেরত যোগ্য পণ্য(ফেরত) :</h5>
                                <%--  START 4TH GRID--%>

                                <div style="width: 96%; margin: 0% 2% 0% 2%;">
                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="False"
                                        OnRowDataBound="GridView4_OnRowDataBound" Width="100%" Font-Bold="True" Font-Size="20px">
                                        <Columns>
                                            <asp:BoundField HeaderText="#SL">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Item Name (English)" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Item Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Remarks" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="Qty">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <%--        <asp:BoundField HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>--%>

                                            <asp:BoundField HeaderText="Rate">
                                                <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                                <ItemStyle HorizontalAlign="Right" Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                                    </asp:GridView>

                                </div>
                                <table align="right" width="100%" style="font-size: 14px">
                                    
                                     <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">মোট :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="0.00" ID="lblTo" Style="text-align: right"></asp:Label></td>
                                    </tr>

                                    <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">মোট ফেরত :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="0.00" ID="lblCLAmnt" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                 <%--   <tr align="right">

                                        <td style="width: 75%; text-align: right; font-size: 12px;">Net Receive :
                                        </td>

                                        <td>
                                            <asp:Label runat="server" ID="Label3" Style="text-align: right"></asp:Label></td>
                                    </tr>--%>

                                   
                                </table>
                     
                              
                                <%--START 5TH GRID--%>
                              <%--  <h4 align="left"></h4>--%>

                                <div style="width: 96%; margin: 0% 2% 0% 2%;">
                                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="False"
                                        OnRowDataBound="GridView5_OnRowDataBound" Width="100%" Font-Bold="True" Font-Size="20px">
                                        <Columns>
                                            <asp:BoundField HeaderText="#SL">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Item Name (English)" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Justify" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Item Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Remarks" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="Qty">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <%--        <asp:BoundField HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:BoundField>--%>

                                            <asp:BoundField HeaderText="Rate">
                                                <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                                <ItemStyle HorizontalAlign="Right" Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                                        <RowStyle Font-Names="Calibri" Font-Size="12px" />
                                    </asp:GridView>

                                </div>
                                
                                  <table align="right" width="100%" style="font-size: 14px">
                                      
                                      <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">খ.  নেট প্রাপ্য (ফেরত যোগ্য পণ্য) :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="0.00" ID="lblnetTotal" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                    <tr align="right">
                                        <td style="width: 75%; text-align: right; font-size: 12px;">নেট প্রাপ্য (ক+খ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" Text="0.00" ID="lblclAmountNT" Style="text-align: right"></asp:Label></td>
                                    </tr>
                                 <%--   <tr align="right">

                                        <td style="width: 75%; text-align: right; font-size: 12px;">Net Receive :
                                        </td>

                                        <td>
                                            <asp:Label runat="server" ID="Label3" Style="text-align: right"></asp:Label></td>
                                    </tr>--%>

                                   
                                </table>
                            </div>
                        </div>
                    </td>
                    <td style="width: 50%; text-align: center">
                        <div id="maincontectforcopyto">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
