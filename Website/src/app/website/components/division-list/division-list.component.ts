import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-division-list',
  templateUrl: './division-list.component.html',
  styleUrls: ['./division-list.component.scss']
})
export class DivisionListComponent implements OnInit {
  @Input() year: string;

  divisions = [
    { Name: 'Tournament Men Single' },
    { Name: 'Tournament Women Single' },
    { Name: 'Tournament Men' },
    { Name: 'Tournament Women' },
    { Name: 'Teaching Men' },
    { Name: 'Teaching Women' },
    { Name: 'Seniors' },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
