import { Component, OnChanges, Input } from '@angular/core';

import { StatisticsService } from 'src/app/services/statistics.service';
import { HighScores } from 'src/app/services/models/highscores';
import { Match } from 'src/app/services/models/match';

@Component({
  selector: 'app-highscores[year][division][stat]',
  templateUrl: './highscores.component.html',
  styleUrls: ['./highscores.component.scss']
})
export class HighscoresComponent implements OnChanges {
  @Input() year: number;
  @Input() limit: number;
  @Input() division: string;
  @Input() game: number;
  @Input() stat: string;

  men: any[];
  women: any[];

  constructor(
    private highscoresService: StatisticsService
  ) { }

  ngOnChanges(changes: any): void {
    if (!(changes?.stat || changes?.year || changes?.game || changes?.division)) {
      return;
    }

    if (['Wins', 'Average'].includes(this.stat)) {
      this.highscoresService.getIndividualStats(this.year, this.division)
        .subscribe(individualStats => {
          const mens = individualStats
            .filter(x => x.Gender === 'M');
          const womens = individualStats
            .filter(x => x.Gender !== 'M');

          this.men = mens.sort((a, b) => b[this.stat] - a[this.stat]).slice(0, this.limit || 10);
          this.women = womens.sort((a, b) => b[this.stat] - a[this.stat]).slice(0, this.limit || 10);
        });

    } else {
      this.highscoresService.getHighScores(this.year, this.division)
        .subscribe(highscores => {
          const filteredByGame = (!!this.game)
            ? highscores.Scores.filter(x => x.Number === this.game)
            : highscores.Scores;

          const mensScores = filteredByGame
            .filter(x => x.Gender === 'M');
          const womensScores = filteredByGame
            .filter(x => x.Gender !== 'M');

          this.men = mensScores.sort((a, b) => b[this.stat] - a[this.stat]).slice(0, this.limit || 10);
          this.women = womensScores.sort((a, b) => b[this.stat] - a[this.stat]).slice(0, this.limit || 10);
        });
      }
  }

}
