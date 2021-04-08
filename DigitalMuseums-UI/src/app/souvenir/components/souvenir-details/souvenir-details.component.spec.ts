import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SouvenirDetailsComponent } from './souvenir-details.component';

describe('SouvenirDetailsComponent', () => {
  let component: SouvenirDetailsComponent;
  let fixture: ComponentFixture<SouvenirDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SouvenirDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SouvenirDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
