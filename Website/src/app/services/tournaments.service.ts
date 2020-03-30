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
    },
    2018: {
      welcome: 'Welcome to the online home of the 2018 Master Bowlers Association of Canada Nationals, ' +
               'taking place June 30 - July 4, 2018 in Thunder Bay, ON.' +
               '<a href="https://www.pscp.tv/MBACNationals2018" target="_blank">Click here for Live Streaming!!!</a>',
    },
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
