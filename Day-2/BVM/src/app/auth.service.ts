import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() {}

  // Simulated login function
  login(email: string, password: string): Observable<boolean> {
    
     // Hardcoded credentials for demonstration purposes
     const hardcodedEmail = 'test@example.com';
     const hardcodedPassword = 'password123';

    return of(email === hardcodedEmail && password === hardcodedPassword);
  }

  // Simulated function to check if user is logged in
  isLoggedIn(): boolean {
    // This should check real authentication state (e.g., token existence/validity)
    return !!localStorage.getItem('user');
  }

  // Simulated function to logout
  logout(): void {
    localStorage.removeItem('user');
  }
}
