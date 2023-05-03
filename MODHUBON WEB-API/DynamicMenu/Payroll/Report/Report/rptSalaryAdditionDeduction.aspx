<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptSalaryAdditionDeduction.aspx.cs" Inherits="DynamicMenu.Payroll.Report.Report.rptSalaryAdditionDeduction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Report || Salary Addition Deduction ::</title>
    
    
    <script src="../../../Stock/Report/../../MenuCssJs/js/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../../Stock/Report/../../MenuCssJs/js/jquery-ui.js" type="text/javascript"></script>

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
        th{ padding: 1px;
               padding-top: 5px
        }
        .GroupHeaderStyle {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            font-weight: bold;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div>
                <div>
                    <div>
                        <table style="width: 100%; margin: 1% 1% 0% 1%;">
                            <tr>
                                <td style="text-align: center; font-size: xx-large; font-weight: 700"
                                    class="style4" rowspan="4">
                                    <img src="../../../Images/logo.png" alt="logo" style="float: left; width: 187px; height: 100px; margin-top: -18px;" /></td>
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
                                <td class="style5"
                                    style="text-align: center;">
                                    <strong><asp:Label runat="server" ID="lblheadmn"></asp:Label></strong></td>
                                <td class="style15"
                                    style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    <asp:Label ID="lblTime" runat="server"
                                        Style="text-align: center; font-family: Calibri; font-size: 8px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 98%; margin: 0% 1% 0% 1%; height: 1px; background: #000000;">
                        </div>
                        <table width="100%" style="font-family: Calibri; margin: 1% 1% 0% 1%; font-size:medium; font-weight: bold">
                            <tr>
                                <td>BRANCH NAME :
                                    <strong><asp:Label runat="server" ID="lblStore"></asp:Label></strong></td>
                                <td>
                                    <strong>
                                        MONTH : <asp:Label runat="server" ID="lblMonth"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style=" margin: 1% 1% 0% 1%;">

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField HeaderText="Employee Name">
                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                    <ItemStyle Width="25%" HorizontalAlign="Left" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Daily Allowance">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Bonus Present">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Bonus Festival">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Incentive">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="PF">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Conveyance">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Mobile">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Due Adjust">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Commission">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Advance">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="PF">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Stamp">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fooding">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fine">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Pay Adjust">
                                     <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10PX" />
                            <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                            <RowStyle Font-Names="Calibri" Font-Size="10px" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
