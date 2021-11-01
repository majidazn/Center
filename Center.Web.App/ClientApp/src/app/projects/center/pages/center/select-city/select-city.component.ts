import { Component, OnInit, AfterViewInit, Inject, HostListener } from '@angular/core';
import { WindowRef, DialogRef } from '@progress/kendo-angular-dialog';
import { AddCenterComponent } from '../add-center/add-center.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { CenterService } from '../center.service';
import { CenterComponent } from '../center.component';


@Component({
  selector: 'app-select-city',
  templateUrl: './select-city.component.html',
  styleUrls: ['./select-city.component.css']
})
/** SelectCity component*/
export class SelectCityComponent implements OnInit {
  tempSelectedCity: any;
  interval: any;
  dataFilter: any;
  stateListItem: any;
  stateListItemFilter: any;
  cityListItem: any;
  cityListItemFilter: any;
  cityListItemLoading = false;
  selectedCity: any;
  selectedState: any;
  citylistItems: any;
  public dialogOpened = false;
  public windowOpened = false;


  parentInstance!: AddCenterComponent | CenterComponent;

  /** SelectCity ctor */
  constructor(
    public dialogRef: MatDialogRef<SelectCityComponent>,
    private _centerService: CenterService, public dialog: DialogRef, @Inject(MAT_DIALOG_DATA) public dataItem: any) {
  }
  ngOnInit(): void {
    this.fillStateDropDown();

  }
  @HostListener('window:keyup.esc') onKeyUp() {
    this.dialogRef.close();
  }

  fillStateDropDown() {
    return this._centerService.getAllStates().subscribe(
      res => {
         
        this.stateListItem = res.selectListDtos;
        this.stateListItemFilter = res.selectListDtos;
      }
    );
  }

  handleFilterState(value: any) {
    this.stateListItem = this.stateListItemFilter.filter((s: any) => s.text.indexOf(value) !== -1);
  }

  handleStateChange(value: any) {

    this.selectedState = value;

    if (this.selectedState == undefined) {
      this.cityListItem = [];
      this.cityListItemFilter = [];
    }
    else {
      this.fillCitiesDropDown();
    }
  }

  fillCitiesDropDown() {
    this.cityListItemLoading = true;
    return this._centerService.getCityByState(this.selectedState).subscribe(
      res => {
        this.cityListItem = res.selectListDtos;
        this.cityListItemFilter = res.selectListDtos;
        this.cityListItemLoading = false;
      }
    );
  }

  handleCityFilter(value: any) {
    this.cityListItem = this.cityListItemFilter.filter((s: any) => s.text.indexOf(value) !== -1);
  }

  handleCityChange(value: any) {
    this.selectedCity = value;

      this.parentInstance.dropDownValueChangeCity(value);
  }

  selectThisCity() {

    this.parentInstance.selectCityDropDown(this.selectedCity);
    this.dialogRef.close();

  }

  close() {
    this.parentInstance.selectCityDropDown(null);
    this.dialogRef.close();
  }




}
