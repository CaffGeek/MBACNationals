import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  constructor(private http: HttpClient) { }

  getNews(year): Observable<any[]> {
    return this.http
      .get<any[]>(`${environment.apiEndPoint}/News/List/${year}`);
  }
}
