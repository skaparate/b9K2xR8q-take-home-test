import { TestBed } from '@angular/core/testing';

import { AccountHoldersService } from './account-holders.service';

describe('AccountHoldersService', () => {
  let service: AccountHoldersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountHoldersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
