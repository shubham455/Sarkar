<%@ Page Language="C#" MasterPageFile="~/Client/Menu.Master" AutoEventWireup="true" CodeBehind="inplay.aspx.cs" Inherits="betplayer.Client.inplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
        { %>
    <div>
    </div>

    <div class="TeamName">
        <a  style="color:black" href="#"><%: row["teamname"] %></a>
    </div>
    <div class="profile-details">
        <a href="BetDetails.aspx?id=<%: row["apiID"] %>&fk=<%: row["firebasekey"] %>&Type=<%: row["Type"] %>">
            <table width="100%" border="0" cellspacing="2" cellpadding="2">
                <tbody>
                    <tr style="color:black">
                        <td width="1%">&nbsp;</td>
                        <td align="center" style="vertical-align: middle; text-align: center">

                            <div class="profile_picture">
                                <p align="center">
                                    <img src="images/inplayImg/mark1.png" alt="team image" style="height:100px"/>
                                </p>
                            </div>

                        </td>
                        <td class="GameList" style="vertical-align: top;">

                            <table width="99%" border="0" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr >
                                        <td class="GameList" align="center"><%: row["Date"] %> </td>
                                    </tr>
                                    <tr>
                                        <td class="GameList" align="center">Match Bets :<%: row["MatchBetCount"] %> </td>
                                    </tr>
                                    <tr>
                                        <td class="GameList" align="center">Session Bets : <%: row["SessionBetCount"] %></td>
                                    </tr>
                                    
                                    
                                </tbody>
                            </table>

                        </td>
                        <td align="center" style="vertical-align: middle;">
                            <p align="center">
                                <img src="images/inplayImg/mark1.png" alt="team image" style="height:100px"/>
                            </p>
                        </td>
                        <td width="1%">&nbsp;</td>
                    </tr>
                </tbody>
            </table>
        </a>
    </div>
    <br/>
    &nbsp;<br/>

    <% } //foreach %>


    <div class="content_bottom">
        <div class="clear"></div>
    </div>


</asp:Content>
