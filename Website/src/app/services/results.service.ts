import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TeamResults } from './models/teamresults';
import { MatchResult } from './models/match';

@Injectable({
  providedIn: 'root'
})
export class ResultsService {
  constructor(private http: HttpClient) { }

  getStandings(year, division): Observable<TeamResults[]> {
    const params = new HttpParams()
      .set('year', year.toString())
      .set('division', division.replace(/Women/i, 'Ladies'));

    return this.http
      .get<TeamResults[]>(`${environment.apiEndPoint}/Scores/Standings`, { params });
  }

  getMatch(id: string): Observable<MatchResult> {
    const params = new HttpParams()
    .set('matchId', id);

    return this.http
      .get<MatchResult>(`${environment.apiEndPoint}/Scores/Match`, { params });
  }
}
