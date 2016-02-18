<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_2.jpg'); "></div>
				<div id="photoCredit"><strong>Spencer Smith Park</strong> &bull; Credit: Andrew Lynes - WikiCommons</div>
				</div>
			</div>

		<div class="section group content" data-ng-app="app">
      <div data-ng-controller="NewsController as vm">
        <div class="col span_1_of_3">
          <a href="" ng-click="vm.selectedMonth = ''">All</a><br/>
          <div ng-repeat="month in vm.Months">
            <a href="" ng-click="vm.selectedMonth = month.name">{{month.name}}</a><br/>
          </div>
        </div>
        <div class="col span_2_of_3">
        <h2>NEWS</h2>
          <div ng-repeat="newsItem in vm.News | filter:vm.filterByMonth(vm.selectedMonth)">
          <h4>
            <span style="color:#cc0000">{{newsItem.Title}}</span>
          </h4>
          <p style="white-space: pre-wrap;">{{newsItem.Content}}</p>
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
  <script src="/ClientApp/Controllers/controller.news.js"></script>
  <script src="/ClientApp/Services/service.data.js"></script>
  <script>
    // Init responsive-nav.js
    var nav = responsiveNav("#nav");
  </script>

</body>
</html>