<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="InputData_User.aspx.cs" Inherits="MRS.MRS.Data_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="FORM DATA USER"></asp:Label>
            </h3>

            <div class="col-lg-12 col-md-12 col-sm-12">
                <! -- MODALS -->
      				<div class="showback">
                          <form class="form-horizontal style-form" runat="server">

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Nomor Induk Karyawan
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="NIK" class="form-control" placeholder="Nomor Induk Karyawan" runat="server" required="required">
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Nama
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="FULLNAME" class="form-control" placeholder="Nama Lengkap" runat="server" required="required">
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Divisi<span class="asteriskField">
                                         *
                                      </span>
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:DropDownList ID="UNIT" runat="server" class="form-control" required="required">
                                      </asp:DropDownList>
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      No. HP 
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="NOUSER" runat="server" class="input-medium bfh-phone" data-format="+62 ddd-dddd-dddd" placeholder="xxxx-xxxx-xxxx" maxlength="17" required="required">
                                  </div>
                              </div>
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Email
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="email" class="form-control" runat="server" id="email" placeholder="Enter email" name="email" required="required">
                                  </div>
                              </div>
                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Email lainnya
                                       
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="email" class="form-control" runat="server" id="email2" placeholder="Enter email" name="email" >
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      ROLE MENU<span class="asteriskField">
                                         *
                                      </span>
                                  </label>
                                  <div class="col-sm-10">
                                      <asp:DropDownList ID="GROUP" runat="server" class="form-control" required="required" AutoPostBack="True" OnSelectedIndexChanged="GROUP_SelectedIndexChanged">
                                      </asp:DropDownList>
                                  </div>
                              </div>

                              <div id="div_penerima" runat="server" visible="false">
                                  <div class="form-group">
                                      <label class="col-sm-2 col-sm-2 control-label">
                                          PIC Delegated
                                      </label>
                                      <div class="col-sm-10">
                                          <asp:DropDownList ID="PIC2" runat="server" class="form-control">
                                          </asp:DropDownList>
                                      </div>
                                  </div>
                              </div>
                              <div id="div_role" runat="server" visible="false">
                                  <%-- <div class="form-group">
                                      <label class="col-sm-2 col-sm-2 control-label">
                                          No. HP Kepala Divisi
                                        <span class="asteriskField">*</span>
                                      </label>
                                      <div class="col-sm-10">
                                          <input type="text" id="NOKADIV" runat="server" class="input-medium bfh-phone" data-format="+62 ddd-dddd-dddd" placeholder="xxxx-xxxx-xxxx" maxlength="17" required="required">
                                      </div>
                                  </div>
                                  <div class="form-group">
                                      <label class="col-sm-2 col-sm-2 control-label">
                                          Email Kepala Divisi
                                        <span class="asteriskField">*</span>
                                      </label>
                                      <div class="col-sm-10">
                                          <input type="email" class="form-control" runat="server" id="emailDivisi" placeholder="Enter email" name="email" required="required">
                                      </div>
                                  </div>--%>
                                  <div class="form-group">
                                      <asp:Label ID="lb_judul" runat="server" Text="Kepala Divisi" class="col-sm-2 col-sm-2 control-label"></asp:Label>
                                      <div class="col-sm-10">
                                          <asp:DropDownList ID="SU_UPLINER" runat="server" class="form-control" >
                                          </asp:DropDownList>
                                      </div>
                                  </div>
                              </div>
                              <div id="div_kadiv" runat="server" visible="false">

                                  <div class="form-group">
                                      <label class="col-sm-2 col-sm-2 control-label">
                                          No. HP Direktur
                                        <span class="asteriskField">*</span>
                                      </label>
                                      <div class="col-sm-10">
                                          <input type="text" id="NODIREKTUR" runat="server" class="input-medium bfh-phone" data-format="+62 ddd-dddd-dddd" placeholder="xxxx-xxxx-xxxx" maxlength="17" required="required">
                                      </div>
                                  </div>
                                  <div class="form-group">
                                      <label class="col-sm-2 col-sm-2 control-label">
                                          Email Direktur
                                        <span class="asteriskField">*</span>
                                      </label>
                                      <div class="col-sm-10">
                                          <input type="email" class="form-control" runat="server" id="emailDirektur" placeholder="Enter email" name="email" required="required">
                                      </div>
                                  </div>
                              </div>
                               <div id="div_login" runat="server" visible="false">

                                  <div class="form-group">
                                      <label class="col-sm-2 col-sm-2 control-label">
                                          Status Login
                                      </label>
                                      <div class="col-sm-10">
                                          <asp:CheckBox ID="cb_login" runat="server" Text=" Tidak Login" />
                                      </div>
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
