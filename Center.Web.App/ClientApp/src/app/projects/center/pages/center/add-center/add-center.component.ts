import { Component, OnInit, Inject, HostListener } from '@angular/core';
import { WindowService, DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { FormGroup, Validators, FormBuilder, ValidationErrors } from '@angular/forms';
import { SelectCityComponent } from '../select-city/select-city.component';
import { State } from '@progress/kendo-data-query';
import { AddTelecomComponent } from '../add-telecom/add-telecom.component';
import { AddEaddressComponent } from '../add-eaddress/add-eaddress.component';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { CenterComponent } from '../center.component';
import { CenterService } from '../center.service';
import { ElectronicTypeEnum } from '../../../moldes/electronic-type.enum';
import { TelecomModel } from '../../../moldes/center/telecom.model';
import { EAddressModel } from '../../../moldes/center/eAddress.model';
import { CenterModel } from '../../../moldes/center/center.model';
import { HttpClient } from '@angular/common/http';
import { TelTypeEnum } from '../../../moldes/tel-kind.enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-center',
  templateUrl: './add-center.component.html',
  styleUrls: ['./add-center.component.css']
})

export class AddCenterComponent implements OnInit {
  windowTelecomforEdit: DialogRef;
  telecomEditMode: boolean;
  public selectedTelecomForEdit: TelecomModel = new TelecomModel();
  public selectedEaddressForEdit: EAddressModel = new EAddressModel();
  public windowTelecom: MatDialogRef<AddTelecomComponent>;
  public windowEaddress: MatDialogRef<AddEaddressComponent>;
  dialogRef: MatDialogRef<SelectCityComponent>;
  logoImageBase64: string | ArrayBuffer | null;
  logoImageBase64WithDataImage: string | ArrayBuffer | null;
  editingTelecomIndex: any;
  title: any;
  public IsLivingCity: boolean = false;
  public IsBirthCity: boolean = false;
  public livingCity: number;
  public birthCity: number;
  public centerGroup: number;
  selectedBirthCity: any;
  selectedCity: any;
  selectedTitle: any;
  selectedCenterGroup: any;
  data: any;
  citylistItems: any;
  model: any = {};
  angForm: FormGroup;
  centerGrouplistItems: any = [];
  centerTitlelistItems: any = [];
  public idToUpdate: number;
  public centerId: number;
  public telecomlist: TelecomModel[] = [];
  public eAddressList: EAddressModel[] = [];
  public Etypes: any;
  public sendableFormData: any;
  public progress: any;
  public httpEvent: any;
  public lastInvalids: any;
  public numberTextBoxFormat: string = '#';
  public parentInstance: CenterComponent;
  routineServiceGroupForm: FormGroup;
  maxSize = 200000;
  accept = ".png";
  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  public EAddressgridState: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  logoImage: File;
  files: File[] = [];
  file: any;
  editingEaddressIndex: number;
  citiesListItemsFilter: any;
  selectedFile: File;
  formData: FormData;
  lastFileAt: Date;

  @HostListener('window:keyup.esc') onKeyUp() {
    this.dialog.close();
    this.MatDialog.closeAll();
  }

  constructor(
    public dialog: MatDialogRef<AddCenterComponent>,
    private windowService: WindowService,
    private fb: FormBuilder,
    private dialogService: DialogService,
    private _centerService: CenterService,
    public MatDialog: MatDialog,
    private notifyService: ToastrService,
    @Inject(MAT_DIALOG_DATA) public dataItem: any,
    private spinner: NgxSpinnerService,
    private http: HttpClient) { }

  ngOnInit(): void {
    this.createForm();
    this.LoadEditModeData();
    this.citiesListItemsFilter = this.dataItem.citylistItems;
  }

  dataStateChange(state: State) {
    this.gridState = state;
  }

  eAddressdataStateChange(state: State) {
    this.EAddressgridState = state;
  }

  closeDialog() {
    this.dialog.close();
  }

