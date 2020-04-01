import { Component, OnChanges, Input } from '@angular/core';
import * as moment from 'moment';

import { TournamentsService } from 'src/app/services/tournaments.service';
import { Tournament } from 'src/app/services/models/tournament';

@Component({
  selector: 'app-schedule[tournament][displayDate]',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnChanges {
  @Input() tournament: Tournament;
  @Input() displayDate: string;
  dateKeys: string[];
  scheduleItems: any[];

  constructor(
    private tournamentService: TournamentsService
  ) { }

  ngOnChanges(changes): void {
    if (changes?.tournament && this.tournament) {
      this.tournamentService.getSchedule(this.tournament)
        .subscribe(scheduleItems => {
          const grouped = scheduleItems.reduce((result, item) => {
            const key = moment(item.start.date || item.start.dateTime).format('YYYYMMDD');
            return {
              ...result,
              [key]: [
                ...(result[key] || []),
                item,
              ],
            };
          }, {});

          const todayKey = moment().format('YYYYMMDD');
          const allKeys = Object.keys(grouped);
          const futureKeys = allKeys.filter(x => x >= todayKey);
          const showKey = futureKeys.length ? futureKeys[0] : allKeys[allKeys.length - 1];

          this.dateKeys = allKeys;
          this.scheduleItems = grouped[showKey];
        });
    }
  }

}
