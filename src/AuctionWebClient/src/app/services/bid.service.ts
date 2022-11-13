import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {BidRequest} from "../requests/bid-request";
import {BidResponse} from "../responses/bid-response";

@Injectable({
  providedIn: 'root'
})
export class BidService {

  constructor(private httpClient : HttpClient) { }

  createBid(bid : BidRequest) : Observable<BidResponse> {
    return this.httpClient.post<BidResponse>(`${environment.apiUrl}/bids`, bid);
  }
}
