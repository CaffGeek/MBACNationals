import { Component, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';

import { ScheduleService } from 'src/app/services/schedule.service';
import { Tournament } from 'src/app/services/models/tournament';
import { ScheduleDay } from 'src/app/services/models/schedule';

@Component({
  selector: 'app-schedule[tournament]',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnChanges {
  @Input() tournament: Tournament;
  @Input() displayDate: string;

  @Output() dateKeysLoaded = new EventEmitter();

  scheduleDays: any[];

  constructor(
    private scheduleService: ScheduleService
  ) { }

  ngOnChanges(changes): void {
    if (changes && this.tournament?.Schedule?.Url) {
      this.scheduleService.getSchedule(this.tournament.Schedule.Url)
        .subscribe((schedules: ScheduleDay[]) => {
          const allDateKeys = schedules.map(x => x.key);
          this.dateKeysLoaded.emit(allDateKeys);

          if (this.displayDate) {
            const requestedSchedule = schedules.filter(x => x.key >= this.displayDate).slice(0, 1);
            this.scheduleDays = requestedSchedule.length
              ? requestedSchedule
              : schedules.slice(-1);
          } else {
            this.scheduleDays = schedules;
          }
        });
    }
  }

}
