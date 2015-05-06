<?php 
include "header.php"; 
?> 


			<div class="section group">
				<div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_10.jpg'); "></div>
				<div id="photoCredit"><strong>Downtown Hamilton</strong></div>
				</div>
			</div>

		  <div class="section group content" data-ng-app="app">
        <div data-ng-controller="ContingentController">
				  <div class="col span_4_of_12">
				    <div id="centres_links">
				      <a href="" data-ng-click="loadContingent('BC')">British Columbia</a><br />
				      <a href="" data-ng-click="loadContingent('AB')">Alberta</a><br />
				      <a href="" data-ng-click="loadContingent('SK')">Saskatchewan</a><br />
				      <a href="" data-ng-click="loadContingent('MB')">Manitoba</a><br />
				      <a href="" data-ng-click="loadContingent('NO')">Northern Ontario</a><br />
				      <a href="" data-ng-click="loadContingent('SO')">Southern Ontario</a><br />
				      <a href="" data-ng-click="loadContingent('QC')">Quebec</a><br />
				      <a href="" data-ng-click="loadContingent('NL')">Newfoundland &amp; Labrador</a>
				    </div>
				  </div>
				  <div class="col span_8_of_12" id="contentArea">

            <h2>{{model.Province}} Contingents</h2>
            <p>* Rookie</p>

            <div class="col span_5_of_12" data-ng-repeat="team in model.Teams | orderBy:'-1*Name.length'" data-ng-show="team.Id">
              <h5>{{team.Name}}</h5>

              <ol style="list-style-type:none;">
                <li data-ng-if="team.Coach.Id">
                  <span class="col span_9_of_12">
                    <strong>Coach:</strong> {{team.Coach.Name || 'Vacancy'}}
                  </span>
                  <span class="col span_2_of_12 text-right" data-ng-if="team.RequiresAverage">{{totalAverage(team)}}</span>
                </li>
                <li data-ng-repeat="bowler in team.Bowlers">
                  <span class="col span_9_of_12">
                    {{bowler.Name || 'Vacancy'}}
                    <span data-ng-show="bowler.IsRookie">
                      <sup>*</sup>
                    </span>
                    <span data-ng-show="bowler.IsDelegate">
                      <sup>D</sup>
                    </span>
                  </span>
                  <span data-ng-if="team.Name.indexOf('Teaching') >= 0 || team.Name.indexOf('Seniors') >= 0" class="col span_1_of_3 text-right">
                    <span data-ng-if="team.RequiresAverage" class="col span_2_of_12 text-right">
                      {{bowler.Average}}
                    </span>
                  </span>
                </li>
              </ol>
            </div>

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

    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.0-beta.7/angular.js"></script>

    <script src="app/app.js"></script>
    <script src="app/Controllers/controller.contingent.js"></script>
    <script src="app/Services/service.data.js"></script>

    <script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

</body>
</html>