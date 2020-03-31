import { Component, Input, OnChanges } from '@angular/core';

import { NewsService } from 'src/app/services/news.service';
import { NewsItem } from 'src/app/services/models/newsItem';

@Component({
  selector: 'app-news[year]',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnChanges {
  @Input() year: number;
  @Input() limit: number;
  news: NewsItem[];

  constructor(
    private newsService: NewsService
  ) {
  }

  ngOnChanges(changes: any): void {
    if (changes && changes.year) {
      this.newsService.getNews(this.year)
        .subscribe(news => this.news = this.limit ? news.slice(0, this.limit) : news);
    }
  }
}