  createForm() {
    this.angForm = this.fb.group({
      name: ['', [Validators.required]],
      enName: ['', [Validators.required]],
      centerGroup: [null, Validators.required],
      title: [null, Validators.required],
      address: ['', Validators.required],
      zipCode: [null, [Validators.required, Validators.pattern(/^-?([0-9]\d*)?$/), Validators.maxLength(10), Validators.minLength(10)]],
      nationalCode: [null, [Validators.required]],
      financialCode: [null, [Validators.required, Validators.max(9999999999999)]],
      centerCode: [null, [Validators.required, Validators.max(9999999999999)]],
      city: [null, Validators.required],
      centerId: [0],
      hostName: ['', Validators.required]
    });
  }

  LoadEditModeData() {
    if (this.dataItem.centerId !== undefined) { // Edit Mode
      this.angForm.get('name').setValue(this.dataItem.editData.name);
      this.angForm.get('enName').setValue(this.dataItem.editData.enName);
      this.angForm.get('address').setValue(this.dataItem.editData.address);
      this.angForm.get('zipCode').setValue(parseFloat(this.dataItem.editData.zipCode));
      this.angForm.get('centerCode').setValue(parseFloat(this.dataItem.editData.sepasCode));
      this.angForm.get('financialCode').setValue(this.dataItem.editData.financhialCode);
      this.angForm.get('nationalCode').setValue(this.dataItem.editData.nationalCode);
      this.angForm.get('centerId').setValue(this.dataItem.editData.centerId);
      this.angForm.get('hostName').setValue(this.dataItem.editData.hostName);
      this.logoImage = this.dataItem.editData.logo
      this.logoImageBase64 = this.dataItem.editData.logo;
      this.logoImageBase64WithDataImage = 'data:image/png;base64,' + this.dataItem.editData.logo;
      this.centerId = this.dataItem.editData.centerId;
      this.selectedTitle = this.dataItem.centerTitlelistItems.filter(q => q.centerVariableId == this.dataItem.editData.title)[0] || null;
      this.selectedCenterGroup = this.dataItem.centerGrouplistItems.filter(q => q.centerVariableId == this.dataItem.editData.centerGroup)[0] || null;
      this.angForm.value.centerGroup = this.selectedCenterGroup;
      this.angForm.value.city = this.dataItem.editData.city;
      this.angForm.value.title = this.selectedTitle;
      this.angForm.get('centerGroup').setValue(this.selectedCenterGroup);
      this.angForm.get('city').setValue(this.dataItem.editData.city);
      this.angForm.get('title').setValue(this.selectedTitle);
      this.eAddressList = this.setEAddresses(this.dataItem.editData.electronicAddresses);
      this.telecomlist = this.setTelecoms(this.dataItem.editData.telecoms);
    }
  }

  setEAddresses(eAddresses) {
    let eAddressList = [];
    eAddresses.forEach(eAddress => {
      eAddressList.push({ eType: eAddress.eType, eTypeStr: ElectronicTypeEnum[eAddress.eType], eAddress: eAddress.eAddress })
    })
    return eAddressList;
  }

  setTelecoms(telecoms) {
    let telecomList = [];
    telecoms.forEach(telecom => {
      telecomList.push({ type: telecom.type, typestr: TelTypeEnum[telecom.type], tellNo: telecom.tellNo, section: telecom.section, comment: telecom.comment })
    })
    return telecomList;
  }

  convertBinaryImageToFile(imageData) {
    var u8 = new Uint8Array(imageData.length);
    for (var i = 0; i < imageData.length; i++) {
      u8[i] = imageData[i].charCodeAt(0);
    }
    var imageBlob = new Blob([u8], { type: 'image/png' });
    return imageBlob;
  }

  handleFilter(value: any) {
    if (value !== "")
      this.dataItem.citylistItems = this.citiesListItemsFilter.filter((s) => s.text.indexOf(value) !== -1);
  }

  selectCityDropDown(value: any) {
    this.selectedCity = value;
  }

  selectCityByState() {
    this.dialogRef = this.MatDialog.open(SelectCityComponent, {
      width: '60%',
      data: { title: 'انتخاب شهر' },
      disableClose: true,
    })
    this.dialogRef.componentInstance.parentInstance = this;
  }

