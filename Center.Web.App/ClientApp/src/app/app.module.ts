import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BaseApiService } from './services/base-api.service';
import { MessageService } from '@progress/kendo-angular-l10n';
import { PersianMessageService } from './services/persian-message.service';
import { AuthInterceptor } from './services/auth.interceptor';
import { SharedService } from './services/shared.service';
import { CommonModule } from '@angular/common';
import { SharedModule } from './modules/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { ToastrModule } from 'ngx-toastr';
import { DefinePermissionsAndRolesService } from './services/define-permissions-and-roles.service';
import { PermissionService } from './modules/ng2-permission/services/permission.service';
import { PermissionGuard } from './modules/ng2-permission/services/permission.guard';
import { AuthGuard } from './services/auth.guard';
import { AccessDeniedComponent } from './projects/center/pages/error-pages/access-denied/access-denied.component';
import { UnauthorizedComponent } from './projects/center/pages/error-pages/unauthotrized/unauthorized.component';
import { Ng2Permission } from './modules/ng2-permission/module/ng2-permission.module';
import { PermissionHelper } from './modules/ng2-permission/services/permission-helper.service';
import { AddFullAccessUsersPipe } from './pipes/add-fullAccess-users.pipe';


@NgModule({
  declarations: [
    AppComponent,
    UnauthorizedComponent,
    AccessDeniedComponent,
  ],
  imports: [
    SharedModule,
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    AppRoutingModule,
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.circle,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#ffffff',
      secondaryColour: '#ffffff',
      tertiaryColour: '#ffffff'
    }),
    ToastrModule.forRoot(),
    Ng2Permission
  ],
  exports: [
    Ng2Permission
  ],
  providers: [
    SharedService,
    BaseApiService,
    { provide: MessageService, useClass: PersianMessageService },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    DefinePermissionsAndRolesService,
    PermissionGuard,
    PermissionService,
    AuthGuard,
    PermissionHelper
  ],
  entryComponents: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
