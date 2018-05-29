<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Report_Approval.aspx.cs" Inherits="MRS.MRS.Report_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to approve data?")) {
                confirm_value.value = "Yes";
                document.forms[0].appendChild(confirm_value);
            }


        }


    </script>
    <!-- **********************************************************************************************************************************************************
      MAIN CONTENT
      *********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>APPROVAL REPORT</h3>
            <div class="row">

                <form runat="server" id="Form1">
                    <div class="row mt">
                        <div class="col-md-12">

                            <div class="content-panel">
                                <div class="content">
                                    <h4 class="mb"><i class="fa fa-angle-right"></i>Pencarian</h4>
                                    <div class="col-sm-3" runat="server" id="div_unit">
                                        Divisi
                                        <asp:DropDownList ID="ddl_UNIT" ToolTip="Pilih Divisi" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 ">
                                        Regulator
                                        <asp:DropDownList ID="INSTANSIID" runat="server" class="form-control" ToolTip="Pilih Regulator">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 " style="margin-top: 19px; margin-bottom: 20px">
                                        <asp:Button ID="btncari" runat="server" class="btn btn-primary" ToolTip="Cari Data" Text="Search" OnClick="cari_Click" />

                                    </div>

                                </div>



                                <div class="content" style="margin-top: 80px">
                                    <%--<h4><i class="fa fa-angle-right"></i>Advanced Table</h4>--%>
                                    <div class="form-group">
                                        <asp:GridView ID="DataList" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList"
                                            CssClass="table table-striped table-advance table-hover" AllowPaging="true" PageSize="10"
                                            OnRowCommand="DataList_RowCommand"
                                            OnRowDataBound="DataList_RowDataBound"
                                            OnPageIndexChanged="DataList_PageIndexChanged" KeyFieldName="AP_REGINPUT"
                                            OnPageIndexChanging="DataList_PageIndexChanging" GridLines="None">

                                            <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="NO" HeaderStyle-BackColor="Yellow">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="TGLSURAT" HeaderText=" TANGGAL SURAT" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="NOSURAT" ItemStyle-CssClass="hidden-phone" HeaderText=" NO. SURAT" />
                                                <asp:BoundField HeaderStyle-CssClass="fa fa-home" HeaderStyle-BackColor="Yellow" DataField="INSTANSINAME" HeaderText=" REGULATOR" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-file" HeaderStyle-BackColor="Yellow" DataField="PERIHAL" ItemStyle-CssClass="hidden-phone" HeaderText=" PERIHAL" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATETIMELIMIT" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" TANGGAL PELAPORAN" />
                                                <asp:TemplateField HeaderText=" STATUS" HeaderStyle-BackColor="Yellow" HeaderStyle-CssClass="fa fa-clipboard">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <span class="label label-primary label-mini" visible='<%# Convert.ToString(Eval("hari"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">none</span>
                                                        <span class="label label-default label-mini" visible='<%# Convert.ToString(Eval("hari"))=="7"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">7 Hari</span>
                                                        <span class="label label-warning label-mini" visible='<%# Convert.ToString(Eval("hari"))=="3"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">3 Hari</span>
                                                        <span class="label label-danger label-mini" visible='<%# Convert.ToString(Eval("hari"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">1 Hari</span>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-BackColor="Yellow">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                            <%-- <asp:Button ID="" runat="server" type="button" class="btn btn-primary btn-xs" Text="Approve" CommandName="Approve" CommandArgument='<%# Convert.ToString(Eval("NOSURAT")) %>' />--%>
                                                            <asp:Button ID="btn_Approve" runat="server" OnClick="OnConfirm" class="btn btn-danger btn-xs" CommandName="Approve" CommandArgument='<%# Convert.ToString(Eval("AP_REGINPUT"))%>' Text="Belum Approve" OnClientClick="Confirm()" />

                                                        </span>

                                                        <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                            <asp:Button ID="Button1" runat="server" type="button" disabled="disabled" value="approve" class="btn btn-primary btn-xs" Text="Sudah Approve" />
                                                        </span>

                                                        <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                            <asp:Button ID="btn_edit" runat="server" type="button" value="Edit" class="btn btn-success btn-xs" Text="Edit" CommandName="Edit" CommandArgument='<%# Convert.ToString(Eval("AP_REGINPUT")) %>' />

                                                        </span>

                                                        <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                            <asp:Button ID="Button2" runat="server" type="button" value="Edit" disabled="disabled" class="btn btn-success btn-xs" Text="Edit" CommandName="Edit" CommandArgument='<%# Convert.ToString(Eval("AP_REGINPUT")) %>' />

                                                        </span>

                                                        <span runat="server">
                                                            <asp:Button ID="btn_view" runat="server" type="button" value="View" class="btn btn-success btn-xs" Text="View" CommandName="View" CommandArgument='<%# Convert.ToString(Eval("AP_REGINPUT")) %>' />
                                                        </span>



                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                    </div>
                                    <h1 id="lb_data" style="margin-left: 10px" runat="server" visible="false">DATA NOT FOUND</h1>
                                </div>
                            </div>

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
