import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Guid} from "guid-typescript";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BlobService {

  constructor(private http: HttpClient) {}

  upload(formData: FormData, lotId : string) {
    let guid = Guid.parse(lotId);
    return this.http.post(`${environment.apiUrl}/blobs/${guid}`, formData);
  }

  getByLot(lotId : Guid) : Observable<string[]> {
    return this.http.get<string[]>(`${environment.apiUrl}/blobs/${lotId}`);
  }
}
