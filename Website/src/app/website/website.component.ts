import { Tournament } from 'src/app/services/models/tournament';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TournamentsService } from 'src/app/services/tournaments.service';

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {
  tournament: Tournament;
  page: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private tournamentsService: TournamentsService,
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.tournamentsService.getTournament(year)
        .subscribe(tournament => this.tournament = tournament);
    });
  }

}
