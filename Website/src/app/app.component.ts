import { Component, OnInit } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private adalSvc: MsAdalAngular6Service
  ) {
    this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {
      // TODO: CHAD: Do something with this token...
      // see: https://www.codeproject.com/Articles/2259378/Using-Azure-AD-for-login-to-an-Angular-application
    });
  }

  ngOnInit(): void {
  }
}
