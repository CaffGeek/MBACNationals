import { Component, OnInit, Input } from '@angular/core';
import { Match } from 'src/app/services/models/match';

@Component({
  selector: 'app-match-box[year][match]',
  templateUrl: './match-box.component.html',
  styleUrls: ['./match-box.component.scss']
})
export class MatchBoxComponent implements OnInit {
  @Input() year: number;
  @Input() match: Match;

  constructor() { }

  ngOnInit(): void {
  }

}
