<%@ Page Language="C#" MasterPageFile="~/admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="poweruserdetails.aspx.cs" Inherits="betplayer.admin.poweruserdeatils" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                    <h3 class="page-title">Poweruser Details  <small>Display Poweruser Details</small> </h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Master Details <span class="divider">&nbsp;</span> </li>
                        <li>Poweruser Details<span class="divider">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Poweruser Details</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->


                            <div class="portlet-body">
                                <div class="clearfix">

                                    <a href="CreatePoweruser.aspx">
                                        <button class="btn btn-warning" type="button"><i class="icon-plus icon-white"></i>Create</button></a>

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
                                                    <asp:TextBox ID="txtsearch" CssClass="input-medium" runat="server" AutoPostBack="true" OnTextChanged="txtsearch_TextChanged"></asp:TextBox></label>
                                                </div>
                                            </div>
                                        </div>
                                        <table width="100%" class="table table-striped table-hover table-bordered dataTable" id="sample_1" aria-describedby="sample_1_info" style="width: 100%;">
                                            <thead>
                                                <tr role="row">
                                                    <th height="25" colspan="7" align="center" style="text-align: center; vertical-align: middle;" rowspan="1">Personal Details</th>

                                                </tr>

                                                <tr role="row">
                                                    <td width="2%" align="center" class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label=" " style="width: 24px;">
                                                        <div class="checker" id="uniform-undefined">
                                                            <span>
                                                                <asp:CheckBox ID="CheckboxID" runat="server" />
                                                            </span>
                                                        </div>
                                                    </td>



                                                    <th width="5%" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="&amp;nbsp;: activate to sort column ascending" style="width: 36px;">&nbsp;</th>
                                                    <th height="25" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="SNo.: activate to sort column ascending" style="width: 30px;">SNo.</th>
                                                    <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Code: activate to sort column ascending" style="width: 33px;">Code</th>
                                                    <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Name: activate to sort column ascending" style="width: 72px;">Name</th>
                                                    <th width="60" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Contact: activate to sort column ascending" style="width: 73px;">Contact</th>
                                                    <th width="60" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Contact: activate to sort column ascending" style="width: 73px;">Password</th>

                                                </tr>

                                            </thead>

                                            <tbody role="alert" aria-live="polite" aria-relevant="all">
                                                <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                                    { %>

                                                <tr bgcolor="#FFFFFF" class="even">
                                                    <td align="center" class="  sorting_1">
                                                        <div class="checker" id="uniform-2">
                                                            <span>
                                                                <input type="checkbox" class="checkboxes" name="checkbox" id="check" onclick="ChangesInStatus('<%:row["PoweruserID"] %>" value="<%:row["PoweruserID"] %>" style="opacity: 0;"></span>
                                                        </div>
                                                    </td>
                                                    <td align="left" class=" ">
                                                        <div class="btn-group">
                                                            <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                            <ul class="dropdown-menu">
                                                                <li><a href="ModifyPoweruser.aspx?id=<%:row["PoweruserID"] %>"><i class="icon-pencil"></i>Edit</a></li>
                                                                <li><a onclick="CallHandler('<%:row["PoweruserID"] %>');"><i class="icon-trash"></i>Delete</a></li>
                                                                <li><a onclick="SendLoginDetails('<%:row["PoweruserID"] %>');"><i class="icon-film"></i>Send Mobile Login Details</a></li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                    <td height="20" align="left" class="FontText "><%:row["PoweruserID"] %></td>
                                                    <td align="left" class="FontText "><%:row["Code"] %></td>
                                                    <td align="left" class="FontText "><%:row["name"] %></td>
                                                    <td align="left" class="FontText "><%:row["ContactNo"] %></td>
                                                    <td align="left" class="FontText "><%:row["Password"] %></td>
                                                    <% } //foreach %>
                                            </tbody>
                                        </table>
                                        <div class="row-fluid">
                                            <div class="span6">
                                                <div class="dataTables_info" id="sample_1_info">Showing 1 to 50 of 50 entries</div>
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
                <!-- END PAGE CONTAINER-->
            </div>
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

            fetch('/Agent/Deleteclient.ashx', {
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
        function SendLoginDetails(userId) {
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

            fetch('/Admin/sendpoweruserdetails.ashx', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                },
                body: formBody
            }).then(function (responce) {
                return responce.json();
            }).then(function (data) {
                if (data.status) alert("message sent with ID: " + data.userDeletedId + " Successfully.");
                else alert("message sent  Failed!!!" + "\r\n" + data.error);
            }).then(function () {
                location.reload();
            }).catch(function (err) {
                console.log(err);
            });
        }
    </script>
</asp:Content>


