import { Component, OnInit, Input } from '@angular/core';
import { Participant } from 'src/app/services/models/contingent';

@Component({
  selector: 'app-participant-name',
  templateUrl: './participant-name.component.html',
  styleUrls: ['./participant-name.component.scss']
})
export class ParticipantNameComponent implements OnInit {
  @Input() participant: Participant;

  constructor() { }

  ngOnInit(): void {
  }

}
