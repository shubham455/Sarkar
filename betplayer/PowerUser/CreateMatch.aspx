<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="CreateMatch.aspx.cs" Inherits="betplayer.poweruser.CreateMatch" %>

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
                    <h3 class="page-title">Create Match <small>Display Match Details</small> </h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Create Match<span class="divider">&nbsp;</span> </li>

                        <li><a href="#"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Match Details</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->
                            <div class="portlet-body">
                                <div class="clearfix">

                                    <a href="AddMatchManually.aspx">
                                        <button class="btn btn-warning" type="button"><i class="icon-plus icon-white"></i>Create</button></a>

                                    <a href="AddFromLotus.aspx">
                                        <button class="btn btn-warning" type="button"><i class="icon-plus icon-white"></i>Create From Site1</button></a>

                                    <a href="AddfromDiamond.aspx">
                                        <button class="btn btn-warning" type="button"><i class="icon-plus icon-white"></i>Create From Site2</button></a>

                                    <div class="controls" style="vertical-align: middle; text-align: left; padding-top: 0px;">
                                        <asp:TextBox runat="server" TextMode="Date" class=" m-ctrl-medium date-picker" size="16" name="BillDate" type="text" ID="BillDate" AutoPostBack ="true" OnTextChanged="BillDate_TextChanged" />
                                    </div>

                                </div>

                            </div>
                            <br />

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

                                    </tr>

                                    <%if (!emptyLedgerTable)
                                        {
                                            foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                            { %>

                                    <tr style="background-color: #FFFFFF">
                                        <td align="center">

                                            <a href="AddMatch.aspx?ID=<%:row["MatchesID"] %>"><span class="icon-edit"></span></a>

                                        </td>

                                        <td height="20" align="left" class="FontText"><%:row["MatchesID"] %></td>
                                        <td align="left" class="FontText"><%:row["apiID"] %></td>
                                        <td align="left" class="FontText"><%:row["TeamA"] %> v <%:row["TeamB"] %> </td>
                                        <td align="left" class="FontText"><%: toDateString(row["DateTime"])%></td>
                                        <td width="110" align="left" class="FontText"><%: toTime(row["DateTime"]) %></td>
                                        <td width="110" align="left" class="FontText"><%:row["Type"] %></td>

                                    </tr>
                                    <% } //foreach
                                        }
                                        else
                                        { %>
                                    <td height="20" align="left" class="FontText"></td>
                                    <td align="left" class="FontText"></td>
                                    <td align="left" class="FontText">There is no any match</td>
                                    <td align="left" class="FontText"></td>
                                    <td width="110" align="left" class="FontText"></td>
                                    <td width="110" align="left" class="FontText"></td>
                                    <% } //else %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
