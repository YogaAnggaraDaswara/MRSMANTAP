<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="Report_Alert.aspx.cs" Inherits="MRS.MRS.Report_Alert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <!-- **********************************************************************************************************************************************************
      MAIN CONTENT
      *********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>REPORT ALERT</h3>
            <div class="row">

                <form runat="server" id="Form1">
                    <div class="row mt">
                        <div class="col-md-12">
                            <div class="content-panel">
                                <h4 ><i class="fa fa-angle-right"></i><strong>Report SMS</strong></h4>

                                <%--<h4><i class="fa fa-angle-right"></i>Advanced Table</h4>--%>


                                <div class="content">
                                    <%-- <h5><i class="fa fa-search"></i>Pencarian :</h5>  --%>
                                    <div class="col-sm-5">
                                            Divisi
                                            <asp:DropDownList ID="ddl_UNIT" ToolTip="Pilih Divisi"  runat="server" class="form-control">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" style="margin-top: 19px;">
                                        <asp:Button ID="btncari" runat="server" class="btn btn-primary" Text="Search" OnClick="cari_Click" />

                                    </div>
                                    <div class="col-sm-5">
                                        
                                        <asp:LinkButton ID="lnkGenerateReport" runat="server" ToolTip="Download Report" CssClass="btn btn-default pull-right" OnClick="lnkGenerateReport_Click" align="right">  <i class="fa fa-file-excel-o" style="font-size:25px;color:#00796B"></i></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="content" style="margin-top: 80px">
                                    <div class="form-group">
                                        <asp:GridView ID="DataList_sms" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList_sms"
                                            CssClass="table table-striped table-advance table-hover" AllowPaging="true" OnPageIndexChanged="DataList_sms_PageIndexChanged"
                                            OnPageIndexChanging="DataList_sms_PageIndexChanging" PageSize="10" GridLines="None">

                                            <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                            <Columns>
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATELIMIT" HeaderText=" TANGGAL PELAPORAN" DataFormatString="{0:dd-MMM-yyyy}" />
                                               <%-- <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="TGL_SMS" HeaderText=" TANGGAL SMS" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                                                <%--<asp:BoundField HeaderStyle-CssClass="fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="NOSURAT" HeaderText=" No. Surat" />--%>
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-file" HeaderStyle-BackColor="Yellow" DataField="REPORTNAME" HeaderText=" NAMA LAPORAN" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-home" HeaderStyle-BackColor="Yellow" ItemStyle-CssClass="hidden-phone" DataField="INSTANSINAME" HeaderText=" REGULATOR" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-home" HeaderStyle-BackColor="Yellow" DataField="DIVISI" ItemStyle-CssClass="hidden-phone" HeaderText=" DIVISI" />
                                                <%--<asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATETIMELIMIT" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" Tangga Limit" />--%>

                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone	fa fa-child" HeaderStyle-BackColor="Yellow" ItemStyle-CssClass="hidden-phone" DataField="SU_FULLNAME" HeaderText=" USER" />
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-tag" HeaderStyle-BackColor="Yellow" DataField="NO_HPNUM" HeaderText=" NOMOR" />


                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-send-o" HeaderStyle-BackColor="Yellow" DataField="tgl_sms" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" TANGGAL SMS" />


                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                    <h1 id="lb_Sms" style="margin-left:10px" runat="server" visible="false">DATA NOT FOUND</h1>
                                </div>
                            </div>

                            <!-- BUTTON BLOCK -->

                            <!--/showback -->
                            <!-- /content-panel -->
                        </div>
                    </div>
                    <div class="row mt">
                        <div class="col-md-12">
                            <div class="content-panel">
                                <h4><i class="fa fa-angle-right"></i><strong>Report EMAIL</strong></h4>

                                <%--<h4><i class="fa fa-angle-right"></i>Advanced Table</h4>--%>
                                <div class="content">
                                    <%-- <h5><i class="fa fa-search"></i>Pencarian :</h5>  --%>
                                     <div class="col-sm-5">
                                            Divisi

                                            <asp:DropDownList ID="ddl_UNIT1" ToolTip="Pilih Divisi"  runat="server" class="form-control">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2"  style="margin-top: 19px;">
                                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Search" OnClick="search_Click" />

                                    </div>
                                    <div class="col-sm-5">
                                        
                                        <asp:LinkButton ID="lnkGenerateReportEmail" runat="server" ToolTip="Download Report" CssClass="btn btn-default pull-right" OnClick="lnkGenerateReportEmail_Click" align="right">  <i class="fa fa-file-excel-o" style="font-size:25px;color:#00796B"></i></asp:LinkButton>
                                    </div>
                                </div>
                                

                                <div class="content" style="margin-top: 80px">
                                    <div class="form-group">
                                        <asp:GridView ID="DataList_Email" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList_Email"
                                            CssClass="table table-striped table-advance table-hover" AllowPaging="true" OnPageIndexChanged="DataList_Email_PageIndexChanged"
                                            OnPageIndexChanging="DataList_Email_PageIndexChanging" PageSize="10" GridLines="None">

                                            <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                            <Columns>
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATELIMIT" HeaderText=" TANGGAL PELAPORAN" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-file" HeaderStyle-BackColor="Yellow" DataField="REPORTNAME"  HeaderText=" NAMA LAPORAN" />
                                                <%--<asp:BoundField HeaderStyle-CssClass="fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="NOSURAT" HeaderText=" No. Surat" />--%>
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-home" HeaderStyle-BackColor="Yellow" ItemStyle-CssClass="hidden-phone" DataField="INSTANSINAME" HeaderText=" REGULATOR" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-home" HeaderStyle-BackColor="Yellow" DataField="DIVISI" ItemStyle-CssClass="hidden-phone" HeaderText=" DIVISI" />
                                                <%--<asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-file" HeaderStyle-BackColor="Yellow" DataField="ISI_EMAIL" ItemStyle-CssClass="hidden-phone" HeaderText=" EMAIL ALERT" />--%>
                                                <%--<asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATETIMELIMIT" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" Tangga Limit" />--%>

                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone	fa fa-child" HeaderStyle-BackColor="Yellow" ItemStyle-CssClass="hidden-phone" DataField="SU_FULLNAME" HeaderText=" USER" />
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-tag" HeaderStyle-BackColor="Yellow" DataField="SU_EMAIL" HeaderText=" EMAIL" />


                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-send-o" HeaderStyle-BackColor="Yellow" DataField="tgl_email" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" TANGGAL SMS" />


                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <h1 id="Lb_Email" style="margin-left:10px" runat="server" visible="false">DATA NOT FOUND</h1>
                                </div>
                            </div>
                            <!-- BUTTON BLOCK -->

                            <!--/showback -->
                            <!-- /content-panel -->
                        </div>
                        <!-- /col-md-12 -->
                    </div>
                    <!-- /row -->

                </form>
            </div>
        </section>
        <! --/wrapper -->
    </section>
    <!-- /MAIN CONTENT -->

    <!--main content end-->

</asp:Content>
