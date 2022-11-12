import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {Guid} from "guid-typescript";
import {Category} from "../models/category";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient : HttpClient) { }

  getAllLots() : Observable<Category[]> {
    return this.httpClient.get<Category[]>(`${environment.apiUrl}/categories`);
  }

  getById(id : Guid) : Observable<Category> {
    return this.httpClient.get<Category>(`${environment.apiUrl}/lots/${id}`)
  }

  getByName(name : string) : Observable<Category> {
    return this.httpClient.get<Category>(`${environment.apiUrl}/lots/${name}`)
  }
}
