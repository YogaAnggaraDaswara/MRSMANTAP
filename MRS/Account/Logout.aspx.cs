using CID.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRS.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        #region static vars
        private static string Q_LOGINSCR = "select top 1 LOGIN_SCR from RFMODULE where MODULEID = @1 ";
        private static string SP_USERACTIVITY = "exec SU_ALLUSERACTIVITY @1, @2, '0', '0', '0', @3 ";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "";
            try
            {
                using (DbConnection connESecurity = new DbConnection(authenticate.decryptConnStr(ConfigurationSettings.AppSettings["MRSDATALOGIN"])))
                {
                    int dbtimeout = int.Parse(ConfigurationSettings.AppSettings["dbTimeOut"]);
                    object[] parmodule = new object[1] { ConfigurationSettings.AppSettings["ModuleID"] },
                        paruser = new object[3] { Session["UserID"], Session["GROUPID"], Request.UserHostAddress };

                    connESecurity.ExecReader(Q_LOGINSCR, parmodule, dbtimeout);
                    if (connESecurity.HasRow())
                        url = connESecurity.GetFieldValue(0);

                    connESecurity.ExecuteNonQuery(SP_USERACTIVITY, paruser, dbtimeout);
                }
            }
            catch { }

            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();

            if (Request.QueryString.Keys.Count != 0)
            {
                switch (Request.QueryString[0])
                {
                    case "login":
                        url += "?login";
                        break;
                    case "session":
                        url += "?session=0";
                        break;
                    case "menu":
                        url += "?menu=0";
                        break;
                    default:
                        break;
                }
            }
            //Response.Redirect(url.Trim(), true);
            Response.Write("<html><head><title>Logout</title>");
            Response.Write("<script language='JavaScript'>window.location='" + url + "';</script>");
            Response.Write("</head></html>");
            Response.End();
        }
    }
}