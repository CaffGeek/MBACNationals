import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-high-average',
  templateUrl: './high-average.component.html',
  styleUrls: ['./high-average.component.scss']
})
export class HighAverageComponent implements OnInit {
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
