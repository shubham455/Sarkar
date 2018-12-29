<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="ManuallyUpdation.aspx.cs" Inherits="betplayer.poweruser.ManuallyUpdation" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="main-content">
        <!-- BEGIN PAGE CONTAINER-->
        <div class="container-fluid">
            <!-- BEGIN PAGE HEADER-->
            <div class="row-fluid">
                <div class="span12">
                    <!-- BEGIN THEME CUSTOMIZER-->
                    <!-- END THEME CUSTOMIZER-->
                    <!-- BEGIN PAGE TITLE & BREADCRUMB-->
                    <h3 class="page-title">Sport's Betting</h3>
                    <ul class="breadcrumb">
                        <li>
                            <a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span>
                        </li>
                        <li><a href="ModifyMatches.aspx">Modify Matches</a><span class="divider">&nbsp;</span></li>
                        <li><a href="Dashboard.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
                        <li class="pull-right search-wrap"></li>
                    </ul>
                    <!-- END PAGE TITLE & BREADCRUMB-->
                </div>
            </div>
            <!-- END PAGE HEADER-->
            <!-- BEGIN PAGE CONTENT-->
            <table>
                <tr>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="ScoreBoard.aspx?Matchid=<%=apiID%>&fk=<%=firebasekey %>">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">ScoeBoard</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="MatchRates.aspx?Matchid=<%=apiID%>&fk=<%=firebasekey%>">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">Match Rates</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="SessionRate.aspx?Matchid=<%=apiID%>&fk=<%=firebasekey%>">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">Session Rates</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="FancySession.aspx?Matchid=<%=apiID%>&fk=<%=firebasekey%>">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">Fancy Session</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="SessionRateAutomatic.aspx?Matchid=<%=apiID%>&fk=<%=firebasekey%>">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">Session Rates Automatic</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                     <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="MinMax.aspx?Matchid=<%=apiID%>&fk=<%=firebasekey%>">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">Min/Max</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="sendmatchdetails.aspx">
                                        <div class="stats-circle turquoise-color">
                                            <i class="icon-user"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #4CC5CD;">Sechdule</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="row-fluid circle-state-overview">
                            <div style="float: left; padding-left: 20px; padding-right: 20px;">
                                <div class="circle-wrap">
                                    <a href="Dashboard.aspx">
                                        <div class="stats-circle gray-color">
                                            <i class="icon-comments-alt"></i>
                                        </div>
                                        <strong>
                                            <p style="color: #B9BABA;">Back</p>
                                        </strong>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

