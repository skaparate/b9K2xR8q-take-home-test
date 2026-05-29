import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanPaymentDialogComponent } from './loan-payment-dialog.component';

describe('LoanPaymentDialogComponent', () => {
  let component: LoanPaymentDialogComponent;
  let fixture: ComponentFixture<LoanPaymentDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoanPaymentDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanPaymentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
