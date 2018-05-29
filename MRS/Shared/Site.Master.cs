using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace MRS
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session.Count > 0)
            {


                string S_USERID = Session["UserID"].ToString();
                string S_GROUPID = Session["GROUPID"].ToString();
                lbl_user.Text = Session["FullName"].ToString();
                lbl_role.Text = Session["UserID"].ToString() +" - "+Session["GroupName"].ToString();
                

                if (S_GROUPID.ToString().Equals("00")) //ADMIN
                {
                    L_DATA.Style.Add("display", "");
                    L_INPUTDATAALERT.Style.Add("display", "");
                    L_DATAALERT.Style.Add("display", "");
                    L_INPUTDATAKOMENTAR.Style.Add("display", "");
                    L_DATAKOMENTAR.Style.Add("display", "");

                    L_REPORT.Style.Add("display", "");
                    L_RAPPROVAL.Style.Add("display", "");
                    L_RALERT.Style.Add("display", "");
                    L_RKOMENTAR.Style.Add("display", "");

                    L_MAINTENANCE.Style.Add("display", "");
                    L_INPUTUSER.Style.Add("display", "");
                    L_DATAUSER.Style.Add("display", "");
                    //L_REGULATOR.Style.Add("display", "");
                    //L_DATAREGULATOR.Style.Add("display", "");
                    L_LAPORAN.Style.Add("display", "");
                    L_DATALAPORAN.Style.Add("display", "");

                }
                else
                if (S_GROUPID.ToString().Equals("001COM")) //USER COMPLIANCE
                {
                    L_DATA.Style.Add("display", "");
                    L_INPUTDATAALERT.Style.Add("display", "");
                    L_DATAALERT.Style.Add("display", "");
                    L_INPUTDATAKOMENTAR.Style.Add("display", "NONE");
                    L_DATAKOMENTAR.Style.Add("display", "NONE");

                    L_REPORT.Style.Add("display", "");
                    L_RAPPROVAL.Style.Add("display", "NONE");
                    L_RALERT.Style.Add("display", "");
                    L_RKOMENTAR.Style.Add("display", "");

                    L_MAINTENANCE.Style.Add("display", "NONE");
                    L_INPUTUSER.Style.Add("display", "NONE");
                    L_DATAUSER.Style.Add("display", "NONE");
                    //L_REGULATOR.Style.Add("display", "NONE");
                    //L_DATAREGULATOR.Style.Add("display", "NONE");
                    L_LAPORAN.Style.Add("display", "NONE");
                    L_DATALAPORAN.Style.Add("display", "NONE");

                }
                else
                if (S_GROUPID.ToString().Equals("002PEN")) //USER PENERIMA ALERT
                {
                    L_DATA.Style.Add("display", "");
                    L_INPUTDATAALERT.Style.Add("display", "NONE");
                    L_DATAALERT.Style.Add("display", "NONE");
                    L_INPUTDATAKOMENTAR.Style.Add("display", "");
                    L_DATAKOMENTAR.Style.Add("display", "");

                    L_REPORT.Style.Add("display", "NONE");
                    L_RAPPROVAL.Style.Add("display", "NONE");
                    L_RALERT.Style.Add("display", "NONE");
                    L_RKOMENTAR.Style.Add("display", "NONE");

                    L_MAINTENANCE.Style.Add("display", "NONE");
                    L_INPUTUSER.Style.Add("display", "NONE");
                    L_DATAUSER.Style.Add("display", "NONE");
                   // L_REGULATOR.Style.Add("display", "NONE");
                   // L_DATAREGULATOR.Style.Add("display", "NONE");
                    L_LAPORAN.Style.Add("display", "NONE");
                    L_DATALAPORAN.Style.Add("display", "NONE");

                }
                else
                if (S_GROUPID.ToString().Equals("003HEAD") || S_GROUPID.ToString().Equals("004KADIV"))//DEPT. HEAD
                {
                    L_DATA.Style.Add("display", "NONE");
                    L_INPUTDATAALERT.Style.Add("display", "NONE");
                    L_DATAALERT.Style.Add("display", "NONE");
                    L_INPUTDATAKOMENTAR.Style.Add("display", "NONE");
                    L_DATAKOMENTAR.Style.Add("display", "NONE");

                    L_REPORT.Style.Add("display", "");
                    L_RAPPROVAL.Style.Add("display", "");
                    L_RALERT.Style.Add("display", "NONE");
                    L_RKOMENTAR.Style.Add("display", "NONE");

                    L_MAINTENANCE.Style.Add("display", "NONE");
                    L_INPUTUSER.Style.Add("display", "NONE");
                    L_DATAUSER.Style.Add("display", "NONE");
                   // L_REGULATOR.Style.Add("display", "NONE");
                   // L_DATAREGULATOR.Style.Add("display", "NONE");
                    L_LAPORAN.Style.Add("display", "NONE");
                    L_DATALAPORAN.Style.Add("display", "NONE");

                }
            }
            else
            {

                base.Response.Write("<script for=window event=onload language=javascript>\nalert('Time Out!');</script>");
                Response.Redirect("../Account/Logout.aspx");
            }

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}