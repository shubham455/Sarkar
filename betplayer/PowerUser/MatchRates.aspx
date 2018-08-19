﻿<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="MatchRates.aspx.cs" Inherits="betplayer.poweruser.MatchRates" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="main-content">
        <div class="container-fluid">
            <!-- BEGIN PAGE HEADER-->
            <div class="row-fluid">
                <div class="span12">
                    <!-- BEGIN THEME CUSTOMIZER-->
                    <div id="theme-change" class="hidden-phone"><i class="icon-cogs"></i><span class="settings"><span class="text">Theme:</span> <span class="colors"><span class="color-default" data-style="default"></span><span class="color-gray" data-style="gray"></span><span class="color-purple" data-style="purple"></span><span class="color-navy-blue" data-style="navy-blue"></span></span></span></div>
                    <!-- END THEME CUSTOMIZER-->
                    <!-- BEGIN PAGE TITLE & BREADCRUMB-->
                    <h3 class="page-title">Add Match</h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>

                        <li><a href="#">Add Match</a><span class="divider">&nbsp;</span></li>

                        <li><a href="CreateMatch.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
                    </ul>
                    <!-- END PAGE TITLE & BREADCRUMB-->
                </div>
            </div>
            <!-- END PAGE HEADER-->
            <!-- BEGIN PAGE CONTENT-->
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-globe"></i>Modify Match</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <table>
                                <tbody>
                                    <tr>
                                        <td width="2%" class="welcome">&nbsp;</td>
                                        <td style="font-size: large; font: 100" width="10%" height="30" align="left" valign="middle" class="welcome">
                                            <select name="ctl00$ContentPlaceHolder$DropStatus" id="team_selector" class="btn btn-primary dropdown-toggle" style="width: 180px; height: 50px; font-size: large">
                                                <option id="team1_name" style="font-size: large" value="team_1">Team1</option>
                                                <option id="team2_name" style="font-size: large" value="team_2">Team2</option>
                                            </select>
                                        <td width="5%" align="left" valign="middle"></td>
                                        <td width="14%" align="left" valign="middle">
                                            <input id="team_Lagai" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="lagaiKeyPress(event)" type="text" style="width: 100px; height: 50px; font-size: xx-large; font-weight: bold" name="team1khai" value="" placeholder="Lagai">
                                        </td>
                                        <td width="14%" align="left" valign="middle">
                                            <input id="team_Khai" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="khaiKeyPress(event)" type="text" name="team1lagai" style="width: 100px; height: 50px; font-size: xx-large; font-weight: bold" value="" placeholder="Khai">
                                        </td>

                                        <td width="14%" align="left" valign="middle">
                                            <button id="btnteamupdate" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                        </td>
                                        <td width="14%" align="left" valign="middle">
                                            <button id="btnLock" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Lock</button>
                                        </td>
                                        <td width="14%" align="left" valign="middle">
                                            <button id="btnUnLock" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">UnLock</button>
                                        </td>
                                        <td width="24%" align="left" valign="middle">&nbsp;</td>
                                        <td width="24%" align="left" valign="middle">&nbsp;</td>
                                        <td width="2%" align="left">&nbsp;</td>
                                    </tr>

                                    <br />
                                    <br />




                                </tbody>
                            </table>
                            <div class="widget-body form">
                                <table>
                                    <tr>
                                        <td>
                                            <button id="ball_start" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">Ball Start</button>
                                        </td>
                                        <td>
                                            <button id="1run" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">1 Run</button>
                                        </td>
                                        <td>
                                            <button id="2run" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">2 Run</button>
                                        </td>
                                        <td>
                                            <button id="3run" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">3 Run</button>
                                        </td>
                                        <td>
                                            <button id="four" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">4 Run</button>
                                        </td>
                                        <td>
                                            <button id="six" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">6 Run</button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="widget-body form">
                                <table>
                                    <tr>
                                        <td>
                                            <button id="wide" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">WIDE</button>
                                        </td>
                                        <td>
                                            <button id="noball" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">NO BALL</button>
                                        </td>
                                        <td>
                                            <button id="freehit" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">FREE HIT</button>
                                        </td>
                                        <td>
                                            <button id="wideplus4" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">WIDE+ 4</button>
                                        </td>
                                        <td>
                                            <button id="timeout" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">Time out</button>
                                        </td>
                                        <td>
                                            <button id="thirdumpire" type="button" class="btn btn-success" style="height: 70px; width: 120px; font-size: x-large; font-weight: bold; color: black">Third Umpire</button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="widget-body form">
                                <table>
                                    <tr>
                                        <td>
                                            <select name="ctl00$ContentPlaceHolder$DropStatus" runat="server" id="team_selector1" class="btn btn-primary dropdown-toggle" style="width: 180px; height: 50px; font-size: large">
                                                <option id="team1_name1" style="font-size: large" value="team_1">Team1</option>
                                                <option id="team2_name2" style="font-size: large" value="team_2">Team2</option>
                                            </select>

                                        </td>
                                        <td style="margin-left: 30px;"></td>
                                        <td>
                                            <asp:button id="btnDeclare" runat="server"  onclick="btnDeclare_ServerClick" OnClientClick="Confirm()" Text="Declare" class="btn btn-success" style="height: 50px; width: 120px; font-size: x-large; font-weight: bold; color: black" />
                                        </td>
                                        <td style="margin-left: 30px;"></td>
                                        <td>
                                            <asp:button id="btnnDeclare" class="btn btn-success" runat="server" onclick="btnUnDeclare_ServerClick"  OnClientClick="ConfirmUnDeclare()"  Text="UnDeclare" style="height: 50px; width: 140px; font-size: x-large; font-weight: bold; color: black" />
                                        </td>

                                    </tr>
                                </table>
                            </div>
                            <div class="widget-body form">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td width="25%" align="left" valign="middle">
                                               <span>Runner Min/Max: </span> 
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="minBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="minBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" placeholder="Min Bet">
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="maxBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="maxBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" placeholder="Max Bet">
                                            </td>

                                            <td width="15%" align="left" valign="middle">
                                                <button id="btnminmaxUpdate" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left" valign="middle">
                                               <span>Session Min/Max: </span> 
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="sessionMinBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="sessionMinBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" placeholder="Min Bet">
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="sessionMaxBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="sessionMaxBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" placeholder="Max Bet">
                                            </td>

                                            <td width="15%" align="left" valign="middle">
                                                <button id="btnsessionMinMaxUpdate" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <asp:HiddenField ID="firebasekey" runat="server" />
    <script src="https://www.gstatic.com/firebasejs/4.13.0/firebase.js"></script>
    <script src="js/RunnerUpdate.js"></script>
     <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Declare this Match?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
         }
          function ConfirmUnDeclare() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to UnDeclare this Match?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
