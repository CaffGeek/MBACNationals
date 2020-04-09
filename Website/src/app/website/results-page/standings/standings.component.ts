import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResultsService } from 'src/app/services/results.service';
import { TeamResults } from 'src/app/services/models/teamresults';
import { Match } from 'src/app/services/models/match';

@Component({
  selector: 'app-standings',
  templateUrl: './standings.component.html',
  styleUrls: ['./standings.component.scss']
})
export class StandingsComponent implements OnInit {
  year: number;
  division: string;

  teamResults: TeamResults[];
  start = 1;
  limit = 7;

  constructor(
    private route: ActivatedRoute,
    private resultsService: ResultsService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({ year, division }) => {
      this.year = year;
      this.division = division;

      this.resultsService.getStandings(this.year, this.division)
        .subscribe(teamResults => this.teamResults = teamResults.sort((a, b) => b.RunningPoints - a.RunningPoints));
    });
  }

  findMatch(matches: Match[], game: number): Match {
    return matches.find(x => x.Number === game);
  }

}
