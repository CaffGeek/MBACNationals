<%@  Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_sponsors.jpg');"></div>
    <div id="photoCredit"><strong></strong> &bull;</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <h2>Sponsors</h2>
    <div data-ng-controller="SponsorsController as vm">
        <div class="row col-md-3 col-md-offset-1" ng-repeat="sponsor in vm.Sponsors">
            <a href="{{sponsor.Website}}" target="_blank">
                <img ng-src="{{vm.ImageBase}}/Setup/Sponsors/Image/{{sponsor.Id}}" style="width:100%;" alt="" border="0" />
            </a>
        </div>
    </div>
</asp:Content>
