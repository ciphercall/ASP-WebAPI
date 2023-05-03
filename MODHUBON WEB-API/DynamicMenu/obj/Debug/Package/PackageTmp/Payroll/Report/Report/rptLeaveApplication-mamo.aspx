<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptLeaveApplication-mamo.aspx.cs" Inherits="DynamicMenu.Payroll.Report.Report.rptLeaveApplication_mamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>



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

    <style type="text/css">
        #btnPrint {
            font-weight: 700;
        }

        .style2 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: xx-small;
            width: 1050px;
        }

        .style3 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: small;
            width: 1050px;
        }

        .style4 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            width: 98px;
        }

        .style5 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            font-size: medium;
            width: 1050px;
        }

        .style6 {
            width: 4px;
        }

        .style8 {
            width: 1px;
            font-weight: bold;
        }

        .style10 {
            width: 210px;
        }

        .style11 {
            width: 472px;
        }

        .style13 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: medium;
            width: 98px;
        }

        .style14 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: xx-small;
            width: 140px;
        }

        .style15 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: "Helvetica Neue", Arial, Helvetica, sans-serif;
            font-size: small;
            width: 140px;
        }

        .style19 {
            font-family: Calibri;
        }

        .style20 {
            width: 118px;
        }

        .style25 {
            width: 223px;
            font-family: Arial, Helvetica, sans-serif;
        }

        .style26 {
            width: 95px;
        }

        .style27 {
            width: 135px;
            font-family: Arial, Helvetica, sans-serif;
        }

        .style28 {
            width: 144px;
            font-family: Arial, Helvetica, sans-serif;
        }

        .style29 {
            width: 155px;
            font-family: Arial, Helvetica, sans-serif;
        }

        .style30 {
            width: 420px;
        }

        .style31 {
            width: 152px;
        }

        .style33 {
            text-align: center;
            color: rgb(34, 34, 34);
            font-family: Calibri;
            font-size: small;
            width: 1050px;
        }

        .style34 {
            font-family: Calibri;
            font-size: medium;
        }

        .auto-style2 {
            height: 23px;
        }

        .auto-style8 {
            width: 4px;
            height: 23px;
        }

        .auto-style9 {
            width: 152px;
            height: 23px;
        }

        .auto-style10 {
            width: 1px;
            font-weight: bold;
            height: 23px;
        }

        .auto-style11 {
            width: 276px;
            height: 23px;
        }

        .auto-style12 {
            width: 210px;
            height: 23px;
        }

        #main {
            width: 620px;
            /* to centre page on screen*/
            margin-left: auto;
            margin-right: auto;
            border: 1px solid #000000;
            padding: 5px;
        }
        .auto-style13 {
            width: 276px;
        }
        .auto-style14 {
            width: 330px;
        }
    </style>

