import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomCheckboxComponent } from './checkbox.component';

describe('CustomCheckboxComponent', () => {
  let component: CustomCheckboxComponent;
  let fixture: ComponentFixture<CustomCheckboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomCheckboxComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomCheckboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
