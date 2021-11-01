import { Component, Input, AfterViewInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { AddVariablesComponent } from '../add-variables/add-variables.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { DeleteVariableConfirmationDialogComponent } from '../delete-variable-confirmation-dialog/delete-variable-confirmation-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material';
import { NotifyService } from '../../../../../services/notify.service';
import { VariablesService } from '../variables.service';

@Component({
  selector: 'app-variable-sub-grid',
  templateUrl: './variable-sub-grid.component.html',
  styleUrls: ['./variable-sub-grid.component.scss']
})

export class VariableSubGridComponent implements AfterViewInit {
  viewSubLevel1Loading: boolean = true;
  selectedCvariableForDelete: any;
  deleteCenterVariableDialog: MatDialogRef<DeleteVariableConfirmationDialogComponent, any>;
  ngAfterViewInit(): void {
    this.getData();
  }
  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  @Input() public centerVariableId: number;
  @Input() public centerVariableName: string;
  @Input() public parentInstance: AddVariablesComponent;
  viewSubLevel1: any;

  constructor(private _variablesService: VariablesService,
    private spinner: NgxSpinnerService,
    public MatDialog: MatDialog,
    private notifyService: NotifyService,) {
  }

  getData() {
     
    let model = {
      centerVariableId:0 ,
      name: "",
      enName: "",
      parentId: this.centerVariableId,
      gridState: this.gridState
    }
    this.viewSubLevel1Loading = true;
    this._variablesService.GetCenterVariables(model).subscribe(
      res => {
        this.viewSubLevel1 = res;
        this.viewSubLevel1Loading = false;

      });
  }
  public dataStateChange(state: State) {
    this.gridState = state;
    this.getData();
  }
  OnAddNewCenterVariableClicked() {
     
    this.parentInstance.onAddNewCenterVariable(this.centerVariableId, this.centerVariableName);
  }

  onDeleteCenterVariable(CVariable: any) {
    this.selectedCvariableForDelete = CVariable.centerVariableId;

    this.deleteCenterVariableDialog = this.MatDialog.open(DeleteVariableConfirmationDialogComponent, {
      data: { title: 'حذف', componentName: 'GvariableSubLevel1Component' },
      disableClose: true,
    });

    this.deleteCenterVariableDialog.componentInstance.parentInstance = this;

  }

  delete() {
    this.spinner.show();
    let model = {
      centerVariableId: this.selectedCvariableForDelete
    }
    return this._variablesService.deleteCenterVariable(model).subscribe(
   
      res => {
        if (res.isSuccess) {
          this.notifyService.onSuccess(``, res.message);
          this.getData();
        }
        else
          this.notifyService.onError(``, res.message);

        this.spinner.hide();
      },
      error => {
        this.spinner.hide();
      }
    );
  }

  onEditCenterVariable(item: any) {

    this.parentInstance.onEditCenterVariable(item);
  }



}
