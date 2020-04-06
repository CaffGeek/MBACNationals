import { IndividualStats } from './models/individualstats';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HighScores } from './models/highscores';


@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  constructor(private http: HttpClient) { }

  getHighScores(year: number, division: string): Observable<HighScores> {
    const params = new HttpParams()
      .set('year', year.toString())
      .set('division', division);

    return this.http
      .get<HighScores>(`${environment.apiEndPoint}/Scores/HighScores/`, { params });
  }

  getIndividualStats(year: number, division: string): Observable<IndividualStats[]> {
    const params = new HttpParams()
      .set('year', year.toString())
      .set('division', division);

    return this.http
      .get<any>(`${environment.apiEndPoint}/Scores/Averages/`, { params })
      .pipe(
        map(byDivision => {
          return byDivision.Participants;
        })
      );
  }
}
