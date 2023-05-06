import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StudentProfileService {

  constructor(private http: HttpClient) { }

  getStudentDetails(id: string) {
    return this.http.post('https://localhost:7120/Api/Auth/GetStudentDetails', { id });
  }
}