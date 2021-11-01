import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import { State } from '@progress/kendo-data-query';
import { VariablesService } from '../variables.service';
import { CreateCenterVariableModel } from '../../../moldes/center-variable/create-center-variable.model';
import { EditCenterVariableModel } from '../../../moldes/center-variable/edit-center-variable.model';
import { ToastrService } from 'ngx-toastr';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-add-variable',
  templateUrl: './add-variables.component.html',
  styleUrls: ['./add-variables.component.scss']
})

export class AddVariablesComponent implements OnInit {

  public view: Observable<GridDataResult>;
  public gridState: State = {
    skip: 0,
    take: 10
  };
  gVariablesView: any;
  gVariablesViewLoading: boolean;
  centerVariableForm: FormGroup;
  selectedParent: string;
  centerVariableParentListItem: any;
  selectedParentCenterVariable: number;


  constructor(private _variablesService: VariablesService,
    private fb: FormBuilder,
    private router: Router,
    private spinner: NgxSpinnerService,
    private notifyService: ToastrService) { }

  ngOnInit() {
    this.createCenterVariableForm();
    this.getVariablesList();
  }

  createCenterVariableForm() {
    this.centerVariableForm = this.fb.group({
      Name: ['', [Validators.required]],
      EName: ['', [Validators.required]],
      ParentId: [null, [Validators.required]],
      CentervariableId: [0]
    });
  }

  getVariablesList() {
    this.gVariablesViewLoading = true;
    this._variablesService.GetCenterVariablesByParentId(1).subscribe(
      res => {
        this.centerVariableParentListItem = res;
        this.gVariablesView = res;
        this.gVariablesViewLoading = false;
      });
  }

  dataStateChange(state: State) {
    this.gridState = state;
    this.getVariablesList();
  }

  onAddNewCenterVariable(centerVariableId: number, centerVariableName: string) {
    $([document.documentElement, document.body]).animate({
      scrollTop: $("body").offset().top
    }, 2000);
    this.selectedParent = centerVariableName;
    this.selectedParentCenterVariable = centerVariableId;
    this.centerVariableForm.get('CentervariableId').setValue(0);
    this.centerVariableForm.get('ParentId').setValue(centerVariableId);
    this.centerVariableForm.get('Name').setValue('');
    this.centerVariableForm.get('EName').setValue('');
  }

  saveCenterVariable() {
     
    if (this.centerVariableForm.value.CentervariableId == 0) {
      this.addCenterVariable();
    }
    else {
      this.editCenterVariable();
    }
  }

  addCenterVariable() {
    this.spinner.show();
    const createModel: CreateCenterVariableModel = {
      createCenterVariableDto: {
        enName: this.centerVariableForm.value.EName,
        name: this.centerVariableForm.value.Name,
        parentId: this.centerVariableForm.value.ParentId,
      }
    };

    this._variablesService.createCenterVariable(createModel).subscribe(
      res => {
        if (res.isSuccess) {
          this.notifyService.success(``, res.message);
          this.getVariablesList();
          this.clearForm();
        }
        else {
          this.notifyService.error(``, res.message);
        }
        this.spinner.hide();
      },
      error => {
        this.spinner.hide();
      }
    );
  }

  editCenterVariable() {
    this.spinner.show();
    const editModel: EditCenterVariableModel = {
      editCenterVariableDto: {
        enName: this.centerVariableForm.value.EName,
        name: this.centerVariableForm.value.Name,
        parentId: this.centerVariableForm.value.ParentId,
        id: this.centerVariableForm.value.CentervariableId,
      }
    };

    this._variablesService.updateCenterVariable(editModel).subscribe(
      res => {
        if (res.isSuccess) {
          this.notifyService.success(``, res.message);
          this.getVariablesList();
          this.clearForm();
        }
        else {
          this.notifyService.error(``, res.message);
        }
        this.spinner.hide();
      },
      error => {
        this.spinner.hide();
      });
  }

  clearForm() {
    this.centerVariableForm.reset()
  }

  onEditCenterVariable(item: any) {
    $([document.documentElement, document.body]).animate({
      scrollTop: $("body").offset().top
    }, 2000);
    this.centerVariableForm.get('ParentId').setValue(item.parentId);
    this.selectedParentCenterVariable = item.parentId;
    this.centerVariableForm.get('CentervariableId').setValue(item.centerVariableId);
    this.centerVariableForm.get('Name').setValue(item.name);
    this.centerVariableForm.get('EName').setValue(item.enName);



  }

  onchangeparentCenterVariable(value: any) {
    this.selectedParentCenterVariable = value;
  }

}
