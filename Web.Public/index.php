
<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_5.jpg'); "></div>
				<div id="photoCredit"><strong>Burlington, Ontario at Night</strong> &bull; Credit: WikiCommons</div>
				</div>
			</div>

			<div class="section group content">
				<div class="col span_2_of_3">
					<div class="section group">
					<div class="col span_2_of_2" id="welcome">
					<h2>WELCOME</h2>
					<p>The Master Bowlers Association of Ontario and its host committee welcome you to Ontario and the Burlington/Hamilton area.  Our group of volunteers is excited and privileged to work on your behalf to put forth a memorable experience for you.</p>
<p>Our host hotel is the Holiday Inn Burlington Hotel & Conference Centre.  This hotel is a full service facility with an indoor pool, whirlpool, sauna, restaurant, and free high speed internet and features a Holidome atrium.  It is located approximately 20 minutes from the bowling centre that will be hosting our National Championships.  There are several restaurants across from the hotel, a mall less than a10 minute walk and the waterfront a short 5 minute cab ride.  All hotel reservations must be done through the Master Bowlers Association of Ontario.</p>
<p>Our host bowling centre will be Sherwood Centre, with 48 lanes allows all participants to be in one location allowing guests and provincial contingents access to support their province.</p>
<p>Our theme for the event is “Las Vegas – Let’s Party”.  We encourage all participants to dress up in Vegas style performer attire; this can be of the Hotel gambling & entertainment style, or your favorite performer in Vegas, or even the street entertainers that are so plentiful in Vegas.  For Ontario night we ask that our female Rookies be attired in Elvis traditional wear, while our male Rookies are attired in typical Vegas Show Girl style. </p>
<p>We all look forward to seeing you all in June.</p>
<p>Mike Bates, MBAC Nationals Hosting Chair & Brenda Walters, President, MBAO</p>
					</div>
					</div>
					<div class="section group">
					<div class="col span_1_of_2" id="news">
					<h2>NEWS</h2>
					<h4>March, 2015<br /><span style="color:#cc0000">Discount offered on Souvenir order prior to May 15th</span></h4><p>Order 1 item receive a <strong>5% discount</strong> off pricing shown</p><p>Order 2 or more items receive a <strong>10% discount</strong> off pricing shown</p>
					<h4>March, 2015<br />Web Site Launch</h4><p>Welcome to the 2015 MBAC Nationals Website, please return for future information.</p>
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
					<div class="col span_2_of_2">
					<h2>SPONSORS</h2>
					<div style="height:250px;" class="outerContainer">
					<div class="innerContainer"><a href="http://www.tourismburlington.com/stay/hotels-motels/burlington-hotel-association/" target="_blank"><img id="r1" src="images/sponsors/BurlingtonHotel.jpg" style="width:100%;" alt="" border="0"></a></div>
					</div>

					</div>
					</div>
          <div data-ng-app="app">
            <div data-ng-controller="HighscoresController">
					    <div class="section group" id="highScores" data-ng-include="" data-src="'app/views/highscores.html'">
              </div>
					    <!--<div class="col span_2_of_2">
					    <h2>HIGH SCORES</h2>
					    <h4>Tournament Division</h4>
					    <h5>Men</h5>
					    <ul>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    </ul>
					    <h5>Women</h5>
					    <ul>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    </ul>
					    <h4>Teaching Division</h4>
					    <h5>Men</h5>
					    <ul>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    </ul>
					    <h5>Women</h5>
					    <ul>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    </ul>
					    <h4>Seniors</h4>
					    <h5>Men</h5>
					    <ul>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    </ul>
					    <h5>Women</h5>
					    <ul>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    <li>Name - 000</li>
					    </ul>
					
					    </div>-->
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

    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.0-beta.7/angular.js"></script>

    <script src="app/app.js"></script>
    <script src="app/Controllers/controller.highscores.js"></script>
    <script src="app/Services/service.data.js"></script>
<script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>
    
    

</body>
</html>