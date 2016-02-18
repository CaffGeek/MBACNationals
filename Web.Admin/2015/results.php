<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_3.jpg'); "></div>
				<div id="photoCredit"><strong>James N. Allan Skyway Bridge</strong> &bull; Credit: WikiCommons</div>
				</div>
			</div>

		<div class="section group content" data-ng-app="app">
      <div data-ng-controller="ResultsController">
				<div class="col span_1_of_3">
				  <div id="centres_links">
				  <!--<a href="">Coming Soon</a>-->
            <a ui-sref="standings({division: 'Tournament Men Single'})">TOURNAMENT MEN SINGLE</a><br />
            <a ui-sref="standings({division: 'Tournament Ladies Single'})">TOURNAMENT LADIES SINGLE</a><br />
            <a ui-sref="standings({division: 'Tournament Men'})">TOURNAMENT MEN</a><br />
            <a ui-sref="standings({division: 'Tournament Ladies'})">TOURNAMENT LADIES</a><br />
            <a ui-sref="standings({division: 'Teaching Men Single'})">TEACHING MEN SINGLE</a><br />
            <a ui-sref="standings({division: 'Teaching Ladies Single'})">TEACHING LADIES SINGLE</a><br />
            <a ui-sref="standings({division: 'Teaching Men'})">TEACHING MEN</a><br />
            <a ui-sref="standings({division: 'Teaching Ladies'})">TEACHING LADIES</a><br />
            <a ui-sref="standings({division: 'Seniors'})">SENIORS</a><br />
            <a ui-sref="standings({division: 'Seniors Single'})">SENIORS SINGLE</a><br />
            
            
            <hr/>
            <a href="#Standings" data-ng-click="viewStepladder()">STEPLADDER</a><br />
				  </div>
				</div>
        <div class="col span_2_of_3">
          <div ui-view=""></div>
          <!--<div class="col span_3_of_3" id="contentArea" data-ng-include="" data-src="viewUrl">

          </div>-->
          <h5 class="text-center">Results are unofficial</h5>
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

    <script src="/scripts/angular.min.js"></script>
    <script src="/scripts/angular-ui-router.min.js"></script>

    <script src="/ClientApp/app.js"></script>
    <script src="/ClientApp/Controllers/controller.results.js"></script>
    <script src="/ClientApp/Services/service.data.js"></script>
<script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

</body>
</html>