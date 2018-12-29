<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="betplayer.Client.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sarkar50</title>

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/login/style_v1.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/login/clndr.css" rel="stylesheet" type="text/css" media="all" />
    <!--<link rel="stylesheet" type="text/css" href="captcha.css">-->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>



</head>
<body style="background-image: url(images/loginimg/bk2.gif);">
    <br />
    <br />
    <br />
    <br />

    <!---start-wrap---->
    <div class="wrap" style="margin-left: 10px;" >
        <!---  End Header ----->

        <!----- Content ------------->
        <div class="content_top">
            <div class="content_top-grid3"></div>
            <div class="clear"></div>
        </div>

        <div class="sign-in">
            <div class="sign_in">
                <h3>Login</h3>
                <form id="Form1" runat="server" defaultfocus="btnSubmit">
                    <asp:Panel DefaultButton="btnSubmit" runat="server">
                        <span><i class="user"></i>
                            <asp:TextBox ID="txtusername" runat="server" placeholder="Username"></asp:TextBox>
                        </span><span><i class="lock"></i>
                            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" placeholder="*********"></asp:TextBox>
                        </span>
                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                            <tbody>

                                <tr>

                                    <asp:ScriptManager ID="SM1" runat="server">
                                    </asp:ScriptManager>
                                    <td>
                                        <asp:UpdatePanel ID="UP1" runat="server">

                                            <ContentTemplate>

                                                <table>

                                                    <tr>

                                                        <td style="height: 50px; width: 100px;">

                                                            <asp:Image ID="imgCaptcha" Style="color: black; background-attachment: fixed;" runat="server" />

                                                        </td>

                                                        <td style="align-content: center"></td>

                                                    </tr>

                                                </table>

                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                    </td>


                                </tr>
                            </tbody>
                        </table>

                        <span><i class="FontText"></i>
                            <asp:TextBox ID="txtCaptcha" Style="background: #FFF;" runat="server" placeholder="Text Captcha"></asp:TextBox>
                        </span>
                    </asp:Panel>
                    <br />
                    <br />
                    <br />

                    <table>
                        <tbody>

                            <tr>
                                <td>

                                    <asp:Button ID="btnHOME" CssClass="HomeButton" style="background-color:#FFBAD3;color:black" runat="server" Text="Home" Font-Bold="true" class="HomeButton" OnClick="btnHOME_Click" />
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSubmit" CssClass="LoginButton" runat="server" style="background-color:lightgreen;color:black" Text="Login" Font-Bold="true" class="LoginButton" OnClick="Submitbtn_Click" />
                                </td>

                            </tr>
                        </tbody>
                    </table>


                </form>
            </div>
        </div>
        <br />
        <br />


    </div>
    <br />

    <br />


    <h4 align="center" style="color: #FFF; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">Copy Right @ Khai Lagai</h4>


</body>

</html>
