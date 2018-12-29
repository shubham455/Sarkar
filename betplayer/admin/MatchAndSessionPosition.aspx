<%@ Page Language="C#" MasterPageFile="~/admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="MatchAndSessionPosition.aspx.cs" Inherits="betplayer.admin.MatchAndSessionPosition" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <meta http-equiv="refresh" content="10">
    <div id="main-content">
        <!-- BEGIN PAGE CONTAINER-->
        <div class="container-fluid">
            <!-- BEGIN PAGE HEADER-->
            <div class="row-fluid">
                <div class="span12">
                    <!-- BEGIN THEME CUSTOMIZER-->
                    <div id="theme-change" class="hidden-phone"><i class="icon-cogs"></i><span class="settings"><span class="text">Theme:</span> <span class="colors"><span class="color-default" data-style="default"></span><span class="color-gray" data-style="gray"></span><span class="color-purple" data-style="purple"></span><span class="color-navy-blue" data-style="navy-blue"></span></span></span></div>
                    <!-- END THEME CUSTOMIZER-->
                    <!-- BEGIN PAGE TITLE & BREADCRUMB-->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Sport's Betting<span class="divider">&nbsp;</span> </li>
                        <li>Match &amp; Session Position<span class="divider">&nbsp;</span></li>
                        <li><a href="SportsDetails.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
                    </ul>
                    <!-- END PAGE TITLE & BREADCRUMB-->
                </div>
            </div>
            <!-- END PAGE HEADER-->
            <!-- BEGIN ADVANCED TABLE widget-->
            <div class="row-fluid">
                <div class="span12">
                    <!-- BEGIN EXAMPLE TABLE widget-->
                    <div class="widget">

                        <div class="widget-body form">
                            <!-- BEGIN FORM-->


                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td width="4" height="250" bgcolor="#FFFFFF">&nbsp;</td>
                                                        <td width="321" colspan="3" align="left" valign="top">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="top">

                                                                            <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td height="35" colspan="2" class="ButtonK" style="text-align: center;">
                                                                                            <span id="ScoreMsg" style="text-decoration: blink !important;"></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="80%" height="70" class="ButtonK" style="text-align: center;">
                                                                                            <div class="ScoreCard_rtmScore " style="color: #fff;">
                                                                                                <p>
                                                                                                    <span id="LocalTeam" style="color: black"></span>
                                                                                                    <br>
                                                                                                    <span id="VisitorTeam" style="color: black"></span>
                                                                                                    <br>
                                                                                                    <span id="Status" style="color: black"></span>
                                                                                                    <br>
                                                                                                </p>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td width="20%" style="text-align: center; vertical-align: middle;">
                                                                                             <img id="LastBall" style="height: 100%;" src="" /></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">

                                                                            <div id="divMatchBhav" style="float: left; padding-top: 10px;" width="400">
                                                                                <table style="width: 500px;" class="table table-striped table-hover table-bordered">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="ButtonK" style="text-align: center; vertical-align: middle; line-height: 35px; width: 120px;">TEAM</td>
                                                                                            <td class="ButtonL" style="text-align: center; vertical-align: middle; line-height: 35px; width: 70px;">KHAI</td>
                                                                                            <td class="ButtonK" style="text-align: center; vertical-align: middle; line-height: 35px; width: 70px;">LAGAI</td>
                                                                                            <td style="text-align: center; vertical-align: middle; line-height: 35px; width: 140px;"><span class="ButtonK">PLUS</span>/<span class="ButtonL">MINUS</span></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Team1" id="Team1" value="" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="ShowMatch()" style="width: 120px;"></td>
                                                                                            <td align="center" valign="middle" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="KRate1" id="LRate1" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddMatchBitK(1)"></td>
                                                                                            <td align="center" valign="middle" style="text-align: center; vertical-align: middle;"><span style="color: ">
                                                                                                <input type="button" name="LRate1" id="KRate1" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddMatchBitL(1)">
                                                                                            </span></td>
                                                                                            <td align="center" valign="middle" style="text-align: center; vertical-align: middle;">
                                                                                                <asp:Label ID="Team1Amt" runat="server" Style="font-size: 13px; font-weight: bold"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;"><span>
                                                                                                <input type="button" name="Team2" id="Team2" value="" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="ShowMatch()" style="width: 120px;">
                                                                                            </span></td>
                                                                                            <td align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="KRate2" id="LRate2" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddMatchBitK(2)"></td>
                                                                                            <td align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;"><span style="color: ">
                                                                                                <input type="button" name="LRate2" id="KRate2" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddMatchBitL(2)">
                                                                                            </span></td>
                                                                                            <td align="center" valign="middle" style="text-align: center; vertical-align: middle;">
                                                                                                <asp:Label ID="Team2Amt" runat="server" Style="font-size: 13px; font-weight: bold"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%if (emptyLedgerTable)
                                                                                            {
                                                                                        %>
                                                                                        <tr>
                                                                                            <td height="35" align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;"><span>
                                                                                                <input type="button" name="Team2" id="Teamc" value="" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="ShowMatch()" style="width: 120px;">
                                                                                            </span></td>
                                                                                            <td align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="KRate2" id="LRate3" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddMatchBitK(2)"></td>
                                                                                            <td align="center" valign="middle" style="color: ; text-align: center; vertical-align: middle;"><span style="color: ">
                                                                                                <input type="button" name="LRate2" id="KRate3" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddMatchBitL(2)">
                                                                                            </span></td>
                                                                                            <td align="center" valign="middle" style="text-align: center; vertical-align: middle;">
                                                                                                <asp:Label ID="Label1" runat="server" Style="font-size: 13px; font-weight: bold"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%  } %>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                            <div id="divSessionBhav" style="float: right; padding-top: 10px;" width="470">
                                                                                <table style="width: 670px" class="table table-striped table-hover table-bordered">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="ButtonK" style="text-align: center; vertical-align: middle; line-height: 35px; width: 150px;">SESSION</td>
                                                                                            <td class="ButtonL" style="text-align: center; vertical-align: middle; line-height: 35px; width: 80px;">NO RUN</td>
                                                                                            <td class="ButtonL" style="text-align: center; vertical-align: middle; line-height: 35px; width: 80px;">NO RATE</td>
                                                                                            <td class="ButtonK" style="text-align: center; vertical-align: middle; line-height: 35px; width: 80px;">YES RUN</td>
                                                                                            <td class="ButtonK" style="text-align: center; vertical-align: middle; line-height: 35px; width: 80px;">YES RATE</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session1" id="Session1" value="NONE"  class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not1" id="not1" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(1)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate1" id="notrate1" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes1" id="yes1" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(1)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate1" id="yesrate1" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session2" id="Session2" value="NONE"  class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not2" id="not2" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(2)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate2" id="notrate2" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes2" id="yes2" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(2)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate2" id="yesrate2" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session3" id="Session3" value="NONE" class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);" /></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not3" id="not3" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(3)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate3" id="notrate3" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes3" id="yes3" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(3)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate3" id="yesrate3" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session4" id="Session4" value="NONE" class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);" />
                                                                                            </td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not4" id="not4" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(4)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate4" id="notrate4" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes4" id="yes4" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(4)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate4" id="yesrate4" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session5" id="Session5" value="NONE" class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not5" id="not5" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(5)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate5" id="notrate5" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes5" id="yes5" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(5)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate5" id="yesrate5" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session6" id="Session6" value="NONE" class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not6" id="not6" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(6)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate6" id="notrate6" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes6" id="yes6" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(6)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate6" id="yesrate6" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session7" id="Session7" value="NONE" class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not7" id="not7" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(7)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate7" id="notrate7" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes7" id="yes7" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(7)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate7" id="yesrate7" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="35" style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="Session8" id="Session8" value="NONE" class="ButtonUserSession" style="width: 150px;" onclick="Redirect1(this.value);"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="not8" id="not8" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'" onclick="AddSessionBitN(8)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="notrate8" id="notrate8" value="0.00" class="ButtonL" onfocus="this.className='ButtonL_hover'" onblur="this.className='ButtonL'" onmouseover="this.className='ButtonL_hover'" onmouseout="this.className='ButtonL'"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yes8" id="yes8" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'" onclick="AddSessionBitY(8)"></td>
                                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                                <input type="button" name="yesrate8" id="yesrate8" value="0.00" class="ButtonK" onfocus="this.className='ButtonK_hover'" onblur="this.className='ButtonK'" onmouseover="this.className='ButtonK_hover'" onmouseout="this.className='ButtonK'"></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>

                                                                            </div>

                                                                        </td>
                                                                    </tr>


                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br>
                            <div class="alert alert-error" id="Error" style="visibility: hidden; font-weight: bold;">
                            </div>

                            <table width="100%" class="table table-striped table-hover table-bordered" id="MatchBet">
                                <tbody>
                                    <tr>
                                        <td align="left" class="TableHeading">&nbsp;</td>
                                        <td height="25" align="left" class="TableHeading"><strong>Sr.</strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong>Rate</strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong>Amount</strong></td>
                                        <td align="left" class="TableHeading"><strong>Mode</strong></td>
                                        <td align="left" class="TableHeading"><strong>Team </strong></td>
                                        <td align="left" class="TableHeading"><strong>Client</strong></td>
                                        <td align="left" class="TableHeading"><strong>User</strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong>
                                            <asp:Label ID="lblTeamA" runat="server"></asp:Label>
                                        </strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong>
                                            <asp:Label ID="lblTeamB" runat="server"></asp:Label>
                                        </strong></td>
                                        <td align="left" class="TableHeading"><strong>Date &amp; Time</strong></td>
                                    </tr> <%int i = 1; %>
                                    <% foreach (System.Data.DataRow row in ClientDataTable1.Rows)
                                        { %>
                                    <tr>
                                        <td align="center">
                                            <div class="btn-group">
                                                <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                <ul class="dropdown-menu">
                                                    <li><a href="ModifyBets.aspx?BetID=<%: row["RunnerID"] %>&&type=Match&&matchId=<%: apiID.Value %>&&fk=<%: firebasekey.Value %>"><i class="icon-pencil"></i>Edit</a></li>
                                                    <li><a onclick="DeleteBet('<%:row["RunnerID"] %>');"><i class="icon-trash"></i>Delete</a></li>
                                                </ul>
                                            </div>
                                        </td>

                                        <td height="25" align="left" class="TableHeading"><strong><%=i%></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><%: row["rate"] %></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><%: row["Amount"] %></strong></td>
                                        <td align="left" class="TableHeading"><strong><%: row["Mode"] %></strong></td>
                                        <td align="left" class="TableHeading"><strong><%: row["Team"] %> </strong></td>
                                        <td align="left" class="TableHeading"><strong><%: row["ClientID"] %>  <%: row["Name"] %></strong></td>
                                        <td align="left" class="TableHeading"><strong><%: row["AgentCreatedby"] %> / <%: row["CreatedBy"] %>  </strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><%: row["Position1"] %> </strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><%: row["Position2"] %>  </strong></td>
                                        <td align="left" class="TableHeading"><strong><%: row["Datetime"] %></strong></td>
                                    </tr>
                                     <% i++; %>
                                    <% } //foreach %>
                                    <tr>
                                        <td align="left" class="TableHeading">&nbsp;</td>
                                        <td height="25" align="left" class="TableHeading"><strong></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong></strong></td>
                                        <td align="left" class="TableHeading"><strong></strong></td>
                                        <td align="left" class="TableHeading"><strong></strong></td>
                                        <td align="left" class="TableHeading"><strong></strong></td>
                                        <td align="left" class="TableHeading"><strong>Total</strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><asp:Label ID="finalposition1" runat="server"></asp:Label>  </strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><asp:Label ID="finalposition2" runat="server"></asp:Label>   </strong></td>
                                        <td align="left" class="TableHeading"><strong></strong></td>
                                    </tr>
                                </tbody>

                            </table>


                            <!-- END FORM-->

                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE widget-->
                </div>
            </div>
            <!-- END ADVANCED TABLE widget-->
            <!-- END PAGE CONTENT-->
        </div>
        <!-- END PAGE CONTAINER-->
    </div>
    <asp:HiddenField ID="apiID" runat="server" />
    <asp:HiddenField ID="firebasekey" runat="server" />
    <script src="https://www.gstatic.com/firebasejs/5.5.0/firebase.js"></script>
    <script src="js/LiveMatch.js"></script>
    <script type="text/javascript">
         <!--


        function Redirect1(value) {
            if (value != "NONE") {
                var matchid = document.getElementById("ContentPlaceHolder_apiID").value;
                var firebasekey = document.getElementById("ContentPlaceHolder_firebasekey").value;
                var urlParams = new URLSearchParams(window.location.search);
                var matchtype = urlParams.get('Type');
                window.location = "MatchAndSessionSPosition.aspx?MatchID=" + matchid + " &&Session=" + value + "&&fk=" + firebasekey + "&&Type=" + matchtype;
            }
        }
    
         //-->
    </script>
    <script>
        function DeleteBet(RunnerID) {
            var params = {
                RunnerID: RunnerID
            };

            var formBody = [];
            for (var property in params) {
                var encodedKey = encodeURIComponent(property);
                var encodedValue = encodeURIComponent(params[property]);
                formBody.push(encodedKey + "=" + encodedValue);
            }
            formBody = formBody.join("&");

            fetch('/Admin/DeleteRunnerBets.ashx', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                },
                body: formBody
            }).then(function (responce) {
                return responce.json();
            }).then(function (data) {
                if (data.status) alert("Bet with ID: " + data.userDeletedId + " Successfully Deleted.");
                else alert("Bet Delete Failed!!!" + "\r\n" + data.error);
            }).then(function () {
                location.reload();
            }).catch(function (err) {
                console.log(err);
            });
        }
    </script>
</asp:Content>
