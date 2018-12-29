<%@ Page Language="C#" MasterPageFile="~/superagent/DashBoard.Master" AutoEventWireup="true" CodeBehind="ViewSessionReport.aspx.cs" Inherits="betplayer.superagent.ViewSessionReport" %>

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
                    <h3 class="page-title">Match Bet Details
  <small></small></h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Sport's Betting <span class="divider">&nbsp;</span> </li>
                        <li>Match &amp; Session Plus Minus Selection<span class="divider">&nbsp;</span></li>
                        <li><a href="SportDetails.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <div class="portlet-body">
                                <div class="clearfix">
                                </div>
                                <div class="space15"></div>

                            </div>
                            <table width="100%" border="0" cellspacing="2" cellpadding="2" class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr class="MainHeading">
                                        <td width="103" height="35" align="left" valign="middle" class="MainHeading1 textTeamHead"><strong>CLIENT</strong></td>
                                        <td width="220" align="left" valign="middle" class="MainHeading1 textTeamHead">
                                            <asp:DropDownList ID="DropDownClient" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Text="All Client" Selected="True"></asp:ListItem>
                                            </asp:DropDownList></td>

                                        <td width="100" height="35" align="left" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td width="121" height="35" align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td width="174" align="left" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                    </tr>
                                    <tr class="MainHeading">
                                        <td height="35" align="left" valign="middle" class="MainHeading1 textTeamHead"><strong>SESSION</strong></td>
                                        <td align="left" valign="middle" class="MainHeading1">
                                            <asp:DropDownList ID="DropDownSession" runat="server"></asp:DropDownList>
                                        </td>
                                        <td align="center" valign="middle" style="text-align: center">
                                            <asp:Button ID="btnview" runat="server" class="btn btn-success" OnClick="btnview_Click" Text="View" />
                                        </td>
                                        <td width="17" align="center" valign="middle" class="MainHeading1 textTeamHead" style="color: #F00"><strong>
                                            <asp:Label ID="lblrate" runat="server"></asp:Label></strong></td>

                                    </tr>
                                </tbody>
                            </table>
                            <br>

                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                <tbody>
                                                    <tr>
                                                        <td width="2%" align="center" class="TableHeadingCheckBox">&nbsp;</td>
                                                        <td height="25" align="left" class="TableHeading">Sr.</td>
                                                        <td align="left" class="TableHeading">Session </td>
                                                        <td align="left" class="TableHeading">Client</td>
                                                        <td align="right" class="TableHeading">Rate</td>
                                                        <td align="right" class="TableHeading">Amount</td>
                                                        <td align="right" class="TableHeading">Runs</td>
                                                        <td align="center" class="TableHeading">Mode</td>
                                                        <td align="right" class="TableHeading">No</td>
                                                        <td align="right" class="TableHeading">Yes</td>
                                                        <td align="left" class="TableHeading">Date &amp; Time</td>
                                                        <td align="left" class="TableHeading">User Name</td>
                                                    </tr>
                                                     <%int i = 1; %>
                                                    <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                                        { %>
                                                    <tr bgcolor="#FFFFFF">
                                                        <td align="center">
                                                            <div class="btn-group">
                                                                <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                                <ul class="dropdown-menu">
                                                                    <li><a href="ModifySessionBet.php?id=3467&amp;MT=402" target="_blank"><i class="icon-pencil"></i>Modify</a></li>
                                                                    <li><a href="DeleteSessionBet.php?id=3467&amp;MT=402" target="_blank"><i class="icon-pencil"></i>Delete</a></li>
                                                                </ul>
                                                            </div>
                                                        </td>


                                                        <td align="left" class="TableHeading"><%=i %></td>
                                                        <td height="25" align="left" class="TableHeading"><strong><%: row["Session"] %></strong></td>
                                                        <td align="left" class="TableHeading"><strong><%: row["ClientID"] %> <%: row["Name"] %></strong></td>
                                                        <td align="left" class="TableHeading"><strong><%: row["rate"] %></strong></td>
                                                        <td align="right" class="TableHeading" style="text-align: right;"><strong><%: row["Amount"] %></strong></td>
                                                        <td align="right" class="TableHeading" style="text-align: right;"><strong><%: row["Runs"] %></strong></td>
                                                        <td align="right" class="TableHeading" style="text-align: right;"><strong><%: row["Mode"] %></strong></td>
                                                        <td align="center" class="TableHeading"><strong>No</strong></td>
                                                        <td align="right" class="TableHeading" style="text-align: right;"><strong>Yes</strong></td>

                                                        <td align="left" class="TableHeading"><strong><%: row["DateTime"] %></strong></td>
                                                        <td align="left" class="TableHeading"><strong><%: row["CreatedBy"] %></strong></td>
                                                    </tr>
                                                     <% i++; %>
                                                    <% } //foreach %>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td width="10" valign="top">&nbsp;</td>
                                        <td width="200" valign="top">
                                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="2" class="table table-striped table-hover table-bordered">
                                                <tbody>
                                                    <tr>
                                                        <td width="33%" height="25" align="left" class="TableHeading">RUNS</td>
                                                        <td width="67%" align="right" class="TableHeading">AMOUNT</td>
                                                    </tr>
                                                    <% foreach (System.Data.DataRow row in runTable1.Rows)

                                                        { %>
                                                    <tr>
                                                        <td width="50" height="25" align="right" style="text-align: right;"><strong><%:row["Runs"] %></strong></td>
                                                        <td width="100" align="right" style="text-align: right;"><strong><%:row["Amount"] %></strong></td>
                                                    </tr>
                                                    <% } //foreach %>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br>
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
