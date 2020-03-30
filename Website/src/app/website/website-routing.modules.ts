
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from 'microsoft-adal-angular6';

import { WebsiteComponent } from './website.component';
import { HomeComponent } from './home/home.component';
import { environment } from 'src/environments/environment';
import { ResultsComponent } from './results/results.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { NewsComponent } from './news/news.component';

const routes: Routes = [
    { path: '', redirectTo: `/${environment.defaultYear}`, pathMatch: 'full'},
    { path: ':year', component: WebsiteComponent, children: [
        { path: '', component: HomeComponent },
        { path: 'results', component: ResultsComponent },
        { path: 'statistics', component: StatisticsComponent },
        { path: 'news', component: NewsComponent },
    ]},
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class WebsiteRoutingModule { }
