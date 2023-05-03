<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeInformationPrint.aspx.cs" Inherits="AlchemyAccounting.Info.Report.EmployeeInformationPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
        <div style="margin:0 auto;height:942px; padding:10px;width:842px;border:groove; border-width:2px;border-color:black">
            <div style="text-align: center; width:842px;height:1104px; margin:auto; font-size: xx-large">
                Employee Information
                <br />
         <hr />
            <div style="height:1104px">
               
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        
                        <asp:BoundField HeaderText="ID">
                            <HeaderStyle Width="7%" />
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Name">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Gender">
                            <HeaderStyle Width="7%" />
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Contact No">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Joining Date">
                            <HeaderStyle Width="7%" />
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText=" Type   ">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Department">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
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

