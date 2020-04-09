import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TeamResults } from './models/teamresults';
import { MatchResult, TeamResult, BowlerResult } from './models/match';
import { StepladderMatch } from './models/stepladder';
import { map } from 'rxjs/operators';

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

  getTeamScores(id: string): Observable<TeamResult> {
    const params = new HttpParams()
      .set('teamId', id);

    return this.http
      .get<TeamResult>(`${environment.apiEndPoint}/Scores/Team`, { params });
  }

  getBowlerScores(id: string): Observable<BowlerResult> {
    const params = new HttpParams()
      .set('participantId', id);

    return this.http
      .get<BowlerResult>(`${environment.apiEndPoint}/Scores/Participant`, { params });
  }

  getStepladder(year: number): Observable<StepladderMatch[]> {
    const params = new HttpParams()
      .set('year', year.toString());

    return this.http
      .get<any[]>(`${environment.apiEndPoint}/Scores/StepladderMatches`, { params })
      .pipe<StepladderMatch[]>(
        map(matches => {
          return matches.map(x => ({
            ...x,
            Created: new Date(x.Created.match(/\d+/)[0] * 1),
            Updated: new Date(x.Updated.match(/\d+/)[0] * 1),
          }));
        })
      );
  }
}
