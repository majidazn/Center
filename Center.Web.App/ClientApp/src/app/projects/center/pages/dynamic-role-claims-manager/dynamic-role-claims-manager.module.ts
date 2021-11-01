import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicRoleClaimsManagerComponent } from './dynamic-role-claims-manager.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../../../modules/shared.module';
import { CenterApiService } from '../../services/center-api.service';
import { CenterAuthGuard } from '../../services/guards/center.auth.guard';
import { DynamicRoleClaimsManagerService } from './dynamic-role-claims-manager.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


const routes: Routes = [
  {
    path: '',
    component: DynamicRoleClaimsManagerComponent,

  }]
@NgModule({
  declarations: [
    DynamicRoleClaimsManagerComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    ToastrModule.forRoot()

  ],
  providers: [
    DynamicRoleClaimsManagerService,
    CenterApiService,
    CenterAuthGuard,
  ]
})
export class DynamicRoleClaimsManagerModule { }
