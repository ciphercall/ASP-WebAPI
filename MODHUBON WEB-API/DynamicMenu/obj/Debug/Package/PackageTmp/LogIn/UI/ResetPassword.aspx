<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="DynamicMenu.LogIn.UI.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="Sergey Pozhilov (GetTemplate.com)" />
    <script src="../assets/js/jquery-2.1.3.js"></script>
    <script src="../../MenuCssJs/js/session.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/headroom.min.js"></script>
    <script src="../assets/js/jQuery.headroom.min.js"></script>
    <script src="../assets/js/template.js"></script>
    <link rel="shortcut icon" href="../assets/images/favicon.ico" />

    <link rel="stylesheet" media="screen" href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />

    <!-- Custom styles for our template -->
    <link rel="stylesheet" href="../assets/css/bootstrap-theme.css" media="screen" />
    <link rel="stylesheet" href="../assets/css/main.css" />
    <title>Reset Password</title>
    <style>
        .padding10px {
            padding: 10px;
        }

        .linkbuttonpadding {
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
            background-color: #FF8F06;
            color: #fff;
            width: 100%;
        }
    </style>
</head>
<body style="background-color: #FFCC66">
    <form id="form1" runat="server">
        <br />
        <div class="container">
            <div class="row">
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                        <div class="panel panel-default">


                            <div class="panel-body">
                                <%
                                    string userid = Session["UserIdNull"].ToString();
                                    if (userid == "FALSE")
                                    {
                                %>

                                <div class="text-center">
                                    <div class="">
                                        <h3 class="text-left">Choose a new password</h3>
                                    </div>
                                    <hr />
                                    <div class="val-resetpass ">
                                        <p>You can change your password immediately because you are logged in to your email account on this browser.</p>
                                        <p>A strong password is a combination of letters and punctuation marks. It must be at least 6 characters long.</p>
                                    </div>
                                    <div class="panel-body">
                                        <!--start form-->
                                        <!--add form action as needed-->
                                        <fieldset>






                                            <div class="row">
                                                <div class="col-md-4">New Password</div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtNewPass" class="form-control" type="password"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">Confirm Password</div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtConPass" class="form-control" type="password"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="text-center">
                                                <asp:Label runat="server" ID="lblMsg" Visible="False"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8"></div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="btnSubmit" class="form-control btn-primary" Text="Submit" OnClick="btnSubmit_Click" runat="server" />
                                                </div>
                                            </div>


                                        </fieldset>
                                        <!--/end form-->
                                    </div>
                                </div>
                                <%
                                    }
                                    else if(Session["InvalidLink"].ToString()=="True")
                                    {
                                %>

                                <div>
                                    <div class="table-bordered padding10px">

                                        <p class="text-success">
                                            <strong>Invalid Link</strong>
                                        </p>
                                        <hr />
                                        <p>
                                            The link you used is invalid. Please try again.
                                        </p>
                                        <div class="row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-3"></div>
                                            <div class="col-md-2"><a href="LogIn.aspx" class="linkbuttonpadding">Cancel</a></div>
                                            <div class="col-md-3"><a href="ForgotPassword.aspx" class="linkbuttonpadding">Try Again</a></div>

                                        </div>
                                    </div>
                                </div>
                                <% } %>

                                <% if (Session["PasswordChanged"].ToString() == "True")
                                   {%>
                                <div>
                                    <div class="table-bordered padding10px">

                                        <p class="text-success">
                                            <strong>Password Saved</strong>
                                        </p>
                                        <hr />
                                        <p>
                                            Your password saved succesfully. Please click login to login in website.
                                        </p>
                                        <div class="row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-3"></div>
                                            <div class="col-md-3"></div>
                                            <div class="col-md-2"><a href="LogIn.aspx" class="linkbuttonpadding">Login</a></div>
                                        </div>
                                    </div>
                                </div>
                                <%}%>

                                <% if (Session["PasswordNotChanged"].ToString() == "True")
                                   {%>
                                <div>
                                    <div class="table-bordered padding10px">

                                        <p class="text-success">
                                            <strong>Password not changed</strong>
                                        </p>
                                        <hr />
                                        <p>
                                            Your password will not save. Please try again.
                                        </p>
                                        <div class="row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-3"></div>
                                            <div class="col-md-2"><a href="LogIn.aspx" class="linkbuttonpadding">Cancel</a></div>
                                            <div class="col-md-3"><a href="ForgotPassword.aspx" class="linkbuttonpadding">Try Again</a></div>

                                        </div>
                                    </div>
                                </div>
                                <%}%>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label runat="server" ID="lblUserid" Visible="False"></asp:Label>
        <asp:Label runat="server" ID="lblToken" Visible="False"></asp:Label>
    </form>
</body>
</html>
