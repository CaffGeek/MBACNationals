import { TestBed } from '@angular/core/testing';

import { TournamentsService } from './tournaments.service';

describe('TournamentsService', () => {
  let service: TournamentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TournamentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
