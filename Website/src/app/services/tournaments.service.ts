import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TournamentsService {

  constructor(private http: HttpClient) { }

  getTournaments(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiEndPoint}/Tournament/All`);
  }
}
