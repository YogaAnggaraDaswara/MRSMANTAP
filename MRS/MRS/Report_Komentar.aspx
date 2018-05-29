<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Report_Komentar.aspx.cs" Inherits="MRS.MRS.Report_Komentar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- **********************************************************************************************************************************************************
      MAIN CONTENT
      *********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>REPORT STATUS APPROVAL</h3>
            <div class="row">

                <form runat="server" id="Form1">
                    <div class="row mt">
                        <div class="col-md-12">

                            <div class="content-panel">
                                <div class="content">
                                    <div class="col-sm-2">
                                        <div>
                                            Divisi
                                            <asp:DropDownList ID="ddl_UNIT" ToolTip="Pilih Divisi" runat="server" class="form-control" AutoPostBack="True"  OnSelectedIndexChanged="UNIT_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div>
                                            Dept. Head
                                            <asp:DropDownList ID="DEPTHEAD" runat="server" class="form-control" ToolTip="Pilih Dept. Head" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div>
                                            Pic
                                            <asp:DropDownList ID="PIC" runat="server" class="form-control" ToolTip="Pilih PIC" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1" style="margin-top: 19px;">
                                        <asp:Button ID="btncari" runat="server" class="btn btn-primary" ToolTip="Cari Data" Text="Search" OnClick="cari_Click" />

                                    </div>
                                    <div class="col-sm-5">
                                        
                                        <asp:LinkButton ID="lnkGenerateReport" runat="server" ToolTip="Download Report" CssClass="btn btn-default pull-right" OnClick="lnkGenerateReport_Click" align="right">  <i class="fa fa-file-excel-o" style="font-size:25px;color:#00796B"></i></asp:LinkButton>
                                    </div>
                                </div>


                                <div class="content" style="margin-top: 80px">

                                    <%--<h4><i class="fa fa-angle-right"></i>Advanced Table</h4>--%>

                                    <asp:GridView ID="DataList" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList"
                                        CssClass="table table-striped table-advance table-hover" AllowPaging="true" PageSize="10"
                                        OnRowCommand="DataList_RowCommand"
                                        OnRowDataBound="DataList_RowDataBound"
                                        OnPageIndexChanged="DataList_PageIndexChanged" KeyFieldName="NOSURAT"
                                        OnPageIndexChanging="DataList_PageIndexChanging" GridLines="None">

                                        <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No" HeaderStyle-BackColor="Yellow">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="TGLSURAT" HeaderText=" TANGGAL SURAT" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField HeaderStyle-CssClass="fa fa-file" HeaderStyle-BackColor="Yellow" DataField="REPORTNAME"  HeaderText=" NAMA LAPORAN" />
                                            <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="NOSURAT" ItemStyle-CssClass="hidden-phone" HeaderText=" NO. SURAT" />
                                            <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-home" HeaderStyle-BackColor="Yellow" ItemStyle-CssClass="hidden-phone" DataField="INSTANSINAME" HeaderText=" REGULATOR" />
                                            <%--<asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-comments" HeaderStyle-BackColor="Yellow" DataField="KOMENTAR" ItemStyle-CssClass="hidden-phone" HeaderText=" Komentar" />--%>
                                            <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATETIMELIMIT" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" TANGGAL PELAPORAN" />
                                            <asp:TemplateField HeaderText=" STATUS" HeaderStyle-BackColor="Yellow" HeaderStyle-CssClass="hidden-phone fa fa-clipboard" ItemStyle-CssClass="hidden-phone">
                                                <ItemStyle />
                                                <ItemTemplate>
                                                    <span class="label label-primary label-mini" visible='<%# Convert.ToString(Eval("hari"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">none</span>
                                                    <span class="label label-default label-mini" visible='<%# Convert.ToString(Eval("hari"))=="7"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">7 Hari</span>
                                                    <span class="label label-warning label-mini" visible='<%# Convert.ToString(Eval("hari"))=="3"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">3 Hari</span>
                                                    <span class="label label-danger label-mini" visible='<%# Convert.ToString(Eval("hari"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">1 Hari</span>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" STATUS APPROVE" HeaderStyle-BackColor="Yellow" HeaderStyle-CssClass="fa fa-clipboard">
                                                <ItemStyle />
                                                <ItemTemplate>
                                                    <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                        <asp:Button ID="btn_Approve" runat="server" type="button" disabled="disabled" class="btn btn-danger btn-xs" Text="Belum Approve" CommandName="Belum Approve" CommandArgument='<%# Convert.ToString(Eval("AP_REGINPUT")) %>' />
                                                    </span>

                                                    <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                        <asp:Button ID="Button1" runat="server" type="button" disabled="disabled" value="approve" class="btn btn-primary btn-xs" Text="Sudah Approve" />
                                                    </span>



                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-BackColor="Yellow">
                                                <ItemStyle />
                                                <ItemTemplate>

                                                    <span runat="server">
                                                        <asp:Button ID="btn_view" runat="server" type="button" value="View" class="btn btn-success btn-xs" Text="View" ToolTip="View Data" CommandName="View" CommandArgument='<%# Convert.ToString(Eval("AP_REGINPUT")) %>' />
                                                    </span>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                    <h1 id="lb_data" style="margin-left: 10px" runat="server" visible="false">DATA NOT FOUND</h1>
                                </div>

                                <!-- /content-panel -->

                            </div>

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
