<?php 
include "header.php"; 
?>

        <div class="section group">
          <div class="col span_3_of_3" style="margin:0px;">
				<div id="headerImage" style="background-image:url('images/header_image_4.jpg'); "></div>
				<div id="photoCredit"><strong>James N. Allan Skyway Bridge</strong> &bull; Credit: WikiCommons</div>
         </div>
        </div>

        <div class="section group content" data-ng-app="app">
          <div data-ng-controller="ScheduleController">
            <div class="col span_1_of_3">
              <div id="centres_links">
                <a href="" data-ng-click="loadLaneDraw('Tournament Men Single')">TOURNAMENT MEN SINGLE</a><br />
                <a href="" data-ng-click="loadLaneDraw('Tournament Ladies Single')">TOURNAMENT LADIES SINGLE</a><br />
                <a href="" data-ng-click="loadLaneDraw('Tournament Men')">TOURNAMENT MEN</a><br />
                <a href="" data-ng-click="loadLaneDraw('Tournament Ladies')">TOURNAMENT LADIES</a><br />
                <a href="" data-ng-click="loadLaneDraw('Teaching Men')">TEACHING MEN</a><br />
                <a href="" data-ng-click="loadLaneDraw('Teaching Ladies')">TEACHING LADIES</a><br />
                <a href="" data-ng-click="loadLaneDraw('Seniors')">SENIORS</a><br />
                <hr />
                <a href="images/forms/2015NationalsLaneDraw.pdf" target="_blank"> DOWNLOAD PDF</a><br />
              </div>
            </div>
            <div class="col span_2_of_3" id="contentArea">

              <h2>LANE DRAW</h2>
              <h4>{{model.Division}}</h4>
              <table style="width: 100%;" class="lanedraw">
                <tbody data-ng-repeat="game in model.Games | unique:'Number'">
                  <tr data-ng-show="game.ShowLocation">
                    <td colspan="3">
                      <h4>{{game.CentreName}}</h4>
                    </td>
                  </tr>
                  <tr data-ng-show="game.ShowLocation">
                    <th>#</th>
                    <th class="alt" data-ng-if="HasGames('BC')">BC</th>
                    <th class=""    data-ng-if="HasGames('AB')">AB</th>
                    <th class="alt" data-ng-if="HasGames('SK')">SK</th>
                    <th class=""    data-ng-if="HasGames('MB')">MB</th>
                    <th class="alt" data-ng-if="HasGames('NO')">NO</th>
                    <th class=""    data-ng-if="HasGames('SO')">SO</th>
                    <th class="alt" data-ng-if="HasGames('QC')">QC</th>
                    <th class=""    data-ng-if="HasGames('NL')">NL</th>
                  </tr>
                  <tr>
                    <td>{{game.Number}}</td>
                    <td data-ng-if="HasGames('BC')" class="alt">
                      <div data-ng-if="Opponent('BC', game.Number)+'' != ''">
                        {{IsHomeTeam('BC', game.Number) ? 'vs' : '@'}} {{Opponent('BC', game.Number)}}<br /> on {{Lane('BC', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('AB')">
                      <div data-ng-if="Opponent('AB', game.Number)+'' != ''">
                        {{IsHomeTeam('AB', game.Number) ? 'vs' : '@'}} {{Opponent('AB', game.Number)}}<br /> on {{Lane('AB', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('SK')" class="alt">
                      <div data-ng-if="Opponent('SK', game.Number)+'' != ''">
                        {{IsHomeTeam('SK', game.Number) ? 'vs' : '@'}} {{Opponent('SK', game.Number)}}<br /> on {{Lane('SK', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('MB')">
                      <div data-ng-if="Opponent('MB', game.Number)+'' != ''">
                        {{IsHomeTeam('MB', game.Number) ? 'vs' : '@'}} {{Opponent('MB', game.Number)}}<br /> on {{Lane('MB', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('NO')" class="alt">
                      <div data-ng-if="Opponent('NO', game.Number)+'' != ''">
                        {{IsHomeTeam('NO', game.Number) ? 'vs' : '@'}} {{Opponent('NO', game.Number)}}<br /> on {{Lane('NO', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('SO')">
                      <div data-ng-if="Opponent('SO', game.Number)+'' != ''">
                        {{IsHomeTeam('SO', game.Number) ? 'vs' : '@'}} {{Opponent('SO', game.Number)}}<br /> on {{Lane('SO', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('QC')" class="alt">
                      <div data-ng-if="Opponent('QC', game.Number)+'' != ''">
                        {{IsHomeTeam('QC', game.Number) ? 'vs' : '@'}} {{Opponent('QC', game.Number)}}<br /> on {{Lane('QC', game.Number)}}
                      </div>
                    </td>
                    <td data-ng-if="HasGames('NL')">
                      <div data-ng-if="Opponent('NL', game.Number)+'' != ''">
                        {{IsHomeTeam('NL', game.Number) ? 'vs' : '@'}} {{Opponent('NL', game.Number)}}<br /> on {{Lane('NL', game.Number)}}
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table>
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