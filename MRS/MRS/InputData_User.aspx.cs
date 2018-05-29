using System;
using System.Web;
using System.Web.UI;
using CID.Framework;
using MRS.Web;
using System.Collections.Specialized;
using CID.Tools;
using MRS.Models;

namespace MRS.MRS
{
    public partial class Data_User : DataPage
    {

        private static string Q_GROUP = "SELECT [GROUPID],[SG_GRPNAME] FROM VW_GROUP ORDER BY [SG_GRPNAME] ASC";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string Q_USER = "SELECT [USERID],[GROUPID] ,[SU_FULLNAME]FROM VW_USER WHERE USERID =@1";
        private static string hash_password = "", ID = "";
        private static int PWDEXPDAY = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                RetreivedataDDL();
                if (!Request.QueryString["ID"].ToString().Equals(""))
                {
                    ID = base.Request.QueryString["ID"].ToString();
                    RetrieveData();
                    NIK.Disabled = true;
                    lbl_judul.Text = "Form Edit Data User";
                }

            }
        }
        private void RetreivedataDDL()
        {
            try
            {

                StaticFramework.Reff(this.GROUP, Q_GROUP, null, this.conn, false);
                StaticFramework.Reff(this.UNIT, Q_UNIT, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["ID"].ToString().Equals(""))
            {
                save_data();

            }
            else
            {
                edit_data();
                var page11 = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Update Success');window.location ='Report_User.aspx';", true);
            }
        }
        protected void RetrieveData()
        {
            object[] param = new object[]
           {
                ID,
                this.USERID
           };

            System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_USER] where USERID =@1", param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {

                StaticFramework.Retrieve(dataTable, "USERID", NIK);
                StaticFramework.Retrieve(dataTable, "SU_FULLNAME", FULLNAME);
                StaticFramework.Retrieve(dataTable, "UNIT", this.UNIT);
                StaticFramework.Retrieve(dataTable, "SU_HPNUM", this.NOUSER);
                StaticFramework.Retrieve(dataTable, "SU_EMAIL", this.email);
                StaticFramework.Retrieve(dataTable, "SU_EMAIL_2", this.email2);

                //StaticFramework.Retrieve(dataTable, "SU_HPNUM_DEPHEAD", this.NODEVHEAD);
                //StaticFramework.Retrieve(dataTable, "SU_EMAIL_DEPHEAD", this.emailDevHead);
                //StaticFramework.Retrieve(dataTable, "SU_HPNUM_KADIV", this.NOKADIV);
                //StaticFramework.Retrieve(dataTable, "SU_EMAIL_KADIV", this.emailDivisi);
                //StaticFramework.Retrieve(dataTable, "SU_HPNUM_DIREKTUR", this.NODIREKTUR);
                //StaticFramework.Retrieve(dataTable, "SU_EMAIL_DIREKTUR",this.emailDirektur);
                StaticFramework.Retrieve(dataTable, "GROUPID", this.GROUP);
                //email.Value = dataTable.Rows[0]["SU_EMAIL"].ToString();
                //emailDevHead.Value = dataTable.Rows[0]["SU_EMAIL_DEPHEAD"].ToString();
                //emailDivisi.Value = dataTable.Rows[0]["SU_EMAIL_KADIV"].ToString();
                //emailDirektur.Value = dataTable.Rows[0]["SU_EMAIL_DIREKTUR"].ToString();
                if (GROUP.SelectedValue.Equals("003HEAD"))
                {
                    div_role.Visible = true;
                    div_kadiv.Visible = false;
                    div_penerima.Visible = false;
                    // StaticFramework.Retrieve(dataTable, "SU_HPNUM_KADIV", this.NOKADIV);
                    // StaticFramework.Retrieve(dataTable, "SU_EMAIL_KADIV", this.emailDivisi);

                    StaticFramework.Reff(this.SU_UPLINER, "SELECT USERID,SU_FULLNAME FROM VW_KADIV WHERE UNIT='" + UNIT.SelectedValue.ToString() + "'", null, this.conn, false);
                    StaticFramework.Retrieve(dataTable, "SU_UPLINER", SU_UPLINER);
                    lb_judul.Text = "Kepala Divisi";
                }
                else if (GROUP.SelectedValue.Equals("002PEN"))
                {
                    div_penerima.Visible = true;
                    div_role.Visible = true;
                    lb_judul.Text = "Approval";
                    div_kadiv.Visible = false;

                    StaticFramework.Reff(this.SU_UPLINER, "SELECT USERID,SU_FULLNAME FROM [VW_HEAD] WHERE UNIT='" + UNIT.SelectedValue.ToString() + "'", null, this.conn, false);
                    StaticFramework.Retrieve(dataTable, "SU_UPLINER", SU_UPLINER);

                    StaticFramework.Reff(this.PIC2, "SELECT USERID,SU_FULLNAME FROM VW_PIC2 WHERE UNIT='" + UNIT.SelectedValue.ToString() + "' and USERID not in('" + NIK.Value.ToString() + "')", null, this.conn, false);
                    StaticFramework.Retrieve(dataTable, "USER_DELEGATE", this.PIC2);
                }
                else if (GROUP.SelectedValue.Equals("004KADIV"))
                {
                    div_kadiv.Visible = true;
                    div_role.Visible = false;
                    div_penerima.Visible = false;
                    StaticFramework.Retrieve(dataTable, "SU_HPNUM_DIREKTUR", this.NODIREKTUR);
                    StaticFramework.Retrieve(dataTable, "SU_EMAIL_DIREKTUR", this.emailDirektur);
                }
                else
                {
                    div_role.Visible = false;
                    div_kadiv.Visible = false;
                    div_penerima.Visible = false;
                }

                System.Data.DataTable dataTable1 = this.conn.GetDataTable("select SU_LOGON from SCALLUSERFLAG  where USERID =@1", param, this.dbtimeout, true, true);
                if (dataTable1.Rows.Count > 0)
                {
                    StaticFramework.Retrieve(dataTable1, "SU_LOGON", this.cb_login);
                    if (cb_login.Checked)
                    {
                        cb_login.Text = "Sedang Login";
                    }
                    else
                    {
                        cb_login.Text = "Tidak Login";

                    }
                }
                div_login.Visible = true;
            }


        }
        public void GROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GROUP.SelectedValue.Equals("003HEAD"))
            {
                div_role.Visible = true;
                div_penerima.Visible = false;
                div_kadiv.Visible = false;
                StaticFramework.Reff(this.SU_UPLINER, "SELECT USERID,SU_FULLNAME FROM VW_KADIV WHERE UNIT='" + UNIT.SelectedValue.ToString() + "'", null, this.conn, false);

                lb_judul.Text = "Kepala Divisi";
            }
            else if (GROUP.SelectedValue.Equals("002PEN"))
            {
                div_penerima.Visible = true;
                div_role.Visible = true;
                div_kadiv.Visible = false;
                lb_judul.Text = "Approval";

                StaticFramework.Reff(this.SU_UPLINER, "SELECT USERID,SU_FULLNAME FROM [VW_HEAD] WHERE UNIT='" + UNIT.SelectedValue.ToString() + "'", null, this.conn, false);

                StaticFramework.Reff(this.PIC2, "SELECT USERID,SU_FULLNAME FROM VW_PIC2 WHERE UNIT='" + UNIT.SelectedValue.ToString() + "' and USERID not in('" + NIK.Value.ToString() + "')", null, this.conn, false);
            }
            else if (GROUP.SelectedValue.Equals("004KADIV"))
            {
                div_penerima.Visible = false;
                div_role.Visible = false;
                div_kadiv.Visible = true;
            }

            else
            {
                div_kadiv.Visible = false;
                div_role.Visible = false;
                div_penerima.Visible = false;
            }

        }
        protected void save_data()
        {
            try
            {

                if (base.Request.QueryString["ID"].ToString().Equals(""))
                {

                    System.Data.DataTable dt = this.conn.GetDataTable("select * FROM [MRSDATA].[dbo].[LOGINPARAM]", null, this.dbtimeout, true, true);
                    if (dt.Rows.Count > 0)
                    {
                        PWDEXPDAY = int.Parse(dt.Rows[0]["PWDEXPDAY"].ToString());
                    }

                    object[] param = new object[]
                    {
                    NIK.Value.ToString().ToUpper()
                     };
                    System.Data.DataTable dataTableSms = this.conn.GetDataTable(Q_USER, param, this.dbtimeout, true, true);
                    if (dataTableSms.Rows.Count > 0)
                    {



                        if (dataTableSms.Rows[0]["USERID"].ToString().ToUpper().Equals(NIK.Value.ToString().ToUpper()))
                        {

                            var page1 = HttpContext.Current.CurrentHandler as Page;
                            ScriptManager.RegisterStartupScript(page1, page1.GetType(), "alert", "alert('User id atau NIK : " + NIK.Value.ToString() + " sudah terdaftar');", true);
                            MyPage.SetFocus(this, this.NIK);
                        }
                        else
                        {

                            hash_password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("bankmantap1", "sha1");

                            NameValueCollection nameValueCollectionKey = new NameValueCollection();
                            NameValueCollection nameValueCollection = new NameValueCollection();
                            StaticFramework.SaveNvc(nameValueCollectionKey, "USERID", NIK);
                            StaticFramework.SaveNvc(nameValueCollection, "GROUPID", GROUP);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_FULLNAME", FULLNAME);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL", email);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_2", email2);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_REGISTERBY", Session["UserID"].ToString());
                            StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM", NOUSER.Value.Replace("-", "").Replace(" ", ""));
                            StaticFramework.SaveNvc(nameValueCollection, "SU_PWD", hash_password);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_REGISTERDATE", DateTime.Now);

                            StaticFramework.SaveNvc(nameValueCollection, "SU_PWDEXPDATE", DateTime.Now.AddDays(PWDEXPDAY));

                            StaticFramework.SaveNvc(nameValueCollection, "SU_ACTIVE", "1");
                            StaticFramework.SaveNvc(nameValueCollection, "UNIT", UNIT);

                            if (GROUP.SelectedValue.Equals("003HEAD"))
                            {
                                //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_KADIV", emailDivisi);
                                //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DEPHEAD", emailDevHead);
                                StaticFramework.SaveNvc(nameValueCollection, "SU_UPLINER", SU_UPLINER);
                                //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DEPHEAD", NODEVHEAD.Value.Replace("-", "").Replace(" ", ""));
                                //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_KADIV", NOKADIV.Value.Replace("-", ""));
                            }
                            else if (GROUP.SelectedValue.Equals("002PEN"))
                            {
                                StaticFramework.SaveNvc(nameValueCollection, "USER_DELEGATE", PIC2);
                                StaticFramework.SaveNvc(nameValueCollection, "SU_UPLINER", SU_UPLINER);
                            }
                            else if (GROUP.SelectedValue.Equals("004KADIV"))
                            {
                                StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DIREKTUR", emailDirektur);
                                StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DIREKTUR", NODIREKTUR.Value.Replace("-", "").Replace(" ", ""));

                            }

                            StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "SCALLUSER", this.conn);

                            var page = HttpContext.Current.CurrentHandler as Page;
                            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Save Data Success');", true);


                            NameValueCollection nameValueCollectionKeyflag = new NameValueCollection();
                            NameValueCollection nameValueCollectionflag = new NameValueCollection();
                            StaticFramework.SaveNvc(nameValueCollectionKeyflag, "USERID", NIK);
                            StaticFramework.SaveNvc(nameValueCollectionflag, "SU_LOGON", "0");
                            StaticFramework.SaveNvc(nameValueCollectionflag, "SU_REVOKE", "0");
                            StaticFramework.SaveNvc(nameValueCollectionflag, "SU_FALSEPWDCOUNT", "0");

                            StaticFramework.Save(nameValueCollectionflag, nameValueCollectionKeyflag, "scalluserflag", this.conn);
                            clear();
                        }
                    }
                    else
                    {

                        hash_password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("bankmantap1", "sha1");

                        NameValueCollection nameValueCollectionKey = new NameValueCollection();
                        NameValueCollection nameValueCollection = new NameValueCollection();
                        StaticFramework.SaveNvc(nameValueCollectionKey, "USERID", NIK);
                        StaticFramework.SaveNvc(nameValueCollection, "GROUPID", GROUP);
                        StaticFramework.SaveNvc(nameValueCollection, "SU_FULLNAME", FULLNAME);
                        StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL", email);
                        StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_2", email2);
                        StaticFramework.SaveNvc(nameValueCollection, "SU_REGISTERBY", Session["UserID"].ToString());
                        StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM", NOUSER.Value.Replace("-", "").Replace(" ", ""));
                        StaticFramework.SaveNvc(nameValueCollection, "SU_PWD", hash_password);
                        StaticFramework.SaveNvc(nameValueCollection, "SU_REGISTERDATE", DateTime.Now);
                        StaticFramework.SaveNvc(nameValueCollection, "SU_PWDEXPDATE", DateTime.Now.AddDays(PWDEXPDAY));
                        StaticFramework.SaveNvc(nameValueCollection, "SU_ACTIVE", "1");
                        StaticFramework.SaveNvc(nameValueCollection, "UNIT", UNIT);

                        if (GROUP.SelectedValue.Equals("003HEAD"))
                        {
                            //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_KADIV", emailDivisi);
                            //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DEPHEAD", emailDevHead);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_UPLINER", SU_UPLINER);
                            //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DEPHEAD", NODEVHEAD.Value.Replace("-", "").Replace(" ", ""));
                            //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_KADIV", NOKADIV.Value.Replace("-", ""));
                        }
                        else if (GROUP.SelectedValue.Equals("002PEN"))
                        {
                            StaticFramework.SaveNvc(nameValueCollection, "USER_DELEGATE", PIC2);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_UPLINER", SU_UPLINER);
                        }
                        else if (GROUP.SelectedValue.Equals("004KADIV"))
                        {
                            StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DIREKTUR", emailDirektur);
                            StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DIREKTUR", NODIREKTUR.Value.Replace("-", "").Replace(" ", ""));

                        }

                        StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "SCALLUSER", this.conn);

                        var page = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Save Data Success');", true);

                        NameValueCollection nameValueCollectionKeyflag = new NameValueCollection();
                        NameValueCollection nameValueCollectionflag = new NameValueCollection();
                        StaticFramework.SaveNvc(nameValueCollectionKeyflag, "USERID", NIK);
                        StaticFramework.SaveNvc(nameValueCollectionflag, "SU_LOGON", "0");
                        StaticFramework.SaveNvc(nameValueCollectionflag, "SU_REVOKE", "0");
                        StaticFramework.SaveNvc(nameValueCollectionflag, "SU_FALSEPWDCOUNT", "0");

                        StaticFramework.Save(nameValueCollectionflag, nameValueCollectionKeyflag, "scalluserflag", this.conn);

                        clear();
                    }
                }



            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error save');", true);
            }
        }
        protected void edit_data()
        {
            try
            {
                NameValueCollection nameValueCollectionKey = new NameValueCollection();
                NameValueCollection nameValueCollection = new NameValueCollection();
                StaticFramework.SaveNvc(nameValueCollectionKey, "USERID", NIK);
                StaticFramework.SaveNvc(nameValueCollection, "GROUPID", GROUP);
                StaticFramework.SaveNvc(nameValueCollection, "SU_FULLNAME", FULLNAME);
                StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL", email);
                StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_2", email2);
                //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_KADIV", emailDivisi);
                //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DEPHEAD", emailDevHead);
                //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DIREKTUR", emailDirektur);
                //StaticFramework.SaveNvc(nameValueCollection, "SU_REGISTERBY", "TEST");
                StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM", NOUSER.Value.Replace("-", "").Replace(" ", ""));
                //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DEPHEAD", NODEVHEAD.Value.Replace("-", "").Replace(" ", ""));
                //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_KADIV", NOKADIV.Value.Replace("-", ""));
                //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DIREKTUR", NODIREKTUR.Value.Replace("-", "").Replace(" ", ""));
                //StaticFramework.SaveNvc(nameValueCollection, "SU_PWD", hash_password);
                // StaticFramework.SaveNvc(nameValueCollection, "SU_REGISTERDATE", DateTime.Now);
                //StaticFramework.SaveNvc(nameValueCollection, "SU_PWDEXPDATE", DateTime.Now.AddYears(5));
                // StaticFramework.SaveNvc(nameValueCollection, "SU_ACTIVE", "1");
                StaticFramework.SaveNvc(nameValueCollection, "UNIT", UNIT);

                if (GROUP.SelectedValue.Equals("003HEAD"))
                {
                    //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_KADIV", emailDivisi);
                    //StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DEPHEAD", emailDevHead);
                    StaticFramework.SaveNvc(nameValueCollection, "SU_UPLINER", SU_UPLINER);
                    //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DEPHEAD", NODEVHEAD.Value.Replace("-", "").Replace(" ", ""));
                    //StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_KADIV", NOKADIV.Value.Replace("-", ""));
                }
                else if (GROUP.SelectedValue.Equals("002PEN"))
                {
                    StaticFramework.SaveNvc(nameValueCollection, "USER_DELEGATE", PIC2);
                    StaticFramework.SaveNvc(nameValueCollection, "SU_UPLINER", SU_UPLINER);
                }
                else if (GROUP.SelectedValue.Equals("004KADIV"))
                {
                    StaticFramework.SaveNvc(nameValueCollection, "SU_EMAIL_DIREKTUR", emailDirektur);
                    StaticFramework.SaveNvc(nameValueCollection, "SU_HPNUM_DIREKTUR", NODIREKTUR.Value.Replace("-", "").Replace(" ", ""));

                }

                StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "SCALLUSER", this.conn);

                NameValueCollection nameValueCollectionKey1 = new NameValueCollection();
                NameValueCollection nameValueCollection1 = new NameValueCollection();
                StaticFramework.SaveNvc(nameValueCollectionKey1, "USERID", NIK);
                StaticFramework.SaveNvc(nameValueCollection1, "SU_LOGON", cb_login);

                StaticFramework.Save(nameValueCollection1, nameValueCollectionKey1, "SCALLUSERFLAG", this.conn);
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error save');", true);
            }
        }

        protected void clear()
        {
            NIK.Value = "";
            UNIT.ClearSelection();
            GROUP.ClearSelection();
            FULLNAME.Value = "";
            email.Value = "";
            email2.Value = "";
            //emailDivisi.Value = "";
            //emailDevHead.Value = "";
            //emailDirektur.Value = "";
            NOUSER.Value = "";
            //NODEVHEAD.Value = "";
            //NOKADIV.Value = "";
            //NODIREKTUR.Value = "";
            if (GROUP.SelectedValue.Equals("003HEAD"))
            {
                SU_UPLINER.ClearSelection();
            }
            else if (GROUP.SelectedValue.Equals("002PEN"))
            {
                PIC2.ClearSelection();
                SU_UPLINER.ClearSelection();
            }
            else if (GROUP.SelectedValue.Equals("004KADIV"))
            {

                emailDirektur.Value = "";
                NODIREKTUR.Value = "";
            }
        }
    }
}