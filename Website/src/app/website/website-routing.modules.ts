
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from 'microsoft-adal-angular6';

import { environment } from 'src/environments/environment';

import { WebsiteComponent } from './website.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ResultsPageComponent } from './results-page/results-page.component';
import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { NewsPageComponent } from './news-page/news-page.component';

const routes: Routes = [
    { path: '', redirectTo: `/${environment.defaultYear}`, pathMatch: 'full'},
    { path: ':year', component: WebsiteComponent, children: [
        { path: '', component: HomePageComponent },
        { path: 'results', component: ResultsPageComponent },
        { path: 'statistics', component: StatisticsPageComponent },
        { path: 'news', component: NewsPageComponent },
    ]},
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class WebsiteRoutingModule { }
