import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TournamentsService } from 'src/app/services/tournaments.service';
import { Tournament } from 'src/app/services/models/tournament';
import { ContingentsService } from 'src/app/services/contingents.service';
import { Contingent } from 'src/app/services/models/contingent';

@Component({
  selector: 'app-contingents-page',
  templateUrl: './contingents-page.component.html',
  styleUrls: ['./contingents-page.component.scss']
})
export class ContingentsPageComponent implements OnInit {
  tournament: Tournament;
  contingent: Contingent;

  constructor(
    private route: ActivatedRoute,
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

}
