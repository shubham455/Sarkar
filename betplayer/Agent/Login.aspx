<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="betplayer.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="gr__34zone_co_in">
<head>
    <meta charset="utf-8" />
    <title>Login page</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="./assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="./assets/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="./css/style.min.css" rel="stylesheet" />
    <link href="./css/style_responsive.css" rel="stylesheet" />
    <link href="./css/style_default.css" rel="stylesheet" id="style_color" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body id="login-body">
    <div class="login-header">
        <div id="logo" class="center">
            <a href="#">Sarkar50</a>
        </div>
    </div>
    <div id="login">

        <form id="loginform" runat="server" class="form-vertical no-padding no-margin">
    <div class="lock"><i class="icon-lock"></i></div>
    <div class="control-wrap">
        <h4>User Login</h4>
        <div class="control-group">
            <div class="controls">
                <div class="input-prepend">
                    <span class="add-on"><i class="icon-user"></i></span>
                    <asp:TextBox ID="txtusername" runat="server" placeholder="Username" />
                </div>
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <div class="input-prepend">
                    <span class="add-on"><i class="icon-key"></i></span>
                    <asp:TextBox ID="txtpassword" runat="server" placeholder="***********" TextMode="Password" />
                </div>
                <div class="mtop10">
                    <div class="block-hint pull-left small">
                        <input type="checkbox" id="" />
                        Remember Me
                    </div>
                    <div class="block-hint pull-right"><a href="javascript:;" class="" id="forget-password">Forgot Password?</a></div>
                </div>
                <div class="clearfix space5"></div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnsubmit" CssClass="btn btn-block login-btn" runat="server" Text="Login" OnClick="Submitbtn_Click" />
    </form>
    </div>
    <div id="login-copyright">2018 &copy; livegame25.com </div>


</body>
</html>
