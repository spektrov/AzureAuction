import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {TaskResponse} from "../responses/task-response";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Guid} from "guid-typescript";
import {TaskRequest} from "../requests/task-request";

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private httpClient : HttpClient) { }

  getTasks(): Observable<TaskResponse[]> {
    return this.httpClient.get<TaskResponse[]>(`${environment.apiUrl}/tasks`);
  }

  saveTask(task: TaskRequest): Observable<TaskRequest> {
    return this.httpClient.post<TaskRequest>(`${environment.apiUrl}/tasks`, task);
  }

  updateTask(task: TaskResponse): Observable<TaskResponse> {
    return this.httpClient.put<TaskResponse>(`${environment.apiUrl}/tasks`, task);
  }

  deleteTask(taskId: Guid) {
    return this.httpClient.delete<TaskResponse>(`${environment.apiUrl}/tasks/${taskId}`);
  }
}
