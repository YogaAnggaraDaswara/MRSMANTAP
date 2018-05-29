using CID.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRS.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private static string Q_OLDPWD = "select SU_PWD from SCALLUSER where USERID = @1 ";
        private static string Q_VALIDATEPOLICY = "select dbo.isPwdValid (@1, @2, @3) ";
        private static string Q_MODULEDB = "SELECT DB_IP, DB_NAMA, DB_LOGINID, DB_LOGINPWD FROM SCALLUSER U JOIN GRPACCESSMODULE G ON G.GROUPID = U.GROUPID JOIN RFMODULE M ON M.MODULEID = G.MODULEID WHERE U.USERID = @1 ";
        private static string SP_USRPWDALL = "exec SU_SCALLUSERPASSWORD @1, @2 ";
        private static string SP_USRPWD = "exec SU_SCUSERPASSWORD @1, @2 ";
        private static string s_TXT_LAMA = "", s_TXT_BARU = "", s_BARUVER = "";


        private DbConnection connESecurity;
        private int dbtimeout;

        protected void Page_Load(object sender, EventArgs e)
        {
           if (!base.IsPostBack)
            {
                if (base.Request.QueryString.Keys.Count != 0)
                {
                    if (base.Request.QueryString[0] == "expired")
                    {
                        this.LBL_MESSAGE.Text = "Password has expired, please change your password.";
                    }
                    else
                    {
                        if (base.Request.QueryString[0] == "initial")
                        {
                            this.TXT_LAMA.Enabled = false;
                            this.LBL_MESSAGE.Text = "Please change your login password.";
                        }
                    }
                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            s_TXT_LAMA = TXT_LAMA.Text;
            s_TXT_BARU = TXT_BARU.Text;
            s_BARUVER = TXT_BARUVER.Text;

            if (CheckBox1.Checked)
            {
                TXT_LAMA.TextMode = TextBoxMode.SingleLine;
                TXT_BARU.TextMode = TextBoxMode.SingleLine;
                TXT_BARUVER.TextMode = TextBoxMode.SingleLine;

            }
            else
            {

                TXT_LAMA.TextMode = TextBoxMode.Password;
                TXT_BARU.TextMode = TextBoxMode.Password;
                TXT_BARUVER.TextMode = TextBoxMode.Password;

                //TXT_LAMA.Text = s_TXT_LAMA;
                //TXT_BARU.Text = s_TXT_BARU;
                //TXT_BARUVER.Text = s_BARUVER;
                TXT_LAMA.Attributes.Add("value", s_TXT_LAMA);
                TXT_BARU.Attributes.Add("value", s_TXT_BARU);
                TXT_BARUVER.Attributes.Add("value", s_BARUVER);
            }

        }
        protected void BTN_CHANGE_Click(object sender, EventArgs e)
        {


            if (this.TXT_BARU.Text.Trim() != this.TXT_BARUVER.Text.Trim())
            {
                this.LBL_MESSAGE.Text = "Password mismatch!";
                this.Clear();
            }
            else
            {
                int dbtimeout = int.Parse(ConfigurationSettings.AppSettings["dbTimeOut"]);
                DbConnection conn = new DbConnection(MRSs.Account.Login1.decryptConnStr(ConfigurationSettings.AppSettings["MRSDATALOGIN"]));
                string oldPassword = "";
                string dbPassword = "";
                object[] user = new object[]
                    {
                    this.Session["UserID"]
                    };
                if (this.TXT_LAMA.Enabled)
                {
                    oldPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.TXT_LAMA.Text.Trim(), "sha1");
                    conn.ExecReader(ChangePassword.Q_OLDPWD, user, dbtimeout);
                    if (conn.HasRow())
                    {
                        dbPassword = conn.GetFieldValue(0);
                    }
                }
                if (!this.TXT_LAMA.Enabled || oldPassword == dbPassword)
                {
                    string newPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.TXT_BARU.Text, "sha1");
                    object[] parnew = new object[]
                        {
                        this.Session["UserID"],
                        this.TXT_BARU.Text,
                        newPassword
                        };
                    conn.ExecReader(ChangePassword.Q_VALIDATEPOLICY, parnew, dbtimeout);
                    if (conn.HasRow())
                    {
                        if (conn.GetFieldValue(0) == "")
                        {
                            parnew = new object[]
                                {
                                this.Session["UserID"].ToString(),
                                newPassword
                                };
                            conn.ExecuteNonQuery(ChangePassword.SP_USRPWDALL, parnew, dbtimeout);
                            conn.ExecReader(ChangePassword.Q_MODULEDB, user, dbtimeout);
                            while (conn.HasRow())
                            {
                                string connectionString = string.Concat(new string[]
                                    {
                                    "Data Source=",
                                    conn.GetFieldValue(0),
                                    ";Initial Catalog=",
                                    conn.GetFieldValue(1),
                                    ";uid=",
                                    conn.GetFieldValue(2),
                                    ";pwd=",
                                    conn.GetFieldValue(3),
                                    ";Pooling=true"
                                    });
                                using (DbConnection lclConn = new DbConnection(connectionString))
                                {
                                    lclConn.ExecuteNonQuery(ChangePassword.SP_USRPWD, parnew, dbtimeout);
                                }
                            }
                            this.LBL_MESSAGE.Text = "";
                            this.Clear();
                            this.Session.Remove("sha1");

                            var page11 = HttpContext.Current.CurrentHandler as Page;
                            ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Password Updated! Please Re-login Application');", true);
                            // ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Password Updated! Please Re-login Application');window.location ='../Account/Logout.aspx';", true);

                            //base.Response.Write("<script for=window event=onload language=javascript>\nalert('Password Updated!');</script>");
                            //Response.Redirect("../Account/Logout.aspx");
                        }
                        else
                        {
                            this.LBL_MESSAGE.Text = conn.GetFieldValue(0);
                            this.Clear();
                        }
                    }
                }
                else
                {
                    this.LBL_MESSAGE.Text = "Old Password invalid!";
                    this.Clear();
                }
                conn.Dispose();
            }
        }
        private void Clear()
        {
            this.TXT_BARU.Text = "";
            this.TXT_BARUVER.Text = "";
            this.TXT_LAMA.Text = "";
            if (this.TXT_LAMA.Enabled)
            {
                MyPage.SetFocus(this, TXT_LAMA);
            }
            else
            {
                MyPage.SetFocus(this, TXT_BARU);
            }

        }
    }
}