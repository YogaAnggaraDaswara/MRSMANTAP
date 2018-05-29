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
    public partial class InputData_Laporan : DataPage
    {

        private static string Q_RFINSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string hash_password = "", ID = "";
        private static string JUMLAH = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                RetreivedataDDL();
                if (!Request.QueryString["ID"].ToString().Equals(""))
                {
                    ID = base.Request.QueryString["ID"].ToString();// EncryptAndDecrypt.Decrypt(base.Request.QueryString["ID"].ToString());
                    RetrieveData();
                    lbl_judul.Text = "Form Edit Data Laporan";
                }

            }
        }
        private void RetreivedataDDL()
        {
            try
            {
                StaticFramework.Reff(INSTANSIID, Q_RFINSTANSI, null, this.conn, false);
                StaticFramework.Reff(UNIT, Q_UNIT, null, this.conn, false);

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
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Save Data Success');", true);
                clear();
            }
            else
            {
                save_data();
                var page11 = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page11, page11.GetType(), "alert", "alert('Update Success');window.location ='Report_Laporan.aspx';", true);
            }
        }
        protected void RetrieveData()
        {
            object[] param = new object[]
           {
                ID
           };

            System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_JENISREPORT] where REPORTID =@1", param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {

                UNIT.Enabled = false;
                UNIT.CssClass = "form-control";
                StaticFramework.Retrieve(dataTable, REPORTID);
                StaticFramework.Retrieve(dataTable, REPORTNAME);
                StaticFramework.Retrieve(dataTable, "UNITID", UNIT);
                StaticFramework.Retrieve(dataTable, INSTANSIID);
                StaticFramework.Retrieve(dataTable, PERIODE);

            }


        }
        public void UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            object[] param = new object[]
          {
                UNIT.SelectedValue.ToString()
          };
            System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT  CONVERT(VARCHAR,COUNT(*)+1) AS JUMLAH FROM [VW_JENISREPORT] where UNITID =@1", param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                REPORTID.Value= "R-"+UNIT.SelectedValue.ToString()+"0"+dataTable.Rows[0]["JUMLAH"].ToString();

            }

        }
        protected void save_data()
        {
            try
            {


                NameValueCollection nameValueCollectionKey = new NameValueCollection();
                NameValueCollection nameValueCollection = new NameValueCollection();
                StaticFramework.SaveNvc(nameValueCollectionKey, REPORTID);
                StaticFramework.SaveNvc(nameValueCollection, REPORTNAME);
                StaticFramework.SaveNvc(nameValueCollection, "UNITID", UNIT);
                StaticFramework.SaveNvc(nameValueCollection, INSTANSIID);
                StaticFramework.SaveNvc(nameValueCollection, "ACTIVE", "1");
                StaticFramework.SaveNvc(nameValueCollection, PERIODE);
                StaticFramework.SaveNvc(nameValueCollection, "DATECREATED", DateTime.Now);

                StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "JENISREPORT", this.conn);
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error insert data');", true);
            }
        }


        protected void clear()
        {
            REPORTID.Value = "";
            UNIT.ClearSelection();
            INSTANSIID.ClearSelection();
            PERIODE.ClearSelection();
            REPORTNAME.Value = "";

        }
    }
}