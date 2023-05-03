<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostReportPrint.aspx.cs" Inherits="AlchemyAccounting.Info.Report.PostReportPrint" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
        <div style="margin:0 auto;height:942px; padding:10px;width:842px;border:groove; border-width:2px;border-color:black">
            <div style="text-align: center; width:842px;height:1104px; margin:auto; font-size: xx-large">
                Post Information
            <hr />
            <div style="height:1104px">
               
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        
                        <asp:BoundField HeaderText="ID">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Name">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                            <HeaderStyle Width="55%" />
                            <ItemStyle Width="55%" HorizontalAlign="Center" />
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
