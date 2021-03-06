import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { NewsItem } from './models/newsItem';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  constructor(private http: HttpClient) { }

  getNews(year): Observable<NewsItem[]> {
    return this.http
      .get<NewsItem[]>(`${environment.apiEndPoint}/News/List/${year}`);
  }
}
