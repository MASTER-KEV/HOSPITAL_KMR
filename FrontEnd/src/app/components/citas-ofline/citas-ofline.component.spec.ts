import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CitasOflineComponent } from './citas-ofline.component';

describe('CitasOflineComponent', () => {
  let component: CitasOflineComponent;
  let fixture: ComponentFixture<CitasOflineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CitasOflineComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CitasOflineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
