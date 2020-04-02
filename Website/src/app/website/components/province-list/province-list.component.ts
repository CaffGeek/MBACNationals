import { Component, OnInit, Input } from '@angular/core';
import { Tournament } from 'src/app/services/models/tournament';

@Component({
  selector: 'app-province-list',
  templateUrl: './province-list.component.html',
  styleUrls: ['./province-list.component.scss']
})
export class ProvinceListComponent implements OnInit {
  @Input() tournament: Tournament;

  constructor() { }

  ngOnInit(): void {
  }

}
