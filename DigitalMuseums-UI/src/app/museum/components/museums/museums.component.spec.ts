import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MuseumsComponent } from './museums.component';

describe('MuseumsComponent', () => {
  let component: MuseumsComponent;
  let fixture: ComponentFixture<MuseumsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MuseumsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MuseumsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
