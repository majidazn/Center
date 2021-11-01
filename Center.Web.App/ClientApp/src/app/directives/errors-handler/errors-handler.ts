//import { ErrorHandler, Injectable} from '@angular/core';
//@Injectable()
//export class ErrorsHandler implements ErrorHandler {
//  handleError(error: Error) {
//     // Do whatever you like with the error (send it to the server?)
//     // And log it to the console
//     //console.error('It happens: ', error);
//    if (error.status == 403) {

//    }
//  }
//}


// errors-handler.ts
import { ErrorHandler, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NotifyService } from '../../services/notify.service';

@Injectable()
export class ErrorsHandler implements ErrorHandler {
  constructor(private notifyService: NotifyService,) {
  }


  handleError(error: Error | HttpErrorResponse) {

    if (error instanceof HttpErrorResponse) {
      // Server or connection error happened
      if (!navigator.onLine) {
        // Handle offline error
      } else {
        
        // Handle Http Error (error.status === 403, 404...)
        if (error.status == 403) {
           //this.notifyService.onWarning("", "مجوز دسترسی این درخواست برای شما صادر نشده است!");
          alert("مجوز دسترسی این درخواست برای شما صادر نشده است!");
        }
      }
    } else { 
      // Handle Client Error (Angular Error, ReferenceError...)     
    }
  
    // Log the error anyway
    console.error('It happens: ', error);
  }

}
