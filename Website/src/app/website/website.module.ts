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
import { TwitterComponent } from './components/twitter/twitter.component';
import { HighscoresComponent } from './components/highscores/highscores.component';
import { SponsorsComponent } from './components/sponsors/sponsors.component';

// Pages
import { HomePageComponent } from './home-page/home-page.component';
import { ResultsPageComponent } from './results-page/results-page.component';
import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { NewsPageComponent } from './news-page/news-page.component';

@NgModule({
  declarations: [
    WebsiteComponent,
    TopmenuComponent,
    SidemenuComponent,
    NewsComponent,
    ScheduleComponent,
    BannerComponent,
    FollowUsComponent,
    FacebookComponent,
    TwitterComponent,
    HighscoresComponent,
    SponsorsComponent,
    HomePageComponent,
    ResultsPageComponent,
    StatisticsPageComponent,
    NewsPageComponent,
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
