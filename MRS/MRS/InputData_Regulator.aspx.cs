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
    public partial class InputData_Regulator : DataPage
    {
        private static string Q_INSTANSI = " SELECT  * FROM [VW_RFINSTANSI]";
        private static string JUMLAH ="";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (!Request.QueryString["ID"].ToString().Equals(""))
                {
                    ID = base.Request.QueryString["ID"].ToString();// EncryptAndDecrypt.Decrypt(base.Request.QueryString["ID"].ToString());
                    RetrieveData();
                    lbl_judul.Text = "Form Edit Data Regulator";
                }
                else {

                    System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT * FROM [VW_JUMLAH_RFVIEWINSTANSI] ", null, this.dbtimeout, true, true);
                    if (dataTable.Rows.Count > 0)
                    {
                        JUMLAH = dataTable.Rows[0]["JUMLAH"].ToString();
                        INSTANSIID.Value = JUMLAH;
                    }

                }

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
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Update Success');window.location ='Report_Regulator.aspx';", true);
            }
        }
        protected void RetrieveData()
        {
         
            System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_RFINSTANSI] where INSTANSIID='"+ ID + "' ", null, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                StaticFramework.Retrieve(dataTable, INSTANSIID);
                StaticFramework.Retrieve(dataTable, INSTANSINAME);
            }


        }

        protected void save_data()
        {
            try
            {

                if (base.Request.QueryString["ID"].ToString().Equals(""))
                {

                    NameValueCollection nameValueCollectionKey = new NameValueCollection();
                    NameValueCollection nameValueCollection = new NameValueCollection();
                    StaticFramework.SaveNvc(nameValueCollectionKey, INSTANSIID);
                    StaticFramework.SaveNvc(nameValueCollection, INSTANSINAME);
                    StaticFramework.SaveNvc(nameValueCollection, "DATECREATED",DateTime.Now); 

                    StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "RFINSTANSI", this.conn);
                }
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error input data');", true);
            }
        }

        protected void clear()
        {
            System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT * FROM [VW_JUMLAH_RFVIEWINSTANSI] ", null, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                JUMLAH = dataTable.Rows[0]["JUMLAH"].ToString();
                INSTANSIID.Value = JUMLAH;
            }
            INSTANSINAME.Value="";
        }
    }
}