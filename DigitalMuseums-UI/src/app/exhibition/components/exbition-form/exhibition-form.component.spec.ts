import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitionFormComponent } from './exhibition-form.component';

describe('ExbitionFormComponent', () => {
  let component: ExhibitionFormComponent;
  let fixture: ComponentFixture<ExhibitionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitionFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
