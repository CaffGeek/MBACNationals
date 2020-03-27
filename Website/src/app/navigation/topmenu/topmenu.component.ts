import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { TournamentsService } from 'src/app/services/tournaments.service';

@Component({
  selector: 'app-topmenu',
  templateUrl: './topmenu.component.html',
  styleUrls: ['./topmenu.component.less']
})
export class TopmenuComponent implements OnInit {
  tournaments = [];

  @Output() navToggle = new EventEmitter<boolean>();
  navOpen() {
    this.navToggle.emit(true);
  }

  constructor(
    private tournamentsService: TournamentsService
  ) { }

  ngOnInit(): void {
    this.tournamentsService.getTournaments()
      .subscribe(tournaments => this.tournaments = tournaments);
  }

}
