using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRS
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count > 0)
            {


                string S_USERID = Session["UserID"].ToString();
                string S_GROUPID = Session["GROUPID"].ToString();
                lbl_user.Text = Session["FullName"].ToString();
                lbl_role.Text = Session["GroupName"].ToString();


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
                if (S_GROUPID.ToString().Equals("003HEAD"))//DEPT. HEAD
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