import { Component, OnChanges, Input } from '@angular/core';
import { LocationsService } from 'src/app/services/location.service';
import { Location } from 'src/app/services/models/location';

@Component({
  selector: 'app-location[year]',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss']
})
export class LocationComponent implements OnChanges {
  @Input() year: number;
  locations: Location[];

  constructor(
    private locationService: LocationsService
  ) {
  }

  ngOnChanges(changes: any): void {
    if (changes && changes.year) {
      this.locationService.getLocations(this.year)
        .subscribe(locations => this.locations = locations);
    }
  }
}
