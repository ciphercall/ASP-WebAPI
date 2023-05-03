<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yearlyLeaveReportPrint.aspx.cs" Inherits="AlchemyAccounting.Info.Report.yearlyLeaveReportPrint" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
        .auto-style3 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
        <div style="margin:0 auto;height:942px; padding:10px;width:842px;border:groove; border-width:2px;border-color:black">
            <div style="text-align: center; width:842px;height:1104px; margin:auto; font-size: xx-large">
                Yearly Leave Report
          <hr />
            <div style="height:1104px">
                <div class="auto-style1">
                    <table class="auto-style3">
                        <tr>
                            <td style="font-size: medium; width: 10%">Year :</td>
                            <td style="font-size: medium">
                                <asp:Label ID="lblYR" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        
                        <asp:BoundField HeaderText="Leave Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Days">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:BoundField>
                       
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                </asp:GridView>
            </div>
    </div>
    </div></div>
    </form>
</body>
</html>
