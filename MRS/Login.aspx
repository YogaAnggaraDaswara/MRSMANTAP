<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MRSs.Account.Login1" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">

    <!-- js placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="MRS/assets/js/jquery.js"></script>
    <script type="text/javascript" src="MRS/assets/js/bootstrap.min.js"></script>
    <!--BACKSTRETCH-->
    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <%-- <script  type="text/javascript"  src="MRS/assets/js/jquery.backstretch.min.js"></script>
    <script>
        $.backstretch("../MRS/assets/img/login-bg.png" , { speed: 500 });
    </script> --%>
    <title>Bank MANTAP - MRS</title>

    <!-- Bootstrap core CSS -->
    <link href="MRS/assets/css/bootstrap.css" rel="stylesheet" />
    <!--external css-->
    <link href="MRS/assets/font-awesome/css/font-awesomeee.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="MRS/assets/css/style.css" rel="stylesheet" />
    <link href="MRS/assets/css/style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script language="JavaScript" type="text/javascript">
        if (top != self) { top.location = self.location; }
        function kutip_satu() {
            if ((event.keyCode == 35) || (event.keyCode == 39)) {
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $("#chkShowPassword").bind("click", function () {
                var txtPassword = $("#TXT_PASSWORD");
                if ($(this).is(":checked")) {
                    txtPassword.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "txt_' + txtPassword.attr("id") + '" type = "text" value = "' + txtPassword.val() + '" />');
                    txtPassword.hide();
                    
                } else {
                    txtPassword.val(txtPassword.next().val());
                    txtPassword.next().remove();
                    txtPassword.show();
                }
            });
        });
        function PasswordChanged(txt) {
            $(txt).prev().val($(txt).val());
        }
</script>
</head>

<body style="background-image: url('MRS/assets/img/login-bg.png'); background-size: cover;">
    <div id="login-page">
        <div>

            <form class="form-login" runat="server">
                <h2 class="form-login-heading">sign in now</h2>
                <div class="login-wrap">
                    <asp:TextBox ID="TXT_USERNAME" onkeypress="return kutip_satu()" class="form-control" runat="server" placeholder="User ID"></asp:TextBox>
                    <br>

                    <asp:TextBox ID="TXT_PASSWORD" class="form-control" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                    <br>
                    <label for="chkShowPassword">
                        <input type="checkbox" id="chkShowPassword" />
                        Show password</label><br />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    <%-- <label class="checkbox">
                        <span class="pull-right">
                            <a data-toggle="modal" href="login.html#myModal">Forgot Password?</a>

                        </span>
                    </label>--%>
                    <br />
                    <%--<button class="btn btn-theme btn-block" href="MRS/Default.aspx" type="submit"><i class="fa fa-lock"></i> SIGN IN</button>--%>
                    <asp:Button class="btn btn-theme btn-block" ID="signin" Text="SIGN IN" runat="server" OnClick="signin_Click" />
                    <%--<hr>--%>

                    <%--                    <div class="login-social-link centered">
                        <p>or you can sign in via your social network</p>
                        <button class="btn btn-facebook" type="submit"><i class="fa fa-facebook"></i>Facebook</button>
                        <button class="btn btn-twitter" type="submit"><i class="fa fa-twitter"></i>Twitter</button>
                    </div>
                    <div class="registration">
                        Don't have an account yet?<br />
                        <a class="" href="#">Create an account
                        </a>
                    </div>--%>
                </div>

                <%--                <!-- Modal -->
                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Forgot Password ?</h4>
                            </div>
                            <div class="modal-body">
                                <p>Enter your e-mail address below to reset your password.</p>
                                <input type="text" name="email" placeholder="Email" autocomplete="off" class="form-control placeholder-no-fix">
                            </div>
                            <div class="modal-footer">
                                <button data-dismiss="modal" class="btn btn-default" type="button">Cancel</button>
                                <button class="btn btn-theme" type="button">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal -->--%>
            </form>

        </div>
        <h1 style="text-align: center; color: #ffffff"><b>APPLICATION MONITORING<b></h1>
        <h2 style="text-align: center; color: #ffffff">(MONITORING REPORTING SYSTEM)</h2>
    </div>
</body>
</html>
