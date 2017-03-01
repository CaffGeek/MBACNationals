<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_news.jpg');"></div>
    <div id="photoCredit"><strong>Regina's Downtown Skyline</strong> &bull; Credit: Flickr Commons - daryl_mitchell</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="row" data-ng-controller="NewsController as vm ">
        <div class="col-md-2">
            <ul class="sidebarNav">
                <li><a href="" ng-click="vm.selectedMonth = ''">All</a></li>
                <div ng-repeat="month in vm.Months">
                    <li><a href="" ng-click="vm.selectedMonth = month.name">{{month.name}}</a></li>
                </div>
            </ul>

        </div>
        <div class="col-md-10">
            <div ng-repeat="newsItem in vm.News | filter:vm.filterByMonth(vm.selectedMonth) | orderBy: '-Created'">
                <h4>
                    <span class="newsHeader" ng-bind-html="newsItem.Title"></span>
                </h4>
                <p style="white-space: pre-wrap;" ng-bind-html="newsItem.Content"></p>
            </div>
        </div>
    </div>
</asp:Content>
