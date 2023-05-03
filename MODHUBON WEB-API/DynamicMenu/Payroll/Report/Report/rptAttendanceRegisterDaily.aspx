<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptAttendanceRegisterDaily.aspx.cs" Inherits="DynamicMenu.Payroll.Report.Report.rptAttendanceRegisterDaily" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../MenuCssJs/css/bootstrap.min.css" rel="stylesheet" />
     <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0 auto; padding: 10px; width: 842px;">
            <div style="text-align: center; font-size: x-large">
                <asp:Label runat="server" ID="lblCompanyName"></asp:Label>
                <br/>
                ATTENDANCE REGISTER - DAILY
            </div>
            <hr />
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%">Date</td>
                    <td style="width: 1%">:</td>
                    <td style="width: 50%">
                        <asp:Label ID="lblFrDT" runat="server" CssClass="auto-style2"></asp:Label>
                    </td>
                    <td style="text-align: right"> <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                               class="form-control input-sm btn-primary" style="width: 100px"/></td>
                </tr>
                <tr>
                    <td style="width: 10%">Shift</td>
                    <td style="width: 1%">:</td>
                    <td style="width: 50%">
                        <asp:Label ID="lblShiftName" runat="server" CssClass="auto-style2"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 10%">Branch</td>
                    <td style="width: 1%">:</td>
                    <td style="width: 50%">
                        <asp:Label ID="lblBranchName" runat="server" CssClass="auto-style2"></asp:Label>
                    </td>
                    <td style="font-size: 10px; text-align: right; font-weight: 700">
                         Print Date : <asp:Label ID="lblDate" runat="server" ></asp:Label>
                    </td>
                </tr>
            </table>

            <div style="height: 1104px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Employee Name">
                            <HeaderStyle Width="40%" HorizontalAlign="Center" />
                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Shift Time From">
                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Shift Time To">
                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="In Time">
                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Out Time">
                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
