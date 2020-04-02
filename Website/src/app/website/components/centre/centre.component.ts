import { Component, OnChanges, Input } from '@angular/core';
import { CentresService } from 'src/app/services/centres.service';
import { Centre } from 'src/app/services/models/centre';

@Component({
  selector: 'app-centre',
  templateUrl: './centre.component.html',
  styleUrls: ['./centre.component.scss']
})
export class CentreComponent implements OnChanges {
  @Input() year: number;
  centres: Centre[];

  constructor(
    private centresService: CentresService
  ) {
  }

  ngOnChanges(changes: any): void {
    if (changes && changes.year) {
      this.centresService.getCentres(this.year)
        .subscribe(centres => this.centres = centres);
    }
  }
}
