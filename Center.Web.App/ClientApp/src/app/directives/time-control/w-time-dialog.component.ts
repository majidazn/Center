import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';

import { CLOCK_TYPE } from './w-clock.component';



@Component({
  styleUrls: ['./w-time-dialog.component.scss'],
  templateUrl: './w-time-dialog.component.html'
})
export class WTimeDialogComponent {

  public userTime: any = {};
  public VIEW_HOURS = CLOCK_TYPE.HOURS;
  public VIEW_MINUTES = CLOCK_TYPE.MINUTES;
  public currentView: CLOCK_TYPE = this.VIEW_HOURS;



  constructor(
    @Inject(MAT_DIALOG_DATA) private userTimeData,
    private dialogRef: MatDialogRef<WTimeDialogComponent>,
    private cdRef: ChangeDetectorRef) {

    this.userTime = userTimeData;
  }


  public formatMinute(): string {

    if (this.userTime.minute < 10) {

      return '0' + String(this.userTime.minute);
    } else {

      return String(this.userTime.minute);
    }
  }

  public setCurrentView(type: CLOCK_TYPE) {

    this.currentView = type;
  }

  public setMeridien(m: string) {

    this.userTime.meriden = m;
  }

  public revert() {

    this.dialogRef.close(-1);
  }

  public submit() {

    this.dialogRef.close(this.userTime);
  }
}
