import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { WebsiteRoutingModule } from './website-routing.modules';
import { WebsiteComponent } from './website.component';

import { TopmenuComponent } from './navigation/topmenu/topmenu.component';
import { SidemenuComponent } from './navigation/sidemenu/sidemenu.component';
import { HomeComponent } from './home/home.component';
import { NewsComponent } from './components/news/news.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { BannerComponent } from './components/banner/banner.component';
import { FollowUsComponent } from './components/follow-us/follow-us.component';
import { FacebookComponent } from './components/facebook/facebook.component';
import { TwitterComponent } from './components/twitter/twitter.component';
import { HighscoresComponent } from './components/highscores/highscores.component';
import { SponsorsComponent } from './components/sponsors/sponsors.component';
import { ResultsComponent } from './results/results.component';
import { StatisticsComponent } from './statistics/statistics.component';

@NgModule({
  declarations: [
    TopmenuComponent,
    SidemenuComponent,
    HomeComponent,
    NewsComponent,
    ScheduleComponent,
    BannerComponent,
    FollowUsComponent,
    FacebookComponent,
    TwitterComponent,
    HighscoresComponent,
    SponsorsComponent,
    ResultsComponent,
    StatisticsComponent,
    WebsiteComponent
  ],
  imports: [
    CommonModule,
    WebsiteRoutingModule,
    AngularMaterialModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class WebsiteModule { }
