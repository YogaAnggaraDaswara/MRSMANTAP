<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Report_SMS.aspx.cs" Inherits="MRS.MRS.Report_SMS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                //row.style.backgroundColor = "aqua";
                objRef.checked = true;
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    //  row.style.backgroundColor = "#C2D69B";
                }
                else {
                    // row.style.backgroundColor = "white";
                }

                objRef.checked = false;
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                // var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        ;
                        break;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
            //headerCheckBox.checked = checked;
        }
    </script>

    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        // row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            //  row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            // row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <!-- **********************************************************************************************************************************************************
      MAIN CONTENT
      *********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <h3><i class="fa fa-angle-right"></i>Report SMS DAN EMAIL</h3>
            <div class="row">

                <form runat="server" id="Form1">
                    <div class="row mt">
                        <div class="col-md-12">
                            <div class="content-panel">

                                <%--<h4><i class="fa fa-angle-right"></i>Advanced Table</h4>--%>

                                <asp:GridView ID="DataList" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList"
                                    CssClass="table table-striped table-advance table-hover" AllowPaging="true" PageSize="10"
                                    OnRowCommand="DataList_RowCommand"
                                    OnRowDataBound="DataList_RowDataBound"
                                    OnPageIndexChanged="DataList_PageIndexChanged" KeyFieldName="NOSURAT"
                                    OnPageIndexChanging="DataList_PageIndexChanging" GridLines="None">

                                    <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-BackColor="Yellow" >
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <span visible='<%# Convert.ToString(Eval("STATUS"))=="BELUM TERKIRIM" || Convert.ToString(Eval("STATUS"))=="GAGAL"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                    <asp:CheckBox ID="chkCheck" runat="server" onclick="Check_Click(this)"  />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No" HeaderStyle-BackColor="Yellow"  >
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="TGLSURAT" HeaderText=" Tanggal Surat" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="NOSURAT" ItemStyle-CssClass="hidden-phone" HeaderText=" No. Surat" />
                                        <asp:BoundField HeaderStyle-CssClass="fa fa-home" HeaderStyle-BackColor="Yellow" DataField="INSTANSINAME" HeaderText=" Regulator" />
                                        <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-file" HeaderStyle-BackColor="Yellow" DataField="PERIHAL" ItemStyle-CssClass="hidden-phone" HeaderText=" Perihal" />
                                        <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATETIMELIMIT" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" Tgl Pelaporan" />
                                        <asp:TemplateField HeaderStyle-CssClass="hidden-phone fa fa-clipboard" HeaderStyle-BackColor="Yellow" HeaderText=" Status" ItemStyle-CssClass="hidden-phone"  >
                                            <ItemStyle  />
                                            <ItemTemplate>
                                                <span class="label label-primary label-mini" visible='<%# Convert.ToString(Eval("hari"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">none</span>
                                                <span class="label label-default label-mini" visible='<%# Convert.ToString(Eval("hari"))=="7"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">7 Hari</span>
                                                <span class="label label-warning label-mini" visible='<%# Convert.ToString(Eval("hari"))=="3"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">3 Hari</span>
                                                <span class="label label-danger label-mini" visible='<%# Convert.ToString(Eval("hari"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">1 Hari</span>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Status SMS dan Email" HeaderStyle-BackColor="Yellow" HeaderStyle-CssClass="fa fa-clipboard">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <span class="label btn-warning label-mini" visible='<%# Convert.ToString(Eval("STATUS"))=="BELUM TERKIRIM"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">BELUM TERKIRIM</span>
                                                <span class="label label-default label-mini" visible='<%# Convert.ToString(Eval("STATUS"))=="TERKIRIM"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">TERKIRIM</span>
                                                <span class="label label-danger label-mini" visible='<%# Convert.ToString(Eval("STATUS"))=="GAGAL"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">GAGAL</span>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <div class="container"> 
                                    <asp:Button ID="btn_view" runat="server" type="button" value="View" class="btn btn-primary btn-lg btn-block" Text="KIRIM"  OnClick="kirim_Click"  />
                                               
                                </div>
                            </div>
                            <!-- BUTTON BLOCK -->

                            <!--/showback -->
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
