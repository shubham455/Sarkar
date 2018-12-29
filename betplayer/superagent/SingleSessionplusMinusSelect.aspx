<%@ Page Language="C#" MasterPageFile="~/superagent/DashBoard.Master" AutoEventWireup="true" CodeBehind="SingleSessionplusMinusSelect.aspx.cs" Inherits="betplayer.superagent.SingleSessionplusMinusSelect" %>

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
                    <h3 class="page-title">Session Plus Minus Report Selection
  <small></small></h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Sport's Betting <span class="divider">&nbsp;</span> </li>
                        <li>Session Plus Minus Selection<span class="divider">&nbsp;</span></li>
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



                                <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br>
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td height="25" width="70%" style="text-align: center; vertical-align: middle;">
                                                <strong>AGENT &amp; CLIENT OF
                                                    <asp:Label ID="lblTeamA" runat="server"></asp:Label>V
                                                    <asp:Label ID="lblTeamB" runat="server"></asp:Label>
                                                </strong></td>
                                            <td height="25" style="text-align: center; vertical-align: middle;">
                                                <asp:button id="btnshow" runat="server" class="btn btn-success" onclick="btnshow_Click" Text="Show" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br>

                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td valign="top">&nbsp;</td>
                                            <td valign="top">
                                                <table align="left" class="table table-striped table-hover table-bordered">
                                                    <tbody>
                                                    </tbody>
                                                    <tbody>
                                                        <tr>
                                                            <td width="25%" height="25" colspan="2" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div class="checker" id="uniform-ChkSession">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="ChkSession" id="ChkSession" onclick="SelectDeselectAllSession()" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="25" align="left" class="TableHeading"><strong>SESSION</strong></td>
                                                                        </tr>
                                                                        <% foreach (System.Data.DataRow row in SessionDatatable.Rows)
                                                                            { %>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-402_S1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox"  onclick="redirect(this.value)"  id="chksession" value="<%:row["Session"] %>" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText"><%:row["Session"] %></td>
                                                                        </tr>

                                                                        <% } //foreach %>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="402_S_S_ALL" id="402_S_S_ALL" value="8">
                                                            </td>
                                                            <td width="25%" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center" class="TableHeadingCheckBox">
                                                                                <div class="checker" id="uniform-ChkSuperAgent">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="ChkSuperAgent" id="ChkSuperAgent" onclick="SelectDeselectAllSuperAgent()" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td align="left" class="TableHeading"><strong>SUPER AGENT</strong></td>
                                                                        </tr>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-402_SA1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="402_SA1" id="402_SA1" value="one two gwalior" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText">
                                                                                <asp:Label ID="lblname" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="402_SA_ALL" id="402_SA_ALL" value="1">
                                                            </td>
                                                            <td width="25%" colspan="2" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center" class="TableHeadingCheckBox">
                                                                                <div class="checker" id="uniform-ChkAgent">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="ChkAgent" id="ChkAgent" onclick="SelectDeselectAllAgent()" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td align="left" class="TableHeading"><strong>AGENT</strong></td>
                                                                        </tr>
                                                                        <% foreach (System.Data.DataRow row in AgentDatatable.Rows)
                                                                            { %>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-402_A1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="402_A1" id="402_A1" value="Adi" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText"><%:row["Name"] %></td>
                                                                        </tr>
                                                                        <% } //foreach %>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="402_A_ALL" id="402_A_ALL" value="5">
                                                            </td>
                                                            <td width="25%" colspan="2" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center" class="TableHeadingCheckBox">
                                                                                <div class="checker" id="uniform-ChkClient">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="ChkClient" id="ChkClient" onclick="SelectDeselectAllClient()" style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td align="left" class="TableHeading"><strong>CLIENT</strong></td>
                                                                        </tr>
                                                                        <% foreach (System.Data.DataRow row in ClientDatatable.Rows)
                                                                            { %>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-402_C1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="402_C1" id="402_C1" value="1533 Shetty " style="width: 25px; opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText"><%:row["Name"] %> </td>
                                                                        </tr>

                                                                        <% } //foreach %>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="402_C_ALL" id="402_C_ALL" value="56">
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </td>
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
    <script>
        function redirect(sessionname) {
            var urlParams = new URLSearchParams(window.location.search);
            var matchID = urlParams.get('MatchID')
            window.location.href = "SingleSessionplusMinusDisplay.aspx?SessionName=" + sessionname + "&MatchID=" + matchID;
        }
    </script>
</asp:Content>
