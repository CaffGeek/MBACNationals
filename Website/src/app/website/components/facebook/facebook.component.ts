import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-facebook',
  templateUrl: './facebook.component.html',
  styleUrls: ['./facebook.component.scss']
})
export class FacebookComponent implements OnInit {
  applicationId: string;

  constructor() { }

  ngOnInit(): void {
    const d = document;
    const s = 'script';
    const id = 'facebook-jssdk';

    const fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {
      return;
    }

    const js = d.createElement(s) as any;
    js.id = id;
    js.src = '//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.5&appId=1336679153024562';
    fjs.parentNode.insertBefore(js, fjs);
  }
}
