import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-most-wins',
  templateUrl: './most-wins.component.html',
  styleUrls: ['./most-wins.component.scss']
})
export class MostWinsComponent implements OnInit {
  year: number;

  constructor(
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.year = year;
    });
  }

}
