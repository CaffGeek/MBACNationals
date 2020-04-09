import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResultsService } from 'src/app/services/results.service';
import { BowlerResult } from 'src/app/services/models/match';

@Component({
  selector: 'app-bowler-results',
  templateUrl: './bowler-results.component.html',
  styleUrls: ['./bowler-results.component.scss']
})
export class BowlerResultsComponent implements OnInit {
  year: number;
  bowlerId: string;
  bowlerResult: BowlerResult;
  bowlerHighScratch: number;
  bowlerHighPOA: number;

  constructor(
    private route: ActivatedRoute,
    private resultsService: ResultsService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({ year, id }) => {
      this.year = year;
      this.bowlerId = id;

      this.resultsService.getBowlerScores(this.bowlerId)
        .subscribe(bowlerResult => {
          this.bowlerResult = {
            ...bowlerResult,
            Scores: bowlerResult.Scores.sort((a, b) => a.Number - b.Number),
          };

          this.bowlerHighScratch = bowlerResult.Scores
            .reduce((max, p) => p.Scratch > max ? p.Scratch : max, bowlerResult.Scores?.[0]?.Scratch || 0);

          this.bowlerHighPOA = bowlerResult.Scores
            .reduce((max, p) => p.POA > max ? p.POA : max, bowlerResult.Scores?.[0]?.POA || 0);
        });
    });
  }

}
