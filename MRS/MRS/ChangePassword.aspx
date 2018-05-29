<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="ChangePassword.aspx.cs" Inherits="MRS.Account.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

        var warn_window = null;
        var can_close = true;
        var appurl = '';
        function warn_close(nochk) {
            if (!nochk && !can_close) return;
            if (warn_window != null)
                try {
                    warn_window.opener = null;
                    warn_window.close();
                    warn_window = null;
                } catch (e) { }
        }
        function warn_timeout() {
            if (warn_window != null) return;
            var X = (screen.availWidth - 380) / 2;
            var Y = (screen.availHeight - 350) / 2;

            try { app_url = document.frames(1).document.location.href; }
            catch (e) { }
            can_close = false;
            window.focus();
            warn_window = window.open("warning.html", "losmnt",
                "height=350px,width=380px,left=" + X + ",top=" + Y +
                ",status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes");
        }
        function warn_closed() {
            warn_window = null;
            try {
                if (document.frames(2).document.readyState == 'interactive' || document.frames(2).document.readyState == 'complete')
                    document.frames(2).document.parentWindow.execScript('reset_post()');
            } catch (e) { }
        }
        function logout_now() {
            warn_close(true);
            top.location.href = '../Account/Logout.aspx';
        }
        function postback() {
            form1.post_cnt.value = eval(form1.post_cnt.value + '+1');
            form1.submit();
        }
        function set_post() {
            try {
                if (form1.post_cnt.value == '4') window.parent.execScript('warn_timeout()');
                if (form1.post_cnt.value == '6') window.parent.execScript('logout_now()');
                else setTimeout('postback()', form1.Session_Time.value);
            } catch (e) { }
        }
        function reset_post() {
            form1.post_cnt.value = 0;
            //clearTimeout(timer);
            //set_post();
        }
    </script>

    <%--<script type="text/javascript">
        $(function () {
            $("#chkShowPassword").bind("click", function () {
                var txtPassword = $("#TXT_LAMA");
                var txtPassword1 = $("#TXT_BARU");
                var txtPassword2 = $("#TXT_BARUVER");
                if ($(this).is(":checked")) {
                    alert("masuk1" + txtPassword);
                    alert("masuk 1" + txtPassword.attr("id"));
                    txtPassword.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "MainContent_' + txtPassword.attr("id") + '" type = "text" value = "' + txtPassword.val() + '" />');
                    txtPassword.hide();
                    txtPassword2.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "MainContent_' + txtPassword2.attr("id") + '" type = "text" value = "' + txtPassword2.val() + '" />');
                    txtPassword2.hide();
                    txtPassword1.after('<input class="form-control"  onchange = "PasswordChanged(this);" id = "MainContent_' + txtPassword1.attr("id") + '" type = "text" value = "' + txtPassword1.val() + '" />');
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
    </script>--%>
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="CHANGE PASSWORD"></asp:Label>
            </h3>

            <div class="col-lg-12 col-md-12 col-sm-12">
                <! -- MODALS -->
      				<div class="showback">
                          <form class="form-horizontal style-form" runat="server">
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Old Password
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:TextBox ID="TXT_LAMA" class="form-control" onkeypress="return kutip_satu()" placeholder="Old Password" runat="server" required="required" TextMode="Password"></asp:TextBox>
                                  </div>
                              </div>
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      New Password
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:TextBox ID="TXT_BARU" class="form-control" onkeypress="return kutip_satu()" placeholder="New Password" runat="server" required="required" TextMode="Password"></asp:TextBox>
                                  </div>
                              </div>
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Verification New Password
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:TextBox ID="TXT_BARUVER" class="form-control" onkeypress="return kutip_satu()" placeholder="Verification New Password" runat="server" required="required" TextMode="Password"></asp:TextBox>
                                  </div>
                              </div>
                              <div class="form-group">
                                  <div class="col-sm-12">
                                     <%-- <label for="chkShowPassword">
                                          <input type="checkbox" id="chkShowPassword" />
                                          Show password</label>--%>

                                      <label for="chkShowPassword1">
                                      <asp:CheckBox ID="CheckBox1" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                      Show password</label><br />

                                      <asp:Label ID="LBL_MESSAGE" runat="server" ForeColor="Red"></asp:Label>&nbsp;
												<asp:Label ID="LBL_SC_ID" runat="server" Visible="False"></asp:Label>
                                  </div>


                              </div>
                              <div class="form-group">
                                  <div class="col-sm-12">
                                      <asp:Button ID="BTN_CHANGE" runat="server" Text="Change"
                                          class="btn btn-primary btn-lg btn-block" OnClick="BTN_CHANGE_Click"></asp:Button>
                                  </div>
                              </div>
                          </form>

                      </div>
            </div>
        </section>
    </section>
</asp:Content>
