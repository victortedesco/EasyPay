import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatementPageComponent } from './statement-page.component';

describe('ExtractPageComponent', () => {
  let component: StatementPageComponent;
  let fixture: ComponentFixture<StatementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatementPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
