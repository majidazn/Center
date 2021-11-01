import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../modules/shared.module';
import { CenterLayoutComponent } from './center-layout/center-layout.component';
import { AppHeaderCenterComponent } from './center-layout/app-header/app-header-center.component';
import { AsideNavCenterComponent } from './center-layout/aside-nav/aside-nav-center.component';
import { CenterApiService } from './services/center-api.service';
import { NgxMaskModule } from 'ngx-mask';
import { CtrlSDetectorDirective } from '../../directives/ctrl-s.directive-ts/ctrl-s.directive';
import { CenterAuthGuard } from './services/guards/center.auth.guard';
import { ngfModule } from 'angular-file';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastyModule } from 'ng2-toasty';
import { CentersAppRoutingModule } from './centers-app.routing.module';
import { CenterService } from './pages/center/center.service';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';
import { NotifyService } from '../../services/notify.service';
import { SharedService } from '../../services/shared.service';
import { PermissionService } from '../../modules/ng2-permission/services/permission.service';
import { PermissionGuard } from '../../modules/ng2-permission/services/permission.guard';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2Permission } from '../../modules/ng2-permission/module/ng2-permission.module';
import { PermissionHelper } from '../../modules/ng2-permission/services/permission-helper.service';

@NgModule({
  declarations: [
    CenterLayoutComponent,
    AppHeaderCenterComponent,
    AsideNavCenterComponent,
    CtrlSDetectorDirective
  ],
  imports: [
    CommonModule,
    SharedModule,
    CentersAppRoutingModule,
    NgxMaskModule.forRoot(),
    ngfModule,
    ToastyModule.forRoot(),
    NgxSpinnerModule,
    SnotifyModule,
    NgbDropdownModule,

  ],

  exports: [
  ],
  providers: [
    CenterService,
    CenterApiService,
    CenterAuthGuard,
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService,
    SharedService,
    NotifyService,
    PermissionGuard,
    PermissionService,
    PermissionHelper
  ],

})
export class CentersAppModule { }
