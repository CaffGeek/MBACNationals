import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-sidemenu[year]',
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.scss']
})
export class SidemenuComponent implements OnInit {
  @Input() year: number;

  constructor() { }

  ngOnInit(): void {
  }

}
