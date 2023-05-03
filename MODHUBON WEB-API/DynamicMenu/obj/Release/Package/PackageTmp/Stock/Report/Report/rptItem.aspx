<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptItem.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.Report.rptItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <link href="../../../MenuCssJs/ui-gray/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../MenuCssJs/js/jquery-2.1.3.js"></script>
    <script type="text/javascript" src="../../../MenuCssJs/js/jquery-ui.js"></script>

    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>

    <style media="print">
        .showHeader thead {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        #main {
            float: left;
            border: 1px solid #cccccc;
            width: 100%;
            padding-bottom: 40px;
        }

        #btnPrint {
            font-weight: 700;
        }

        .style1 {
            font-size: small;
        }

        .SubTotalRowStyle {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
        }

        .GrandTotalRowStyle {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 12px;
            text-align: right;
            height: 35px;
        }

        .GroupHeaderStyle {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            font-weight: bold;
            height: 30px;
        }

        .GridRowStyle {
           
        }

        .style3 {
            font-family: Calibri;
        }

        .padding {
            padding-left: 5px;
        }
    </style>

</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server" ID="scriptmanger1">
            </asp:ScriptManager>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="style6">&nbsp;</td>
                        <td class="style1">
                            <asp:Label ID="lblCompNM" runat="server"
                                Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                        </td>
                        <td class="style5" style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style3">
                            <asp:Label ID="lblAddress" runat="server"
                                Style="font-family: Calibri; font-size: 9px"></asp:Label>
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style3">
                            <strong>Item Details</strong></td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style7">&nbsp;</td>
                        <td class="style8">
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td class="style5">&nbsp;</td>
                        <td style="text-align: right">
                            <asp:Label ID="lblTime" runat="server"
                                Style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>


            <div style="border: 1px solid #000000; height: auto">


                <%--<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div style="border: double; border-width: 1px; border-color: black;">
                                <p style="padding-left: 20px;">
                                    <asp:Label runat="server" ID="lblText" Text="Date :" Style="font-weight: bold"></asp:Label>
                                    <asp:Label runat="server" ID="lbldate" Text='<%#Eval("MSTNM") %>' Style="font-weight: bold"></asp:Label>
                                </p>
                            </div>--%>
                <table style="width: 100%; border: 1px solid #000000" class="table table-bordered">
                    <tr>
                        <td style="width: 35%; border-right: 1px solid #000000; text-align: center"><strong>Item Name</strong></td>
                           <td style="width: 5%; border-right: 1px solid #000000; text-align: center"><strong>Edition</strong></td>
                        <td style="width: 15%; border-right: 1px solid #000000; text-align: center"><strong>Sub Cat Name</strong></td>
                        <td style="width: 8%; border-right: 1px solid #000000; text-align: center"><strong>Bar Code</strong></td>
                        <td style="width: 7%; border-right: 1px solid #000000; text-align: center"><strong>Unit</strong></td>
                        <td style="width: 15%; border-right: 1px solid #000000; text-align: center"><strong>Author Name</strong></td>
                        <td style="width: 7%; border-right: 1px solid #000000; text-align: center"><strong>Buy Rate</strong></td>
                        <td style="width: 7%; text-align: center"><strong>Sale Rate</strong></td>
                    </tr>
                </table>
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <div>
                            <%-- <asp:Label runat="server" ID="lblHead" Text='<%#Eval("TRANSNO") %>' Style="font-weight: bold"></asp:Label>--%>
                            <span style="padding-bottom: 20px;"><strong><u>Master Category : &nbsp; 
                                            <asp:Label runat="server" ID="lblid" Text='<%#Eval("MSTID") %>' Visible="False"></asp:Label></strong>
                                <asp:Label runat="server" Visible="True" ID="lbldate1" Text='<%#Eval("MSTNM") %>' Style="font-weight: bold"></asp:Label></u></span>
                            <br />
                            <br />
                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblid2" Text='<%#Eval("SUBID") %>' Visible="False"></asp:Label>

                                    <span style="padding-left: 20px"><strong>Sub Category : &nbsp;
                                        <asp:Label runat="server" Visible="True" ID="lblSubNM" Text='<%#Eval("SUBNM") %>' Style="font-weight: bold"></asp:Label></strong></span>

                                    <%--<asp:Label runat="server" Visible="false" ID="lblTransDT" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>--%>

                                    <asp:GridView ID="gv_Trans" runat="server" Width="100%" ShowHeader="False" ShowFooter="true"
                                        AutoGenerateColumns="False" Font-Size="12px" Style="margin-bottom: 0px;"
                                        OnRowDataBound="gv_Trans_RowDataBound">
                                        <Columns>

                                            <asp:BoundField HeaderText="Item Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="35%" />
                                                <ItemStyle Width="35%" CssClass="padding" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                               <asp:BoundField HeaderText="Edition">
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle Width="5%" CssClass="" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Sub Cat. Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                <ItemStyle Width="15%" CssClass="padding" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Bar Code">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkPO" NavigateUrl='<%# "/Stock/UI/ReportBarcode2.aspx?code="+Eval("ITEMCD") %>' 
                                                        Text="<%# Bind('ITEMCD') %>" Target="_blank" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle Width="8%" CssClass="" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Unit">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle Width="7%" CssClass="" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Author Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                <ItemStyle Width="15%" CssClass="" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Buy Rate">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle Width="7%" CssClass="" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Sale Rate">
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            </asp:BoundField>
                                            <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                     <asp:ImageButton ID="btnPrint" runat="server" CommandName="print" ToolTip="Print" ImageUrl="../../Images/print.png"
                                            CssClass="glyphicon glyphicon-print" Height="20px" Width="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle Font-Size="11pt" />
                                    </asp:GridView>
                                    <%-- <div style="border: double; border-width: 1px; border-color: red;">
                    <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("AMOUNT") %>' Style="font-weight: bold"></asp:Label>
                                </div>--%>
                                </ItemTemplate>
                            </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                <%--</ItemTemplate>
                    </asp:Repeater>--%>

                <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowcreated="GridView1_RowCreated" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%">
                    <Columns>
                        
                        <asp:BoundField HeaderText="Category Name" >
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle Width="15%" CssClass="" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Item Name" >
                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                        <ItemStyle Width="30%" CssClass="" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Brand" >
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle Width="15%" CssClass="" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Unit" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" CssClass="" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Buy Rate">
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                           
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sale Rate">
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                           
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                </asp:GridView>--%>
            </div>
        </div>
    </form>
</body>
</html>
