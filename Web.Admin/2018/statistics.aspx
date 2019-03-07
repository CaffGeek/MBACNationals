<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_results.jpg');"></div>
    <div id="photoCredit"><strong>Port Arthur</strong> &bull; Credit: Sharon Mollerus, Flickr Commons</div>
    <style>@page { size: landscape; }</style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-sm-2 hidden-print" data-ng-controller="HighscoresController">
        <ul class="sidebarNav">
            <li><a ui-sref="highscores()">HIGH SCORES</a></li>
            <li><a ui-sref="highpoa()">HIGH POA</a></li>
            <li><a ui-sref="highaverage()">HIGH AVERAGE</a></li>
            <li><a ui-sref="highwins()">MOST WINS</a></li>
        </ul>
    </div>

    <div class="col-sm-10">
        <div class="col-md-12" ui-view=""></div>
    </div>
</asp:Content>
