import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotByUserBuyComponent } from './lot-by-user-buy.component';

describe('LotByUserBuyComponent', () => {
  let component: LotByUserBuyComponent;
  let fixture: ComponentFixture<LotByUserBuyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotByUserBuyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotByUserBuyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
