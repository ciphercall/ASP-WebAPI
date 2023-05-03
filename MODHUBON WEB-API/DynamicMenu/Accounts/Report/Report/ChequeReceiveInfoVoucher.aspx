<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChequeReceiveInfoVoucher.aspx.cs" Inherits="DynamicMenu.Accounts.Report.Report.ChequeReceiveInfoVoucher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />


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
        #main {
            width: 620px;
            border: 2px solid #000;
            /* to centre page on screen*/
            margin-left: auto;
            margin-right: auto;
        }

        #btnPrint {
            font-weight: 700;
            font-style: italic;
        }

        .style1 {
            font-size: smaller;
        }

        .style2 {
            width: 862px;
        }

        .style3 {
            font-size: 11px;
            width: 862px;
        }

        .style12 {
            width: 2px;
            font-weight: bold;
        }

        .style14 {
            width: 529px;
        }

        .style15 {
            width: 1051px;
        }

        .style16 {
            font-size: 18px;
            width: 182px;
            font-family: Calibri;
        }

        .style18 {
            width: 182px;
        }

        .style19 {
            width: 280px;
            font-family: Calibri;
        }

        .style22 {
            width: 5px;
            font-size: medium;
        }

        .style23 {
            width: 5px;
        }

        .style24 {
            width: 275px;
        }

        .style25 {
            width: 188px;
        }

        .style26 {
            width: 3px;
            font-weight: bold;
        }

        .style27 {
            width: 381px;
        }

        .style28 {
            width: 221px;
        }

        .style29 {
            width: 3px;
        }

        .style30 {
            width: 3px;
            font-size: medium;
        }

        .style31 {
            width: 180px;
            text-align: right;
        }

        .style32 {
            height: 12px;
        }

        .style33 {
            width: 105px;
        }

        .style34 {
            width: 1px;
        }

        .style38 {
            width: 54px;
        }

        .style39 {
            width: 277px;
            text-align: center;
        }

        .style42 {
            width: 283px;
            text-align: center;
        }

        .style43 {
            width: 296px;
            text-align: center;
        }

        .style44 {
            width: 297px;
            text-align: center;
        }

        .style46 {
            font-family: Calibri;
        }

        .style47 {
            width: 1051px;
            font-family: Calibri;
        }

        .style48 {
            font-family: Calibri;
        }

        .style50 {
            width: 211px;
            font-family: Calibri;
        }

        .style52 {
            font-family: Calibri;
        }

        .style53 {
            width: 572px;
        }

        .style55 {
            width: 300px;
        }

        .style56 {
            width: 480px;
            height: 19px;
        }

        .style58 {
            height: 19px;
        }

        .style59 {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 186px;
        }

        .style60 {
            text-align: right;
            font-family: Calibri;
            width: 186px;
        }

        .style61 {
            font-family: Calibri;
        }

        .style62 {
            width: 186px;
        }

        .style63 {
            text-align: right;
            font-family: Calibri;
            width: 2px;
        }

        .style64 {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 2px;
        }

        .style65 {
            width: 2px;
        }

        .style66 {
            width: 480px;
        }

        @font-face {
            font-family: "Dodgv2";
            src: url("../../../MenuCssJs/fonts/Dodgv2.ttf")format("truetype");
            font-weight: normal;
            font-style: normal;
        }

        .auto-style1 {
            width: 97px;
        }

        .auto-style3 {
            width: 42%;
        }

        .auto-style4 {
            width: 8px;
        }

        .auto-style5 {
            width: 148px;
        }

        .auto-style6 {
            width: 95%;
        }

        .auto-style7 {
            width: 98%;
            height: 30px;
        }

        .auto-style8 {
            width: 37%;
        }

        .auto-style9 {
            width: 122px;
        }

        .auto-style10 {
            width: 18px;
        }

        .auto-style11 {
            width: 4px;
        }
        .auto-style12 {
            width: 24%;
            height: 26px;
        }
        .auto-style13 {
            height: 26px;
        }
        .auto-style14 {
            width: 75%;
            height: 26px;
        }
        .auto-style15 {
            width: 100%;
        }
        .auto-style16 {
            width: 321px;
            font-family: Calibri;
            height: 20px;
        }
        .auto-style17 {
            width: 5px;
            font-size: medium;
            height: 20px;
        }
        .auto-style18 {
            width: 275px;
            height: 20px;
        }
        .auto-style19 {
            width: 242px;
            height: 20px;
        }
        .auto-style20 {
            width: 3px;
            font-weight: bold;
            height: 20px;
        }
        .auto-style21 {
            width: 381px;
            height: 20px;
        }
        .auto-style22 {
            width: 242px;
        }
        .auto-style23 {
            width: 321px;
            font-family: Calibri;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
<div id="main">
            <div>
                <table style="width: 100%; border-bottom: 1px solid #000">
                    <tr>
                        <td style="width: 15%">
                            <div style="width: 120px; height: 80px;">
                                <img src="/Images/logo.png" width="100%" alt="logo" style="height: 77px" />
                            </div>
                        </td>

                        <td style="width: 70%">
                            <table style="width: 100%; text-align: center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCompNM" runat="server" Style="font-family: fantasy; font-size: 30px; letter-spacing: 3px; text-transform: uppercase;">SHODESH CHEMICALS</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10px; font-family: Calibri">Saima Vandar Market (3rd Floor).309 SK Mujib Road,(Opposite to Fire Service),CHittagong.
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td style="font-size: 10px; font-family: Calibri">Contact    	: Mobile: 01730735716, E-mail: salahuddin@shodesh-chemicals.com
                                    </td>--%>
                                </tr>
                            </table>
                        </td>

                        <td style="width: 15%; font-size: 11px; text-align: right">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
                        </td>
                    </tr>
                </table>

            </div>

            <div>

                <table style="width: 100%; font-size: 12px; font-family: Calibri">
                    <tr>
                        <td style="width: 35%">
                            <table>
                                <tr>
                                    <td class="auto-style3">Print Time</td>
                                    <td style="text-align: right" class="auto-style10">
                                        <strong>&nbsp; :</strong></td>
                                    <td class="auto-style9">

                                        <asp:Label ID="lblPrintTime" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">Entry Time</td>
                                    <td style="text-align: right;" class="auto-style10">
                                        <strong>:</strong></td>
                                    <td class="auto-style9">
                                        <asp:Label ID="lblInTime" runat="server"></asp:Label>

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="auto-style8">
                            <div class="auto-style7">
                                <table class="auto-style6">

                                    <tr>
                                        <td colspan="3" style="font-weight: 700; font-size: 17px; text-align: center; border: 2px solid #000">
                                            <asp:Label ID="lblVtype" runat="server" Style="font-family: Calibri">CHEQUE RECEIVE VOUCHER</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width: 30%">
                            <table style="width: 100%; font-size: 12px">

                                <tr>
                                    <td class="auto-style5">Voucher No</td>
                                    <td class="auto-style4">:</td>
                                    <td class="auto-style1">

                                        <asp:Label ID="lblVNo" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">Voucher Date</td>
                                    <td class="auto-style4">:</td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lblTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>

                <table style="width: 100%; font-size: 12px; font-family: Calibri">
                 <tr>
                        <td style="width: 70%">
                            <table style="width: 100%; font-size: 12px; font-family: Calibri">
                                <tr>

                                    <td style="width: 24%; text-align: left">
                                        <asp:Label ID="lblReceiveCrBy" runat="server">Receive To</asp:Label>
                                    </td>
                                    <td style="text-align: center"><strong>:</strong></td>
                                    <td style="width: 75%; text-align: left">
                                        <asp:Label ID="lblrcvto" runat="server" CssClass="style52"></asp:Label>
                                        <asp:Label ID="lblMidDate" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            <%--       <tr style="display: none">
                                    <td style="text-align: left" class="auto-style12">
                                        <asp:Label ID="lblReceiveCrFrom" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: center" class="auto-style13"><strong>:</strong></td>
                                    <td style="text-align: left" class="auto-style14">
                                        <asp:Label runat="server" style="display: none" id=lblamntspell></asp:Label>
                                        <asp:Label ID="lblReceivedFrom" runat="server" CssClass="style52"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Style="font-size: large; font-family: Calibri;" Visible="False"></asp:Label>
                                    </td>

                                </tr>--%>
                                
                                  <tr>
                                    <td style="text-align: left" class="auto-style12">
                                        <asp:Label ID="Label2" runat="server">Receive From</asp:Label>
                                    </td>
                                    <td style="text-align: center" class="auto-style13"><strong>:</strong></td>
                                    <td style="text-align: left" class="auto-style14">
                                        <asp:Label ID="lblrcvfrm" runat="server" CssClass="style52"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Style="font-size: large; font-family: Calibri;" Visible="False"></asp:Label>
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <td style="width: 30%; font-family: Calibri;">
                            <table style="width: 100%; font-size: 12px">
                                <tr>
                                    <td class="auto-style11">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblEntryUserName" runat="server" Style="display: none"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%; font-family: Calibri; border: 1px solid #000000">
                                <tr>
                                    <td style="text-align: center; font-weight: 700;"
                                        class="style16">Particulars</td>
                                    <td style="border-left: 1px solid #000000; text-align: center; font-weight: 700;"
                                        class="style16">Amount (Tk.)</td>
                                </tr>
                                <tr style="">
                                    <td style="border-top: 1px solid #000000; height: 40px; width: 80%">
                                        <asp:Label ID="lblParticulars" runat="server"></asp:Label>
                                    </td>
                                    <td style="border-top: 1px solid #000000; border-left: 1px solid #000000; text-align: right; height: 40px; width: 20%">
                                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <table style="font-family: Calibri;" class="auto-style15">
                                <tr>
                                    <td class="auto-style23">
                                        Transaction Mode</td>
                                    <td class="style22">
                                        <strong>:</strong></td>
                                    <td class="style24">
                                        <asp:Label ID="lblRMode" runat="server"
                                            CssClass="style52"></asp:Label>
                                    </td>
                                    <td class="auto-style22">
                                        Transaction For</td>
                                    <td class="style26">
                                        <asp:Label ID="lblTransforSC" runat="server" Text=":"></asp:Label></td>
                                    <td class="style27">
                                        <asp:Label ID="lblTransFor" runat="server"></asp:Label></td>
                                    <td class="style28">&nbsp;</td>
                                    <td class="style29">&nbsp;</td>
                                    <td class="style31">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Cheque No</td>
                                    <td class="auto-style17">
                                        <strong>:</strong></td>
                                    <td class="auto-style18">
                                        <asp:Label ID="lblChequeNo" runat="server"
                                            CssClass="style52"></asp:Label>
                                    </td>
                                    <td class="auto-style19" style="font-family: Calibri;">Cheque Date</td>
                                    <td class="auto-style20">:</td>
                                    <td class="auto-style21">
                                        <asp:Label ID="lblChequeDT" runat="server"
                                            CssClass="style46"></asp:Label>
                                    </td>
                                   <%-- <td class="style28"
                                        style="font-size: 15px; font-weight: 600; text-align: right; font-family: Calibri;">Total (Tk.)</td>
                                    <td class="style30">
                                        <strong>:</strong></td>
                                    <td class="style31">
                                        <asp:Label ID="lblTotAmount" runat="server"
                                            Style="font-size: 15px; text-align: right; font-weight: 600;"
                                            CssClass="style46"></asp:Label>
                                    </td>--%>
                                </tr>
                          <%--         <tr>
                                    <td class="style33">Remarks</td>
                                    <td class="style34">
                                        <strong>:</strong></td>
                                    <td>
                             
                                        <div style="width: 60%; font-family: Calibri;">
                                            <asp:Label Style="font-family: Calibri;" ID="lblRemarks" runat="server"></asp:Label>
                                           
                                        </div>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>

                <table width="100%">
                    <tr>
                        <td>
                            <table style="width: 100%; font-family: Calibri;">
                                                            

                                <tr>
                                    <td class="style33">In Words</td>
                                    <td class="style34">
                                        <strong>:</strong></td>
                                    <td>
                                        <%-- <div style="border-bottom: 1px solid #000000; width: 60%; font-family: Calibri;">--%>
                                        <div style="width: 60%; font-family: Calibri;">
                                            <asp:Label Style="font-family: Calibri;" ID="lblInWords" runat="server"></asp:Label>
                                            <%--  <div style="border-bottom: 1px solid #000000; width: 100%;"></div>--%>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="style33" style="font-size: medium">&nbsp;</td>
                                    <td class="style34" style="font-size: medium">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </table>

            </div>
            <div style="width: 100%">
                <table style="width: 100%; font-family: Calibri; font-size: 13px">
                    <tr>
                        <td style="width: 25%; text-align: center">&nbsp;</td>
                        <td style="width: 25%; text-align: center">
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                        </td>
                        <td style="width: 25%; text-align: center">&nbsp;</td>
                        <td style="width: 25%; text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 25%; text-align: left"><span style="border-top: 1px solid #000000">Received By</span></td>
                        <td style="width: 25%; text-align: center"><span style="border-top: 1px solid #000000">Prepared By</span></td>
                        <td style="width: 25%; text-align: center"><span style="border-top: 1px solid #000000">Accounts</span></td>
                        <td style="width: 25%; text-align: right"><span style="border-top: 1px solid #000000">Approved By</span></td>
                    </tr>
                </table>
                <%-- <table style="width: 100%; font-family: Calibri; font-size: 13px">
                    <tr>
                        <td style="width: 33%; text-align: center">&nbsp;</td>
                        <td style="width: 34%; text-align: center">&nbsp;</td>
                        <td style="width: 33%; text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 33%; text-align: center">Prepared By</td>
                        <td style="width: 34%; text-align: center">Received By</td>
                        <td style="width: 33%; text-align: center">Authorized By</td>
                    </tr>
                </table>--%>
            </div>
        </div>
    </form>
</body>
</html>
