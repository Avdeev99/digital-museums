import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMuseumComponent } from './user-museum.component';

describe('UserMuseumComponent', () => {
  let component: UserMuseumComponent;
  let fixture: ComponentFixture<UserMuseumComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserMuseumComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserMuseumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
