﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialTypeList.aspx.cs" Inherits="WebSystem_Material_MaterialTypeList" %>

<asp:Content ID="MaterialTypeListHead" ContentPlaceHolderID="ContentHead" runat="Server">
</asp:Content>

<asp:Content ID="MaterialTypeListContent" ContentPlaceHolderID="ContentMain" runat="Server">
	<div class="form-group">
        <asp:LinkButton ID="lbAddType" CssClass="btn btn-primary" runat="server" data-original-title="เพิ่มรายการ" data-toggle="tooltip" OnCommand="btnCommand" CommandName="cmdAddType"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></asp:LinkButton>
    </div>
	<asp:GridView ID="gvTypeList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="TypeIDX" OnRowCommand="gvRowCommand">
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
                    <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblTypeDesc" runat="server" Text='<%# Eval("TypeDesc") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ">
                <ItemTemplate>
                    <asp:Label ID="lblTypeStatus" runat="server" Text='<%# Eval("TypeStatus").ToString() == "1" ? "ใช้งาน" : "ไม่ใช้งาน" %>' CssClass='<%# Eval("TypeStatus").ToString() == "1" ? "text-success bg-success" : "text-warning bg-warning" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ดำเนินการ">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-info btn-xs" runat="server" data-original-title="แก้ไข" data-toggle="tooltip" CommandName="cmdEdit" CommandArgument='<%# Eval("TypeIDX") %>'><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Literal ID="litTest" runat="server" Mode="Encode" Visible="true" />
    <asp:FormView ID="fvTypeList" runat="server" DefaultMode="Insert" CssClass="table table-borderless">
        <InsertItemTemplate>
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control" placeholder="Name" MaxLength="30" ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbDesc" runat="server" CssClass="form-control" placeholder="Description" MaxLength="500" ValidationGroup="formInsert" />
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
            <asp:Label ID="lblTypeIDXE" runat="server" Visible="false" Text='<%# Eval("TypeIDX") %>' ValidationGroup="fromEdit" />
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control" placeholder="Name" MaxLength="30" Text='<%# Eval("TypeName") %>' ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbDesc" runat="server" CssClass="form-control" placeholder="Description" MaxLength="500" Text='<%# Eval("TypeDesc") %>' ValidationGroup="formInsert" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">สถานะ</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlStatusE" runat="server" CssClass="form-control" SelectedValue='<%# Eval("TypeStatus") %>' ValidationGroup="fromEdit">
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