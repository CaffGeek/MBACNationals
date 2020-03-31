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
  @Input() limit: 3;
  @Input() division: string;
  highscores: Match[];
  menScratch: Match[];
  womenScratch: Match[];
  menPoa: Match[];
  womenPoa: Match[];

  constructor(
    private highscoresService: HighscoresService
  ) { }

  ngOnChanges(changes: any): void {
    if (changes && changes.year) {
      this.highscoresService.getHighscores(this.year, this.division)
        .subscribe(highscores => {
          this.highscores = highscores.Scores.slice(0, this.limit);
        });
    }
  }

}
