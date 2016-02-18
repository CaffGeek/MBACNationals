<?php 
include "header.php"; 
?>

        <div class="section group">
          <div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_6.jpg'); "></div>
				<div id="photoCredit"><strong>Hamilton Skyline</strong></div>
         </div>
        </div>

        <div class="section group content" data-ng-app="app">
          <div data-ng-controller="ScheduleController">
            <div class="col span_1_of_3">
              <div id="centres_links">
              <a href="images/forms/souvenirs.pdf"> catalogue</a><br />
			 <a href="images/forms/Souvenir_Order_Form.xlsx"> order form</a>
              </div>
            </div>
            <div class="col span_2_of_3" id="contentArea">

              <h2>souvenirs</h2>
				<h4 style="color:#cc0000">Discount offered on Souvenir order prior to May 15th</h4><p>Order 1 item receive a <strong>5% discount</strong> off pricing shown<br />Order 2 or more items receive a <strong>10% discount</strong> off pricing shown</p>
              <p><a href="images/forms/souvenirs.pdf" target="_blank">Click here</a> to download the catalogue of available souvenirs</p>
              <p><a href="images/forms/Souvenir_Order_Form.xlsx" target="_blank">Click here</a> to download the order form</p>
              <p><a href="images/SizingInformation.jpg" target="_blank">Click here</a> to view sizing information.</p>
              <p><a href="images/forms/souvenirs.pdf" target="_blank"><img src="images/catalogue.jpg" alt="catalogue" style="width:48%;margin-right:1%;border:1px solid #ccc;"/></a> <a href="images/forms/Souvenir_Order_Form.xlsx" target="_blank"><img src="images/Souvenir_Order_Form.jpg" alt="Order Form" style="width:48%;border:1px solid #ccc;"/></a></p>
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
  <script>
    window.jQuery || document.write('<script src="js/jquery-1.7.2.min.js">
      <\/script>')
    </script>

    <!--[if (lt IE 9) & (!IEMobile)]>
	<script src="js/selectivizr-min.js"></script>
	<![endif]-->


    <!-- More Scripts-->
    <script src="js/responsivegridsystem.js"></script>

    <script src="/ClientApp/app.js"></script>
    <script src="/ClientApp/Controllers/controller.schedule.js"></script>
    <script src="/ClientApp/Services/service.data.js"></script>
    
    <script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

  </body>
</html>