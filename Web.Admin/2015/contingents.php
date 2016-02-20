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
              <h5>
                <a href="results.php#/team/{{team.Id}}">{{team.Name || 'Vacancy'}}</a>
              </h5>

              <ol style="list-style-type:none;">
                <li data-ng-if="team.Coach.Id">                  
                  <strong>Coach:</strong> {{team.Coach.Name || 'Vacancy'}}
                  <span data-ng-show="team.Coach.IsRookie">
                    <sup>*</sup>
                  </span>
                  <span data-ng-show="team.Coach.IsDelegate">
                    <sup>D</sup>
                  </span>
                  <span data-ng-show="team.Coach.IsManager">
                    <sup>M</sup>
                  </span>
                </li>
                <li data-ng-repeat="bowler in team.Bowlers">
                  <span data-ng-if="team.Name.indexOf('Teaching') >= 0 || team.Name.indexOf('Seniors') >= 0" style="float:right;">
                    <span data-ng-if="team.RequiresAverage" class=" text-right">
                      {{bowler.Average}}
                    </span>
                  </span>
                  <a href="results.php#/bowler/{{bowler.Id}}">{{bowler.Name || 'Vacancy'}}</a>
                  <span data-ng-show="bowler.IsRookie">
                    <sup>*</sup>
                  </span>
                  <span data-ng-show="bowler.IsDelegate">
                    <sup>D</sup>
                  </span>
                  <span data-ng-show="bowler.IsManager">
                    <sup>M</sup>
                  </span>
                </li>
                <li>
                  <span data-ng-if="team.RequiresAverage" style="float:right; font-weight: bold;">{{totalAverage(team)}}</span>
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

    <script src="/ClientApp/app.js"></script>
    <script src="/ClientApp/Controllers/controller.contingent.js"></script>
    <script src="/ClientApp/Services/service.data.js"></script>

    <script>
      // Init responsive-nav.js
      var nav = responsiveNav("#nav");
    </script>

</body>
</html>