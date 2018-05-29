using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRS.Web;
using System.Data;
using MRS.log;
using System.Text;

namespace MRS.MRS
{
    public partial class Report_SMS : DataPage
    {

        static string Q_Sms = "select *from VW_SMS where NOSURAT =@1";
        static string Q_Template = "select *from SMSTEMPLATE ";
        private static string Q_InserSms = "exec SP_SMS @1, @2, @3, @4";
        private static string sms_template = "", isi_sms="";
        private static string strResult = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                Bind_DataList();


        }

        public void Bind_DataList()
        {

            System.Data.DataTable dataTable = this.conn.GetDataTable("SELECT  * FROM VW_SMS order by TGLSURAT desc", null, this.dbtimeout, true, true);
            DataList.DataSource = dataTable;
            DataList.DataBind();

        }


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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string no_surat = DataBinder.Eval(e.Row.DataItem, "NOSURAT").ToString();

            //    Button btn = (Button)e.Row.FindControl("btn_Approve");
            //    btn.OnClientClick = "return confirm('Do you really want to delete this record?'); return false; ";

            //   //btn.OnClientClick = "confirm('Are you sure you want to Approve this Nosurat "+DataBinder.Eval(e.Row.DataItem, "NOSURAT") + ")?'";

            //}
        }


        protected void DataList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string NOSURAT = e.CommandArgument.ToString();

                if (e.CommandName == "View")
                {
                    //Response.Redirect("~/MRS/InputData.aspx?nosurat=" + NOSURAT + "&approve=1");
                    Server.Transfer("~/MRS/InputData.aspx?nosurat=" + NOSURAT + "&approve=1");
                }
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('error : data');", true);
            }
        }
        protected void kirim_Click(object sender, EventArgs e)
        {


            foreach (GridViewRow row in DataList.Rows)
            {
                

                CheckBox cb = (CheckBox)row.FindControl("chkCheck");
                if (cb.Checked==true) {
                    if (cb != null)
                    {

                        send_SMS(row.Cells[3].Text.ToString());
                    }
                }
            }

        }


        protected void send_SMS(String NOSURAT)
        {
            object[] param = new object[] { NOSURAT };


            System.Data.DataTable dataTableSms = this.conn.GetDataTable(Q_Template, null, this.dbtimeout, true, true);
            if (dataTableSms.Rows.Count > 0)
            {
                sms_template = dataTableSms.Rows[0]["value"].ToString();
            }
            System.Data.DataTable dataTable = this.conn.GetDataTable(Q_Sms, param, this.dbtimeout, true, true);
            if (dataTable.Rows.Count > 0)
            {
                //NO_DEPHEAD
                SendSMS(NOSURAT, dataTable.Rows[0]["NO_DEPHEAD"].ToString(), sms_template, dataTable.Rows[0]["DIVISI"].ToString(), dataTable.Rows[0]["INSTANSINAME"].ToString(), dataTable.Rows[0]["TGL"].ToString());

                object[] param1 = new object[] { NOSURAT, dataTable.Rows[0]["NO_DEPHEAD"].ToString(), isi_sms, strResult };
                conn.ExecNonQuery(Q_InserSms, param1, dbtimeout);
                //NO_KADIV
                SendSMS(NOSURAT, dataTable.Rows[0]["NO_KADIV"].ToString(), sms_template, dataTable.Rows[0]["DIVISI"].ToString(), dataTable.Rows[0]["INSTANSINAME"].ToString(), dataTable.Rows[0]["TGL"].ToString());


                object[] param2 = new object[] { NOSURAT, dataTable.Rows[0]["NO_KADIV"].ToString(), isi_sms, strResult };
                conn.ExecNonQuery(Q_InserSms, param2, dbtimeout);
                //NO_DEIREKSI
                SendSMS(NOSURAT, dataTable.Rows[0]["NO_DIREKSI"].ToString(), sms_template, dataTable.Rows[0]["DIVISI"].ToString(), dataTable.Rows[0]["INSTANSINAME"].ToString(), dataTable.Rows[0]["TGL"].ToString());

                object[] param3 = new object[] { NOSURAT, dataTable.Rows[0]["NO_DIREKSI"].ToString(), isi_sms, strResult };
                conn.ExecNonQuery(Q_InserSms, param3, dbtimeout);
            }

        }
        
        public static void SendSMS(String NOSURAT, String NOMOR, String SMS_TEMPLATE, String DIVISI, String INSTANSI, String TGL)
        {
            DataTable dttemplate = new DataTable();
            StringBuilder transmessage = new StringBuilder();
            string strStatus = ConfigurationSettings.AppSettings["status"];
            string strIP = ConfigurationSettings.AppSettings["ip"];
            string strPort = ConfigurationSettings.AppSettings["port"];
            string strUserid = ConfigurationSettings.AppSettings["userid"];
            string strPassword = ConfigurationSettings.AppSettings["password"];
            string strDate = DateTime.Now.ToString("dd/MM/yyyy");
            LogFile ilog = new LogFile();


            if (NOMOR.Length > 0)
            {


                transmessage.AppendFormat(SMS_TEMPLATE, DIVISI, INSTANSI, TGL);
                if (transmessage.Length > 160)
                {
                    INSTANSI = INSTANSI.Substring(0, INSTANSI.Length - (transmessage.Length - 160));
                    transmessage.Clear();
                    transmessage.AppendFormat(SMS_TEMPLATE, DIVISI, INSTANSI, TGL);
                }



                try
                {
                    String sms = "https://sms-api.jatismobile.com/index.ashx?userid=BANKMANTAP&password=BANKMANTAP123&msisdn=" + NOMOR + "&message=" + transmessage + "&sender=BANKMANTAP&division=IT DIVISION&batchname=test&uploadby=YULIUS&channel=1";
                    ilog.MyLogFile("URL SMS =", sms);

                    //  System.Net.WebRequest req = System.Net.WebRequest.Create("https://sms-api.jatismobile.com/index.ashx?userid=BANKMANTAP&password=BANKMANTAP123&msisdn=" + NOMOR + "&message=" + transmessage + "&sender=BANKMANTAP&division=IT DIVISION&batchname=test&uploadby=YULIUS&channel=1");

                    System.Net.WebRequest req = System.Net.WebRequest.Create(sms);



                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                    strResult = sr.ReadToEnd().Trim();
                    isi_sms = transmessage.ToString();
                     

                }
                catch (Exception ex)
                {
                    ilog.MyLogFile("SMS", ex.Message.ToString());
                }

            }
        }




        //public bool sendNoReplyMail(String senderAlias, String emailTo, String emailCC, String emailBCC, String emailSubject, String emailContent)
        //{
        //    Map<String, String> mailProp = Function.getMailProp();
        //    Properties props = new Properties();
        //    props.put("mail.host", mailProp.get(Constant.MAIL_HOST));
        //    props.put("mail.port", mailProp.get(Constant.MAIL_PORT));
        //    Session session = Session.getDefaultInstance(props);
        //    try
        //    {
        //        MimeMessage message = new MimeMessage(session);
        //        message.setFrom(new InternetAddress(mailProp.get(Constant.MAIL_SENDER), senderAlias));
        //        message.setRecipients(Message.RecipientType.TO, InternetAddress.parse(emailTo));
        //        if (((emailCC != null)
        //                    && (emailCC.trim().length() != 0)))
        //        {
        //            message.setRecipients(Message.RecipientType.CC, InternetAddress.parse(emailCC));
        //        }

        //        if (((emailBCC != null)
        //                    && (emailBCC.trim().length() != 0)))
        //        {
        //            message.setRecipients(Message.RecipientType.BCC, InternetAddress.parse(emailBCC));
        //        }

        //        message.setSubject(emailSubject);
        //        message.setContent(emailContent, "text/html; charset=UTF-8");
        //        Transport transport = session.getTransport("smtp");
        //        System.out.println("Connecting...");
        //        transport.connect();
        //        System.out.println("Sending...");
        //        transport.sendMessage(message, message.getAllRecipients());
        //        transport.close();
        //        System.out.println("Email Sent");
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        //  TODO: handle exception
        //        e.printStackTrace();
        //        return false;
        //    }

        //}

        //public bool sendNoReplyMailSSL(String senderAlias, String emailTo, String emailCC, String emailBCC, String emailSubject, String emailContent)
        //{
        //    Map<String, String> mailProp = Function.getMailProp();
        //    Properties props = new Properties();
        //    //         props.put("mail.host", mailProp.get(Constant.MAIL_HOST));
        //    //         props.put("mail.port", mailProp.get(Constant.MAIL_PORT));
        //    //         props.put("mail.smtp.host", mailProp.get(Constant.MAIL_HOST));    
        //    //         props.put("mail.smtp.auth", "true");
        //    props.setProperty("mail.transport.protocol", "smtp");
        //    props.setProperty("mail.host", mailProp.get(Constant.MAIL_HOST));
        //    props.put("mail.smtp.auth", "true");
        //    props.put("mail.smtps.ssl.checkserveridentity", "false");
        //    props.put("mail.smtps.ssl.trust", "*");
        //    props.put("mail.smtp.port", mailProp.get(Constant.MAIL_PORT_SSL));
        //    // props.put("mail.debug", "true"); 
        //    props.put("mail.debug", "false");
        //    props.put("mail.smtp.socketFactory.port", mailProp.get(Constant.MAIL_PORT_SSL));
        //    props.put("mail.smtp.socketFactory.class", "javax.net.ssl.SSLSocketFactory");
        //    props.put("mail.smtp.socketFactory.fallback", "false");
        //    Session session = Session.getDefaultInstance(props, new javax.mail.Authenticator());
        //    try
        //    {
        //        MimeMessage message = new MimeMessage(session);
        //        message.setFrom(new InternetAddress(mailProp.get(Constant.MAIL_SENDER), senderAlias));
        //        message.setRecipients(Message.RecipientType.TO, InternetAddress.parse(emailTo));
        //        if (((emailCC != null)
        //                    && (emailCC.trim().length() != 0)))
        //        {
        //            message.setRecipients(Message.RecipientType.CC, InternetAddress.parse(emailCC));
        //        }

        //        if (((emailBCC != null)
        //                    && (emailBCC.trim().length() != 0)))
        //        {
        //            message.setRecipients(Message.RecipientType.BCC, InternetAddress.parse(emailBCC));
        //        }

        //        message.setSubject(emailSubject);
        //        message.setContent(emailContent, "text/html; charset=UTF-8");
        //        Transport transport = session.getTransport("smtp");
        //        System.out.println("Connecting...");
        //        transport.connect();
        //        System.out.println("Sending...");
        //        transport.sendMessage(message, message.getAllRecipients());
        //        transport.close();
        //        System.out.println("Email Sent");
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        //  TODO: handle exception
        //        e.printStackTrace();
        //        return false;
        //    }

        //}

        //public static void main(String[] a)
        //{
        //    // yulius.ramliandy@gmail.com,yulius@bankmantap.co.id
        //    (new InstantMailService() + this.sendNoReplyMailSSL("mine", "yulius.ramliandy@gmail.com", null, null, "tess realy zerr", "err</br>relay"));
        //}
    }
}
