import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MesArticles } from './mes-articles';

describe('MesArticles', () => {
  let component: MesArticles;
  let fixture: ComponentFixture<MesArticles>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MesArticles],
    }).compileComponents();

    fixture = TestBed.createComponent(MesArticles);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
