using System;
using System.Web.UI;
using CID.Tools;
using System.Configuration;

namespace MRS.Web
{
    public class Masterpages : Page
    {
        protected DbConnection conn;
        protected int dbtimeout;
        protected string USERID, GROUPID;

        protected override void OnInit(EventArgs e)
        {
            if (Session["ConnString"] == null || Session["ConnString"].ToString().Trim() == "")
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }
            dbtimeout = (int)Session["DbTimeOut"];
            conn = new DbConnection((string)Session["ConnString"]);
            USERID = (string)Session["UserID"];
            GROUPID = (string)Session["GroupID"];           

            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            try
            {
                conn.Dispose();
            }
            catch { }
        }        
    }

    public class MasterUserControl : System.Web.UI.UserControl
    {
        protected DbConnection conn;
        protected int dbtimeout;
        protected string USERID, GROUPID;

        protected override void OnInit(EventArgs e)
        {
            dbtimeout = (int)Session["DbTimeOut"];
            conn = new DbConnection((string)Session["ConnString"]);
            USERID = (string)Session["UserID"];
            GROUPID = (string)Session["GroupID"];

            base.OnInit(e);
        }


        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            try
            {
                conn.Dispose();
            }
            catch { }
        }
    }

    public class DataPage : Masterpages
    {
        protected bool allowViewState = false;
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.Request.QueryString["readonly"] != null)
                CID.Framework.ModuleSupport.DisableControls(this, allowViewState);

            DbConnection conn = new DbConnection(this.Session["ConnString"].ToString());
            string urlPath = this.Request.Url.AbsolutePath.ToString();
            string regno = this.Request.QueryString["regno"];
            string title = this.Request.QueryString["title"];
            string[] splitPath = urlPath.Split('/');
            object[] param = new object[] { urlPath, splitPath[splitPath.Length - 1] };

            //conn.ExecReader("SELECT 1 FROM RFAUDITTRAIL WHERE AU_LOGOPEN=1", null, dbtimeout);
            //if (conn.hasRow())
            //{
            //    conn.ExecReader("EXEC USP_GETMENU @2", param, dbtimeout);
            //    if (conn.hasRow())
            //    {
            //        string urlPage = conn.GetFieldValue("MENUURL").ToString();
            //        //conn.ExecNonQuery("INSERT INTO AUDITTRAIL VALUES (@1, @2, @3, @4, @5, @6)", new object[] { DateTime.Now, this.Session["USERID"], this.Request.UserHostAddress, "Opening Page", regno, "Opening Page : " + title + " - " + urlPage + " - " + urlPath }, dbtimeout);
            //        conn.ExecNonQuery("EXEC SP_AUDITTRAIL @1, @2, @3, @4, @5, @6", new object[] { DateTime.Now, this.Session["USERID"], this.Request.UserHostAddress, "Opening Page", regno, "Opening Page : " + title + " - " + urlPage + " - " + urlPath }, dbtimeout);
            //    }
            //}
        }
        
    }
      

    public class DataUserControl : MasterUserControl
    {
        protected bool allowViewState = false;
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.Request.QueryString["readonly"] != null)
                CID.Framework.ModuleSupport.DisableControls(this, allowViewState);

            DbConnection conn = new DbConnection(this.Session["ConnString"].ToString());
            string urlPath = this.Request.Url.AbsolutePath.ToString();
            string regno = this.Request.QueryString["regno"];
            string title = this.Request.QueryString["title"];
            string[] splitPath = urlPath.Split('/');
            object[] param = new object[] { urlPath, splitPath[splitPath.Length - 1] };

            //conn.ExecReader("SELECT 1 FROM RFAUDITTRAIL WHERE AU_LOGOPEN=1", null, dbtimeout);
            //if (conn.hasRow())
            //{
            //    conn.ExecReader("EXEC USP_GETMENU @2", param, dbtimeout);
            //    if (conn.hasRow())
            //    {
            //        //string urlPage = conn.GetFieldValue("SM_MENUDISPLAY").ToString();
            //        string urlPage = conn.GetFieldValue("MENUURL").ToString();
            //        //conn.ExecNonQuery("INSERT INTO AUDITTRAIL VALUES (@1, @2, @3, @4, @5, @6)", new object[] { DateTime.Now, this.Session["USERID"], this.Request.UserHostAddress, "Opening Page", regno, "Opening Page : " + title + " - " + urlPage + " - " + urlPath }, dbtimeout);
            //        conn.ExecNonQuery("EXEC SP_AUDITTRAIL @1, @2, @3, @4, @5, @6", new object[] { DateTime.Now, this.Session["USERID"], this.Request.UserHostAddress, "Opening Page", regno, "Opening Page : " + title + " - " + urlPage + " - " + urlPath }, dbtimeout);
            //    }
            //}
        }
    }


}
