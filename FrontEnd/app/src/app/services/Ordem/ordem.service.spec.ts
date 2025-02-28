import { TestBed } from '@angular/core/testing';
import { OrdemSevice } from './ordem.service';


describe('OrdemSevice', () => {
  let service: OrdemSevice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrdemSevice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
