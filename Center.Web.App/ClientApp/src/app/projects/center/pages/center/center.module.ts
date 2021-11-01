import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../../modules/shared.module';
import { AddCenterComponent } from './add-center/add-center.component';
import { DeleteCenterConfirmationDialogComponent } from './delete-center-confirmation-dialog/delete-center-confirmation-dialog.component';
import { SelectCityComponent } from './select-city/select-city.component';
import { AddTelecomComponent } from './add-telecom/add-telecom.component';
import { AddEaddressComponent } from './add-eaddress/add-eaddress.component';
import { CenterAssignedActivityComponent } from './center-assigned-activity/center-assigned-activity.component';
import { CenterApiService } from '../../services/center-api.service';
import { CenterAuthGuard } from '../../services/guards/center.auth.guard';
import { NotifyService } from '../../../../services/notify.service';
import { NgxMaskModule } from 'ngx-mask';
import { ngfModule } from 'angular-file';
import { ToastyModule } from 'ng2-toasty';
import { NgxSpinnerModule } from 'ngx-spinner';
import { CenterComponent } from './center.component';
import { CenterService } from './center.service';
import { ToastrModule } from 'ngx-toastr';

const routes: Routes = [
  {
    path: '',
    component: CenterComponent,

  }]
@NgModule({
  declarations: [
    CenterComponent,
    AddCenterComponent,
    DeleteCenterConfirmationDialogComponent,
    SelectCityComponent,
    AddTelecomComponent,
    AddEaddressComponent,
    CenterAssignedActivityComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    NgxMaskModule.forRoot(),
    ngfModule,
    ToastyModule.forRoot(),
    NgxSpinnerModule,
    ToastrModule.forRoot()


  ],
  entryComponents: [
    AddCenterComponent,
    DeleteCenterConfirmationDialogComponent,
    SelectCityComponent,
    AddTelecomComponent,
    AddEaddressComponent
  ],
  providers: [
    CenterService,
    CenterApiService,
    CenterAuthGuard,
    NotifyService,
  ]

})
export class CenterModule { }
