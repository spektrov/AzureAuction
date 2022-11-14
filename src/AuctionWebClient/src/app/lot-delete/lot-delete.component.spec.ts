import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotDeleteComponent } from './lot-delete.component';

describe('LotDeleteComponent', () => {
  let component: LotDeleteComponent;
  let fixture: ComponentFixture<LotDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotDeleteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
