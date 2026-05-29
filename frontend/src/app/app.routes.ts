import {Routes} from '@angular/router';
import {LoansComponent} from './loans/loans.component';
import {CreateLoanComponent} from './loans/create-loan/create-loan.component';
import {AppComponent} from './app.component';

export const routes: Routes = [
  {path: '', component: LoansComponent},
  {path: 'loans/create', component: CreateLoanComponent},
  {path: '**', redirectTo: ''},
];
