import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";
import {UserResponse} from "../responses/user-response";
import {TokenResponse} from "../responses/token-response";
import {SignupRequest} from "../requests/signup-request";
import {LoginRequest} from "../requests/login-request";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  login(loginRequest: LoginRequest): Observable<TokenResponse> {
    return this.httpClient.post<TokenResponse>(`${environment.apiUrl}/users/login`, loginRequest);
  }

  signup(signupRequest: SignupRequest) {
    return this.httpClient.post(`${environment.apiUrl}/users/signup`, signupRequest, { responseType: 'text'});
  }

  refreshToken(session: TokenResponse) {
    let refreshTokenRequest: any = {
      UserId: session.userId,
      RefreshToken: session.refreshToken
    };

    return this.httpClient.post<TokenResponse>(`${environment.apiUrl}/users/refresh-token`, refreshTokenRequest);
  }

  logout() {
    return this.httpClient.post(`${environment.apiUrl}/users/signup`, null);
  }

  // @ts-ignore
  getUserInfo(): Observable<UserResponse> {
    return this.httpClient.get<UserResponse>(`${environment.apiUrl}/users/info`);
  }
}
