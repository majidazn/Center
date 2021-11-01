import { Component, Input,Output, EventEmitter , OnInit } from '@angular/core';

@Component({
    selector: 'app-fire-function',
    templateUrl: './fire-function.component.html',
    styleUrls: ['./fire-function.component.scss']
})
/** FireFunction component*/
export class FireFunctionComponent implements OnInit {
  
    /** FireFunction ctor */
    constructor() {

    }

  @Input()
  public myCallback: Function;


  ngOnInit(): void {
    this.myCallback();
  }

}
