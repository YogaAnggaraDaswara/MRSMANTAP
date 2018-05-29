<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Change_Password.aspx.cs" Inherits="MRS.Change_Password" %>


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
                var txtPassword = $("#TXT_LAMA");
                var txtPassword1 = $("#TXT_BARU");
                var txtPassword2 = $("#TXT_BARUVER");
                if ($(this).is(":checked")) {
                    txtPassword.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "txt_' + txtPassword.attr("id") + '" type = "text" value = "' + txtPassword.val() + '" />');
                    txtPassword.hide();
                    txtPassword2.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "txt_' + txtPassword2.attr("id") + '" type = "text" value = "' + txtPassword2.val() + '" />');
                    txtPassword2.hide();
                    txtPassword1.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "txt_' + txtPassword1.attr("id") + '" type = "text" value = "' + txtPassword1.val() + '" />');
                    txtPassword1.hide();

                } else {
                    txtPassword.val(txtPassword.next().val());
                    txtPassword.next().remove();
                    txtPassword.show();


                    txtPassword1.val(txtPassword1.next().val());
                    txtPassword1.next().remove();
                    txtPassword1.show();


                    txtPassword2.val(txtPassword2.next().val());
                    txtPassword2.next().remove();
                    txtPassword2.show();
                }
            });

            $("#cancel").bind("click", function () {

                $("#TXT_LAMA").removeAttr("data-val-required");
                $("#TXT_BARU").removeAttr("data-val-required");
                $("#TXT_BARUVER").removeAttr("data-val-required");
                window.location.href = "/Account/Logout.aspx";
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
                <h2 class="form-login-heading">Change Password</h2>
                <div class="login-wrap">
                                      <asp:TextBox ID="TXT_LAMA" class="form-control" onkeypress="return kutip_satu()" placeholder="Old Password" runat="server" required="required" TextMode="Password"></asp:TextBox>
                    <br />
                                      <asp:TextBox ID="TXT_BARU" class="form-control" onkeypress="return kutip_satu()" placeholder="New Password" runat="server" required="required" TextMode="Password"></asp:TextBox>
                    <br />
                                      <asp:TextBox ID="TXT_BARUVER" class="form-control" onkeypress="return kutip_satu()" placeholder="Verification New Password" runat="server" required="required" TextMode="Password"></asp:TextBox>
                    <br>
                    <label for="chkShowPassword">
                        <input type="checkbox" id="chkShowPassword" />
                        Show password</label>
                    <br />
                    <asp:Label ID="LBL_MESSAGE" runat="server" ForeColor="Red"></asp:Label>&nbsp;
												<asp:Label ID="LBL_SC_ID" runat="server" Visible="False"></asp:Label>
                    <asp:Button class="btn btn-theme btn-block" ID="signin" Text="CHANGE" runat="server" OnClick="BTN_CHANGE_Click" />
                    <asp:Button class="btn btn-danger btn-block" ID="cancel" Text="CANCEL" runat="server"  /> 
                </div>
            </form>

        </div>
        <h1 style="text-align: center; color: #ffffff"><b>APPLICATION MONITORING<b></h1>
        <h2 style="text-align: center; color: #ffffff">(MONITORING REPORTING SYSTEM)</h2>
    </div>
</body>
</html>
