import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-schedule[year]',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {
  @Input() year: number;

  constructor() { }

  ngOnInit(): void {
  }

}
