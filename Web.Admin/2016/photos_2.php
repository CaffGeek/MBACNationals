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
       			<li><a href="photos.php">Day 1</a></li>
                <li><a href="photos_2.php">Day 2</a></li>
                <li><a href="photos_3.php">Day 3</a></li>
                <li><a href="photos_4.php">Day 4</a></li>
                <li><a href="photos_5.php">Day 5</a></li>
                <li><a href="photos_6.php">Day 6</a></li>
                <li><a href="photos_7.php">Day 7</a></li>
                <li><a href="#instagram">Instagram</a></li>

    	</ul>
          
        </div>
    	<div class="col-md-10">
        <h2>PHOTOS DAY 2</h2>
      	<div id="galleria"></div>


      	</div>


      	<div id="instagram">

			<div id="instafeed"></div>
		</div>
		
      </div>
      </div>



      <hr>

      <footer>
      	<ul class="footerNav">
      			<li><a href="index.php">Home</a></li>
                <li><a href="results.php">Results</a></li>
                <li><a href="news.php">News</a></li>
                <li><a href="schedule.php">Schedule</a></li>
                <li><a href="lanedraw.php">Lane Draw</a></li>
                <li><a href="photos.php">Photos</a></li>
                <li><a href="contingents.php">Contingents</a></li>
                <li><a href="http://www.mbaofbc.com/shopping-cart.html">Souvenirs</a></li>
                <li><a href="centres.php">Centres</a></li>
                <li><a href="hotel.php">Location</a></li>
    	</ul>
        <p>&copy; Site Design by Charlene McIvor & Chad Hurd</p>
      </footer>
    </div> <!-- /container -->


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/vendor/jquery.min.js"><\/script>')</script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
	<script type='text/javascript' src='https://my.sendinblue.com/public/theme/version3/js/subscribe-validate.js'></script>
	<script type='text/javascript'>
	    jQuery.noConflict(true);
	</script>
    <script src="bootstrap/js/ie10-viewport-bug-workaround.js"></script>
    <script>
    $(window).scroll(function() {
    if($(this).scrollTop() > 100) {
        $('.navbar-fixed-top').addClass('opaque');
    } else {
        $('.navbar-fixed-top').removeClass('opaque');
    }
});



</script>
	<!-- More Scripts-->

    <script src="/ClientApp/app.js"></script>
    <script src="/ClientApp/Controllers/controller.contingent.js"></script>
    <script src="/ClientApp/Controllers/controller.highscores.js"></script>
    <script src="/ClientApp/Controllers/controller.news.js"></script>
    <script src="/ClientApp/Controllers/controller.results.js"></script>
    <script src="/ClientApp/Controllers/controller.schedule.js"></script>
    <script src="/ClientApp/Controllers/controller.sponsors.js"></script>
    <script src="/ClientApp/Services/service.data.js"></script>
    
    <script src="http://mbacnationals.com/2015/js/galleria/galleria-1.4.2.min.js"></script>
<script src="http://mbacnationals.com/2015/js/galleria/plugins/facebook/galleria.facebook.js"></script>
<script>
Galleria.loadTheme('http://mbacnationals.com/2015/js/galleria/themes/classic/galleria.classic.min.js');
Galleria.run('#galleria', {
 facebook: 'album:1129830643748930',
 height: 550,
 lightbox: true,
 facebookOptions: {
   max: 100,
   facebook_access_token: '871325676235910|5640fa457799c71eeace0176717512b2'
 }
});
</script>
	<script type="text/javascript" src="http://mbacnationals.com/2016/js/instafeed.min.js"></script>


<script type="text/javascript">
    var feed = new Instafeed({
        get: 'tagged',
        tagName: 'awesome',
		clientId: '8dff542608854143b95ffae445a35390'
    });
    feed.run();
</script>  </body>
</html>
