import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';

import { TournamentsService } from 'src/app/services/tournaments.service';
import { Tournament } from 'src/app/services/models/tournament';

@Component({
  selector: 'app-schedule-page',
  templateUrl: './schedule-page.component.html',
  styleUrls: ['./schedule-page.component.scss']
})
export class SchedulePageComponent implements OnInit {
  year: number;
  tournament: Tournament;
  displayDate: string;
  dateKeys: string[];

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService
  ) {
    console.log('constructor');
  }

  ngOnInit(): void {
    this.route.params.subscribe(({ year, displayDate }) => {
      console.log('params', {year, displayDate});

      this.year = year;
      this.displayDate = displayDate;

      this.tournamentsService.getTournament(this.year)
        .subscribe(tournament => this.tournament = tournament);
    });
  }

  loadDateKeys(data) {
    this.dateKeys = data.map(x => ({
      key: x,
      date: moment(x, 'YYYYMMDD'),
    }));
  }
}
