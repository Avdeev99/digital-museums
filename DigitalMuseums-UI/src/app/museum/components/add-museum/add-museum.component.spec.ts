import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMuseumComponent } from './add-museum.component';

describe('AddMuseumComponent', () => {
  let component: AddMuseumComponent;
  let fixture: ComponentFixture<AddMuseumComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMuseumComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMuseumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
