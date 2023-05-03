<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptPaySlip.aspx.cs" Inherits="DynamicMenu.Payroll.Report.Report.rptPaySlip" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="AlchemyAccounting" %>
<%@ Import Namespace="DynamicMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../MenuCssJs/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        td {
            padding-left: 3px;
            padding-right: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%
            string monthYear = Session["MonthYear"].ToString();
            string branchid = Session["BranchId"].ToString();
            string empid = Session["EmployeeId"].ToString();
            IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
            DateTime dateFR = DateTime.Parse(monthYear, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string mon = dateFR.ToString("MMMM yyyy").ToUpper();
            SqlConnection con = new SqlConnection(dbFunctions.connection);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (empid == "ALL")
                cmd = new SqlCommand(@"SELECT        HR_EMP.EMPNM,HR_EMP.EMPIDM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                         CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                         CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                         CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW,HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                         HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                         HR_SALPAY.FOODING, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM,HR_SALPAY.ABSDED, HR_SALPAY.DAYABS,HR_SALPAY.AMTOTDAYS,HR_SALPAY.AMTOTHOUR,
                         HR_SALPAY.FOODINGADD,HR_SALPAY.BONUSP,HR_SALPAY.ITAX
                         FROM HR_SALPAY INNER JOIN
                         HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID INNER JOIN
                         HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                         WHERE (HR_SALPAY.TRANSMY = '" + monthYear + "') AND (HR_SALPAY.BRANCHCD = '" + branchid + "') ORDER BY  HR_EMP.EMPID", con);
            else
                cmd = new SqlCommand(@"SELECT        HR_EMP.EMPNM,HR_EMP.EMPIDM, HR_SALPAY.BASICSAL, HR_SALPAY.HOUSERENT, HR_SALPAY.MEDICAL, HR_SALPAY.TRANSPORT, HR_SALPAY.GROSSSAL, CAST(HR_SALPAY.DAYTOT AS INT) DAYTOT, 
                         CAST(HR_SALPAY.DAYWORK AS INT) DAYWORK, CAST(HR_SALPAY.DAYHOLI AS INT) DAYHOLI, 
                         CAST(HR_SALPAY.DAYPRE AS INT) DAYPRE, CAST(HR_SALPAY.DAYLEAVE AS INT) DAYLEAVE, CAST(HR_SALPAY.DAYABS AS INT) DAYABS, CAST(HR_SALPAY.OTDAY AS INT) OTDAY, 
                         CAST(HR_SALPAY.OTHOUR AS INT) OTHOUR, HR_SALPAY.ALWDAILYNW,HR_SALPAY.ALWDAILYOT, HR_SALPAY.BONUSP, HR_SALPAY.BONUSF, HR_SALPAY.INCENTIVE, 
                         HR_SALPAY.PFADD, HR_SALPAY.CONVEY, HR_SALPAY.MOBILE, HR_SALPAY.DUEADJ, HR_SALPAY.TOTADD, HR_SALPAY.ADVANCE, HR_SALPAY.PFDED, HR_SALPAY.STAMP, 
                         HR_SALPAY.FOODING, HR_SALPAY.FINEADJ, HR_SALPAY.TOTDED, HR_SALPAY.NETPAY, HR_POST.POSTNM,HR_SALPAY.ABSDED, HR_SALPAY.DAYABS,HR_SALPAY.AMTOTDAYS,HR_SALPAY.AMTOTHOUR,
                         HR_SALPAY.FOODINGADD,HR_SALPAY.BONUSP,HR_SALPAY.ITAX
                         FROM HR_SALPAY INNER JOIN
                         HR_POST ON HR_SALPAY.COMPID = HR_POST.COMPID AND HR_SALPAY.POSTID = HR_POST.POSTID INNER JOIN
                         HR_EMP ON HR_SALPAY.EMPID = HR_EMP.EMPID
                         WHERE (HR_SALPAY.TRANSMY = '" + monthYear + "') AND (HR_EMP.EMPID='" + empid + "') AND (HR_SALPAY.BRANCHCD = '" + branchid + "') ORDER BY  HR_EMP.EMPID", con);
            SqlDataReader dr = cmd.ExecuteReader();
            foreach (var item in dr)
            {
        %>


        <div class="container" style="padding: 10px; padding-bottom: 5px; width: 500px">
            <table style="width: 100%">
                <tr>
                    <td colspan="4" class="text-center" style="font-weight: 700; font-size: 18px">
                        <asp:Label runat="server" ID="lblCompanyName"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td style="width: 6%; text-align: left; font-weight: 700">Name </td>
                    <td style="width: 20%; text-align: left"><%=dr["EMPNM"].ToString() %></td>
                    
                    <td style="width: 8%; text-align: left; font-weight: 200">Member Id</td>
                    <td style="width: 9.8%; text-align: left"><%=dr["EMPIDM"].ToString() %></td>
                </tr>
                
            </table>
            <table style="width: 100%">
                <tr>
                    
                    <td style="width: 10%; text-align: left; font-weight:700">Post </td>
                    <td style="width: 51%; text-align: left"><%=dr["POSTNM"].ToString() %></td>
                    <td style="width: 10%; text-align: left; font-weight: 700">Month</td>
                    <td style="width: 18%; text-align: left"><%=mon %></td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: left"></td>
                    <td style="width: 9.8%; text-align: left"></td>
                    <td style="width: 10%; text-align: left"></td>
                    <td style="width: 30%; text-align: left"></td>
                </tr>
            </table>
            <table style="width: 100%" class="table-bordered">
                <tr>
                    <td style="width: 33%; text-align: center; font-weight: 700">Addition</td>
                    <td style="width: 15%; text-align: center; font-weight: 700">Amount</td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: center; font-weight: 700">Deduction</td>
                    <td style="width: 15%; text-align: center; font-weight: 700">Amount</td>
                </tr>
                <%--<tr>
                    <td style="width: 33%; text-align: left">Gross Salary</td>
                    <td style="width: 15%; text-align: right"><%=dr["GROSSSAL"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left">Absent</td>
                    <td style="width: 15%; text-align: right"><%=dr["ABSDED"].ToString() %></td>
                </tr>--%>
                
                <tr>
                    <td style="width: 33%; text-align: left">Basic Salary</td>
                    <td style="width: 15%; text-align: right"><%=dr["BASICSAL"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left">Advance</td>
                    <td style="width: 15%; text-align: right"><%=dr["ADVANCE"].ToString() %></td>
                </tr>
                <tr>
                    <td style="width: 33%; text-align: left">House Rent</td>
                    <td style="width: 15%; text-align: right"><%=dr["HOUSERENT"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left">Fooding</td>
                    <td style="width: 15%; text-align: right"><%=dr["FOODING"].ToString() %></td>
                </tr>
                
              <%--  <tr >
                    <td style="width: 33%; text-align: left">Transport</td>
                    <td style="width: 15%; text-align: right"><%=dr["TRANSPORT"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left">Fooding</td>
                    <td style="width: 15%; text-align: right"><%=dr["FOODING"].ToString() %></td>
                </tr>--%>
                 <tr >
                    <td style="width: 33%; text-align: left">Conveyance</td>
                    <td style="width: 15%; text-align: right"><%=dr["TRANSPORT"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left">Fine</td>
                    <td style="width: 15%; text-align: right"><%=dr["FINEADJ"].ToString() %></td>
                </tr>
                 <tr >
                    <td style="width: 33%; text-align: left">Others</td>
                    <td style="width: 15%; text-align: right"><%=dr["STAMP"].ToString() %></td>
                    <td style="width: 4%;"></td>
                     <td style="width: 33%; text-align: left">Income Tax</td>
                    <td style="width: 15%; text-align: right"><%=dr["ITAX"].ToString() %></td>
                </tr>
                <%--GROSSSAL,BASICSAL,HOUSERENT,TRANSPORT,STAMP,INCENTIVE,FOODINGADD,BONUSP,AMTOTHOUR,TOTADD
                    ABSDED,ADVANCE,FOODING,FINEADJ,ITAX,TOTDED,NETPAY--%>
                <tr>
                    <td style="width: 33%; text-align: left">Medical</td>
                    <td style="width: 15%; text-align: right"><%=dr["MEDICAL"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left">Absent</td>
                    <td style="width: 15%; text-align: right"><%=dr["ABSDED"].ToString() %></td>
                </tr>
                <tr>
                    <td style="width: 33%; text-align: left">Incentive</td>
                    <td style="width: 15%; text-align: right"><%=dr["INCENTIVE"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>
                <tr>
                    <td style="width: 33%; text-align: left">Fooding Addition</td>
                    <td style="width: 15%; text-align: right"><%=dr["FOODINGADD"].ToString() %></td>
                    <td style="width: 4%;"></td>
                     <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                  
                </tr>
                <tr>
                    <td style="width: 33%; text-align: left">Attendance Bonus</td>
                    <td style="width: 15%; text-align: right"><%=dr["BONUSP"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>
                <tr>
                    <td style="width: 33%; text-align: left">OT Amount</td>
                    <td style="width: 15%; text-align: right"><%=dr["AMTOTHOUR"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>
                <%--<tr>
                    <td style="width: 33%; text-align: left">Provident Fund</td>
                    <td style="width: 15%; text-align: right"><%=dr["PFADD"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>--%>
                <%--<tr>
                    <td style="width: 33%; text-align: left">Conveyance</td>
                    <td style="width: 15%; text-align: right"><%=dr["CONVEY"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>--%>
                <%--<tr>
                    <td style="width: 33%; text-align: left">Mobile</td>
                    <td style="width: 15%; text-align: right"><%=dr["MOBILE"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>
                <tr>
                    <td style="width: 33%; text-align: left">Due Salary</td>
                    <td style="width: 15%; text-align: right"><%=dr["DUEADJ"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                </tr>--%>
                <tr>
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left; font-weight: 700">Total Deduction</td>
                    <td style="width: 15%; text-align: right; font-weight: 700"><%=dr["TOTDED"].ToString() %></td>
                </tr>
               <%-- <tr >
                    <td style="width: 33%; text-align: left"></td>
                    <td style="width: 15%; text-align: right"></td>
                    <td style="width: 4%;"></td>
                </tr>--%>
                <tr>
                    <td style="width: 33%; text-align: left; font-weight: 700">Total Addition</td>
                    <td style="width: 15%; text-align: right; font-weight: 700"><%=dr["TOTADD"].ToString() %></td>
                    <td style="width: 4%;"></td>
                    <td style="width: 33%; text-align: left; font-weight: 700">Net Pay</td>
                    <td style="width: 15%; text-align: right; font-weight: 700"><%=dr["NETPAY"].ToString() %></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 45%;">&nbsp;</td>
                    <td style="width: 10%;"></td>
                    <td style="width: 45%;"></td>
                </tr>
                <tr>
                    <td style="width: 45%;">&nbsp;</td>
                    <td style="width: 10%;"></td>
                    <td style="width: 45%;"></td>
                </tr>
                <tr>
                    <td style="width: 45%;; border-top: 1px solid; text-align: center">Receiver</td>
                    <td style="width: 10%;"></td>
                    <td style="width: 45%; border-top: 1px solid; text-align: center">Authorized Signature</td>
                </tr>
            </table>
        </div>

        <%
            }
            dr.Close();
            con.Close(); %>
    </form>
</body>
</html>
