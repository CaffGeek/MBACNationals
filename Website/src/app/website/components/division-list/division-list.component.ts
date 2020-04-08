import { Component, OnChanges, Input } from '@angular/core';

@Component({
  selector: 'app-division-list',
  templateUrl: './division-list.component.html',
  styleUrls: ['./division-list.component.scss']
})
export class DivisionListComponent implements OnChanges {
  @Input() year: string;
  @Input() includePoaSingles = false;
  divisions = [];

  constructor() { }

  ngOnChanges(changes: any): void {
    const divisions = [
      { Name: 'Tournament Men Single' },
      { Name: 'Tournament Women Single' },
      { Name: 'Tournament Men' },
      { Name: 'Tournament Women' },
      { Name: 'Teaching Men' },
      { Name: 'Teaching Men Single', PoaSingle: true },
      { Name: 'Teaching Women' },
      { Name: 'Teaching Women Single', PoaSingle: true },
      { Name: 'Seniors' },
      { Name: 'Seniors Single', PoaSingle: true },
    ];

    this.divisions = divisions.filter(x => !x.PoaSingle || (this.includePoaSingles && x.PoaSingle))
  }

}
