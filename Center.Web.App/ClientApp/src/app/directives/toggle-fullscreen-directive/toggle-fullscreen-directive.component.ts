import { Component, ElementRef, Input, EventEmitter, Output, OnDestroy } from '@angular/core';
import { Directive, HostListener, OnInit} from '@angular/core';
import * as screenfull from 'screenfull';
//import screenfull = require('screenfull');

@Directive({
    selector: '[toggleFullscreen]'
})
export class ToggleFullscreenDirective {

  isFullScreen: boolean;


  ss: any = screenfull;
  constructor(private el: ElementRef) {  }


  @Input('elementId') elementId: string;

  @HostListener('click') onClick() {
    
    const target = $('#' + this.elementId + '')[0];
    
    if (this.ss.enabled) {
      this.isFullScreen = !this.ss.isFullscreen;
      this.ss.toggle(target);
    }
    
  }
  @HostListener('window:keydown', ['$event']) onKeyDown(event: KeyboardEvent): void {
    if (event.getModifierState && event.getModifierState('Control') && event.keyCode === 122) {
      const target = $('#' + this.elementId + '')[0];

      if (this.ss.enabled) {
        this.ss.toggle(target);
      }
    }
  }
}


