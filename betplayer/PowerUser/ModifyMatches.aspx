<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="ModifyMatches.aspx.cs" Inherits="betplayer.poweruser.ModifyMatches" %>

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
                    <h3 class="page-title">Modify Matches </h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>

                        <li>Modify Match<span class="divider">&nbsp;</span></li>
                        <li><a href="ModifyMatch.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Game Details</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->
                            <table width="100%" class="table table-striped table-hover table-bordered" id="matchesTable">
                                <tbody>
                                    <tr>
                                        <td width="20" align="center" class="TableHeadingCheckBox">&nbsp;</td>
                                        <td width="30" height="25" align="left" class="TableHeading">SNo.</td>
                                        <td width="40" align="left" class="TableHeading">Code</td>
                                        <td align="left" class="TableHeading">Name</td>
                                        <td width="70" align="left" class="TableHeading">Date</td>
                                        <td width="110" align="left" class="TableHeading">Time</td>
                                        <td width="110" align="left" class="TableHeading">Match Type </td>
                                        <td width="110" align="left" class="TableHeading">Declare </td>
                                        <td width="110" align="left" class="TableHeading">Won by</td>

                                    </tr>
                                    <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                        {
                                            string MatchesID = row["MatchesID"].ToString(); %>
                                    <tr style="background-color: #FFFFFF">

                                        <td align="left" class=" ">
                                            <div class="btn-group">
                                                <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                <ul class="dropdown-menu">
                                                    <li><a href="EditMatches.aspx?Matchid=<%:row["MatchesID"] %>"><i class="icon-pencil"></i>Edit</a></li>
                                                    <li><a href="ManuallyUpdation.aspx?Matchid=<%:row["apiID"] %>&fk=<%:row["firebasekey"] %>"><i class="icon-pencil"></i>Manually Updataion</a></li>
                                                    <li><a onclick="schedulematch(<%:row["apiID"] %>,<%:row["MatchesID"] %>)"><i class="icon-pencil"></i>Schedule</a></li>

                                                </ul>
                                            </div>
                                        </td>

                                        <td height="20" align="left" class="FontText"><%:row["MatchesID"] %></td>
                                        <td align="left" class="FontText"><%:row["apiID"] %></td>
                                        <td align="left" class="FontText"><%:row["TeamA"] %> VS <%:row["TeamB"] %> </td>
                                        <td align="left" class="FontText"><%: toDateString(row["DateTime"])%></td>
                                        <td width="110" align="left" class="FontText"><%: toTime(row["DateTime"]) %></td>
                                        <td width="110" align="left" class="FontText"><%:row["Type"] %></td>
                                        <td width="100" align="left" class="FontText"><%if (Convert.ToInt16(row["Declear"]) == 1){%>Yes<% }else{%>No<% } %></td>
                                        <td width="92" align="left" class="FontText"><%: row["WinnerTeam"] %></td>
                                        

                                    </tr>
                                    <% } //foreach %>
                                </tbody>
                            </table>
                            <script>
                                function schedulematch(apiID, matchesID) {
                                    var params = {
                                        apiId: apiID,
                                        matchesId: matchesID
                                    };

                                    var formBody = [];
                                    for (var property in params) {
                                        var encodedKey = encodeURIComponent(property);
                                        var encodedValue = encodeURIComponent(params[property]);
                                        formBody.push(encodedKey + "=" + encodedValue);
                                    }
                                    formBody = formBody.join("&");

                                    fetch('/PowerUser/ScheduleMatch.ashx', {
                                        credentials: 'same-origin',
                                        method: 'POST',
                                        headers: {
                                            'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                                        },
                                        body: formBody
                                    }).then(function (responce) {
                                        return responce.json();
                                    }).then(function (data) {
                                        console.log(data);
                                        if (data.status)
                                            alert("Scheduled Successfully");
                                        else
                                            alert(data.error);
                                    }).then(function () {

                                    }).catch(function (err) {
                                        console.log(err);
                                    });
                                }

                            </script>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
