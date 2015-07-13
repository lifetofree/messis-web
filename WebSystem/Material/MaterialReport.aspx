<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialReport.aspx.cs" Inherits="WebSystem_Material_MaterialReport" %>

<asp:Content ID="MaterialReportHead" ContentPlaceHolderID="ContentHead" runat="Server">
    
</asp:Content>

<asp:Content ID="MaterialReportContent" ContentPlaceHolderID="ContentMain" runat="Server">
	<asp:GridView ID="gvMaterialReport" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
        <%-- AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPageIndexChanging" OnRowCommand="gvRowCommand" table-striped table-hover--%>
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
            <asp:TemplateField HeaderText="Material Name">
                <ItemTemplate>
                    <asp:Label ID="lblMName" runat="server" Text='<%# Eval("MName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวนคงเหลือ">
                <ItemTemplate>
                    <asp:Label ID="lblMCount" runat="server" Text='<%# Eval("MCount") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>