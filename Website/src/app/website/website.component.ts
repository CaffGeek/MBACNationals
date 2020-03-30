import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TournamentsService } from 'src/app/services/tournaments.service';

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {
  currentTheme = 'default'; // See: https://medium.com/@tomastrajan/the-complete-guide-to-angular-material-themes-4d165a9d24d1
  year: number;

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService,
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.year = year;

      this.tournamentsService.getTournament(year)
        .subscribe(tournament => this.currentTheme = tournament?.Theme || 'default');
    });
  }

}
