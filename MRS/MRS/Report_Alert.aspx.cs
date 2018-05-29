using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.Web;
using System.Data;
using MRS.log;
using System.Text;
using CID.Framework;
using System.IO;

namespace MRS.MRS
{
    public partial class Report_Alert : DataPage
    {

        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT  where active ='1' ORDER BY [DIVISI] ASC";
        private static string Q_UNIT1 = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string s_unit = "", s_unitemail = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RetreivedataDDL();
                Bind_DataList();
                Bind_DataList_EMAIL();
            }
        }
        private void RetreivedataDDL()
        {
            try
            {

                StaticFramework.Reff(this.ddl_UNIT1, Q_UNIT1, null, this.conn, false);
                StaticFramework.Reff(this.ddl_UNIT, Q_UNIT, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : getdata')", true);
            }
        }
        protected void cari_Click(object sender, EventArgs e)
        {

            s_unit = ddl_UNIT.SelectedItem.Text;

            var dt = this.conn.GetDataTable("SELECT  * FROM vw_alert_sms where (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='')  order by TGL_SMS asc", null, this.dbtimeout, true, true);

            if (dt.Rows.Count > 0)
            {
                DataList_sms.DataSource = dt;
                DataList_sms.DataBind();
                lb_Sms.Visible = false;
            }
            else
            {
                DataList_sms.DataSource = dt;
                DataList_sms.DataBind();
                lb_Sms.Visible = true;
            }

        }
        #region--Generate Excel--
        protected void lnkGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {


                var dt = this.conn.GetDataTable("SELECT  * FROM VW_EXPORTSMS where  (DIVISI =  '" + s_unit + "' or isnull('" + s_unit + "' ,'')='')  order by [TANGGAL KIRIM] asc", null, this.dbtimeout, true, true);


                if (dt.Rows.Count > 0)
                {
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Report Alert Sms.xls"));
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
        protected void lnkGenerateReportEmail_Click(object sender, EventArgs e)
        {
            try
            {


                var dt = this.conn.GetDataTable("SELECT  * FROM VW_EXPORTEMAIL where  (DIVISI =  '" + s_unitemail + "' or isnull('" + s_unitemail + "' ,'')='')  order by [TANGGAL KIRIM] asc", null, this.dbtimeout, true, true);


                if (dt.Rows.Count > 0)
                {
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Report Alert Email.xls"));
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
        protected void search_Click(object sender, EventArgs e)
        {
            s_unitemail = ddl_UNIT1.SelectedItem.Text;
            var dt = this.conn.GetDataTable("SELECT  * FROM vw_alert_EMAIL where (UNIT =  '" + ddl_UNIT1.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT1.SelectedValue.ToString() + "' ,'')='')  order by TGL_EMAIL asc", null, this.dbtimeout, true, true);


            if (dt.Rows.Count > 0)
            {
                DataList_Email.DataSource = dt;
                DataList_Email.DataBind();
                Lb_Email.Visible = false;
            }
            else
            {
                DataList_Email.DataSource = dt;
                DataList_Email.DataBind();
                Lb_Email.Visible = true;
            }
        }
        public void Bind_DataList()
        {
            var dt = this.conn.GetDataTable("SELECT  * FROM vw_alert_sms order by TGL_SMS asc", null, this.dbtimeout, true, true);
            if (dt.Rows.Count > 0)
            {
                DataList_sms.DataSource = dt;
                DataList_sms.DataBind();
                lb_Sms.Visible = false;
            }
            else
            {
                DataList_sms.DataSource = dt;
                DataList_sms.DataBind();
                lb_Sms.Visible = true;
            }
        }
        public void Bind_DataList_EMAIL()
        {
            var dt = this.conn.GetDataTable("SELECT  * FROM VW_ALERT_EMAIL order by TGL_EMAIL asc", null, this.dbtimeout, true, true);
            if (dt.Rows.Count > 0)
            {
                DataList_Email.DataSource = dt;
                DataList_Email.DataBind();
                Lb_Email.Visible = false;
            }
            else
            {
                DataList_Email.DataSource = dt;
                DataList_Email.DataBind();
                Lb_Email.Visible = true;
            }
        }
        protected void DataList_sms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList_sms.PageIndex = e.NewPageIndex;
        }
        protected void DataList_sms_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList();

        }
        protected void DataList_Email_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList_Email.PageIndex = e.NewPageIndex;
        }
        protected void DataList_Email_PageIndexChanged(object sender, EventArgs e)
        {

            this.Bind_DataList_EMAIL();

        }
    }
}