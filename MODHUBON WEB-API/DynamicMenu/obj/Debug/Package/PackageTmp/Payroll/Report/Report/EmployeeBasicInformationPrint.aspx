<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeBasicInformationPrint.aspx.cs" Inherits="AlchemyAccounting.Info.Report.EmployeeBasicInformationPrint" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
            font-size: x-large;
        }
        .auto-style3 {
            text-align: center;
            font-size: 30pt;
            background-color: #CCCCCC;
        }
        .auto-style4 {
            width: 394px;
            text-align: right;
        }
        .auto-style5 {
            text-align: right;
        }
        .auto-style9 {
            width: 1%;
            height: 20px;
        }
        .auto-style10 {
            width: 8%;
            text-align: right;
            height: 20px;
        }
        .auto-style11 {
            width: 20%;
            height: 20px;
        }
        .auto-style12 {
            width: 1%;
        }
        .auto-style13 {
            width: 8%;
            text-align: right;
        }
        .auto-style14 {
            width: 20%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto;height:942px; padding:10px;width:842px;border:groove; border-width:2px;border-color:black">
        <div>
        <table class="auto-style1">
             <tr>
                 <td style="width: 1%">&nbsp;</td>
                 <td class="auto-style3" colspan="4"><asp:Label runat="server" ID="lblCompanyNM"></asp:Label></td>
                 <td style="width: 1%">&nbsp;</td>
             </tr>
             <tr>
                 <td style="width: 1%">&nbsp;</td>
                 <td class="auto-style2" colspan="4">Employee Basic Information </td>
                 <td style="width: 1%">&nbsp;</td>
             </tr>
             <tr>
                 <td style="width: 1%">&nbsp;</td>
                 <td colspan="4"><hr /></td>
                 <td style="width: 1%">&nbsp;</td>
             </tr>

             <tr>
                 <td class="auto-style9"></td>
                 <td class="auto-style10">
                     &nbsp;Name&nbsp; : </td>
                 <td class="auto-style11">
                     <asp:Label ID="lblEmpNM" runat="server"></asp:Label>
                 </td>
                 <td class="auto-style10">
                     Department :</td>
                 <td class="auto-style11">
                     <asp:Label ID="lblDptNM" runat="server"></asp:Label>
                 </td>
                 <td class="auto-style9"></td>
             </tr>

             <tr>
                 <td class="auto-style12"></td>
                 <td class="auto-style13">
                     Gender : </td>
                 <td class="auto-style14">
                     <asp:Label ID="lblGender" runat="server"></asp:Label>
                 </td>
                 <td class="auto-style13">
                     Joining Date</td>
                 <td class="auto-style14">
                     <asp:Label ID="lblJoiningDT" runat="server"></asp:Label>
                 </td>
                 <td class="auto-style12"></td>
             </tr>

             <tr>
                 <td style="width: 1%">&nbsp;</td>
                 <td class="auto-style4" style="width: 8%">
                     Member Type :</td>
                 <td style="width: 20%">
                     <asp:Label ID="lblEmpTP" runat="server"></asp:Label>
                 </td>
                 <td style="width: 8%; " class="auto-style5">
                     &nbsp;</td>
                 <td style="width: 20%">
                     &nbsp;</td>
                 <td style="width: 1%">&nbsp;</td>
             </tr>

             <tr>
                 <td style="width: 1%">&nbsp;</td>
                 <td colspan="4">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"   Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound">
                         <Columns>                            
                             <asp:BoundField HeaderText="Post">
                             <HeaderStyle Width="12%" />
                             <ItemStyle Width="12%" HorizontalAlign="Left"/>
                             </asp:BoundField>                             
                              <asp:BoundField HeaderText="Status">
                             <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                             <ItemStyle Width="10%"  HorizontalAlign="Left"/>
                             </asp:BoundField>
                             <asp:BoundField HeaderText="Basic">
                             <HeaderStyle Width="5%"/>
                             <ItemStyle Width="5%"  HorizontalAlign="Right"/>
                             </asp:BoundField>
                             <asp:BoundField HeaderText="House Rent">
                             <HeaderStyle Width="5%" />
                             <ItemStyle Width="5%" HorizontalAlign="Right"/>
                             </asp:BoundField>                         
                             <asp:BoundField HeaderText="Medical">
                             <HeaderStyle Width="5%"  />
                             <ItemStyle Width="5%" HorizontalAlign="Right"/>
                             </asp:BoundField>                           
                             <asp:BoundField HeaderText="PF Rate">
                             <HeaderStyle Width="5%" />
                             <ItemStyle Width="5%" HorizontalAlign="Right"/>
                             </asp:BoundField>
                             <asp:BoundField HeaderText="PF From">
                             <HeaderStyle Width="5%" />
                             <ItemStyle Width="5%" HorizontalAlign="Center"/>
                             </asp:BoundField>
                             <asp:BoundField HeaderText="Job From">
                             <HeaderStyle Width="5%" />
                             <ItemStyle Width="5%" HorizontalAlign="Center"/>
                             </asp:BoundField>
                             <asp:BoundField HeaderText="Job To">
                             <HeaderStyle Width="5%" />
                             <ItemStyle Width="5%" HorizontalAlign="Center"/>
                             </asp:BoundField>
                                          
                         </Columns>
                         <FooterStyle Font-Size="9pt" />
                         <HeaderStyle BackColor="#CCCCCC" Font-Bold="False" Font-Size="10pt" />
                     </asp:GridView>
                 </td>
                 <td style="width: 1%">&nbsp;</td>
             </tr>
             <tr>
                 <td style="width: 1%">&nbsp;</td>
                 <td colspan="4">&nbsp;</td>
                 <td style="width: 1%">&nbsp;</td>
             </tr>
             </table>
            </div>
    </div>
    </form>
</body>
</html>

