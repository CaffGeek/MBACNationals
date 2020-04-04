import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Sponsor } from './models/sponsor';

@Injectable({
  providedIn: 'root'
})
export class SponsorsService {
  constructor(private http: HttpClient) { }

  getSponsors(year): Observable<Sponsor[]> {
    return this.http
      .get<Sponsor[]>(`${environment.apiEndPoint}/Sponsors/List/${year}`)
      .pipe(
        map((sponsors: Sponsor[]) => {
          return sponsors.map(x => ({
            ...x,
            ImageUrl:  `${environment.apiEndPoint}/Sponsors/Image/${x.Id}`,
          }));
        })
      );
  }
}
