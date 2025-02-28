import { TestBed } from '@angular/core/testing';
import { ClienteSevice } from './cliente.service';


describe('ClienteSevice', () => {
  let service: ClienteSevice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClienteSevice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
