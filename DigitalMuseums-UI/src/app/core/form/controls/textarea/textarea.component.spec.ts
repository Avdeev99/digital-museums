import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomTextareaComponent } from './textarea.component';

describe('CustomTextareaComponent', () => {
  let component: CustomTextareaComponent;
  let fixture: ComponentFixture<CustomTextareaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomTextareaComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomTextareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
