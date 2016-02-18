<?php 
include "header.php"; 
?> 

<body>

<div id="skiptomain"><a href="#maincontent">skip to main content</a></div>

<div id="wrapper" >
	<div id="maincontentcontainer" >
		<div id="maincontent">
			
			<div class="section group">
				<div class="col span_3_of_3" id="header">
				<div id="mobileLogo"><img src="images/2014_logo.png" /></div>
				<div id="logo"><img src="images/2014_logo.png" /></div>
				<div id="socialLinks">Join Us:&nbsp;&nbsp;<a href="https://www.facebook.com/MBAofCanada" target="_blank"><img src="images/facebook_icon.png" /></a> <a href="https://twitter.com/MBANationals"><img src="images/twitter_icon.png" /></a> <a href="http://instagram.com/mb_master_bowlers"><img src="images/instagram_icon.png" /></a></div>
				<div id="navigation">
          <nav class="nav-collapse" >
            <ul>
              <li><a href="index.php" class="first">Home</a></li>
              <li><a href="results.php">RESULTS</a></li>
              <li><a href="news.php">NEWS</a></li>
              <li><a href="schedule.php">SCHEDULE</a></li>
              <li><a href="lanedraw.php">LANE DRAW</a></li>
              <li><a href="photos.php">PHOTOS</a></li>
              <li><a href="contingents.php">CONTINGENTS</a></li>
			        <li><a href="http://www.tnmpromostore.com/default.aspx?business_id=2495&page=main" target="_blank">SOUVENIRS</a></li>
              <li><a href="centres.php">CENTRES</a></li>
              <li><a href="hotel.php">HOTEL</a></li>
            </ul>
          </nav>
        </div>				
				</div>
			</div>
			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_8.jpg'); "></div>
				<div id="photoCredit"><strong>Royal Winnipeg Ballet company dancers in Moulin Rouge - The Ballet</strong> &bull; Credit: Bruce Monk</div>
				</div>
			</div>

		<div class="section group content" data-ng-app="app">
      <div data-ng-controller="ResultsController">
				<div class="col span_1_of_3">
				  <div id="centres_links">
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

    <script src="/ClientApp/app.js"></script>
    <script src="/ClientApp/Controllers/controller.results.js"></script>
    <script src="/ClientApp/Services/service.data.js"></script>
<script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

</body>
</html>