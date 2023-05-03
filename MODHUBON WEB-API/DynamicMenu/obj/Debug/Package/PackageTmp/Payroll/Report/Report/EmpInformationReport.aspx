<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="EmpInformationReport.aspx.cs" Inherits="AlchemyAccounting.Info.Report.EmpInformationReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
     <br />
        <div style="margin:0 auto;height:942px; padding:10px;width:842px;border:groove; border-width:2px;border-color:black">
            <div style="text-align: center; font-size: xx-large">
                Employee Salary Information</div>
           <hr />
            <div style="height:1104px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Employee Particulars">
                            <HeaderStyle Width="6%" />
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Post Name">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Basic Salary">
                            <HeaderStyle Width="3%" />
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="House Rent">
                            <HeaderStyle Width="3%" />
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Medical">
                            <HeaderStyle Width="2%" />
                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="PF Rate">
                            <HeaderStyle Width="2%" />
                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="PF From">
                            <HeaderStyle Width="3%" />
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Job From">
                            <HeaderStyle Width="3%" />
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Job To">
                            <HeaderStyle Width="3%" />
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                </asp:GridView>
            </div>
            </div>
    </form>
</body>
</html>

