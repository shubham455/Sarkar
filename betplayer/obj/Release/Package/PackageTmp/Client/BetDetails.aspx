<%@ Page Language="C#" MasterPageFile="~/Client/Menu.Master" AutoEventWireup="true" CodeBehind="BetDetails.aspx.cs" Inherits="betplayer.Client.BetDetails" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td valign="top">

                    <table style="width: 100%" border="0" cellspacing="2" cellpadding="2">
                        <tbody>
                            <tr>

                                <td colspan="3" width="100%" height="45" align="center" class="TeamCombo">
                                    <p class="price-btn price-btn-blue popup-with-zoom-anim" style="height: auto; font-size: 13px; font-weight: bold; text-decoration: blink;"><span id="ScoreMsg" style="text-decoration: blink !important;"></span></p>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" height="30" align="center" class="TeamCombo">
                                    <div class="ScoreCard">
                                        <p>
                                            <span id="batsmanOne"></span>
                                            <br/>
                                            <br />
                                            <span id="batsmanTwo"></span>
                                        </p>
                                    </div>
                                </td>
                                <td width="40%" height="30" align="center" class="TeamCombo">
                                    <div class="ScoreCard_rtmScore ScoreCard" style="color: #000; height: 100%;">
                                        <p>
                                            <span id="LocalTeam"></span>
                                            <br/>
                                            <br />
                                            <span id="VisitorTeam"></span>
                                            <br/>
                                            <span id="Status"></span>
                                        </p>
                                    </div>
                                </td>
                                <td width="20%" height="25" align="center" class="TeamCombo">
                                    <div class="ScoreCard">
                                        <p>
                                            <img id="LastBall" style="height: 100%;" />
                                        </p>
                                    </div>
                                </td>
                            </tr>

                        </tbody>
                    </table>

                </td>
            </tr>
            <tr>
                <td align="left" valign="top">



                    <table style="width: 100%; border-color: #000;" border="1" cellpadding="2" cellspacing="2">
                        <tbody>
                            <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                { %>
                            <tr>
                                <td height="35" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite" style="vertical-align: middle; width: 30%;">RUNNER<p style="font-size:12px;">Max: <span id="RunnerMax">    </span> </p></td>

                                <td align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite" style="vertical-align: middle; width: 20%;">LAGAI</td>
                                <td align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite" style="vertical-align: middle; width: 20%;">KHAI</td>
                                <td align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite" style="vertical-align: middle; width: 20%;">POSITION</td>

                            </tr>

                            <tr id="runnerrow1">
                                <td height="35" id="TeamA" align="center" valign="middle" bgcolor="#FFF" style="vertical-align: middle; font-weight: bold;"><%: row["TeamA"] %></td>


                                <td align="center" valign="middle" bgcolor="#FFF" style="vertical-align: middle; position: relative;">
                                    <div id="runnerfloat1" class="runnerfloat"></div>
                                    <input type="button" name="ButtonL" id="LRate1" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="enableBetting(event, 'team_1', 'Runner', 'Lagai')" /></td>
                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="vertical-align: middle; color: #000">
                                    <input type="button" name="ButtonK" id="KRate1" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="enableBetting(event, 'team_1', 'Runner', 'Khai')" /></td>
                                <td align="center" valign="middle" bgcolor="#FFF" border="1" class="FontTextWhite" style="color: #000; border: 1px solid black; vertical-align: middle">
                                    <asp:Label ID="PositionTeam1" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr id="runnerrow2">
                                <td height="35" id="TeamB" align="center" valign="middle" bgcolor="#FFF" style="vertical-align: middle; font-weight: bold;"><%: row["TeamB"] %></td>


                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="vertical-align: middle; position: relative;">
                                    <div id="runnerfloat2" class="runnerfloat"></div>
                                    <input type="button" name="ButtonL" id="LRate2" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="enableBetting(event, 'team_2', 'Runner', 'Lagai')" /></td>
                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="vertical-align: middle; color: #000">
                                    <input type="button" name="ButtonK" id="KRate2" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL _hover'" onmouseout="this.className='ButtonL'" onclick="enableBetting(event, 'team_2', 'Runner', 'Khai')" /></td>
                                <td align="center" valign="middle" bgcolor="#FFF" class="FontTextWhite" style="color: #000; border: 1px solid black; vertical-align: middle">
                                    <asp:Label ID="PositionTeam2" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <%if (emptyLedgerTable)
                                {
                            %>

                            <tr id="runnerrow3">
                                <td height="35" id="TeamB" align="center" valign="middle" bgcolor="#FFF" style="vertical-align: middle; font-weight: bold;"><%: row["TeamC"] %></td>


                                <td align="center" valign="middle" bgcolor="#79C1F8" class="textTeamHead" style="vertical-align: middle; position: relative;">
                                    <div id="runnerfloat3" class="runnerfloat"></div>
                                    <input type="button" name="ButtonL" id="LRate3" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="enableBetting(event, 'team_c', 'Runner', 'Lagai')" /></td>
                                <td align="center" valign="middle" bgcolor="#FFBAD3" class="textTeamHead" style="vertical-align: middle; color: #000">
                                    <input type="button" name="ButtonK" id="KRate3" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL _hover'" onmouseout="this.className='ButtonL'" onclick="enableBetting(event, 'team_c', 'Runner', 'Khai')" /></td>
                                <td align="center" valign="middle" bgcolor="#FFF" class="FontTextWhite" style="color: #000; border: 1px solid black; vertical-align: middle">
                                    <asp:Label ID="PositionTeam3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%  } //if
                                } //foreach
                            %>
                        </tbody>
                    </table>


                </td>
            </tr>
            <tr>
                <td align="center" valign="bottom" bgcolor="#FFFFFF"></td>
            </tr>
            <tr>
                <td height="35" align="center" class="TeamCombo">
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr bgcolor="#FFF" height="35px;">
                                <td class="FontTextWhite">&nbsp;<span style="color: #00F"></span>/<span style="color: #F00"></span></td>
                                <td width="100" style="font-size: 14px; font-family: Verdana, Geneva, sans-serif; font-weight: bold;">
                                    <td class="FontTextWhite">&nbsp;<span style="color: #00F;">Session Position</span>
                                        <span style="color: #00F">
                                            <asp:Label ID="lblAmount" runat="server" Font-Size="13pt" Font-Bold="true"></asp:Label></span>                 </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <table style="width: 100%;" border="1" cellpadding="0" cellspacing="2">
                        <tbody>
                            <tr>
                                <td height="45" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite10px" style="vertical-align: middle; width: 30%;">SESSION<p>Max:<span id="sessionmax"></span></p></td>
                                <td height="35" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite10px" style="vertical-align: middle; width: 30%">NOT RATE</td>
                                <td height="35" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite10px" style="vertical-align: middle; width: 30%">YES RATE</td>

                            </tr>
                            <tr id="row1">
                                <td height="45" align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #FFF; text-align: center;">
                                    <label style="width: 100%;" id="Session1" class="ButtonSess">None</label></td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000; position: relative;">
                                    <div id="float1" class="float"></div>
                                    <p>
                                        <input type="button" name="Not1" id="not1" value="0.00" class="ButtonNRun" onfocus="this.className='ButtonNRun_hover'" onblur="this.className='ButtonNRun'" onmouseover="this.className='ButtonNRun_hover'" onmouseout="this.className='ButtonNRun'" onclick="enableBetting(event, 'team_1', 'Session', 'Not')">
                                    </p>
                                    <p>
                                        <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                        <input type="button" name="NotRate1" id="notrate1" value="0.00" class="ButtonNRate" onfocus="this.className='ButtonNRate_hover'" onblur="this.className='ButtonNRate'" onmouseover="this.className='ButtonNRate_hover'" onmouseout="this.className='ButtonNRate'" disabled="disabled">
                                    </p>
                                </td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000;">
                                    <p>
                                        <input type="button" name="Yes1" id="yes1" value="0.00" class="ButtonYRun" onfocus="this.className='ButtonYRun_hover'" onblur="this.className='ButtonYRun'" onmouseover="this.className='ButtonYRun_hover'" onmouseout="this.className='ButtonYRun'" onclick="enableBetting(event, 'team_1', 'Session', 'Yes')">
                                    </p>


                                    <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                    <p>
                                        <input type="button" name="YesRate1" id="yesrate1" value="0.00" class="ButtonYRate" onfocus="this.className='ButtonYRate_hover'" onblur="this.className='ButtonYRate'" onmouseover="this.className='ButtonYRate_hover'" onmouseout="this.className='ButtonYRate'" disabled="disabled">
                                    </p>
                                </td>

                            </tr>
                            <tr id="row2">
                                <td height="45" align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #FFF; text-align: center;">
                                    <label style="width: 100%;" id="Session2" class="ButtonSess">None</label></td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000; position: relative;">
                                    <div id="float2" class="float"></div>
                                    <p>
                                        <input type="button" name="Not2" id="not2" value="0.00" class="ButtonNRun" onfocus="this.className='ButtonNRun_hover'" onblur="this.className='ButtonNRun'" onmouseover="this.className='ButtonNRun_hover'" onmouseout="this.className='ButtonNRun'" onclick="enableBetting(event, 'team_1', 'Session', 'Not')">
                                    </p>
                                    <p>
                                        <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                        <input type="button" name="NotRate2" id="notrate2" value="0.00" class="ButtonNRate" onfocus="this.className='ButtonNRate_hover'" onblur="this.className='ButtonNRate'" onmouseover="this.className='ButtonNRate_hover'" onmouseout="this.className='ButtonNRate'" disabled="disabled">
                                    </p>
                                </td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000;">
                                    <p>
                                        <input type="button" name="Yes2" id="yes2" value="0.00" class="ButtonYRun" onfocus="this.className='ButtonYRun_hover'" onblur="this.className='ButtonYRun'" onmouseover="this.className='ButtonYRun_hover'" onmouseout="this.className='ButtonYRun'" onclick="enableBetting(event, 'team_1', 'Session', 'Yes')">
                                    </p>


                                    <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                    <p>
                                        <input type="button" name="YesRate2" id="yesrate2" value="0.00" class="ButtonYRate" onfocus="this.className='ButtonYRate_hover'" onblur="this.className='ButtonYRate'" onmouseover="this.className='ButtonYRate_hover'" onmouseout="this.className='ButtonYRate'" disabled="disabled">
                                    </p>
                                </td>

                            </tr>
                            <tr id="row3">
                                <td height="45" align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #FFF; text-align: center;">
                                    <label style="width: 100%;" id="Session3" class="ButtonSess">None</label></td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000; position: relative;">
                                    <div id="float3" class="float"></div>
                                    <p>
                                        <input type="button" name="Not3" id="not3" value="0.00" class="ButtonNRun" onfocus="this.className='ButtonNRun_hover'" onblur="this.className='ButtonNRun'" onmouseover="this.className='ButtonNRun_hover'" onmouseout="this.className='ButtonNRun'" onclick="enableBetting(event, 'team_1', 'Session', 'Not')">
                                    </p>
                                    <p>
                                        <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                        <input type="button" name="NotRate3" id="notrate3" value="0.00" class="ButtonNRate" onfocus="this.className='ButtonNRate_hover'" onblur="this.className='ButtonNRate'" onmouseover="this.className='ButtonNRate_hover'" onmouseout="this.className='ButtonNRate'" disabled="disabled">
                                    </p>
                                </td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000;">
                                    <p>
                                        <input type="button" name="Yes3" id="yes3" value="0.00" class="ButtonYRun" onfocus="this.className='ButtonYRun_hover'" onblur="this.className='ButtonYRun'" onmouseover="this.className='ButtonYRun_hover'" onmouseout="this.className='ButtonYRun'" onclick="enableBetting(event, 'team_1', 'Session', 'Yes')">
                                    </p>


                                    <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                    <p>
                                        <input type="button" name="YesRate3" id="yesrate3" value="0.00" class="ButtonYRate" onfocus="this.className='ButtonYRate_hover'" onblur="this.className='ButtonYRate'" onmouseover="this.className='ButtonYRate_hover'" onmouseout="this.className='ButtonYRate'" disabled="disabled">
                                    </p>
                                </td>

                            </tr>
                            <tr>
                                <td height="35" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite10px" style="vertical-align: middle; width: 30%;">Fancy Meter<p>Max:<span id="fancymax"></span></p></td>
                                <td height="35" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite10px" style="vertical-align: middle; width: 30%">NOT RATE</td>
                                <td height="35" align="center" valign="middle" bgcolor="#8C8CFD" class="FontTextWhite10px" style="vertical-align: middle; width: 30%">YES RATE</td>
                            </tr>
                            <tr id="row4">
                                <td height="45" align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #FFF; text-align: center;">
                                    <label style="width: 100%;" id="Session4" class="ButtonSess">None</label></td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000; position: relative;">
                                    <div id="float4" class="float"></div>
                                    <p>
                                        <input type="button" name="Not4" id="not4" value="0.00" class="ButtonNRun" onfocus="this.className='ButtonNRun_hover'" onblur="this.className='ButtonNRun'" onmouseover="this.className='ButtonNRun_hover'" onmouseout="this.className='ButtonNRun'" onclick="enableBetting(event, 'team_1', 'Fancy', 'Not')">
                                    </p>
                                    <p>
                                        <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                        <input type="button" name="NotRate4" id="notrate4" value="0.00" class="ButtonNRate" onfocus="this.className='ButtonNRate_hover'" onblur="this.className='ButtonNRate'" onmouseover="this.className='ButtonNRate_hover'" onmouseout="this.className='ButtonNRate'" disabled="disabled">
                                    </p>
                                </td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000;">
                                    <p>
                                        <input type="button" name="Yes4" id="yes4" value="0.00" class="ButtonYRun" onfocus="this.className='ButtonYRun_hover'" onblur="this.className='ButtonYRun'" onmouseover="this.className='ButtonYRun_hover'" onmouseout="this.className='ButtonYRun'" onclick="enableBetting(event, 'team_1', 'Fancy', 'Yes')">
                                    </p>


                                    <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                    <p>
                                        <input type="button" name="YesRate4" id="yesrate4" value="0.00" class="ButtonYRate" onfocus="this.className='ButtonYRate_hover'" onblur="this.className='ButtonYRate'" onmouseover="this.className='ButtonYRate_hover'" onmouseout="this.className='ButtonYRate'" disabled="disabled">
                                    </p>
                                </td>

                            </tr>
                            <tr id="row5">
                                <td height="45" align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #FFF; text-align: center;">
                                    <label style="width: 100%;" id="Session5" class="ButtonSess">None</label></td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000; position: relative;">
                                    <div id="float5" class="float"></div>
                                    <p>
                                        <input type="button" name="Not5" id="not5" value="0.00" class="ButtonNRun" onfocus="this.className='ButtonNRun_hover'" onblur="this.className='ButtonNRun'" onmouseover="this.className='ButtonNRun_hover'" onmouseout="this.className='ButtonNRun'" onclick="enableBetting(event, 'team_1', 'Fancy', 'Not')">
                                    </p>
                                    <p>
                                        <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                        <input type="button" name="NotRate5" id="notrate5" value="0.00" class="ButtonNRate" onfocus="this.className='ButtonNRate_hover'" onblur="this.className='ButtonNRate'" onmouseover="this.className='ButtonNRate_hover'" onmouseout="this.className='ButtonNRate'" disabled="disabled">
                                    </p>
                                </td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000;">
                                    <p>
                                        <input type="button" name="Yes5" id="yes5" value="0.00" class="ButtonYRun" onfocus="this.className='ButtonYRun_hover'" onblur="this.className='ButtonYRun'" onmouseover="this.className='ButtonYRun_hover'" onmouseout="this.className='ButtonYRun'" onclick="enableBetting(event, 'team_1', 'Fancy', 'Yes')">
                                    </p>


                                    <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                    <p>
                                        <input type="button" name="YesRate5" id="yesrate5" value="0.00" class="ButtonYRate" onfocus="this.className='ButtonYRate_hover'" onblur="this.className='ButtonYRate'" onmouseover="this.className='ButtonYRate_hover'" onmouseout="this.className='ButtonYRate'" disabled="disabled">
                                    </p>
                                </td>

                            </tr>
                            <tr id="row6">
                                <td height="45" align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #FFF; text-align: center;">
                                    <label style="width: 100%;" id="Session6" class="ButtonSess">None</label></td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000; position: relative;">
                                    <div id="float6" class="float"></div>
                                    <p>
                                        <input type="button" name="Not6" id="not6" value="0.00" class="ButtonNRun" onfocus="this.className='ButtonNRun_hover'" onblur="this.className='ButtonNRun'" onmouseover="this.className='ButtonNRun_hover'" onmouseout="this.className='ButtonNRun'" onclick="enableBetting(event, 'team_1', 'Fancy', 'Not')">
                                    </p>
                                    <p>
                                        <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                        <input type="button" name="NotRate6" id="notrate6" value="0.00" class="ButtonNRate" onfocus="this.className='ButtonNRate_hover'" onblur="this.className='ButtonNRate'" onmouseover="this.className='ButtonNRate_hover'" onmouseout="this.className='ButtonNRate'" disabled="disabled">
                                    </p>
                                </td>

                                <td align="center" valign="middle" bgcolor="#FFF" class="textTeamHead" style="color: #000;">
                                    <p>
                                        <input type="button" name="Yes6" id="yes6" value="0.00" class="ButtonYRun" onfocus="this.className='ButtonYRun_hover'" onblur="this.className='ButtonYRun'" onmouseover="this.className='ButtonYRun_hover'" onmouseout="this.className='ButtonYRun'" onclick="enableBetting(event, 'team_1', 'Fancy', 'Yes')">
                                    </p>


                                    <span class="textTeamHead" style="color: #000; text-align: center;"></span>
                                    <p>
                                        <input type="button" name="YesRate6" id="yesrate6" value="0.00" class="ButtonYRate" onfocus="this.className='ButtonYRate_hover'" onblur="this.className='ButtonYRate'" onmouseover="this.className='ButtonYRate_hover'" onmouseout="this.className='ButtonYRate'" disabled="disabled">
                                    </p>
                                </td>

                            </tr>

                        </tbody>
                    </table>
                    <table onload="countdown()" style="width: 100%;" border="0" cellpadding="0" cellspacing="0" id="PlaceBet">
                        <tbody>
                            <tr style="background-color: #FF9100">
                                <td height="50" align="center" valign="middle" class="FontTextWhite" style="vertical-align: middle">AMOUNT</td>
                                <td align="center" valign="middle" style="vertical-align: middle">

                                    <input type="text" id="matchAmount" disabled="" style="width: 100px" />

                                    <label id="timerLabel" style="margin-left: 50px; font-family: Verdana; font-weight: bold; color: black; visibility: hidden;"></label>

                                    <input type="button" id="btnSubmit" onclick="doneClick()" style="margin-left: 50px; height: 35px; visibility: hidden;" value="Done" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>

            </tr>
        </tbody>
    </table>


    <div class="menu" id="menu" align="center">
        <ul class="nav">
            <% foreach (System.Data.DataRow row in MatchesDataTable1.Rows)
                { %>
            <li class="active"><a href="BetDetails.aspx?id=<%: row["apiID"] %>&fk=<%: row["firebasekey"] %>&Type=<%: row["Type"] %>"><%: row["TeamA"] %> V <%: row["TeamB"] %> <%: row["Type"] %></a></li>
            <% } //foreach %>

            <li class="active"><a href="AllGamesList.aspx">BACK TO LIST</a></li>
        </ul>
    </div>

    <br />
    <br />

    <table style="width: 100%;" border="1" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td style="height: 25px;" align="right" valign="middle" bgcolor="#FF9100" class="FontTextWhite10px">Rate</td>
                <td align="right" valign="middle" bgcolor="#FF9100" class="FontTextWhite10px">Amount</td>
                <td align="right" bgcolor="#FF9100" style="text-align: center;" class="FontTextWhite10px">Mode</td>
                <td align="center" bgcolor="#FF9100" style="text-align: center;" class="FontTextWhite10px">Team</td>
                <td align="center" bgcolor="#FF9100" style="text-align: center;" class="FontTextWhite10px">Date Time</td>
            </tr>

            <% foreach (System.Data.DataRow row in MatchesDataTable3.Rows)
                { %>
            <tr>

                <td align="left" style="background: #FFF; color: black; text-align: right; height: 25px;" class="FontTextWhite10px "><%:row["Rate"] %></td>
                <td align="left" style="background: #FFF; color: black; text-align: right;" class="FontTextWhite10px "><%:row["Amount"] %></td>
                <td align="left" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["Mode"] %></td>
                <td align="left" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["Team"] %></td>
                <td align="left" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["DateTime"] %></td>
            </tr>
            <% } //foreach %>
        </tbody>
    </table>
    <br />
    <br />
    <table width="100%" border="1" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td height="25" align="center" bgcolor="#FF9100" class="FontTextWhite10px">Session</td>
                <td align="center" valign="middle" bgcolor="#FF9100" class="FontTextWhite10px">Rate</td>
                <td align="center" valign="middle" bgcolor="#FF9100" class="FontTextWhite10px">Amount</td>
                <td align="center" bgcolor="#FF9100" class="FontTextWhite10px">Runs</td>
                <td align="center" bgcolor="#FF9100" class="FontTextWhite10px">Mode</td>
                <td align="center" bgcolor="#FF9100" class="FontTextWhite10px">DateTime</td>
                <td align="center" bgcolor="#FF9100" class="FontTextWhite10px">Dec</td>
            </tr>


            <% foreach (System.Data.DataRow row in MatchesDataTable5.Rows)
                { %>
            <tr>

                <td align="center" style="background: #FFF; color: black; text-align: center; height: 25px; width: 250px" class="FontTextWhite10px "><%:row["Session"] %></td>
                <td align="center" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["Rate"] %></td>
                <td align="center" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["Amount"] %></td>
                <td align="center" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["Runs"] %></td>
                <td align="center" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["Mode"] %></td>
                <td align="center" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["DateTime"] %></td>
                <td align="center" style="background: #FFF; color: black; text-align: center;" class="FontTextWhite10px "><%:row["DEC"] %></td>
            </tr>
            <% } //foreach %>
        </tbody>
    </table>
    <link rel="stylesheet" href="css/Home/mss_v1.css" />
    <div align="center" class="tv-container" style="margin: 15px 0">
        <ul id="p1"></ul>
    </div>
    <asp:HiddenField ID="firebasekey" runat="server" />
    <asp:HiddenField ID="apiID" runat="server" />
    <script src="https://www.gstatic.com/firebasejs/5.5.0/firebase.js"></script>
    <!-- player !-->
    <script src="js/Homejs/mss_v1.js" type="text/javascript"></script>
    <script src="js/Homejs/jsbn.js"></script>
    <script src="js/Homejs/money.js"></script>
    <script src="js/Homejs/BetDetailsCustom_v3.1.js"></script>
</asp:Content>
