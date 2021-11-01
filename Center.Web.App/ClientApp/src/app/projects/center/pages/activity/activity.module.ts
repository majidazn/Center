import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../../modules/shared.module';
import { AddActivityToCenterComponent } from './add-activity-to-center/add-activity-to-center.component';
import { CenterApiService } from '../../services/center-api.service';
import { CenterAuthGuard } from '../../services/guards/center.auth.guard';
import { NotifyService } from '../../../../services/notify.service';
import { ActivityService } from './activity.service';
import { ToastrModule } from 'ngx-toastr';

const routes: Routes = [

  {
    path: '',
    component: AddActivityToCenterComponent
  }, 
]
@NgModule({
  declarations: [
    AddActivityToCenterComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    ToastrModule.forRoot()

  ],
  entryComponents: [
  ],
  providers: [
    ActivityService,
    CenterApiService,
    CenterAuthGuard,
    NotifyService,
  ]
})
export class ActivityModule { }
