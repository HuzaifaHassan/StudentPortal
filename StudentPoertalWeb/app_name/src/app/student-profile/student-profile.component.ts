import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class ProfileComponent implements OnInit {
  studentDetails: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const studentId = localStorage.getItem('studentId');
    if (studentId) {
      const data = { id: studentId };
      this.http.post<any>('https://localhost:7120/Api/Auth/GetStudentDetails', data).subscribe(result => {
        this.studentDetails = result.data;
      }, error => {
        console.error(error);
      });
    }
  }
}