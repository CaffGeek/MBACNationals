import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DivisionListComponent } from './division-list.component';

describe('DivisionListComponent', () => {
  let component: DivisionListComponent;
  let fixture: ComponentFixture<DivisionListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DivisionListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DivisionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
