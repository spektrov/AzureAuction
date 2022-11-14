import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotSelectedComponent } from './lot-selected.component';

describe('LotSelectedComponent', () => {
  let component: LotSelectedComponent;
  let fixture: ComponentFixture<LotSelectedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotSelectedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotSelectedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
