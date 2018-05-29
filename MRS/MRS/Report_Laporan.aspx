<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="Report_Laporan.aspx.cs" Inherits="MRS.MRS.Report_Laporan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="DATA LAPORAN"></asp:Label>
            </h3>

            <form runat="server" id="Form1">

                <!-- /form-panel -->


                <div class="content-panel">

                    <h4 class="mb"><i class="fa fa-angle-right"></i>Pencarian</h4>

                    <div class="content">

                        <div class="col-sm-2">
                            Nama Laporan
                                <input type="text" id="REPORTNAME" class="form-control" tooltip="Input Nama Laporan" placeholder="Nama Laporan" runat="server">
                        </div>


                        <div class="col-sm-3">
                            Regulator
                                <asp:DropDownList ID="INSTANSIID" runat="server" ToolTip="Pilih Regulator" class="form-control">
                                </asp:DropDownList>
                        </div>


                        <div class="col-sm-3">
                            Divisi
                                <asp:DropDownList ID="ddl_UNIT" ToolTip="Pilih Divisi" runat="server" class="form-control">
                                </asp:DropDownList>

                        </div>

                        <div class="col-sm-2" style="margin-top: 19px;">
                            <asp:Button ID="btncari" runat="server" OnClick="cari_Click" class="btn btn-primary" Text="Search" />

                        </div>

                    </div>
                    <div class="content" style="margin-top: 100px">
                        <asp:GridView ID="DataList" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList"
                            CssClass="table table-striped table-advance table-hover" AllowPaging="true" PageSize="10"
                            OnPageIndexChanged="DataList_PageIndexChanged" KeyFieldName="REPORTID"
                            OnPageIndexChanging="DataList_PageIndexChanging" GridLines="None">

                            <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                            <Columns>

                                <asp:TemplateField HeaderText="No" HeaderStyle-BackColor="Yellow">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="REPORTID" HeaderText=" Id Report" />
                                <asp:BoundField HeaderStyle-CssClass="fa fa-home" HeaderStyle-BackColor="Yellow" DataField="REPORTNAME" HeaderText=" Nama Laporan" />
                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-building-o" HeaderStyle-BackColor="Yellow" DataField="DIVISI" ItemStyle-CssClass="hidden-phone" HeaderText=" Divisi" />
                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-building-o" HeaderStyle-BackColor="Yellow" DataField="INSTANSINAME" ItemStyle-CssClass="hidden-phone" HeaderText=" Regulator" />
                                <asp:TemplateField HeaderStyle-CssClass="hidden-phone fa fa-clipboard" ItemStyle-CssClass="hidden-phone" HeaderStyle-BackColor="Yellow" HeaderText=" Status">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <span class="label label-primary label-mini" visible='<%# Convert.ToString(Eval("ACTIVE"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">AKTIF</span>
                                        <span class="label label-danger label-mini" visible='<%# Convert.ToString(Eval("ACTIVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">TIDAK AKTIF</span>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" HeaderStyle-BackColor="Yellow">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnedit" runat="server" OnClick="edit_Click" class="btn btn-success" CommandName="edit" CommandArgument='<%# Convert.ToString(Eval("REPORTID")) %>' Text="Edit" />
                                        <asp:Button ID="btnhapus" runat="server" OnClick="OnConfirm" class="btn btn-danger" CommandName="hapus" CommandArgument='<%# Convert.ToString(Eval("REPORTID")) %>' Text="Delete" OnClientClick="Confirm()" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <h1 id="lb_User" style="margin-left: 10px" runat="server" visible="false">DATA NOT FOUND</h1>
                        <%-- <div class="container">
                    <asp:Button ID="btn_view" runat="server" type="button" value="View" class="btn btn-primary btn-lg btn-block" Text="KIRIM" OnClick="kirim_Click" />

                </div>--%>
                    </div>
                </div>
            </form>
            <!--/ row -->
        </section>
        <! --/wrapper -->
    </section>
    <!-- /MAIN CONTENT -->


</asp:Content>

