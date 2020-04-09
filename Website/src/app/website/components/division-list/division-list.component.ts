import { Component, OnChanges, Input } from '@angular/core';

@Component({
  selector: 'app-division-list[prefix]',
  templateUrl: './division-list.component.html',
  styleUrls: ['./division-list.component.scss']
})
export class DivisionListComponent implements OnChanges {
  @Input() prefix: string;
  @Input() includePoaSingles = false;
  @Input() includeStepladder = false;
  divisions = [];

  constructor() { }

  ngOnChanges(changes: any): void {
    const divisions = [
      { Order: 1, Name: 'Tournament Men Single' },
      { Order: 2, Name: 'Tournament Women Single' },
      { Order: 3, Name: 'Tournament Men' },
      { Order: 4, Name: 'Tournament Women' },
      { Order: 5, Name: 'Teaching Men' },
      { Order: 6, Name: 'Teaching Men Single', PoaSingle: true },
      { Order: 7, Name: 'Teaching Women' },
      { Order: 8, Name: 'Teaching Women Single', PoaSingle: true },
      { Order: 9, Name: 'Seniors' },
      { Order: 10, Name: 'Seniors Single', PoaSingle: true },
    ];

    this.divisions = [
      ...divisions.filter(x => !x.PoaSingle),
      ...divisions.filter(x => this.includePoaSingles && x.PoaSingle),
    ].sort((a, b) => a.Order - b.Order);
  }

}
