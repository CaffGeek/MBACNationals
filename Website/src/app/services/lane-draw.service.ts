import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LaneDraw } from './models/lane-draw';

@Injectable({
  providedIn: 'root'
})
export class LaneDrawService {
  constructor(private http: HttpClient) { }

  getLaneDraw(year, division): Observable<LaneDraw> {
    const params = new HttpParams()
      .set('year', year.toString())
      .set('division', division.replace(/Women/i, 'Ladies'));

    return this.http
      .get<LaneDraw>(`${environment.apiEndPoint}/Scores/Schedule`, { params });
  }
}
