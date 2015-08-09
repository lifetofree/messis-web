<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SiteName.aspx.cs" Inherits="WebSystem_Site_SiteName" %>

<asp:Content ID="SiteNameHead" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>

<asp:Content ID="SiteNameContent" ContentPlaceHolderID="ContentMain" runat="Server">
    <script src='<%=ResolveUrl("~/Scripts/bootstrap-datepicker.js")%>'></script>

    <div class="form-group">
        <asp:LinkButton ID="lbAddSite" CssClass="btn btn-primary" runat="server" data-original-title="เพิ่มรายการ" data-toggle="tooltip" OnCommand="btnCommand" CommandName="cmdAddSite"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></asp:LinkButton>
    </div>

    <asp:GridView ID="gvSiteList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPageIndexChanging" OnRowCommand="gvRowCommand">
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
            <asp:TemplateField HeaderText="Site Code">
                <ItemTemplate>
                    <asp:Label ID="lblSiteCode" runat="server" Text='<%# Eval("SiteCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project">
                <ItemTemplate>
                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Manager">
                <ItemTemplate>
                    <asp:Label ID="lblManagerName" runat="server" Text='<%# Eval("ManagerName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Admin. Staff">
                <ItemTemplate>
                    <asp:Label ID="lblStaffName" runat="server" Text='<%# Eval("StaffName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="Location">
                <ItemTemplate>
                    <asp:Label ID="lblSiteLocation" runat="server" Text='<%# Eval("SiteLocation") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start">
                <ItemTemplate>
                    <asp:Label ID="lblSiteStart" runat="server" Text='<%# Eval("SiteStart") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End">
                <ItemTemplate>
                    <asp:Label ID="lblSiteEnd" runat="server" Text='<%# Eval("SiteEnd") %>' />
                </ItemTemplate>
            </asp:TemplateField> --%>
            <asp:TemplateField HeaderText="สถานะ">
                <ItemTemplate>
                    <asp:Label ID="lblSiteStatus" runat="server" Text='<%# Eval("SiteStatus").ToString() == "1" ? "ใช้งาน" : "ไม่ใช้งาน" %>' CssClass='<%# Eval("SiteStatus").ToString() == "1" ? "text-success bg-success" : "text-danger bg-danger" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ดำเนินการ">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="แก้ไข" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("SiteIDX") %>'><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--<hr class="divider" />--%>
    <asp:Literal ID="litTest" runat="server" Mode="Encode" Visible="true" />
    <asp:FormView ID="fvSiteList" runat="server" DefaultMode="Insert" CssClass="table table-borderless" DataKeyNames="SiteIDX">
        <InsertItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Site Code</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSiteCode" runat="server" CssClass="form-control" placeholder="Site Code" MaxLength="7" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Project</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbProjectName" runat="server" CssClass="form-control" placeholder="Project" MaxLength="500" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Manager</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbManagerName" runat="server" CssClass="form-control" placeholder="Manager" MaxLength="500" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">Admin. Staff</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbStaffName" runat="server" CssClass="form-control" placeholder="Admin. Staff" MaxLength="500" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-sm-2 control-label">Location</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="tbSiteLocation" runat="server" CssClass="form-control" placeholder="Location" MaxLength="1000" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label" />
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Start</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSiteStart" runat="server" CssClass="form-control datepicker" placeholder="Site Start Date : DD/MM/YYYY" ValidationGroup="formInsert" />
                    </div>
                    <label class="col-sm-2 control-label">End</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSiteEnd" runat="server" CssClass="form-control datepicker" placeholder="Site End Date : DD/MM/YYYY" ValidationGroup="formInsert" />
                    </div>
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
                    <label class="col-sm-2 control-label">Site Code</label>
                    <div class="col-sm-3">
                        <asp:Label ID="lblSiteIDXE" runat="server" Visible="false" Text='<%# Eval("SiteIDX") %>' ValidationGroup="formEdit" />
                        <asp:Label ID="lblRSiteIDXE" runat="server" Visible="false" Text='<%# Eval("RSiteIDX") %>' ValidationGroup="formEdit" />
                        <asp:TextBox ID="tbSiteCodeE" runat="server" CssClass="form-control" placeholder="Site Code" MaxLength="7" Text='<%# Eval("SiteCode") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Project</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbProjectNameE" runat="server" CssClass="form-control" placeholder="Project" MaxLength="500" Text='<%# Eval("ProjectName") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Manager</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbManagerNameE" runat="server" CssClass="form-control" placeholder="Manager" MaxLength="500" Text='<%# Eval("ManagerName") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">Admin. Staff</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbStaffNameE" runat="server" CssClass="form-control" placeholder="Admin. Staff" MaxLength="500" Text='<%# Eval("StaffName") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-sm-2 control-label">Location</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="tbSiteLocationE" runat="server" CssClass="form-control" placeholder="Location" MaxLength="1000" Text='<%# Eval("SiteLocation") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label" />
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Start</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSiteStartE" runat="server" CssClass="form-control datepicker" placeholder="Site Start Date : DD/MM/YYYY" Text='<%# Eval("SiteStart") %>' ValidationGroup="formEdit" />
                    </div>
                    <label class="col-sm-2 control-label">End</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbSiteEndE" runat="server" CssClass="form-control datepicker" placeholder="Site End Date : DD/MM/YYYY" Text='<%# Eval("SiteEnd") %>' ValidationGroup="formEdit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">สถานะ</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlSiteStatusE" runat="server" CssClass="form-control" SelectedValue='<%# Eval("SiteStatus") %>' ValidationGroup="formEdit">
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
