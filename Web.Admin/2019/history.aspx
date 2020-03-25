<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_photos.jpg');"></div>
    <div id="photoCredit"><strong>TODO: description</strong> &bull; Credit: TODO: credit</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="row" data-ng-controller="HistoryController as vm ">
        <div class="col-md-2">
            <ul class="sidebarNav">
                <div ng-repeat="historyItem in vm.History | orderBy:'-year'">
                    <li><a href="" ng-click="vm.selected = historyItem">{{historyItem.year}} - {{historyItem.province}} - {{historyItem.city}}</a></li>
                </div>
            </ul>

        </div>
        <div class="col-md-10">
            <h2>
                <span class="historyHeader">{{vm.selected.year}} - {{vm.selected.province}} - {{vm.selected.city}}</span>
            </h2>
            
            <iframe data-ng-if="!!vm.selected.year && !vm.selected.isMissing" ng-src="{{vm.getPdfLink(vm.selected.year)}}" style="width: 100%; height: 800px;"></iframe>
            <h4 ng-if="vm.selected.isMissing">MISSING {{vm.selected.year}}</h4>
        </div>
    </div>
</asp:Content>
