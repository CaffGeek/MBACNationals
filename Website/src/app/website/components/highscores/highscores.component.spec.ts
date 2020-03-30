import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HighscoresComponent } from './highscores.component';

describe('HighscoresComponent', () => {
  let component: HighscoresComponent;
  let fixture: ComponentFixture<HighscoresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HighscoresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HighscoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
