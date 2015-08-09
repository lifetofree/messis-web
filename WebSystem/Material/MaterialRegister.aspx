<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialRegister.aspx.cs" Inherits="WebSystem_Material_MaterialRegister" %>

<asp:Content ID="MaterialRegisterHead" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>

<asp:Content ID="MaterialRegisterContent" ContentPlaceHolderID="ContentMain" runat="Server">
    <script src='<%=ResolveUrl("~/Scripts/jquery.MultiFile.js")%>'></script>
    <script src='<%=ResolveUrl("~/Scripts/chosen.jquery.js")%>'></script>
    <script src='<%=ResolveUrl("~/Scripts/bootstrap-datepicker.js")%>'></script>

    <div class="form-group">
        <asp:LinkButton ID="lbAddMatReg" CssClass="btn btn-primary" runat="server" data-original-title="เพิ่มรายการ" data-toggle="tooltip" OnCommand="btnCommand" CommandName="cmdAddMatReg"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></asp:LinkButton>
    </div>
    <asp:GridView ID="gvMaterialRegList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPageIndexChanging" OnRowCommand="gvRowCommand" OnRowDataBound="gvRowDataBound">
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
            <asp:TemplateField HeaderText="Material Code">
                <ItemTemplate>
                    <asp:HiddenField ID="hfMCode" runat="server" Value='<%# Eval("MCode") %>' />
                    <asp:HiddenField ID="hfRCode" runat="server" Value='<%# Eval("RCode") %>' />
                    <asp:Label ID="lblMCode" runat="server" Text='<%# (string)Eval("MCode") + (string)Eval("RCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Material Name">
                <ItemTemplate>
                    <asp:Label ID="lblMName" runat="server" Text='<%# Eval("MName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Material Desc.">
                <ItemTemplate>
                    <asp:Label ID="lblMDesc" runat="server" Text='<%# Eval("MDesc") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Material Img.">
                <ItemTemplate>
                    <%--<asp:Literal ID="litMatImg" runat="server" />--%>
                    <asp:Image ID="imgMaterial" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Kind">
                <ItemTemplate>
                    <asp:Label ID="lblKName" runat="server" Text='<%# Eval("KName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Serial No.">
                <ItemTemplate>
                    <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%-- <!-- <asp:TemplateField HeaderText="สถานะคงคลัง">
                <ItemTemplate>
                    <asp:Label ID="lblUseStatus" runat="server" Text='<%# Eval("UseStatus").ToString() == "1" ? "ใช้งาน" : "ว่าง" %>' CssClass='<%# Eval("UseStatus").ToString() == "1" ? "text-danger bg-danger" : "text-success bg-success" %>' />
                </ItemTemplate>
            </asp:TemplateField> -->--%>
            <asp:TemplateField HeaderText="สถานะ">
                <ItemTemplate>
                    <asp:Label ID="lblRegStatus" runat="server" Text='<%# Eval("RegStatus").ToString() == "1" ? "ใช้งาน" : "ไม่ใช้งาน" %>' CssClass='<%# Eval("RegStatus").ToString() == "1" ? "text-success bg-success" : "text-danger bg-danger" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ดำเนินการ">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="แก้ไข" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("RegIDX") %>'><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
   <%-- <hr class="divider" />--%>
    <asp:Literal ID="litTest" runat="server" Mode="Encode" Visible="true" />
    <asp:FormView ID="fvMaterialRegList" runat="server" DefaultMode="Insert" CssClass="table table-borderless" DataKeyNames="RegIDX">
        <InsertItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Code</label>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <asp:DropDownList ID="ddlMCode" runat="server" CssClass="btn chosen-select" OnSelectedIndexChanged="ddlMCode_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="formInsert" />
                            </div>
                            <asp:TextBox ID="tbRCode" runat="server" CssClass="form-control" placeholder="xxxx" MaxLength="4" ValidationGroup="formInsert" />
                        </div>
                    </div>
                    <label class="col-sm-2 control-label">Asset Type</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbAsName" runat="server" CssClass="form-control" placeholder="Asset Type" Enabled="false" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Name</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMName" runat="server" CssClass="form-control" placeholder="Material Name" MaxLength="250" Enabled="false" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Material Desc.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMDesc" runat="server" CssClass="form-control" placeholder="Material Description" MaxLength="500" Enabled="false" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Kind</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbKName" runat="server" CssClass="form-control" placeholder="Kind" Enabled="false" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Rental/Day</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbRUD" runat="server" CssClass="form-control" placeholder="Rental per Day" MaxLength="10" Enabled="false" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Serial No.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSerialNo" runat="server" CssClass="form-control" placeholder="Serial No." MaxLength="100" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Purchase Date</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbPurDate" runat="server" CssClass="form-control datepicker" placeholder="Purchase Date" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Purchase From</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbPurFrom" runat="server" CssClass="form-control" placeholder="Purchase From" MaxLength="250" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Quantity</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" placeholder="Quantity" MaxLength="10" Text="1" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Voucher No.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbVoucherNo" runat="server" CssClass="form-control" placeholder="Voucher No." MaxLength="50" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Unit Price</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbUnitPrice" runat="server" CssClass="form-control" placeholder="Unit Price" MaxLength="50" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Amount(Baht)</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbAmount" runat="server" CssClass="form-control" placeholder="Amount(Baht)" MaxLength="50" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Image(s)</label>
                    <div class="col-sm-3">
                        <asp:FileUpload ID="fuImages" runat="server" CssClass="uploadfile control-label multi" accept="gif|jpg|pdf|png" />
                    </div>
                    <label class="col-sm-5 control-label" />
                </div>
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
                    <label class="col-sm-2 control-label">Material Code</label>
                    <div class="col-sm-3">
                        <asp:Label ID="lblRegIDXE" runat="server" Visible="false" Text='<%# Eval("RegIDX") %>' ValidationGroup="formEdit" />
                        <div class="input-group">
                            <div class="input-group-btn">
                                <asp:DropDownList ID="ddlMCodeE" runat="server" CssClass="btn chosen-select" ValidationGroup="formEdit" OnSelectedIndexChanged="ddlMCode_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <asp:TextBox ID="tbRCodeE" runat="server" CssClass="form-control" placeholder="xxxx" MaxLength="4" Text='<%# Eval("RCode") %>' ValidationGroup="formEdit" />
                        </div>
                    </div>
                    <label class="col-sm-2 control-label">Asset Type</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbAsName" runat="server" CssClass="form-control" placeholder="Asset Type" Enabled="false" Text='<%# Eval("AsName") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Name</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMName" runat="server" CssClass="form-control" placeholder="Material Name" MaxLength="250" Enabled="false" Text='<%# Eval("MName") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Material Desc.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMDesc" runat="server" CssClass="form-control" placeholder="Material Description" MaxLength="500" Enabled="false" Text='<%# Eval("MDesc") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Kind</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbKName" runat="server" CssClass="form-control" placeholder="Kind" Enabled="false" Text='<%# Eval("KName") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Rental/Day</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbRUD" runat="server" CssClass="form-control" placeholder="Rental per Day" MaxLength="10" Enabled="false" Text='<%# Eval("RUD") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Serial No.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSerialNoE" runat="server" CssClass="form-control" placeholder="Serial No." MaxLength="100" Text='<%# Eval("SerialNo") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Purchase Date</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbPurDateE" runat="server" CssClass="form-control datepicker" placeholder="Purchase Date" Text='<%# Eval("PurDate") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Purchase From</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbPurFromE" runat="server" CssClass="form-control" placeholder="Purchase From" MaxLength="250" Text='<%# Eval("PurFrom") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Quantity</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbQuantityE" runat="server" CssClass="form-control" placeholder="Quantity" MaxLength="10" Text='<%# Eval("Quantity") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Voucher No.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbVoucherNoE" runat="server" CssClass="form-control" placeholder="Voucher No." MaxLength="50" Text='<%# Eval("VoucherNo") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Unit Price</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbUnitPriceE" runat="server" CssClass="form-control" placeholder="Unit Price" MaxLength="50" Text='<%# Eval("UnitPrice") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Amount(Baht)</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbAmountE" runat="server" CssClass="form-control" placeholder="Amount(Baht)" MaxLength="50" Text='<%# Eval("Amount") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Image(s)</label>
                    <div class="col-sm-8">
                        <asp:Repeater ID="rptImagesE" runat="server">
                            <ItemTemplate>
                                <asp:Image ID="imgListE" runat="server" ImageUrl='<%# Container.DataItem %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:FileUpload ID="fuImagesE" runat="server" CssClass="uploadfile control-label multi" accept="gif|jpg|pdf|png" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">สถานะ</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlRegStatusE" runat="server" CssClass="form-control" SelectedValue='<%# Eval("RegStatus") %>' ValidationGroup="formEdit">
                            <asp:ListItem Value="1" Text="ใช้งาน" />
                            <asp:ListItem Value="0" Text="ไม่ใช้งาน" />
                        </asp:DropDownList>
                    </div>
                    <label class="col-sm-5 control-label"></label>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:LinkButton ID="lbInsert" CssClass="btn btn-success" runat="server" data-original-title="บันทึก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdUpdate" CommandArgument="0" Text="บันทึก" ValidationGroup="formEdit"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-danger" runat="server" data-original-title="ยกเลิก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdCancel" Text="ยกเลิก" ValidationGroup="formEdit"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
    <script type="text/javascript">
        $(".chosen-select").chosen({ search_contains: true });
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
