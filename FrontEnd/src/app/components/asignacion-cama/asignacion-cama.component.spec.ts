import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignacionCamaComponent } from './asignacion-cama.component';

describe('AsignacionCamaComponent', () => {
  let component: AsignacionCamaComponent;
  let fixture: ComponentFixture<AsignacionCamaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsignacionCamaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AsignacionCamaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
