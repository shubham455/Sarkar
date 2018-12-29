<%@ Page Language="C#" MasterPageFile="~/superagent/DashBoard.Master" AutoEventWireup="true" CodeBehind="CompanyMatchSessionPlusMinusDisplay.aspx.cs" Inherits="betplayer.superagent.MatchSessionPlusMinusDisplay" %>

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
                                <table width="99%" border="0" align="left" cellpadding="0" cellspacing="0" class="table table-striped table-bordered">
                                    <tbody>
                                        <tr>
                                            <td width="11%" height="25" align="left" valign="middle"><strong>SUPER AGENT</strong></td>
                                            <td width="89%" align="left" valign="middle"><strong>
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </strong></td>
                                        </tr>
                                        <tr>
                                            <td height="25" colspan="2" align="left" valign="middle">


                                                <% int index = 0, totalindex = 0; foreach (System.Data.DataRow row1 in agentname.Rows)
                                                    { %>
                                                <table width="99%" border="0" align="left" cellpadding="0" cellspacing="0" class="table table-striped table-bordered" style="margin-bottom: 10px !important;">
                                                    <tbody>
                                                        <tr>
                                                            <td width="7%" height="25" align="left" valign="middle"><strong>AGENT</strong></td>
                                                            <td width="93%" align="left" valign="middle"><strong><%:row1["AgentName"] %>   </strong></td>
                                                        </tr>
                                                        <tr>
                                                            <td height="25" colspan="2" align="left" valign="middle">

                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" class="table table-striped table-bordered" style="padding-bottom: 100px;">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td height="25" align="left" valign="middle">&nbsp;</td>
                                                                            <td colspan="10" align="right" valign="middle" style="text-align: center;"><strong>AGENT PLUS MINUS</strong></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180" height="25" align="left" valign="middle"><strong>CLIENT</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>M AMT</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>SESS.</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>TOT. AMT</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>M. COM</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>S. COM</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>TOL. COM</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>NET AMT</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>SA %</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>SA Share</strong></td>
                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>MOB. APP</strong></td>

                                                                            <td width="100" align="right" style="text-align: right;" valign="middle"><strong>FINAL</strong></td>

                                                                        </tr>

                                                                        <% int rows = 0; foreach (System.Data.DataRow row in AgentDataList[index].Rows)
                                                                            { %>
                                                                        <% if (rows == AgentDataList[index].Rows.Count - 1)
                                                                            { %>
                                                                        <tr>
                                                                            <td height="25" align="left" valign="middle"><strong>TOTAL</strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["MatchAmount"] %> </strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["SessionAmount"] %></strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalAmount"] %></strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["MatchCommision"] %></strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["SessionCommision"] %> </strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalCommisionAmount"] %> </strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["TotalNetAmount"] %> </strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["SHR"] %> </strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["SAAgentShare"] %></strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["MOBAppAmount"] %></strong></td>
                                                                            <td align="right" style="text-align: right;" valign="middle"><strong><%:row["FinalAmount"] %>  </strong></td>
                                                                        </tr>
                                                                        <% }
                                                                            else
                                                                            { %>
                                                                        <tr>
                                                                            <td height="25" align="left" valign="middle" class="FontText"><%:row["Name"] %></td>
                                                                            <td style="text-align: right;"><%:row["MatchAmount"] %></td>
                                                                            <td style="text-align: right;"><%:row["SessionAmount"] %></td>
                                                                            <td style="text-align: right;"><strong><%:row["TotalAmount"] %></strong></td>
                                                                            <td style="text-align: right;"><%:row["MatchCommision"] %></td>
                                                                            <td style="text-align: right;"><%:row["SessionCommision"] %></td>
                                                                            <td style="text-align: right;"><strong><%:row["TotalCommisionAmount"] %></strong></td>
                                                                            <td style="text-align: right;"><strong><%:row["TotalNetAmount"] %></strong></td>
                                                                            <td style="text-align: right;"><strong><%:row["SHR"] %></strong></td>
                                                                            <td style="text-align: right;"><%:row["SAAgentShare"] %></td>
                                                                            <td style="text-align: right;"><%:row["MOBAppAmount"] %></td>
                                                                            <td style="text-align: right;"><strong><%:row["FinalAmount"] %></strong></td>

                                                                        </tr>
                                                                        <% }
                                                                                rows++; //else
                                                                            } //foreach %>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <%  index++; totalindex++;
                                                    } //foreach %>

                                                <br>

                                                <br>
                                                <br>

                                                <br>


                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" class="table table-striped table-bordered">
                                                    <tbody>
                                                        <tr>
                                                            <td width="125" height="25" align="left" valign="middle"><strong>SA TOTAL</strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SAMatchAmount" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SASessionAmount" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SATotalAmount" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SAMatchCommision" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SASessionCommision" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SATotalCommisionAmount" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SATotalNetAmount" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label12" runat="server"></asp:Label></strong></td>
                                                            
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SBSAAgentShare" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SAMOBAppAmount" runat="server"></asp:Label></strong></td>
                                                            
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="SAFinalAmount" runat="server"></asp:Label></strong></td>

                                                        </tr>
                                                    </tbody>
                                                </table>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br>
                                &nbsp;
                    <!--//Close Super Agent-->




                                <br>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table table-striped table-bordered">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" class="table table-striped table-bordered">
                                                    <tbody>
                                                        <tr>
                                                            <td width="180" height="25" align="left" valign="middle"><strong>GRAND TOTAL</strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label2" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label3" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label4" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label5" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label6" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label7" runat="server"></asp:Label></strong></td>
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label13" runat="server"></asp:Label></strong></td>
                                                             <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label10" runat="server"></asp:Label></strong></td>
                                                             <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label9" runat="server"></asp:Label></strong></td>
                                                            
                                                            <td width="80" align="right" style="text-align: right;" valign="middle"><strong>
                                                                <asp:Label ID="Label11" runat="server"></asp:Label></strong></td>

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
    </div>
</asp:Content>


