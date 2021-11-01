import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { AddApplicationComponent } from '../add-application/add-application.component';

@Component({
  selector: 'app-delete-application-confirmation-dialog',
  templateUrl: './delete-application-confirmation-dialog.component.html',
  styleUrls: ['./delete-application-confirmation-dialog.component.scss']
})
export class DeleteApplicationConfirmationDialogComponent  {

  title: string;
  parentInstance!: AddApplicationComponent;

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
