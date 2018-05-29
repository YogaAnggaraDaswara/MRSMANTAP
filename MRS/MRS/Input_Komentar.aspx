<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Input_Komentar.aspx.cs" Inherits="MRS.MRS.Input_Komentar" %>


<%@ Register Assembly="CIDControls" Namespace="CIDControls" TagPrefix="cc1" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function formatAmountNoDecimals(number) {
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(number)) {
                number = number.replace(rgx, '$1' + '.' + '$2');
            }
            return number;
        }

        function formatAmount(number) {

            // remove all the characters except the numeric values
            number = number.replace(/[^0-9]/g, '');

            // set the default value
            if (number.length == 0) number = "0.00";
            else if (number.length == 1) number = "0.0" + number;
            else if (number.length == 2) number = "0." + number;
            else number = number.substring(0, number.length - 2) + '.' + number.substring(number.length - 2, number.length);

            // set the precision
            number = new Number(number);
            number = number.toFixed(2);    // only works with the "."

            // change the splitter to ","
            number = number.replace(/\./g, ',');

            // format the amount
            x = number.split(',');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';

            return formatAmountNoDecimals(x1) + x2;
        }


        $(function () {

            $('.amount').keyup(function () {
                $(this).val(formatAmount($(this).val()));
            });

        });

    </script>
    <script>
        $(document).ready(function () {
            var date_input = $('input[name="ctl00$MainContent$DATETIMELIMIT"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            date_input.datepicker({
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
            })
        })
        $(document).ready(function () {
            var date_input = $('input[name="ctl00$MainContent$TGLSURAT"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            date_input.datepicker({
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
            })
        })

        $(document).ready(function () {
            var date_input = $('input[name="ctl00$MainContent$TGLINPUT"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            date_input.datepicker({
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
            })
        })

    </script>

    <script type="text/javascript" src="assets/js/bootstrap-datetimepicker.min.js"></script>

    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="FORM DATA PELAPORAN"></asp:Label>
            </h3>

            <!-- BASIC FORM ELELEMNTS -->
            <div class="row mt">
                <div class="col-lg-12">
                    <div class="form-panel">
                        <%--<h4 class="mb"><i class="fa fa-angle-right"></i> Form Elements</h4>--%>
                        <form class="form-horizontal style-form" runat="server">
                            <div class="form-group" id="div_unit" runat="server" visible="false">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Divisi<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="UNIT" runat="server" class="form-control" AutoPostBack="True" required="required" OnSelectedIndexChanged="UNIT_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Regulator<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="INSTANSIID" runat="server" class="form-control" AutoPostBack="True" required="required" OnSelectedIndexChanged="INSTANSIID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Periode<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="PERIODE" runat="server" class="form-control" AutoPostBack="True" required="required" OnSelectedIndexChanged="PERIODE_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Nama Laporan<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="JENIS" runat="server" class="form-control" AutoPostBack="True" required="required" OnSelectedIndexChanged="JENIS_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Jenis Report<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="JENISREPORT" runat="server" class="form-control" required="required">
                                        <asp:ListItem Value=""> (none) </asp:ListItem>
                                        <asp:ListItem Value="OL"> Online</asp:ListItem>
                                        <asp:ListItem Value="OF"> Offline </asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Nomor Surat
                                     <%--   <span class="asteriskField">*</span>--%>
                                </label>
                                <div class="col-sm-10">
                                    <input type="text" id="NOSURAT" class="form-control" placeholder="Nomor Surat" runat="server">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Tanggal Surat
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" id="TGLSURAT" class="form-control" name="date" runat="server" maxlength="10" placeholder="DD/MM/YYYY" required="required">
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Perihal
                                </label>
                                <div class="col-sm-10">
                                    <input type="text" id="PERIHAL" class="form-control" placeholder=" " runat="server">
                                </div>
                            </div>
                            <%-- <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Tanggal Pelaporan
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    
                                    <input type="date" id="DATETIMELIMIT" class="form-control" runat="server" required="required" disabled>
                                </div>
                            </div>--%>

                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Tanggal Pelaporan
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" id="DATETIMELIMIT" class="form-control" name="date" runat="server" placeholder="DD/MM/YYYY" maxlength="10" required="required" disabled>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Keterangan
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    <textarea class="form-control" rows="5" runat="server" id="KETERANGAN" placeholder="Keterangan" required="required"></textarea>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Sanksi
                                        <span class="asteriskField">*</span></label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                        <span class="input-group-addon">Rp.</span>
                                        <input type="text" class="amount form-control" runat="server" id="Sanksi" required="required" maxlength="20" disabled />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    &nbsp
                                </label>
                                <div class="col-sm-10">
                                    <textarea class="form-control" rows="5" runat="server" id="SANKSI_KET" placeholder="Sanksi" required="required" disabled></textarea>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">File input <span class="asteriskField">*</span></label>
                                <div class="col-sm-10">
                                    <input type="file" class="form-control-file" runat="server" id="UPLOADFILE" aria-describedby="fileHelp">

                                    <small id="lb_file" runat="server">Server FIle Name :
                                        <asp:Label ID="fileupload" class="form-text text-muted" runat="server" Style="color: red"></asp:Label>
                                    </small>
                                    <br />
                                    <asp:Button runat="server" ID="uduh" Text="Download" class="btn btn-primary " OnClick="uduh_Click" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                     Tanggal Input
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" id="TGLINPUT" class="form-control" name="date" runat="server" maxlength="10"  placeholder="DD/MM/YYYY" required="required">
                                    </div>

                                </div>
                            </div>
                            <%--<div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Tanggal Input
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    <input type="date" id="TGLINPUT" class="form-control" runat="server" required="required">
                                </div>
                            </div>--%>

                            <div>
                                <asp:Button runat="server" ID="btnsave" Text="Simpan" class="btn btn-primary btn-lg btn-block" OnClick="save_Click" />
                            </div>
                        </form>
                    </div>
                </div>
                <!-- col-lg-12-->
            </div>
            <!-- /row -->


        </section>
        <! --/wrapper -->
    </section>
    <!-- /MAIN CONTENT -->


</asp:Content>

