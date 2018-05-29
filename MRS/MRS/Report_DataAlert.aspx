<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="Report_DataAlert.aspx.cs" Inherits="MRS.MRS.Report_DataAlert" %>

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
        function Reset() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to reset password?")) {
                confirm_value.value = "Yes";
                document.forms[0].appendChild(confirm_value);
            }


        }
    </script>

    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>
                <asp:Label ID="lbl_judul" runat="server" Text="DATA ALERT"></asp:Label>
            </h3>

            <form runat="server" id="Form1">

                <!-- /form-panel -->


                <div class="content-panel">
                    <h4 class="mb"><i class="fa fa-angle-right"></i>Pencarian</h4>
                    <div class="form-group" >
                        <label class="col-sm-2 col-sm-2 control-label">
                            Divisi
                        </label>
                        <div class="col-sm-10" style="margin-bottom: 10px">
                            <asp:DropDownList ID="UNIT" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="UNIT_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" >
                        <label class="col-sm-2 col-sm-2 control-label">
                            Regulator
                        </label>
                        <div class="col-sm-10" style="margin-bottom: 10px">
                            <asp:DropDownList ID="INSTANSIID" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="INSTANSIID_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" >
                        <label class="col-sm-2 col-sm-2 control-label">
                            Nama Laporan
                        </label>
                        <div class="col-sm-10" style="margin-bottom: 10px">
                            <asp:DropDownList ID="JENIS" runat="server" class="form-control" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group"> 
                            <asp:Button ID="btncari" runat="server" OnClick="cari_Click" class="btn btn-primary btn-lg btn-block" Text="Search" /> 
                    </div>

                </div>
                <div class="content-panel">
                    <div class="content" >
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
                                <asp:BoundField HeaderStyle-CssClass="fa fa-home" HeaderStyle-BackColor="Yellow" DataField="INSTANSINAME" HeaderText=" REGULATOR" />
                                <asp:BoundField HeaderStyle-CssClass="fa fa-building-o" HeaderStyle-BackColor="Yellow" DataField="REPORTNAME" HeaderText=" NAMA LAPORAN" />
                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-home" HeaderStyle-BackColor="Yellow"  ItemStyle-CssClass="hidden-phone" DataField="DIVISI" HeaderText=" DIVISI" />
                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="PERIODENAME" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" PERIODE" />
                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="DATECREATED" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" DATE CREATED" />
                                <%--<asp:TemplateField HeaderStyle-CssClass="hidden-phone fa fa-clipboard" ItemStyle-CssClass="hidden-phone" HeaderStyle-BackColor="Yellow" HeaderText=" STATUS">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <span class="label label-primary label-mini" visible='<%# Convert.ToString(Eval("STATUS"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">AKTIF</span>
                                        <span class="label label-danger label-mini" visible='<%# Convert.ToString(Eval("STATUS"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">TIDAK AKTIF</span>

                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="" HeaderStyle-BackColor="Yellow">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnedit" runat="server" OnClick="edit_Click" class="btn btn-success" CommandName="edit" CommandArgument='<%# Convert.ToString(Eval("AP_REGNO"))+":"+ Convert.ToString(Eval("UNIT"))+":"+ Convert.ToString(Eval("JENIS"))%>' Text="Edit" />
                                        <asp:Button ID="btnhapus" runat="server" OnClick="OnConfirm" class="btn btn-danger" CommandName="hapus" CommandArgument='<%# Convert.ToString(Eval("AP_REGNO"))+":"+ Convert.ToString(Eval("UNIT"))+":"+ Convert.ToString(Eval("JENIS"))%>' Text="Delete" OnClientClick="Confirm()" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <h1 id="lb_User" style="margin-left: 10px" runat="server" visible="false">DATA NOT FOUND</h1>
                    </div>

                </div>
            </form>
            <!--/ row -->
        </section>
        <! --/wrapper -->
    </section>
    <!-- /MAIN CONTENT -->


</asp:Content>

