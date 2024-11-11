import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptPageComponent } from './receipt-page.component';

describe('ReceiptPageComponent', () => {
  let component: ReceiptPageComponent;
  let fixture: ComponentFixture<ReceiptPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceiptPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
