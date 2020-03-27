
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from 'microsoft-adal-angular6';

import { HomeComponent } from './home/home.component';
import { environment } from 'src/environments/environment';


const routes: Routes = [
    { path: '', redirectTo: '/2019', pathMatch: 'full'},
    { path: ':year', component: HomeComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class WebsiteRoutingModule { }
