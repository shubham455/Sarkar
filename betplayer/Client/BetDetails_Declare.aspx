<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Client/Menu.Master" CodeBehind="BetDetails_Declare.aspx.cs" Inherits="betplayer.Client.BetDetails_Declare" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td align="left" valign="top" bgcolor="#336600">

                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td height="35" align="center" bgcolor="#CCFFCC" class="TeamCombo">
                                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">
                                                        <asp:Label ID="lblteamA" runat="server"></asp:Label>
                                                        VS
                                                        <asp:Label ID="lblteamB" runat="server"></asp:Label>
                                                    </p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="padding-top: 5px;">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Match Bet(s)</p>
                                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td colspan="6" align="center"></td>
                                            </tr>
                                            <% foreach (System.Data.DataRow row in MatchesDataTable1.Rows)
                                                { %>
                                            <tr>
                                                <td height="25" align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Rate</td>
                                                <td align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Amount</td>
                                                <td align="center" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Mode</td>
                                                <td align="center" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Team</td>
                                                <td align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px"><%:row["TeamA"] %></td>
                                                <td align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px"><%:row["TeamB"] %></td>
                                            </tr>
                                            <%} //foreach %>
                                            <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                                { %>
                                            <tr>
                                                <td height="25" align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Rate"] %></td>
                                                <td align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Amount"] %></td>
                                                <td align="center" bgcolor="#CCFFFF" class="FontText"><%:row["Mode"] %></td>
                                                <td align="center" bgcolor="#CCFFFF" class="FontText"><%:row["Team"] %></td>
                                                <td align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Position1"] %></td>
                                                <td align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Position2"] %></td>
                                            </tr>
                                            <%} //foreach %>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td align="center" valign="bottom">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="padding-top: 5px;">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Session Bet(s)</p>
                                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                        <tbody>
                                           
                                            <tr>
                                                <td height="25" align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Session</td>
                                                <td height="25" align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Rate</td>
                                                <td align="right" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Amount</td>
                                                <td align="center" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Mode</td>
                                                <td align="center" valign="middle" bgcolor="#006600" class="FontTextWhite10px">Team</td>
                                                
                                            </tr>
                                           
                                            <% foreach (System.Data.DataRow row in SessionDataTable.Rows)
                                                { %>
                                            <tr>
                                                <td height="25" align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Session"] %></td>
                                                <td height="25" align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Rate"] %></td>
                                                <td align="right" bgcolor="#CCFFFF" class="FontText"><%:row["Amount"] %></td>
                                                <td align="center" bgcolor="#CCFFFF" class="FontText"><%:row["Mode"] %></td>
                                                <td align="center" bgcolor="#CCFFFF" class="FontText"><%:row["Team"] %></td>
                                               
                                            </tr>
                                            <%} //foreach %>
                                            

                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">&nbsp;</td>
                            </tr>

                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Match Plus Minus</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class="">
                                    <asp:Label ID="lblMatch" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Session Plus Minus</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class="">
                                    <asp:Label ID="lblSession" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">My Commission</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class="" style="color: #00F;">0</td>
                            </tr>
                            <tr>
                                <td valign="top">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">My Share</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class="" style="color: #F00;"></td>
                            </tr>
                            <tr>
                                <td valign="top">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Amount After Comm. &amp; Share</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class=""></td>
                            </tr>
                            <tr>
                                <td valign="top">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Mob. App. Charges</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class="" style="color: #F00;">You Lost 100 Coins.</td>
                            </tr>

                            <tr>
                                <td valign="top">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim">Net Plus Minus</p>
                                </td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class="">
                                    <asp:Label ID="lblfinalAmount" runat="server"></asp:Label></td>
                            </tr>
                            <tr bgcolor="">
                                <td height="25" colspan="6" align="center" class=""></td>
                            </tr>
                            <tr>
                                <td valign="top"></td>
                            </tr>
                        </tbody>
                    </table>
                </td>

            </tr>
        </tbody>
    </table>



    <tbody>

        <br>
        <br>

        <table width="100%" border="0" cellspacing="2" cellpadding="0">
            <tbody>
                <tr>
                    <td>
                        <div class="menu" id="menu" align="center">
                            <ul class="nav">
                                <li class="active"><a href="AllGamesList.php">BACK TO LIST</a></li>

                            </ul>
                        </div>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </tbody>
        </table>
</asp:Content>
