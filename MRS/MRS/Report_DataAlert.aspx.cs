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
    public partial class Report_DataAlert : DataPage
    {
        private static string Q_DeleteUser = "exec SP_DELETEALERT @1 ,@2,@3";
        private string hash_password = "";

        private static string Q_INSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI order by INSTANSINAME asc";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string Q_JENIS = "SELECT REPORTID , REPORTNAME FROM JENISREPORT  where ACTIVE=1 ORDER BY REPORTNAME ASC";
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

                StaticFramework.Reff(this.INSTANSIID, Q_INSTANSI, null, this.conn, false);
                StaticFramework.Reff(this.UNIT, Q_UNIT, null, this.conn, false);
                StaticFramework.Reff(this.JENIS, Q_JENIS, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
            }
        }
        public void Bind_DataList()
        {

            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_DATAALERT order by DIVISI asc", null, this.dbtimeout, true, true);
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


            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_DATAALERT where (AP_REGNO IN (SELECT AP_REGNO FROM VW_GETINSTANSI where INSTANSIID =   '" + INSTANSIID.SelectedValue.ToString() + "') or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( UNIT ='" + UNIT.SelectedValue.ToString() + "' or isnull('" + UNIT.SelectedValue.ToString() + "','')='') and ( JENIS ='" + JENIS.SelectedValue.ToString() + "' or isnull('" + JENIS.SelectedValue.ToString() + "','')='') order by DIVISI asc ", null, this.dbtimeout, true, true);
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
                        delete_user(btn.CommandArgument.ToString().Split(':')[0].ToString(), btn.CommandArgument.ToString().Split(':')[1].ToString(), btn.CommandArgument.ToString().Split(':')[2].ToString());
                        // Response.Redirect(Request.RawUrl);

                        this.Bind_DataList();
                    }


                    // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                }
            }
            catch (Exception)
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Action')", true);
            }

        }
        
        protected void delete_user(String instansi, String Unit, String Jenis)
        {
            try
            {

                object[] param1 = new object[] { instansi,Unit,Jenis };
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
                    //string s_instansi = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[0].ToString());
                    //string s_Jenis = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[2].ToString());
                    //string s_Unit = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[1].ToString());


                    string s_instansi = e.CommandArgument.ToString().Split(':')[0].ToString();
                    string s_Jenis = e.CommandArgument.ToString().Split(':')[2].ToString();
                    string s_Unit = e.CommandArgument.ToString().Split(':')[1].ToString();
                    Response.Redirect("~/MRS/InputData.aspx?IN=" + s_instansi+"&JN=" +s_Jenis+"&UN"+s_Unit);


                    //Server.Transfer("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);
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
            
            string s_instansi = btn.CommandArgument.ToString().Split(':')[0].ToString();
            //string s_Jenis = EncryptAndDecrypt.Encrypt(btn.CommandArgument.ToString().Split(':')[2].ToString());
            //string s_Unit = EncryptAndDecrypt.Encrypt(btn.CommandArgument.ToString().Split(':')[1].ToString());
            string s_Jenis = btn.CommandArgument.ToString().Split(':')[2].ToString();
            string s_Unit =btn.CommandArgument.ToString().Split(':')[1].ToString();
            Response.Redirect("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN=" + s_Unit);

           // Server.Transfer("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN=" + s_Unit);
        }

        public void UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {

            StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "'AND INSTANSIID ='"+INSTANSIID.SelectedValue.ToString()+"'", null, this.conn, false);
            
        }
        public void INSTANSIID_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='"+INSTANSIID.SelectedValue.ToString()+"'", null, this.conn, false);
         }
        protected void cari_Click(object sender, EventArgs e)
        {


            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_DATAALERT where (AP_REGNO IN (SELECT AP_REGNO FROM VW_GETINSTANSI where INSTANSIID =   '" + INSTANSIID.SelectedValue.ToString() + "') or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='')  and ( UNIT ='" + UNIT.SelectedValue.ToString() + "' or isnull('" + UNIT.SelectedValue.ToString() + "','')='') and ( JENIS ='" + JENIS.SelectedValue.ToString() + "' or isnull('" + JENIS.SelectedValue.ToString() + "','')='') order by DIVISI asc ", null, this.dbtimeout, true, true);
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