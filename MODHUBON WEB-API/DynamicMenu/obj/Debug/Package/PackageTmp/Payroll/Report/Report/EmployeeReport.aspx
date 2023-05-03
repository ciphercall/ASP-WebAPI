<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeReport.aspx.cs" Inherits="AlchemyAccounting.Info.Report.EmployeeReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
        <div style="margin:0 auto;padding:14px;width:100%;">
            <div style="text-align: center; font-size: xx-large">
                Employee Information (All)</div>
          <hr />
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="14px" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Employee ID">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Employee Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Gender">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Contact">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Joining Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>                      
                        <asp:BoundField HeaderText="Type">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Department">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                </asp:GridView>
            </div>
    </div>
    </div>
    </form>
</body>
</html>
