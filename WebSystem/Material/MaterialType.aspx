<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialType.aspx.cs" Inherits="WebSystem_Material_MaterialType" %>

<asp:Content ID="MaterialTypeHead" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>

<asp:Content ID="MaterialTypeContent" ContentPlaceHolderID="ContentMain" runat="Server">
    <asp:MultiView ID="mvMaterialType" runat="server">
        <asp:View ID="vMaterialKind" runat="server">
            <%--<div class="form-group">
                <label for="inputEmail3" class="col-sm-3 control-label">ชื่อผู้ใช้</label>
                <div class="col-sm-9">
                    <asp:TextBox ID="tbUserName" runat="server" CssClass="form-control" placeholder="ชื่อผู้ใช้" />
                </div>
            </div>--%>
            <asp:GridView ID="gvKindList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="KIDX">
                <%-- table-striped table-hover--%>
                <HeaderStyle CssClass="info" />
                <EmptyDataTemplate>
                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Error:</span>
                    ไม่พบข้อมูล
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="ประเภท">
                        <ItemTemplate>
                            <asp:Label ID="lblKindName" runat="server" Text='<%# Eval("KName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblKindDesc" runat="server" Text='<%# Eval("KDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="สถานะ">
                        <ItemTemplate>
                            <asp:Label ID="lblKindStatus" runat="server" Text='<%# Eval("KStatus").ToString() == "1" ? "ใช้งาน" : "ไม่ใช้งาน" %>' CssClass='<%# Eval("KStatus").ToString() == "1" ? "text-success bg-success" : "text-warning bg-warning" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ดำเนินการ">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="แก้ไข" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("SiteIDX") %>'><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>
        <asp:View ID="vMaterialAsset" runat="server">
            <asp:GridView ID="gvAssetList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="AsIDX">
                <HeaderStyle CssClass="info" />
                <EmptyDataTemplate>
                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span class="sr-only">Error:</span>
                    ไม่พบข้อมูล
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="ประเภท Asset">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetName" runat="server" Text='<%# Eval("AsName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetDesc" runat="server" Text='<%# Eval("AsDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="สถานะ">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetStatus" runat="server" Text='<%# Eval("AsStatus").ToString() == "1" ? "ใช้งาน" : "ไม่ใช้งาน" %>' CssClass='<%# Eval("AsStatus").ToString() == "1" ? "text-success bg-success" : "text-warning bg-warning" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ดำเนินการ">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="แก้ไข" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("SiteIDX") %>'><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>
    </asp:MultiView>
</asp:Content>
