import { MatchComponent } from './results-page/match/match.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

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
import { HighByGameComponent } from './statistics-page/high-by-game/high-by-game.component';
import { HighAverageComponent } from './statistics-page/high-average/high-average.component';
import { MostWinsComponent } from './statistics-page/most-wins/most-wins.component';
import { HighPoaByGameComponent } from './statistics-page/high-poa-by-game/high-poa-by-game.component';
import { StandingsComponent } from './results-page/standings/standings.component';
import { TeamResultsComponent } from './results-page/team-results/team-results.component';
import { BowlerResultsComponent } from './results-page/bowler-results/bowler-results.component';
import { StepladderComponent } from './results-page/stepladder/stepladder.component';

const routes: Routes = [
    { path: '', redirectTo: `/${environment.defaultYear}`, pathMatch: 'full'},
    { path: ':year', component: WebsiteComponent, children: [
        { path: '', component: HomePageComponent },
        { path: 'results', component: ResultsPageComponent, children: [
            { path: '', redirectTo: 'standings/Tournament Men Single', pathMatch: 'full' },
            { path: 'standings/stepladder', redirectTo: 'stepladder' }, // One off route fix
            { path: 'standings/:division', component: StandingsComponent },
            { path: 'match/:id', component: MatchComponent },
            { path: 'team/:id', component: TeamResultsComponent },
            { path: 'bowler/:id', component: BowlerResultsComponent },
            { path: 'stepladder', component: StepladderComponent },
        ]},
        { path: 'statistics', component: StatisticsPageComponent, children: [
            { path: '', redirectTo: 'high-by-game', pathMatch: 'full' },
            { path: 'high-by-game', component: HighByGameComponent },
            { path: 'high-poa-by-game', component: HighPoaByGameComponent },
            { path: 'high-average', component: HighAverageComponent },
            { path: 'most-wins', component: MostWinsComponent },
        ]},
        { path: 'news', component: NewsPageComponent },
        { path: 'schedule', component: SchedulePageComponent, pathMatch: 'full' },
        { path: 'schedule/:displayDate', component: SchedulePageComponent },
        { path: 'lanedraw', redirectTo: 'lanedraw/Tournament Men Single', pathMatch: 'full' },
        { path: 'lanedraw/:division', component: LaneDrawPageComponent },
        { path: 'contingents', redirectTo: 'contingents/BC', pathMatch: 'full' },
        { path: 'contingents/:provinceCode', component: ContingentsPageComponent },
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
