import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitSearchComponent } from './exhibit-search.component';

describe('ExhibitSearchComponent', () => {
  let component: ExhibitSearchComponent;
  let fixture: ComponentFixture<ExhibitSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
