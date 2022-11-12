import { TestBed } from '@angular/core/testing';

import { SelectedLotServiceService } from './selected-lot.service';

describe('SelectedLotServiceService', () => {
  let service: SelectedLotServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SelectedLotServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
