<%@  Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_location.jpg');"></div>
    <div id="photoCredit"><strong>Benches in Victoria Park</strong> &bull; Credit: WikiCommons - Ccyyrree</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">

    <!-- Example row of columns -->
    <div data-ng-controller="HotelsController as vm">
        <div class="row hotel" ng-repeat="hotel in vm.Hotels">
            <div class="col-md-2 col-sm-2">
            </div>
            <div class="col-md-10 col-sm-10">
                <h2>Location</h2>
                <h4 id="hotel">{{hotel.Name}}</h4>
                <div class="row">
                    <div class="col-md-3 col-sm-3">
                        <img ng-src="/Setup/Hotels/Logo/{{hotel.Id}}" alt="{{hotel.Name}}" border="0" />
                        <p>Visit their website</p>
                        <p><a href="{{hotel.Website}}" target="_blank">{{hotel.Name}}</a></p>
                        <p><a href="tel:{{hotel.PhoneNumber}}" target="_blank">{{hotel.PhoneNumber}}</a></p>
                    </div>
                    <div class="col-md-9 col-sm-9">
                        <img ng-src="/Setup/Hotels/Image/{{hotel.Id}}" alt="{{hotel.Name}}" border="0" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
