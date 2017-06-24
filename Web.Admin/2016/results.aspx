<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_7.jpg');"></div>
    <div id="photoCredit"><strong>Crescent Beach in Surrey, BC</strong> &bull; Credit: Jerry Meaden, WikiCommons</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-sm-2 hidden-print" data-ng-controller="ResultsController">
        <ul class="sidebarNav">
            <!--<li><a href="">Coming Soon</a></li>-->
            <li><a ui-sref="standings({division: 'Tournament Men Single'})">TOURNAMENT MEN SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Tournament Ladies Single'})">TOURNAMENT LADIES SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Tournament Men'})">TOURNAMENT MEN</a></li>
            <li><a ui-sref="standings({division: 'Tournament Ladies'})">TOURNAMENT LADIES</a></li>
            <li><a ui-sref="standings({division: 'Teaching Men Single'})">TEACHING MEN SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Teaching Ladies Single'})">TEACHING LADIES SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Teaching Men'})">TEACHING MEN</a></li>
            <li><a ui-sref="standings({division: 'Teaching Ladies'})">TEACHING LADIES</a></li>
            <li><a ui-sref="standings({division: 'Seniors'})">SENIORS</a></li>
            <li><a ui-sref="standings({division: 'Seniors Single'})">SENIORS SINGLE</a></li>
            <hr />
            <li><a ui-sref="stepladder()" data-ng-click="viewStepladder()">STEPLADDER</a></li>
        </ul>
    </div>

    <div class="col-sm-10">
        <div class="col-md-12" ui-view=""></div>

        <h5 class="col-md-10 text-center">Results are unofficial</h5>
    </div>
</asp:Content>
