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
        <h2>PHOTOS DAY 1</h2>
      	<div id="galleria"></div>

<script src="http://mbacnationals.com/2015/js/galleria/galleria-1.4.2.min.js"></script>
<script src="http://mbacnationals.com/2015/js/galleria/plugins/facebook/galleria.facebook.js"></script>
<script>
Galleria.loadTheme('http://mbacnationals.com/2015/js/galleria/themes/classic/galleria.classic.min.js');
Galleria.run('#galleria', {
 facebook: 'album:967252896673373',
 height: 550,
 lightbox: true,
 facebookOptions: {
   max: 100,
   facebook_access_token: '871325676235910|5640fa457799c71eeace0176717512b2'
 }
});
</script>
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
			<div id="instafeed"></div>
		</div>
		
      </div>
      </div>

<?php

include 'footer.php';

?>