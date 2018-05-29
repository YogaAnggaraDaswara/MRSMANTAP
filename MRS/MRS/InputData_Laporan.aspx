<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="InputData_Laporan.aspx.cs" Inherits="MRS.MRS.InputData_Laporan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="FORM DATA PELAPORAN"></asp:Label>
            </h3>

            <div class="col-lg-12 col-md-12 col-sm-12">
                <! -- MODALS -->
      				<div class="showback">
                          <form class="form-horizontal style-form" runat="server">
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Divisi<span class="asteriskField">
                                         *
                                      </span>
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:DropDownList ID="UNIT" runat="server" class="form-control" required="required" AutoPostBack="True"  OnSelectedIndexChanged="UNIT_SelectedIndexChanged">
                                      </asp:DropDownList>
                                  </div>
                              </div>
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Regulator
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">

                                      <asp:DropDownList ID="INSTANSIID" runat="server" class="form-control" required="required" >
                                      </asp:DropDownList>
                                  </div>
                              </div>
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Id Report
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="REPORTID" class="form-control" placeholder="(GENERATE)" disabled="disabled" runat="server" required="required">
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Nama Laporan
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="REPORTNAME" class="form-control" placeholder="Nama Lengkap" runat="server" required="required">
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Periode
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:DropDownList ID="PERIODE" runat="server" class="form-control" >
                                          <asp:ListItem Value=""> (none) </asp:ListItem>
                                          <asp:ListItem Value="HARI"> Harian </asp:ListItem>
                                          <asp:ListItem Value="MINGGU"> Mingguan </asp:ListItem>
                                          <asp:ListItem Value="BULAN"> Bulanan </asp:ListItem>
                                          <asp:ListItem Value="TRIWULAN"> Triwulan </asp:ListItem>
                                          <asp:ListItem Value="SEMESTER"> Semesteran </asp:ListItem>
                                          <asp:ListItem Value="TAHUN"> Tahunan </asp:ListItem>
                                          <asp:ListItem Value="INSEDENTIL"> Insedentil </asp:ListItem>
                                      </asp:DropDownList>
                                  </div>
                              </div>


                              <div>
                                  <asp:Button runat="server" ID="btnsave" Text="Simpan" class="btn btn-primary btn-lg btn-block" OnClick="save_Click" />
                              </div>
                          </form>


                      </div>
                <!-- /showback -->

            </div>
            <! --/col-lg-6 -->
      			
      			 
            <!--/ row -->
        </section>
        <! --/wrapper -->
    </section>
    <!-- /MAIN CONTENT -->


</asp:Content>
