import { Component, OnDestroy } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogService } from '@progress/kendo-angular-dialog';
import { DragulaService } from 'ng2-dragula';
import { Subscription } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material';
import { CreateCenterVariableModel } from '../../../moldes/center-variable/create-center-variable.model';
import { EditCenterVariableModel } from '../../../moldes/center-variable/edit-center-variable.model';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../application.service';
import { DeleteApplicationConfirmationDialogComponent } from '../delete-application-confirmation-dialog/delete-application-confirmation-dialog.component';
import { SharedService } from '../../../../../services/shared.service';
import { Permissions } from '../../../../../models/permissions.enum'

declare var $: any;
@Component({
  selector: 'app-add-application',
  templateUrl: './add-application.component.html',
  styleUrls: ['./add-application.component.scss']
})

export class AddApplicationComponent implements OnDestroy {

  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  addApplicationForm: FormGroup;
  searchForm: FormGroup;
  showSpinner: boolean;
  activityGroups: any;
  dataSource: any;
  displayedColumns: string[];

  subs = new Subscription();
  deleteCenterVariableDialog: any;
  selectedCvariableForDelete: any;
  selectedActivityGroup: any;
  internalUsage = [];
  showNoRecord = true;
  isCloudApp = false;
  selectedFile: File;
  actMaxSize = 50000;
  logoImageBase64WithDataImage: string | ArrayBuffer;
  logoImageBase64: string;
  logoImage: File;
  files: File[] = [];
  file: any;
  public lastInvalids: any;
  public sendableFormData: any;
  fullAccessRoles = [];
  public Permissions = Permissions;
  constructor(private fb: FormBuilder, private dialogService: DialogService,
    private _sharedService: SharedService,
    private _applicationService: ApplicationService,
    private notifyService: ToastrService, private dragulaService: DragulaService, public MatDialog: MatDialog, private spinner: NgxSpinnerService,) {
    this.fullAccessRoles = this._sharedService.fullAccessRoles;
    this.createForm();
    this.getActivityGroups();
    this.createSearchForm();
    this.getInternalUsage()
    this.dragulaService.createGroup("SPILL", {
      removeOnSpill: false,
      direction: 'horizental',
    });

    this.subs.add(this.dragulaService.dropModel(this.dataSource)
      .subscribe(({ name, el, target, source, sibling, sourceModel, targetModel, item }) => {
        var i = 1;
        const sortedModel = targetModel.map(({ centerVariableId: centerVariableId, parentId: parentId, sort: sort }) => [{ centerVariableId: centerVariableId, parentId: parentId, sort: i++ }][0]);
        return this._applicationService.sortCenterVariables(sortedModel).subscribe(
          res => {
            this.searchApplication();
          }
        )
      })
    );
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
    this.dragulaService.remove('SPILL');
    this.dragulaService.removeModel('SPILL');
    this.dragulaService.destroy('SPILL');
  }

  createForm() {
    this.addApplicationForm = this.fb.group({
      activityGroup: [null, [Validators.required]],
      applicationName: ['', [Validators.required]],
      applicationEName: ['', [Validators.required]],
      centervariableId: [0],
      internalUsage: [0, [Validators.required]],
      code: [null],

    });
  }

  createSearchForm() {
    this.searchForm = this.fb.group({
      activityGroup: [null, [Validators.required]],
    });
  }

  getInternalUsage() {
    this._applicationService.getCenterVariable(2).subscribe(
      res => {
        this.internalUsage = res
      }
    )
  }

  getActivityGroups() {
    return this._applicationService.getCenterVariable(10).subscribe(
      res => {
        this.activityGroups = res;
      }
    )
  }

  saveApplication() {
    this.spinner.show();
    //Add Mode
    if (!this.addApplicationForm.value.centervariableId  ) {
      const model: CreateCenterVariableModel = {
        createCenterVariableDto: {
          name: this.addApplicationForm.value.applicationName,
          id:0,
          parentId: this.selectedActivityGroup,
          enName: this.addApplicationForm.value.applicationEName,
          internalUsage: this.addApplicationForm.value.internalUsage,
          icon: this.isCloudApp?this.logoImageBase64:null,
          address: this.isCloudApp ? this.addApplicationForm.value.address:null,
          shortKey: this.isCloudApp ? this.addApplicationForm.value.shortKey : null,
          code: this.addApplicationForm.value.code ? this.addApplicationForm.value.code:0
        }
      }
      this._applicationService.createCenterVariable(model).subscribe(
        res => {
          if (res.isSuccess) {
            this.notifyService.success(``, res.message);
            this.searchApplication();
            this.resetForm();
            this.removeLogoImage();
            this.addApplicationForm.get('activityGroup').setValue(this.selectedActivityGroup);
            this.selectedActivityGroup = model.createCenterVariableDto.parentId;
            this.searchApplication();

          }
          else {
            this.notifyService.error(``, res.message);
            this.spinner.hide();

          }
        },
        error => {
          this.spinner.hide();
        }
      )
    }
    // Edit Mode
    else {

      const model: EditCenterVariableModel = {
        editCenterVariableDto: {
          name: this.addApplicationForm.value.applicationName,
          parentId: this.selectedActivityGroup,
          enName: this.addApplicationForm.value.applicationEName,
          id: this.addApplicationForm.value.centervariableId,
          internalUsage: this.addApplicationForm.value.internalUsage,
          icon: this.isCloudApp?this.logoImageBase64:null,
          address: this.isCloudApp?this.addApplicationForm.value.address:null,
          shortKey: this.isCloudApp ? this.addApplicationForm.value.shortKey : null,
          code: this.addApplicationForm.value.code ? this.addApplicationForm.value.code : 0
        }
      }

      this._applicationService.updateCenterVariable(model).subscribe(
        res => {
          if (res.isSuccess) {
            this.notifyService.success(``, res.message);
            this.resetForm();
            this.removeLogoImage();
            this.selectedActivityGroup = model.editCenterVariableDto.parentId;
            this.addApplicationForm.get('activityGroup').setValue(this.selectedActivityGroup);
            this.searchApplication();
          }
          else {
            this.notifyService.error(``, res.message);
            this.spinner.hide();

          }
        },
        error => {
          this.spinner.hide();
        }
      );
    }
  }

