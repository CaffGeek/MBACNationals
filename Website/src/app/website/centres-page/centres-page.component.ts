import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-centres-page',
  templateUrl: './centres-page.component.html',
  styleUrls: ['./centres-page.component.scss']
})
export class CentresPageComponent implements OnInit {
  year: number;

  constructor(
    private route: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({year}) => {
      this.year = year;
    });
  }

}
