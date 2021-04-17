import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SouvenirEditingComponent } from './souvenir-editing.component';

describe('SouvenirEditingComponent', () => {
  let component: SouvenirEditingComponent;
  let fixture: ComponentFixture<SouvenirEditingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SouvenirEditingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SouvenirEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
