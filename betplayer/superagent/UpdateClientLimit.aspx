<%@ Page Language="C#" MasterPageFile="~/superagent/DashBoard.Master" AutoEventWireup="true" CodeBehind="UpdateClientLimit.aspx.cs" Inherits="betplayer.Superagent.UpdateClientLimit" %>

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
                    <h3 class="page-title">Client Limit</h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Master Details <span class="divider">&nbsp;</span> </li>
                        <li>Client Limit<span class="divider">&nbsp;</span></li>
                        <li><a href="ViewClient.aspx?AgentID=<%: Agentcode %>"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
                    </ul>
                    <!-- END PAGE TITLE & BREADCRUMB-->
                </div>
            </div>
            <!-- END PAGE HEADER-->
            <!-- BEGIN PAGE CONTENT-->
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-globe"></i>Client Limit</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->

                            <div class="alert alert-error" id="Error" style="visibility: hidden">
                            </div>
                            <table id="ClientLimitTable" width="100%" class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td height="25" align="left" class="TableHeading">&nbsp;</td>
                                        <td align="left" class="TableHeading">&nbsp;</td>
                                        <td colspan="3" align="left" class="TableHeading">Client Limit</td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="left" class="TableHeading">SNo.</td>
                                        <td align="left" class="TableHeading">Client  Name</td>
                                        <td align="left" class="TableHeading">Fix Limit</td>
                                        <td align="left" class="TableHeading">Current Limit</td>
                                        <td align="left" class="TableHeading">Used Limit</td>
                                    </tr>
                                    <%int i = 1; %>
                                    <% int index = 2; foreach (System.Data.DataRow row in UpdateDataTable.Rows)
                                        { %>

                                    <tr bgcolor="#FFFFFF">

                                        <td height="25" align="left" class="FontText"><%=i %></td>
                                        <td align="left" class="FontText">
                                            <input name="ClientName1" type="text" class="TextBoxTransparent" id="ClientName1" style="text-align: right; text-align: left;" value="<%: row["ClientID"] %><%: row["Name"] %>" readonly="">
                                        </td>
                                        <td align="left" class="TextBox100">
                                            <input id="FixLimit" style="text-align: right" type="text" value="<%: row["FixLimit"] %>" class="TextBox100"></td>
                                        <td align="left" class="TextBox100">
                                            <input id="ClientID" type="hidden" value="<%: row["ClientID"] %>" />
                                            <input id="ClientLimit" style="text-align: right" type="text" onchange="check('<%: index %>')" value="<%: row["CurrentLimit"] %>" class="TextBox100"></td>
                                        <td align="left" class="FontText">
                                            <input name="ULimit1" type="text" id="ULimit1" style="text-align: right" value="<%: row["UsedLimit"] %>" class="TextBox100" readonly="" disabled=""></td>
                                    </tr>
                                    <%i++; %>
                                    <% index++;
                                    }//foreach  %>


                                    <tr bgcolor="#E9F3FD" class="TableHeading">
                                        <td height="25" align="center" bgcolor="#FFFFFF"><strong>Limit</strong></td>
                                        <td align="left" valign="middle" bgcolor="#FFFFFF" class="TableHeading">
                                            <input runat="server" name="AgentLimit" type="text" id="agentlimit" style="text-align: right" value="" readonly=""></td>
                                        <td align="left" bgcolor="#FFFFFF" class="FontText">&nbsp;</td>
                                        <td align="left" bgcolor="#FFFFFF" class="FontText">
                                            <input runat="server" name="TotalLimit" type="text" class="TextBox100" readonly="" id="TotalLimit" style="text-align: right" value=""></td>
                                        <td align="left" bgcolor="#FFFFFF" class="FontText">&nbsp;</td>
                                    </tr>

                                    <tr bgcolor="#E9F3FD" class="TableHeading">
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <a id="btnUpdate" class="btn btn-success" onclick="updateLimits()">Update Limits </a>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <a href="ViewClient.aspx?AgentID=<%: Agentcode %>" class="btn">Cancel</a>


                            <!-- END FORM-->
                        </div>
                    </div>
                </div>
            </div>
            <!-- END PAGE CONTENT-->
        </div>
        <!-- END PAGE CONTAINER-->
    </div>
    <script>
        function check(rowID) {
            var table = document.getElementById('ClientLimitTable');

            var input1 = Number(table.rows[rowID].cells[2].children[0].value);
            var input2 = Number(table.rows[rowID].cells[3].children[1].value);

            if (input1 >= input2) {

            }
            else {
                table.rows[rowID].cells[3].children[1].value = 0;
                alert("Current Limit Is Not greater than Fix Limit")
            }
        }

        function getValueByAttrName(attrName, children) {
            for (var i = 0; i < children.length; i++) {
                if (children[i].getAttribute('id') == attrName) // any attribute could be used here
                    return children[i].value;
            }
        }
        function updateLimits() {
            var valueArray = [];
            var table = document.getElementById("ClientLimitTable");
            for (var i = 2, row; i < (table.rows.length - 2); i++) {
                //iterate through rows
                //rows would be accessed using the "row" variable assigned in the for loop
                row = table.rows[i];
                var Fixchildren = row.cells[2].getElementsByTagName('input');// any tag could be used here..
                var children = row.cells[3].getElementsByTagName('input');// any tag could be used here..
                valueArray.push({

                    ClientID: getValueByAttrName("ClientID", children),
                    ClientLimit: getValueByAttrName("ClientLimit", children),
                    FixLimit: getValueByAttrName("FixLimit", Fixchildren)
                })
            }
            var formBody = [];
            for (var j = 0; j < valueArray.length; j++) {
                var params = valueArray[j];
                for (var property in params) {
                    var encodedKey = encodeURIComponent(property.concat(j));
                    var encodedValue = encodeURIComponent(params[property]);
                    formBody.push(encodedKey + "=" + encodedValue);
                }
                if (j === (valueArray.length - 1))
                    formBody.push("Length=".concat(valueArray.length));
            }
            console.log(formBody);
            var urlParams = new URLSearchParams(window.location.search);
            formBody = formBody.join("&");
            formBody += "&AgentCode=" + urlParams.get('AgentID');
            fetch('/superagent/updateclientLimits.ashx', {
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
                if (data.status === "success") {
                    alert("Updated Limits Successfully");
                    location.reload();
                }
                else if (data.status === "unsuccess") {
                    alert("please Check Agent Limit!!!")
                }
                else alert("Update Limits Rejected!!!");
            }).catch(function (err) {
                console.log(err);
            });
        }
    </script>

</asp:Content>
