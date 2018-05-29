using CID.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRS.Account
{
    public partial class authenticate : System.Web.UI.Page
    {

        private static string Q_LOGINSCR = "select top 1 LOGIN_SCR from RFMODULE where MODULEID = @1 ";
        private static string Q_ES_TOKEN = "select USERID, GROUPID, MODULEID, DB_IP, DB_NAMA, DB_LOGINID, DB_LOGINPWD  from VW_ES_APPTOKEN where TOKENID = @1 ";
        private static string Q_USERDATA = "select USERID, GROUPID, SU_FULLNAME, BRANCHID, SG_GRPNAME,ISFRONTING,ISLF,UNIT from VW_SESSION where USERID = @1 ";
        private static string SP_TOKENDELETE = "EXEC ES_APPTOKEN_DELETE @1";

        public static string decryptConnStr(string encryptedConnStr)
        {
            string text = "";
            int num = encryptedConnStr.IndexOf("pwd=");
            int num2 = encryptedConnStr.IndexOf(";", num + 4);
            string text2 = encryptedConnStr.Substring(num + 4, num2 - num - 4);
            for (int i = 2; i < text2.Length; i++)
            {
                char c = (char)(text2[i] - '\u0002');
                text += new string(c, 1);
            }
            return encryptedConnStr.Replace(text2, text);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            int timeout = int.Parse(ConfigurationSettings.AppSettings["dbTimeOut"]);
            DbConnection dbConnection = new DbConnection(authenticate.decryptConnStr(ConfigurationSettings.AppSettings["MRSDATALOGIN"]));
            string url = "";
            object[] param = new object[]
            {
                new Guid(base.Request.QueryString["tkn"])
            };
            object[] param2 = new object[]
            {
                ConfigurationSettings.AppSettings["ModuleID"]
            };
            dbConnection.ExecReader(authenticate.Q_LOGINSCR, param2, timeout);
            if (dbConnection.HasRow())
            {
                url = dbConnection.GetFieldValue(0);
            }
            dbConnection.ExecReader(authenticate.Q_ES_TOKEN, param, timeout);
            if (dbConnection.HasRow())
            {
                string fieldValue = dbConnection.GetFieldValue(0);
                string fieldValue2 = dbConnection.GetFieldValue(1);
                string fieldValue3 = dbConnection.GetFieldValue(2);
                string fieldValue4 = dbConnection.GetFieldValue(3);
                string fieldValue5 = dbConnection.GetFieldValue(4);
                string fieldValue6 = dbConnection.GetFieldValue(5);
                string fieldValue7 = dbConnection.GetFieldValue(6);
                dbConnection.ExecuteNonQuery(authenticate.SP_TOKENDELETE, param, timeout);
                string text = "Pooling=true";
                if (ConfigurationSettings.AppSettings["DbOtherSetting"] != null && ConfigurationSettings.AppSettings["DbOtherSetting"].Trim() != "")
                {
                    text = ConfigurationSettings.AppSettings["DbOtherSetting"];
                }
                if (this.AddSession(fieldValue, string.Concat(new string[]
                {
                    "Data Source=",
                    fieldValue4,
                    ";Initial Catalog=",
                    fieldValue5,
                    ";uid=",
                    fieldValue6,
                    ";pwd=",
                    fieldValue7,
                    ";",
                    text
                }), timeout))
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(fieldValue, false);
                    if (base.Request.QueryString["tl"] != null && base.Request.QueryString["tl"] == "1")
                    {
                        url = "mainTestConn.aspx";
                    }
                    else
                    {
                        url = "../MRS/Dashboard.aspx";
                    }
                }
            }
            dbConnection.Dispose();
            base.Response.Redirect(url, true);
        }
        private bool AddSession(string uid, string connstr, int timeout)
        {
            bool result = false;
            using (DbConnection dbConnection = new DbConnection(authenticate.decryptConnStr(ConfigurationSettings.AppSettings["MRSDATALOGIN"])))
            {
                object[] param = new object[]
                {
                    uid
                };
                dbConnection.ExecReader(authenticate.Q_USERDATA, param, timeout);
                if (dbConnection.HasRow())
                {
                    this.Session.Add("UserID", dbConnection.GetFieldValue("USERID"));
                    this.Session.Add("FullName", dbConnection.GetFieldValue("SU_FULLNAME"));
                    this.Session.Add("GroupID", dbConnection.GetFieldValue("GROUPID"));
                    this.Session.Add("GrpFronting", dbConnection.GetFieldValue("ISFRONTING"));
                    this.Session.Add("GrpLF", dbConnection.GetFieldValue("ISLF"));
                    if (dbConnection.GetFieldValue("SG_GRPNAME").Trim() != "")
                    {
                        this.Session.Add("GroupName", "(" + dbConnection.GetFieldValue("SG_GRPNAME") + ")");
                    }
                    
                    this.Session.Add("UnitID", dbConnection.GetFieldValue("UNIT"));
                    this.Session.Add("BranchID", dbConnection.GetFieldValue("BRANCHID"));
                    this.Session.Add("ModuleID", ConfigurationSettings.AppSettings["ModuleID"]);
                    this.Session.Add("ConnString", connstr);
                    this.Session.Add("dbTimeOut", timeout);
                    this.Session.Add("dbBigTimeOut", int.Parse(ConfigurationSettings.AppSettings["dbBigTimeOut"]));
                    this.Session.Add("LoginTime", DateTime.Now);
                    result = true;
                }
            }
            return result;
        }
    }
}