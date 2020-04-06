import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-high-poa-by-game',
  templateUrl: './high-poa-by-game.component.html',
  styleUrls: ['./high-poa-by-game.component.scss']
})
export class HighPoaByGameComponent implements OnInit {
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
