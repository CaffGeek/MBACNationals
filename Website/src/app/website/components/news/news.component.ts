import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.less']
})
export class NewsComponent implements OnInit {
  @Input() year: number;
  news = [];

  constructor(
    private route: ActivatedRoute,
    private newsService: NewsService
  ) {
  }

  ngOnInit(): void {
      this.newsService.getNews(this.year)
        .subscribe(news => this.news = news);
  }
}
