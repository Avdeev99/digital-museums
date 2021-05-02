import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitionInstructionalScreenComponent } from './exhibition-instructional-screen.component';

describe('ExhibitionInstructionalScreenComponent', () => {
  let component: ExhibitionInstructionalScreenComponent;
  let fixture: ComponentFixture<ExhibitionInstructionalScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitionInstructionalScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitionInstructionalScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
