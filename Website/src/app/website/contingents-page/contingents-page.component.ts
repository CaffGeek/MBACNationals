import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TournamentsService } from 'src/app/services/tournaments.service';
import { Tournament } from 'src/app/services/models/tournament';
import { ContingentsService } from 'src/app/services/contingents.service';
import { Contingent, Team } from 'src/app/services/models/contingent';

@Component({
  selector: 'app-contingents-page',
  templateUrl: './contingents-page.component.html',
  styleUrls: ['./contingents-page.component.scss']
})
export class ContingentsPageComponent implements OnInit {
  tournament: Tournament;
  contingent: Contingent;

  getManager = Contingent.getManager;
  getDelegates = Contingent.getDelegates;
  getTeamAverage = Team.getAverage;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private tournamentsService: TournamentsService,
    private contingentsService: ContingentsService
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({ year, provinceCode }) => {
      this.tournamentsService.getTournament(year)
        .subscribe(tournament => this.tournament = tournament);

      this.contingentsService.getContingent(year, provinceCode)
        .subscribe(contingent => this.contingent = contingent);
    });
  }

  sortTeams(teams: Team[]): Team[] {
    return teams.sort((a, b) => b.Name.length - a.Name.length);
  }
}
