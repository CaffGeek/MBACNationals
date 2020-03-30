import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-banner[year]',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {
  @Input() year: number;

  constructor() { }

  ngOnInit(): void {
  }

}
