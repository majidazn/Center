import { Component , Input } from '@angular/core';
import { trigger, style, transition, animate }
  from '@angular/animations';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-custom-dialog',
  templateUrl: './custom-dialog.component.html',
  styleUrls: ['./custom-dialog.component.scss'],
  animations: [
    trigger('dialog', [
      transition('void => *', [
        style({ transform: 'scale3d(.3, .3, .3)' }),
        animate(100)
      ]),
      transition('* => void', [
        animate(100, style({ transform: 'scale3d(.0, .0, .0)' }))
      ])
    ])
  ]
})
/** CustomDialog component*/



export class CustomDialogComponent {

  @Input() title = `Information`;

  constructor(
    public activeModal: NgbActiveModal
  ) { } 

  ngOnInit() {
  }

}  

