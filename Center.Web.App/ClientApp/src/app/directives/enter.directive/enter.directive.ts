import { Directive, OnInit, HostListener, OnDestroy, Output, EventEmitter, ElementRef } from '@angular/core';

@Directive({
  selector: '[enter]'
})

export class EnterDetectorDirective {

  @Output() enter: EventEmitter<boolean> = new EventEmitter();
  constructor() {
  }

  @HostListener('window:keydown', ['$event'])
  onKeyDown(event: KeyboardEvent): void {
    if (event.getModifierState  && event.keyCode === 13) {
      event.preventDefault();
      this.enter.emit(true);
    }
  }
}
