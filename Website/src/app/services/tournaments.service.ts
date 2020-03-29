import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TournamentsService {
  private staticTournamentInfo = {
    2019: {
      welcome: 'Welcome to the online home of the 2019 Master Bowlers Association of Canada Nationals, ' +
               'taking place June 28 - July 4, 2019 in Gatineau, Quebec.',
    }
  };

  constructor(private http: HttpClient) { }

  getTournaments(): Observable<any[]> {
    return this.http
      .get<any[]>(`${environment.apiEndPoint}/Tournament/All`);
  }

  getTournament(year): Observable<any[]> {
    return this.http
      .get<any[]>(`${environment.apiEndPoint}/Tournament/All`)
      .pipe(
        map(tournaments => tournaments.find(x => x.Year === year)),
        map(tournament => ({ ...this.staticTournamentInfo[year], ...tournament }))
      );
  }
}
