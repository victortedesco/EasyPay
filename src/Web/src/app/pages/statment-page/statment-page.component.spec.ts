import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatmentPageComponent } from './statment-page.component';

describe('ExtractPageComponent', () => {
  let component: StatmentPageComponent;
  let fixture: ComponentFixture<StatmentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatmentPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatmentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
