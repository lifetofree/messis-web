<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialList.aspx.cs" Inherits="WebSystem_Material_MaterialList" %>

<asp:Content ID="MaterialListHead" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>

<asp:Content ID="MaterialListContent" ContentPlaceHolderID="ContentMain" runat="Server">
    <div class="form-group">
        <asp:LinkButton ID="lbAddMatList" CssClass="btn btn-primary" runat="server" data-original-title="เพิ่มรายการ" data-toggle="tooltip" OnCommand="btnCommand" CommandName="cmdAddMatList"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></asp:LinkButton>
    </div>
    <asp:GridView ID="gvMaterialList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPageIndexChanging" OnRowCommand="gvRowCommand">
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
                    <asp:Label ID="lblMCode" runat="server" Text='<%# Eval("MCode") %>' />
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
            <asp:TemplateField HeaderText="Kind">
                <ItemTemplate>
                    <asp:Label ID="lblKName" runat="server" Text='<%# Eval("KName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label ID="lblAsName" runat="server" Text='<%# Eval("AsName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rental/Day">
                <ItemTemplate>
                    <asp:Label ID="lblRUD" runat="server" Text='<%# Eval("RUD") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ">
                <ItemTemplate>
                    <asp:Label ID="lblMStatus" runat="server" Text='<%# Eval("MStatus").ToString() == "1" ? "ใช้งาน" : "ไม่ใช้งาน" %>' CssClass='<%# Eval("MStatus").ToString() == "1" ? "text-success bg-success" : "text-danger bg-danger" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ดำเนินการ">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="แก้ไข" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("MIDX") %>'><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--<hr class="divider" />--%>
    <asp:Literal ID="litTest" runat="server" Mode="Encode" Visible="true" />
    <asp:FormView ID="fvMaterialList" runat="server" DefaultMode="Insert" CssClass="table table-borderless" DataKeyNames="MIDX">
        <InsertItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Code</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMCode" runat="server" CssClass="form-control" placeholder="xxxxxx-xxx" MaxLength="10" ValidationGroup="fromInsert" />

                    </div>
                    <label class="col-sm-2 control-label">Asset Type</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlAsIDX" runat="server" CssClass="form-control" ValidationGroup="fromInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Name</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMName" runat="server" CssClass="form-control" placeholder="Material Name" MaxLength="250" ValidationGroup="fromInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Material Desc.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMDesc" runat="server" CssClass="form-control" placeholder="Material Descrition" MaxLength="500" ValidationGroup="fromInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Kind</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlKIDX" runat="server" CssClass="form-control" ValidationGroup="fromInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Rental/Day</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbRUD" runat="server" CssClass="form-control" placeholder="Rental per Day" MaxLength="10" ValidationGroup="fromInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:LinkButton ID="lbInsert" CssClass="btn btn-success" runat="server" data-original-title="บันทึก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdInsert" CommandArgument="0" Text="บันทึก" ValidationGroup="fromInsert"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-danger" runat="server" data-original-title="ยกเลิก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdReset" Text="ยกเลิก" ValidationGroup="fromInsert"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </InsertItemTemplate>
        <EditItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Code</label>
                    <div class="col-sm-3">
                        <asp:Label ID="lblMIDXE" runat="server" Visible="false" Text='<%# Eval("MIDX") %>' ValidationGroup="fromEdit" />
                        <asp:Label ID="lblRMIDXE" runat="server" Visible="false" Text='<%# Eval("RMIDX") %>' ValidationGroup="fromEdit" />
                        <asp:TextBox ID="tbMCodeE" runat="server" CssClass="form-control" placeholder="xxxxxx-xxx" MaxLength="10" Text='<%# Eval("MCode") %>' ValidationGroup="fromEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Asset Type</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlAsIDXE" runat="server" CssClass="form-control" ValidationGroup="fromEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Material Name</label>
                    <div class="col-sm-3">
                        <asp:Label ID="lblMNIDX" runat="server" Visible="false" Text='<%# Eval("MNIDX") %>' ValidationGroup="fromEdit" />
                        <asp:TextBox ID="tbMNameE" runat="server" CssClass="form-control" placeholder="Material Name" MaxLength="250" Text='<%# Eval("MName") %>' ValidationGroup="fromEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Material Desc.</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbMDescE" runat="server" CssClass="form-control" placeholder="Material Description" MaxLength="500" Text='<%# Eval("MDesc") %>' ValidationGroup="fromEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Kind</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlKIDXE" runat="server" CssClass="form-control" ValidationGroup="fromEdit" /><%--SelectedValue='<%# DataBinder.Eval(Container.DataItem, "KIDX")%>'--%>
                    </div>
                    <label class="col-sm-2 control-label">Rental/Day</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbRUDE" runat="server" CssClass="form-control" placeholder="Rental per Day" MaxLength="10" Text='<%# Eval("RUD") %>' ValidationGroup="fromEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">สถานะ</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlMStatusE" runat="server" CssClass="form-control" SelectedValue='<%# Eval("MStatus") %>' ValidationGroup="fromEdit">
                            <asp:ListItem Value="1" Text="ใช้งาน" />
                            <asp:ListItem Value="0" Text="ไม่ใช้งาน" />
                        </asp:DropDownList>
                    </div>
                    <label class="col-sm-5 control-label"></label>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:LinkButton ID="lbInsert" CssClass="btn btn-success" runat="server" data-original-title="บันทึก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdUpdate" CommandArgument="0" Text="บันทึก" ValidationGroup="fromEdit"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CssClass="btn btn-danger" runat="server" data-original-title="ยกเลิก" data-toggle="tooltip" OnCommand="fvCommand" CommandName="cmdCancel" Text="ยกเลิก" ValidationGroup="fromEdit"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
