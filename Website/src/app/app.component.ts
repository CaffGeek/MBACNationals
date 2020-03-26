import { Component } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  constructor(private adalSvc: MsAdalAngular6Service) {
    this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {
      // TODO: CHAD: Do something with this token...
      // see: https://www.codeproject.com/Articles/2259378/Using-Azure-AD-for-login-to-an-Angular-application
    });
  }
}
