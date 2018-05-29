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
using System.IO;
using MRS.Models;
using System.Globalization;

namespace MRS
{
    public partial class _Default : DataPage
    {
        private static string Q_RFINSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string Q_BULAN = "SELECT [BULANID],[BULANNAME] FROM [RFBULAN] ORDER BY cast(BULANID as int) ASC ";
        private static string Q_TANGGAL = "SELECT [TANGGALID],[TANGGALNAME] FROM [RFTANGGAL] ORDER BY cast(TANGGALID as int) ASC";
        private static string Q_PERIODE = "SELECT [PERIODEID],[PERIODENAME] FROM VW_RFPERIODE ";
        private static string instansi = "", IdGenerate = "",s_periode="";
        private static string Q_Delete = "exec SP_DELETEPERIODE @1 ,@2,@3,@4";
        private void RetreivedataDDL()
        {
            try
            {
                //StaticFramework.Reff(this.INSTANSIID, Q_RFINSTANSI, null, this.conn, false);
                StaticFramework.Reff(this.PERIODE, Q_PERIODE, null, this.conn, false);
                StaticFramework.Reff(this.UNIT, Q_UNIT, null, this.conn, false);
                System.Data.DataTable dt = this.conn.GetDataTable(Q_RFINSTANSI, null, this.dbtimeout, true, true);
                if (dt.Rows.Count > 0)
                {
                    INSTANSIID.DataValueField = "INSTANSIID";
                    INSTANSIID.DataTextField = "INSTANSINAME";
                    INSTANSIID.DataSource = dt;
                    INSTANSIID.DataBind();

                }

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error getdata')", true);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                RetreivedataDDL();
                if (!Request.QueryString["IN"].ToString().Equals(""))
                {
                    //IdGenerate = EncryptAndDecrypt.Decrypt(Request.QueryString["IN"].ToString());
                    IdGenerate = Request.QueryString["IN"].ToString();

                    lbl_judul.Text = "Form Edit Data Alert";
                    UNIT.Enabled = false;
                    //INSTANSIID.Enabled = false;
                    JENIS.Enabled = false;
                    JENIS.CssClass = "form-control";
                    //INSTANSIID.CssClass = "form-control";
                    UNIT.CssClass = "form-control";
                    PERIODE.Enabled = false;
                    INSTANSIID.Enabled = false;
                    PERIODE.CssClass = "form-control";

                    div_BulanPelaporan.Style.Add("display", "NONE");
                    div_tanggalPelaporan.Style.Add("display", "NONE");
                    RetrieveData();
                }
                else
                {
                    IdGenerate = generateID();
                }

            }

        }
        protected void RetrieveData()
        {

            //String AP_REGNO = EncryptAndDecrypt.Decrypt(base.Request.QueryString["IN"].ToString()).ToString();
           // String UNIT = EncryptAndDecrypt.Decrypt(base.Request.QueryString["UN"].ToString()).ToString();
           // String JENIS = EncryptAndDecrypt.Decrypt(base.Request.QueryString["JN"].ToString()).ToString();
            String UNIT = base.Request.QueryString["UN"].ToString().ToString();
            String JENIS = base.Request.QueryString["JN"].ToString().ToString();

            object[] param = new object[]
           {
               IdGenerate
           };
            //System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT INSTANSIID,JENIS, DATETIMELIMIT,KETERANGAN,Sanksi, UNIT,convert(varchar,DATECREATED,126) as DATECREATED,USERCREATED,STATUS FROM DATA_COMPLIANCE where INSTANSIID =@1 and UNIT=@2 and JENIS =@3", param, this.dbtimeout, true, true);
            System.Data.DataTable dataTable = conn.GetDataTable("SELECT UNIT,JENIS, DATETIMELIMIT,KETERANGAN,Sanksi,Sanksi_ket, convert(varchar,DATECREATED,126) as DATECREATED,DEPTHEAD,PIC,PERIODE,USERCREATED,STATUS,JENISREPORT FROM VW_DATAALERT where AP_REGNO = @1 ", param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {

                StaticFramework.Retrieve(dataTable, this.UNIT);
                StaticFramework.Retrieve(dataTable, this.KETERANGAN);
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT + "'AND INSTANSIID in (SELECT SPLITDATA FROM [dbo].[fnSplitString]('" + instansi + "',','))", null, this.conn, false);
                //StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + UNIT + "' and [GROUPID] ='002PEN'", null, this.conn, false);
                StaticFramework.Reff(this.DEPTHEAD, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_HEAD] WHERE UNIT ='" + UNIT + "'", null, this.conn, false);

                System.Data.DataTable dt = conn.GetDataTable("SELECT * FROM VW_GETINSTANSI where AP_REGNO = @1 ", param, this.dbtimeout, true, true);
                if (dt.Rows.Count > 0)
                {
                    
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        for (int i = 0; i < INSTANSIID.Items.Count; i++)
                        {

                            if (dt.Rows[a]["INSTANSIID"].ToString() == INSTANSIID.Items[i].Value.ToString()) {
                                INSTANSIID.Items[i].Selected=true;
                            }
                        }
                    }

                }


                StaticFramework.Retrieve(dataTable, DEPTHEAD);

                StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + UNIT + "' and SU_UPLINER='" + DEPTHEAD.SelectedValue.ToString() + "'  and [GROUPID] ='002PEN'", null, this.conn, false);

