import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Location } from './models/location';

@Injectable({
  providedIn: 'root'
})
export class LocationsService {

  constructor(private http: HttpClient) { }

  getLocations(year): Observable<Location[]> {
    return this.http
      .get<Location[]>(`${environment.apiEndPoint}/Hotels/List/${year}`)
      .pipe(
        map((locations: Location[]) => {
          return locations.map(x => ({
            ...x,
            LogoUrl: `${environment.apiEndPoint}/Hotels/Logo/${x.Id}`,
            ImageUrl: `${environment.apiEndPoint}/Hotels/Image/${x.Id}`,
          }));
        })
      );
  }
}
