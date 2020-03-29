import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TournamentsService } from 'src/app/services/tournaments.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  tournament: any = {};

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.tournamentsService.getTournament(year)
        .subscribe(tournament => this.tournament = tournament);
    });
  }
}
