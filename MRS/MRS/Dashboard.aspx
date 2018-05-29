<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MRS.DataEntry.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">

            <div class="row">
                <div class="col-lg-12 main-chart">

<%--                    <div class="row mtbox">
                        <div class="col-md-2 col-sm-2  box0">
                            <div class="box1">
                                <span>
                                    <img src="assets/img/ojk.png" />
                                </span>
                                <br />
                                <asp:Label ID="count_OJK" runat="server"></asp:Label>
                            </div>
                            <p>OTORITAS JASA KEUANGAN</p>
                        </div>
                        <div class="col-md-2 col-sm-2 box0">
                            <div class="box1">
                                <span>
                                    <img src="assets/img/BI.png" />

                                </span>
                                <br />
                                <asp:Label ID="count_BI" runat="server"></asp:Label>
                            </div>
                            <p>BANK INDONESIA</p>
                        </div>

                        <div class="col-md-2 col-sm-2 box0">
                            <div class="box1">
                                <span>
                                    <img src="assets/img/ojk_pasar_modal.png" />
                                </span>
                                <br />
                                <asp:Label ID="count_OJK_PASARMODAL" runat="server"></asp:Label>

                            </div>
                            <p>OTORITAS JASA KEUANGAN PASAR MODAL</p>
                        </div>

                        <div class="col-md-2 col-sm-2 box0">
                            <div class="box1">
                                <span>
                                    <img src="assets/img/PAJAK.png" />
                                </span>
                                <br />
                                <asp:Label ID="count_PAJAK" runat="server"></asp:Label>

                            </div>
                            <p>DIREKTORAT PAJAK</p>
                        </div>

                        <div class="col-md-2 col-sm-2 box0">
                            <div class="box1">
                                <span>
                                    <img src="assets/img/LOGO_PPATK.png" />
                                </span>
                                <br />
                                <asp:Label ID="count_PPATK" runat="server"></asp:Label>

                            </div>
                            <p>PUSAT PELAPORAN DAN ANALISIS TRANSAKSI KEUANGAN</p>
                        </div>
                        <div class="col-md-2 col-sm-2 box0">
                            <div class="box1">
                                <span>
                                    <img src="assets/img/instansi.jpg" />
                                </span>
                                <br />
                                <asp:Label ID="count_REGULATOR_LAIN" runat="server"></asp:Label>

                            </div>
                            <p>REGULATOR LAINNYA</p>
                        </div>
                    </div>--%>
                    <!-- /row mt -->


                    <div>
                        <div class="mb" runat="server" id="IN001" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=BI">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>BANK INDONESIA</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/BI.png"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_BI" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="BI_7" runat="server"></asp:Label>
                                                   
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari </b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="BI_3" runat="server"></asp:Label>
                                                 </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b>1 Hari </b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b>
                                                    <asp:Label ID="BI_1" runat="server"></asp:Label>
                                                 </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div>

                        <div class="mb" runat="server" id="IN002" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=OJK">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>OTORITAS JASA KEUANGAN</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/ojk.png"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_OJK" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="OJK_7" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari</b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="OJK_3" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 1 Hari</b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="OJK_1" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div>
                        
                        <div class="mb" runat="server" id="IN003" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=OJKPASARMODAL">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>OTORITAS JASA KEUANGAN PASAR MODAL</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/ojk_pasar_modal.png"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_OJK_PASARMODAL" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="OJKPASARMODAL_7" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari</b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="OJKPASARMODAL_3" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 1 Hari</b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="OJKPASARMODAL_1" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div>
                        
                        <div class="mb" runat="server" id="IN004" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=PAJAK">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>DIREKTORAT PAJAK</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/PAJAK.png"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_PAJAK" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="PAJAK_7" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari</b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="PAJAK_3" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 1 Hari</b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="PAJAK_1" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div>
                        <div class="mb" runat="server" id="IN005" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=PTATK">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>PUSAT PELAPORAN DAN ANALISIS TRANSAKSI KEUANGAN</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/LOGO_PPATK.png"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_PPATK" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="PPATK_7" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari</b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="PPATK_3" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 1 Hari</b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="PPATK_1" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div>
                        <div class="mb" runat="server" id="IN006" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=MANDIRI">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>BANK MANDIRI</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/MANDIRI.png"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_MANDIRI" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="MANDIRI_7" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari</b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="MANDIRI_3" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 1 Hari</b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="MANDIRI_1" runat="server"></asp:Label>
                                                </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div><div class="mb" runat="server" id="IN007" visible="false">
                            <!-- WHITE PANEL - TOP USER -->
                            <a href="Report_Dashboard.aspx?ID=REGULATOR">
                                <div class="white-panel">
                                    <div class="white-header">
                                        <h2>REGULATOR LAINNYA</h2>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-2">
                                            <img src="assets/img/instansi.jpg"  />
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <h4>TELAH DILAPORKAN</h4>
                                                <h2>
                                                    <asp:Label ID="count_REGULATOR_LAIN" runat="server"></asp:Label>
                                                </h2>
                                                <h4>LAPORAN</h4>
                                            </div>
                                            <br />
                                            <br />
