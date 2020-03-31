import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AuthenticationGuard, MsAdalAngular6Module } from 'microsoft-adal-angular6';

import { environment } from 'src/environments/environment';
import { AppComponent } from './app.component';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { AdminModule } from './admin/admin.module';
import { WebsiteModule } from './website/website.module';
import { ForbiddenComponent } from './forbidden/forbidden.component';

const appRoutes: Routes = [
  { path: 'forbidden', component: ForbiddenComponent, pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AppComponent,
    ForbiddenComponent,
  ],
  imports: [
    MsAdalAngular6Module.forRoot({
      ...environment.adSettings,
      redirectUri: window.location.href,
      navigateToLoginRequestUrl: true,
      cacheLocation: 'localStorage',
    }),
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AngularMaterialModule,
    RouterModule.forRoot(appRoutes, {
      useHash: true,
      paramsInheritanceStrategy: 'always',
      // enableTracing: true,
    }),
    AdminModule,
    WebsiteModule,
  ],
  exports: [
  ],
  providers: [
    AuthenticationGuard,
  ],
  bootstrap: [
    AppComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
