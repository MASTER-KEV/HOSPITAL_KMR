import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardCamasComponent } from './dashboard-camas.component';

describe('DashboardCamasComponent', () => {
  let component: DashboardCamasComponent;
  let fixture: ComponentFixture<DashboardCamasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardCamasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardCamasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