  searchApplication() {
    const parentId = this.selectedActivityGroup;
    if (parentId) {
      this.spinner.show();

      let data = this._applicationService.getCenterVariable(parentId).subscribe(
        res => {
          this.dataSource = res;
          this.dataSource.forEach(item => {
            item.iconbase64 = item.icon?`data:image/png;base64,${item.icon}`:null;
          })
          this.displayedColumns = ['Name', 'EName'];
          if (this.dataSource.length != 0) {
            this.showNoRecord = false;
          }
          else {
            this.showNoRecord = true;
          }
          this.spinner.hide();
        },
        error => {
          this.spinner.hide();
        }
      );
    }
    else {
      this.showNoRecord = true;
      this.dataSource = [];

    }
  }

  edit(item: any) {

    $("#addSubProjectTab").click();
    $([document.documentElement, document.body]).animate({
      scrollTop: $("body").offset().top
    }, 2000);

    this.selectedActivityGroup = item.parentId;
    this.addApplicationForm.get('applicationName').setValue(item.name);
    this.addApplicationForm.get('applicationEName').setValue(item.enName);
    this.addApplicationForm.get('centervariableId').setValue(item.centerVariableId);
    this.addApplicationForm.get('internalUsage').setValue(item.internalUsage);
    this.addApplicationForm.get('code').setValue(item.code);
    if (this.isCloudApp) {
      this.addApplicationForm.get('address').setValue(item.address);
      this.addApplicationForm.get('shortKey').setValue(item.shortKey);
      this.addApplicationForm.get('code').setValue(item.code);
      this.logoImage = item.icon;
      this.logoImageBase64 = item.icon;
      this.logoImageBase64WithDataImage = item.iconbase64;
    }

  }

  delete() {
    this.spinner.show();
    let model = {
      centerVariableId: this.selectedCvariableForDelete
    }

    this._applicationService.deleteApplication(model).subscribe(
      res => {
        if (res.isSuccess) {
          this.notifyService.success(``, res.message);
          this.searchApplication();
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

  onDeleteCenterVariable(CVariable: any) {
    this.selectedCvariableForDelete = CVariable.centerVariableId;
    this.deleteCenterVariableDialog = this.MatDialog.open(DeleteApplicationConfirmationDialogComponent, {
      data: { title: 'حذف', componentName: 'AddApplicationComponent' },
      disableClose: true,
    });
    this.deleteCenterVariableDialog.componentInstance.parentInstance = this;
  }

  resetForm() {
    debugger
    this.addApplicationForm.reset()
    this.addApplicationForm.markAsPristine();
    this.addApplicationForm.markAsUntouched();
    this.addApplicationForm.updateValueAndValidity();
    this.dataSource = [];
    this.showNoRecord = true;
    this.isCloudApp = false;

  }

  resetSearchForm() {
    debugger
    this.searchForm.reset();
    $(".subProjectList tbody tr.hasDataRow").hide();
    this.showNoRecord = true;
    this.isCloudApp = false;
  }

  onchangeActivityGroup(value: any) {
    this.selectedActivityGroup = value;
    this.searchForm.get('activityGroup').setValue(value);
    this.addApplicationForm.get('activityGroup').setValue(value);
    this.searchApplication();
    if (this.selectedActivityGroup == 24 || this.selectedActivityGroup == 25) {
      this.isCloudApp = true;
      this.addApplicationForm.addControl('address', new FormControl('', Validators.required));
      this.addApplicationForm.addControl('shortKey', new FormControl(''));
      this.addApplicationForm.addControl('icon', new FormControl(''));
    }
    else {
      this.isCloudApp = false;
      this.addApplicationForm.removeControl('address');
      this.addApplicationForm.removeControl('shortKey');
      this.addApplicationForm.removeControl('icon');
    }
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
    this.showNoRecord = true;


  }

}
