import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebsiteRoutingModule } from './website-routing.modules';

import { HomeComponent } from './home/home.component';
import { NewsComponent } from './news/news.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { BannerComponent } from './banner/banner.component';
import { FollowUsComponent } from './follow-us/follow-us.component';
import { FacebookComponent } from './facebook/facebook.component';
import { TwitterComponent } from './twitter/twitter.component';
import { HighscoresComponent } from './highscores/highscores.component';

@NgModule({
  declarations: [
    HomeComponent,
    NewsComponent,
    ScheduleComponent,
    BannerComponent,
    FollowUsComponent,
    FacebookComponent,
    TwitterComponent,
    HighscoresComponent
  ],
  imports: [
    CommonModule,
    WebsiteRoutingModule,
  ]
})
export class WebsiteModule { }
