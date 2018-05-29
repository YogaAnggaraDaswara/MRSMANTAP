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
    public partial class Report_Laporan : DataPage
    {

        private static string Q_Delete = "exec SP_DELETELAPORAN @1";
        private string hash_password = "";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string Q_RFINSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI";

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
                StaticFramework.Reff(this.INSTANSIID, Q_RFINSTANSI, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error ambil data')", true);
            }
        }
        public void Bind_DataList()
        {

            var dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_JENISREPORT] order by UNITID asc", null, this.dbtimeout, true, true);
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


            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_JENISREPORT where (REPORTNAME like  '%" + REPORTNAME.Value.ToString() + "%' or isnull('" + REPORTNAME.Value.ToString() + "' ,'')='')  and ( UNITID ='" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "','')='') and ( INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "','')='') order by UNITID asc ", null, this.dbtimeout, true, true);
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
                        delete_laporan(btn.CommandArgument.ToString());
                       // Response.Redirect(Request.RawUrl);

                        this.Bind_DataList();

                    }
                }
            }
            catch (Exception)
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Action')", true);
            }

        }
        
        protected void delete_laporan(String userid)
        {
            try
            {

                object[] param1 = new object[] { userid };
                conn.ExecNonQuery(Q_Delete, param1, dbtimeout);

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
                    // Response.Redirect("~/MRS/InputData_Laporan.aspx?nosurat=" + NOSURAT + "&approve=1");

                    Server.Transfer("~/MRS/InputData_Laporan.aspx?nosurat=" + NOSURAT + "&approve=1");
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

            string userid_en = btn.CommandArgument.ToString();//EncryptAndDecrypt.Encrypt(btn.CommandArgument.ToString());

            Response.Redirect("~/MRS/InputData_Laporan.aspx?ID=" + userid_en);

            //Server.Transfer("~/MRS/InputData_Laporan.aspx?ID=" + userid_en);

        }
        protected void cari_Click(object sender, EventArgs e)
        {


            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_JENISREPORT where (REPORTNAME like  '%" + REPORTNAME.Value.ToString() + "%' or isnull('" + REPORTNAME.Value.ToString() + "' ,'')='')  and ( UNITID ='" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "','')='') and ( INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "','')='') order by UNITID asc ", null, this.dbtimeout, true, true);
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