                StaticFramework.Retrieve(dataTable, PIC);
                StaticFramework.Retrieve(dataTable, PERIODE);

                System.Data.DataTable dt_PERIODE = conn.GetDataTable("SELECT * FROM VW_RFPERIODE where PERIODEID= '"+ PERIODE .SelectedValue.ToString()+ "' ", null, this.dbtimeout, true, true);
                if (dt_PERIODE.Rows.Count > 0)
                {
                    s_periode = dt_PERIODE.Rows[0]["FLAG"].ToString();
                    if (dt_PERIODE.Rows[0]["FLAG"].ToString().Equals("1"))
                    {
                        div_ListPelaporan.Style.Add("display", "");
                        div_BulanPelaporan.Style.Add("display", "");
                        div_tanggalPelaporan.Style.Add("display", "NONE");
                        StaticFramework.Reff(this.TANGGAL_PERIODE, Q_TANGGAL, null, this.conn, false);
                        StaticFramework.Reff(this.BULAN_PERIODE, Q_BULAN, null, this.conn, false);
                        Bind_DataList();
                    }else
                    if (dt_PERIODE.Rows[0]["FLAG"].ToString().Equals("2"))
                    {


                        div_BulanPelaporan.Style.Add("display", "NONE");
                        div_ListPelaporan.Style.Add("display", "NONE");
                        div_tanggalPelaporan.Style.Add("display", "NONE");
                    }
                    else
                    {
                        div_BulanPelaporan.Style.Add("display", "NONE");
                        div_ListPelaporan.Style.Add("display", "NONE");
                        div_tanggalPelaporan.Style.Add("display", "");
                        StaticFramework.Reff(this.DATETIMELIMIT, Q_TANGGAL, null, this.conn, false);
                        StaticFramework.Retrieve(dataTable, DATETIMELIMIT);
                    }

                }

                StaticFramework.Retrieve(dataTable, JENISREPORT);
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT + "' ", null, this.conn, false);

