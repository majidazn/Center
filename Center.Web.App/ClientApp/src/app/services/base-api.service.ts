import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { HttpHeaders } from '@angular/common/http';
import { throwError as observableThrowError, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { NotifyService } from "./notify.service";
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import { EnvService } from '../env/env.service';

export interface BlobCLass {
  ContentType: string;
  EnableRangeProcessing: boolean;
  EntityTag: string;
  FileContents: string;
  FileDownloadName: string;
  LastModified: Date;
}

@Injectable()
export class BaseApiService {

  constructor(
    private http: HttpClient,
    public notifyService: ToastrService,
    private spinner: NgxSpinnerService,
    private _env: EnvService,
    private router: Router) { }

  upload(url: string, model: any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
      })
    };

    const formData = new FormData();

    formData.append(model.name, model);
    return this.http
      .post<any>(url, formData, httpOptions)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));
  }
  post(url: string, model: any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };


    return this.http
      .post<any>(url, model, httpOptions)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));
  }


  postWithFile(url: string, model: any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'enctype': 'multipart/form-data',
        'Accept': 'application/json'
      })
    };


    return this.http
      .post<any>(url, model, httpOptions)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));
  }


  delete(url: string, data: any): Observable<any> {


    return this.http
      .delete<any>(url, data)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));

  }

  open(url: string): Observable<any> {



    return this.http
      .get<any>(url)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));
  }

  public getFile(url: string): Observable<Blob> {
    const httpOptions: any = {
      headers: new HttpHeaders({
      }),
      responseType: 'blob'
    };
    return this.http
      .get<any>(url, httpOptions)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));
  }

  get(url: string): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };


    return this.http
      .get<any>(url, httpOptions)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));

  }

  postImage(url: string, model: any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'multipart/form-data; charset=utf-8',
      })
    };

    return this.http
      .post<any>(url, model, httpOptions)
      .pipe(
        map(response => response || {}),
        catchError(err => this.handleError(err)));
  }

  toQueryString(obj: any): string {

    const parts = [];
    for (const key in obj) {
      if (obj.hasOwnProperty(key)) {
        const value = obj[key];
        if (value !== null && value !== undefined && value !== 0 && value !== "") {
          parts.push(encodeURIComponent(key) + '=' + encodeURIComponent(value));
        }
      }
    }
    return parts.join('&');
  }


  public handleError(error: HttpErrorResponse): Observable<any> {
     debugger
    if (error) {
      switch (error.status) {
        case 401: {
          this.router.navigate(['/401']);
          break;
        }
        case 403: {
          this.notifyService.warning("", "مجوز دسترسی این درخواست برای شما صادر نشده است!");
          break;
        }
        case 500: {
          if (error.error.Message) {
            this.notifyService.error("", error.error.Message);
          }
          else {
            this.notifyService.error("", "خطایی در برنامه رخ داده است.");
          }
          break;
        }
        default:
          this.notifyService.error("", "خطایی در برنامه رخ داده است.");
          break;
      }
    }
    return observableThrowError(error);
  }
  getDynamicPermissions(roleIds: number[]): Promise<any> {
    return this.post(`${this._env.CenterApi}/api/DynamicRoleClaimsManager/RawDynamicPermissions`, roleIds).toPromise();
  }
}
