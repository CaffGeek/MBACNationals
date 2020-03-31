import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Sponsor } from './models/sponsor';

@Injectable({
  providedIn: 'root'
})
export class SponsorsService {
  constructor(private http: HttpClient) { }

  getSponsors(year): Observable<Sponsor[]> {
    return this.http
      .get<Sponsor[]>(`${environment.apiEndPoint}/Sponsors/List/${year}`);
  }
}
