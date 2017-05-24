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
                    <h4><span class="newsHeader" ng-bind-html="newsItem.Title"></span></h4>
                    <p style="white-space: pre-wrap;" ng-bind-html="newsItem.Content"></p>
                </div>
                <p><a class="btn btn-default" href="news.aspx" role="button">More News</a></p>
            </div>


            <div class="col-md-6 col-sm-6">
                <h2>Schedule</h2>
                <div class="row" data-ng-controller="ScheduleController">
                    <div data-ng-repeat="day in schedule.days | orderBy:''">
                        <div data-ng-if="schedule.days.indexOf(currentDate) == $index || (schedule.days.indexOf(currentDate) == -1 && $index == 0)">
                            <h4 id="{{day}}">Day {{$index + 1}} - {{day | date:'EEEE MMM d'}}</h4>

                            <div data-ng-repeat="event in schedule.events | filter:{key:day} | orderBy:'start'">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h5>
                                            {{event.start | date:'h:mma':'CST'}}
                                            <span data-ng-if="event.start != event.end">
                                            - {{event.end | date:'h:mma':'CST'}}
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
              
        <h2>Follow Us</h2>
        <h3>Newsletter</h3>
<!-- SendinBlue Signup Form HTML Code --><head> <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"> </head> <div id="sib_embed_signup" style="padding: 10px;"> <div class="forms-builder-wrapper" style="position:relative;margin-left: auto;margin-right: auto;"> <form class="description" id="theform" name="theform" action="https://my.sendinblue.com/users/subscribeembed/js_id/2dew5/id/1" method="POST"> <input type="hidden" name="js_id" id="js_id" value="2dew5"><input type="hidden" name="listid" id="listid" value="4"><input type="hidden" name="from_url" id="from_url" value="yes"><input type="hidden" name="hdn_email_txt" id="hdn_email_txt" value=""> <input type="hidden" name="sib_simple" value="simple"> <input type="hidden" name="sib_forward_url" value="http://mbacnationals.com/2017/" id="sib_forward_url"> <div class="sib-container rounded"> <input class="hidden" type="hidden" name="req_hid" id="req_hid" value="" style="font-family: &quot;Trebuchet MS&quot;, Verdana, Tahoma, Geneva, sans-serif; font-size: 13px;"> <div class="header" style="padding: 0px 5px;"> <h1 class="title editable" data-editfield="newsletter_name" style="font-family: Futura, &quot;Century Gothic&quot;, &quot;Apple Gothic&quot;, sans-serif; font-size: 24px; color: rgb(35, 35, 35); display: none;"></h1> <h3 id="company-name" style="font-family: Futura, &quot;Century Gothic&quot;, &quot;Apple Gothic&quot;, sans-serif; color: rgb(52, 52, 52); font-size: 14px; display: none;"></h3> </div> <div class="description editable" data-editfield="newsletter_description" style="font-family: Futura, &quot;Century Gothic&quot;, &quot;Apple Gothic&quot;, sans-serif; font-size: 11px; color: rgb(52, 52, 52); padding: 0px 5px 10px; display: none;"></div> <div class="view-messages" style=" margin:5px 0;"> </div> <!-- an email as primary --> <div class="primary-group email-group forms-builder-group ui-sortable" style=""> <div class="row mandatory-email" style="font-family: Futura, &quot;Century Gothic&quot;, &quot;Apple Gothic&quot;, sans-serif; font-size: 11px; color: rgb(52, 52, 52); padding: 5px;"> <input type="email" name="email" id="email" value="" style="padding: 4px; width: 80%; min-width: auto;"> <div style="clear:both;"></div> <div class="hidden-btns"> <a class="btn move" href="#"><i class="icon-move"></i></a><br> <!--<a class="btn btn-danger delete" href="#"><i class="icon-white icon-trash"></i></a>--> </div> </div> </div><div class="captcha forms-builder-group" style="display: none;"><div class="row" style="font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; color: rgb(52, 52, 52); font-size: 17px; padding: 10px 20px;"><div id="gcaptcha"></div></div></div> <!-- end of primary --> <div class="byline" style="font-family: Futura, &quot;Century Gothic&quot;, &quot;Apple Gothic&quot;, sans-serif; color: rgb(52, 52, 52);"> <button class="button editable " type="submit" data-editfield="subscribe" style="font-family: Futura, &quot;Century Gothic&quot;, &quot;Apple Gothic&quot;, sans-serif; font-size: 11px; color: rgb(255, 255, 255); background: rgb(5, 5, 5); height: auto; min-height: 20px; padding: 0px 10px; line-height: 150%;">Subscribe</button></div> <div style="clear:both;"></div> </div> </form> </div> </div> <!-- End : SendinBlue Signup Form HTML Code -->

       
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
