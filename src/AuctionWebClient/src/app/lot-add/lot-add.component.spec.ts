import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotAddComponent } from './lot-add.component';

describe('LotAddComponent', () => {
  let component: LotAddComponent;
  let fixture: ComponentFixture<LotAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
