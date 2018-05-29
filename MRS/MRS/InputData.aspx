<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="InputData.aspx.cs" Inherits="MRS._Default" %>


<%@ Register Assembly="CIDControls" Namespace="CIDControls" TagPrefix="cc1" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        //window.onbeforeunload = confirmExit;
        //function confirmExit() {
        //    return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
        //}

        ////window.onbeforeunload = function () {
        ////    var Ans = confirm("Are you sure you want change page!");
        ////    if (Ans == true)
        ////        return true;
        ////    else
        ////        return false;
        ////};
    </script>
     <script type="text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to delete data?")) {
                 confirm_value.value = "Yes";
                 document.forms[0].appendChild(confirm_value);
             }


         }
    </script>

   <%-- <script type="text/javascript">
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

    </script>--%>

    <script type="text/javascript">
        $(function () {
            $("[type='currency']").keydown(function (event) {
                var position = this.selectionStart;
                var $this = $(this);
                var val = $this.val();
                if (position == this.selectionEnd &&
                    ((event.keyCode == 8 && val.charAt(position - 1) == "," && val.substr(0, position - 1).indexOf(".") == -1)
                        || (event.keyCode == 46 && val.charAt(position) == "," && val.substr(0, position).indexOf(".") == -1))) {
                    event.preventDefault();
                    if (event.keyCode == 8) {
                        $this.val(val.substr(0, position - 2) + val.substr(position));
                        position = position - 2;
                    } else {
                        $this.val(val.substr(0, position) + val.substr(position + 2));
                    }
                    $this.trigger('keyup', { position: position });
                } else {
                    this.dispatchEvent(event);
                }
            });

            $("[type='currency']").keyup(function (event, args) {
                if (event.which >= 37 && event.which <= 40) {
                    event.preventDefault();
                }

                var position = args ? args.position : this.selectionStart;
                var $this = $(this);
                var val = $this.val();
                var parts = val.split(".");
                var valOverDecimalPart = parts[0];
                var commaCountBefore = valOverDecimalPart.match(/,/g) ? valOverDecimalPart.match(/,/g).length : 0;
                var num = valOverDecimalPart.replace(/[^0-9]/g, '');
                var result = parts.length == 1 ? num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") : num.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + "." + parts[1].replace(/[^0-9.]/g, "");
                $this.val(result);
                var commaCountAfter = $this.val().match(/,/g) ? $this.val().match(/,/g).length : 0;
                position = position + (commaCountAfter - commaCountBefore);
                this.setSelectionRange(position, position);
            });
        });
    </script>
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="FORM DATA ENTRY ALERT"></asp:Label>
            </h3>

            <!-- BASIC FORM ELELEMNTS -->
            <div class="row mt">
                <div class="col-lg-12">
                    <div class="form-panel">
                        <%--<h4 class="mb"><i class="fa fa-angle-right"></i> Form Elements</h4>--%>
                        <form class="form-horizontal style-form" runat="server">


                            <div class="form-group">
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
                                    <%--<asp:DropDownList ID="INSTANSIID" runat="server" class="form-control" AutoPostBack="True" required="required" OnSelectedIndexChanged="INSTANSIID_SelectedIndexChanged">
                                    </asp:DropDownList>--%>

                                    <asp:CheckBoxList ID="INSTANSIID" runat="server" AutoPostBack="True" required="required" RepeatDirection="Horizontal" CellPadding="10"
                                        CellSpacing="10"
                                        RepeatColumns="2" RepeatLayout="Table" Width="100%" OnSelectedIndexChanged="INSTANSIID_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Nama Laporan<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="JENIS" runat="server" class="form-control" required="required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Approval<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="DEPTHEAD" runat="server" class="form-control" required="required" AutoPostBack="True" OnSelectedIndexChanged="DEPTHEAD_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    PIC<span class="asteriskField">
                                         *
                                    </span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="PIC" runat="server" class="form-control" required="required">
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
                                        <%--<asp:ListItem Value=""> (none) </asp:ListItem>
                                        <asp:ListItem Value="HARI"> Harian </asp:ListItem>
                                        <asp:ListItem Value="MINGGU"> Mingguan </asp:ListItem>
                                        <asp:ListItem Value="BULAN"> Bulanan </asp:ListItem>
                                        <asp:ListItem Value="TRIWULAN"> Triwulan </asp:ListItem>
                                        <asp:ListItem Value="SEMESTER"> Semesteran </asp:ListItem>
                                        <asp:ListItem Value="TAHUN"> Tahunan </asp:ListItem>
                                        <asp:ListItem Value="INSEDENTIL"> Insedentil </asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>



                            <div style="display: none;" id="div_BulanPelaporan" runat="server" class="form-group">
                                <div class="col-sm-2 col-sm-2 control-label">
                                    &nbsp
                                </div>
                                <div class="col-sm-6">
                                    <table>
                                        <tr>
                                            <th class="col-sm-2">Bulan Pelaporan</th>
                                            <th class="col-sm-2">Tanggal Pelaporan</th>
                                            <th class="col-sm-2"></th>
                                        </tr>
                                        <tr>
                                            <td class="col-sm-2">
                                                <asp:DropDownList ID="BULAN_PERIODE" runat="server" class="form-control" >
                                                   
                                                </asp:DropDownList></td>
                                            <td class="col-sm-2">
                                                <asp:DropDownList ID="TANGGAL_PERIODE" runat="server" class="form-control" >
                                                    
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button runat="server" ID="add" Text="Add" UseSubmitBehavior="false" class="btn btn-primary" OnClick="add_Click" />

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                            <div style="display: none;" id="div_ListPelaporan" runat="server" class="form-group">
                                <div class="col-sm-2 col-sm-2 control-label">
                                    &nbsp
                                </div>
                                <div class="col-sm-6">
                                    <asp:GridView ID="DataList" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList"
                                        CssClass="table table-striped table-advance table-hover" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanged="DataList_PageIndexChanged"
                                        OnPageIndexChanging="DataList_PageIndexChanging" GridLines="None">

                                        <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="No" HeaderStyle-BackColor="Yellow">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderStyle-CssClass="fa fa-home" HeaderStyle-BackColor="Yellow" DataField="BULANNAME" HeaderText=" BULAN" />
                                            <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="TANGGAL" HeaderText=" TANGGAL" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-BackColor="Yellow">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="btnedit" runat="server" OnClick="edit_Click" class="btn btn-success" CommandName="edit" UseSubmitBehavior="false"  CommandArgument='<%# Convert.ToString(Eval("BULAN"))+":"+ Convert.ToString(Eval("TANGGAL"))%>' Text="Edit" />--%>
                                                    <asp:Button ID="btnhapus" runat="server" OnClick="OnConfirm" class="btn btn-danger" CommandName="hapus" UseSubmitBehavior="false"  CommandArgument='<%# Convert.ToString(Eval("BULAN"))+":"+ Convert.ToString(Eval("TANGGAL"))+":"+ Convert.ToString(UNIT.SelectedValue.ToString())+":"+ Convert.ToString(PERIODE.SelectedValue.ToString())%>' Text="Delete" OnClientClick="Confirm()" />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div style="display: none;" id="div_tanggalPelaporan" runat="server" class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    Tanggal Pelaporan
                                        <span class="asteriskField">*</span>
                                </label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="DATETIMELIMIT" runat="server" class="form-control" >
                                       
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
                                        <input type='currency' class="form-control" runat="server" id="Sanksi" required="required" maxlength="20" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">
                                    &nbsp
                                </label>
                                <div class="col-sm-10">
                                    <textarea class="form-control" rows="5" runat="server" id="Sanksi_ket" placeholder="Sanksi" required="required"></textarea>
                                </div>
                            </div>

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

