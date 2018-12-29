<%@ Page Language="C#" MasterPageFile="~/Agent/DashBoard.Master" AutoEventWireup="true" CodeBehind="SportsDetails.aspx.cs" Inherits="betplayer.Agent.SportsDetails" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                    <h3 class="page-title">Sports Details  <small>Display Game Details</small> </h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Sport's Betting<span class="divider">&nbsp;</span> </li>
                        <li>Game Details<span class="divider">&nbsp;</span></li>
                        <li><a href="Sportsbetting.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                        <div class="widget-title">
                            <h4><i class="icon-globe"></i>Game Details</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->


                            <table width="100%" class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td width="20" align="center" class="TableHeadingCheckBox">&nbsp;</td>
                                        <td width="30" height="25" align="left" class="TableHeading">SNo.</td>
                                        <td width="40" align="left" class="TableHeading">Code</td>
                                        <td align="left" class="TableHeading">Name</td>
                                        <td width="70" align="left" class="TableHeading">Date</td>
                                        <td width="110" align="left" class="TableHeading">Time</td>
                                        <td width="110" align="left" class="TableHeading">Match Type </td>
                                        <td width="100" align="left" class="TableHeading">Declare</td>
                                        <td width="92" align="left" class="TableHeading">Won By</td>
                                    </tr>
                                    <%int i = 1; %>
                                    <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                        { %>
                                    <tr style="background-color: #FFFFFF">
                                        <td align="center">
                                            <div class="btn-group">
                                                <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                <ul class="dropdown-menu">
                                                    <li><a href="<%if (Convert.ToInt16(row["Declear"]) == 1) {%>#<% }else{%>MatchAndSessionPosition.aspx?MatchID=<%: row["apiID"] %>&fk=<%: row["firebasekey"] %>&Type=<%: row["Type"] %><% } %>"><i class="icon-film"></i>Match &amp; Session Position</a></li>
                                                     <li><a href="PlusMinusSelect.aspx?MatchID=<%: row["apiID"] %>"><i class="icon-filter"></i>Match &amp; Session Plus Minus</a></li>
                                                    <li><a href="ViewMatchReport.aspx?MatchID=<%: row["apiID"] %>"><i class="icon-pushpin"></i>Display Match Bets</a></li>
                                                    <li><a href="ViewSessionReport.aspx?MatchID=<%: row["apiID"] %>"><i class="icon-bullhorn"></i>Display Session Bets</a></li>

                                                </ul>
                                            </div>
                                        </td>

                                        <td height="20" align="left" class="FontText"><%=i %></td>
                                        <td align="left" class="FontText"><%: row["MatchesID"] %></td>
                                        <td align="left" class="FontText"><%: row["TeamA"] %> v <%: row["TeamB"] %>(<%: row["Type"] %>)</td>
                                        <td align="left" class="FontText"><%: toDateString(row["DateTime"])%></td>
                                        <td width="110" align="left" class="FontText"><%: toTime(row["DateTime"]) %></td>
                                        <td width="110" align="left" class="FontText"><%: row["Type"] %></td>
                                        <td width="100" align="left" class="FontText"><%if (Convert.ToInt16(row["Declear"]) == 1) {%>Yes<% }else{%>No<% } %></td>
                                        <td width="92" align="left" class="FontText"><%: row["WinnerTeam"] %></td>
                                    </tr>
                                    <%i++; %>
                                    <% } //foreach %>
                                    



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
</asp:Content>
