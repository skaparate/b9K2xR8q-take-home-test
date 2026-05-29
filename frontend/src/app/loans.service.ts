import {Injectable} from '@angular/core';

import {environment} from '../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LoanDetails} from '../types/loan-details';

@Injectable({
  providedIn: 'root'
})
export class LoansService {
  constructor(private http: HttpClient) {
  }

  public getLoans(): Observable<LoanDetails[]> {
    return this.http.get<LoanDetails[]>(environment.apiUrl + '/loans');
  }

  public payLoan(loanId: number, amount: number): Observable<LoanDetails> {
    return this.http.post<LoanDetails>(environment.apiUrl + '/loans/' + loanId, {
      amount
    })
  }

  public createLoan(amountRequested: number, accountHolderId: number): Observable<LoanDetails> {
    return this.http.post<LoanDetails>(environment.apiUrl + '/loans', {
      amountRequested,
      accountHolderId
    });
  }
}
