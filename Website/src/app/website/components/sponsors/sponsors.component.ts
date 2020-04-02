import { Component, OnChanges, Input } from '@angular/core';

import { SponsorsService } from 'src/app/services/sponsors.service';
import { Sponsor } from 'src/app/services/models/sponsor';
import { interval } from 'rxjs';
import { startWith } from 'rxjs/operators';

@Component({
  selector: 'app-sponsors[year]',
  templateUrl: './sponsors.component.html',
  styleUrls: ['./sponsors.component.scss']
})
export class SponsorsComponent implements OnChanges {
  @Input() year: number;
  @Input() isRotating: false;
  sponsor: Sponsor;
  sponsors: Sponsor[];

  constructor(
    private sponsorsService: SponsorsService
  ) { }

  ngOnChanges(changes: any): void {
    if (changes && changes.year) {
      this.sponsorsService.getSponsors(this.year)
        .subscribe((sponsors: Sponsor[]) => {
          this.sponsors = sponsors.sort((a, b) => a.Position - b.Position);

          if (this.isRotating) {
            const secondsCounter = interval(4000).pipe(startWith(0));

            secondsCounter.subscribe((n) => {
              const curr = this.sponsors.find(x => x.Id === this.sponsor?.Id);
              const ix = this.sponsors.indexOf(curr);

              this.sponsor = {
                ...this.sponsors[ix + 1]
              };
            });
          }
        });
    }
  }
}
