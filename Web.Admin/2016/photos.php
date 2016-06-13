<?php

include 'header.php';

?>
 
<div id="headerContainer" class="container"  >
    	<div class="row">
        	<div class="col-md-12">
    			<div id="logo" class="shadowed" style="position: absolute;left: 50%; transform: translate(-50%, 0);margin-top:-35px;position:absolute;z-index:10;"><img src="images/2016_Logo.png" alt="2016 Masters Nationals" /></div>
    			<div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_image_8.jpg'); "></div>
				<div id="photoCredit"><strong>Carnival Rides at 2010 Cloverdale Rodeo</strong> &bull; Credit: Denise M, WikiCommons</div>
			</div>
		</div>
</div>
</header>
    

    <div class="container">
    	
      <!-- Example row of columns -->
      <div class="row">
        <div class="col-md-2">
        <ul class="sidebarNav">
       			<li><a href="#day1">Day 1</a></li>
                <li><a href="#day2">Day 2</a></li>
                <li><a href="#day3">Day 3</a></li>
                <li><a href="#day4">Day 4</a></li>
                <li><a href="#day5">Day 5</a></li>
                <li><a href="#day6">Day 6</a></li>
                <li><a href="#day7">Day 7</a></li>
                <li><a href="#instagram">Instagram</a></li>

    	</ul>
          
        </div>
    	<div class="col-md-10">
        <h2>PHOTOS</h2>
      	<div id="galleria"></div>

      	</div>
<!--      	<div id="day2">
      		<h2>Day 2</h2>
      	</div>
      	<div id="day3">
      		<h2>Day 3</h2>
      	</div>
      	<div id="day4">
      		<h2>Day 4</h2>
      	</div>
      	<div id="day5">
      		<h2>Day 5</h2>
      	</div>
      	<div id="day6">
      		<h2>Day 6</h2>
      	</div>
      	<div id="day7">
      		<h2>Day 7</h2>
      	</div>-->

      	<div id="instagram">
      		<script type="text/javascript">
  			  var feed = new Instafeed({
      		  get: 'user',
 		      userId: '1745902510',
			  clientId: '8dff542608854143b95ffae445a35390'
    			});
  			  feed.run();
			</script>
		</div>
		
      </div>
      </div>

<?php

include 'footer.php';

?>