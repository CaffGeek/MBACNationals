import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-high-by-game',
  templateUrl: './high-by-game.component.html',
  styleUrls: ['./high-by-game.component.scss']
})
export class HighByGameComponent implements OnInit {
  year: number;
  game: number;

  constructor(
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.year = year;
    });
  }

}
