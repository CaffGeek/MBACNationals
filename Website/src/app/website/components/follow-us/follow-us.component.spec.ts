import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowUsComponent } from './follow-us.component';

describe('FollowUsComponent', () => {
  let component: FollowUsComponent;
  let fixture: ComponentFixture<FollowUsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FollowUsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FollowUsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
