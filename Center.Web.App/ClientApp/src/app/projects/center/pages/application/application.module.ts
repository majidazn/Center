import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../../modules/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { CenterApiService } from '../../services/center-api.service';
import { CenterAuthGuard } from '../../services/guards/center.auth.guard';
import { NotifyService } from '../../../../services/notify.service';
import { ApplicationService } from './application.service';
import { DeleteApplicationConfirmationDialogComponent } from './delete-application-confirmation-dialog/delete-application-confirmation-dialog.component';
import { ngfModule } from 'angular-file';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { AddApplicationComponent } from './add-application/add-application.component';

const routes: Routes = [
  {
    path: '',
    component: AddApplicationComponent,

  }]
@NgModule({
  declarations: [
    AddApplicationComponent,
    DeleteApplicationConfirmationDialogComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    ngfModule,
    NgxSpinnerModule,
    RouterModule.forChild(routes),
    ToastrModule.forRoot()

  ],
  providers: [
    ApplicationService,
    CenterApiService,
    CenterAuthGuard,
    NotifyService,
  ], 
  entryComponents: [
    DeleteApplicationConfirmationDialogComponent,
  ],
})
export class ApplicationModule { }
