<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputData_Regulator.aspx.cs" MasterPageFile="~/Shared/Site.Master" Inherits="MRS.MRS.InputData_Regulator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="Form Data Regulator"></asp:Label>
            </h3>

            <div class="col-lg-12 col-md-12 col-sm-12">
                <! -- MODALS -->
      				<div class="showback">
                          <form class="form-horizontal style-form" runat="server">

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      ID
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="INSTANSIID" class="form-control" placeholder="Id" disabled="disabled" runat="server" required="required">
                                  </div>
                              </div>

                              <div class="form-group">
                                  <label class="col-sm-2 col-sm-2 control-label">
                                      Nama Regulator
                                        <span class="asteriskField">*</span>
                                  </label>
                                  <div class="col-sm-10">
                                      <input type="text" id="INSTANSINAME" class="form-control" placeholder="Nama Regulator" runat="server" required="required">
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
