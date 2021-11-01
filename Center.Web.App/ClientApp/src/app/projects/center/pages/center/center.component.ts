import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { WindowService, DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { AddCenterComponent } from './add-center/add-center.component';
import { SelectCityComponent } from './select-city/select-city.component';
import { DeleteCenterConfirmationDialogComponent } from './delete-center-confirmation-dialog/delete-center-confirmation-dialog.component';
import { CenterLayoutComponent } from '../../center-layout/center-layout.component';
import { CenterService } from './center.service';
import { GeneralVariablesParentIdEnum } from '../../moldes/general-variables.enum';
import { SharedService } from '../../../../services/shared.service';
import { Permissions } from '../../../../models/permissions.enum'
export interface FilterModel {
  title: number;
  centerGroup: number;
  name: string;
  city: number;
  gridState: State;
}

@Component({
  selector: 'app-center',
  templateUrl: './center.component.html',
  styleUrls: ['./center.component.css']
})
/** CenterIndex component*/
export class CenterComponent implements OnInit {
  public IsBirthCity = false;
  centerTitleData: any;
  centerGroupData: any;
  public IsLivingCity = false;
  selectedBirthCity: any;
  selectedCity: any;
  city: any;
  AddcenterDialog: MatDialogRef<AddCenterComponent>;
  dialog: MatDialogRef<AddCenterComponent>;
  dialogRef: MatDialogRef<SelectCityComponent>;
  cityDialogRef: MatDialogRef<SelectCityComponent>;
  isLoading: boolean;
  DeletecenterDialog: MatDialogRef<DeleteCenterConfirmationDialogComponent, any>;

  showSpinner = false;
  citiesList: any;
  citylistItems: any;
  centerGrouplistItems: any = [];
  centerTitlelistItems: any = [];
  public centerGroup: number;
  title: any;

  searchForm: FormGroup;
  gridData: any;
  public centerList: Observable<GridDataResult>;
  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  citiesListItemsFilter: any;
  fullAccessRoles = [];
  public Permissions = Permissions;
  constructor(
    private _centerService: CenterService, private dialogService: DialogService, private fb: FormBuilder, private router: Router,
    public MatDialog: MatDialog, private _sharedService: SharedService, public centerLayout: CenterLayoutComponent, private spinner: NgxSpinnerService,
  ) {
    this.fullAccessRoles = this._sharedService.fullAccessRoles;
  }

  ngOnInit(): void {
    this.fillCitiesDropDown();
    this.createSearchForm();
    this.fillCenterGroupDropdown();
    this.fillCenterTitleDropdown();
    this.loadSearchHistory();

  }

  loadSearchHistory() {
    const searchItems = JSON.parse(localStorage.getItem("searchItems"));

    if (localStorage.getItem("searchItems") != "{}" && searchItems != null) {
      this.searchForm.get('city').setValue(searchItems.city);
      this.searchForm.get('title').setValue(searchItems.title);
      this.searchForm.get('centerGroup').setValue(searchItems.centerGroup);
      this.searchForm.get('centerName').setValue(searchItems.name);
      this.centerGroup = searchItems.centerGroup;
      this.title = searchItems.title;
      
    }
  }

  createSearchForm() {
    this.searchForm = this.fb.group({
      centerName: [null],
      centerGroup: [null],
      city: [null],
      title: [''],
    });
  }

  fillCitiesDropDown() {
    return this._centerService.getAllCities().subscribe(
      res => {
        this.citylistItems = res[0].selectListDtos;
        this.citiesListItemsFilter = res[0].selectListDtos;
        this.getCenterList();
      }
    );
  }

  cityList_handleFilter(value: string) {
    this.citylistItems = this.citiesListItemsFilter.filter((s: any) => s.text.indexOf(value) !== -1);
  }

  fillCenterGroupDropdown() {
    return this._centerService.getCenterVariable(GeneralVariablesParentIdEnum.CenterGroup).subscribe(
      res => {
        this.centerGrouplistItems = res;
        this.centerGroupData = res;

      }
    );
  }

  handleFilterCenterGroup(value: string) {
    if (value !== "")
      this.centerGroupData = this.centerGrouplistItems.filter((s: any) => s.name.indexOf(value) !== -1);
    else
      this.centerGroupData = this.centerGrouplistItems;
  }

  fillCenterTitleDropdown() {
    return this._centerService.getCenterVariable(GeneralVariablesParentIdEnum.CenterTitle).subscribe(
      res => {
        this.centerTitlelistItems = res;
        this.centerTitleData = res;
      }
    );
  }

  handleFilterTitle(value: string) {

    if (value !== "")
      this.centerTitleData = this.centerTitlelistItems.filter((s: any) => s.name.indexOf(value) !== -1);
    else
      this.centerTitleData = this.centerTitlelistItems;
  }

  public dropDownValueChangecenterGroup(value: any): void {
    //if (value != undefined)
    this.centerGroup = value;
  }

  public dropDownValueChangecenterTitle(value: any): void {
    //if (value != undefined)
    this.title = value;
  }

  public dataStateChange(state: State) {
    this.gridState = state;
    this.getCenterList();
  }

  searchCenter() {
    this.getCenterList();
  }

  getCenterList(flag?: boolean) {
    this.isLoading = true;

    const centerModel: FilterModel = {
      centerGroup: this.centerGroup != undefined ? this.centerGroup :0,
      city: this.searchForm.value.city != undefined ? this.searchForm.value.city : 0,
      name: this.searchForm.value.centerName != undefined ? this.searchForm.value.centerName : "",
      title: this.title != undefined ? this.title:0,
      gridState: this.gridState
    }

    if (flag == true) {
      centerModel.name = "";
      centerModel.city = 0;
      centerModel.centerGroup = 0;
      centerModel.title = 0;
    }

    localStorage.setItem("searchItems", JSON.stringify(centerModel));

    return this._centerService.getCenterInformation(centerModel).subscribe(
      res => {
        this.centerList = res;
        this.showSpinner = false;
        this.isLoading = false;

      }
    );
  }

  closeAddCenterDialog() {
    if (this.AddcenterDialog)
      this.AddcenterDialog.close()
  }

  public onAddNewClicked() {
    var wWidth = $(window).width();
    var dWidth = wWidth * 0.9;
    this.AddcenterDialog = this.MatDialog.open(AddCenterComponent, {
      width: '75%',
      data: { title: 'افزودن مرکز', citylistItems: this.citylistItems, centerGrouplistItems: this.centerGroupData, centerTitlelistItems: this.centerTitleData },
      disableClose: true,
    });
    this.AddcenterDialog.componentInstance.parentInstance = this;
    this.centerLayout.addValidatorClass();
  }

  public closeDialog() {

    if (this.dialog) {
      this.dialog.close();
    }

  }

  public onEditCenter(centerId: any) {
    this.spinner.show();
    var editData;
    this._centerService.getCenterById(centerId).subscribe(
      res => {
        if (res) {
          editData = res;
          this.dialog = this.MatDialog.open(AddCenterComponent, {
            width: '75%',
            data: { title: 'ویرایش مرکز', centerId: centerId, editData: editData, citylistItems: this.citylistItems, centerGrouplistItems: this.centerGroupData, centerTitlelistItems: this.centerTitleData },
            disableClose: true,
          });
          this.dialog.componentInstance.parentInstance = this;

          this.dialog.afterClosed().subscribe(result => {

            this.getCenterList();
          });

        }
        this.spinner.hide();
      },
      error => {
        this.spinner.hide();

      }
    )

  }

  public redirectToAssignActivityToCenter(centerId: number) {
    this.router.navigate(['/activity/', { CenterId: centerId }]);
  }

  ClearFields() {
    this.searchForm = this.fb.group({
      centerName: [''],
      centerGroup: [''],
      city: [null],
      title: 0,
    });

    this.getCenterList(true);
  }

  public selectCityByState() {

    this.cityList_handleFilter("");

    this.cityDialogRef = this.MatDialog.open(SelectCityComponent, {
      width: '60%',
      data: { title: 'انتخاب شهر', cityId: this.searchForm.value.city },
      disableClose: true,
    });
    this.cityDialogRef.componentInstance.parentInstance = this;
  }

  public selectCityDropDown(value: any) {
    this.selectedCity = value;
  }

  public dropDownValueChangeCity(value: any): void {
    if (value != undefined) {
      this.city = value.CityId;
      this.searchForm.get('city').setValue(value);
    }
  }

  onDeleteCenter(centerId: any) {
    this.DeletecenterDialog = this.MatDialog.open(DeleteCenterConfirmationDialogComponent, {
      data: { title: 'حذف', centerId },
      disableClose: true,
    });
    this.DeletecenterDialog.componentInstance.parentInstance = this;
  }

}
