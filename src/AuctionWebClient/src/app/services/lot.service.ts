import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {LotResponse} from "../responses/lot-response";
import {TaskResponse} from "../responses/task-response";
import {environment} from "../../environments/environment";
import {Guid} from "guid-typescript";
import {LotRequest} from "../requests/lot-request";

@Injectable({
  providedIn: 'root'
})
export class LotService {

  constructor(private httpClient : HttpClient) { }

  getAllLots() : Observable<LotResponse[]> {
    return this.httpClient.get<LotResponse[]>(`${environment.apiUrl}/lots`);
  }

  getById(id : Guid) : Observable<LotResponse> {
    return this.httpClient.get<LotResponse>(`${environment.apiUrl}/lots/${id}`);
  }

  getUserLots(userId : Guid) : Observable<LotResponse[]> {
    return this.httpClient.get<LotResponse[]>(`${environment.apiUrl}/lots/holder/${userId}`);
  }

  getUserBoughtLots(userId : Guid) : Observable<LotResponse[]> {
    return this.httpClient.get<LotResponse[]>(`${environment.apiUrl}/lots/holder/${userId}/bought`);
  }

  addLot(lot : LotRequest) : Observable<string> {
    return this.httpClient.post<string>(`${environment.apiUrl}/lots`, lot);
  }

  deleteLot(lotId: Guid) {
    return this.httpClient.delete<LotResponse>(`${environment.apiUrl}/lots/${lotId}`);
  }
}
