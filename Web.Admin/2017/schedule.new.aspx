<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_image_schedule.jpg'); "></div>
    <div id="photoCredit"><strong>Tiger Lilies</strong> &bull; Credit: Flickr Commons - Audrey</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <h2>SCHEDULE</h2>

    <div class="row" data-ng-controller="ScheduleController">
        <div data-ng-repeat="event in schedule.items">
            <h4>{{event.summary}}</h4>
            <h5>{{event.description}}</h5>
            <h6>{{event.start.dateTime | date:'h:mma'}} - {{event.end.dateTime | date:'h:mma'}} @ {{event.location}}</h6>
        </div>
    </div>
</asp:Content>


