<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpr_dailyPresent.aspx.cs" Inherits="DynamicMenu.Payroll.Report.Report.rpr_dailyPresent" %>

<!DOCTYPE html>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="AlchemyAccounting" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../MenuCssJs/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .tbl {
            width: 100%;
            margin-top: 10px;
        }

            .tbl td {
                border: 1px solid #000000;
            }
             
        .list li {
            display: inline-block;
            vertical-align: top;
        }
        .auto-style1 {
            background: #999999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height:1000px">
            <div style="text-align:center; background:#999999; color:white; font-weight:bold">
                DATE: <%=Session["Fdate"].ToString() %> 
            </div>
            <hr />
            <ul class="list">
                <%int sl = 1;
                    IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
                    string date = Session["Fdate"].ToString();
                    DateTime dateFR = DateTime.Parse(date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string FRDT = dateFR.ToString("yyyy-MM-dd");
                    SqlConnection conn = new SqlConnection(dbFunctions.connection);
                    string script = @"SELECT *,(
SELECT  COUNT(DISTINCT HR_EMP.EMPNM)--DISTINCT   HR_EMP.EMPNM, HR_EMPSALARY.PBONUSRT
FROM            HR_EMP INNER JOIN  HR_ATREG ON HR_EMP.EMPID = HR_ATREG.EMPID LEFT OUTER JOIN
HR_EMPSALARY ON HR_ATREG.EMPID = HR_EMPSALARY.EMPID WHERE HR_EMP.DEPTID=A.DEPTID
) VAL FROM (
SELECT DISTINCT HR_DEPT.DEPTNM,HR_DEPT.DEPTID
FROM HR_EMP INNER JOIN HR_ATREG ON HR_EMP.EMPID = HR_ATREG.EMPID INNER JOIN
HR_DEPT ON HR_EMP.DEPTID = HR_DEPT.DEPTID WHERE HR_ATREG.TRANSDT='"+FRDT+"') A order by VAL desc";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(script, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    foreach (var row in dr)
                    {
                        sl = 1;
                        string DEPT = dr["DEPTNM"].ToString();
                        string DEPTID = dr["DEPTID"].ToString();
                %>
                <li>
                    <table class="tbl">
                        <tr>
                            <td colspan="3" class="auto-style1" ><%=DEPT %></td>
                        </tr>
                        <%
                            //SqlConnection con = new SqlConnection(dbFunctions.connection);
                            string script1 = @"SELECT DISTINCT   HR_EMP.EMPNM, HR_EMPSALARY.PBONUSRT
FROM            HR_EMP INNER JOIN  HR_ATREG ON HR_EMP.EMPID = HR_ATREG.EMPID LEFT OUTER JOIN
                         HR_EMPSALARY ON HR_ATREG.EMPID = HR_EMPSALARY.EMPID WHERE HR_EMP.DEPTID='" + DEPTID + "'";
                           // conn.Open();
                            SqlCommand cmd1 = new SqlCommand(script1, conn);
                            SqlDataReader dr1 = cmd1.ExecuteReader();

                            foreach (var row1 in dr1)
                            {
                                string EMPNM = dr1["EMPNM"].ToString();
                                string PBONUSRT = dr1["PBONUSRT"].ToString();
                        %>
                        <tr>
                            <td style="width: 25px;text-align:center"><%=sl %></td>
                            <td style="width:150px"><%=EMPNM %></td>
                            <td style="width: 40px"><%=PBONUSRT %></td>
                        </tr>
                        <% sl++;}
                             %>
                    </table>
                </li>
                <%
                    }
                    dr.Close();
                    conn.Close(); %>
            </ul>
        </div>
    </form>
</body>
</html>
