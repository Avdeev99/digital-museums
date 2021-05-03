import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExhibitionEditingComponent } from './exhibition-editing.component';

describe('ExhibitionEditingComponent', () => {
  let component: ExhibitionEditingComponent;
  let fixture: ComponentFixture<ExhibitionEditingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExhibitionEditingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExhibitionEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
