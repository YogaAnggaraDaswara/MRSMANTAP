using CID.Framework;
using MRS.Models;
using MRS.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRS.MRS
{
    public partial class Report_Dashboard : DataPage
    {

        static string Q_Approve = "exec SP_APPROVE @1";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string s_unit = "", s_pic = "", s_depthead = "",S_REGULATOR="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (base.Request.QueryString["ID"].Equals("BI"))
                {

                    Lb_judul.Text = "BANK INDONESIA";
                    S_REGULATOR = "IN001";
                }
                else if (base.Request.QueryString["ID"].Equals("OJK"))
                {

                    Lb_judul.Text = "OTORITAS JASA KEUANGAN";
                    S_REGULATOR = "IN002";

                }
                else if (base.Request.QueryString["ID"].Equals("OJKPASARMODAL"))
                {

                    Lb_judul.Text = "OTORITAS JASA KEUANGAN PASAR MODAL";
                    S_REGULATOR = "IN003";

                }
                else if (base.Request.QueryString["ID"].Equals("PAJAK"))
                {

                    Lb_judul.Text = "DIREKTORAT PAJAK";
                    S_REGULATOR = "IN004";

                }
                else if (base.Request.QueryString["ID"].Equals("PTATK"))
                {

                    Lb_judul.Text = "PUSAT PELAPORAN DAN ANALISIS TRANSAKSI KEUANGAN";
                    S_REGULATOR = "IN005";

                }
                else if (base.Request.QueryString["ID"].Equals("MANDIRI"))
                {

                    Lb_judul.Text = "BANK MANDIRI";
                    S_REGULATOR = "IN006";

                }
                else if (base.Request.QueryString["ID"].Equals("REGULATOR"))
                {

                    Lb_judul.Text = "REGULATOR LAINNYA";
                    S_REGULATOR = "IN007";

                }
                Bind_DataList();
                Bind_DataList7();
                Bind_DataList3();
                Bind_DataList1();
                // RetreivedataDDL();


            }
        }
        //private void RetreivedataDDL()
        //{
        //    try
        //    {

        //        StaticFramework.Reff(this.ddl_UNIT, Q_UNIT, null, this.conn, false);

        //    }
        //    catch (Exception e)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
        //    }
        //}

        public void Bind_DataList()
        {
            DataTable dt = new DataTable();


            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS  WHERE INSTANSIID ='" + S_REGULATOR + "' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND (userid =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD") )
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS  WHERE INSTANSIID ='" + S_REGULATOR + "' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='')  order by TGLSURAT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("004KADIV"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM [VW_APPMRS_KADIV]  WHERE INSTANSIID ='" + S_REGULATOR + "' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='')  AND (KADIV =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by TGLSURAT desc", null, this.dbtimeout, true, true);

            }
            else {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS_ADMIN  WHERE INSTANSIID ='" + S_REGULATOR + "' order by TGLSURAT desc", null, this.dbtimeout, true, true);

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

        public void Bind_DataList7()
        {
            DataTable dt = new DataTable();


            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "' AND HARI='7' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([PIC] =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='7' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([DEPTHEAD] =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("004KADIV"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='7' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([DEPTHEAD] in (select USERID from SCALLUSER where SU_UPLINER ='" + Session["USERID"].ToString() + "') or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='7' order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            if (dt.Rows.Count > 0)
            {
                DataList7.DataSource = dt;
                DataList7.DataBind();
                lb_data7.Visible = false;
            }
            else
            {
                DataList7.DataSource = dt;
                DataList7.DataBind();
                lb_data7.Visible = true;
            }

        }
        public void Bind_DataList3()
        {
            DataTable dt = new DataTable();


            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='3' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([PIC] =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='3' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([DEPTHEAD] =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("004KADIV"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='3' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([DEPTHEAD] in (select USERID from SCALLUSER where SU_UPLINER ='" + Session["USERID"].ToString() + "') or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='3' order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            if (dt.Rows.Count > 0)
            {
                DataList3.DataSource = dt;
                DataList3.DataBind();
                lb_data3.Visible = false;
            }
            else
            {
                DataList3.DataSource = dt;
                DataList3.DataBind();
                lb_data3.Visible = true;
            }

        }
        public void Bind_DataList1()
        {
            DataTable dt = new DataTable();


            if (Session["GROUPID"].ToString().Equals("002PEN"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='1' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([PIC] =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("003HEAD"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='1' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([DEPTHEAD] =  '" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else if (Session["GROUPID"].ToString().Equals("004KADIV"))
            {

                s_unit = Session["UnitID"].ToString();
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='1' AND (UNIT =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='') AND ([DEPTHEAD] in (select USERID from SCALLUSER where SU_UPLINER = '" + Session["USERID"].ToString() + "') or isnull('" + Session["USERID"].ToString() + "' ,'')='')   order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            else
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_DASHBOARD  WHERE INSTANSIID ='" + S_REGULATOR + "'  AND HARI='1' order by DATETIMELIMIT desc", null, this.dbtimeout, true, true);

            }
            if (dt.Rows.Count > 0)
            {
                DataList1.DataSource = dt;
                DataList1.DataBind();
                lb_data1.Visible = false;
            }
            else
            {
                DataList1.DataSource = dt;
                DataList1.DataBind();
                lb_data1.Visible = true;
            }

        }
        //public void UNIT_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    //            StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'", null, this.conn, false);

        //    //StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' ", null, this.conn, false);

        //    StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + ddl_UNIT.SelectedValue.ToString() + "' and [GROUPID] ='002PEN'", null, this.conn, false);

        //    StaticFramework.Reff(this.DEPTHEAD, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + ddl_UNIT.SelectedValue.ToString() + "' and [GROUPID] ='003HEAD'", null, this.conn, false);

        //}
        #region--Generate Excel--
        protected void lnkGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {


                var dt = this.conn.GetDataTable("SELECT  * FROM VW_EXPORTKOMENTAR where  (DIVISI =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='')  and (PIC =  '" + s_pic + "' or isnull('" + s_pic + "' ,'')='')  and (PIC =  '" + s_pic + "' or isnull('" + s_pic + "' ,'')='') order by [TANGGAL SURAT] asc", null, this.dbtimeout, true, true);


                if (dt.Rows.Count > 0)
                {
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Report Status Approval.xls"));
                    Response.ContentType = "application/ms-excel";



                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GridView gd = new GridView();

                    gd.DataSource = dt;
                    gd.DataBind();
                    //Change the Header Row back to white color
                    gd.HeaderRow.Style.Add("background-color", "#FFFFFF");
                    //Applying stlye to gridview header cells
                    for (int i = 0; i < gd.HeaderRow.Cells.Count; i++)
                    {
                        gd.HeaderRow.Cells[i].Style.Add("background-color", "#ffff00");
                    }
                    gd.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('data tidak ada');", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion
        protected void DataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList.PageIndex = e.NewPageIndex;
        }
        protected void DataList_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList();

        }

        protected void DataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void DataList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();
                //string AP = EncryptAndDecrypt.Encrypt("1");
                string AP ="1";

                if (e.CommandName == "View")
                {
                    Response.Redirect("~/MRS/Input_komentar.aspx?NS=" + NOSURAT + "&AP=" + AP + "");


                    //Server.Transfer("~/MRS/Input_komentar.aspx?NS=" + NOSURAT + "&AP=" + AP + "");
                }
                else if (e.CommandName == "Approve")
                {
                    Approve_Data(e.CommandArgument.ToString());

                    this.Bind_DataList();
                   // Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }


        protected void DataList7_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList7.PageIndex = e.NewPageIndex;
        }
        protected void DataList7_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList7();

        }

        protected void DataList7_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void DataList7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();

                if (e.CommandName == "View")
                {
                   // string s_instansi = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[0].ToString());
                   // string s_Jenis = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[2].ToString());
                   // string s_Unit = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[1].ToString());


                    string s_instansi = e.CommandArgument.ToString().Split(':')[0].ToString();
                    string s_Jenis = e.CommandArgument.ToString().Split(':')[2].ToString();
                    string s_Unit = e.CommandArgument.ToString().Split(':')[1].ToString();

                     Response.Redirect("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);

                    //Server.Transfer("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }

        protected void DataList3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList3.PageIndex = e.NewPageIndex;
        }
        protected void DataList3_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList3();

        }

        protected void DataList3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void DataList3_RowCommand(object sender, GridViewCommandEventArgs e)
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
                     Response.Redirect("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);


                    //Server.Transfer("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }


        protected void DataList1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList1.PageIndex = e.NewPageIndex;
        }
        protected void DataList1_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList1();

        }

        protected void DataList1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void DataList1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();

                if (e.CommandName == "View")
                {
                    //string s_instansi = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[0].ToString());
                    //string s_Jenis = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[2].ToString());
                    //string s_Unit = EncryptAndDecrypt.Encrypt(e.CommandArgument.ToString().Split(':')[1].ToString());
                    //Response.Redirect("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);

                    


                    string s_instansi = e.CommandArgument.ToString().Split(':')[0].ToString();
                    string s_Jenis = e.CommandArgument.ToString().Split(':')[2].ToString();
                    string s_Unit = e.CommandArgument.ToString().Split(':')[1].ToString();

                   Response.Redirect("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);
                   // Server.Transfer("~/MRS/InputData.aspx?IN=" + s_instansi + "&JN=" + s_Jenis + "&UN" + s_Unit);
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }
        //protected void cari_Click(object sender, EventArgs e)
        //{
        //    s_unit = ddl_UNIT.SelectedItem.Text;
        //    s_pic = PIC.SelectedValue.ToString();
        //    s_depthead = DEPTHEAD.SelectedValue.ToString();

        //    var dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='')  and (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

        //    if (dt.Rows.Count > 0)
        //    {
        //        DataList.DataSource = dt;
        //        DataList.DataBind();
        //        lb_data.Visible = false;
        //    }
        //    else
        //    {
        //        DataList.DataSource = dt;
        //        DataList.DataBind();
        //        lb_data.Visible = true;
        //    }

        //}
        public void Approve_Data(String NOSURAT)
        {
            try
            {

                object[] param = new object[] { NOSURAT };
                conn.ExecNonQuery(Q_Approve, param, dbtimeout);

                Response.Write("<script>alert('Berhasil Approve data '  " + NOSURAT + "')</script>");
                return;
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error approve');", true);
            }

        }
    }
}