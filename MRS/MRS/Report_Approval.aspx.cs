using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CID.Framework;
using MRS.Models;
using MRS.Web;


namespace MRS.MRS
{
    public partial class Report_Approval : DataPage
    {
        static string Q_Approve = "exec SP_APPROVE @1,@2";
        private static string Q_UNIT = "SELECT [UNITID],[DIVISI] FROM UNIT where active ='1' ORDER BY [DIVISI] ASC";
        private static string Q_RFINSTANSI = "SELECT [INSTANSIID],[INSTANSINAME] FROM VW_RFINSTANSI";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RetreivedataDDL();
                Bind_DataList();


                if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
                {

                    div_unit.Style.Add("display", "");
                }
                else
                {

                    div_unit.Style.Add("display", "NONE");
                }
            }


        }

        public void Bind_DataList()
        {

            System.Data.DataTable dataTable = null;

            if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
            {
                dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_APPMRS_ADMIN]  order by TGLSURAT desc", null, this.dbtimeout, true, true);
            }
            else if (Session["groupid"].ToString().Equals("004KADIV"))
            {
                //dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  AND ([DEPTHEAD] in (select USERID from SCALLUSER where SU_UPLINER ='" + Session["USERID"].ToString() + "') or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

                dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  AND ([DEPTHEAD]  ='" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

            }

            else
            {
                dataTable = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  and DEPTHEAD ='" + Session["Userid"].ToString() + "' order by TGLSURAT asc", null, this.dbtimeout, true, true);


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
        private void RetreivedataDDL()
        {
            try
            {

                StaticFramework.Reff(this.ddl_UNIT, Q_UNIT, null, this.conn, false);
                StaticFramework.Reff(this.INSTANSIID, Q_RFINSTANSI, null, this.conn, false);

            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : get ddl')", true);
            }
        }
        protected void cari_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt;
            if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS_ADMIN where  (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='')  and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

            }
            else if (Session["groupid"].ToString().Equals("004KADIV"))
            {

                //dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  AND ([DEPTHEAD] in (select USERID from SCALLUSER where SU_UPLINER ='" + Session["USERID"].ToString() + "') or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  AND ([DEPTHEAD]  ='" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);
            }
            else
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='') and DEPTHEAD ='" + Session["Userid"].ToString() + "' order by TGLSURAT asc", null, this.dbtimeout, true, true);


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

        protected void DataList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.DataList.PageIndex = e.NewPageIndex;
        }
        protected void DataList_PageIndexChanged(object sender, EventArgs e)
        {

            System.Data.DataTable dt;
            if (Session["groupid"].ToString().Equals("00") || Session["groupid"].ToString().Equals("001COM"))
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS_ADMIN where  (UNIT =  '" + ddl_UNIT.SelectedValue.ToString() + "' or isnull('" + ddl_UNIT.SelectedValue.ToString() + "' ,'')='')  and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

            }
            else if (Session["groupid"].ToString().Equals("004KADIV"))
            {
                //dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  AND ([DEPTHEAD] in (select USERID from SCALLUSER where SU_UPLINER ='" + Session["USERID"].ToString() + "') or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  AND ([DEPTHEAD]  ='" + Session["USERID"].ToString() + "' or isnull('" + Session["USERID"].ToString() + "' ,'')='') order by TGLSURAT asc", null, this.dbtimeout, true, true);

            }
            else
            {
                dt = this.conn.GetDataTable("SELECT  * FROM VW_APPMRS where  (UNIT =  '" + Session["Unitid"].ToString() + "' or isnull('" + Session["Unitid"].ToString() + "' ,'')='')  and (INSTANSIID =  '" + INSTANSIID.SelectedValue.ToString() + "' or isnull('" + INSTANSIID.SelectedValue.ToString() + "' ,'')='') and DEPTHEAD ='" + Session["Userid"].ToString() + "' order by TGLSURAT asc", null, this.dbtimeout, true, true);


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

        protected void DataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void DataList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();
                //string AP = EncryptAndDecrypt.Encrypt("1");
                string AP = "1";

                if (e.CommandName == "View")
                {
                    Response.Redirect("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=" + AP + "");

                    //Server.Transfer("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=" + AP + "");
                }
                if (e.CommandName == "Edit")
                {
                    Response.Redirect("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=");

                    //Server.Transfer("~/MRS/Input_Komentar.aspx?NS=" + NOSURAT + "&AP=");
                }
                //else if (e.CommandName == "Approve")
                //{
                //    Approve_Data(e.CommandArgument.ToString());
                //    Response.Redirect(Request.RawUrl);
                //}
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error data');", true);
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
                    if (btn.CommandName.Equals("Approve"))
                    {
                        Approve_Data(btn.CommandArgument.ToString());

                        this.Bind_DataList();
                        // Response.Redirect(Request.RawUrl);

                    }


                    // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                }
            }
            catch (Exception)
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Action')", true);
            }

        }

        public void Approve_Data(String NOSURAT)
        {
            try
            {

                object[] param = new object[] { NOSURAT, Session["UserID"].ToString() };
                conn.ExecNonQuery(Q_Approve, param, dbtimeout);


                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('Berhasil Approve data ');", true);

                //Response.Write("<script>alert('Berhasil Approve data '  " + NOSURAT + "')</script>");
                //return;
            }
            catch (Exception e)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error approve');", true);
            }

        }

    }
}