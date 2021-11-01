import { Component, AfterViewInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import * as moment from 'jalali-moment';
import { MatDatepickerInputEvent, MatDialog } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { ValidationService } from '../../../../../services/validation.service';
import { ActivityService } from '../activity.service';
import { RowArgs, SelectableSettings } from '@progress/kendo-angular-grid';
import { ToastrService } from 'ngx-toastr';
import { FullAccessRoles } from '../../../../../models/fullAccess-roles.enum';
import { Permissions } from '../../../../../models/permissions.enum';

export interface ActivityGroupModel {
  CenterId: number;
  ActivityGroup: number;
  StartDate: Date;
  EndDate: Date;

}

@Component({
  selector: 'app-add-activity-to-center',
  templateUrl: './add-activity-to-center.component.html',
  styleUrls: ['./add-activity-to-center.component.scss']
})

export class AddActivityToCenterComponent implements AfterViewInit {

  addActivityForm: FormGroup;
  startDate: any;
  selectedActivityGroup: any;
  centerModel: any;
  showSpinner: boolean;
  activityList: any;
  activityGroups: any;
  CenterId: number;
  sub: Subscription;
  viewCenterActivity: any;
  isLoading: boolean;
  fullAccessRoles = [];
  public Permissions = Permissions;
  public gridStateCenterActivity: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  public selectableSettings: SelectableSettings = {
    mode: 'multiple',
    checkboxOnly: true

  };
  assignedActivities = [];
  datePickerConfig = {
    format: 'YYYY/MM/DD',
  }
  selectedActivities: number[] = [];

  constructor(private _activityService: ActivityService,
    private route: ActivatedRoute,
    private fb: FormBuilder, private notifyService: ToastrService,
    private _router: Router,
    public MatDialog: MatDialog,
    private spinner: NgxSpinnerService) {
    this.datePickerConfig;
    Object.keys(FullAccessRoles).filter((key) => {
      this.fullAccessRoles.push(FullAccessRoles[key]);
    });
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.CenterId = +params['CenterId'];
      this.createForm();
      this.getCenterById(this.CenterId);
    });
  }

  ngAfterViewInit(): void {
    this.getActivityGroups();
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  createForm() {
    this.addActivityForm = this.fb.group({
      StartDate: ['', [Validators.required]],
      EndDate: [''],
      activityGroup: ['', [Validators.required]]
    },
      {
        validator: ValidationService.EndDateValidator
      });
  }

  getCenterById(centerId: number) {
    return this._activityService.getCenterById(centerId).subscribe(
      res => {
        this.centerModel = res;
      });
  }

  public getActivityGroups() {
    this.showSpinner = true;
    return this._activityService.getCenterVariable(10).subscribe(
      res => {
        this.activityGroups = res;
        this.showSpinner = false;
      }
    )
  }

  assignActivity() {
    this.spinner.show();
    let addedActivity: number[] = [];
    addedActivity = this.selectedActivities.filter(obj => { return this.assignedActivities.indexOf(obj) == -1 });
    if (this.addActivityForm.valid && addedActivity.length != 0) {
      let model = {
        activityDto:
        {
          centerVariableId: addedActivity[0],
          centerId: this.CenterId,
          parentId: this.selectedActivityGroup,
          validFrom: moment.from(this.addActivityForm.value.StartDate, "en").utc(true).toDate(),
          validTo: moment.from(this.addActivityForm.value.EndDate, "en").utc(true).toDate()
        }
      }
      this._activityService.createActivit(model).subscribe(
        response => {

          if (response.isSuccess) {
            this.notifyService.success(``, response.message);
            this.addActivityForm.reset();
            this.addActivityForm.controls['activityGroup'].setValue(this.selectedActivityGroup);
            this.getActivities()
          }

          else {
            this.notifyService.error(``, response.message);
          }

          this.spinner.hide();

        },
        error => {
          this.notifyService.error(``, error.error.Message);
          this.spinner.hide();
        })
    }

    else {

      this.spinner.hide ();
      (<any>Object).values(this.addActivityForm.controls).forEach(control => {
        control.markAsTouched();
      })

      if (addedActivity.length == 0)
        this.notifyService.error(``, 'برنامه مورد نظر برای تخصیص به مرکز را انتخاب نمایید');

    }
  }

  public dropDownValueChangeActivityGroup(value: any): void {
    this.selectedActivityGroup = value;
    this.getActivities();
  }

  getActivities() {
    this._activityService.getCenterVariablesWithActiveApplications(+this.selectedActivityGroup, this.centerModel.tenantId, this.centerModel.centerId).subscribe(
      response => {

        this.activityList = response;
        this.assignedActivities = [];
        this.selectedActivities = [];
        this.activityList.forEach(item => {
          if (item.isAssined) {
            this.assignedActivities.push(item.centerVariableId)
            this.selectedActivities.push(item.centerVariableId)
          }
        })
      }
    )
  }

  redirectToCenterPage() {
    this._router.navigate(['/center/'])
  }

  getPersianDate(value: any) {
    if (value == null) return "";
    const date = moment(value, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD');
    return date;
  }

  dateFilter = (d: Date): boolean => {
    var date = new Date(d);
    const currentDate = new Date();
    const dt = new Date();
    dt.setDate(currentDate.getDate() - 5);
    const isValid = ((currentDate < date));

    return isValid;
  }

  public isRowSelected = (e: RowArgs) => e.dataItem.isAssined == true;

  changeSelection(data) {
    this.selectedActivities = [];
    if (data.selectedRows.length != 0) {
      this.selectedActivities = this.assignedActivities.concat(data.selectedRows[0].dataItem.centerVariableId).map(x => x)
    }
    else if (data.deselectedRows.length != 0) {
      this.selectedActivities = this.assignedActivities.map((x) => x);
    }
  }
  clearEndDate() {
    this.addActivityForm.controls['EndDate'].patchValue('');
  }
}

