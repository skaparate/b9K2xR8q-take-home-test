import {Injectable} from '@angular/core';
import {AccountHolder} from '../types/account-holder';
import {HttpClient} from '@angular/common/http';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountHoldersService {
  constructor(private readonly httpClient: HttpClient) {
  }

  public getAccountHolders(name: string | undefined) {
    let url = environment.apiUrl + '/account-holders';

    if (name && name.trim()) {
      url += `?name=${name}`;
    }

    return this.httpClient.get<AccountHolder[]>(url);
  }
}
