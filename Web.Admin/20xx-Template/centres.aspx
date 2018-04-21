<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_centres.jpg');"></div>
    <div id="photoCredit"><strong>TODO: description</strong> &bull; Credit: TODO: credit</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div data-ng-controller="CentresController as vm">
        <div class="col-md-2">
            <ul class="sidebarNav">
                <li ng-repeat="centre in vm.Centres"><a href="##{{centre.Id}}">{{centre.Name}}</a></li>
            </ul>

        </div>

        <div class="col-md-10" >
            <h2>Centres</h2>

            <div class="row centre" ng-repeat="centre in vm.Centres">
                <h4 id="{{centre.Id}}">{{centre.Name}}</h4>
                <div class="container">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 centreLogo">
                            <img ng-src="/Setup/Centres/Image/{{centre.Id}}" alt="{{centre.Name}}" border="0" />
                        </div>
                        <div class="col-md-9 col-sm-9 centreDetails">
                            <p><strong><a href="tel:{{centre.PhoneNumber}}" target="_blank">{{centre.PhoneNumber}}</a></strong></p>
                            <p><strong>{{centre.Address}}</strong></p>
                            <p>
                                For more information, check out their website: <a href="{{centre.Website}}" target="_blank">{{centre.Website}}</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

