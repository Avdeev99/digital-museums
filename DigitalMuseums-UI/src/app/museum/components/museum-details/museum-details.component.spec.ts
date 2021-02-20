import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MuseumDetailsComponent } from './museum-details.component';

describe('MuseumDetailsComponent', () => {
  let component: MuseumDetailsComponent;
  let fixture: ComponentFixture<MuseumDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MuseumDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MuseumDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
