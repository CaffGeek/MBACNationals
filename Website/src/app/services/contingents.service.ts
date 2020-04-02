import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Contingent } from './models/contingent';

@Injectable({
  providedIn: 'root'
})
export class ContingentsService {

  constructor(private http: HttpClient) { }

  getContingent(year, province): Observable<Contingent> {
    return this.http
      .get<Contingent>(`${environment.apiEndPoint}/Contingent?province=${province}&year=${year}`);
  }
}
