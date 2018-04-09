﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="betplayer.admin.Login" %>

<!DOCTYPE html>

<html>
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
            <img src="./img/logo.png" alt="logo" class="center" /></div>
    </div>
    <div id="login">
        <form id="loginform" class="form-vertical no-padding no-margin" action="index.html" />
        <div class="lock"><i class="icon-lock"></i></div>
        <div class="control-wrap">
            <h4>User Login</h4>
            <div class="control-group">
                <div class="controls">
                    <div class="input-prepend"><span class="add-on"><i class="icon-user"></i></span>
                        <input id="input-username" type="text" placeholder="Username" /></div>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <div class="input-prepend"><span class="add-on"><i class="icon-key"></i></span>
                        <input id="input-password" type="password" placeholder="Password" /></div>
                    <div class="mtop10">
                        <div class="block-hint pull-left small">
                            <input type="checkbox" id="" />
                            Remember Me </div>
                        <div class="block-hint pull-right"><a href="javascript:;" class="" id="forget-password">Forgot Password?</a></div>
                    </div>
                    <div class="clearfix space5"></div>
                </div>
            </div>
        </div>
        <input type="submit" id="login-btn" class="btn btn-block login-btn" value="Login" /></form><form id="forgotform" class="form-vertical no-padding no-margin hide" action="index.html" />
        <p class="center">Enter your e-mail address below to reset your password.</p>
        <div class="control-group">
            <div class="controls">
                <div class="input-prepend"><span class="add-on"><i class="icon-envelope"></i></span>
                    <input id="input-email" type="text" placeholder="Email" /></div>
            </div>
            <div class="space20"></div>
        </div>
        <input type="button" id="forget-btn" class="btn btn-block login-btn" value="Submit" /></form></div>
    <div id="login-copyright">2013 &copy; Admin Lab Dashboard. </div>
   
</body>
</html>