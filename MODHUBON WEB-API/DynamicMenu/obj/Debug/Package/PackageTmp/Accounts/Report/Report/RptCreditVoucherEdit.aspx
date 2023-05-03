﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptCreditVoucherEdit.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.RptCreditVoucherEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        .auto-style3 {
            width: 95%;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style5 {
            width: 102%;
            height: 30px;
        }
        .auto-style6 {
            width: 40%;
        }
        .auto-style7 {
            width: 18%;
        }
    </style>
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
        <div id="main">
            <div>
                <table style="width: 100%; border-bottom: 1px solid #000">
                    <tr>
                        <td style="width: 15%">
                            <div style="width: 120px; height: 80px;">
                                <img src="../../../Images/logo.png" width="100%" alt="logo" style="height: 78px" />
                            </div>
                        </td>

                        <td style="width: 70%">
                            <table style="width: 100%; text-align: center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCompNM" runat="server" Style="font-family: fantasy; font-size: 33px; letter-spacing: 3px; text-transform: uppercase;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10px; font-family: Calibri"><asp:Label ID="lblAddress" runat="server" ></asp:Label>
                          </td>
                                </tr>
                                <tr>
                                    <%-- <td style="font-size: 10px; font-family: Calibri">Contact    	: Mobile: 01730735716, E-mail: salahuddin@shodesh-chemicals.com
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
                        <td  style="width: 33%">
                            <table style="font-size: 12px" class="auto-style4">
                                <tr>
                                    <td class="auto-style6">Print Time</td>
                                    <td style="text-align: left">
                                        <strong>&nbsp; :</strong></td>
                                    <td style="text-align: right">

                                        <asp:Label ID="lblPrintTime" runat="server" ></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6"><span style="color: rgb(0, 0, 0); font-family: Calibri; font-size: 12px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Entry Time</span></td>
                                    <td style="text-align: left;">
                                        <strong>&nbsp; :</strong></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblInTime" runat="server" ></asp:Label>

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%">
                            <div class="auto-style5">
                                <table class="auto-style3">
                                    <tr>
                                        <td colspan="3" style="font-weight: 700; font-size: 18px; text-align: center; border: 2px solid #000">
                                            <asp:Label ID="lblVtype" runat="server" Style="font-family: Calibri"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 100%; font-size: 12px">
                                <tr>
                                    <td>Voucher No</td>
                                    <td>:</td>
                                    <td>

                                        <asp:Label ID="lblVNo" runat="server" CssClass="style61"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Voucher Date</td>
                                    <td>:</td>
                                    <td>
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

                                    <td style="text-align: left" class="auto-style7">
                                        <asp:Label ID="lblReceiveCrBy" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 1%; text-align: center"><strong>:</strong></td>
                                    <td style="width: 75%; text-align: left">
                                        <asp:Label ID="lblReceivedBy" runat="server"></asp:Label>
                                        <asp:Label ID="lblMidDate" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="auto-style7">
                                        <asp:Label ID="lblReceiveCrFrom" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 1%; text-align: center"><strong>:</strong></td>
                                    <td style="width: 75%; text-align: left">
                                        <asp:Label ID="lblReceivedFrom" runat="server"></asp:Label>
                                        <asp:Label ID="lblAmount" runat="server" Visible="False"></asp:Label>
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <td style="width: 30%; font-family: Calibri;">
                            <table style="width: 100%; font-size: 12px">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style29">&nbsp;</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style29">&nbsp;</td>
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
                                        <asp:Label ID="lblAmountComma" runat="server"></asp:Label>
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
                            <table style="width: 100%; font-family: Calibri;">
                                <tr>
                                    <td class="style19">
                                        <asp:Label ID="lblReceiveMode" runat="server"></asp:Label>
                                        Mode
                                    </td>
                                    <td class="style22">
                                        <strong>:</strong></td>
                                    <td class="style24">
                                        <asp:Label ID="lblRMode" runat="server"
                                            CssClass="style52"></asp:Label>
                                    </td>
                                    <td class="style25">
                                        <asp:Label ID="lblTransForName" runat="server" Text="Transaction For"></asp:Label></td>
                                    <td class="style26">
                                        <asp:Label ID="lblTransforSC" runat="server" Text=":"></asp:Label></td>
                                    <td class="style27">
                                        <asp:Label ID="lblTransFor" runat="server"></asp:Label></td>
                                    <td class="style28">&nbsp;</td>
                                    <td class="style29">&nbsp;</td>
                                    <td class="style31">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style19">Cheque No</td>
                                    <td class="style22">
                                        <strong>:</strong></td>
                                    <td class="style24">
                                        <asp:Label ID="lblChequeNo" runat="server"
                                            CssClass="style52"></asp:Label>
                                    </td>
                                    <td class="style25" style="font-family: Calibri;">Cheque Date</td>
                                    <td class="style26">:</td>
                                    <td class="style27">
                                        <asp:Label ID="lblChequeDT" runat="server"></asp:Label>
                                    </td>
                                    <td class="style28"
                                        style="font-size: 15px; font-weight: 600; text-align: right; font-family: Calibri;">Total (Tk.)</td>
                                    <td class="style30">
                                        <strong>:</strong></td>
                                    <td class="style31">
                                        <asp:Label ID="lblTotAmount" runat="server"
                                            Style="font-size: 15px; text-align: right; font-weight: 600;"
                                            CssClass="style46"></asp:Label>
                                    </td>
                                </tr>
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
                                        <div style="width: 60%; font-family: Calibri;">
                                            <asp:Label Style="font-family: Calibri;" ID="lblInWords" runat="server"></asp:Label>
                                            <div style="width: 100%;"></div>
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
            </div>
        </div>
    </form>
</body>
</html>
