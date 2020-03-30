import { Component, OnInit, Input } from '@angular/core';
import { Tournament } from 'src/app/services/models/tournament';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  @Input() tournament: Tournament;

  constructor() { }

  ngOnInit(): void {
  }

}