                StaticFramework.Retrieve(dataTable, this.JENIS);
                //StaticFramework.Retrieve(dataTable, this.Sanksi);
                Sanksi.Value = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "").Replace(",", "")));
                //Sanksi.Value = string.Format("{0:###,###}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "")));

              //Sanksi.Value=String.Format(((Math.Round(Convert.ToDouble(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", ""))) == Convert.ToDouble(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", ""))) ? "{0:0}" : "{0:###,00}"), Convert.ToDouble(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", ""))); ;
                StaticFramework.Retrieve(dataTable, Sanksi_ket);

                
            }



        }
        public void DEPTHEAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + UNIT.SelectedValue.ToString() + "' and SU_UPLINER='"+DEPTHEAD.SelectedValue.ToString()+"'  and [GROUPID] ='002PEN'", null, this.conn, false);

        }
        public void INSTANSIID_SelectedIndexChanged(object sender, EventArgs e)
        {
            instansi = "";
            int index = 0;
            for (int i = 0; i < INSTANSIID.Items.Count; i++)
            {

                if (INSTANSIID.Items[i].Selected)
                {
                    if (index == 0)
                    {
                        instansi += INSTANSIID.Items[i].Value.ToString();
                    }
                    else
                    {
                        instansi += "," + INSTANSIID.Items[i].Value.ToString();
                    }
                    index += 1;

                }



            }

            StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID in (SELECT SPLITDATA FROM [dbo].[fnSplitString]('" + instansi + "',','))", null, this.conn, false);
            //StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' ", null, this.conn, false);
        }
        public void UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {

            StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "'AND INSTANSIID in (SELECT SPLITDATA FROM [dbo].[fnSplitString]('" + instansi + "',','))", null, this.conn, false);

            //StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' ", null, this.conn, false);
           
             //   StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + UNIT.SelectedValue.ToString() + "' and [GROUPID] ='002PEN'", null, this.conn, false);
          
            StaticFramework.Reff(this.DEPTHEAD, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_HEAD] WHERE UNIT ='" + UNIT.SelectedValue.ToString() + "' ", null, this.conn, false);

        }
        public void PERIODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dt_PERIODE = conn.GetDataTable("SELECT * FROM VW_RFPERIODE where PERIODEID= '" + PERIODE.SelectedValue.ToString() + "' ", null, this.dbtimeout, true, true);
            if (dt_PERIODE.Rows.Count > 0)
            {

                s_periode = dt_PERIODE.Rows[0]["FLAG"].ToString();
                if (dt_PERIODE.Rows[0]["FLAG"].ToString().Equals("1"))
                {

                    div_ListPelaporan.Style.Add("display", "");
                    div_BulanPelaporan.Style.Add("display", "");
                    div_tanggalPelaporan.Style.Add("display", "NONE");
                    StaticFramework.Reff(this.TANGGAL_PERIODE, Q_TANGGAL, null, this.conn, false);
                    StaticFramework.Reff(this.BULAN_PERIODE, Q_BULAN, null, this.conn, false);
                    Bind_DataList();
                }
               else if (dt_PERIODE.Rows[0]["FLAG"].ToString().Equals("2"))
                {


                    div_BulanPelaporan.Style.Add("display", "NONE");
                    div_ListPelaporan.Style.Add("display", "NONE");
                    div_tanggalPelaporan.Style.Add("display", "NONE");
                }
                else
                {
                    div_BulanPelaporan.Style.Add("display", "NONE");
                    div_ListPelaporan.Style.Add("display", "NONE");
                    div_tanggalPelaporan.Style.Add("display", "");
                    StaticFramework.Reff(this.DATETIMELIMIT, Q_TANGGAL, null, this.conn, false);
                }
            }

        }
        protected void DataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList.PageIndex = e.NewPageIndex;
        }
        protected void DataList_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList();

        }
        protected void save_Click(object sender, EventArgs e)
        {
            save_data();


        }
        protected void add_Click(object sender, System.EventArgs e)
        {
            if (UNIT.SelectedValue.ToString().Equals(""))
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Pilih item pada daftar Divisi ');", true);
                UNIT.Focus();
            }
            else if (INSTANSIID.SelectedValue.ToString().Equals(""))
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Pilih item pada daftar Regulator ');", true);
                INSTANSIID.Focus();
            }
            else
            {

                save_data_periode();
                cleardata_PERIODE();
            }


        }
        protected void save_data_periode()
        {
            try
            {
                if (BULAN_PERIODE.SelectedValue.ToString().Equals(""))
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Pilih item pada daftar Bulan Pelaporan ');", true);
                    BULAN_PERIODE.Focus();
                }
                else if (TANGGAL_PERIODE.SelectedValue.ToString().Equals(""))
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Pilih item pada daftar Tanggal Pelaporan ');", true);
                    TANGGAL_PERIODE.Focus();
                }
                else
                {
                    for (int i = 0; i < INSTANSIID.Items.Count; i++)
                    {

                        if (INSTANSIID.Items[i].Selected)
                        {

                            NameValueCollection nameValueCollectionKey = new NameValueCollection();
                            NameValueCollection nameValueCollection = new NameValueCollection();
                            StaticFramework.SaveNvc(nameValueCollectionKey, "AP_REGNO", IdGenerate);
                            StaticFramework.SaveNvc(nameValueCollectionKey, "INSTANSIID", INSTANSIID.Items[i].Value.ToString());
                            StaticFramework.SaveNvc(nameValueCollectionKey, "UNIT", UNIT);
                            StaticFramework.SaveNvc(nameValueCollectionKey, "BULAN", BULAN_PERIODE);
                            StaticFramework.SaveNvc(nameValueCollectionKey, "TANGGAL", TANGGAL_PERIODE);
                            StaticFramework.SaveNvc(nameValueCollectionKey, PERIODE);
                            StaticFramework.SaveNvc(nameValueCollection, "USERCREATED", this.Session["UserID"].ToString());
                            StaticFramework.SaveNvc(nameValueCollection, "DATECREATED", DateTime.Now);
                            StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "DATA_PERIODE", this.conn);

                        }



                    }
                    Bind_DataList();
                }
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error insert periode');", true);
            }
        }
        public string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = "REG" + String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000) + DateTime.Now.ToString("yyyyMMddHHmmss");

            return number;
        }
        protected void edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //BULAN_PERIODE.SelectedValue = btn.CommandArgument.ToString().Split(':')[0].ToString();
            //TANGGAL_PERIODE.SelectedValue = btn.CommandArgument.ToString().Split(':')[1].ToString();
            StaticFramework.Retrieve(btn.CommandArgument.ToString().Split(':')[0].ToString(), BULAN_PERIODE);
            StaticFramework.Retrieve(btn.CommandArgument.ToString().Split(':')[1].ToString(), KETERANGAN);
        }
        protected void save_data()
        {
            try
            {

                for (int i = 0; i < INSTANSIID.Items.Count; i++)
                {

                    if (INSTANSIID.Items[i].Selected)
                    {
                        NameValueCollection nameValueCollectionKey = new NameValueCollection();
                        NameValueCollection nameValueCollection = new NameValueCollection();
                        StaticFramework.SaveNvc(nameValueCollectionKey, "AP_REGNO", IdGenerate);
                        StaticFramework.SaveNvc(nameValueCollectionKey, "INSTANSIID", INSTANSIID.Items[i].Value.ToString());
                        StaticFramework.SaveNvc(nameValueCollectionKey, "UNIT", UNIT);
                        StaticFramework.SaveNvc(nameValueCollectionKey, "JENIS", JENIS);
                        StaticFramework.SaveNvc(nameValueCollection, "DEPTHEAD", DEPTHEAD);
                        StaticFramework.SaveNvc(nameValueCollection, "PIC", PIC);
                        StaticFramework.SaveNvc(nameValueCollectionKey, "PERIODE", PERIODE);

                        StaticFramework.SaveNvc(nameValueCollection, JENISREPORT);
                        StaticFramework.SaveNvc(nameValueCollection, "KETERANGAN", KETERANGAN);
                        StaticFramework.SaveNvc(nameValueCollection, "SANkSI_KET", Sanksi_ket);
                        StaticFramework.SaveNvc(nameValueCollection, "Sanksi", Sanksi.Value.ToString().Replace(",", ""));
                        //StaticFramework.SaveNvc(nameValueCollection, "Sanksi", Sanksi.Value.Replace(".", ""));
                        StaticFramework.SaveNvc(nameValueCollection, "USERCREATED", this.Session["UserID"].ToString());
                        StaticFramework.SaveNvc(nameValueCollection, "DATECREATED", DateTime.Now);
                        StaticFramework.SaveNvc(nameValueCollection, "STATUS", "1");
                        if (s_periode.Equals("0"))
                        {

                            StaticFramework.SaveNvc(nameValueCollection, DATETIMELIMIT);
                        }

                        StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "DATA_COMPLIANCE", this.conn);
                    }
                }

                if (!Request.QueryString["IN"].ToString().Equals(""))
                {

                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Update Success');window.location ='Report_DataAlert.aspx';", true);
                }
                else
                {

                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Save Success');", true);

                }
                cleardata();
                IdGenerate = generateID();
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error save');", true);
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
                        delete_PERIODE(btn.CommandArgument.ToString().Split(':')[0].ToString(), btn.CommandArgument.ToString().Split(':')[1].ToString(), btn.CommandArgument.ToString().Split(':')[2].ToString(), btn.CommandArgument.ToString().Split(':')[3].ToString());

                        Bind_DataList();
                    }


                    // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                }
            }
            catch (Exception)
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Action')", true);
            }

        }
        protected void delete_PERIODE(string bulan, string tanggal, string unit, string periode)
        {
            try
            {

                object[] param1 = new object[] { bulan, tanggal, unit, periode };
                conn.ExecNonQuery(Q_Delete, param1, dbtimeout);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Success You delete data ');", true);
                return;
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Delete')", true);
            }
        }
        public void Bind_DataList()
        {
            object[] param1 = new object[] {  IdGenerate };
            var dataTable = this.conn.GetDataTable("SELECT  * FROM VW_PERIODE where ap_regno =@1 order by cast(BULAN as int) ASC", param1, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                DataList.DataSource = dataTable;
                DataList.DataBind();

            }
            else {

                DataList.DataSource = null;
                DataList.DataBind();
            }
        }
        protected void cleardata_PERIODE()
        {
            BULAN_PERIODE.ClearSelection();
            TANGGAL_PERIODE.ClearSelection();

        }
        protected void cleardata()
        {
            this.INSTANSIID.ClearSelection();
            UNIT.ClearSelection();
            JENIS.ClearSelection();
            DEPTHEAD.ClearSelection();
            PIC.ClearSelection();
            Sanksi_ket.Value = "";
            PERIODE.ClearSelection();
            // this.DATETIMELIMIT.Value = "";
            this.DATETIMELIMIT.Text = "";

            div_BulanPelaporan.Style.Add("display", "NONE");
            div_ListPelaporan.Style.Add("display", "NONE");
            div_tanggalPelaporan.Style.Add("display", "NONE");
            this.KETERANGAN.Value = "";
            this.Sanksi.Value = "";
            DataList.DataSource = null;
            DataList.DataBind();
        }

    }
}