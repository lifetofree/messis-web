<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialRecord.aspx.cs" Inherits="WebSystem_Material_MaterialRecord" %>

<asp:Content ID="MaterialRecordHead" ContentPlaceHolderID="ContentHead" runat="Server">
    <!-- http://plugins.krajee.com/file-input/demo#advanced-usage -->
</asp:Content>

<asp:Content ID="MaterialRecordContent" ContentPlaceHolderID="ContentMain" runat="Server">
    <script src='<%=ResolveUrl("~/Scripts/jquery.MultiFile.js")%>'></script>
    <script src='<%=ResolveUrl("~/Scripts/chosen.jquery.js")%>'></script>
    <script src='<%=ResolveUrl("~/Scripts/bootstrap-datepicker.js")%>'></script>

    <script type="text/javascript">
        function CallPrint() {
            var printContent = document.getElementById('<%= pnlToPrint.ClientID %>');
            //alert(printContent.innerHTML);
            var printWindow = window.open("", "Print Panel", 'left=50000,top=50000,width=0,height=0');
            ////printWindow.document.write('<style type = "text/css">thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>');
            //printWindow.document.write('<style type="text/css">.printDoc table {border:solid #000 !important;border-width:1px 0 0 1px !important;} .printDoc th, .printDoc td { border:solid #000 !important; border-width:0 1px 1px 0 !important; }</style>');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
            //return false;
        }
        <%--function printItn() {
            //In my case i have append gridview in Panel that y..you can put your contentID which is you want to print.

            var printContent = document.getElementById('<%= fvMaterialRecList.ClientID %>');
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();

            //  you should add all css refrence for your Gridview. something like.

            var WinPrint = window.open(windowUrl, windowName, 'left=300,top=300,right=500,bottom=500,width=1000,height=500'); WinPrint.document.write('<' + 'html' + '><head><link href="cssreference" rel="stylesheet" type="text/css" /><link href="gridviewcssrefrence" rel="stylesheet" type="text/css" /></head><' + 'body style="background:none !important"' + '>');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.write('<' + '/body' + '><' + '/html' + '>');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }--%>
    </script>

    <%--<style type="text/css">
        @media print {
            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-footer-group;
            }
        }

        @media screen {
            thead {
                display: block;
            }

            tfoot {
                display: block;
            }
        }
    </style>--%>

    <div class="form-group">
        <asp:LinkButton ID="lbAddRec" CssClass="btn btn-primary" runat="server" data-original-title="เพิ่มรายการ" data-toggle="tooltip" OnCommand="btnCommand" CommandName="cmdAddRec"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></asp:LinkButton>
    </div>

    <asp:GridView ID="gvMaterialRecList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPageIndexChanging" OnRowCommand="gvRowCommand">
        <%-- table-striped table-hover--%>
        <HeaderStyle CssClass="info" />
        <SelectedRowStyle CssClass="warning" />
        <PagerStyle CssClass="pageCustom" />
        <PagerSettings Position="Bottom" Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="หน้าแรก" LastPageText="หน้าสุดท้าย" />
        <EmptyDataTemplate>
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Error:</span>
            ไม่พบข้อมูล
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblCustomTextDate" runat="server" Text='<%# setCustomText(0) %>'></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblRecNo" runat="server" Text='<%# Eval("RecNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblCustomTextNo" runat="server" Text='<%# setCustomText(1) %>'></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblRecDate" runat="server" Text='<%# Eval("RecDate") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From">
                <ItemTemplate>
                    <asp:Label ID="lblRecFromSiteName" runat="server" Text='<%# Eval("RecFromSiteName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To">
                <ItemTemplate>
                    <asp:Label ID="lblRecToSiteName" runat="server" Text='<%# Eval("RecToSiteName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Delivery Date">
                <ItemTemplate>
                    <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate") %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%--<asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblRecStatus" runat="server" Text='<%# textStatus((Int32)Eval("RecStatus")) %>' CssClass='<%# cssStatus((Int32)Eval("RecStatus")) %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="ดำเนินการ">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="ดูรายละเอียด" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("RecIDX") %>'><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--<hr class="divider" />--%>
    <asp:Literal ID="litTest" runat="server" Mode="Encode" Visible="true" />
    <asp:FormView ID="fvMaterialRecList" runat="server" DefaultMode="Insert" CssClass="table table-borderless" DataKeyNames="RecIDX">
        <InsertItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblCustomTextNo" runat="server" Text='<%# setCustomText(0) %>'></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbOrderNo" runat="server" CssClass="form-control" placeholder="No. xxxxxx" MaxLength="8" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblCustomTextDate" runat="server" Text='<%# setCustomText(1) %>'></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbOrderCreate" runat="server" CssClass="form-control datepicker" placeholder="Date dd/mm/yyyy" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">From</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlFromSiteList" runat="server" CssClass="btn chosen-select" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">To</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlToSiteList" runat="server" CssClass="btn chosen-select" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <hr class="divider" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Code</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlMCode" runat="server" CssClass="btn chosen-select" OnSelectedIndexChanged="ddlMCode_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Name</label>
                    <div class="col-sm-3">
                        <asp:Label ID="lblFMCode" runat="server" Visible="false" />
                        <asp:Label ID="lblSerialNo" runat="server" Visible="false" />
                        <asp:TextBox ID="tbMName" runat="server" CssClass="form-control" placeholder="Material Name" MaxLength="250" Enabled="false" />
                    </div>
                    <label class="col-sm-2 control-label">Material Desc.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMDesc" runat="server" CssClass="form-control" placeholder="Material Description" MaxLength="500" Enabled="false" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Quantity</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" placeholder="Quantity" MaxLength="250" Enabled="true" />
                    </div>
                    <label class="col-sm-2 control-label">Remark</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbRemark" runat="server" CssClass="form-control" placeholder="Remark" MaxLength="250" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:LinkButton ID="lbAddItem" CssClass="btn btn-primary" runat="server" data-original-title="เพิ่ม" data-toggle="tooltip" OnCommand="btnCommand" CommandName="cmdAddItem" Text="เพิ่ม"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <asp:GridView ID="gvMaterialItemList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <HeaderStyle CssClass="info" />
                            <SelectedRowStyle CssClass="warning" />
                            <EmptyDataTemplate>
                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                <span class="sr-only">Error:</span>
                                กรุณาเลือก Material ที่ต้องการ
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Material Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMCode" runat="server" Text='<%# Eval("CMCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMName" runat="server" Text='<%# Eval("MName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMDesc" runat="server" Text='<%# Eval("MDesc") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("RecQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("RecRemark") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <asp:TextBox ID="tbRecComment" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="ข้อความเพิ่มเติม"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <hr class="divider" />
                    </div>
                </div>--%>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:LinkButton ID="lbInsert" CssClass="btn btn-success" runat="server" data-original-title="บันทึก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdInsert" CommandArgument="0" Text="บันทึก" ValidationGroup="formInsert"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-danger" runat="server" data-original-title="ยกเลิก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdReset" Text="ยกเลิก" ValidationGroup="formInsert"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </InsertItemTemplate>
        <EditItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblCustomTextNo" runat="server" Text='<%# setCustomText(0) %>'></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbOrderNoE" runat="server" CssClass="form-control" Text='<%# Eval("RecNo") %>' placeholder="Delivery No. xxxxxx" MaxLength="8" ReadOnly="true" ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblCustomTextDate" runat="server" Text='<%# setCustomText(1) %>'></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:Label ID="lblRecIDX" runat="server" Text='<%# Eval("RecIDX") %>' Visible="false" />
                        <asp:TextBox ID="tbOrderCreateE" runat="server" CssClass="form-control" Text='<%# Eval("RecDate") %>' placeholder="Date dd/mm/yyyy" ReadOnly="true" ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">From</label>
                    <div class="col-sm-3">
                        <!-- <asp:DropDownList ID="ddlFromSiteList" runat="server" CssClass="btn chosen-select" ValidationGroup="formInsert" /> -->
                        <asp:TextBox ID="tbRecFromSiteNameE" runat="server" CssClass="form-control" Text='<%# Eval("RecFromSiteName") %>' ReadOnly="true" ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">To</label>
                    <div class="col-sm-3">
                        <!-- <asp:DropDownList ID="ddlToSiteList" runat="server" CssClass="btn chosen-select" ValidationGroup="formInsert" /> -->
                        <asp:TextBox ID="tbRecToSiteNameE" runat="server" CssClass="form-control" Text='<%# Eval("RecToSiteName") %>' ReadOnly="true" ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <asp:GridView ID="gvMaterialItemList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <%--AllowPaging="true" PageSize="10"--%>
                            <HeaderStyle CssClass="info" />
                            <SelectedRowStyle CssClass="warning" />
                            <EmptyDataTemplate>
                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                <span class="sr-only">Error:</span>
                                ไม่พบรายการ Material
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Material Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCMCode" runat="server" Text='<%# Eval("CMCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMName" runat="server" Text='<%# Eval("MName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMDesc" runat="server" Text='<%# Eval("MDesc") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("RecQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("RecRemark") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <asp:TextBox ID="tbRecCommentE" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="ข้อความเพิ่มเติม" Text='<%# Eval("RecComment") %>' ReadOnly="true" ValidationGroup="formEdit"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="form-group">
                    <div class="col-sm-8 col-sm-offset-2">
                        <hr class="divider" />
                    </div>
                </div>--%>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <%--<asp:LinkButton ID="lbReturnItem" CssClass="btn btn-success" runat="server" data-original-title="รับคืน" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdReturnItem" Text="รับคืน" />--%>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-info" runat="server" data-original-title="กลับ" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdReset" Text="กลับ" ValidationGroup="formEdit"><span class="glyphicon glyphicon-backward" aria-hidden="true"></span></asp:LinkButton>
                        <%--<asp:LinkButton ID="lbPrint" CssClass="btn btn-primary" runat="server" data-original-title="Print" data-toggle="tooltip" ValidationGroup="formEdit" Visible='<%# Eval("RecStatus").ToString() == "100000" ? true : false %>' OnClientClick="CallPrint();"><span class="glyphicon glyphicon-print" aria-hidden="true"></span></asp:LinkButton>--%><%-- OnCommand="fvCommand" CommandName="cmdPrint" CommandArgument='<%# Eval("RecIDX") %>' --%>
                    </div>
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
    <div style="display: none;">
        <asp:Panel runat="server" ID="pnlToPrint">
            <div class="container">
                <%--<table style="width: 100%;" class="printDoc">
                    <tbody>
                        <tr style="line-height: 30px;">
                            <td style="width: 80%; text-align: center;">THAI OBAYASHI CORP., LTD.</td>
                            <td style="width: 20%;">No.
                            <asp:Literal ID="litOrderNo" runat="server"></asp:Literal></td>
                        </tr>
                        <tr style="line-height: 70px;">
                            <td style="width: 80%; text-align: center;">
                                <h3><b>ORDER SHEET</b></h3>
                            </td>
                            <td style="width: 20%;">Date
                            <asp:Literal ID="litOrderDate" runat="server"></asp:Literal></td>
                        </tr>
                    </tbody>
                </table>
                <table style="width: 100%;">
                    <tbody>
                        <tr style="line-height: 50px;">
                            <td style="width: 50%;">FROM :
                            <asp:Literal ID="litFromSite" runat="server"></asp:Literal></td>
                            <td style="width: 50%;">TO :
                            <asp:Literal ID="litToSite" runat="server"></asp:Literal></td>
                        </tr>
                    </tbody>
                </table>--%>
                
                <asp:GridView ID="gvPrint" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvPrint_RowDataBound" HeaderStyle-Height="30" RowStyle-Height="30">
                    <Columns>
                        <asp:TemplateField HeaderText="CODE">
                            <ItemTemplate>
                                <asp:Label ID="lblCMCode" runat="server" Text='<%# Eval("CMCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MATERIAL">
                            <ItemTemplate>
                                <asp:Label ID="lblMName" runat="server" Text='<%# Eval("MName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SHAPE & SIZE">
                            <ItemTemplate>
                                <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("RecQty") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REMARKS">
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("RecRemark") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <%--<div style="page-break-after:always;"></div>--%>
                <asp:Literal ID="litRecComment" runat="server"></asp:Literal>
                <table style="width: 100%;">
                    <tbody>
                        <tr style="line-height: 30px;">
                            <td style="width: 20%;">SENDER'S SIGN</td>
                            <td style="width: 14%;"></td>
                            <td style="width: 20%;">RECEIVER'S SIGN</td>
                            <td style="width: 14%;"></td>
                            <td style="width: 20%;">TRUCK NO.</td>
                            <td style="width: 12%;"></td>
                        </tr>
                        <tr style="height: 50px;">
                            <td colspan="6">NOTICE :<br />
                                asdasdasdasdasdasd<br />
                                asdasdasdasdadadas<br />
                                asdasdasdasdasdasd<br />
                                asdasdasdasdadadas<br />
                                asdasdasdasdasdasd<br />
                                asdasdasdasdadadas<br />
                                asdasdasdasdasdasd<br />
                                asdasdasdasdadadas
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
        <%--<input type="button" value="Print" onclick="CallPrint();" />--%>
    </div>
    <script type="text/javascript">
        $(".chosen-select").chosen({ search_contains: true, width: "100%" });
        $(".chosen-select-deselect").chosen({ allow_single_deselect: true });

        $(document).ready(function () {
            var dp = $(".datepicker");
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                autoclose: true,
                language: "tr"
            });
        });
    </script>
</asp:Content>
