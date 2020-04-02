
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from 'microsoft-adal-angular6';

import { environment } from 'src/environments/environment';

import { WebsiteComponent } from './website.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ResultsPageComponent } from './results-page/results-page.component';
import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { NewsPageComponent } from './news-page/news-page.component';
import { SchedulePageComponent } from './schedule-page/schedule-page.component';
import { LaneDrawPageComponent } from './lane-draw-page/lane-draw-page.component';
import { ContingentsPageComponent } from './contingents-page/contingents-page.component';
import { SouvenirsPageComponent } from './souvenirs-page/souvenirs-page.component';
import { SponsorsPageComponent } from './sponsors-page/sponsors-page.component';
import { CentresPageComponent } from './centres-page/centres-page.component';
import { LocationPageComponent } from './location-page/location-page.component';
import { HistoryPageComponent } from './history-page/history-page.component';

const routes: Routes = [
    { path: '', redirectTo: `/${environment.defaultYear}`, pathMatch: 'full'},
    { path: ':year', component: WebsiteComponent, children: [
        { path: '', component: HomePageComponent },
        { path: 'results', component: ResultsPageComponent },
        { path: 'statistics', component: StatisticsPageComponent },
        { path: 'news', component: NewsPageComponent },
        { path: 'schedule', component: SchedulePageComponent, pathMatch: 'full' },
        { path: 'schedule/:displayDate', component: SchedulePageComponent },
        { path: 'lanedraw', component: LaneDrawPageComponent },
        { path: 'contingents', component: ContingentsPageComponent },
        { path: 'souvenirs', component: SouvenirsPageComponent },
        { path: 'sponsors', component: SponsorsPageComponent },
        { path: 'centres', component: CentresPageComponent },
        { path: 'location', component: LocationPageComponent },
        { path: 'history', component: HistoryPageComponent },
    ]},
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class WebsiteRoutingModule { }
