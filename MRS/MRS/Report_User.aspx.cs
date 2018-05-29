using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CID.Framework;
using MRS.Models;
using MRS.Web;

namespace MRS.MRS
{
    public partial class REPORT_USER : DataPage
    {

        private static string Q_DeleteUser = "exec SP_DELETEUSER @1";
        private string hash_password = "";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RetreivedataDDL();
                Bind_DataList();

            }

        }
        private void RetreivedataDDL()
        {
            try
            {
                
                StaticFramework.Reff(this.ddl_UNIT, Q_UNIT, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
            }
        }
        public void Bind_DataList()
        {

            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_USER order by SU_FULLNAME asc", null, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_User.Visible = false;
            }
            else
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_User.Visible = true;

            }
        }
        protected void DataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList.PageIndex = e.NewPageIndex;
        }
        protected void DataList_PageIndexChanged(object sender, EventArgs e)
        {


            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_USER where (USERID =  '" + s_userid.Value.ToString() + "' or isnull('" + s_userid.Value.ToString() + "' ,'')='')  and ( SU_FULLNAME ='" + s_nama.Value.ToString() + "' or isnull('" + s_nama.Value.ToString() + "','')='') and ( UNIT ='" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "','')='') order by SU_FULLNAME asc ", null, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_User.Visible = false;
            }
            else
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_User.Visible = true;

            }

        }


        public void OnConfirm(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Button btn = (Button)sender;
                    if (btn.CommandName.Equals("hapus"))
                    {
                        delete_user(btn.CommandArgument.ToString());
                        this.Bind_DataList();

                    }
                    else if (btn.CommandName.Equals("reset"))
                    {
                        reset_user(btn.CommandArgument.ToString());
                    }
                    else if (btn.CommandName.Equals("unblock"))
                    {
                        unblock_user(btn.CommandArgument.ToString());
                        this.Bind_DataList();
                        //Response.Redirect(Request.RawUrl);

                    }


                    // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                }
            }
            catch (Exception)
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Action')", true);
            }

        }
        protected void unblock_user(String userid)
        {
            try
            {

                var dataTable = this.conn.GetDataTable("SELECT  * FROM SCALLUSERFLAG where userid='"+ userid + "' and SU_REVOKE ='0'", null, this.dbtimeout, true, true);
                if (dataTable.Rows.Count > 0)
                {

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User not block');", true);
                    return;

                }
                else {
                    NameValueCollection nameValueCollectionKey = new NameValueCollection();
                    NameValueCollection nameValueCollection = new NameValueCollection();
                    StaticFramework.SaveNvc(nameValueCollectionKey, "USERID", userid);
                    StaticFramework.SaveNvc(nameValueCollection, "SU_REVOKE", "0");
                    StaticFramework.SaveNvc(nameValueCollection, "SU_FALSEPWDCOUNT", "0");
                    StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "SCALLUSERFLAG", this.conn);

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Succes You unblock user ');", true);
                    //return;

                    // Response.Write("<script>alert('Succes You unblock user')</script>");
                    // return;


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Succes You unblock user');", true);
                }


            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error unblock')", true);
            }
        }

        protected void reset_user(String userid)
        {
            try
            {

                hash_password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("bankmantap1", "sha1");

                NameValueCollection nameValueCollectionKey = new NameValueCollection();
                NameValueCollection nameValueCollection = new NameValueCollection();
                StaticFramework.SaveNvc(nameValueCollectionKey, "USERID", userid);
                StaticFramework.SaveNvc(nameValueCollection, "SU_PWD", hash_password);
                StaticFramework.SaveNvc(nameValueCollection, "SU_ACTIVE", "1");
                StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "SCALLUSER", this.conn);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Succes You reset password. Password changed to bankmantap1 ');", true);
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Delete')", true);
            }
        }
        protected void delete_user(String userid)
        {
            try
            {

                object[] param1 = new object[] { userid };
                conn.ExecNonQuery(Q_DeleteUser, param1, dbtimeout);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Success You delete data ');", true);
                return;
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Delete')", true);
            }
        }
        protected void DataList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();

                if (e.CommandName == "View")
                {
                    //Response.Redirect("~/MRS/InputData.aspx?nosurat=" + NOSURAT + "&approve=1");

                    Server.Transfer("~/MRS/InputData.aspx?nosurat=" + NOSURAT + "&approve=1");
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }
        protected void edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string userid_en = btn.CommandArgument.ToString();

            Response.Redirect("~/MRS/InputData_User.aspx?ID=" + userid_en);

            //Server.Transfer("~/MRS/InputData_User.aspx?ID=" + userid_en);

        }
        protected void cari_Click(object sender, EventArgs e)
        {


            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_USER where (USERID =  '" + s_userid.Value.ToString() + "' or isnull('" + s_userid.Value.ToString() + "' ,'')='')  and ( SU_FULLNAME ='" + s_nama.Value.ToString() + "' or isnull('" + s_nama.Value.ToString() + "','')='') and ( UNIT ='" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "','')='') order by SU_FULLNAME asc ", null, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_User.Visible = false;
            }
            else
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_User.Visible = true;

            }


        }


    }
}