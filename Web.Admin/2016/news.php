<?php

include 'header.php';

?>
 
<div id="headerContainer" class="container"  >
    	<div class="row">
        	<div class="col-md-12">
    			<div id="logo" class="shadowed" style="position: absolute;left: 50%; transform: translate(-50%, 0);margin-top:-35px;position:absolute;z-index:10;"><img src="images/2016_Logo.png" alt="2016 Masters Nationals" /></div>
    			<div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_image_6.jpg'); "></div>
				<div id="photoCredit"><strong>King George Boulevard Bridge over Nicomekl River</strong> &bull; Credit: WikiCommons</div>
			</div>
		</div>
</div>
</header>
    

    <div class="container" data-ng-app="app">
    	
      <!-- Example row of columns -->
      <div class="row" data-ng-controller="NewsController as vm ">
        <div class="col-md-2">
        <ul class="sidebarNav">
			<li><a href="" ng-click="vm.selectedMonth = ''">All</a></li>
          <div ng-repeat="month in vm.Months">
            <li><a href="" ng-click="vm.selectedMonth = month.name">{{month.name}}</a></li>
          </div>    	
        </ul>
          
        </div>
        <div class="col-md-10">
        <div ng-repeat="newsItem in vm.News | filter:vm.filterByMonth(vm.selectedMonth) | orderBy: '-Created'">
          <h4>
            <span class="newsHeader" ng-bind-html="newsItem.Title"></span>
          </h4>
          <p style="white-space: pre-wrap;" ng-bind-html="newsItem.Content"></p>
        </div>
      </div>
      </div>

<?php

include 'footer.php';

?>

