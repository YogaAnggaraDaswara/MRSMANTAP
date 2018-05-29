using CID.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRSs.Account
{
    public partial class Login1 : System.Web.UI.Page
    {
        private enum loginResult
        {
            logNotFound,
            logSuccess,
            logHasLogon,
            logLocked,
            logPwdExpired,
            logPwdEmpty,
            logPwdInvalid,
            logPwdDefault,
            logJustLocked,
            logUserExpired,
            logGrantInvalid,
            logAuthFail,
            logNoLOSAccess,
            logNoMenuAccess,
            logSessionLost,
            logReLogin,
            logUnknown
        }
        private string connectionString;
        private bool logon = false;
        private string hash_password;
        private static string Q_VWLOGIN = "exec SU_USERLOGINGIN @1, @2";
        private static string Q_CHECKREVOKE = "select SU_REVOKE from scalluserflag where USERID = @1 and SU_REVOKE = '1' ";
        private static string SP_TOKENDELETE = "exec ES_APPTOKEN_DELETE @1";
        private static string SP_USERACTIVITY = "exec SU_ALLUSERACTIVITY @1, @2, @3, @4, '1', @5, null, @6 ";
        private static string SP_LOGINSTARTED = "exec SU_LOGINSTARTED @1, @2 ";
        private static string SP_SAVETOKEN = "exec ES_SAVETOKEN @1, @2 ";



        private string _conn = ConfigurationSettings.AppSettings["MRSDATA"];
        private int dbtimeout = int.Parse(ConfigurationSettings.AppSettings["dbTimeOut"]);
         
        protected void Page_Load(object sender, EventArgs e)
        {
            MyPage.SetFocus(this, this.TXT_USERNAME);

        }


        protected void signin_Click(object sender, EventArgs e)
        {
            if (TXT_USERNAME.Text.ToString().Equals(""))
            { 
                Response.Write("<script>alert('User Id Tidak Boleh Kosong')</script>");
                MyPage.SetFocus(this, this.TXT_USERNAME);
                return;
            }
            else if (TXT_PASSWORD.Text.ToString().Equals(""))
            {
                Response.Write("<script>alert('Password Tidak Boleh Kosong')</script>"); 
                MyPage.SetFocus(this, this.TXT_PASSWORD);
                return;
            }

            string nexturl = "";
            if (!this.logon)
            {
                this.hash_password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TXT_PASSWORD.Text, "sha1");
            }

            this.connectionString = Login1.decryptConnStr(ConfigurationSettings.AppSettings["MRSDATALOGIN"]);
            using (DbConnection conn = new DbConnection(this.connectionString))
            {
                try
                {
                    Login1.loginResult flag = Login1.ValidateLogin(this.TXT_USERNAME.Text, this.TXT_PASSWORD.Text, conn, this.dbtimeout, this.logon, base.Request.UserHostAddress);
                    Login1.loginResult loginResult = flag;
                    if (loginResult != Login1.loginResult.logSuccess)
                    {
                        if (loginResult != Login1.loginResult.logPwdExpired)
                        {
                            if (loginResult != Login1.loginResult.logPwdDefault)
                            {
                                this.LogonMessage(flag);
                            }
                            else
                            {
                                System.Web.Security.FormsAuthentication.SetAuthCookie(this.TXT_USERNAME.Text, false);
                                this.Session.Add("UserID", this.TXT_USERNAME.Text);
                                nexturl = "Change_Password.aspx?initial";
                            }
                        }
                        else
                        {
                            System.Web.Security.FormsAuthentication.SetAuthCookie(this.TXT_USERNAME.Text, false);
                            this.Session.Add("sha1", this.hash_password);
                            this.Session.Add("UserID", this.TXT_USERNAME.Text);
                            nexturl = "Change_Password.aspx?expired";
                        }
                    }
                    else
                    {
                        object[] lgparam = new object[]
                            {
                            this.TXT_USERNAME.Text,
                            base.Request.UserHostAddress
                            };
                        conn.ExecuteNonQuery(Login1.SP_LOGINSTARTED, lgparam, this.dbtimeout);
                        System.Web.Security.FormsAuthentication.SetAuthCookie(this.TXT_USERNAME.Text, false);
                        nexturl = this.AuthenticateUser(conn);
                    }
                }
                catch (Exception ex)
                {
                    string errmsg = ex.Message;
                    if (errmsg.IndexOf("Last Query: exec SU_USERLOGINGIN") > 0)
                    {
                        errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
                        this.Label1.Text = errmsg;
                    }
                    else
                    {
                        Response.Write("<!-- ex msg: " + ex.Message.Replace("-->", "--)") + " -->\n");
                        this.LogonMessage(Login1.loginResult.logUnknown);
                    }
                }
            }

            if (nexturl != "")
            {
                Session.Add("ConnString", _conn);
                Session.Add("DbTimeOut", dbtimeout);
                Response.Redirect(nexturl);
            }
            
        }
        private string AuthenticateUser(DbConnection conn)
        {
            string url = "";
            Guid token = Guid.NewGuid();
            object[] objtkn = new object[]
                {
                token,
                this.TXT_USERNAME.Text
                };
            conn.ExecReader(Login1.SP_SAVETOKEN, objtkn, this.dbtimeout);
            if (conn.HasRow())
            {
                string tempurl = conn.GetFieldValue(0);
                if (tempurl.IndexOf("?") >= 0)
                {
                    url = tempurl + "&tkn=" + token.ToString();
                }
                else
                {
                    url = tempurl + "?tkn=" + token.ToString();
                }
            }
            else
            {
                this.LogonMessage(Login1.loginResult.logNoLOSAccess);
            }
            return url;
        }
        private void LogonMessage(Login1.loginResult ret)
        {
            this.Label1.Text = Login1.getLogonMsg(ret, this.TXT_USERNAME.Text);
            if (ret == Login1.loginResult.logPwdInvalid || ret == Login1.loginResult.logPwdEmpty || ret == Login1.loginResult.logJustLocked)
            {
                MyPage.SetFocus(this, this.TXT_PASSWORD);
            }
            else
            {
                MyPage.SetFocus(this, this.TXT_USERNAME);
            }
        }
        private static string getLogonMsg(Login1.loginResult ret, string user)
        {
            string msg = string.Empty;
            switch (ret)
            {
                case Login1.loginResult.logNotFound:
                    if (user != string.Empty)
                    {
                        msg = "Invalid UserID/Password!";
                    }
                    break;
                case Login1.loginResult.logHasLogon:
                    msg = "User is currently logged in!";
                    break;
                case Login1.loginResult.logLocked:
                    msg = "User ID is Locked, Please contact your System Administrator!";
                    break;
                case Login1.loginResult.logPwdEmpty:
                    msg = "Please type in your password...";
                    break;
                case Login1.loginResult.logPwdInvalid:
                    msg = "Invalid UserID/Password";
                    break;
                case Login1.loginResult.logJustLocked:
                    msg = "User ID is Locked, Please contact your System Administrator!";
                    break;
                case Login1.loginResult.logGrantInvalid:
                    msg = "Server Error : Permission Denied for '" + user.ToUpper() + "'";
                    break;
                case Login1.loginResult.logAuthFail:
                    msg = "Login failed. Unable to Authenticate!";
                    break;
                case Login1.loginResult.logNoLOSAccess:
                    msg = "User does not have access to application!";
                    break;
                case Login1.loginResult.logNoMenuAccess:
                    msg = "Menu Access Not Yet Defined For This User.";
                    break;
                case Login1.loginResult.logSessionLost:
                    msg = "Session Lost... Please Login";
                    break;
                case Login1.loginResult.logReLogin:
                    msg = "Please Re-Login";
                    break;
                case Login1.loginResult.logUnknown:
                    msg = "Server Error : Unknown Error";
                    break;
            }
            return msg;
        }
        public static string decryptConnStr(string encryptedConnStr)
        {
            string decpwd = "";
            int pos = encryptedConnStr.IndexOf("pwd=");
            int pos2 = encryptedConnStr.IndexOf(";", pos + 4);
            string encpwd = encryptedConnStr.Substring(pos + 4, pos2 - pos - 4);
            for (int i = 2; i < encpwd.Length; i++)
            {
                char chr = (char)(encpwd[i] - '\u0002');
                decpwd += new string(chr, 1);
            }
            return encryptedConnStr.Replace(encpwd, decpwd);
        }

        private static Login1.loginResult ValidateLogin(string userName, string password, DbConnection conn, int timeout, bool logon, string host)
        {
            object[] user = new object[]
                {
                userName,
                host
                };
            string falsepwd = "0";
            string surevoke = "0";
            conn.ExecReader(Login1.Q_VWLOGIN, user, timeout);
            Login1.loginResult flag;
            if (!conn.HasRow())
            {
                flag = Login1.loginResult.logNotFound;
            }
            else
            {
                surevoke = conn.GetFieldValue("SU_REVOKE");
                string sulogon = conn.GetFieldValue("SU_LOGON");
                string lastfalsecount = conn.GetFieldValue("SU_FALSEPWDCOUNT");
                if (logon)
                {
                    flag = Login1.loginResult.logSuccess;
                }
                else
                {
                    if (System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1") == conn.GetFieldValue("SU_PWD"))
                    {
                        if (conn.GetFieldValue("SU_PWD") == System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(conn.GetFieldValue("CHECKDEFPWD").Trim(), "sha1"))
                        {
                            flag = Login1.loginResult.logPwdDefault;
                        }
                        else
                        {
                            if (conn.GetFieldValue("DEFPWD") == "1")
                            {
                                flag = Login1.loginResult.logPwdDefault;
                            }
                            else
                            {
                                if (conn.GetFieldValue("SU_LOGON") == "1")
                                {
                                    flag = Login1.loginResult.logHasLogon;
                                }
                                else
                                {
                                    if (conn.GetFieldValue("SU_PWDEXPIRED") == "1")
                                    {
                                        flag = Login1.loginResult.logPwdExpired;
                                    }
                                    else
                                    {
                                        flag = Login1.loginResult.logSuccess;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        falsepwd = "1";
                        flag = Login1.loginResult.logPwdInvalid;
                        if (password == string.Empty)
                        {
                            falsepwd = "0";
                            flag = Login1.loginResult.logPwdEmpty;
                        }
                    }
                }
                if (flag != Login1.loginResult.logPwdEmpty)
                {
                    object[] actiparam = new object[]
                        {
                        userName,
                        conn.GetNativeFieldValue("GROUPID"),
                        falsepwd,
                        surevoke,
                        host,
                        sulogon
                        };
                    conn.ExecuteNonQuery(Login1.SP_USERACTIVITY, actiparam, timeout);



                }
            }
            conn.ExecReader(Login1.Q_CHECKREVOKE, user, timeout);
            if (conn.HasRow())
            {
                flag = Login1.loginResult.logLocked;
                if (surevoke == "0" && conn.GetFieldValue("SU_REVOKE") != "0")
                {
                    flag = Login1.loginResult.logJustLocked;
                }
            }
            return flag;
        }
    }
}