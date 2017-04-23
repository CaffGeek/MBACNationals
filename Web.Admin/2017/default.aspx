<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaPlaceHolder" runat="server">
    <meta property="og:url" content="http://mbacnationals.com/2017/index.php" />
    <meta property="og:image" content="http://mbacnationals.com/2017/images/Logo.png" />
    <meta property="og:title" content="2017 Master Bowlers Association National Championships :: Regina, SK" />
    <meta property="og:description" content="Home of the 2017 Master Bowlers Association of Canada Nationals" />
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_image_schedule.jpg'); "></div>
    <div id="photoCredit"><strong>Tiger Lilies</strong> &bull; Credit: Flickr Commons - Audrey</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">    
    <div class="col-md-8 col-sm-8">
        <div class="row welcome">
            <div class="col-md-12">
                <h2>Welcome</h2>
                <p>Welcome to the online home of the 2017 Master Bowlers Association of Canada Nationals, taking place June 29 - July 3, 2017 in Regina, SK.</p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6 col-sm-6" data-ng-controller="NewsController as vm">
                <h2>News</h2>
                <div ng-repeat="newsItem in vm.News">
                    <h4><span class="newsHeader">{{newsItem.Title}}</span></h4>
                    <p style="white-space: pre-wrap;">{{newsItem.Content}}</p>
                </div>
                <p><a class="btn btn-default" href="news.php" role="button">More News</a></p>
            </div>


            <div class="col-md-6 col-sm-6">
                <h2>Schedule</h2>
                <div class="message"></div>
                <!--<h3 id="day1" class='day'>Day 1 - Wednesday, June 28th</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Registrations for Early Arrivals<br />Participants & Guests<br /></p>
                <p><a class="btn btn-default" href="schedule.aspx" role="button">Full Schedule &raquo;</a></p>-->
            </div>
        </div>
    </div>
            
    <div class="col-md-4 col-sm-4">
    <br />
        <h2>SPONSORS</h2>
        <div style="height:320px;max-width:300px;" data-ng-controller="SponsorsController as vm">
            <div class="innerContainer">
            <a href="{{vm.CurrentSponsor.Website}}" target="_blank">
                <img ng-src="{{vm.ImageBase}}/Setup/Sponsors/Image/{{vm.CurrentSponsor.Id}}" style="width:100%;" alt="" border="0" />
            </a>
            </div>
        </div> 
         <hr />
<!--                
        <h2>Follow Us</h2>
        <h3>Newsletter</h3>


       
-->
        <div class="fb-page" style="margin-bottom: 15px;" data-href="https://www.facebook.com/MBAofCanada/" data-small-header="false" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="false">
            <div class="fb-xfbml-parse-ignore">
                <blockquote cite="https://www.facebook.com/MBAofCanada/"><a href="https://www.facebook.com/MBAofCanada/">Master Bowlers Association of Canada</a></blockquote>
            </div>
        </div>
                
        <hr />
                
        <div><a class="twitter-timeline" href="https://twitter.com/MBANationals" data-widget-id="702222211382259713">Tweets by @MBANationals</a><script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script></div>
                
        <hr />

        <div data-ng-controller="HighscoresController">
            <div class="section group" id="highScores" data-ng-include="" data-src="'/ClientApp/views/highscores.html'">
            </div>
        </div>
    </div>
</asp:Content>
