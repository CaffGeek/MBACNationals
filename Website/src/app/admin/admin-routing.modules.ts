
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from 'microsoft-adal-angular6';

import { AdminComponent } from './admin/admin.component';
import { environment } from 'src/environments/environment';
import { AuthGuard } from '../services/auth-guard.service';


const routes: Routes = [
    { path: 'admin', canActivate: [AuthenticationGuard, AuthGuard],
        data: {
            roles: [
                environment.roles.admin,
                environment.roles.hostCommittee,
            ],
        },
        component: AdminComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class AdminRoutingModule { }
