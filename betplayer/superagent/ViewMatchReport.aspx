<%@ Page Language="C#" MasterPageFile="~/superagent/DashBoard.Master" AutoEventWireup="true" CodeBehind="ViewMatchReport.aspx.cs" Inherits="betplayer.superagent.ViewMatchReport" %>

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

                            <div class="portlet-body">
                                <div class="clearfix">
                                </div>
                                <div class="space15"></div>

                            </div>

                            <table width="100%" border="0" cellspacing="3" cellpadding="0" class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr class="MainHeading">
                                        <td width="181" height="35" align="left" valign="middle" class="MainHeading1 textTeamHead">
                                           <strong> <asp:Label ID="lblTeamA" runat="server"></asp:Label></strong></td>
                                        <td width="193" style="text-align: right; color: #000" valign="middle" class="MainHeading1 textTeamHead"><strong><asp:Label ID="lblPositionA" runat="server"></asp:Label> </strong></td>
                                        <td width="118" align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td width="199" height="35" align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td width="398" align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td width="189" align="left" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                    </tr>
                                    <tr class="MainHeading">
                                        <td height="35" align="left" valign="middle" class="MainHeading1 textTeamHead">
                                            <strong><asp:Label ID="lblTeamB" runat="server"></asp:Label></strong></td>
                                        <td style="text-align: right; color: #000" valign="middle" class="MainHeading1 textTeamHead"><strong><asp:Label ID="lblPositionB" runat="server"></asp:Label></strong> </td>
                                        <td align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td align="center" valign="middle" class="MainHeading">&nbsp;</td>
                                        <td align="left" valign="middle" class="MainHeading"></td>
                                    </tr>
                                    <tr class="MainHeading">
                                        <td height="35" align="center" valign="middle" style="vertical-align: middle;"><strong>CLIENT</strong></td>
                                        <td align="center" valign="middle" class="MainHeading1 textTeamHead">
                                            <label for="ClientName"></label>
                                            <asp:DropDownList ID="Dropdownclient" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="Dropdownclient_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Text="All Client" Value="0"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td align="center" valign="middle" class="MainHeading1 textTeamHead">&nbsp;</td>
                                        <td align="center" valign="middle" class="MainHeading">&nbsp;</td>
                                        <td align="left" valign="middle" class="MainHeading"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <br>
                            <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td width="2%" align="center" class="TableHeadingCheckBox">&nbsp;</td>
                                        <td height="25" align="left" class="TableHeading"><strong>Sr.</strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong>Rate</strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong>Amount</strong></td>
                                        <td align="left" class="TableHeading"><strong>Mode</strong></td>
                                        <td width="60" align="left" class="TableHeading"><strong>Team </strong></td>
                                        <td align="left" class="TableHeading"><strong>Client</strong></td>
                                        <td align="left" class="TableHeading"><strong>Date &amp; Time</strong></td>
                                        <td align="left" class="TableHeading"><strong>User</strong></td>
                                        <td width="90" style="text-align: right;" class="TableHeading"><strong>
                                            <asp:Label ID="lblTeamA1" runat="server"></asp:Label>
                                        </strong></td>
                                        <td width="90" style="text-align: right;" class="TableHeading"><strong>
                                            <asp:Label ID="lblTeamB1" runat="server"></asp:Label>
                                        </strong></td>
                                    </tr>


                                     <%int i = 1; %>
                                    <% foreach (System.Data.DataRow row in ClientDataTable1.Rows)
                                        { %>
                                    <tr>
                                        <td align="center">
                                            <div class="btn-group">
                                                <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                <ul class="dropdown-menu">
                                                    <li><a href="javascript:Modify('402','2463')"><i class="icon-pencil"></i>Edit</a></li>
                                                    <li><a href="javascript:Delete('402','2463')"><i class="icon-trash"></i>Delete</a></li>
                                                </ul>
                                            </div>
                                        </td>
                                        <td height="25" align="left" class="TableHeading"><strong><%=i %></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><%:row["Rate"] %></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><%:row["Amount"] %></strong></td>
                                        <td align="left" class="TableHeading"><strong><%:row["Mode"] %></strong></td>
                                        <td width="60" align="left" class="TableHeading"><strong><%:row["Team"] %> </strong></td>
                                        <td align="left" class="TableHeading"><strong><%:row["ClientID"] %><%:row["Name"] %></strong></td>
                                        <td align="left" class="TableHeading"><strong><%:row["DateTime"] %></strong></td>
                                        <td align="left" class="TableHeading"><strong><%:row["CreatedBy"] %></strong></td>
                                        <td width="90" style="text-align: right;" class="TableHeading"><strong><%:row["Position1"] %></strong></td>
                                        <td width="90" style="text-align: right;" class="TableHeading"><strong><%:row["Position2"] %></strong></td>
                                    </tr>
                                     <% i++; %>
                                    <% } //foreach %>


                                    <tr>
                                        <td height="25" colspan="7" style="text-align: right;" class="TableHeading">&nbsp;</td>
                                        <td style="text-align: right;" class="TableHeading">TOTAL AMOUNT</td>
                                        <td style="text-align: right;" class="TableHeading">&nbsp;</td>
                                        
                                        <td style="text-align: right;" class="TableHeading"><strong><asp:Label ID="lblTotalPosition1" runat="server"></asp:Label></strong></td>
                                        <td style="text-align: right;" class="TableHeading"><strong><asp:Label ID="lblTotalPosition2" runat="server"></asp:Label></strong></td>
                                    </tr>
                                </tbody>
                            </table>


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
