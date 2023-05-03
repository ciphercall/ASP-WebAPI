<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="DynamicMenu.LogIn.UI.LogIn" %>

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
    <title>Sign in || Madhuban ||</title>
    <script>
        $(document).ready(function () {
            $('#<%=txtlink.ClientID%>').val($.session.get('URLLINK'));
        });
    </script>
    <link rel="shortcut icon" href="../assets/images/favicon.ico" />

    <link rel="stylesheet" media="screen" href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />

    <!-- Custom styles for our template -->
    <link rel="stylesheet" href="../assets/css/bootstrap-theme.css" media="screen" />
    <link rel="stylesheet" href="../assets/css/main.css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
        <!-- Fixed navbar -->

        <div class="navbar navbar-inverse navbar-fixed-top headroom" style="background: #FF9B22">
            <div class="container">
                <div class="navbar-header">
                    <!-- Button for smallest screens -->
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                    <a class="navbar-brand" href="#">
                        <%--<img src="../assets/images/logo.png" alt="Progressus HTML5 template" />--%>Modhuban</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav pull-right">
                        <%--<li><a href="#">Home</a></li>
                        <li><a href="#">About</a></li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">More Pages <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="#">Menu 1</a></li>
                                <li><a href="#">Menu 2</a></li>
                            </ul>
                        </li>
                        <li><a href="#">Contact</a></li>--%>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
        <!-- /.navbar -->

        <header id="head" class="secondary"></header>

        <!-- container -->
        <div class="container">

            <div class="row">

                <!-- Article main content -->
                <article class="col-xs-12 maincontent">
                    <%--<header class="page-header">
					<h1 class="page-title">Sign in</h1>
				</header>--%>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <asp:TextBox runat="server" Style="display: none" ID="txtIp" ClientIDMode="Static"></asp:TextBox>
                            <div class="col-md-4 col-md-offset-4 col-sm-8 col-sm-offset-2" style="padding-top: 10px">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <h3 class="thin text-center">Sign in to your account</h3>

                                        <hr />
                                        <asp:TextBox runat="server" Style="display: none" ClientIDMode="Static" ID="txtLotiLongTude"></asp:TextBox>

                                        <div class="top-margin">
                                            <label>Username/Email <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtUser" type="text" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="top-margin">
                                            <label>Password <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPassword" type="password" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="text-center; top-margin">
                                            <asp:Label runat="server" ID="lblMsg" Visible="False" ForeColor="red"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtlink" ClientIDMode="Static" Style="display: none" class="form-control" runat="server"></asp:TextBox>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <b><a href="ForgotPassword.aspx">Forgot password?</a></b>
                                            </div>
                                            <div class="col-lg-12 text-center">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-action" type="submit" Text="Sign in" OnClick="btnSubmit_Click"></asp:Button>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </article>
                <!-- /Article -->

            </div>
        </div>
        <!-- /container -->


        <footer id="footer" class="top-space">

            <div class="footer1">
                <div class="container">
                    <div class="row">

                        <div class="col-md-3 widget">
                            <h3 class="widget-title">Contact</h3>
                            <div class="widget-body">
                                <p>
                                    Phone: 635013, 615190<br>
                                    <a href="mailto:#"></a><br>
                                    <br>
                                    Address: 72, Bagmoniram, Dampara, <br>Chittagong-1321
                                </p>
                            </div>
                        </div>

                        <div class="col-md-6 widget">
                        </div>

                        <div class="col-md-3 widget">
                            <h3 class="widget-title">Follow me</h3>
                            <div class="widget-body">
                                <p class="follow-me-icons clearfix">
                                    <a href="#"><i class="fa fa-google-plus"></i></a>
                                    <a href="#" target="_blank"><i class="fa fa-facebook"></i></a>
                                </p>

                                <p>
                                    Copyright &copy; <%=DateTime.Now.Year %><br />
                                    Developed by <a href="http://alchemy-bd.com/" rel="designer">Alchemy Software</a>
                                </p>
                            </div>
                        </div>

                    </div>
                    <!-- /row of widgets -->
                </div>
            </div>

            <%--<div class="footer2">
                <div class="container">
                    <div class="row">

                        <div class="col-md-6 widget">
                            <div class="widget-body">
                                <p class="simplenav">
                                    <a href="#">Home</a> | 
								<a href="#">About</a> |
								<a href="#">Sidebar</a> |
								<a href="#">Contact</a> |
								<b><a href="#">Sign up</a></b>
                                </p>
                            </div>
                        </div>

                        <div class="col-md-6 widget">
                            <div class="widget-body">
                                <p class="text-right">
                                    Copyright &copy; 2014, Demo Company. Developed by <a href="http://alchemy-bd.com/" rel="designer">Alchemy Software</a>
                                </p>
                            </div>
                        </div>

                    </div>
                    <!-- /row of widgets -->
                </div>
            </div>--%>
        </footer>





        <!-- JavaScript libs are placed at the end of the document so the pages load faster -->


        <script>
            $(document).ready(function () {
                navigator.geolocation.getCurrentPosition(showPosition);
                function showPosition(position) {
                    var coordinates = position.coords;
                    var long = coordinates.longitude;
                    var loti = coordinates.latitude;
                    $("#<%=txtLotiLongTude.ClientID %>").val(loti + ", " + long);

                }
                $.getJSON("http://jsonip.com/?callback=?", function (data) {
                    console.log(data);
                    $("#<%=txtIp.ClientID %>").val(data.ip);
                });
              
            });
        </script>
    </form>
</body>
</html>
