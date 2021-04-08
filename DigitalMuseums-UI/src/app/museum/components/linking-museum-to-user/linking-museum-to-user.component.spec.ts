import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkingMuseumToUserComponent } from './linking-museum-to-user.component';

describe('LinkingMuseumToUserComponent', () => {
  let component: LinkingMuseumToUserComponent;
  let fixture: ComponentFixture<LinkingMuseumToUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LinkingMuseumToUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkingMuseumToUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
