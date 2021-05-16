import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInfoEditingComponent } from './user-info-editing.component';

describe('UserInfoEditingComponent', () => {
  let component: UserInfoEditingComponent;
  let fixture: ComponentFixture<UserInfoEditingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserInfoEditingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserInfoEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