<%--                                            <h3>DATA YANG HARUS DILAPORKAN </h3>--%>
                                            <div class="col-md-1">
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 7 Hari</b> </font> </p>
                                                <p style="background-color: green; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="REGULATOR_7" runat="server"></asp:Label>
                                             </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 3 Hari</b> </font> </p>
                                                <p style="background-color: yellow; color: black;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="REGULATOR_3" runat="server"></asp:Label>
                                               </b> </font> </p>
                                            </div>
                                            <div class="col-md-1">
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 1 Hari</b> </font> </p>
                                                <p style="background-color: red; color: white;"><font size="3" face="sans-serif"> <b> 
                                                    <asp:Label ID="REGULATOR_1" runat="server"></asp:Label>
                                             </b> </font> </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </a>
                        </div>
                        <%--<div class="col-md-4 mb">
                            <!-- WHITE PANEL - TOP USER -->
                            <div class="white-panel pn">
                                <div class="white-header">
                                    <h5>DATA DETAIL</h5>
                                </div>
                                <p>
                                    <img src="assets/img/BI.png" class="img-circle" width="80" height="80" />
                                </p>
                                <div class="row ">
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: green; color: white;">7 Hari</p>
                                        <p style="background-color: green; color: white;">
                                            <asp:Label ID="BI_7" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-5">
                                        <p class="small mt" style="background-color: yellow; color: black;">3 hari</p>
                                        <p style="background-color: yellow; color: black;">
                                            <asp:Label ID="BI_3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: red; color: white;">1 Hari</p>
                                        <p style="background-color: red; color: white;">
                                            <asp:Label ID="BI_1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                       <%-- <div class="col-md-4 mb">
                            <!-- WHITE PANEL - TOP USER -->
                            <div class="white-panel pn">
                                <div class="white-header">
                                    <h5>DATA DETAIL</h5>
                                </div>
                                <p>

                                    <img src="assets/img/ojk_pasar_modal.png" class="img" width="80" height="80" />
                                </p>
                                <div class="row ">
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: green; color: white;">7 Hari</p>
                                        <p style="background-color: green; color: white;">
                                            <asp:Label ID="OJKPASARMODAL_7" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-5">
                                        <p class="small mt" style="background-color: yellow; color: black;">3 hari</p>
                                        <p style="background-color: yellow; color: black;">
                                            <asp:Label ID="OJKPASARMODAL_3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: red; color: white;">1 Hari</p>
                                        <p style="background-color: red; color: white;">
                                            <asp:Label ID="OJKPASARMODAL_1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <%--<div class="col-md-4 mb">
                            <!-- WHITE PANEL - TOP USER -->
                            <div class="white-panel pn">
                                <div class="white-header">
                                    <h5>DATA DETAIL</h5>
                                </div>
                                <p>
                                    <img src="assets/img/PAJAK.png" class="img-circle" width="80" height="80" />
                                </p>
                                <div class="row ">
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: green; color: white;">7 Hari</p>
                                        <p style="background-color: green; color: white;">
                                            <asp:Label ID="PAJAK_7" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-5">
                                        <p class="small mt" style="background-color: yellow; color: black;">3 hari</p>
                                        <p style="background-color: yellow; color: black;">
                                            <asp:Label ID="PAJAK_3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: red; color: white;">1 Hari</p>
                                        <p style="background-color: red; color: white;">
                                            <asp:Label ID="PAJAK_1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <%--<div class="col-md-4 mb">
                            <!-- WHITE PANEL - TOP USER -->
                            <div class="white-panel pn">
                                <div class="white-header">
                                    <h5>DATA DETAIL</h5>
                                </div>
                                <p>
                                    <img src="assets/img/LOGO_PPATK.png" class="img-circle" width="80" height="80" />
                                </p>
                                <div class="row ">
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: green; color: white;">7 Hari</p>
                                        <p style="background-color: green; color: white;">
                                            <asp:Label ID="PPATK_7" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-5">
                                        <p class="small mt" style="background-color: yellow; color: black;">3 hari</p>
                                        <p style="background-color: yellow; color: black;">
                                            <asp:Label ID="PPATK_3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: red; color: white;">1 Hari</p>
                                        <p style="background-color: red; color: white;">
                                            <asp:Label ID="PPATK_1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                       <%-- <div class="col-md-4 mb">
                            <!-- WHITE PANEL - TOP USER -->
                            <div class="white-panel pn">
                                <div class="white-header">
                                    <h5>DATA DETAIL</h5>
                                </div>
                                <p>
                                    <img src="assets/img/instansi.jpg" class="img-circle" width="80" height="80">
                                </p>
                                <div class="row ">
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: green; color: white;">7 Hari</p>
                                        <p style="background-color: green; color: white;">
                                            <asp:Label ID="REGULATOR_7" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-5">
                                        <p class="small mt" style="background-color: yellow; color: black;">3 hari</p>
                                        <p style="background-color: yellow; color: black;">
                                            <asp:Label ID="REGULATOR_3" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col-md-3">
                                        <p class="small mt" style="background-color: red; color: white;">1 Hari</p>
                                        <p style="background-color: red; color: white;">
                                            <asp:Label ID="REGULATOR_1" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <!-- /col-md-4 -->
                    </div>
                    <!-- /row -->
                </div>
            <! --/row -->
        </section>
    </section>

    <!--main content end-->


</asp:Content>
