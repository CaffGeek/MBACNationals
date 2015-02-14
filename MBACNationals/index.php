
<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_1.jpg'); "></div>
				<div id="photoCredit"><strong>Burlington</strong> &bull; Credit: </div>
				</div>
			</div>

			<div class="section group content">
				<div class="col span_2_of_3">
					<div class="section group">
					<div class="col span_2_of_2" id="welcome">
					<h2>WELCOME</h2>
					<p>Hi Everyone, and Welcome to the 2015 MBAC Nationals Website!
</p><p>
The Masters of Manitoba hope you find the site informative, and easy to use, as those were our goals during the construction and design process.
</p><p>
To those of you joining us in Winnipeg for the National championship, we hope you enjoy your stay in the Exchange District, the heart of the city, where you will find all sorts of entertainment venues and diverse restaurants to take your mind off the stress of competition.
</p><p>
Best wishes to all of you for success at the upcoming event.
</p><p>
Doug Wood<br />
President, Master Bowlers Association of Manitoba 					</p>
					</div>
					</div>
					<div class="section group">
					<div class="col span_1_of_2" id="news">
					<h2>NEWS</h2>
					<h4>February, 2015<br />National Information for Provincial Presidents</h4><p>Information on the 2015 Nationals<br /> <a href="images/forms/2015_MBAC_Nat_Inform_Pkg.pdf">Download PDF</a></p>
					<h4>February, 2015<br />Web Site Launch</h4><p>Welcome to the 2015 MBAC Nationals Website, please return for future information.</p>
					</div>
					<div class="col span_1_of_2" id="schedule">
					<h2>TODAY'S SCHEDULE</h2>
					<h3 id="day6"></h3>
					<h4>Coming Soon</h4><p><strong></p>
					</div>
					</div>
				</div>
				<div class="col span_1_of_3">
					<div class="section group" id="sponsors">
					<div class="col span_2_of_2">
					<h2>SPONSORS</h2>
					<div style="height:250px;" class="outerContainer">
					<div class="innerContainer"><!--<a href="index.html">--><img id="r1" src="images/sponsors/Cooperators.jpg" style="width:100%;" alt="" border="0"><!--</a>--></div>
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