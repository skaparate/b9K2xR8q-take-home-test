import {Component, inject, Input, OnInit} from '@angular/core';
import {LoansService} from '../../loans.service';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {FormsModule} from '@angular/forms';
import {catchError, throwError} from 'rxjs';
import {NgIf} from '@angular/common';
import {MatError, MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {MatButton} from '@angular/material/button';

type DialogData = {
  loanId: number;
  balance: number;
};

@Component({
  selector: 'app-loan-payment-dialog',
  imports: [
    FormsModule,
    NgIf,
    MatLabel,
    MatFormField,
    MatInput,
    MatButton,
    MatDialogContent,
    MatDialogTitle,
    MatDialogActions,
    MatError
  ],
  templateUrl: './loan-payment-dialog.component.html',
  styleUrl: './loan-payment-dialog.component.scss'
})
export class LoanPaymentDialogComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<LoanPaymentDialogComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);
  amount: number | undefined;
  loading: boolean = false;
  errorMsg: string | undefined;

  constructor(private readonly loansService: LoansService) {
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (!this.amount || this.amount <= 0 || this.amount > this.data.balance) {
      this.errorMsg = 'Amount to pay must be a positive number and not greater than the current balance';
      return;
    }

    this.loading = true;
    this.errorMsg = undefined;
    this.loansService.payLoan(this.data.loanId, this.amount!)
      .pipe(catchError(e => {
        this.loading = false;
        this.errorMsg = 'Failed to pay loan. Please try again later.';
        console.log('Error occurred while paying loan:', e);
        return e;
      }))
      .subscribe(() => {
        this.loading = false;
        this.dialogRef.close();
      });
  }
}
