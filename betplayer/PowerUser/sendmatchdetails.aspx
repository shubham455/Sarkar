<%@ Page Language="C#" MasterPageFile="~/PowerUser/Dashboard.Master" AutoEventWireup="true" CodeBehind="sendmatchdetails.aspx.cs" Inherits="betplayer.PowerUser.sendmatchdetails" %>

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
                    <h3 class="page-title">Add Match</h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>

                        <li><a href="#">Add Match</a><span class="divider">&nbsp;</span></li>

                        <li><a href="#"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Add Match</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->

                            <div class="alert alert-error" id="Error" style="visibility: hidden">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    
                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Description</td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtdescription" CssClass="textbox" runat="server" TextMode="MultiLine" Width="300px" Height="100px" /></td>
                                                        <td align="left" valign="middle">&nbsp;</td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>

                                                     
                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">All Superagent </td>
                                                        <td align="left" valign="middle">
                                                            <asp:CheckBox ID="CheckBox1" Enabled="true" Checked="true" runat="server" Text="All Superagent" /></td>
                                                        <td align="left" valign="middle">&nbsp;</td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">All Agent </td>
                                                        <td align="left" valign="middle">
                                                            <asp:CheckBox ID="CheckBox2" Enabled="true" Checked="true" runat="server" Text="All Agent" /></td>
                                                        <td align="left" valign="middle">&nbsp;</td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">All Client </td>
                                                        <td align="left" valign="middle">
                                                            <asp:CheckBox ID="CheckBox3" Enabled="true" Checked="true" runat="server" Text="All Client" /></td>
                                                        <td align="left" valign="middle">&nbsp;</td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                   
                                                   
                                                   
                                                    
                                                    <tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <div class="form-actions">
                                <asp:Button ID="btnsend" runat="server" ValidationGroup="vg" class="btn btn-success" Text="send" OnClick="btnsend_Click" />
                                <asp:Button ID="btncancel" runat="server" class="btn btn-success" Text="Cancel" OnClick="btncancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
