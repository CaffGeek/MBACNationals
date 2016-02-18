<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_7.jpg'); "></div>
				<div id="photoCredit"><strong>Gore Park Fountain</strong></div>
				</div>
			</div>

		<div class="section group content">
				<div class="col span_1_of_3">
				<div id="centres_links">
				<a href="photos.php">Participation Awards</a><br />
				<a href="photos_teams.php">Teams & Singles</a><br />
				<a href="photos_vegas.php">Vegas Night</a><br />
				<a href="photos_Winners.php">Victory Banquet</a><br /> 
				</div>
				</div>
				<div class="col span_2_of_3" id="contentArea">
					
					<h2>PHOTOS - Participation Awards</h2>
					<div id="galleria"></div>
					
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
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
<script>
    if (!window.jQuery) { document.write('<script src="js/jquery.min.js"><\/script>'); }
</script>

<script src="js/galleria/galleria-1.4.2.min.js"></script>
<script src="js/galleria/plugins/facebook/galleria.facebook.js"></script>
<script>
Galleria.loadTheme('js/galleria/themes/classic/galleria.classic.min.js');
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

	<!--[if (lt IE 9) & (!IEMobile)]>
	<script src="js/selectivizr-min.js"></script>
	<![endif]-->


	<!-- More Scripts-->
	<script src="js/responsivegridsystem.js"></script>
<script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

</body>
</html>