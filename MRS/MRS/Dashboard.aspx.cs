using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CID.Framework;
using MRS.Web;

namespace MRS.DataEntry
{
    public partial class Default : DataPage
    {
        private static string Q_RFINSTANSI = "";
        private static string Q_RFINSTANSICOUNT = "";

        private void Retreivedata()
        {
            try
            {
                System.Data.DataTable dt = null;



                if (Session["GROUPID"].ToString().Equals("002PEN"))
                {
                    Q_RFINSTANSI = "SELECT * FROM VW_DASBOARD_COUNT_USER where USERCREATED ='" + Session["userid"].ToString() + "'";
                    Q_RFINSTANSICOUNT = "SELECT * FROM [VW_DASBOARD_COUNT_USER1] where USERCREATED ='" + Session["userid"].ToString() + "'";

                    dt = this.conn.GetDataTable("SELECT distinct [INSTANSIID],[PIC],[UNIT] FROM [MRSDATA].[dbo].[DATA_COMPLIANCE] where PIC='" + Session["Userid"].ToString() + "'", null, this.dbtimeout, true, true);

                    if (dt.Rows.Count > 0)
                    {
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN001"))
                            {
                                IN001.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN002"))
                            {
                                IN002.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN003"))
                            {
                                IN003.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN004"))
                            {
                                IN004.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN005"))
                            {
                                IN005.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN006"))
                            {
                                IN006.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN007"))
                            {
                                IN007.Visible = true;

                            }
                        }


                    }
                }
                else if (Session["GROUPID"].ToString().Equals("003HEAD") )
                {
                    Q_RFINSTANSI = "SELECT * FROM VW_DASBOARD_COUNT_UNIT where UNIT ='" + Session["UnitID"].ToString() + "' and (DEPTHEAD='" + Session["Userid"].ToString() + "') ";

                    Q_RFINSTANSICOUNT = "SELECT * FROM VW_DASBOARD_COUNT_UNIT1 where UNIT ='" + Session["UnitID"].ToString() + "' and (DEPTHEAD='" + Session["Userid"].ToString() + "') ";

                    dt = this.conn.GetDataTable("SELECT distinct [INSTANSIID],[DEPTHEAD],[UNIT] FROM [MRSDATA].[dbo].[DATA_COMPLIANCE] where DEPTHEAD='" + Session["Userid"].ToString() + "'", null, this.dbtimeout, true, true);

                    if (dt.Rows.Count > 0)
                    {
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN001"))
                            {
                                IN001.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN002"))
                            {
                                IN002.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN003"))
                            {
                                IN003.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN004"))
                            {
                                IN004.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN005"))
                            {
                                IN005.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN006"))
                            {
                                IN006.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN007"))
                            {
                                IN007.Visible = true;

                            }
                        }


                    }
                }
                else if (Session["GROUPID"].ToString().Equals("004KADIV"))
                {
                    Q_RFINSTANSI = "SELECT * FROM [VW_DASBOARD_COUNT_KADIV] where UNIT ='" + Session["UnitID"].ToString() + "' and (USERID='" + Session["Userid"].ToString() + "') ";
                    Q_RFINSTANSICOUNT = "SELECT * FROM VW_DASBOARD_COUNT_UNIT1 where UNIT ='" + Session["UnitID"].ToString() + "' and (DEPTHEAD in (select USERID from SCALLUSER where SU_UPLINER='" + Session["Userid"].ToString() + "')) ";

                    dt = this.conn.GetDataTable("SELECT distinct [INSTANSIID],[DEPTHEAD],[UNIT] FROM [MRSDATA].[dbo].[DATA_COMPLIANCE] where DEPTHEAD IN (select USERID from SCALLUSER where SU_UPLINER ='" + Session["Userid"].ToString() + "' and UNIT='"+ Session["uNITiD"].ToString() + "') and UNIT='" + Session["uNITiD"].ToString() + "'", null, this.dbtimeout, true, true);

                    if (dt.Rows.Count > 0)
                    {
                        for (int a = 0; a < dt.Rows.Count; a++)
                        {
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN001"))
                            {
                                IN001.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN002"))
                            {
                                IN002.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN003"))
                            {
                                IN003.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN004"))
                            {
                                IN004.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN005"))
                            {
                                IN005.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN006"))
                            {
                                IN006.Visible = true;

                            }
                            if (dt.Rows[a]["INSTANSIID"].ToString().Equals("IN007"))
                            {
                                IN007.Visible = true;

                            }
                        }


                    }
                }
                else
                {
                    Q_RFINSTANSI = "SELECT * FROM VW_DASBOARD_COUNT";
                    IN001.Visible = true;
                    IN002.Visible = true;
                    IN003.Visible = true;
                    IN004.Visible = true;
                    IN005.Visible = true;
                    IN006.Visible = true;
                    IN007.Visible = true;
                }


                System.Data.DataTable dataTable = this.conn.GetDataTable(Q_RFINSTANSI, null, this.dbtimeout, true, true);

                if (dataTable.Rows.Count > 0)
                {
                    StaticFramework.Retrieve(dataTable, this.count_BI);
                    StaticFramework.Retrieve(dataTable, this.count_OJK);
                    StaticFramework.Retrieve(dataTable, this.count_OJK_PASARMODAL);
                    StaticFramework.Retrieve(dataTable, this.count_PAJAK);
                    StaticFramework.Retrieve(dataTable, this.count_PPATK);
                    StaticFramework.Retrieve(dataTable, this.count_REGULATOR_LAIN);
                    StaticFramework.Retrieve(dataTable, this.count_MANDIRI);

                    StaticFramework.Retrieve(dataTable, this.BI_7);
                    StaticFramework.Retrieve(dataTable, this.BI_3);
                    StaticFramework.Retrieve(dataTable, this.BI_1);
                    StaticFramework.Retrieve(dataTable, this.OJK_7);
                    StaticFramework.Retrieve(dataTable, this.OJK_3);
                    StaticFramework.Retrieve(dataTable, this.OJK_1);
                    StaticFramework.Retrieve(dataTable, this.OJKPASARMODAL_7);
                    StaticFramework.Retrieve(dataTable, this.OJKPASARMODAL_3);
                    StaticFramework.Retrieve(dataTable, this.OJKPASARMODAL_1);
                    StaticFramework.Retrieve(dataTable, this.PAJAK_7);
                    StaticFramework.Retrieve(dataTable, this.PAJAK_3);
                    StaticFramework.Retrieve(dataTable, this.PAJAK_1);
                    StaticFramework.Retrieve(dataTable, this.PPATK_7);
                    StaticFramework.Retrieve(dataTable, this.PPATK_3);
                    StaticFramework.Retrieve(dataTable, this.PPATK_1);
                    StaticFramework.Retrieve(dataTable, this.MANDIRI_7);
                    StaticFramework.Retrieve(dataTable, this.MANDIRI_3);
                    StaticFramework.Retrieve(dataTable, this.MANDIRI_1);
                    StaticFramework.Retrieve(dataTable, this.REGULATOR_7);
                    StaticFramework.Retrieve(dataTable, this.REGULATOR_3);
                    StaticFramework.Retrieve(dataTable, this.REGULATOR_1);
                }
                else {
                    count_BI.Text = "0";
                    count_OJK.Text = "0";
                    count_OJK_PASARMODAL.Text = "0";
                    count_PAJAK.Text = "0";
                    count_PPATK.Text = "0";
                    count_REGULATOR_LAIN.Text = "0";
                    count_MANDIRI.Text = "0";

                    BI_7.Text = "0";
                    BI_3.Text = "0";
                    BI_1.Text = "0";
                    OJK_7.Text = "0";
                    OJK_3.Text = "0";
                    OJK_1.Text = "0";
                    OJKPASARMODAL_7.Text = "0";
                    OJKPASARMODAL_3.Text = "0";
                    OJKPASARMODAL_1.Text = "0";
                    PAJAK_7.Text = "0";
                    PAJAK_3.Text = "0";
                    PAJAK_1.Text = "0";
                    PPATK_7.Text = "0";
                    PPATK_3.Text = "0";
                    PPATK_1.Text = "0";
                    MANDIRI_7.Text = "0";
                    MANDIRI_3.Text = "0";
                    MANDIRI_1.Text = "0";
                    REGULATOR_7.Text = "0";
                    REGULATOR_3.Text = "0";
                    REGULATOR_1.Text = "0";
                }

                if (!Q_RFINSTANSICOUNT.Equals("") && dataTable.Rows.Count <= 0) {

                    System.Data.DataTable dataTable1 = this.conn.GetDataTable(Q_RFINSTANSICOUNT, null, this.dbtimeout, true, true);

                    if (dataTable1.Rows.Count > 0)
                    {

                        StaticFramework.Retrieve(dataTable1, this.BI_7);
                        StaticFramework.Retrieve(dataTable1, this.BI_3);
                        StaticFramework.Retrieve(dataTable1, this.BI_1);
                        StaticFramework.Retrieve(dataTable1, this.OJK_7);
                        StaticFramework.Retrieve(dataTable1, this.OJK_3);
                        StaticFramework.Retrieve(dataTable1, this.OJK_1);
                        StaticFramework.Retrieve(dataTable1, this.OJKPASARMODAL_7);
                        StaticFramework.Retrieve(dataTable1, this.OJKPASARMODAL_3);
                        StaticFramework.Retrieve(dataTable1, this.OJKPASARMODAL_1);
                        StaticFramework.Retrieve(dataTable1, this.PAJAK_7);
                        StaticFramework.Retrieve(dataTable1, this.PAJAK_3);
                        StaticFramework.Retrieve(dataTable1, this.PAJAK_1);
                        StaticFramework.Retrieve(dataTable1, this.PPATK_7);
                        StaticFramework.Retrieve(dataTable1, this.PPATK_3);
                        StaticFramework.Retrieve(dataTable1, this.PPATK_1);
                        StaticFramework.Retrieve(dataTable1, this.MANDIRI_7);
                        StaticFramework.Retrieve(dataTable1, this.MANDIRI_3);
                        StaticFramework.Retrieve(dataTable1, this.MANDIRI_1);
                        StaticFramework.Retrieve(dataTable1, this.REGULATOR_7);
                        StaticFramework.Retrieve(dataTable1, this.REGULATOR_3);
                        StaticFramework.Retrieve(dataTable1, this.REGULATOR_1);
                    }
                    else
                    {
                        BI_7.Text = "0";
                        BI_3.Text = "0";
                        BI_1.Text = "0";
                        OJK_7.Text = "0";
                        OJK_3.Text = "0";
                        OJK_1.Text = "0";
                        OJKPASARMODAL_7.Text = "0";
                        OJKPASARMODAL_3.Text = "0";
                        OJKPASARMODAL_1.Text = "0";
                        PAJAK_7.Text = "0";
                        PAJAK_3.Text = "0";
                        PAJAK_1.Text = "0";
                        PPATK_7.Text = "0";
                        PPATK_3.Text = "0";
                        PPATK_1.Text = "0";
                        MANDIRI_7.Text = "0";
                        MANDIRI_3.Text = "0";
                        MANDIRI_1.Text = "0";
                        REGULATOR_7.Text = "0";
                        REGULATOR_3.Text = "0";
                        REGULATOR_1.Text = "0";
                    }

                }
            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('error : get data')", true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Retreivedata();
                string session = Session["UserID"].ToString();
            }

        }

    }
}