import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { VariableSubGridComponent } from '../variable-sub-grid/variable-sub-grid.component';

@Component({
  selector: 'app-delete-variable-confirmation-dialog',
  templateUrl: './delete-variable-confirmation-dialog.component.html',
  styleUrls: ['./delete-variable-confirmation-dialog.component.scss']
})
/** DeleteCenterVariableConfirmationDialog component*/
export class DeleteVariableConfirmationDialogComponent {

  title: string;
  parentInstance!: VariableSubGridComponent;

  constructor(@Inject(MAT_DIALOG_DATA) public dataItem: any) {
    this.title = "حذف";
  }

  deleteRecord() {
    this.parentInstance.delete();
    this.closeDialog();
  }
  closeDialog() {
    this.parentInstance.deleteCenterVariableDialog.close();
  }
}