  dropDownValueChangeCity(value: any): void {
    if (value != undefined) {
      this.selectedCity = value;
      this.birthCity = value;
      this.angForm.get('city').setValue(value);
    }
  }

  dropDownValueChangecenterGroup(value: any): void {
    this.centerGroup = value.CentervariableId;
  }

  dropDownValueChangecenterTitle(value: any): void {
    this.title = value.CentervariableId;
  }

  /* Electronic Address*/
  setEaddressData(value: any) {
    this.eAddressList.push({
      eType: value.eType,
      eTypeStr: ElectronicTypeEnum[value.eType],
      eAddress: value.eAddress,
    });
    this.windowEaddress.close();
  }

  onEditEaddress(selectedRowIndex: number) {
    this.selectedEaddressForEdit = this.eAddressList[selectedRowIndex];
    this.editingEaddressIndex = selectedRowIndex;
    this.oneditEaddressClicked();
  }

  EditEaddressData(value: any) {
    this.eAddressList[this.editingEaddressIndex].eType = value.eType;
    this.eAddressList[this.editingEaddressIndex].eAddress = value.eAddress;
    this.eAddressList[this.editingEaddressIndex].eTypeStr = ElectronicTypeEnum[value.eType];
    this.windowEaddress.close();
  }

  onAddNewEaddressClicked() {
    this.windowEaddress = this.MatDialog.open(AddEaddressComponent, {
      width: '50%',
      data: { title: 'افزودن رسانه اجتماعی' },
      disableClose: true,
    })
    this.windowEaddress.componentInstance.parentInstance = this;
    this.selectedEaddressForEdit = null;
    this.editingEaddressIndex = null;
  }

  oneditEaddressClicked() {
    this.windowEaddress = this.MatDialog.open(AddEaddressComponent, {
      width: '50%',
      data: { title: 'ویرایش رسانه اجتماعی' },
      disableClose: true,
    })
    this.windowEaddress.componentInstance.parentInstance = this;
  }

  onDeleteEaddress(selectedRowIndex: any) {
    this.eAddressList.splice(selectedRowIndex, 1)
    this.selectedEaddressForEdit = null;
    this.editingEaddressIndex = null;
  }

  /*Telecom*/
  setTelecomData(value: any) {
    debugger 
    if (value.type == 2) {
      value.tellNo = value.tellNo;
      value.typestr = "همراه";
    }
    else if (value.type == 1) {
      value.typestr = "ثابت";
    }
    this.telecomlist.push(value);
    this.windowTelecom.close();
  }


  onEditTelecom(selectedRowIndex: number) {
    debugger
    this.editingTelecomIndex = selectedRowIndex;
    this.windowTelecom = this.MatDialog.open(AddTelecomComponent, {
      width: '50%',
      data: { title: 'ویرایش تلفن', editingRecord: this.telecomlist[selectedRowIndex],editMode:true },
      disableClose: true,
    })
    this.windowTelecom.componentInstance.parentInstance = this;

  }

  onAddNewTelecomClicked() {
    this.windowTelecom = this.MatDialog.open(AddTelecomComponent, {
      width: '50%',
      data: { title: 'افزودن تلفن' },
      disableClose: true,
    })
    this.windowTelecom.componentInstance.parentInstance = this;
    this.selectedTelecomForEdit = null;
    this.editingTelecomIndex = null;
  }

  onDeleteTelecom(selectedRowIndex: number) {
    this.telecomlist.splice(selectedRowIndex, 1)
    this.selectedTelecomForEdit = null;
    this.editingTelecomIndex = null;
  }

  EditTelecomData(value: any) {
    if (value.type == 2) {
      value.tellNo = value.tellNo;
      this.telecomlist[this.editingTelecomIndex].typestr = "همراه";
    }
    else if (value.type == 1) {
      this.telecomlist[this.editingTelecomIndex].typestr = "ثابت";
    }
    this.telecomlist[this.editingTelecomIndex].type = value.type;
    this.telecomlist[this.editingTelecomIndex].tellNo = value.tellNo;
    this.telecomlist[this.editingTelecomIndex].section = value.section;
    this.telecomlist[this.editingTelecomIndex].comment = value.comment;
    this.windowTelecom.close();
  }

