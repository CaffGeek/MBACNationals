import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Tournament } from './models/tournament';

@Injectable({
  providedIn: 'root'
})
export class TournamentsService {
  private staticTournamentInfo = {
    2020: {
      Theme: 'theme-2020',
      Welcome: '',
    },
    2019: {
      Theme: 'theme-2019',
      Welcome: 'Welcome to the online home of the 2019 Master Bowlers Association of Canada Nationals, ' +
               'taking place June 28 - July 4, 2019 in Gatineau, Quebec.',
    },
    2018: {
      Theme: 'theme-2018',
      Welcome: 'Welcome to the online home of the 2018 Master Bowlers Association of Canada Nationals, ' +
               'taking place June 30 - July 4, 2018 in Thunder Bay, ON.' +
               '<a href="https://www.pscp.tv/MBACNationals2018" target="_blank">Click here for Live Streaming!!!</a>',
    },
    2017: {
      Theme: 'theme-2017',
      Welcome: '',
    },
    2016: {
      Theme: 'theme-2016',
      Welcome: '',
    },
    2015: {
      Theme: 'theme-2015',
      Welcome: '',
    },
    2014: {
      Theme: 'theme-2014',
      Welcome: '',
    },
  };

  constructor(private http: HttpClient) { }

  getTournaments(): Observable<Tournament[]> {
    return this.http
      .get<Tournament[]>(`${environment.apiEndPoint}/Tournament/All`);
  }

  getTournament(year): Observable<Tournament> {
    return this.http
      .get<Tournament[]>(`${environment.apiEndPoint}/Tournament/All`)
      .pipe(
        map(tournaments => tournaments.find(x => x.Year === year)),
        map(tournament => ({ ...this.staticTournamentInfo[year], ...tournament }))
      );
  }
}
