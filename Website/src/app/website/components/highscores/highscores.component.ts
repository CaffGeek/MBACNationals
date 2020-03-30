import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-highscores[year]',
  templateUrl: './highscores.component.html',
  styleUrls: ['./highscores.component.scss']
})
export class HighscoresComponent implements OnInit {
  @Input() year: number;

  constructor() { }

  ngOnInit(): void {
  }

}
