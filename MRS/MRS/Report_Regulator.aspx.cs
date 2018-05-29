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
    public partial class Report_Regulator : DataPage
    {

        private static string Q_Delete = "exec SP_DELETEREGULATOR @1";
        private string hash_password = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind_DataList();

            }

        }

        public void Bind_DataList()
        {

            var dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_RFINSTANSI] order by INSTANSINAME asc", null, this.dbtimeout, true, true);
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

            this.Bind_DataList();

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
                        delete_REGULATOR(btn.CommandArgument.ToString());
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
        
        protected void delete_REGULATOR(String userid)
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

        protected void edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string userid_en = btn.CommandArgument.ToString();// EncryptAndDecrypt.Encrypt(btn.CommandArgument.ToString());

            Response.Redirect("~/MRS/InputData_Regulator.aspx?ID=" + userid_en);

            //Server.Transfer("~/MRS/InputData_Regulator.aspx?ID=" + userid_en);

        }
        //protected void cari_Click(object sender, EventArgs e)
        //{


        //    var dataTable = this.conn.GetDataTable("SELECT  * FROM [VW_RFINSTANSI] where (USERID =  '" + s_userid.Value.ToString() + "' or isnull('" + s_userid.Value.ToString() + "' ,'')='')  and ( SU_FULLNAME ='" + s_nama.Value.ToString() + "' or isnull('" + s_nama.Value.ToString() + "','')='') order by SU_FULLNAME asc ", null, this.dbtimeout, true, true);
        //    if (dataTable.Rows.Count > 0)
        //    {
        //        DataList.DataSource = dataTable;
        //        DataList.DataBind();

        //        lb_User.Visible = false;
        //    }
        //    else
        //    {
        //        DataList.DataSource = dataTable;
        //        DataList.DataBind();

        //        lb_User.Visible = true;

        //    }


        //}
    }
}