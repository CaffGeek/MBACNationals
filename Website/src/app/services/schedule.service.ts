import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ScheduleDay, GoogleItemDto } from './models/schedule';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  constructor(private http: HttpClient) { }

  getSchedule(scheduleUrl: string): Observable<ScheduleDay[]> {
    return this.http
      .get<any>(scheduleUrl)
      .pipe(
        map(data => ([ ...data.items ])),
        map((items: GoogleItemDto[]) => {
          return items.reduce((result, item, idx) => {
            const d = moment(item.start.date || item.start.dateTime);
            const key = d.format('YYYYMMDD');
            const group = result.find(x => x.key === key) || { key, date: d, items: [] };

            return [
              ...result.filter(x => x.key !== key),
              {
                ...group,
                items: [ ...group.items, item ],
              }
            ];
          }, [])
          .sort((a, b) => a.date.diff(b.date));
        })
      );
  }
}
