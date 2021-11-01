import { Injectable } from '@angular/core';
import { GridSettings } from '../models/grid-settings.interface';
import { MatDatepickerInputEvent } from "@angular/material";
import * as moment from "jalali-moment";
import { FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { DomSanitizer } from '@angular/platform-browser';
import { FullAccessRoles } from '../models/fullAccess-roles.enum';
declare var jquery: any;
declare var $: any;

@Injectable()
export class SharedService {
  token: any;
  public fullAccessRoles = [];

  constructor(private sanitizer: DomSanitizer) {
    Object.keys(FullAccessRoles).filter((key) => {
      this.fullAccessRoles.push(FullAccessRoles[key]);
    });
    this.token = this.getCookie('token')
  }

  public getGridSettings<T>(token: string): T {
    const settings = localStorage.getItem(token);
    return settings ? JSON.parse(settings) : settings;
  }

  public setGridSettings<T>(token: string, gridConfig: any): void {
    localStorage.setItem(token, JSON.stringify(gridConfig));
  }



  public sexes: any[] = [
    { value: 1, text: 'مرد' },
    { value: 2, text: 'زن' }
  ];

  //public dropdownlistValueAgeUnit: any;
  public ageUnits: any[] = [
    { value: 1, text: 'روز' },
    { value: 2, text: 'ماه' },
    { value: 3, text: 'سال' }
  ];


  workListKind: { Id: number, Name: string }[] = [
    { "Id": 0, "Name": "جواب یکتا" },
    { "Id": 1, "Name": "دارای مجموعه جواب" },
  ];


  public isNullOrEmtpy(model: any): boolean {
    return model == null || model == ''
  }
  public getPersianDate(value: any, format: string = 'YYYY/MM/DD , h:mm:ss a') {

    if (this.isNullOrEmtpy(value) || value == "0001-01-01T00:00:00")
      return ``;
    else {
      const date = moment(value, 'YYYY/MM/DD , h:mm:ss a').locale('fa').format(format);
      return date;
    }
  }
  getPersianDateWithoutTime(value: any) {
    if (value == null) return "";
    const date = moment(value, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD');
    return date;
  }

  public getShortPersianDate(value: any, format: string = 'YYYY/MM/DD , HH:mm') {

    if (this.isNullOrEmtpy(value) || value == "0001-01-01T00:00:00")
      return ``;
    else {
      const date = moment(value, 'YYYY/MM/DD , HH:mm').locale('fa').format(format);
      return date;
    }
  }
  getForceIcon(value: any) {

    switch (value) {
      case 2: {
        return "orange";
      }
      case 100: {
        return "blue";
      }
      case 101: {
        return "yellow";
      }
      case 102: {
        return "red";
      }
      case 103: {
        return "green";
      }

    }

  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
  getCurrentUser() {
    const helper = new JwtHelperService();
    this.token = this.getCookie("token");
    const decodedToken = helper.decodeToken(this.token);
    return decodedToken;

  }

  getUserTenantId() {
    const helper = new JwtHelperService();
    this.token = this.getCookie("token");
    const decodedToken = helper.decodeToken(this.token);
    return +decodedToken.TenantId;

  }
  IsUserInRoleId(roleId: number) {

    var currentUser = this.getCurrentUser();

    if (typeof (currentUser.RoleId) == "number") {
      return currentUser.RoleId == roleId;
    }
    else {
      const hasRole = currentUser.RoleId.indexOf(roleId) >= 1;
      return hasRole;
    }
  }

  getCookie(name: any) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
  }

  getToken() {
    return this.getCookie("token");
  }

  clearToken() {
    document.cookie = 'token=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    localStorage.removeItem('token');
    sessionStorage.removeItem('token');
  }

  goToLoginPage() {
    var baseURL = this.getToken()
    window.location.href = baseURL;
  }

  getLandingAddress() {
    const helper = new JwtHelperService();
    this.token = this.getCookie("token");
    const decodedToken = helper.decodeToken(this.token);
    return decodedToken.LoginPageUrl;
  }

}
