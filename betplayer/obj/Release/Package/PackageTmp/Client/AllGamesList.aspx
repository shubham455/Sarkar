<%@ Page Language="C#" MasterPageFile="~/Client/Menu.Master" AutoEventWireup="true" CodeBehind="AllGamesList.aspx.cs" Inherits="betplayer.Client.AllGamesList" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <% foreach(System.Data.DataRow row in MatchesDataTable.Rows) { %>
    <div>
    
    </div>
    
    <div class="TeamName">
        <a href="#"><%: row["teamname"] %> </a>
    </div>
    <div class="profile-details">
        
        <a href="<%if (Convert.ToInt16(row["status"]) == 1) {%>BetDetails.aspx?iD=<%: row["apiID"] %>&&fk=<%: row["firebasekey"] %><% }else{%>BetDetails_Declare.aspx?iD=<%: row["apiID"] %><% } %>">

            <table width="100%" border="0" cellspacing="2" cellpadding="2">
                <tbody>
                    <tr>
                        <td width="1%">&nbsp;</td>
                        <td align="center" style="vertical-align: middle; text-align: center">

                            <div class="profile_picture">
                                <p align="center">
                                    <img src="images/Inplayimg/mark1.png" alt="team image" style="height:100px"></p>
                            </div>

                        </td>
                        <td class="GameList" style="vertical-align: top;">

                            <table width="99%" border="0" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td class="GameList" align="center"><%: row["Date"] %> </td>
                                    </tr>
                                    <tr>
                                        <td class="GameList" align="center">Match Bets :<%: row["MatchBetCount"] %> </td>
                                    </tr>
                                    <tr>
                                        <td class="GameList" align="center">Session Bets :<%: row["SessionBetCount"] %> </td>
                                    </tr>
                                    <tr>
                                        <td class="GameList" align="center">Declared :<%if(Convert.ToInt16( row["Status"]) == 1) {%>NO<% }else{%>Yes<% } %> </td>
                                    </tr>
                                    <tr>
                                        <td class="GameList" align="center">Won By : <%: row["winnerTeam"] %></td>
                                    </tr>
                                </tbody>
                            </table>

                        </td>
                        <td align="center" style="vertical-align: middle;">
                            <p align="center">
                                <img src="images/Inplayimg/mark1.png"alt="team image" style="height:100px"></p>
                        </td>
                        <td width="1%">&nbsp;</td>
                    </tr>
                </tbody>
            </table>
        </a>
    </div>
    <br>
    &nbsp;<br>
    
    <% } //foreach %>

</asp:Content>