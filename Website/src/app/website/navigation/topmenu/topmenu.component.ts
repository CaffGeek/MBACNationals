import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { TournamentsService } from 'src/app/services/tournaments.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-topmenu',
  templateUrl: './topmenu.component.html',
  styleUrls: ['./topmenu.component.less']
})
export class TopmenuComponent implements OnInit {
  @Input() year: number;
  tournaments = [];

  @Output() navToggle = new EventEmitter<boolean>();
  navOpen() {
    this.navToggle.emit(true);
  }

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService
  ) { }

  ngOnInit(): void {
    this.tournamentsService.getTournaments()
      .subscribe(tournaments => this.tournaments = tournaments);
  }

}
