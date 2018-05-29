using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CID.Framework;
using MRS.Models;
using MRS.Web;

namespace MRS.MRS
{
    public partial class Report_Komentar : DataPage
    {
        static string Q_Approve = "exec SP_APPROVE @1";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string s_unit = "", s_pic = "", s_depthead = "";
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

                StaticFramework.Reff(this.ddl_UNIT, Q_UNIT, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
            }
        }

        public void Bind_DataList()
        {

            var dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS_ADMIN order by TGLSURAT desc", null, this.dbtimeout, true, true);
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
        public void UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {

            //            StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' AND INSTANSIID ='" + INSTANSIID.SelectedValue.ToString() + "'", null, this.conn, false);

            //StaticFramework.Reff(this.JENIS, "SELECT REPORTID , REPORTNAME FROM VW_JENISREPORT WHERE UNITID ='" + UNIT.SelectedValue.ToString() + "' ", null, this.conn, false);

            StaticFramework.Reff(this.PIC, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + ddl_UNIT.SelectedValue.ToString() + "' and [GROUPID] ='002PEN'", null, this.conn, false);

            StaticFramework.Reff(this.DEPTHEAD, "SELECT [USERID] , [SU_FULLNAME] FROM  [MRSDATA].[dbo].[VW_USER] WHERE UNIT ='" + ddl_UNIT.SelectedValue.ToString() + "' and [GROUPID] ='003HEAD'", null, this.conn, false);

        }
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

            s_unit = ddl_UNIT.SelectedItem.Text;
            s_pic = PIC.SelectedValue.ToString();
            s_depthead = DEPTHEAD.SelectedValue.ToString();

            var dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS_ADMIN where  (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='')  and (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

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

        protected void DataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void DataList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();
                string AP = "1";// EncryptAndDecrypt.Encrypt("1");

                if (e.CommandName == "View")
                {
                    Response.Redirect("~/MRS/Input_komentar.aspx?NS=" + NOSURAT + "&AP=" + AP + "");

                    //Server.Transfer("~/MRS/Input_komentar.aspx?NS=" + NOSURAT + "&AP=" + AP + "");
                }
                else if (e.CommandName == "Approve")
                {
                    Approve_Data(e.CommandArgument.ToString());
                    //Response.Redirect(Request.RawUrl);

                    this.Bind_DataList();
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }
        protected void cari_Click(object sender, EventArgs e)
        {
            s_unit = ddl_UNIT.SelectedItem.Text;
            s_pic = PIC.SelectedValue.ToString();
            s_depthead = DEPTHEAD.SelectedValue.ToString();

            var dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS_ADMIN where  (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='')  and (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

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