import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Centre } from './models/centre';

@Injectable({
  providedIn: 'root'
})
export class CentresService {

  constructor(private http: HttpClient) { }

  getCentres(year): Observable<Centre[]> {
    return this.http
      .get<Centre[]>(`${environment.apiEndPoint}/Centres/List/${year}`)
      .pipe(
        map((centres: Centre[]) => {
          return centres.map(x => ({
            ...x,
            ImageUrl: `${environment.apiEndPoint}/Centres/Image/${x.Id}`,
          }));
        })
      );
  }
}
