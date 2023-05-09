import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnosticosCasosComponent } from './diagnosticos-casos.component';

describe('DiagnosticosCasosComponent', () => {
  let component: DiagnosticosCasosComponent;
  let fixture: ComponentFixture<DiagnosticosCasosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiagnosticosCasosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagnosticosCasosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
