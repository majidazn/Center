import { Component, AfterViewInit, Inject, HostListener, OnInit } from '@angular/core';
import { DialogRef, WindowService, DialogService } from '@progress/kendo-angular-dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AddCenterComponent } from '../add-center/add-center.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { CenterService } from '../center.service';
import { ElectronicTypeEnum } from '../../../moldes/electronic-type.enum';
import { EAddressModel } from '../../../moldes/center/eAddress.model';
@Component({
  selector: 'app-add-eaddress',
  templateUrl: './add-eaddress.component.html',
  styleUrls: ['./add-eaddress.component.scss']
})
/** AddEAddress component*/
export class AddEaddressComponent implements OnInit {

  Etypes=[];
  parentInstance!: AddCenterComponent ;
  public addCenterInstance!: AddCenterComponent;
  selectedEtype: any;
  public IsEditMode: boolean;
  eAddressForm: FormGroup;

  constructor(public dialog: MatDialogRef<AddEaddressComponent>, private windowService: WindowService,
    private fb: FormBuilder, private dialogService: DialogService, @Inject(MAT_DIALOG_DATA) public dataItem: any,
    private _centerService: CenterService) {
  }

  ngOnInit(): void {
    this.createForm();
    this.getElectronicTypeListItem();
    this.loadData();
  }

  @HostListener('window:keyup.esc') onKeyUp() {
    this.dialog.close();
  }

  createForm() {
    this.eAddressForm = this.fb.group({
      EType: [null, [Validators.required]],
      EAddress: ['', [Validators.required]],
    });
  } 

  getElectronicTypeListItem() {
     
    Object.keys(ElectronicTypeEnum).filter(key =>
    {
      let item = { text: '', value: null };
      if (!isNaN(+key)){
        item = { text: ElectronicTypeEnum[key], value: +key }
        this.Etypes.push(item);
      }
    });
  }
  comboETypeChange(value: any): void {
    this.eAddressForm.get('EType').setValue(value);
  }

  saveEaddress() {
     
    let eAddressModel: EAddressModel = {
      eType: this.eAddressForm.value.EType,
      eAddress: this.eAddressForm.value.EAddress,
    }
    if (this.IsEditMode != true) {
      this.parentInstance.setEaddressData(eAddressModel);
    }
    else {
      this.parentInstance.EditEaddressData(eAddressModel);
    }
  }

  loadData() {
    if (this.parentInstance.selectedEaddressForEdit != null) {
      var model = this.parentInstance.selectedEaddressForEdit;
      this.eAddressForm.get('EAddress').setValue(model.eAddress);
      this.eAddressForm.get('EType').setValue(model.eType);
      this.IsEditMode = true;
      this.selectedEtype = model.eType; 

    }
  }
}
