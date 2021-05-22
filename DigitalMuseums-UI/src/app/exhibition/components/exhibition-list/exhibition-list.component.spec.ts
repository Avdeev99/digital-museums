import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitionListComponent } from './exhibition-list.component';

describe('ExhibitionListComponent', () => {
  let component: ExhibitionListComponent;
  let fixture: ComponentFixture<ExhibitionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitionListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
