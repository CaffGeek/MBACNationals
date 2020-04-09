import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResultsService } from 'src/app/services/results.service';
import { TeamResult } from 'src/app/services/models/match';

@Component({
  selector: 'app-team-results',
  templateUrl: './team-results.component.html',
  styleUrls: ['./team-results.component.scss']
})
export class TeamResultsComponent implements OnInit {
  year: number;
  teamId: string;
  teamResult: TeamResult;

  constructor(
    private route: ActivatedRoute,
    private resultsService: ResultsService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({ year, id }) => {
      this.year = year;
      this.teamId = id;

      this.resultsService.getTeamScores(this.teamId)
        .subscribe(teamResult => this.teamResult = {
          ...teamResult,
          Scores: teamResult.Scores.sort((a, b) => a.Number - b.Number),
        });
    });
  }

}
