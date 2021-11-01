import { Component, HostListener, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { CenterService } from '../center.service';
import { CenterComponent } from '../center.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete-center-confirmation-dialog',
  templateUrl: './delete-center-confirmation-dialog.component.html',
  styleUrls: ['./delete-center-confirmation-dialog.component.scss']
})
export class DeleteCenterConfirmationDialogComponent {

  public parentInstance: CenterComponent;

  constructor(public dialog: MatDialogRef<DeleteCenterConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dataItem: any,
    private _centerService: CenterService,
    private notifyService: ToastrService) { }

  @HostListener('window:keyup.esc') onKeyUp() {
    this.dialog.close();
  }

  public closeDialog() {
    this.dialog.close();
  }

  deleteRecord() {
    
    let model = {
      centerId: this.dataItem.centerId
    }
    this._centerService.deleteCenter(model).subscribe(
      res => {
        if (res.isSuccess) {
          this.notifyService.success(``, res.message);
          this.parentInstance.getCenterList();
        }
        else {
          this.notifyService.error(``, res.message);
        }
      this.closeDialog();
      },
      error => {
        this.closeDialog();
      }
    )
  }
}
