using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CID.Framework;
using MRS.Web;
using System.Data;
using System.Collections.Specialized;
using System.Drawing;
using MRS.Models;

namespace MRS.Report
{
    public partial class ReportMRS : DataPage
    {

        private static string Q_Delete = "exec SP_DELETEAPP @1, @2";
        private static string Q_INSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI order by INSTANSINAME asc";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind_DataList();
                RetreivedataDDL();
            }
        }

        private void RetreivedataDDL()
        {
            try
            {

                if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
                {
                    StaticFramework.Reff(this.FORWARD, "select USERID, SU_FULLNAME  from VW_FORWARD where GROUPID in('003HEAD','004KADIV')  ", null, this.conn, false);

                }
                else {

                    StaticFramework.Reff(this.FORWARD, "select USERID, SU_FULLNAME  from VW_FORWARD where UNIT = '" + Session["Unitid"].ToString() + "'and GROUPID in('003HEAD','004KADIV')  and USERID not in (select distinct DEPTHEAD from DATA_COMPLIANCE where pic = '" + Session["userid"].ToString() + "')", null, this.conn, false);

                }
                StaticFramework.Reff(this.INSTANSIID, Q_INSTANSI, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
            }
        }
        public void Bind_DataList()
        {
            DataTable dt = new DataTable();
            if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS  order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else
            {

                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where USERCREATED='" + Session["userid"].ToString() + "' order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }


            if (dt.Rows.Count > 0)
            {
                DataList.DataSource = dt;
                DataList.DataBind();
                lb_data.Visible = false;
            }
            else
            {
                DataList.DataSource = dt;
                DataList.DataBind();
                lb_data.Visible = true;
            }

        }
        protected void cari_Click(object sender, EventArgs e)
        {


            FORWARD.Style.Remove("data-val-required");
            KET.Style.Remove("data-val-required");
            //var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where USERCREATED='" + Session["userid"].ToString() + "' and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( PERIHAL ='" + PERIHAL.Value.ToString() + "' or isnull('" + PERIHAL.Value.ToString() + "','')='')  order by DATETIMELIMIT asc ", null, this.dbtimeout, true, true);

            DataTable dataTable = new DataTable();
            if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
            {
                dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( PERIHAL ='" + PERIHAL.Value.ToString() + "' or isnull('" + PERIHAL.Value.ToString() + "','')='')  order by DATETIMELIMIT desc ", null, this.dbtimeout, true, true);

            }
            else
            {

                dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where USERCREATED='" + Session["userid"].ToString() + "' and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( PERIHAL ='" + PERIHAL.Value.ToString() + "' or isnull('" + PERIHAL.Value.ToString() + "','')='')  order by DATETIMELIMIT desc ", null, this.dbtimeout, true, true);

            }

            if (dataTable.Rows.Count > 0)
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_data.Visible = false;
            }
            else
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_data.Visible = true;

            }


        }
        protected void forward_Click(object sender, EventArgs e)
        {
            f_forward.Visible = true;
            f_keterangan.Visible = true;
            f_save.Visible = true;

            FORWARD.ClearSelection();
            KET.Value = "";
        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (FORWARD.SelectedValue.Equals(""))
                {

                    var page11 = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Pilih item pada Forward');", true);

                    FORWARD.Focus();

                }
                else if (KET.Value.ToString().Equals(""))
                {

                    var page11 = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Harap isi keterangan');", true);

                    KET.Focus();
                }
                else
                {
                    int a = 0;
                    foreach (GridViewRow row in DataList.Rows)
                    {


                        CheckBox cb = (CheckBox)row.FindControl("chkCheck");
                        if (cb.Checked == true)
                        {
                            if (cb != null)
                            {

                                NameValueCollection nameValueCollectionKey = new NameValueCollection();
                                NameValueCollection nameValueCollection = new NameValueCollection();
                                StaticFramework.SaveNvc(nameValueCollectionKey, "AP_REGINPUT", row.Cells[2].Text.ToString());
                                StaticFramework.SaveNvc(nameValueCollection, "USERFORWARD", FORWARD);
                                StaticFramework.SaveNvc(nameValueCollection, "KETERANGAN", KET);
                                StaticFramework.SaveNvc(nameValueCollection, "DATECREATED", DateTime.Now);
                                StaticFramework.SaveNvc(nameValueCollection, "USERCREATED", Session["Userid"].ToString());

                                StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "DATA_FORWARD", this.conn);

                            }
                            a++;
                        }
                    }

                    if (a > 0)
                    {

                        var page11 = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Berhasil Forward');", true);
                        Bind_DataList();
                        f_forward.Visible = false;
                        f_keterangan.Visible = false;
                        f_save.Visible = false;
                        FORWARD.ClearSelection();
                        KET.Value = "";
                    }
                    else {

                        var page11 = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Pilih data untuk Forward');", true);
                    }
                }
            }
            catch (Exception)
            {

                var page11 = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Gagal Forward');", true);
            }

        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            f_forward.Visible = false;
            f_keterangan.Visible = false;
            f_save.Visible = false;

            FORWARD.ClearSelection();
            KET.Value = "";
        }
        protected void DataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList.PageIndex = e.NewPageIndex;
        }
        protected void DataList_PageIndexChanged(object sender, EventArgs e)
        {

            DataTable dataTable = new DataTable();
            if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
            {
                dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( PERIHAL ='" + PERIHAL.Value.ToString() + "' or isnull('" + PERIHAL.Value.ToString() + "','')='')  order by DATETIMELIMIT desc ", null, this.dbtimeout, true, true);

            }
            else
            {

                dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where USERCREATED='" + Session["userid"].ToString() + "' and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( PERIHAL ='" + PERIHAL.Value.ToString() + "' or isnull('" + PERIHAL.Value.ToString() + "','')='')  order by DATETIMELIMIT desc ", null, this.dbtimeout, true, true);

            }

            if (dataTable.Rows.Count > 0)
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_data.Visible = false;
            }
            else
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

                lb_data.Visible = true;

            }

        }
        protected void DataList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string INSTANSIID = "", AP = "", NOSURAT = "";
            if (e.CommandName.ToString().Equals("View") || e.CommandName.ToString().Equals("Edit"))
            {

                INSTANSIID = e.CommandArgument.ToString().Split(':')[0].ToString();
                // string NOSURAT = e.CommandArgument.ToString().Split(':')[1].ToString();


                AP = "1";// EncryptAndDecrypt.Encrypt("1");
                NOSURAT = e.CommandArgument.ToString().Split(':')[1].ToString();

            }


            if (e.CommandName == "View")
            {
                Response.Redirect("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=" + AP);

                //Server.Transfer("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=" + AP);
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=");
                //Server.Transfer("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=");
            }
            //else if (e.CommandName == "Forward")
            //{

            //    string strPopup = "<script language='javascript' ID='script1'> window.open('forward.aspx?data=" + HttpUtility.UrlEncode(NOSURAT) + "','new window', 'top=90, left=400px,  width=400px, height=300px, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes') </script>";

            //    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

            //}
        }

        public void OnConfirm(object sender, EventArgs e)
        {
            try
            {

                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Button btn = (Button)sender;
                    if (btn.CommandName.ToString().Equals("Hapus"))
                    {

                        string AP_REGINPUT = btn.CommandArgument.ToString().Split(':')[1].ToString();
                        string INSTANSI = btn.CommandArgument.ToString().Split(':')[0].ToString();
                        object[] param1 = new object[] { INSTANSI, AP_REGINPUT };
                        conn.ExecNonQuery(Q_Delete, param1, dbtimeout);

                        this.Bind_DataList();
                        //  Response.Redirect(Request.RawUrl);

                    }


                    // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                }
            }
            catch (Exception)
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Action')", true);
            }

        }
    }
}
