<%@ Page Language="C#" MasterPageFile="~/agent/DashBoard.Master" AutoEventWireup="true" CodeBehind="MatchPlusMinusDisplay.aspx.cs" Inherits="betplayer.agent.MatchPlusMinusDisplay" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                    <h3 class="page-title">Match &amp; Session Plus Minus Report
  <small></small></h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Sport's Betting <span class="divider">&nbsp;</span> </li>
                        <li>Match &amp; Session Plus Minus Report<span class="divider">&nbsp;</span></li>
                        <li><a href="MatchSessionPlusMinusSelect.php?MT=381"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                                <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="2">
                                                    <tbody>
                                                        <tr>
                                                            <td width="400" align="left" valign="top">
                                                                <table width="99%" border="0" align="left" cellpadding="0" cellspacing="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width="75" height="25" align="left" valign="middle"><strong>AGENT</strong></td>
                                                                            <td width="630" align="left" valign="middle"><strong>
                                                                                <asp:Label ID="lblAgentName" runat="server"></asp:Label></strong></td>
                                                                            <td width="170" align="left" valign="middle">
                                                                                <input type="hidden" name="Match" id="Match" value="381">&nbsp;</td>
                                                                            <td width="125" align="left" valign="middle">&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="25" colspan="4" align="left" valign="middle">

                                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" class="table table-striped table-hover table-bordered">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td height="25" align="left" valign="middle">&nbsp;</td>
                                                                                            <td colspan="3" align="center" valign="middle" style="text-align: center; vertical-align: middle;"><strong>PLUS MINUS</strong></td>
                                                                                            <td colspan="3" align="center" valign="middle" style="text-align: center; vertical-align: middle;"><strong>COMMISSION</strong></td>
                                                                                            <td colspan="4" align="center" valign="middle" style="text-align: center; vertical-align: middle;"><strong>OTHERS</strong></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width:20%" height="25"  align="left" valign="middle"><strong>CLIENT NAME</strong></td>
                                                                                            <td width="70" align="right" valign="middle" style="text-align: right;"><strong>MATCH</strong></td>
                                                                                            <td width="70" align="right" valign="middle" style="text-align: right;"><strong>SESSION</strong></td>
                                                                                            <td width="70" align="right" valign="middle" style="text-align: right;"><strong>TOTAL</strong></td>
                                                                                            <td width="50" align="right" style="text-align: right;" valign="middle"><strong>MATCH</strong></td>
                                                                                            <td width="50" align="right" style="text-align: right;" valign="middle"><strong>SESSION</strong></td>
                                                                                            <td width="50" align="right" style="text-align: right;" valign="middle"><strong>TOTAL</strong></td>
                                                                                            <td width="70" align="right" style="text-align: right;" valign="middle"><strong>NET AMT</strong></td>
                                                                                            <td width="70" align="right" style="text-align: right;" valign="middle"><strong>SHR %</strong></td>
                                                                                            <td width="70" align="right" style="text-align: right;" valign="middle"><strong>SHR AMT</strong></td>
                                                                                            <td width="70" align="right" style="text-align: right;" valign="middle"><strong>MOB.APP</strong></td>
                                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>FINAL</strong></td>
                                                                                        </tr>

                                                                                        <% foreach (System.Data.DataRow row in runTable.Rows)
                                                                                            { %>
                                                                                        <tr>
                                                                                            <td height="25" align="left" valign="middle" class="FontText"><%:row["Name"] %></td>

                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["MatchAmount"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["SessionAmount"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><strong><%:row["TotalAmount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["MatchCommision"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["SessionCommision"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><strong><%:row["TotalCommisionAmount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><strong><%:row["TotalNetAmount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["ClientShare"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["TotalHalfAmount"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><%:row["MOBAppAmount"] %></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle" class="FontText"><strong><%:row["FinalAmount"] %></strong></td>
                                                                                        </tr>
                                                                                        <% } //foreach %>
                                                                                        <tr>
                                                                                            <% foreach (System.Data.DataRow row in runTable1.Rows)
                                                                                                { %>

                                                                                            <td height="25" align="left" valign="middle"><strong>TOTAL</strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["MatchAmount"] %> </strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["SessionAmount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalPlusMinusAmount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["Match1Amount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["Session1Amount"] %> </strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalCommisionAmount"] %> </strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalNetAmount"] %> </strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong> </strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalHalfAmount"] %> </strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalAppAmount"] %></strong></td>
                                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalFinalAmount"] %>  </strong></td>

                                                                                            <% } //foreach %>
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

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br>

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
    </div>
</asp:Content>