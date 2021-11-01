import { LocationStrategy, PathLocationStrategy } from "@angular/common";
import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Inject, NgZone } from "@angular/core";
import { NotifyService } from '../services/notify.service';

export class AppErrorHandler extends ErrorHandler {
  constructor(
    private notifyService: NotifyService,
    @Inject(NgZone) private ngZone: NgZone,
    @Inject(LocationStrategy) private locationProvider: LocationStrategy
  ) {
    super();

  }

  handleError(error: any): void {
    alert(" error handle!")

    console.log("Error:", error);
    
    const url = this.locationProvider instanceof PathLocationStrategy ? this.locationProvider.path() : "";
    const message = this.getError(error);
    this.ngZone.run(() => {

      //this.notifyService.onError(`خطا`, `${message}`);
      
    });

    super.handleError(error);
  }

  getError(error: any): string {
    alert(" error handle2!")

    const date = new Date().toISOString();
    
   // this.getStackTrace(error).then(stackTrace => {
      // TODO: log on the server --> { message, url, stackTrace }
  //    console.log("StackTrace", stackTrace);
  //  });

    if (error instanceof HttpErrorResponse) {
      //return `HTTP error occurred at ${date}, ${error.message}, ${(<HttpErrorResponse>error).status}, ${error.statusText},${this.gerErrorDetails(error.error)}`;
      return `${this.gerErrorDetails(error.error)}`;
    }

    if (error instanceof TypeError) {
      return `Type error occurred at ${date}, message - ${error.message}`;
      //return `${error.error}`;
    }

    if (error instanceof Error) {
      return `General error occurred at ${date}, message - ${error.message}`;
    }

    return `Some magical error occurred at ${date}, error - ${error}`;
  }

  gerErrorDetails(error: any): string {
    alert(" error handle3!")

    const errors: string[] = [];
    if (typeof error === "object" && error.constructor === Object) {
      for (const fieldName in error) {
        if (error.hasOwnProperty(fieldName)) {
          const modelStateError = error[fieldName];
          errors.push(`${fieldName}: ${modelStateError}`);
        }
      }
    } else {
      errors.push(error.toString());
    }
    return errors.join(", ");
  }

  // getStackTrace(error: any): Promise<string> {
  //  return StackTrace.fromError(error)
  //     .then(stackFrames => stackFrames.splice(0, 20).map(stackFrame => stackFrame.toString()).join("\n"));
  // }
}
