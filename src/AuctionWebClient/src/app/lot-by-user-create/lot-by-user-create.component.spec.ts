import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotByUserCreateComponent } from './lot-by-user-create.component';

describe('LotByUserCreateComponent', () => {
  let component: LotByUserCreateComponent;
  let fixture: ComponentFixture<LotByUserCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LotByUserCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotByUserCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
