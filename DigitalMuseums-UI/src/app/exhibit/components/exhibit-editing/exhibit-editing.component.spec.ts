import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitEditingComponent } from './exhibit-editing.component';

describe('ExhibitEditingComponent', () => {
  let component: ExhibitEditingComponent;
  let fixture: ComponentFixture<ExhibitEditingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitEditingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
