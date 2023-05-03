<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptSalarySheetByEmpGroup.aspx.cs" Inherits="DynamicMenu.Payroll.Report.rptSalarySheetByEmpGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Report || Salary Sheet ::</title>


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

        .footer {
            position: absolute;
            bottom: 0;
            width: 100%;
            height: 90px;
            background-color: #ffd800;
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

        th {
            padding: 1px;
            padding-top: 5px;
        }

        .GroupHeaderStyle {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            font-weight: bold;
            height: 30px;
            font-size: 20px;
        }

        .auto-style6 {
            width: 115px;
        }

        .auto-style8 {
            width: 432px;
        }

        .auto-style10 {
            width: 308px;
        }

        .auto-style11 {
            width: 211px;
        }

        .auto-style12 {
            width: 108px;
        }

        .auto-style13 {
            width: 484px;
        }

        .auto-style14 {
            width: 123px;
        }

        .page {
            page-break-after: always;
            position: relative;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="page">
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
                                    style="text-align: center;">
                                    <asp:Label ID="lblContact" runat="server"
                                        Style="font-family: Calibri; font-size: 11px" Visible="False"></asp:Label>
                                </td>
                                <td class="style15"
                                    style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5"
                                    style="text-align: center;">
                                    <strong>
                                        <asp:Label runat="server" ID="lblheadmn"></asp:Label></strong></td>
                                <td class="style15"
                                    style="text-align: center; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                                    <asp:Label ID="lblTime" runat="server"
                                        Style="text-align: center; font-family: Calibri; font-size: 8px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 98%; margin: 0% 1% 0% 1%; height: 1px; background: #000000;">
                        </div>
                        <table width="100%" style="font-family: Calibri; font-size: medium; font-weight: bold; margin: 1% 1% 0% 1%;">
                            <tr>
                                <%--<td>BRANCH NAME :
                                    <strong>
                                        <asp:Label runat="server" ID="lblStore"></asp:Label>
                                    </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    DEPARTMENT : <strong>
                                        <asp:Label runat="server" ID="lbldtp"></asp:Label>
                                    </strong>
                                </td>--%>
                                <td>
                                    <strong>MONTH :
                                        <asp:Label runat="server" ID="lblMonth"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="margin: 1% 1% 0% 1%;">
                        <table style="border-collapse: collapse; font-weight: bold">
                            <tr>
                                <td style="width: 9.12%; text-align: center; border: 1px solid #000000">Employee's Information</td>
                                <td style="width: 12.1%; text-align: center; border: 1px solid #000000">Details Salary</td>
                                <td style="width: 11.7%; text-align: center; border: 1px solid #000000">Addition</td>
                                <td style="width: 18%; text-align: center; border: 1px solid #000000">Deduction</td>
                            </tr>

                        </table>

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                            OnRowCreated="GridView1_RowCreated">
                            <Columns>
                                <asp:BoundField HeaderText="SL">
                                    <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                    <ItemStyle Width="1%" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Employee Name">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle Width="10%" HorizontalAlign="Left" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Post">
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                    <ItemStyle Width="6%" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Basic Salary">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="House Rent">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Medical">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Convey">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Others">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Gross Salary">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Incen- tive">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fooding Addition">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Attendance Bonus">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="OT Hour">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="OT Amount">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Total Addition">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Absent">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Advance">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fooding">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Fine">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Income Tax">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Total Deduction">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Net Pay">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Signature">
                                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                            </Columns>
                            <FooterStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10PX" Height="20px" />
                            <HeaderStyle Font-Names="Calibri" Font-Size="12px" />
                            <RowStyle Font-Names="Calibri" Font-Size="10px" Height="25px" />
                        </asp:GridView>

                    </div>
                    <section id="footer">

                        <table style="width: 98%; margin: 5% 1% 0% 1%; height: 47px;">
                            <tr>
                                <td style="width: 20%; text-align: center"><span style="border-top: 1px solid #a9a9a9">Ass. Manager (admin)</span></td>
                                <td style="width: 20%; text-align: center"><span style="border-top: 1px solid #a9a9a9">Asst. Manager(A&F)</span></td>
                                <td style="width: 20%; text-align: center"><span style="border-top: 1px solid #a9a9a9">DGM (Operation)</span></td>
                                <td style="width: 20%; text-align: center"><span style="border-top: 1px solid #a9a9a9">Managing Director</span></td>
                                <td style="width: 20%; text-align: center"><span style="border-top: 1px solid #a9a9a9">Chairman</span></td>
                            </tr>
                        </table>

                    </section>


                </div>
            </div>
        </div>
    </form>
</body>
</html>
