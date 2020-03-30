import { Tournament } from 'src/app/services/models/tournament';
import { Component, Input, OnChanges } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { map, filter } from 'rxjs/operators';

@Component({
  selector: 'app-banner[tournament]',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnChanges {
  @Input() tournament: Tournament;
  logoImage: string;
  headerImage: string;
  bannerImage: string;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ) {
    router.events.pipe(
      filter((event) => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      const [ , , route ] = event.url.split('/');
      this.refresh(route);
    });
  }

  ngOnChanges(changes): void {
    if (changes?.tournament) {
      this.refresh();
    }
  }

  refresh(route?: string): void {
    this.logoImage = this.tournament.Logo;
    this.headerImage = this.tournament.Header;
    this.bannerImage = this.tournament.Banners[route || 'default'];
  }
}
