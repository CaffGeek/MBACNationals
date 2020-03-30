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
  // TODO: CHAD: move this stuff into the tournament response (and add the ability to edit in admin)
  private staticTournamentInfo = {
    2020: {
      Theme: 'theme-2020',
    },
    2019: {
      Theme: 'theme-2019',
      Logo: 'http://mbacnationals.com/2019/images/Logo.png',
      Header: 'http://mbacnationals.com/2019/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2019/images/header_image_default.jpg',
        results: 'http://mbacnationals.com/2019/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2019/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2019/images/header_image_news.jpg',
      },
      Welcome: `Welcome to the online home of the 2019 Master Bowlers Association of Canada Nationals,
        taking place June 28 - July 4, 2019 in Gatineau, Quebec.`,
    },
    2018: {
      Theme: 'theme-2018',
      Logo: 'http://mbacnationals.com/2018/images/Logo.png',
      Header: 'http://mbacnationals.com/2018/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2018/images/header_image_default.jpg',
        results: 'http://mbacnationals.com/2018/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2018/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2018/images/header_image_news.jpg',
      },
      Welcome: `Welcome to the online home of the 2018 Master Bowlers Association of Canada Nationals,
               taking place June 30 - July 4, 2018 in Thunder Bay, ON. <br />
               <a href="https://www.pscp.tv/MBACNationals2018" target="_blank">Click here for Live Streaming!!!</a>`,
    },
    2017: {
      Theme: 'theme-2017',
      Logo: 'http://mbacnationals.com/2017/images/Logo.png',
      Header: 'http://mbacnationals.com/2017/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2017/images/header_image_schedule.jpg',
        results: 'http://mbacnationals.com/2017/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2017/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2017/images/header_image_news.jpg',
      },
    },
    2016: {
      Theme: 'theme-2016',
      Logo: 'http://mbacnationals.com/2016/images/Logo.png',
      Header: 'http://mbacnationals.com/2016/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2016/images/header_image_1.jpg',
        results: 'http://mbacnationals.com/2016/images/header_image_7.jpg',
        statistics: 'http://mbacnationals.com/2016/images/header_image_7.jpg',
        news: 'http://mbacnationals.com/2016/images/header_image_6.jpg',
      },
    },
    2015: {
      Theme: 'theme-2015',
      Logo: 'http://mbacnationals.com/2015/images/Logo.png',
      Header: 'http://mbacnationals.com/2015/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2015/images/header_image_default.jpg',
        results: 'http://mbacnationals.com/2015/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2015/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2015/images/header_image_news.jpg',
      },
    },
    2014: {
      Theme: 'theme-2014',
      Logo: 'http://mbacnationals.com/2014/images/Logo.png',
      Header: 'http://mbacnationals.com/2014/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2014/images/header_image_default.jpg',
        results: 'http://mbacnationals.com/2014/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2014/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2014/images/header_image_news.jpg',
      },
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
