import {Component, inject, OnInit} from '@angular/core';
import {LoanDetails} from '../../types/loan-details';
import {LoansService} from '../loans.service';
import {
  MatCell, MatCellDef, MatColumnDef,
  MatHeaderCell, MatHeaderCellDef,
  MatHeaderRow,
  MatHeaderRowDef,
  MatRow,
  MatRowDef,
  MatTable
} from '@angular/material/table';
import {NgIf} from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatDialog} from '@angular/material/dialog';
import {LoanPaymentDialogComponent} from './loan-payment-dialog/loan-payment-dialog.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-loans',
  imports: [
    MatButtonModule,
    MatTable,
    NgIf,
    MatHeaderRowDef,
    MatHeaderRow,
    MatRowDef,
    MatRow,
    MatCell,
    MatHeaderCell,
    MatColumnDef,
    MatHeaderCellDef,
    MatCellDef,
    RouterLink
  ],
  templateUrl: './loans.component.html',
  styleUrl: './loans.component.scss'
})
export class LoansComponent implements OnInit {
  tableColumns: string[] = ['amountRequested', 'amountPaid', 'balance', 'status', 'actions'];
  loanDetailsList: LoanDetails[] = [];
  readonly dialog = inject(MatDialog);

  constructor(private readonly loansService: LoansService) {
  }

  ngOnInit(): void {
    this.loansService.getLoans().subscribe(i => this.loanDetailsList = i);
  }

  onPay(loan: LoanDetails) {
    this.openDialog(loan);
  }

  openDialog(loan: LoanDetails): void {
    const dialogRef = this.dialog.open(LoanPaymentDialogComponent, {
      data: {loanId: loan.id, balance: loan.balance},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if (result !== undefined) {
        console.log('Payment successful for loan ID:', loan.id);
      }
    });
  }

}
