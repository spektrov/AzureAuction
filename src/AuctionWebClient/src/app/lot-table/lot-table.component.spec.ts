import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotTableComponent } from './lot-table.component';

describe('LotTableComponent', () => {
  let component: LotTableComponent;
  let fixture: ComponentFixture<LotTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
