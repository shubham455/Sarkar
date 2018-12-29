
<%@ Page Language="C#" MasterPageFile="~/admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="AgentDetails.aspx.cs" Inherits="betplayer.admin.AgentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="main-content">
        <div class="container-fluid">
            <!-- BEGIN PAGE HEADER-->
            <div class="row-fluid">
                <div class="span12">
                    <!-- BEGIN THEME CUSTOMIZER-->
                    <div id="theme-change" class="hidden-phone"><i class="icon-cogs"></i><span class="settings"><span class="text">Theme:</span> <span class="colors"><span class="color-default" data-style="default"></span><span class="color-gray" data-style="gray"></span><span class="color-purple" data-style="purple"></span><span class="color-navy-blue" data-style="navy-blue"></span></span></span></div>
                    <!-- END THEME CUSTOMIZER-->
                    <!-- BEGIN PAGE TITLE & BREADCRUMB-->
                    <h3 class="page-title">Client Details  <small>Create, Modify, Delete and Display Agent Details</small> </h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Master Details <span class="divider">&nbsp;</span> </li>
                        <li>Agent Details<span class="divider">&nbsp;</span></li>
                        <li><a href="MasterDetails.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Agent Details</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->

                            <div class="portlet-body">
                                <div class="clearfix">

                                    <a href="CreateAgent.aspx">
                                        <button class="btn btn-warning" type="button"><i class="icon-plus icon-white"></i>Create</button></a>
                                    <div class="btn-group">
                                        <asp:DropDownList ID="DropDownstatus" runat="server" CssClass="btn btn-primary dropdown-toggle" Width="150px" OnSelectedIndexChanged="DropDownstatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Please Select" Value="0">--Change Status--</asp:ListItem>
                                            <asp:ListItem Text="Active" Value="Active">Active</asp:ListItem>
                                            <asp:ListItem Text="Inactive" Value="Inactive">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <a href="UpdateAgentlimit.aspx">
                                        <button class="btn btn-primary" type="button">Update Limit</button></a>
                                </div>
                                <div class="space15"></div>

                            </div>

                            <div id="sample_1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                <div class="row-fluid">
                                    <div class="span6">
                                        <div id="sample_1_length" class="dataTables_length">
                                            <label>
                                                <select size="1" name="sample_1_length" aria-controls="sample_1" class="input-mini">
                                                    <option value="100" selected="selected">100</option>
                                                    <option value="500">500</option>
                                                    <option value="1000">1000</option>
                                                    <option value="All">All</option>
                                                </select>
                                                records per page</label>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="dataTables_filter" id="sample_1_filter">
                                            <label>
                                                Search:
                                                <asp:TextBox ID="txtsearch" runat="server" aria-controls="sample_1" class="input-medium" AutoPostBack="true" OnTextChanged="txtsearch_TextChanged" /></label>
                                        </div>
                                    </div>
                                </div>
                                <table width="100%" class="table table-striped table-hover table-bordered dataTable" id="sample_1" aria-describedby="sample_1_info" style="width: 100%;">
                                    <thead>
                                        <tr role="row">
                                            <th align="center" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th height="25" align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>
                                            <th colspan="3" align="center" valign="middle" style="text-align: center; vertical-align: middle;" rowspan="1">Agent Commission %</th>
                                            <th colspan="3" align="right" style="text-align: center; vertical-align: middle;" rowspan="1">Limit</th>
                                            <th align="left" rowspan="1" colspan="1">&nbsp;</th>

                                        </tr>
                                        <tr role="row">
                                            <th width="2%" align="center" class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label="
                          "
                                                style="width: 24px;">
                                                <div class="checker" id="uniform-undefined">
                                                    <span>
                                                        <input type="checkbox" class="group-checkable" data-set="#sample_1 .checkboxes" style="opacity: 0;"></span>
                                                </div>
                                            </th>
                                            <th width="5%" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="&amp;nbsp;: activate to sort column ascending" style="width: 36px;">&nbsp;</th>
                                            <th width="5%" height="25" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="SNo.: activate to sort column ascending" style="width: 36px;">SNo.</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Code: activate to sort column ascending" style="width: 33px;">Code</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Name : activate to sort column ascending" style="width: 89px;">Name </th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Contact : activate to sort column ascending" style="width: 73px;">Contact </th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="D.O.J.: activate to sort column ascending" style="width: 67px;">D.O.J.</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Share%: activate to sort column ascending" style="width: 48px;">Share%</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="PWD: activate to sort column ascending" style="width: 44px;">PWD</th>
                                            <th align="right" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Type: activate to sort column ascending" style="width: 36px;">Type</th>
                                            <th align="right" style="text-align: right; width: 32px;" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Mat: activate to sort column ascending">Mat</th>
                                            <th align="right" style="text-align: right; width: 32px;" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Ses: activate to sort column ascending">Ses</th>
                                            <th align="right" style="text-align: center; width: 51px;" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Fix: activate to sort column ascending">Fix</th>
                                            <th align="right" style="text-align: center; width: 51px;" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Agent : activate to sort column ascending">Agent </th>
                                            <th align="right" style="text-align: center; width: 44px;" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Client : activate to sort column ascending">Client </th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Status: activate to sort column ascending" style="width: 45px;">Status</th>
                                        </tr>
                                    </thead>
                                    <%int i = 1; %>
                                    <% foreach (System.Data.DataRow row in MatchDataTable.Rows)
                                        { %>

                                    <tbody role="alert" aria-live="polite" aria-relevant="all">
                                        <tr class="odd">
                                            <td align="center" class="  sorting_1">
                                                <div class="checker" id="uniform-1">
                                                    <span>
                                                        <input type="checkbox" class="checkboxes" name="checkbox" id="check" onclick="ChangesInStatus('<%:row["AgentID"] %>" value="<%:row["AgentID"] %>" style="opacity: 0;"></span>
                                                </div>

                                            </td>
                                            <td align="left" class=" ">
                                                <div class="btn-group">
                                                    <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="ModifyAgent.aspx?id=<%:row["AgentID"] %>"><i class="icon-pencil"></i>Edit</a></li>
                                                        <li><a onclick="CallHandler('<%:row["AgentID"] %>');"><i class="icon-trash"></i>Delete</a></li>
                                                        <li><a onclick="ChangesInStatus('<%:row["AgentID"] %>');"><i class="icon-ban-circle"></i>
                                                            InActive                              </a></li>
                                                        <li class="divider"></li>
                                                        <li><a href="ClientLimit.php?id=17"><i class="icon-columns"></i>Client Limit</a></li>
                                                        <li><a href="javascript:SendLoginDetails('17');"><i class="icon-film"></i>Send Login Details</a></li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <td height="20" align="left" class=" "><%=i %></td>
                                            <td align="left" class=" "><%:row["Code"] %></td>
                                            <td align="left" class=" "><%:row["Name"] %></td>
                                            <td align="left" class=" "><%:row["ContactNO"] %></td>
                                            <td align="left" class=" "><%= Date(Convert.ToDateTime(row["Date"]))%></td>
                                            <td align="left" class=" "><%:row["Agentshare"] %></td>
                                            <td align="left" class=" "><%:row["Password"] %></td>
                                             <% if (row["SessionCommisionType"].ToString() == "Bet By Bet") %>

                                                <% { %>

                                                <td align="right" bgcolor="#FFFFFF" class="FontText ">BBB</td>


                                                <% } %>
                                                <% else if (row["SessionCommisionType"].ToString() == "No Commision") %>

                                                <% { %>

                                                <td align="right" bgcolor="#FFFFFF" class="FontText ">NOC</td>


                                                <% } %>
                                                <% else if (row["SessionCommisionType"].ToString() == "Only On Minus") %>

                                                <% { %>

                                                <td align="right" bgcolor="#FFFFFF" class="FontText ">OOM</td>


                                                <% } %>

                                            <td align="right" bgcolor="#FFFFFF" class="FontText " style="text-align: right;"><%:row["MatchCommision"] %></td>
                                            <td align="right" bgcolor="#FFFFFF" class="FontText " style="text-align: right;"><%:row["SessionCommision"] %></td>
                                            <td align="right" style="text-align: right;" class=" "><%:row["Fixlimit"] %></td>
                                            <td align="right" style="text-align: right;" class=" "><%:row["Currentlimit"] %></td>
                                            <td align="right" style="text-align: right;" class=" ">0</td>
                                            <td align="left" class=" "><%:row["Status"] %></td>

                                        </tr>

                                    </tbody>
                                    <%i++; %>
                                    <% } //foreach %>
                                </table>
                                <div class="row-fluid">
                                    <div class="span6">
                                        <div class="dataTables_info" id="sample_1_info">Showing 1 to 6 of 6 entries</div>
                                    </div>
                                    <div class="span6">
                                        <div class="dataTables_paginate paging_bootstrap pagination">
                                            <ul>
                                                <li class="prev disabled"><a href="#">← Prev</a></li>
                                                <li class="active"><a href="#">1</a></li>
                                                <li class="next disabled"><a href="#">Next → </a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE widget-->
                </div>
            </div>
            <!-- END ADVANCED TABLE widget-->
            <!-- END PAGE CONTENT-->
        </div>
    </div>
    <script>
        function CallHandler(userId) {
            var params = {
                userId: userId
            };

            var formBody = [];
            for (var property in params) {
                var encodedKey = encodeURIComponent(property);
                var encodedValue = encodeURIComponent(params[property]);
                formBody.push(encodedKey + "=" + encodedValue);
            }
            formBody = formBody.join("&");

            fetch('/Admin/DeleteAgent.ashx', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                },
                body: formBody
            }).then(function (responce) {
                return responce.json();
            }).then(function (data) {
                if (data.status) alert("User with ID: " + data.userDeletedId + " Successfully Deleted.");
                else alert("User Delete Failed!!!" + "\r\n" + data.error);
            }).then(function () {
                location.reload();
            }).catch(function (err) {
                console.log(err);
            });
        }
    </script>
    <script>
        function ChangesInStatus(userId) {
            var params = {
                userId: userId
            };

            var formBody = [];
            for (var property in params) {
                var encodedKey = encodeURIComponent(property);
                var encodedValue = encodeURIComponent(params[property]);
                formBody.push(encodedKey + "=" + encodedValue);
            }
            formBody = formBody.join("&");

            fetch('/Admin/ChangeStatus.ashx', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                },
                body: formBody
            }).then(function (responce) {
                return responce.json();
            }).then(function (data) {
                if (data.status) alert("Status Updated with ID: " + data.userDeletedId + " Successfully");
                else alert("User Status Updated Failed!!!" + "\r\n" + data.error);
            }).then(function () {
                location.reload();
            }).catch(function (err) {
                console.log(err);
            });
        }
    </script>
</asp:Content>
