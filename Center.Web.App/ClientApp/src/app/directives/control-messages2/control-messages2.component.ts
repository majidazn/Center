import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, FormGroupDirective } from '@angular/forms';
import { ValidationService } from './../../services/validation.service';


declare var jquery: any;
declare var $: any;



@Component({
  selector: 'app-control-messages2',
  templateUrl: './control-messages2.component.html',//`<div *ngIf="checkIfControlHasError() == true" [ngClass]="{'danger': checkIfControlHasError() }">{{getErrorMessage()}}</div>`,
    styleUrls: ['./control-messages2.component.css']
})
/** ControlMessages component*/
export class ControlMessages2Component {
    /** ControlMessages ctor */
  errorMessage: string;
  // @Input() control: FormControl;


  control: FormControl;
  name: string;
  @Input()
  set _control(_control: FormControl) {
    this.control = _control;
  }

  get _control(): FormControl {
    return this.control;
  }
  @Input()
  set _name(_name: string) {
    this.name = _name;
  }

  get _name(): string {
    return this.name;
  }

  constructor() {

  }
 
  public checkIfControlHasError() {
    return (!this.control.valid) && this.control.errors != null && (this.control.dirty || this.control.touched);  // this.control.touched;
  }

  getErrorMessage() {
    for (let propertyName in this.control.errors) {
      if (this.control.errors.hasOwnProperty(propertyName)) {//&& this.control.touched

        return ValidationService.getValidatorErrorMessage(propertyName, this.control.errors[propertyName]);
      }
    }

    return null;
  }
}
