import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResultsService } from 'src/app/services/results.service';
import { StepladderMatch } from 'src/app/services/models/stepladder';
import { startWith } from 'rxjs/operators';
import { interval } from 'rxjs';

@Component({
  selector: 'app-stepladder',
  templateUrl: './stepladder.component.html',
  styleUrls: ['./stepladder.component.scss']
})
export class StepladderComponent implements OnInit {
  year: number;
  matches: StepladderMatch[];

  constructor(
    private route: ActivatedRoute,
    private resultsService: ResultsService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(({ year }) => {
      this.year = year;

      // Refresh on a timer of 30seconds
      const secondsCounter = interval(30 * 1000).pipe(startWith(0));

      secondsCounter.subscribe((n) => {
        this.resultsService.getStepladder(year)
          .subscribe(matches => this.matches = matches.sort((a, b) => b.Created.toISOString().localeCompare(a.Created.toISOString())));
      });
    });
  }

}
