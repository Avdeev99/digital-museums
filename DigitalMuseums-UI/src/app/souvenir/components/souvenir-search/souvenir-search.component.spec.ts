import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SouvenirSearchComponent } from './souvenir-search.component';

describe('SouvenirSearchComponent', () => {
  let component: SouvenirSearchComponent;
  let fixture: ComponentFixture<SouvenirSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SouvenirSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SouvenirSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
