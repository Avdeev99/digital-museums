import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SouvenirListComponent } from './souvenir-list.component';

describe('SouvenirListComponent', () => {
  let component: SouvenirListComponent;
  let fixture: ComponentFixture<SouvenirListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SouvenirListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SouvenirListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