</head>
<body style="font-size: medium">
    <form id="form1" runat="server">
          <div id="main">
            <div>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; font-size: xx-large; font-weight: 700"
                                class="style4" rowspan="5">
                                <%--  <img src="../../../Images/logo.png" alt="logo" style="float: left; width: 187px; height: 100px; margin-top: -18px;" />--%></td>
                            <td style="text-align: center; font-size: x-large; font-weight: 700"
                                class="style2">
                                <asp:Label ID="lblCompNM" runat="server"
                                    Style="font-family: Calibri; font-size: 25px; font-weight: 700"></asp:Label>
                            </td>
                            <td style="text-align: center; font-size: x-large; font-weight: 700"
                                class="style14">
                                <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                        </tr>
                        <tr>
                            <td class="style33">
                                <asp:Label ID="lblAddress" runat="server"
                                    Style="font-family: Calibri; font-size: 11px"></asp:Label>
                            </td>
                            <td style="text-align: center" class="style14">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style33"
                                style="text-align: center;">TELEPHONE :
                    <asp:Label ID="lblContact" runat="server"
                        Style="font-family: Calibri; font-size: 11px"></asp:Label>
                            </td>
                            <td class="style15"
                                style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3"
                                style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">&nbsp;</td>
                            <td class="style15"
                                style="text-align: right; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style5"
                                style="text-align: center;">
                                <strong>LEAVE APPLICATION</strong></td>
                            <td class="style15"
                                style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            </td>
                        </tr>
                    </table>
                    <div style="width: 98%; margin: 0% 1% 0% 1%; height: 1px; background: #000000;">
                    </div>
                </div>
                <table style="width: 100%; margin-top: 5px; font-family: Calibri; font-size: 12px">
                    <tr align="right">
                        <td class="style6">&nbsp;</td>
                        <td class="style31">&nbsp;</td>
                        <td class="style8">&nbsp;</td>
                        <td class="auto-style13">
                            Serial No <strong>:&nbsp; </strong>
                            <asp:Label ID="lblInVNo" runat="server"
                                Style="font-family: Calibri; font-size: 12px"></asp:Label>
                        </td>
                        <td class="style10">&nbsp;</td>
                       
                    </tr>
                    <tr>
                        <td class="auto-style8"></td>
                        <td class="auto-style9">Date</td>
                        <td class="auto-style10">:</td>
                        <td class="auto-style11">
                            <asp:Label ID="lblInVDT0" runat="server" CssClass="style34"></asp:Label>
                        </td>
                        <%--<td class="auto-style14"> &nbsp;</td>
                        <td style="text-align: right" class="auto-style2">
                          Entry By : 
                            <asp:Label ID="lblEntryBy" runat="server"
                                    Style="text-align: center; font-family: Calibri; font-size: 10px;"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="style6">&nbsp;</td>
                        <td class="style31">Employee Name&nbsp;&nbsp;</td>
                        <td class="style8">:</td>
                        <td class="auto-style13">
                            <asp:Label ID="lblEMNM" runat="server" Style="font-weight: 700;" ></asp:Label>
                        </td>
                        
                        <%--<td class="style10">&nbsp;</td>
                        <td class="auto-style14" style="text-align: right">
                            Entry Date : 
                            <asp:Label ID="lblEntryTime" runat="server"
                                    Style="text-align: center; font-family: Calibri; font-size: 10px;"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="style6">&nbsp;</td>
                        <td class="style31">Month/Year</td>
                        <td class="style8">:</td>
                        <td class="auto-style13">
                            <asp:Label ID="lblMY" runat="server"></asp:Label>
                        </td>
                        <td class="style10">&nbsp;</td>
                        <td class="auto-style14" style="text-align: right">  Print By :   
                            <asp:Label ID="lblTime" runat="server"
                                    Style="text-align: center; font-family: Calibri; font-size: 10px;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">&nbsp;</td>
                        <td class="style31">Remarks</td>
                        <td class="style8">:</td>
                        <td class="auto-style13">
                            <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                        </td>
                        
                    </tr>
                    
                </table>
                <div style="width: 98%; margin: 1% 1% 0% 1%;">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" Width="100%" ShowFooter="False"
                            ShowHeaderWhenEmpty="True">
                            <Columns>
                        <asp:BoundField HeaderText="Leave Name">
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Leave From">
                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Leave To">
                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Leave Days">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                                <asp:BoundField HeaderText="Reason">
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                            <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                            <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                            <RowStyle Font-Names="Calibri" Font-Size="14px" />
                        </asp:GridView>

                </div>
                
                <table style="width: 98%; margin: 8% 1% 0% 1%; font-family: Calibri; font-size: 12px">
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: center; border-top: 1px solid #CCCCCC;">Received By</td>
                        <td style="text-align: center">&nbsp;</td>
                        <td style="text-align: center; border-top: 1px solid #CCCCCC;">Checked By</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: center; border-top: 1px solid #CCCCCC;">Authorized By</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: center; border-top: 1px solid #CCCCCC;">Prepared By</td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
