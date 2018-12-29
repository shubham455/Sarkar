<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/admin/Dashboard.Master" CodeBehind="CreateSuperAgent.aspx.cs" Inherits="betplayer.admin.CreateSuperAgent" %>


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
                    <h3 class="page-title">Create New Client</h3>
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span> </li>
                        <li>Master Details <span class="divider">&nbsp;</span> </li>
                        <li><a href="ClientDetails.aspx">Client Details</a><span class="divider">&nbsp;</span></li>
                        <li>Create New Client<span class="divider">&nbsp;</span></li>
                        <li><a href="ClientDetails.aspx"><span style="color: #00F;"><strong>Back</strong></span></a><span class="divider-last">&nbsp;</span></li>
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
                            <h4><i class="icon-globe"></i>Create New Agent</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;" class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body form">
                            <!-- BEGIN FORM-->

                            <div class="alert alert-error" id="Error" style="visibility: hidden">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td width="550">
                                            <div></div>
                                            <table width="850" border="0" cellspacing="0" cellpadding="0">
                                                <tbody>

                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Name</td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="textbox" ID="txtname" runat="server" /></td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Contact No</td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" CssClass="textbox" MaxLength="10" ID="txtContactno" /></td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Password</td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtPassword" /></td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left" class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">SuperAgent Limit</td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox CssClass="textbox" ID="txtAgentlimit" runat="server" /></td>
                                                        <td align="left" valign="middle">&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left" class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">SuperAgent Share %</td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox CssClass="textbox" ID="txtAgentShare" runat="server" /></td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left" class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Mobile App</td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="textbox" ID="txtMobileApp" runat="server" Text="0" MaxLength="3" />
                                                            <asp:RegularExpressionValidator ID="regmob" runat="server" ErrorMessage="Mobile App Amount Should Be less Than 100 Or Equal To 100" ControlToValidate="txtMobileApp" ValidationExpression="^([0-9]|[1-9][0-9]|100)$"></asp:RegularExpressionValidator>
                                                        
                                                        </td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="welcome">
                                                            <p>&nbsp;</p>
                                                        </td>
                                                        <td height="30" align="left" valign="middle" class="welcome"><strong>SuperAgent Commission Percentage</strong></td>

                                                        <td align="left">&nbsp;</td>

                                                        <td align="left">&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left" class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Session Commission Type</td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="SessionDropDown" runat="server" class="form-control" AppendDataBoundItems="False" DataTextField="UserName" DataValueField="UserID">
                                                                <asp:ListItem Text="Please Select" Value="0">Commission Type</asp:ListItem>
                                                                <asp:ListItem Text="No Commission" Value="1">No Commission</asp:ListItem>
                                                                <asp:ListItem Text=">Only On Minus" Value="2">Only On Minus</asp:ListItem>
                                                                <asp:ListItem Text="Bet By Bet" Value="3">Bet By Bet</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="SessionDropDown"
                                                                ErrorMessage="Value Required!" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="left" valign="middle">&nbsp;</td>
                                                    </tr>
                                                    <tr id="divMatchCommission">
                                                        <td align="left" class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">
                                                            <span id="CommHeading">Match Commission</span>
                                                        </td>
                                                        <td align="left">
                                                            <input runat="server" name="MatchCommissionClient" type="text" class="textbox" id="MatchCommissionAgent" onblur="showCustomer(this.value)" value="0"></td>

                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr id="divSessionCommission">
                                                        <td align="left" class="welcome">&nbsp;</td>
                                                        <td height="30" align="left" valign="middle" class="welcome">Session Commission</td>
                                                        <td align="left">
                                                            <input name="SessionCommissionClient" runat="server" type="text" class="textbox" id="SessionCommissionAgent" onblur="showCustomer(this.value)" value="0"></td>

                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="form-actions">
                                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" OnClick="Submit_Click" Text="Save" />
                                <asp:Button ID="btnCancel" ValidationGroup="vg1" CssClass="btn btn-success" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <!-- END PAGE CONTENT-->
        </div>
        <!-- END PAGE CONTAINER-->
    </div>

</asp:Content>

