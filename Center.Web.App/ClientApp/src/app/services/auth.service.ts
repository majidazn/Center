import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CanDeactivate, RoutesRecognized } from '@angular/router';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthService {

    constructor(private http: HttpClient, private router: Router) {

    }
    

  userAllowedPath: {  name: string }[] = [

    { 'name': '/lab' },
    { 'name': '/lab/page1' },
    { 'name': '/lab/dynamic-grid' },

    { 'name': '/person/page1' },
    { 'name': '/person' },
];

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
   // console.log(state.url);
       
    if (state.url == '/')
      return true;

    for (var i = 0; i < this.userAllowedPath.length; i++) {
      if (state.url == this.userAllowedPath[i].name)
        return true;
    }

    console.log(state.url);//'candidates'
  }

    get token() {
        
    const tkn = localStorage.getItem('token');
    return tkn;
  }

  get isAuthenticated() {

    const tkn = localStorage.getItem('token');
    return !!localStorage.getItem('token');
  }

  baseUrl: any = `/api/account/Login`;

  login(credential: any) {
    
    return this.http.post<any>(this.baseUrl, credential).subscribe(res => {
      if (res) {
        this.authenticate(res);
      } else {
        alert('نام کاربری یا رمز عبور صحیح نمی باشد');
      }
    });
  }

  authenticate(res: any) {
    if (res) {
      if (res.Result) {
        localStorage.setItem('token', res.Result);
        window.location.href = '/';
        // window.location.reload();
      } else {
        alert('نام کاربری یا رمز عبور صحیح نمی باشد');
      }
    }
  }

  logout() {
    localStorage.removeItem('token');
    window.location.reload();
  }
}
