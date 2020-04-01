import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';

import { TournamentsService } from 'src/app/services/tournaments.service';
import { Tournament } from 'src/app/services/models/tournament';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  now = moment(); // expose the current date
  year: number;
  tournament: Tournament;

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.year = year;

      this.tournamentsService.getTournament(year)
        .subscribe(tournament => this.tournament = tournament);
    });
  }
}
