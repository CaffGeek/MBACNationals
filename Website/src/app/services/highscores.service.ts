import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HighScores } from './models/highscores';


@Injectable({
  providedIn: 'root'
})
export class HighscoresService {
  constructor(private http: HttpClient) { }

  getHighscores(year: number, division: string): Observable<HighScores> {
    const params = new HttpParams()
      .set('year', year.toString())
      .set('division', division);

    return this.http
      .get<HighScores>(`${environment.apiEndPoint}/Scores/HighScores/`, { params });
  }
}
