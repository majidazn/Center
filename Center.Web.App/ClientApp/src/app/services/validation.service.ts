import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';
import * as moment from 'jalali-moment';
import * as jalaliMoment from "jalali-moment";
import { extend } from 'webdriver-js-extender';
import { MaterialPersianDateAdapter } from '../directives/material-persian-date-adapter/material.persian-date.adapter';
import { container } from '@angular/core/src/render3/instructions';

@Injectable()
export class ValidationService   {
  constructor() {
      
  }

  static timeValidator(control: any) {
    // RFC 2822 compliant regex

    if (control.value == undefined || control.value == null)
      return { 'invalidTime': true };


    if (control.value.match(/^(([0-1][0-9])|(2[0-3])):[0-5][0-9]$/)) {
      return null;
    } else {
      return { 'invalidTime': true };
    }
  }


  static CustomdateValidator(control: AbstractControl) {
    
    

      if(  control.value=="")
      return { 'invalidDate': true };

  }


  static dateValidator(control: AbstractControl) {
    // RFC 2822 compliant regex
    

    const m = moment.from(control.value, "en").utc(true).toDate()

    const isvalid = moment(m, "YYYY/MM/DD").isValid();



    //if (control.value == undefined || control.value == null)
    //  return { 'invalidTime': true };
    

    if (isvalid) {
      // if (control.value.match(/^\d{4}[./-]\d{2}[./-]\d{2}$/)) {
      return null;
    } else {
      
      return { 'invalidDate': true };
    }
  }


  static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
     
    let config = {
      'required': 'پاسخ به این فیلد اجباری است',
      'invalidCreditCard': ' مقدار وارد شده نامعتبر است',
      'invalidEmailAddress': 'مقدار وارد شده نامعتبر است',
      'invalidPassword': 'Invalid password. Password must be at least 6 characters long, and contain a number.',
      'minlength': ` کمترین کاراکتر مجاز ${validatorValue.requiredLength} می باشد `,
      'maxlength': `  بیشترین کاراکتر مجاز ${validatorValue.requiredLength} می باشد`,
      'invalidTime': 'مقدار وارد شده نامعتبر است',
      'invalidDate': 'مقدار وارد شده نامعتبر است',
      'endDateIsBeforeStartDate': 'تاریخ پایان باید بعد از تاریخ شروع باشد',
      'invalidNumber': 'مقدار وارد شده نامعتبر است',
      'pattern': 'مقدار وارد شده نامعتبر است',
      'notEqual': 'پاسخ به این فیلد اجباری است',
      'max':'مقدار وارد شده معتبر نمی باشد.',
      'min':'مقدار وارد شده معتبر نمی باشد.',
    };

    return config[validatorName];
  }

  static creditCardValidator(control: any) {
    // Visa, MasterCard, American Express, Diners Club, Discover, JCB
    if (control.value.match(/^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/)) {
      return null;
    } else {
      return { 'invalidCreditCard': true };
    }
  }

  static emailValidator(control: any) {
    // RFC 2822 compliant regex
    if (control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
      return null;
    } else {
      return { 'invalidEmailAddress': true };
    }
  }

  static passwordValidator(control: any) {
    // {6,100}           - Assert password is between 6 and 100 characters
    // (?=.*[0-9])       - Assert a string has at least one number
    if (control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{6,100}$/)) {
      return null;
    } else {
      return { 'invalidPassword': true };
    }
  }


  static EndDateValidator(AC: AbstractControl){

    
    let startDate = AC.get('StartDate').value; // to get value in input tag
    let endDate = AC.get('EndDate').value; // to get value in input tag
    var m = moment(endDate);
    var isEndDateBeforeStartDate = m.isBefore(startDate);
    
    if (isEndDateBeforeStartDate) {
      //return { 'endDateIsBeforeStartDate': true };
      AC.get('EndDate').setErrors({ endDateIsBeforeStartDate: true })
      return false;

    }
    else {
      AC.get('EndDate').setErrors(null)
      return false;
    }
  }
  static numberValidator(control: any) {
    debugger
    if (control.value.match(/^([0-9]*)$/)) {
      return null;
    } else {
      return { 'invalidNumber': true };
    }
  }
}
