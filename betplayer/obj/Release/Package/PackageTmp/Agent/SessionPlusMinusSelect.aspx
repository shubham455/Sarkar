<%@ Page Language="C#" MasterPageFile="~/agent/DashBoard.Master" AutoEventWireup="true" CodeBehind="SessionPlusMinusSelect.aspx.cs" Inherits="betplayer.agent.SessionPlusMinus" %>

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
                    <h3 class="page-title">Match &amp; Session Plus Minus Selection
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
                                                <strong>AGENT &amp; CLIENT OF <asp:Label ID="lblTeamA" runat="server"></asp:Label> V <asp:Label ID="lblTeamB" runat="server"></asp:Label>  </strong></td>
                                            <td height="25" style="text-align: center; vertical-align: middle;">
                                                <button type="button" class="btn btn-success" onclick="ShowReport()">Show</button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br>

                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            
                                            <td valign="top">
                                                <table align="left" class="table table-striped table-hover table-bordered">
                                                    <tbody>
                                                        <tr>
                                                            <th width="33%" height="25" colspan="2" align="center" class="TableHeadingCheckBox">

                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-381_S_S">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="381_S_S" id="381_S_S" onclick="SelectDeselectAllSession('381_S_S')" value="381_S_S" style="opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="25" align="left" class="TableHeading">SESSION</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </th>
                                                            <th width="33%" colspan="2" align="center">

                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width="20" align="center" class="TableHeadingCheckBox">
                                                                                <div class="checker" id="uniform-381_S_A">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="381_S_A" id="381_S_A" onclick="SelectDeselectAllAgent('381_S_A')" value="381_S_A" style="opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td align="left" class="TableHeading">AGENT</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </th>
                                                            <th width="33%" colspan="2" align="center">

                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width="20" align="center" class="TableHeadingCheckBox">
                                                                                <div class="checker" id="uniform-381_S_C">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="381_S_C" id="381_S_C" onclick="SelectDeselectAllClient('381_S_C')" value="381_S_C" style="opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td align="left" class="TableHeading">CLIENT </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </th>
                                                        </tr>
                                                    </tbody>
                                                    <tbody>
                                                        <tr>
                                                            <td height="25" colspan="2" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">

                                                                    <tbody>
                                                                        <% foreach (System.Data.DataRow row in MatchesDataTable3.Rows)
                                                                            { %>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-381_S_S1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="381_S_S1" id="381_S_S1" value="06 over-Bangladesh 05-06" style="opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText"><%:row["Session"] %></td>
                                                                        </tr>
                                                                        <% } //foreach %>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="381_S_S_ALL" id="381_S_S_ALL" value="5">
                                                            </td>
                                                            <td colspan="2" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-381_S_A1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="381_S_A1" id="381_S_A1" value="BANTU" style="opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText">
                                                                                <asp:Label ID="lblAgentNAme1" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="381_S_A_ALL" id="381_S_A_ALL" value="1">
                                                            </td>
                                                            <td colspan="2" align="center" valign="top">

                                                                <table width="100%" border="0" cellspacing="2" cellpadding="0" class="table table-striped table-hover table-bordered">
                                                                    <tbody>
                                                                        <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                                                            { %>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td width="20" align="center">
                                                                                <div class="checker" id="uniform-381_S_C1">
                                                                                    <span class="checked">
                                                                                        <input type="checkbox" checked="" name="381_S_C1" id="381_S_C1" value="528 BHAGWAN" style="opacity: 0;"></span>
                                                                                </div>
                                                                            </td>
                                                                            <td height="20" align="left" class="FontText"><%:row["Name"] %></td>
                                                                        </tr>
                                                                        <% } //foreach %>
                                                                    </tbody>
                                                                </table>
                                                                <input type="hidden" name="381_S_C_ALL" id="381_S_C_ALL" value="1">
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <br>
                                <br>
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
    <asp:HiddenField ID="apiID" runat="server" />
    <script>
        function ShowReport() {
            var matchid = document.getElementById("ContentPlaceHolder_apiID").value;
            window.location = "SessionPlusMinusDisplay.aspx?MatchID=" + matchid;
        }
    </script>
</asp:Content>
