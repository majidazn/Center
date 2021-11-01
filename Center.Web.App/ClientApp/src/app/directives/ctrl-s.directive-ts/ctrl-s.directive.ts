import { Directive, OnInit, HostListener, OnDestroy, Output, EventEmitter, ElementRef } from '@angular/core';

@Directive({
  selector: '[ctrlS]'
})

export class CtrlSDetectorDirective {

  @Output() ctrlS: EventEmitter<boolean> = new EventEmitter();
  constructor() {
  }

  @HostListener('window:keydown', ['$event'])
  onKeyDown(event: KeyboardEvent): void {
    if (event.getModifierState && event.getModifierState('Control') && event.keyCode === 83) {
      event.preventDefault();
      this.ctrlS.emit(true);
    }
  }
}
