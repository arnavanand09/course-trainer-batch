// src/app/services/batch.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Batch } from '../models/batch';

@Injectable({
  providedIn: 'root'
})
export class BatchService {
  private apiUrl = 'http://localhost:5248/api/batch';

  constructor(private http: HttpClient) {}

  getBatches(): Observable<Batch[]> {
    return this.http.get<Batch[]>(this.apiUrl);
  }

  getBatch(id: number): Observable<Batch> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Batch>(url);
  }

  addBatch(batch: Batch): Observable<Batch> {
    return this.http.post<Batch>(this.apiUrl, batch);
  }

  updateBatch(id: number, batch: Batch): Observable<Batch> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<Batch>(url, batch);
  }

  deleteBatch(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url);
  }
}