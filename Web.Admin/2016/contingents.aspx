<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_3.jpg');"></div>
    <div id="photoCredit"><strong>Surrey Museum in Cloverdale, BC</strong> &bull; Credit: David Stanley, Flickr Commons</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="row" data-ng-controller="ContingentController">
        <div class="col-md-2">
            <ul class="sidebarNav">
                <li><a href="" data-ng-click="loadContingent('BC')">British Columbia</a></li>
                <li><a href="" data-ng-click="loadContingent('AB')">Alberta</a></li>
                <li><a href="" data-ng-click="loadContingent('SK')">Saskatchewan</a></li>
                <li><a href="" data-ng-click="loadContingent('MB')">Manitoba</a></li>
                <li><a href="" data-ng-click="loadContingent('NO')">Northern Ontario</a></li>
                <li><a href="" data-ng-click="loadContingent('SO')">Southern Ontario</a></li>
                <li><a href="" data-ng-click="loadContingent('QC')">Quebec</a></li>
                <li><a href="" data-ng-click="loadContingent('NL')">Newfoundland &amp; Labrador</a></li>
            </ul>
        </div>

        <div class="col-md-10">
            <h2>{{model.Province}} Contingent</h2>
            <div class="row">
                <h5 class="col-md-2">Manager</h5>
                <p class="col-md-2" data-ng-repeat="person in managers">{{person.Name}}</p>
            </div>
            <div class="row">
                <h5 class="col-md-2">Delegates</h5>
                <p class="col-md-2" data-ng-repeat="person in delegates">{{person.Name}}</p>
            </div>

            <p>* Rookie </p>

            <div class="contingentColumn" style="width: 40.7%; display: block; float: left; margin: 1% 0 1% 1.6%;" data-ng-repeat="team in model.Teams | orderBy:'-1*Name.length'" data-ng-show="team.Id">
                <h5>
                    <a href="results.aspx#/team/{{team.Id}}">{{team.Name || 'Vacancy'}}</a>
                </h5>

                <ol style="list-style-type: none;">
                    <li data-ng-if="team.Coach.Id">
                        <strong>Coach:</strong> {{team.Coach.Name || 'Vacancy'}}
                <span data-ng-show="team.Coach.IsRookie">
                    <sup>*</sup>
                </span>
                        <span data-ng-show="team.Coach.IsDelegate">
                            <sup>D</sup>
                        </span>
                        <span data-ng-show="team.Coach.IsManager">
                            <sup>M</sup>
                        </span>
                    </li>
                    <li data-ng-repeat="bowler in team.Bowlers">
                        <span data-ng-if="team.Name.indexOf('Teaching') >= 0 || team.Name.indexOf('Seniors') >= 0" style="float: right;">
                            <span data-ng-if="team.RequiresAverage" class=" text-right" data-ng-class="{strike: !!bowler.ReplacedBy}">{{bowler.Average}} 
                            </span>
                        </span>
                        <a data-ng-class="{strike: !!bowler.ReplacedBy}"
                            href="results.aspx#/bowler/{{bowler.Id}}">{{bowler.Name || 'Vacancy'}}</a>
                        <span data-ng-show="bowler.IsRookie">
                            <sup>*</sup>
                        </span>
                        <span data-ng-show="bowler.IsDelegate">
                            <sup>D</sup>
                        </span>
                        <span data-ng-show="bowler.IsManager">
                            <sup>M</sup>
                        </span>
                    </li>
                    <li>
                        <span data-ng-if="team.RequiresAverage" style="float: right; font-weight: bold;">{{totalAverage(team)}}</span>
                    </li>
                </ol>
            </div>
        </div>
    </div>
</asp:Content>
