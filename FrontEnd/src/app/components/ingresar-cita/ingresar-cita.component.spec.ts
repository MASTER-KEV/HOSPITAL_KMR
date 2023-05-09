import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngresarCitaComponent } from './ingresar-cita.component';

describe('IngresarCitaComponent', () => {
  let component: IngresarCitaComponent;
  let fixture: ComponentFixture<IngresarCitaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IngresarCitaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngresarCitaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
