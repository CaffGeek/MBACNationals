import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  year: string;

  constructor(
    private route: ActivatedRoute,
  ) {
    this.route.params.subscribe(({year}) => this.year = year);
  }

  ngOnInit(): void {
  }

}
