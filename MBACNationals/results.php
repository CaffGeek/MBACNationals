<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_1.jpg'); "></div>
				<div id="photoCredit"><strong>Burlington</strong> &bull; Credit: </div>
				</div>
			</div>

		<div class="section group content" data-ng-app="app">
      <div data-ng-controller="ResultsController">
				<div class="col span_1_of_3">
				  <div id="centres_links">
				  <a href="">Coming Soon</a><!--
            <a href="#Standings" data-ng-click="viewStandings('Tournament Men Single')">TOURNAMENT MEN SINGLE</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Tournament Ladies Single')">TOURNAMENT LADIES SINGLE</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Tournament Men')">TOURNAMENT MEN</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Tournament Ladies')">TOURNAMENT LADIES</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Teaching Men Single')">TEACHING MEN SINGLE</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Teaching Ladies Single')">TEACHING LADIES SINGLE</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Teaching Men')">TEACHING MEN</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Teaching Ladies')">TEACHING LADIES</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Seniors')">SENIORS</a><br />
            <a href="#Standings" data-ng-click="viewStandings('Seniors Single')">SENIORS SINGLE</a><br />-->
				  </div>
				</div>
        <div class="col span_2_of_3">
          <div class="col span_3_of_3" id="contentArea" data-ng-include="" data-src="viewUrl">
              <h2>RESULTS</h2>

          </div>
         <!-- <h5 class="text-center">Results are unofficial</h5>-->
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
    <script src="app/Controllers/controller.results.js"></script>
    <script src="app/Services/service.data.js"></script>
<script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

</body>
</html>