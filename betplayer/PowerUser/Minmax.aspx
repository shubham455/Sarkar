<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="Minmax.aspx.cs" Inherits="betplayer.PowerUser.Minmax" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="main-content">
        <div class="container-fluid">
            <!-- BEGIN PAGE HEADER-->
            <div class="row-fluid">
                <div class="span12">
                    <!-- BEGIN THEME CUSTOMIZER-->
                    <div id="theme-change" class="hidden-phone"><i class="icon-cogs"></i><span class="settings"><span class="text">Theme:</span> <span class="colors"><span class="color-default" data-style="default"></span><span class="color-gray" data-style="gray"></span><span class="color-purple" data-style="purple"></span><span class="color-navy-blue" data-style="navy-blue"></span></span></span></div>
                    <!-- END THEME CUSTOMIZER-->
                    <!-- BEGIN PAGE TITLE & BREADCRUMB-->
                    <h3 class="page-title">Runner</h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>

                        <li><a href="#">Modify matches</a><span class="divider">&nbsp;</span></li>

                        <li><a href="ModifyMatches.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Runner</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                       

                            <div class="widget-body form">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td width="25%" align="left" valign="middle">
                                                <span>Runner Min/Max: </span>
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="minBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="minBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" >
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="maxBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="maxBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" >
                                            </td>

                                            <td width="15%" align="left" valign="middle">
                                                <button id="btnminmaxUpdate" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left" valign="middle">
                                                <span>Session Min/Max: </span>
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="sessionMinBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="sessionMinBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" >
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="sessionMaxBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="sessionMaxBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" >
                                            </td>

                                            <td width="15%" align="left" valign="middle">
                                                <button id="btnsessionMinMaxUpdate" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left" valign="middle">
                                                <span>Fancy Min/Max: </span>
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="fancyMinBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="fancyMinBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" >
                                            </td>
                                            <td width="30%" align="left" valign="middle">
                                                <input id="fancyMaxBet" onfocus="this.setSelectionRange(0, this.value.length)" onkeydown="fancyMaxBetKeyPress(event)" type="text" style="width: 90%; height: 50px; font-size: xx-large; font-weight: bold" value="" >
                                            </td>

                                            <td width="15%" align="left" valign="middle">
                                                <button id="btnfancyMinMaxUpdate" type="button" class="btn btn-success" style="height: 50px; width: 100px; font-size: large;">Update</button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            
                        </div>
                    
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="firebasekey" runat="server" />
    <script src="https://www.gstatic.com/firebasejs/4.13.0/firebase.js"></script>
    <script src="js/minmaxUpdate.js"></script>

    <script>
        function matchDeclareConfirmation() {
            if (confirm("Are you sure you want to declare this match?"))
                return true;
            else
                return false;
        }
        
    </script>
   
</asp:Content>

