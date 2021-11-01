import { Component,  AfterViewInit } from '@angular/core';

declare var jquery: any;
declare var $: any;

@Component({
    selector: 'app-inline-label',
    templateUrl: './inline-label.component.html',
    styleUrls: ['./inline-label.component.scss']
})
/** InlineLabel component*/
/**

this component stretch inline labels and its input size

*/
export class InlineLabelComponent implements AfterViewInit {
    
    /** InlineLabel ctor */
    constructor() {

  }


  ngAfterViewInit(): void {
    //alert('ngOnInit');
    var lblWidth = $('.label-inline').width();
    lblWidth += 3;
    var parentWidth = $('.label-inline').parent().width();
    var fieldWidth = parentWidth - lblWidth;
    $('.form-control-inline').css("width", fieldWidth);
    console.log(`**************************** ******************************************************`);
  }

}
