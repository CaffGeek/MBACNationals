import { Component, OnChanges, Input } from '@angular/core';

@Component({
  selector: 'app-bowlinggame',
  templateUrl: './bowlinggame.component.html',
  styleUrls: ['./bowlinggame.component.scss']
})
export class BowlinggameComponent implements OnChanges {
  @Input() shots: string;
  game: any;

  constructor() { }

  ngOnChanges(changes: any): void {
    if (!changes?.shots) { return; }

    this.buildGame(this.shots);
  }

  buildGame(aShots: string): any {
    const upperShots = aShots.toUpperCase();

    const game = { frames: [], score: 0, fouls: 0 };
    const normalizedShots = [];
    for (let i = 0; i < upperShots.length; i++) {
      if (upperShots[i] === '1') { // 2 digit shots like 11, 13, 15, etc...
        normalizedShots.push(upperShots[i] + upperShots[++i]);
      } else if (upperShots[i] === 'F') { // track fouls
          game.fouls++;
      } else { // all the rest
          normalizedShots.push(upperShots[i]);
      }
    }

    let currentFrame = { number: 1, shots: [], score: 0, runningScore: 0 };
    game.frames.push(currentFrame);
    for (let i = 0; i < normalizedShots.length && currentFrame.number <= 10; i++) {
        const shot = normalizedShots[i];

        const shotScore = this.calcShotScore(normalizedShots, i);
        currentFrame.shots.push(shot);

        currentFrame.score += shotScore;

        if (shot === 'X' && currentFrame.number !== 10) {
            if (normalizedShots[i + 1]) {
                currentFrame.score += this.calcShotScore(normalizedShots, i + 1);
            }
            if (normalizedShots[i + 2]) {
                currentFrame.score += this.calcShotScore(normalizedShots, i + 2);
            }
        }

        if (shot === '/' && normalizedShots[i + 1] && currentFrame.number !== 10) {
            currentFrame.score += this.calcShotScore(normalizedShots, i + 1);
        }

        if (currentFrame.shots.length === 3 || (currentFrame.score >= 15 && currentFrame.number !== 10)) {
            game.score += currentFrame.score;
            currentFrame.runningScore = game.score;

            if (currentFrame.number !== 10) {
                currentFrame = { number: currentFrame.number + 1, shots: [], score: 0, runningScore: 0 };
                game.frames.push(currentFrame);
            }
        }
    }

    this.game = game;
  }

  calcShotScore(shots, i) {
    const shot = shots[i];

    switch (shot) {
        case 'X': return 15;
        case 'R': return 13;
        case 'L': return 13;
        case 'D': return 12;
        case 'A': return 11;
        case 'C': return 10;
        case 'S': return 8;
        case 'H': return 5;
        case '-': return 0;
        case '/': return 15 - this.calcShotScore(shots, i - 1);
        default: return shot * 1;
    }
  }
}
