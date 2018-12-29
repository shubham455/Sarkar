<%@ Page Language="C#" MasterPageFile="~/Client/Menu.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="betplayer.Client.MainMenu" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-color: #FFF">
        <tbody>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center"><a href="inplay.aspx">
                                    <img src="images/MainMenuImg/crick.png" alt="" width="112" height="112"></a></td>
                            </tr>
                            <tr>
                                <td height="25" align="center" class="GameList"><a href="inplay.aspx" style="color: #000000">In Play</a></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center"><a href="AllGamesList.aspx">
                                    <img src="images/MainMenuImg/allGame (1).png" alt="" width="112" height="112"></a></td>
                            </tr>
                            <tr>
                                <td height="25" align="center" class="GameList"><a href="UpcomingGame.php" style="color: #000000">All Games</a></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center"><a href="MyLedger.aspx">
                                    <img src="images/MainMenuImg/CL.png" alt="" width="112" height="112"></a></td>
                            </tr>
                            <tr>
                                <td height="25" align="center" class="GameList"><a href="CompleteGame.php" style="color: #000000">My Legder</a></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center"><a href="Change_Password.aspx">
                                    <img src="images/MainMenuImg/CP.png" alt="" width="112" height="112"></a></td>
                            </tr>
                            <tr>
                                <td height="25" align="center" class="GameList"><a href="ClientProfile.php" style="color: #000000">Change password</a></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center"><a href="MyLedger.aspx">
                                    <img src="images/MainMenuImg/Rules.png" alt="" width="112" height="112"></a></td>
                            </tr>
                            <tr>
                                <td height="25" align="center" class="GameList"><a href="MyLedger.php" style="color: #000000">Terms & Conditions</a></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td align="center"><a href="Login.aspx">
                                    <img src="images/MainMenuImg/Logout.png" alt="" width="112" height="112"></a></td>
                            </tr>
                            <tr>
                                <td height="25" align="center" class="GameList"><a href="ChangePassword.php" style="color: #000000">Log out</a></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </tbody>
    </table>
</asp:Content>
