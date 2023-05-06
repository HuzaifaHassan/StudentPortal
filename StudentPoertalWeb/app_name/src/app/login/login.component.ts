import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username!: string;
  password!: string;

  constructor(private http: HttpClient,private router: Router) {}

  onSubmit() {
    const data = { UserName: this.username, Password: this.password };
    this.http.post<any>('https://localhost:7120/Api/Auth/Login', data).subscribe(result => {
      localStorage.setItem('studentId', result.data);
      this.router.navigate(['student-profile', result.data]);
    },
        error => {
          // Handle login error
          console.log('Login successful:', error);
        }
      );
  }
}