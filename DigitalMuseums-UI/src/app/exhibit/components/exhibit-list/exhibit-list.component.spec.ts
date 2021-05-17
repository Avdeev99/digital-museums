import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitListComponent } from './exhibit-list.component';

describe('ExhibitListComponent', () => {
  let component: ExhibitListComponent;
  let fixture: ComponentFixture<ExhibitListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