  saveCenter() {
    this.spinner.show();
    this.getFormValidationErrors();
    if (this.telecomlist.length == 0) {
      this.notifyService.error(`خطا`, `لطفا حداقل یک شماره تماس وارد نمایید !`);
      this.spinner.hide();
      return false;
    }
    this.createCenterModel();
    const centerModel = this.createCenterModel();
    // Edit Mode
    if (this.angForm.value.centerId !== 0) {
      let editCenterModel = { editCenterDto: centerModel };
      this._centerService.updateCenter(editCenterModel).subscribe(
        res => {
          if (res.isSuccess) {
            this.notifyService.success(``, res.message);
            this.parentInstance.closeDialog();
            this.parentInstance.getCenterList();
          }
          else {
            this.notifyService.error(``, res.message);
          }
          this.spinner.hide();
        },
        error => {
          this.spinner.hide();
        })
    }
    // Create Mode
    else {
      this.angForm.value.centerGroup = this.centerGroup;
      this.angForm.value.city = this.birthCity;
      this.angForm.value.title = this.title;
      let createCenterModel = { createCenterDto: centerModel };
      this._centerService.createCenter(createCenterModel).subscribe(
        res => {
          if (res.isSuccess) {
            this.notifyService.success(``, res.message);
            this.parentInstance.closeAddCenterDialog();
            this.parentInstance.getCenterList();
          }
          else {
            this.notifyService.error(``, res.message);
          }
          this.spinner.hide();
        },
        error => {
          this.spinner.hide();
        }
      )
    }
  }

  createCenterModel(): CenterModel {
    return {
      address: this.angForm.value.address,
      sepasCode: this.angForm.value.centerCode,
      centerGroup: this.angForm.value.centerGroup.centerVariableId,
      centerId: this.angForm.value.centerId,
      city: this.angForm.value.city,
      financhialCode: this.angForm.value.financialCode,
      name: this.angForm.value.name,
      enName: this.angForm.value.enName,
      nationalCode: this.angForm.value.nationalCode,
      title: this.angForm.value.title.centerVariableId,
      zipCode: this.angForm.value.zipCode,
      electronicAddresses: this.eAddressList,
      telecoms: this.telecomlist,
      hostName: this.angForm.value.hostName,
      tenantId: 0,
      logo: this.logoImageBase64,
      validFrom: new Date()
    }
  }

  getFormValidationErrors() {
    Object.keys(this.angForm.controls).forEach(key => {
      const controlErrors: ValidationErrors = this.angForm.get(key).errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach(keyError => {
          console.log('Key control: ' + key + ', keyError: ' + keyError + ', err value: ', controlErrors[keyError]);
        });
      }
    });
  }

  imageLogoChanged($event): void {
    this.readThis($event);
  }

  readThis(inputValue: any): void {
    this.selectedFile = inputValue[0];
    const count = inputValue.length;
    var file: File = inputValue[count - 1];
    var myReader: FileReader = new FileReader();

    myReader.onloadend = (e) => {

      this.logoImageBase64WithDataImage = myReader.result;
      var strImage = (myReader.result as string).replace(/^data:image\/[a-z]+;base64,/, "");
      this.logoImageBase64 = strImage;
    }
    myReader.readAsDataURL(file);
  }

  removeLogoImage() {
    this.logoImageBase64 = null;
    this.logoImageBase64WithDataImage = null;
    this.logoImage = null;
    this.files = null;
    this.lastInvalids = null;
    this.sendableFormData = null;
  }

  dataURLtoFile(dataurl, filename) {
    var arr = dataurl.split(','),
      mime = arr[0].match(/:(.*?);/)[1],
      bstr = atob(arr[1]),
      n = bstr.length,
      u8arr = new Uint8Array(n);

    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
  }

}
