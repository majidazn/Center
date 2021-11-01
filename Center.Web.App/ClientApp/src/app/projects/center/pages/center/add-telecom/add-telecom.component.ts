import { Component, Inject, HostListener, OnInit } from '@angular/core';
import { AddCenterComponent } from '../add-center/add-center.component';
import { WindowService, DialogService } from '@progress/kendo-angular-dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ValidationService } from '../../../../../services/validation.service';
import { CenterService } from '../center.service';
import { TelecomModel } from '../../../moldes/center/telecom.model';
import { TelTypeEnum } from '../../../moldes/tel-kind.enum';

@Component({
  selector: 'app-add-telecom',
  templateUrl: './add-telecom.component.html',
  styleUrls: ['./add-telecom.component.scss']
})
export class AddTelecomComponent implements OnInit {

  selectedTelKind!: {
    text: string;
    value: number;
  };
  public mobilePattern = '^[0][9][1][0-9]{8,8}$';  ///^(0[0-9]{9})$
  public tellPattern = '^[1-9]{8,8}$';
  parentInstance!: AddCenterComponent;
  showPrefix: boolean = true;
  showInternel: boolean = true;
  isMobile: boolean;

  telKindlistItems: { text: string; value: number; }[] = [];
  telecomForm: FormGroup;
  public list: Array<any> = [];
  /** AddTelecom ctor */

  public telKindlistItemssource: Array<{ text: string, value: number }> = [
    { text: "ثابت", value: TelTypeEnum['ثابت'] },

    { text: "همراه", value: TelTypeEnum['همراه'] },

  ];
  public addCenterInstance: AddCenterComponent;

  public IsEditMode: boolean;

  constructor(public dialog: MatDialogRef<AddTelecomComponent>,
    private windowService: WindowService,
    private fb: FormBuilder, private dialogService: DialogService, @Inject(MAT_DIALOG_DATA) public dataItem: any,
    private _centerService: CenterService) {
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
    this.getTelTypeListItem()
  }

  @HostListener('window:keyup.esc') onKeyUp() {
    this.dialog.close();
  }

  createForm() {
    this.telecomForm = this.fb.group({
      type: [null, [Validators.required]],
      tellNo: [''],
      mobileNo: [''],
      areaCode: [''],
      section: [''],
      comment: [''],
    });
  }

  getTelTypeListItem() {
    Object.keys(TelTypeEnum).filter(key => {
      let item = { text: '', value: null };
      if (!isNaN(+key)) {
        item = { text: TelTypeEnum[key], value: +key }
        this.telKindlistItems.push(item);
      }
    });
  }

  comboTypeChange(value: any) {
    debugger
    if (value == TelTypeEnum['ثابت']) {
      this.setTellNoValidation()

    }
    else if (value == TelTypeEnum['همراه']) {
      this.telecomForm.value.section = null;
      this.telecomForm.value.areaCode = null;
      this.setMobileValidations()
    }
    else {
      this.telecomForm.get('type').setValue(value);
      this.telecomForm.get('type').markAsUntouched();
    }
  }

  saveTelecom() {
    if (this.telecomForm.valid) {
      let telecomModel: TelecomModel = {
        type: this.telecomForm.value.type,
        section: !this.telecomForm.value.section || this.telecomForm.value.section=='' ?0: this.telecomForm.value.section,
        tellNo: this.isMobile ? this.telecomForm.value.mobileNo : `${this.telecomForm.value.areaCode}-${this.telecomForm.value.tellNo}`,
        comment: this.telecomForm.value.comment
      }
      if (this.IsEditMode != true) {
        this.parentInstance.setTelecomData(telecomModel);
      }
      else {
        this.parentInstance.EditTelecomData(telecomModel);
      }
    }
  }

  setTellNoValidation() {
    const tellNoControl = this.telecomForm.get("tellNo");
    const mobileNoControl = this.telecomForm.get("mobileNo");
    const areaCodeControl = this.telecomForm.get("areaCode");
    const sectionControl = this.telecomForm.get("section");
    this.isMobile = false;
    tellNoControl.setValidators([Validators.required, Validators.pattern(/^-?([0-9]\d*)?$/), Validators.maxLength(8)]);
    tellNoControl.updateValueAndValidity();
    areaCodeControl.setValidators([Validators.required, Validators.pattern(/^-?([0-9]\d*)?$/), Validators.maxLength(4), Validators.minLength(3)]);
    areaCodeControl.updateValueAndValidity();
    sectionControl.setValidators([Validators.pattern(/^-?([0-9]\d*)?$/), Validators.maxLength(5)]);
    sectionControl.updateValueAndValidity();
    mobileNoControl.setValidators(null);
    mobileNoControl.updateValueAndValidity();

  }

  setMobileValidations() {
    const tellNoControl = this.telecomForm.get("tellNo");
    const mobileNoControl = this.telecomForm.get("mobileNo");
    const areaCodeControl = this.telecomForm.get("areaCode");
    const sectionControl = this.telecomForm.get("section");
    this.isMobile = true;
    mobileNoControl.setValidators([Validators.required, Validators.pattern(/^-?([0-9]\d*)?$/), Validators.maxLength(11), Validators.minLength(11)]);
    mobileNoControl.updateValueAndValidity();
    tellNoControl.setValidators(null);
    tellNoControl.setValue('');
    tellNoControl.updateValueAndValidity();
    areaCodeControl.setValidators(null);
    areaCodeControl.setValue('');
    areaCodeControl.updateValueAndValidity();
    sectionControl.setValidators(null);
    sectionControl.setValue('');
    sectionControl.updateValueAndValidity();
  }

  loadData() {
    if (this.dataItem.editMode && this.dataItem.editingRecord != null) {
      this.telecomForm.get('type').setValue(this.dataItem.editingRecord.type ? this.dataItem.editingRecord.type : null);
      this.telecomForm.get('comment').setValue(this.dataItem.editingRecord.comment ? this.dataItem.editingRecord.comment : '');
      this.IsEditMode = true;
      if (this.dataItem.editingRecord.type == TelTypeEnum['ثابت']) {
        this.setTellNoValidation();
        var tellNo=this.dataItem.editingRecord.tellNo.split('-')
        this.telecomForm.get('section').setValue(this.dataItem.editingRecord.section ? this.dataItem.editingRecord.section : '');
        this.telecomForm.get('areaCode').setValue(tellNo ? tellNo[0] : '');
        this.telecomForm.get('tellNo').setValue(tellNo ? tellNo[1] : '');
      }
      else if (this.dataItem.editingRecord.type == TelTypeEnum['همراه']) {
        this.setMobileValidations();
        this.telecomForm.get('mobileNo').setValue(this.dataItem.editingRecord.tellNo ? this.dataItem.editingRecord.tellNo : '');

      }
    }
  }

}
