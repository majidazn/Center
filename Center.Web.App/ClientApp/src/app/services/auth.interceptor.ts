import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      // add authorization header with jwt token if available
    let token = this.getCookie("token");
    if (token) {
    //  request = request.clone({ withCredentials: true });
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });

      //request = request.clone({
      //  headers: request.headers.append('Cookie', document.cookie).append('Authorization', `Bearer ${token}`),
      //});
    }
    return next.handle(request);
  }
    getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
  }

}
