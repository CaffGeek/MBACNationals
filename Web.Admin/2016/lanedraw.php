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
    

    <div class="container" data-ng-app="app">
    	
      <!-- Example row of columns -->
      <div class="row" data-ng-controller="ScheduleController">
        <div class="col-md-2">
        <ul class="sidebarNav">
       			<li><a href="" data-ng-click="loadLaneDraw('Tournament Men Single')">TOURNAMENT MEN SINGLE</a></li>
                <li><a href="" data-ng-click="loadLaneDraw('Tournament Ladies Single')">TOURNAMENT LADIES SINGLE</a></li>
                <li><a href="" data-ng-click="loadLaneDraw('Tournament Men')">TOURNAMENT MEN</a></li>
                <li><a href="" data-ng-click="loadLaneDraw('Tournament Ladies')">TOURNAMENT LADIES</a></li>
                <li><a href="" data-ng-click="loadLaneDraw('Teaching Men')">TEACHING MEN</a></li>
                <li><a href="" data-ng-click="loadLaneDraw('Teaching Ladies')">TEACHING LADIES</a></li>
                <li><a href="" data-ng-click="loadLaneDraw('Seniors')">SENIORS</a></li>
                <hr />
                <li><a href="images/forms/2015NationalsLaneDraw.pdf" target="_blank"> DOWNLOAD PDF</a></li>
    	</ul>
          
        </div>
        <div class="col-md-10">
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

<?php

include 'footer.php';

?>