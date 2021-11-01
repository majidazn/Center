import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ValidationService } from './../../services/validation.service';


declare var jquery: any;
declare var $: any;

var text = $('.label-danger').text();
var emptytext = "";


@Component({
    selector: 'app-control-messages',
    template: `<div *ngIf="checkIfControlHasError() == true" [ngClass]="{'label-danger': checkIfControlHasError() }">{{getErrorMessage()}}</div>`,
    styleUrls: ['./control-messages.component.css']
})
/** ControlMessages component*/
export class ControlMessagesComponent {
    /** ControlMessages ctor */
  errorMessage: string;
  // @Input() control: FormControl;


  control: FormControl
  @Input()
  set _control(_control: FormControl) {
    //console.log('prev value: ', this._name);
    //console.log('got name: ', name);
    //this._name = name;
    this.control = _control;

    // console.log(this.errorMessage);
  }

  get _control(): FormControl {
    // transform value for display

    console.log(this.errorMessage);
    return this.control;
  }

  constructor() {

  }
 
  public checkIfControlHasError() {
    return (!this.control.valid) && this.control.touched;
  }

  getErrorMessage() {
     
    for (let propertyName in this.control.errors) {
      if (this.control.errors.hasOwnProperty(propertyName) && this.control.touched) {
        return ValidationService.getValidatorErrorMessage(propertyName, this.control.errors[propertyName]);
      }
    }

    return null;
  }
}
