<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ReportMRS.aspx.cs" Inherits="MRS.Report.ReportMRS" %>


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
            <h3><i class="fa fa-angle-right"></i>DATA PELAPORAN</h3>
            <div class="row">

                <form runat="server" id="Form1">
                    <div class="row mt">
                        <div class="col-md-12">
                            <div class="content-panel">
                                <h4 class="mb"><i class="fa fa-angle-right"></i>Pencarian</h4>
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-2 control-label">
                                        Regulator
                                    </label>
                                    <div class="col-sm-10" style="margin-bottom: 10px">
                                        <asp:DropDownList ID="INSTANSIID" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-2 control-label">
                                        Perihal
                                    </label>
                                    <div class="col-sm-10" style="margin-bottom: 10px">
                                        <input type="text" id="PERIHAL" class="form-control" placeholder="Perihal" runat="server">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btncari" runat="server" OnClick="cari_Click" class="btn btn-primary btn-lg btn-block" Text="Search" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btn_view" runat="server" OnClick="forward_Click" type="button" value="View" class="btn btn-warning btn-lg btn-block" Text="Forward" />

                                </div>

                                <div class="form-group" runat="server" id="f_forward" visible="false">
                                    <label class="col-sm-2 col-sm-2 control-label">
                                        Forward To<span class="asteriskField">
                                         *
                                        </span>
                                    </label>
                                    <div class="col-sm-10" style="margin-bottom: 10px" >
                                        <asp:DropDownList ID="FORWARD" runat="server" class="form-control"  >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="f_keterangan" visible="false">
                                    <label class="col-sm-2 col-sm-2 control-label">
                                       Keterangan<span class="asteriskField">
                                         *
                                        </span>
                                    </label>
                                    <div class="col-sm-10" style="margin-bottom: 10px">
                                        <textarea class="form-control" rows="5" runat="server" id="KET" placeholder="Keterangan" ></textarea>
                                    </div>
                                </div>
                                
                                <div class="form-group" runat="server" id="f_save" visible="false">
                                    <center>
                                    <asp:Button ID="Button3" runat="server" type="button" value="View" class="btn btn-info btn-sm" Text="Save" OnClick="save_Click"  />
                                    <asp:Button ID="cancel" runat="server" type="button" value="Cancel" class="btn btn-danger btn-sm" Text="Cancel" OnClick="cancel_Click"  />
                                    </center>
                                </div>
                            </div>
                            <div class="content-panel">

                                <%--<h4><i class="fa fa-angle-right"></i>Advanced Table</h4>--%>

                                <asp:GridView ID="DataList" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="DataList"
                                    CssClass="table table-striped table-advance table-hover" AllowPaging="true" PageSize="10"
                                    OnRowCommand="DataList_RowCommand"
                                    OnPageIndexChanged="DataList_PageIndexChanged" KeyFieldName="NOSURAT"
                                    OnPageIndexChanging="DataList_PageIndexChanging" GridLines="None">

                                    <PagerStyle HorizontalAlign="Left" Font-Size="Medium" BorderColor="Transparent" />
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-BackColor="Yellow">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                    <asp:CheckBox ID="chkCheck" runat="server" onclick="Check_Click(this)" />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO" HeaderStyle-BackColor="Yellow">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AP_REGINPUT" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden"/>
                                        <asp:BoundField HeaderStyle-CssClass="fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="TGLSURAT" HeaderText=" TANGGAL SURAT" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-bullseye" HeaderStyle-BackColor="Yellow" DataField="NOSURAT" ItemStyle-CssClass="hidden-phone" HeaderText=" NO. SURAT" />
                                        <asp:BoundField HeaderStyle-CssClass="fa fa-home" HeaderStyle-BackColor="Yellow" DataField="INSTANSINAME" HeaderText=" REGULATOR" />
                                        <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-file" HeaderStyle-BackColor="Yellow" DataField="PERIHAL" ItemStyle-CssClass="hidden-phone" HeaderText=" PERIHAL" />
                                        <asp:BoundField HeaderStyle-CssClass="hidden-phone fa fa-calendar" HeaderStyle-BackColor="Yellow" DataField="DATETIMELIMIT" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="hidden-phone" HeaderText=" TANGGAL PELAPORAN" />

                                        <asp:TemplateField HeaderText="" HeaderStyle-BackColor="Yellow">
                                            <ItemStyle />
                                            <ItemTemplate>


                                                <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                    <asp:Button ID="Button1" runat="server" type="button" value="View" class="btn btn-success btn-xs" Text="View" CommandName="View" CommandArgument='<%# Convert.ToString(Eval("INSTANSIID")+":"+Eval("AP_REGINPUT")) %>' />
                                                </span>

                                                <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                    <asp:Button ID="btn_edit" runat="server" type="button" value="Edit" class="btn btn-success btn-xs" Text="Edit" CommandName="Edit" CommandArgument='<%# Convert.ToString(Eval("INSTANSIID")+":"+Eval("AP_REGINPUT")) %>' />

                                                </span>
                                                <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="0"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">

                                                    <asp:Button ID="Button2" runat="server" type="button" OnClick="OnConfirm" value="Hapus" class="btn btn-danger btn-xs" Text="Delete" CommandName="Hapus" CommandArgument='<%# Convert.ToString(Eval("INSTANSIID")+":"+Eval("AP_REGINPUT")) %>' OnClientClick="Confirm()" />

                                                </span>
                                                <span visible='<%# Convert.ToString(Eval("STATUSAPPROVE"))=="1"?Convert.ToBoolean(1):Convert.ToBoolean(0) %>' runat="server">
                                                    <button class="btn btn-primary btn-xs" disabled="disabled">Sudah Approve</button>
                                                </span>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <h1 id="lb_data" style="margin-left: 10px" runat="server" visible="false">DATA NOT FOUND</h1>
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
