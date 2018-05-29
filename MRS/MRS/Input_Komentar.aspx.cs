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

namespace MRS.MRS
{
    public partial class Input_Komentar : DataPage
    {
        private static string Q_RFINSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI";
        private static string s_Nosurat = "", s_Approve = "", L_UNIT = "", s_user = "";

        DateTime dtime = new DateTime();
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1'  ORDER BY [DIVISI] ASC";
        private static string Q_PERIODE = "SELECT [PERIODEID],[PERIODENAME] FROM VW_RFPERIODE ";

        private static string IdGenerate = "";
        private void RetreivedataDDL()
        {
            try
            {
                StaticFramework.Reff(this.UNIT, Q_UNIT, null, this.conn, false);
                StaticFramework.Reff(this.INSTANSIID, Q_RFINSTANSI, null, this.conn, false);
                StaticFramework.Reff(this.PERIODE, Q_PERIODE, null, this.conn, false);
            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata ')", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lb_file.Visible = false;
                fileupload.Visible = false;
                uduh.Visible = false;

                RetreivedataDDL();
                if (!Request.QueryString["NS"].ToString().Equals(""))
                {

                    IdGenerate = base.Request.QueryString["NS"].ToString();

                    lb_file.Visible = true;
                    fileupload.Visible = true;
                    uduh.Visible = true;

                }
                else
                {
                    IdGenerate = generateID();
                }

                if (!Request.QueryString["AP"].ToString().Equals(""))
                {

                    s_Approve = base.Request.QueryString["AP"].ToString();//EncryptAndDecrypt.Decrypt(base.Request.QueryString["AP"].ToString());
                }

                if (!Request.QueryString["AP"].ToString().ToString().Equals(""))
                {
                    RetrieveData();


                    btnsave.Visible = false;
                    lbl_judul.Text = "Form View Data Entry";

                    this.INSTANSIID.Enabled = false;
                    this.NOSURAT.Disabled = true;
                    this.TGLSURAT.Disabled = true;

                    this.PERIHAL.Disabled = true;
                    this.DATETIMELIMIT.Disabled = true;

                    this.KETERANGAN.Disabled = true;
                    this.SANKSI_KET.Disabled = true;
                    this.Sanksi.Disabled = true;
                    this.TGLINPUT.Disabled = true;
                    this.UPLOADFILE.Disabled = true;
                    UNIT.Enabled = false;
                    UNIT.CssClass = "form-control";
                    JENIS.Enabled = false;
                    JENIS.CssClass = "form-control";
                    INSTANSIID.CssClass = "form-control";

                    PERIODE.Enabled = false;
                    PERIODE.CssClass = "form-control";

                    JENISREPORT.Enabled = false;
                    JENISREPORT.CssClass = "form-control";

                }
                else
                if (!Request.QueryString["NS"].ToString().Equals("") && Request.QueryString["AP"].ToString().Equals(""))
                {
                    lbl_judul.Text = "Form Edit Data Entry";
                    div_unit.Visible = false;
                    RetrieveData();
                }


                if (Session["GROUPID"].ToString().Equals("002PEN") || (Session["GROUPID"].ToString().Equals("004KADIV") && Request.QueryString["AP"].ToString().Equals("")) || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
                {
                    L_UNIT = Session["UnitID"].ToString();

                }
                else
                {
                    L_UNIT = UNIT.SelectedValue.ToString();
                    div_unit.Visible = true;
                }

            }
        }

        public void INSTANSIID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Session["GROUPID"].ToString().Equals("002PEN") || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
            //{
            //    StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);
            //}

            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and PIC ='" + Session["Userid"].ToString() + "'", null, this.conn, false);


            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals(""))
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and DEPTHEAD ='" + Session["Userid"].ToString() + "'", null, this.conn, false);

            }
            else
            {
                StaticFramework.Reff(this.JENIS, "SELECT distinct REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);

            }


        }
        public void PERIODE_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and PIC ='" + Session["Userid"].ToString() + "'", null, this.conn, false);


            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals(""))
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and DEPTHEAD ='" + Session["Userid"].ToString() + "'", null, this.conn, false);

            }
            else
            {
                StaticFramework.Reff(this.JENIS, "SELECT distinct REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);

            }

        }

        public void JENIS_SelectedIndexChanged(object sender, EventArgs e)
        {

            //StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM JENISREPORT WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'", null, this.conn, false);

            object[] param = new object[]
          {
                INSTANSIID.SelectedValue.ToString(),
                L_UNIT ,
                JENIS.SelectedValue.ToString(),
                PERIODE.SelectedValue.ToString()
          };
            var dataTable = this.conn.GetDataTable("SELECT * FROM VW_LIMIT where INSTANSIID =@1 and UNIT=@2 and JENIS=@3 AND PERIODE=@4", param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {

                //StaticFramework.Retrieve(dataTable, this.DATETIMELIMIT);
                if (!dataTable.Rows[0]["DATETIMELIMIT"].ToString().Equals(""))
                {

                    dtime = DateTime.ParseExact(dataTable.Rows[0]["DATETIMELIMIT"].ToString(), "yyyy-MM-d", CultureInfo.InvariantCulture);
                    DATETIMELIMIT.Value = dtime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                StaticFramework.Retrieve(dataTable, this.SANKSI_KET);
                //StaticFramework.Retrieve(dataTable, this.Sanksi);
                //Sanksi.Value = string.Format("{0:#,00}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "")));

                //Sanksi.Value = string.Format("{0:#,00}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "").Replace(",", "")));
                if (!dataTable.Rows[0]["Sanksi"].ToString().Equals(""))
                {
                    Sanksi.Value = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "").Replace(",", "")));

                }
            }
            else
            {
                DATETIMELIMIT.Value = "";
                SANKSI_KET.Value = "";
                Sanksi.Value = "";
            }

            //if (Session["GROUPID"].ToString().Equals("002PEN") || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
            //{
            //    var dataTable1 = this.conn.GetDataTable("SELECT JENISREPORT FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and REPORTID='" + JENIS.SelectedValue.ToString() + "'", null, this.dbtimeout, true, true);
            //    if (dataTable1.Rows.Count > 0)
            //    {
            //        JENISREPORT.SelectedValue = dataTable1.Rows[0]["JENISREPORT"].ToString();
            //    }

            //}

            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {
                var dataTable1 = this.conn.GetDataTable("SELECT JENISREPORT FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and PIC ='" + Session["Userid"].ToString() + "'", null, this.dbtimeout, true, true);
                if (dataTable1.Rows.Count > 0)
                {
                    JENISREPORT.SelectedValue = dataTable1.Rows[0]["JENISREPORT"].ToString();
                }
            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals(""))
            {
                var dataTable1 = this.conn.GetDataTable("SELECT JENISREPORT FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and DEPTHEAD ='" + Session["Userid"].ToString() + "'", null, this.dbtimeout, true, true);
                if (dataTable1.Rows.Count > 0)
                {
                    JENISREPORT.SelectedValue = dataTable1.Rows[0]["JENISREPORT"].ToString();
                }
            }

            else
            {
                var dataTable1 = this.conn.GetDataTable("SELECT distinct JENISREPORT FROM VW_NAMALAPORAN WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and REPORTID='" + JENIS.SelectedValue.ToString() + "'", null, this.dbtimeout, true, true);
                if (dataTable1.Rows.Count > 0)
                {
                    JENISREPORT.SelectedValue = dataTable1.Rows[0]["JENISREPORT"].ToString();
                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            save_data();


        }
        protected void uduh_Click(object sender, EventArgs e)
        {

            if (!fileupload.Text.ToString().Equals(""))
            {
                // string filePath = Server.MapPath("~/Upload/" + UNIT.SelectedValue + "/" + INSTANSIID.SelectedValue + "/" + fileupload.Text.ToString());
                string filePath = Server.MapPath("~/Upload/" + L_UNIT + "/" + INSTANSIID.SelectedValue + "/" + fileupload.Text.ToString());
                if (File.Exists(filePath))
                {
                    HttpResponse res = HttpContext.Current.Response;
                    res.Clear();
                    res.AppendHeader("content-disposition", "attachment; filename=" + filePath);
                    res.ContentType = "application/octet-stream";
                    res.WriteFile(filePath);
                    res.Flush();
                    res.End();
                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('FIle Not Found in Server');", true);
                }

            }
            else
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('FIle Not Found');", true);
            }
        }
        public string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = "IKOM" + String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000) + DateTime.Now.ToString("yyyyMMddHHmmss");

            return number;

        }

        protected void save_data()
        {
            try
            {
                String h_fileupload = "";
                if (UPLOADFILE.Value.Equals(""))
                {
                    h_fileupload = fileupload.Text.ToString();
                }
                else
                {
                    h_fileupload = UPLOADFILE.Value.ToString();
                }

                NameValueCollection nameValueCollectionKey = new NameValueCollection();
                NameValueCollection nameValueCollection = new NameValueCollection();
                if (Session["GROUPID"].ToString().Equals("002PEN") || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
                {

                    StaticFramework.SaveNvc(nameValueCollection, "UNIT", Session["UNITid"].ToString());

                }
                else
                {
                    StaticFramework.SaveNvc(nameValueCollection, UNIT);

                }
                StaticFramework.SaveNvc(nameValueCollectionKey, "AP_REGINPUT", IdGenerate);
                StaticFramework.SaveNvc(nameValueCollection, INSTANSIID);
                StaticFramework.SaveNvc(nameValueCollection, PERIODE);
                StaticFramework.SaveNvc(nameValueCollection, JENIS);
                StaticFramework.SaveNvc(nameValueCollection, JENISREPORT);
                if (!DATETIMELIMIT.Value.ToString().Equals(""))
                {
                    StaticFramework.SaveNvc(nameValueCollection, "DATETIMELIMIT", DateTime.Parse(DATETIMELIMIT.Value.ToString()));
                }
                StaticFramework.SaveNvc(nameValueCollection, NOSURAT);
                if (!TGLSURAT.Value.ToString().Equals(""))
                {
                    StaticFramework.SaveNvc(nameValueCollection, "TGLSURAT", DateTime.Parse(TGLSURAT.Value.ToString()));
                }
                StaticFramework.SaveNvc(nameValueCollection, PERIHAL);
                StaticFramework.SaveNvc(nameValueCollection, KETERANGAN);
                StaticFramework.SaveNvc(nameValueCollection, "Sanksi", Sanksi.Value.Replace(".", ""));
                StaticFramework.SaveNvc(nameValueCollection, SANKSI_KET);
                StaticFramework.SaveNvc(nameValueCollection, "UPLOADFILE", h_fileupload);

                if (!TGLINPUT.Value.ToString().Equals(""))
                {
                    StaticFramework.SaveNvc(nameValueCollection, "TGLINPUT", DateTime.Parse(TGLINPUT.Value.ToString()));
                }

                if (Session["GROUPID"].ToString().Equals("00") || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
                {

                    StaticFramework.SaveNvc(nameValueCollection, "USERCREATED", s_user);
                }
                else
                {

                    StaticFramework.SaveNvc(nameValueCollection, "USERCREATED", Session["Userid"].ToString());
                }



                StaticFramework.SaveNvc(nameValueCollection, "STATUSAPPROVE", "0");
                StaticFramework.SaveNvc(nameValueCollection, "DATECREATED", DateTime.Now);
                StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "APP_MRS", this.conn);




                if (!UPLOADFILE.Value.ToString().Equals(""))
                {

                    //string folderPath = Server.MapPath("~/Upload/" + UNIT.SelectedValue + "/" + INSTANSIID.SelectedValue + "/");

                    //string path = Server.MapPath("~/Upload/" + UNIT.SelectedValue + "/" + INSTANSIID.SelectedValue + "/" + fileupload.Text.ToString());


                    string folderPath = Server.MapPath("~/Upload/" + L_UNIT + "/" + INSTANSIID.SelectedValue + "/");

                    string path = Server.MapPath("~/Upload/" + L_UNIT + "/" + INSTANSIID.SelectedValue + "/" + fileupload.Text.ToString());
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    UPLOADFILE.PostedFile.SaveAs(folderPath + UPLOADFILE.Value.ToString());
                }

                //FileStream fStream = File.OpenRead(ConfigurationSettings.AppSettings["PATHUPLOAD"].ToString() + L_UNIT + "\\"+ INSTANSIID.SelectedValue +"\\"+ UPLOADFILE.Value.ToString()+"");
                //byte[] contents = new byte[fStream.Length];
                //fStream.Read(contents, 0, (int)fStream.Length);
                //fStream.Close();


                //if (fileUpload1.HasFile) {
                //    FileInfo fi = new FileInfo(fileUpload1.FileName);
                //    String name = fi.Name;
                //    byte[] documentContent = fileUpload1.FileBytes;
                //    StaticFramework.SaveNvc(nameValueCollection, "FILE_UPLOAD", documentContent);
                //}

                StaticFramework.Save(nameValueCollection, nameValueCollectionKey, "APP_MRS", this.conn);

                if (!Request.QueryString["NS"].ToString().Equals("") && Session["GROUPID"].ToString().Equals("002PEN"))
                {

                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Update Success');window.location ='ReportMRS.aspx';", true);

                }
                else if ((!Request.QueryString["NS"].ToString().Equals("") && Session["GROUPID"].ToString().Equals("00")) || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Update Success');window.location ='Report_Approval.aspx';", true);

                }
                else
                {

                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Save Success');", true);
                    IdGenerate = generateID();

                }


                cleardata();
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Gagal Save');", true);
                return;
            }
        }

        protected void cleardata()
        {
            this.INSTANSIID.ClearSelection();
            this.JENIS.ClearSelection();
            this.NOSURAT.Value = "";
            this.TGLSURAT.Value = "";
            this.PERIHAL.Value = "";
            this.DATETIMELIMIT.Value = "";

            this.KETERANGAN.Value = "";
            this.Sanksi.Value = "";
            this.TGLINPUT.Value = "";
            //this.UPLOADFILE.Value = "";
            fileupload.Text = "";
            PERIODE.ClearSelection();
            UNIT.ClearSelection();
            SANKSI_KET.Value = "";
            JENISREPORT.ClearSelection();

        }
        protected void RetrieveData()
        {
            object[] param = new object[]
           {
               IdGenerate
           };
            var dataTable = this.conn.GetDataTable("SELECT userid,UNIT,INSTANSIID,NOSURAT, convert(varchar,TGLSURAT,126) as TGLSURAT,PERIHAL,convert(varchar,DATETIMELIMIT,126) as DATETIMELIMIT,KETERANGAN,Sanksi, UNIT,convert(varchar,TGLINPUT,126) as TGLINPUT, UPLOADFILE,JENIS,PERIODE,JENISREPORT,SANKSI_KET FROM VW_APPMRS where AP_REGINPUT =@1", param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {

                StaticFramework.Retrieve(dataTable, this.INSTANSIID);
                StaticFramework.Retrieve(dataTable, this.NOSURAT);
                //StaticFramework.Retrieve(dataTable, this.TGLSURAT);
                if (!dataTable.Rows[0]["TGLSURAT"].ToString().Equals(""))
                {

                    dtime = DateTime.ParseExact(dataTable.Rows[0]["TGLSURAT"].ToString(), "yyyy-MM-d", CultureInfo.InvariantCulture);
                    TGLSURAT.Value = dtime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                StaticFramework.Retrieve(dataTable, this.PERIHAL);
                //StaticFramework.Retrieve(dataTable, this.DATETIMELIMIT);
                if (!dataTable.Rows[0]["DATETIMELIMIT"].ToString().Equals(""))
                {

                    dtime = DateTime.ParseExact(dataTable.Rows[0]["DATETIMELIMIT"].ToString(), "yyyy-MM-d", CultureInfo.InvariantCulture);
                    DATETIMELIMIT.Value = dtime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                StaticFramework.Retrieve(dataTable, this.JENISREPORT);
                StaticFramework.Retrieve(dataTable, this.SANKSI_KET);
                StaticFramework.Retrieve(dataTable, this.PERIODE);
                StaticFramework.Retrieve(dataTable, this.KETERANGAN);
                //StaticFramework.Retrieve(dataTable, this.TGLINPUT);
                if (!dataTable.Rows[0]["TGLINPUT"].ToString().Equals(""))
                {

                    dtime = DateTime.ParseExact(dataTable.Rows[0]["TGLINPUT"].ToString(), "yyyy-MM-d", CultureInfo.InvariantCulture);
                    TGLINPUT.Value = dtime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                fileupload.Text = dataTable.Rows[0]["UPLOADFILE"].ToString();

                //Sanksi.Value = string.Format("{0:#,00}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "").Replace(",", "")));
                if (!dataTable.Rows[0]["Sanksi"].ToString().Equals(""))
                {
                    Sanksi.Value = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToDecimal(dataTable.Rows[0]["Sanksi"].ToString().Replace(",00", "").Replace(",", "")));

                }
                if (Session["GROUPID"].ToString().Equals("002PEN"))
                {
                    StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and PIC ='" + Session["Userid"].ToString() + "'", null, this.conn, false);
                    s_user = dataTable.Rows[0]["userid"].ToString();
                }
                else if (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals(""))
                {
                    StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and DEPTHEAD ='" + Session["Userid"].ToString() + "'", null, this.conn, false);
                    s_user = dataTable.Rows[0]["userid"].ToString();
                }
                else if (Session["GROUPID"].ToString().Equals("00"))
                {
                    StaticFramework.Retrieve(dataTable, this.UNIT);
                    StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);
                    s_user = dataTable.Rows[0]["userid"].ToString();
                }
                else
                {
                    StaticFramework.Retrieve(dataTable, this.UNIT);
                    StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);

                }
                StaticFramework.Retrieve(dataTable, this.JENIS);


            }



        }
        public void UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and PIC ='" + Session["Userid"].ToString() + "'", null, this.conn, false);


            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals(""))
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "' and DEPTHEAD ='" + Session["Userid"].ToString() + "'", null, this.conn, false);

            }
            //if (Session["GROUPID"].ToString().Equals("002PEN") || (Session["GROUPID"].ToString().Equals("003HEAD") && Request.QueryString["AP"].ToString().Equals("")))
            //{
            //    StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + Session["UnitID"].ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'  AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);
            //    L_UNIT = Session["UnitID"].ToString();
            //}

            else
            {
                StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_NAMALAPORAN WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "' AND PERIODE ='" + PERIODE.SelectedValue.ToString() + "'", null, this.conn, false);
                L_UNIT = UNIT.SelectedValue.ToString();
            }
            //StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' ", null, this.conn, false);

            //StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + UNIT.SelectedValue.ToString() + "' and [GROUPID] ='002PEN'", null, this.conn, false);

            //StaticFramework.Reff(this.DEPTHEAD, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + UNIT.SelectedValue.ToString() + "' and [GROUPID] ='003HEAD'", null, this.conn, false);

        }
    }
}