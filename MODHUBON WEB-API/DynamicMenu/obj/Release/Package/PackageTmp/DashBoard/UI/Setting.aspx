<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="DynamicMenu.DashBoard.UI.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <!-- main content start here -->
        <div class="col-md-10 pull-right" id="mainContentBox">
            <div id="contentBox">
                <div id="contentHeaderBox">
                    <h1>Theme Change</h1>
                   
                </div>
                <!-- content header end -->
                <br/>


                <div id="themeContainer">
                    <table id="themesThum">
                        <tr>
                            <td><img src="../../MenuCssJs/images/theme/Default.png" alt=""/>
                                <p>Default</p>
                            <%--</td>--%>
                            <td><img src="../../MenuCssJs/images/theme/theme1.png" alt=""/>
                                <p>Theme1</p>
                            </td>
                            <td><img src="../../MenuCssJs/images/theme/theme2.png" alt=""/>
                                <p>Theme2</p>
                            </td>
                           
                        </tr>

                    </table>
                </div>
            </div>


        </div>
        <!-- main content end here -->
   
</asp:Content>
