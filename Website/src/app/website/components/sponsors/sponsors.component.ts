import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-sponsors[year]',
  templateUrl: './sponsors.component.html',
  styleUrls: ['./sponsors.component.scss']
})
export class SponsorsComponent implements OnInit {
  @Input() year: number;

  constructor() { }

  ngOnInit(): void {
  }

}
