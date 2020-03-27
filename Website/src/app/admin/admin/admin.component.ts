import { Component, OnInit } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.less']
})
export class AdminComponent implements OnInit {
  user = '';
  groups = [];

  constructor(private adalSvc: MsAdalAngular6Service) {
    // console.log(this.adalSvc.userInfo);
    // this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {
    //   console.log(token);
    // });
  }

  ngOnInit(): void {
  }

  logout() {
    this.adalSvc.logout();
  }
}
