import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreEditingComponent } from './genre-editing.component';

describe('GenreEditingComponent', () => {
  let component: GenreEditingComponent;
  let fixture: ComponentFixture<GenreEditingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenreEditingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenreEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
