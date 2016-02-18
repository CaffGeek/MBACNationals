
<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_5.jpg'); "></div>
				<div id="photoCredit"><strong>Burlington, Ontario at Night</strong> &bull; Credit: WikiCommons</div>
				</div>
			</div>

			<div class="section group content" data-ng-app="app">
				<div class="col span_2_of_3">
					<div class="section group">
					  <div class="col span_2_of_2" id="welcome">
					    <h2>THANK YOU</h2>
					    <p>Our host committee would like to thank you all for coming to Burlington/Hamilton. We hope you all had a great time with memories that will last for ever. Congratulations to the winners, and to all who participated, you represented your province proud. Hope you all enjoy the rest of the summer and see you back on the lanes in the fall.</p>
              
					  </div>
					</div>
					<div class="section group">
					<div class="col span_1_of_2" id="news" data-ng-controller="NewsController as vm">
					  <h2>NEWS</h2>
            <div ng-repeat="newsItem in vm.News">
              <h4>
                <span style="color:#cc0000">{{newsItem.Title}}</span>
              </h4>
              <p style="white-space: pre-wrap;">{{newsItem.Content}}</p> 
            </div>
          </div>
					<div class="col span_1_of_2" id="schedule">
					<h2>FORMS</h2>
					<h4 id="day6">National Information for Provincial Presidents</h4>
					<p>Information on the 2015 Nationals<br /> <a href="images/forms/2015_MBAC_Nat_Inform_Pkg.pdf" target="_blank">Download PDF</a></p>
					<h4 id="day6">Guest Information Package</h4>
					<p>Guest Information for the 2015 Nationals<br /> <a href="images/forms/Guest_Info.pdf" target="_blank">Download PDF</a></p>

					<h2>SCHEDULE</h2>
					<h3 id="day6">Day 1 - saturday, june 27, 2014</h3>
          <h4>Saturday June 27th - Early Arrivals</h4>
          <p><Strong>11:30 am – 2:00 a.m.</strong><br />Arrivals & Bus Shuttles to Hotel<br />Participants & Guests</p>
					</div>
					</div>
				</div>
				<div class="col span_1_of_3">
					<div class="section group" id="sponsors">
            <div>
					    <div class="col span_2_of_2">
                <h2>SPONSORS</h2>
                <div style="height:250px;" data-ng-controller="SponsorsController as vm">
                  <div class="innerContainer">
                    <a href="{{vm.CurrentSponsor.Website}}" target="_blank">
                      <img ng-src="{{vm.ImageBase}}/Setup/Sponsors/Image/{{vm.CurrentSponsor.Id}}" style="width:100%;" alt="" border="0" />
                    </a>
                  </div>
                </div>          
					    </div>
              <div data-ng-controller="HighscoresController">
					      <div class="section group" id="highScores" data-ng-include="" data-src="'/ClientApp/views/highscores.html'">
                </div>
              </div>
            </div>
          </div>
				</div>
			</div>
		</div>
	</div>

<?php 
include "footer.php"; 
?>
</div>



	<!-- JavaScript at the bottom for fast page loading -->

	<!-- Grab Google CDN's jQuery, with a protocol relative URL; fall back to local if necessary -->
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
	<script>window.jQuery || document.write('<script src="js/jquery-1.7.2.min.js"><\/script>')</script>

	<!--[if (lt IE 9) & (!IEMobile)]>
	<script src="js/selectivizr-min.js"></script>
	<![endif]-->


	<!-- More Scripts-->
	<script src="js/responsivegridsystem.js"></script>

    <script src="/ClientApp/app.js"></script>
    <script src="/ClientApp/Controllers/controller.highscores.js"></script>
    <script src="/ClientApp/Controllers/controller.sponsors.js"></script>
    <script src="/ClientApp/Controllers/controller.news.js"></script>
    <script src="/ClientApp/Services/service.data.js"></script>
    <script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>
    
    

</body>
</html>