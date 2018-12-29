<%@ Page Language="C#" MasterPageFile="~/Agent/DashBoard.Master" AutoEventWireup="true" CodeBehind="ViewCollectionEntry.aspx.cs" Inherits="betplayer.Agent.ViewCollectionEntry" %>

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
                    <h3 class="page-title">Entry Details  <small> Delete and Display Entry Details</small> </h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Master Details <span class="divider">&nbsp;</span> </li>
                        <li>Collection Details<span class="divider">&nbsp;</span></li>
                        <li><a href="CollectionDetails.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Entry Details</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->


                            <div id="sample_1_wrapper" class="dataTables_wrapper form-inline" role="grid">

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


                                        </tr>
                                        <tr role="row">

                                            <th width="5%" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="&amp;nbsp;: activate to sort column ascending" style="width: 36px;">&nbsp;</th>
                                            <th width="5%" height="25" align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="SNo.: activate to sort column ascending" style="width: 36px;">SNo.</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Code: activate to sort column ascending" style="width: 33px;">Code</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Name : activate to sort column ascending" style="width: 89px;">Name </th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Contact : activate to sort column ascending" style="width: 73px;">Collection Type</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="D.O.J.: activate to sort column ascending" style="width: 67px;">D.O.E.</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Share%: activate to sort column ascending" style="width: 48px;">Amount</th>
                                            <th align="left" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="PWD: activate to sort column ascending" style="width: 44px;">PaynmentType</th>
                                            <th align="right" class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Type: activate to sort column ascending" style="width: 36px;">Remark</th>
                                        </tr>
                                    </thead>
                                    <% int i = 1; %>
                                    <% foreach (System.Data.DataRow row in MatchesDataTable.Rows)
                                        { %>

                                    <tbody role="alert" aria-live="polite" aria-relevant="all">
                                        <tr class="odd">

                                            <td align="left" class=" ">
                                                <div class="btn-group">
                                                    <a class="btn btn-primary dropdown-toggle" data-toggle="dropdown" href="#"><span class="icon-caret-down"></span></a>
                                                    <ul class="dropdown-menu">
                                                        <li><a onclick="CallHandler('<%:row["CollectionID"] %>');"><i class="icon-trash"></i>Delete</a></li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <td height="20" align="left" class=" "><%=i %></td>
                                            <td align="left" class=" "><%:row["CollectionID"] %></td>
                                            <td align="left" class=" "><%:row["Name"] %></td>
                                            <td align="left" class=" "><%:row["CollectionType"] %></td>
                                            <td align="left" class=" "><%: row["Date"]%></td>
                                            <td align="left" class=" "><%:row["Amount"] %></td>
                                            <td align="left" class=" "><%:row["PaynmentType"] %></td>
                                            <td align="right" style="text-align: right;" class=" "><%:row["Remark"] %></td>


                                        </tr>

                                    </tbody>
                                    <%i++; %>
                                    <% } //foreach %>
                                </table>
                               
                            </div>


                            <!-- END FORM-->

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

            fetch('/Agent/DeleteEntry.ashx', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                },
                body: formBody
            }).then(function (responce) {
                return responce.json();
            }).then(function (data) {
                if (data.status) alert("Entry ID: " + data.userDeletedId + " Successfully Deleted.");
                else alert("Entry Delete Failed!!!" + "\r\n" + data.error);
            }).then(function () {
                location.reload();
            }).catch(function (err) {
                console.log(err);
            });
        }
    </script>

</asp:Content>
