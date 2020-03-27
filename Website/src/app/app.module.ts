import { environment } from '../environments/environment';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthenticationGuard, MsAdalAngular6Module } from 'microsoft-adal-angular6';

import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { WebsiteModule } from './website/website.module';
import { ForbiddenComponent } from './forbidden/forbidden.component';

const appRoutes: Routes = [
  { path: 'forbidden', component: ForbiddenComponent, pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AppComponent,
    ForbiddenComponent
  ],
  imports: [
    MsAdalAngular6Module.forRoot({
      ...environment.adSettings,
      redirectUri: window.location.href,
      navigateToLoginRequestUrl: true,
      cacheLocation: 'localStorage',
    }),
    BrowserModule,
    RouterModule.forRoot(appRoutes, {
      useHash: true,
      // enableTracing: true,
    }),
    AdminModule,
    WebsiteModule,
  ],
  providers: [
    AuthenticationGuard,
  ],
  bootstrap: [
    AppComponent,
  ],
})
export class AppModule { }
