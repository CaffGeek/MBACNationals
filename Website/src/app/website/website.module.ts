import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { WebsiteRoutingModule } from './website-routing.modules';
import { WebsiteComponent } from './website.component';

// Components
import { TopmenuComponent } from './navigation/topmenu/topmenu.component';
import { SidemenuComponent } from './navigation/sidemenu/sidemenu.component';
import { NewsComponent } from './components/news/news.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { BannerComponent } from './components/banner/banner.component';
import { FollowUsComponent } from './components/follow-us/follow-us.component';
import { FacebookComponent } from './components/facebook/facebook.component';
import { HighscoresComponent } from './components/highscores/highscores.component';
import { SponsorsComponent } from './components/sponsors/sponsors.component';

// Pages
import { HomePageComponent } from './home-page/home-page.component';
import { ResultsPageComponent } from './results-page/results-page.component';
import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { NewsPageComponent } from './news-page/news-page.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { SchedulePageComponent } from './schedule-page/schedule-page.component';
import { LaneDrawPageComponent } from './lane-draw-page/lane-draw-page.component';
import { ContingentsPageComponent } from './contingents-page/contingents-page.component';
import { SouvenirsPageComponent } from './souvenirs-page/souvenirs-page.component';
import { SponsorsPageComponent } from './sponsors-page/sponsors-page.component';
import { CentresPageComponent } from './centres-page/centres-page.component';
import { LocationPageComponent } from './location-page/location-page.component';
import { HistoryPageComponent } from './history-page/history-page.component';
import { LocationComponent } from './components/location/location.component';
import { CentreComponent } from './components/centre/centre.component';
import { ProvinceListComponent } from './components/province-list/province-list.component';
import { ParticipantNameComponent } from './components/participant-name/participant-name.component';
import { DivisionListComponent } from './components/division-list/division-list.component';
import { HighByGameComponent } from './statistics-page/high-by-game/high-by-game.component';
import { HighAverageComponent } from './statistics-page/high-average/high-average.component';
import { MostWinsComponent } from './statistics-page/most-wins/most-wins.component';
import { HighPoaByGameComponent } from './statistics-page/high-poa-by-game/high-poa-by-game.component';
import { MatchBoxComponent } from './results-page/match-box/match-box.component';
import { MatchComponent } from './results-page/match/match.component';
import { StandingsComponent } from './results-page/standings/standings.component';

@NgModule({
  declarations: [
    WebsiteComponent,

    HomePageComponent,
    ResultsPageComponent,
    StatisticsPageComponent,
    NewsPageComponent,
    SchedulePageComponent,
    LaneDrawPageComponent,
    ContingentsPageComponent,
    SouvenirsPageComponent,
    SponsorsPageComponent,
    CentresPageComponent,
    LocationPageComponent,
    HistoryPageComponent,

    TopmenuComponent,
    SidemenuComponent,
    BannerComponent,
    WelcomeComponent,
    NewsComponent,
    ScheduleComponent,
    FollowUsComponent,
    FacebookComponent,
    HighscoresComponent,
    SponsorsComponent,
    LocationComponent,
    CentreComponent,
    ProvinceListComponent,
    ParticipantNameComponent,
    DivisionListComponent,
    HighByGameComponent,
    HighAverageComponent,
    MostWinsComponent,
    HighPoaByGameComponent,
    MatchBoxComponent,
    MatchComponent,
    StandingsComponent,
  ],
  imports: [
    CommonModule,
    WebsiteRoutingModule,
    AngularMaterialModule,
    FlexLayoutModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class WebsiteModule { }
