<?php

include 'header.php';

?>
 
<div id="headerContainer" class="container"  >
    	<div class="row">
        	<div class="col-md-12">
    			<div id="logo" class="shadowed" style="position: absolute;left: 50%; transform: translate(-50%, 0);margin-top:-35px;position:absolute;z-index:10;"><img src="images/2016_Logo.png" alt="2016 Masters Nationals" /></div>
    			<div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_1.png'); "></div>
				<div id="photoCredit"><strong>Burlington, Ontario at Night</strong> &bull; Credit: WikiCommons</div>
			</div>
		</div>
</div>
</header>
    

    <div class="container">
    	
      <!-- Example row of columns -->
      <div class="row"  data-ng-app="app">
        <div class="col-md-2"  data-ng-controller="ResultsController">
        <ul class="sidebarNav">
       	<!--<li><a href="">Coming Soon</a></li>-->
            <li><a ui-sref="standings({division: 'Tournament Men Single'})">TOURNAMENT MEN SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Tournament Ladies Single'})">TOURNAMENT LADIES SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Tournament Men'})">TOURNAMENT MEN</a></li>
            <li><a ui-sref="standings({division: 'Tournament Ladies'})">TOURNAMENT LADIES</a></li>
            <li><a ui-sref="standings({division: 'Teaching Men Single'})">TEACHING MEN SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Teaching Ladies Single'})">TEACHING LADIES SINGLE</a></li>
            <li><a ui-sref="standings({division: 'Teaching Men'})">TEACHING MEN</a></li>
            <li><a ui-sref="standings({division: 'Teaching Ladies'})">TEACHING LADIES</a></li>
            <li><a ui-sref="standings({division: 'Seniors'})">SENIORS</a></li>
            <li><a ui-sref="standings({division: 'Seniors Single'})">SENIORS SINGLE</a></li>
            <hr/>
            <li><a href="#Standings" data-ng-click="viewStepladder()">STEPLADDER</a></li>
    	</ul>
        </div>

        <div class="col-md-10">
        <h2>RESULTS</h2>
      		<div ui-view=""></div>
      		<h5 class="text-center">Results are unofficial</h5>
	    </div>
      </div>

<?php

include 'footer.php';

?>