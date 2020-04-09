import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResultsService } from 'src/app/services/results.service';
import { MatchResult } from 'src/app/services/models/match';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {
  matchId: string;
  matchResult: MatchResult;

  constructor(
    private route: ActivatedRoute,
    private resultsService: ResultsService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({ id }) => {
      this.matchId = id;

      this.resultsService.getMatch(this.matchId)
        .subscribe(matchResult => this.matchResult = matchResult);
    });
  }

}
