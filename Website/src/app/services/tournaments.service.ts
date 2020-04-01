import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
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
      Welcome: `Welcome to the online home of the 2019 Master Bowlers Association of Canada Nationals,
        taking place June 28 - July 4, 2019 in Gatineau, Quebec.`,
      // tslint:disable-next-line:max-line-length
      ScheduleUrl: 'https://www.googleapis.com/calendar/v3/calendars/p7vg564jka9r90qrb4n6unp928@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY',
      Logo: 'http://mbacnationals.com/2019/images/Logo.png',
      Header: 'http://mbacnationals.com/2019/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2019/images/header_image_default.jpg',
        results: 'http://mbacnationals.com/2019/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2019/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2019/images/header_image_news.jpg',
        schedule: 'http://mbacnationals.com/2019/images/header_image_schedule.jpg',
        lanedraw: 'http://mbacnationals.com/2019/images/header_image_lanedraw.jpg',
        contingents: 'http://mbacnationals.com/2019/images/header_image_contingents.jpg',
        souvenirs: 'http://mbacnationals.com/2019/images/header_image_souvenirs.jpg',
        sponsors: 'http://mbacnationals.com/2019/images/header_image_sponsors.jpg',
        centres: 'http://mbacnationals.com/2019/images/header_image_centres.jpg',
        location: 'http://mbacnationals.com/2019/images/header_image_location.jpg',
        history: 'http://mbacnationals.com/2019/images/header_image_history.jpg',
      },
    },
    2018: {
      Theme: 'theme-2018',
      TimeZone: 'EDT',
      Welcome: `Welcome to the online home of the 2018 Master Bowlers Association of Canada Nationals,
               taking place June 30 - July 4, 2018 in Thunder Bay, ON. <br />
               <a href="https://www.pscp.tv/MBACNationals2018" target="_blank">Click here for Live Streaming!!!</a>`,
      // tslint:disable-next-line:max-line-length
      ScheduleUrl: 'https://www.googleapis.com/calendar/v3/calendars/01bjjf3a3beq7h3ol9rlu4b648@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY',
      Logo: 'http://mbacnationals.com/2018/images/Logo.png',
      Header: 'http://mbacnationals.com/2018/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2018/images/header_image_default.jpg',
        results: 'http://mbacnationals.com/2018/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2018/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2018/images/header_image_news.jpg',
        schedule: 'http://mbacnationals.com/2018/images/header_image_schedule.jpg',
        lanedraw: 'http://mbacnationals.com/2018/images/header_image_lanedraw.jpg',
        contingents: 'http://mbacnationals.com/2018/images/header_image_contingents.jpg',
        souvenirs: 'http://mbacnationals.com/2018/images/header_image_souvenirs.jpg',
        sponsors: 'http://mbacnationals.com/2018/images/header_image_sponsors.jpg',
        centres: 'http://mbacnationals.com/2018/images/header_image_centres.jpg',
        location: 'http://mbacnationals.com/2018/images/header_image_location.jpg',
        history: 'http://mbacnationals.com/2018/images/header_image_history.jpg',
      },
    },
    2017: {
      Theme: 'theme-2017',
      Welcome: `Welcome to the online home of the 2017 Master Bowlers Association of Canada Nationals,
        taking place June 29 - July 3, 2017 in Regina, SK.`,
      // tslint:disable-next-line:max-line-length
      ScheduleUrl: 'https://www.googleapis.com/calendar/v3/calendars/smk8ud28p3nbej0iebr5dum4vc@group.calendar.google.com/events?key=AIzaSyAdeUS3weAGDePVRgV5x5B3u5_aHSRNvOY',
      Logo: 'http://mbacnationals.com/2017/images/Logo.png',
      Header: 'http://mbacnationals.com/2017/images/background.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2017/images/header_image_schedule.jpg',
        results: 'http://mbacnationals.com/2017/images/header_image_results.jpg',
        statistics: 'http://mbacnationals.com/2017/images/header_image_results.jpg',
        news: 'http://mbacnationals.com/2017/images/header_image_news.jpg',
        schedule: 'http://mbacnationals.com/2017/images/header_image_schedule.jpg',
        lanedraw: 'http://mbacnationals.com/2017/images/header_image_lanedraw.jpg',
        contingents: 'http://mbacnationals.com/2017/images/header_image_contingents.jpg',
        souvenirs: 'http://mbacnationals.com/2017/images/header_image_souvenirs.jpg',
        sponsors: 'http://mbacnationals.com/2017/images/header_image_sponsors.jpg',
        centres: 'http://mbacnationals.com/2017/images/header_image_centres.jpg',
        location: 'http://mbacnationals.com/2017/images/header_image_location.jpg',
        history: 'http://mbacnationals.com/2017/images/header_image_history.jpg',
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
        schedule: 'http://mbacnationals.com/2016/images/header_image_10.jpg',
        lanedraw: 'http://mbacnationals.com/2016/images/header_image_5.jpg',
        contingents: 'http://mbacnationals.com/2016/images/header_image_3.jpg',
        souvenirs: 'http://mbacnationals.com/2016/images/header_image_9.jpg',
        sponsors: 'http://mbacnationals.com/2016/images/header_image_8.jpg',
        centres: 'http://mbacnationals.com/2016/images/header_image_2.jpg',
        location: 'http://mbacnationals.com/2016/images/header_image_4.jpg',
        history: 'http://mbacnationals.com/2016/images/header_image_1.jpg',
      },
    },
    2015: {
      Theme: 'theme-2015',
      Logo: 'http://mbacnationals.com/2015/images/2015_logo.png',
      Header: 'http://mbacnationals.com/2015/images/header_bkg.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2015/images/header_image_5.jpg',
        results: 'http://mbacnationals.com/2015/images/header_image_3.jpg',
        statistics: 'http://mbacnationals.com/2015/images/header_image_7.jpg',
        news: 'http://mbacnationals.com/2015/images/header_image_2.jpg',
        schedule: 'http://mbacnationals.com/2015/images/header_image_1.jpg',
        lanedraw: 'http://mbacnationals.com/2015/images/header_image_4.jpg',
        contingents: 'http://mbacnationals.com/2015/images/header_image_10.jpg',
        souvenirs: 'http://mbacnationals.com/2015/images/header_image_6.jpg',
        sponsors: 'http://mbacnationals.com/2015/images/header_image_1.jpg',
        centres: 'http://mbacnationals.com/2015/images/header_image_9.jpg',
        location: 'http://mbacnationals.com/2015/images/header_image_8.jpg',
        history: 'http://mbacnationals.com/2015/images/header_image_5.jpg',
      },
    },
    2014: {
      Theme: 'theme-2014',
      Logo: 'http://mbacnationals.com/2014/images/2014_logo.png',
      Header: 'http://mbacnationals.com/2014/images/header_bkg.jpg',
      Banners: {
        default: 'http://mbacnationals.com/2014/images/header_image_2.jpg',
        results: 'http://mbacnationals.com/2014/images/header_image_8.jpg',
        statistics: 'http://mbacnationals.com/2014/images/header_image_10.jpg',
        news: 'http://mbacnationals.com/2014/images/header_image_7.jpg',
        schedule: 'http://mbacnationals.com/2014/images/header_image_9.jpg',
        lanedraw: 'http://mbacnationals.com/2014/images/header_image_8.jpg',
        contingents: 'http://mbacnationals.com/2014/images/header_image_4.jpg',
        souvenirs: 'http://mbacnationals.com/2014/images/header_image_5.jpg',
        sponsors: 'http://mbacnationals.com/2014/images/header_image_3.jpg',
        centres: 'http://mbacnationals.com/2014/images/header_image_12.jpg',
        location: 'http://mbacnationals.com/2014/images/header_image_6.jpg',
        history: 'http://mbacnationals.com/2014/images/header_image_11.jpg',
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

  getSchedule(tournament: Tournament): Observable<any[]> {
    return this.http
      .get<any>(tournament.ScheduleUrl)
      .pipe(
        map(data => ([ ...data.items ]))
      );
  }
}
