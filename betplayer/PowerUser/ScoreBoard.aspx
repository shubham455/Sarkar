<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="ScoreBoard.aspx.cs" Inherits="betplayer.poweruser.ScoreBoard" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="main-content">
        <div class="container-fluid">

            <div class="row-fluid">
                <div class="span12">

                    <div id="theme-change" class="hidden-phone"><i class="icon-cogs"></i><span class="settings"><span class="text">Theme:</span> <span class="colors"><span class="color-default" data-style="default"></span><span class="color-gray" data-style="gray"></span><span class="color-purple" data-style="purple"></span><span class="color-navy-blue" data-style="navy-blue"></span></span></span></div>

                    <h3 class="page-title">Scoreboard</h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>

                        <li><a href="#">Modify Matches</a><span class="divider">&nbsp;</span></li>

                        <li><a href="ModifyMatches.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
                    </ul>
                </div>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-globe"></i>Scoreboard</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <div class="main-container">
                                <div class="match-container">
                                    <div class="lastball-container">
                                        <button id="ball_start" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Ball Start</button>
                                        <button id="bet_open" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Bet Open</button>
                                        <button id="bet_closed" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Bet Closed</button>
                                        <button id="NoRun" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">No Run</button>
                                        <button id="1run" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">1 Run</button>
                                        <button id="2run" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">2 Run</button>
                                        <button id="3run" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">3 Run</button>
                                        <button id="four" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">FOUR</button>
                                        <button id="six" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">SIX</button>
                                        <button id="out" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">OUT</button>
                                        <button id="wide" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">WIDE</button>
                                        <button id="noball" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">NO BALL</button>
                                        <button id="freehit" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">FREE HIT</button>
                                        <button id="wideplus4" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Wide4</button>
                                        <button id="timeout" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Timeout</button>
                                        <button id="thirdumpire" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Third Umpire</button>
                                        <button id="Review" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">Review</button>
                                        <button id="Inningsbreak" type="button" class="btn btn-success" style="font-size: x-large; font-weight: bold; color: black">InningsBreak</button>
                                    </div>
                                    <div class="runner-container">
                                        <asp:DropDownList Style="margin: 0;" ID="matchAutoManualSystem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="matchAutoManualSystem_SelectedIndexChanged1">
                                            <asp:ListItem Text="Automatic" Value="0" />
                                            <asp:ListItem Text="Manual" Value="1" />
                                        </asp:DropDownList>
                                        <select name="ctl00$ContentPlaceHolder$DropStatus" id="team_selector" class="btn btn-primary dropdown-toggle">
                                            <option id="team1_name" value="team_1">Team1</option>
                                            <option id="team2_name" value="team_2">Team2</option>
                                        </select>
                                        <button style="margin: 1vh 0; font-size: large;" id="btnclearrate" type="button" class="btn btn-success">Clear Rates</button>
                                        <button style="margin: 1vh 0; font-size: large;" id="btnUnclearrate" type="button" class="btn btn-success">UnClear Rates</button>
                                    </div>
                                    <div class="score-container">
                                        <p style="font-size: large;">Score Board</p>
                                        <input style="width: 150px;" id="InputRun" type="text" name="Runs" value="" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="focusNextElementOnEnterKeyPress(event)" placeholder="Run" />
                                        <input style="width: 150px;" id="InputWicket" type="text" name="Wickets" value="" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="focusNextElementOnEnterKeyPress(event)" placeholder="Wicket" />
                                        <input style="width: 150px;" id="InputOver" type="text" name="Overs" value="" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="focusNextElementOnEnterKeyPress(event)" placeholder="Over" />
                                    </div>
                                    <div class="message-container">
                                        <p style="font-size: large;" class="welcome">Message</p>
                                        <textarea id="InputMessage" name="Message" style="width: 500px;"></textarea>
                                        <button id="btnmessage" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="firebasekey" runat="server" />
    <script src="https://www.gstatic.com/firebasejs/4.13.0/firebase.js"></script>
    <script src="js/ManualMatch_v2.5.js"></script>
    <script lang="javascript">

        function Tab() {
            var tb1 = document.getElementById(InputWicket);
            tb1.focus();
        }

    </script>
    <script src="jquery.js" type="text/javascript"></script>


    <script type="text/javascript" lang="js">
        function focusOnNext(e, nextControl) {
            alert(e.keyCode);
            if (e.keyCode == 13) {
                $("." + nextControl).focus();
                return false;
            }
        }
    </script>



</asp:Content>
