<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_image_schedule.jpg'); "></div>
    <div id="photoCredit"><strong>TODO: description</strong> &bull; Credit: TODO: credit</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="row" data-ng-controller="ScheduleController">
        <div class="col-md-2">
            <ul class="sidebarNav">
                <li data-ng-repeat="day in schedule.days | orderBy:''"><a href="##{{day}}">{{day | date:'MMMM d'}}</a></li>
            </ul>         
        </div>

        <div class="col-md-10">
            <h2>SCHEDULE</h2>

            Click here to <a href="https://calendar.google.com/calendar/ical/g5eelbnljr56imc0vkhah828lo%40group.calendar.google.com/public/basic.ics">Download Schedule</a>

            <div data-ng-repeat="day in schedule.days | orderBy:''">
                <h4 id="{{day}}">Day {{$index + 1}} - {{day | date:'EEEE MMM d'}}</h4>

                <div data-ng-repeat="event in schedule.events | filter:{key:day} | orderBy:'start'">
                    <div class="row">
                        <div class="col-sm-3">
                            <h5>
                                {{event.start | date:'h:mma':'MDT'}}
                                <span data-ng-if="event.start != event.end">
                                - {{event.end | date:'h:mma':'MDT'}}
                                </span>
                            </h5>
                        </div>
                        <div class="col-sm-6">
                            <b>{{event.summary}}</b>
                            <br />
                            {{event.description}}
                        </div>
                        <div class="col-sm-3">
                            {{event.location}}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>