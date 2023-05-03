<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DynamicMenu.DashBoard.UI.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- main content start here -->
        <div class="col-md-10 pull-right" id="mainContentBox">
            <div id="contentBox">
                <div id="contentHeaderBox">
                    <h1>System Vitals</h1>
                    <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                    <!-- logout option button -->
                    <%--<div class="btn-group pull-right" id="editOption">
                        <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                            <i class="fa fa-cog"></i>
                        </button>
                        <ul class="dropdown-menu pull-right" style="" role="menu">
                            <li><a href="#"><i class="fa fa-plus"></i> Add record</a>
                            </li>
                            <li><a href="#"><i class="fa fa-edit"></i> Edit record</a>
                            </li>
                            <li><a href="#"><i class="fa fa-times"></i> Delete record</a>
                            </li>

                        </ul>
                    </div>--%>
                    <!-- end logout option -->


                </div>
                <!-- content header end -->
                <br>
                <%--<div class="col-md-6">
                    <div id="chart">
                        <div id="homeChartSummary">
                            <p class="weeklyIncomeAmount">Tk.<span>22120</span>
                            </p>
                            <p>Weekly Income</p>

                            <table>
                                <tr>
                                    <td>Tk. 201348</td>
                                    <td>Orders</td>
                                </tr>
                                <tr>
                                    <td>Tk. 21232</td>
                                    <td>Investment</td>
                                </tr>
                                <tr>
                                    <td>Tk. 243442</td>
                                    <td>Other</td>
                                </tr>
                            </table>

                        </div>
                        <div id="homeChart">

                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div id="homeTiles">
                        <div>
                            <p>21</p>
                            <div>OPERN TICKETS</div>
                        </div>
                        <div>
                            <p>43</p>
                            <div>CLOSE TICKETS</div>
                        </div>
                        <div>
                            <p>11</p>
                            <div>NEW ORDERS</div>
                        </div>
                        <div>
                            <p>20</p>
                            <div>NEW CLIENTS</div>
                        </div>
                    </div>
                </div>--%>

            </div> <!-- content box end here -->


        </div>
        <!-- main content end here -->
    

</asp:Content>
