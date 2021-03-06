import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.modules';
import { AuthGuard } from '../services/auth-guard.service';

import { AdminComponent } from './admin/admin.component';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [AdminComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
  ],
  providers: [
    AuthGuard,
  ],
})
export class AdminModule { }
