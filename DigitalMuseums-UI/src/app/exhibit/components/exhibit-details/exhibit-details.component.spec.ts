import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitDetailsComponent } from './exhibit-details.component';

describe('ExhibitDetailsComponent', () => {
  let component: ExhibitDetailsComponent;
  let fixture: ComponentFixture<ExhibitDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
