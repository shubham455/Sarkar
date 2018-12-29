<%@ Page Language="C#" MasterPageFile="~/Agent/DashBoard.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="betplayer.Agent.Dashboard" %>

<asp:Content ID ="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="main-content">
			<!-- BEGIN PAGE CONTAINER-->
			<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->
				<div class="row-fluid">
					<div class="span12">
						<!-- BEGIN THEME CUSTOMIZER--><!-- END THEME CUSTOMIZER-->
						<!-- BEGIN PAGE TITLE & BREADCRUMB-->
					  <h3 class="page-title">
							Dashboard
							<small> General Information </small>
						</h3>
						<ul class="breadcrumb">
							<li>
                                <a href="#"><i class="icon-home"></i></a><span class="divider">&nbsp;</span>
							</li>
							<li><a href="#">Dashboard</a><span class="divider-last">&nbsp;</span></li>
                            <li class="pull-right search-wrap">
                                
                            </li>
						</ul>
						<!-- END PAGE TITLE & BREADCRUMB-->
					</div>
				</div>
				<!-- END PAGE HEADER-->
				<!-- BEGIN PAGE CONTENT-->
                <div class="row-fluid circle-state-overview">
                    <div style="float:left;padding-left:20px;padding-right:20px;">
                        <div class="circle-wrap">
                            <a href="MasterDetails.aspx">
                                <div class="stats-circle turquoise-color">
                                    <i class="icon-user"></i>
                                </div>
                            <strong><p style="color:#4CC5CD;">Master Details</p></strong>
                            </a>
                        </div>
                    </div>
                	
                    <div style="float:left;padding-left:20px;padding-right:20px;">
                        <div class="circle-wrap">
                            <a href="SportsDetails.aspx">
                                <div class="stats-circle red-color">
                                    <i class="icon-tags"></i>
                                </div>
                                <strong><p style="color:#E17F90;">Sport's Betting</p></strong>
                            </a>
                        </div>
                    </div>
                    
                    <div style="float:left;padding-left:20px;padding-right:20px;">
                        <div class="circle-wrap">
                        	<a href="Ledger.aspx">
                                <div class="stats-circle purple-color">
                                    <i class="icon-eye-open"></i>
                                </div>
                                <strong><p style="color:#C8ABDB;">Ledger</p></strong>
                            </a>
                        </div>
                    </div>
                    
                    <div style="float:left;padding-left:20px;padding-right:20px;">
                        <div class="circle-wrap">
                        	<a href="Settings.aspx">
                                <div class="stats-circle blue-color">
                                    <i class="icon-bar-chart"></i>
                                </div>
                                <strong><p style="color:#93C4E4;">Settings</p></strong>
                            </a>
                        </div>
                    </div>
                    
                    <div style="float:left;padding-left:20px;padding-right:20px;">
                        <div class="circle-wrap">
                            <a href="Login.aspx">
                                <div class="stats-circle gray-color">
                                    <i class="icon-comments-alt"></i>
                                </div>
                                <strong><p style="color:#B9BABA;">Log Out</p></strong>
                        	</a>
                        </div>
                    </div>
                    
                </div>
                       
				<!-- END PAGE CONTENT-->
			</div>
			<!-- END PAGE CONTAINER-->
		</div>
</asp:Content>