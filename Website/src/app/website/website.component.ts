import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TournamentsService } from 'src/app/services/tournaments.service';

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.less']
})
export class WebsiteComponent implements OnInit {
  year: number;

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService,
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.year = year;
    });
  }

}
