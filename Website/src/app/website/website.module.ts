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
