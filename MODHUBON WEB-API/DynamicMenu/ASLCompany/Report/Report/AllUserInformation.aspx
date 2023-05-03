<%@ Page Title="" Language="C#" MasterPageFile="~/Dynamic.Master" AutoEventWireup="true" CodeBehind="AllUserInformation.aspx.cs" Inherits="DynamicMenu.ASLCompany.Report.Report.AllUserInformation" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="AlchemyAccounting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=lblMsg.ClientID%>").fadeOut(20000);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="col-md-10 pull-right" id="mainContentBox">
        <div id="contentBox">
            <div id="contentHeaderBox">
                <h1>User Information</h1>
                <!-- <span class="pull-right" id="editOption"><i class="fa fa-cog"></i></span> -->


                <!-- logout option button -->
                <div class="btn-group pull-right" id="editOption">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                        <i class="fa fa-cog"></i>
                    </button>
                    <ul class="dropdown-menu pull-right" style="" role="menu">
                        <li><a href="#"><i class="fa fa-plus"></i>Add record</a>
                        </li>
                        <li><a href="#"><i class="fa fa-edit"></i>Edit record</a>
                        </li>
                        <li><a href="#"><i class="fa fa-times"></i>Delete record</a>
                        </li>

                    </ul>
                </div>
                <!-- end logout option -->


            </div>
            <!-- content header end -->

            <div class="form-class">
                <div class="row form-class">
                    <div class="col-md-2"></div>
                    <div class="col-md-2"><strong>
                        <asp:Label runat="server" ID="lblComLabel" Text="Company Name"></asp:Label></strong></div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="txtCompanyName" AutoPostBack="True"  CssClass="form-control input-sm" OnSelectedIndexChanged="txtCompanyName_TextChanged"/>
                    </div>
                    <div class="col-md-4"></div>
                </div>
            </div>
            <div class="">
                <div class="row form-class">
                    <div class="col-md-12 text-center">
                        <asp:Label runat="server" ID="lblMsg" Visible="False" ForeColor="red"></asp:Label>
                    </div>
                </div>
            </div>


            <% if (Session["CompanyIdForReport"] != null)
               { %>
            <div class="form-class">
                <div class="panel panel-primary table-responsive">
                    <!-- Default panel contents -->
                    <div class="panel-heading">
                        <table class="" style="font-size: 14px; width: 100%">
                            <tr class="">
                                <td style="width: 20%; text-align: center">User Name</td>
                                <td style="width: 15%; text-align: center">Department</td>
                                <td style="width: 15%; text-align: center">Operation Type</td>
                                <td style="width: 15%; text-align: center">Contact No</td>
                                <td style="width: 15%; text-align: center">Email ID</td>
                                <td style="width: 15%; text-align: center">Login Id</td>
                                <td style="width: 5%; text-align: center">Status</td>
                            </tr>
                        </table>
                    </div>

                    <!-- Table -->

                    <table class="table" style="font-size: 12px">
                       
                        <% string companyid = Session["CompanyIdForReport"].ToString();
                           SqlConnection con = new SqlConnection(dbFunctions.connection);
                           con.Open();
                           SqlCommand cmd = new SqlCommand(@"SELECT ASL_COMPANY.COMPNM, ASL_USERCO.USERNM, ASL_USERCO.DEPTNM, 
                            CASE(ASL_USERCO.OPTP) WHEN 'COMPADMIN' THEN 'COMPANY ADMIN' WHEN 'USERADMIN' THEN 'USER ADMIN' 
                            WHEN 'SUPERADMIN' THEN 'SUPER ADMIN' 
                            WHEN 'USER' THEN 'USER' ELSE ASL_USERCO.OPTP END AS OPTP,
                            ASL_USERCO.ADDRESS, ASL_USERCO.MOBNO, ASL_USERCO.EMAILID, 
                            CASE(ASL_USERCO.LOGINBY) WHEN 'MOBNO' THEN 'MOBILE' ELSE ASL_USERCO.LOGINBY END AS LOGINBY, 
                            ASL_USERCO.LOGINID, ASL_USERCO.LOGINPW, CONVERT(varchar(15),CAST(ASL_USERCO.TIMEFR AS TIME),100) AS TIMEFR,
                            CONVERT(varchar(15),CAST(ASL_USERCO.TIMETO AS TIME),100) AS TIMETO, 
                            CASE(ASL_USERCO.STATUS) WHEN 'A' THEN 'ACTIVE' ELSE 'INACTIVE' END AS STATUS
                            FROM ASL_USERCO INNER JOIN
                            ASL_COMPANY ON ASL_USERCO.COMPID = ASL_COMPANY.COMPID
                            WHERE ASL_COMPANY.COMPID='" + companyid + "'" +
                            " ORDER BY ASL_COMPANY.COMPNM, ASL_USERCO.USERNM", con);
                           SqlDataReader dr = cmd.ExecuteReader();
                           while (dr.Read())
                           { %>
                        <tr>
                            <td style="width: 20%; text-align: left"><%= dr["USERNM"].ToString() %></td>
                            <td style="width: 15%; text-align: left"><%= dr["DEPTNM"].ToString() %></td>
                            <td style="width: 15%; text-align: center"><%= dr["OPTP"].ToString() %></td>
                            <td style="width: 15%; text-align: center"><%= dr["MOBNO"].ToString() %></td>
                            <td style="width: 15%; text-align: center"><%= dr["EMAILID"].ToString() %></td>
                            <td style="width: 15%; text-align: left"><%= dr["LOGINID"].ToString() %></td>
                            <td style="width: 5%; text-align: center"><%= dr["STATUS"].ToString() %></td>
                        </tr>
                        <% }
                           dr.Close();
                           con.Close();
                        %>
                    </table>
                </div>

            </div>

            <% } %>
        </div>
    </div>
 
</asp:Content>
