<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayInformationPrint.aspx.cs" Inherits="AlchemyAccounting.Info.Report.HolidayInformationPrint" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            font-weight: bold;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div style="margin: 0 auto; height: 942px; padding: 10px; width: 842px; border: groove; border-width: 2px; border-color: black">
            <div style="text-align: center; font-size: xx-large">
                Employee Enformation
            </div>
            <hr />
            <table class="auto-style1">
                <tr>
                    <td style="width: 30%">Form Date:&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblFrDT" runat="server" CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>To Date :&nbsp;&nbsp;
                        <asp:Label ID="lblToDT" runat="server" CssClass="auto-style2"></asp:Label>
                    </td>
                </tr>
            </table>

            <div style="height: 1104px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Type">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

