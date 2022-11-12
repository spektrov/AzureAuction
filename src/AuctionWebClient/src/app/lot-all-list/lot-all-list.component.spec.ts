import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotAllListComponent } from './lot-all-list.component';

describe('LotAllListComponent', () => {
  let component: LotAllListComponent;
  let fixture: ComponentFixture<LotAllListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotAllListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotAllListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
