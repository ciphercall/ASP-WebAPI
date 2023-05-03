<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DynamicMenu.LogIn.UI.ForgotPassword" %>

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
    <title>Forgot Password</title>

</head>
<body style="background-color: #FFCC66">
    <form id="form1" runat="server">
        <br />
        <div class="container">
            <div class="row">
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="text-center">
                                    <img src="../../Images/Man_Logo.JPG" alt="forgot password" class="img-circle" style="height: 150px; width: 150px" />
                                    <h3 class="text-center">Forgot Password?</h3>
                                    <p>If you have forgotten your password - reset it here.</p>
                                    <div class="panel-body">


                                        <!--start form-->
                                        <!--add form action as needed-->
                                        <fieldset>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i style="color: blue" class="glyphicon glyphicon-envelope"></i></span>
                                                    <!--EMAIL ADDRESS-->
                                                    <asp:TextBox ID="emailInput" placeholder="email address" class="form-control" type="email" oninvalid="setCustomValidity('Please enter a valid email address!')" onchange="try{setCustomValidity('')}catch(e){}" required="" runat="server" />
                                                </div>
                                            </div>
                                            <div class="text-center">
                                                <asp:Label runat="server" ID="lblMsg" Visible="False"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSubmit" class="btn btn-lg btn-primary btn-block" Text="Submit" value="Send My Password" type="submit" runat="server" OnClick="btnSubmit_Click" />
                                            </div>
                                            <%if ((string) Session["Corectness"] == "ON")
                                              { %>
                                            <div>
                                                <div class="val-message val-success">
                                                    <span class="fa fa-check" title="success"></span>
                                                    <p class="text-success">
                                                        <strong>Account recovery email sent to <asp:Label runat="server" ID="lblEmailId"></asp:Label>
                                                        </strong>
                                                    </p>

                                                    <p>
                                                        If you don't see this email in your inbox within 15 minutes, look for it in your junk mail folder. If you find it there, please mark it as "Not Junk".
                                                    </p>
                                                </div>
                                            </div>
                                            <%} %>
                                        </fieldset>

                                        <!--/end form-->

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
