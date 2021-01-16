import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomErrorComponent } from './error.component';

describe('CustomErrorComponent', () => {
  let component: CustomErrorComponent;
  let fixture: ComponentFixture<CustomErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomErrorComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
