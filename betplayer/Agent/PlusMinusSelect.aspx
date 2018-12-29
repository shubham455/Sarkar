<%@ Page Language="C#" MasterPageFile="~/agent/DashBoard.Master" AutoEventWireup="true" CodeBehind="PlusMinusSelect.aspx.cs" Inherits="betplayer.agent.PlusMinusSelect" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

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
                    <h3 class="page-title">Match &amp; Session Plus Minus Report Selection
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
                            <table class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td height="25" width="70%" style="text-align: center; vertical-align: middle;"><asp:Label ID="lblTeamA" runat="server"></asp:Label> V <asp:Label ID="lblTeamB" runat="server"></asp:Label>   </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br>
                            <table width="1000" class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td width="151" height="25" style="vertical-align: middle;" class="FontText"><strong>REPORT TYPE</strong></td>
                                        <td width="140" align="left" valign="middle">
                                            <label for="ReportType"></label>
                                            <asp:DropDownList ID="Dropdownselect" runat="server">
                                                <asp:ListItem  Text="Match" Value="1"></asp:ListItem>
                                                <asp:ListItem  Text="Session" Value="2"></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Both" Value="3"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td width="584" align="left" style="vertical-align: middle;">&nbsp;
		                	                  <asp:Button ID="btnshow" runat="server" class="btn btn-success" Text="Show" OnClick="btnshow_Click" />
                                        </td>
                                        <td width="125" align="left" valign="middle">&nbsp;</td>
                                    </tr>
                                </tbody>
                            </table>
                            <br>
                            <br>
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
    <asp:HiddenField ID="apiId" runat="server" />
</asp:Content>
