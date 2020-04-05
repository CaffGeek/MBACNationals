import { Component, OnChanges, Input } from '@angular/core';

import { HighscoresService } from 'src/app/services/highscores.service';
import { HighScores } from 'src/app/services/models/highscores';
import { Match } from 'src/app/services/models/match';

@Component({
  selector: 'app-highscores[year][division]',
  templateUrl: './highscores.component.html',
  styleUrls: ['./highscores.component.scss']
})
export class HighscoresComponent implements OnChanges {
  @Input() year: number;
  @Input() limit: number;
  @Input() division: string;
  @Input() game: number;

  menScratch: Match[];
  womenScratch: Match[];
  menPoa: Match[];
  womenPoa: Match[];

  constructor(
    private highscoresService: HighscoresService
  ) { }

  ngOnChanges(changes: any): void {
    if (!(changes?.year || changes?.game || changes?.division)) {
      return;
    }

    this.highscoresService.getHighscores(this.year, this.division)
      .subscribe(highscores => {
        const filteredByGame = (!!this.game)
          ? highscores.Scores.filter(x => x.Number === this.game)
          : highscores.Scores;

        const mensScores = filteredByGame
          .filter(x => x.Gender === 'M')
          .filter(x => x.POA > 0);
        const womensScores = filteredByGame
          .filter(x => x.Gender !== 'M')
          .filter(x => x.POA > 0);

        this.menScratch = mensScores.sort((a, b) => b.Scratch - a.Scratch).slice(0, this.limit || 10);
        this.womenScratch = womensScores.sort((a, b) => b.POA - a.POA).slice(0, this.limit || 10);
        this.menPoa = mensScores.sort((a, b) => b.Scratch - a.Scratch).slice(0, this.limit || 10);
        this.womenPoa = womensScores.sort((a, b) => b.POA - a.POA).slice(0, this.limit || 10);
      });
  }

}
