<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="WebSystem_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MESSIS : Material Stock System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="row">
            <div style="margin-top:50px;" class="col-md-4 col-md-offset-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">กรุณาล็อกอินเพื่อเข้าสู่ระบบ</h3>
                    </div>
                    <div class="panel-body">
                        <form id="formLogin" runat="server" class="form-horizontal" role="form">
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">ชื่อผู้ใช้</label>
                                <div class="col-sm-9">
                                    <%--<input type="email" class="form-control" id="inputEmail3" placeholder="ชื่อผู้ใช้" />--%>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="form-control" placeholder="ชื่อผู้ใช้" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputPassword3" class="col-sm-3 control-label">รหัสผ่าน</label>
                                <div class="col-sm-9">
                                    <input type="password" class="form-control" id="inputPassword3" placeholder="รหัสผ่าน" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-9">
                                    <button type="submit" class="btn btn-success">ล็อกอิน</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery-1.9.0.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</body>
</html>
