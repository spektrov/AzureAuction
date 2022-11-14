import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BidMakeComponent } from './bid-make.component';

describe('BidMakeComponent', () => {
  let component: BidMakeComponent;
  let fixture: ComponentFixture<BidMakeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BidMakeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BidMakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
