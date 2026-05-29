import {Component, OnInit} from '@angular/core';
import {MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {LoanCreation} from '../../../types/loan-creation';
import {MatAnchor, MatButton} from '@angular/material/button';
import {AccountHolder} from '../../../types/account-holder';
import {AccountHoldersService} from '../../account-holders.service';
import {MatOption} from '@angular/material/core';
import {MatSelect} from '@angular/material/select';
import {NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {LoansService} from '../../loans.service';
import {catchError, finalize, throwError} from 'rxjs';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-create-loan',
  imports: [
    MatInput,
    MatFormField,
    MatLabel,
    MatButton,
    MatOption,
    MatSelect,
    NgIf,
    FormsModule,
    RouterLink,
    MatAnchor
  ],
  templateUrl: './create-loan.component.html',
  styleUrl: './create-loan.component.scss'
})
export class CreateLoanComponent implements OnInit {
  isLoading: boolean = false;
  accountHolders: AccountHolder[] = [];
  loanCreation: LoanCreation = {
    amountRequested: 0,
    accountHolderId: 0
  };
  errorMsg: string | undefined;

  constructor(private readonly accountHoldersService: AccountHoldersService,
              private readonly loansService: LoansService,
              private readonly router: Router) {
  }

  ngOnInit() {
    this.isLoading = true;
    this.accountHoldersService.getAccountHolders(undefined).subscribe(accountHolders => {
      this.accountHolders = accountHolders;
      this.isLoading = false;
    });
  }

  onSubmit() {
    console.log('Loan creation form submitted:', this.loanCreation);
    this.isLoading = true;

    if (!this.validate()) {
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    this.loansService.createLoan(this.loanCreation.amountRequested, this.loanCreation.accountHolderId)
      .pipe(catchError(e => {
          console.error('Loan creation failed:', e);
          return e;
        }),
        finalize(() => this.isLoading = false))
      .subscribe(
        loanDetails => {
          console.log('Loan created successfully:', loanDetails);
          this.router.navigate(['/']).then();
        },
      );
  }

  validate() {
    if (this.loanCreation.amountRequested <= 0) {
      this.errorMsg = 'Amount requested must be greater than zero';
      return false;
    }

    if (!this.loanCreation.accountHolderId) {
      this.errorMsg = 'Account holder must be selected';
      return false;
    }

    this.errorMsg = undefined;

    return true;
  }

}
