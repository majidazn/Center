import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { DragulaModule, DragulaService } from 'ng2-dragula';
// Import kendo angular ui 
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule, ExcelModule } from '@progress/kendo-angular-grid';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { RTL } from '@progress/kendo-angular-l10n';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { DpDatePickerModule } from 'ng2-jalali-date-picker';
import { SpinnerModule } from 'spinner-angular';
import { ControlMessagesComponent } from '../directives/control-messages/control-messages.component';
import { FireFunctionComponent } from '../directives/fire-function/fire-function.component';

import { ValidationService } from '../services/validation.service';

import { AuthService } from '../services/auth.service';

import { ToastyModule } from 'ng2-toasty';

import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomDialogComponent } from '../directives/custom-dialog/custom-dialog.component';


import { KendoGridPaginationComponent } from '../directives/kendo-grid-pagination/kendo-grid-pagination.component';
import { AngularDraggableModule } from 'angular2-draggable';

import { DialogRef } from '@progress/kendo-angular-dialog';

import {
  MatFormFieldModule, MatInputModule,
  MatButtonModule, MatButtonToggleModule,
  MatDialogModule, MatIconModule,
  MatSelectModule, MatToolbarModule,
  MatDatepickerModule,
  DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, MatTableModule, MatCheckboxModule, MatRadioModule, MatCardModule, MatTooltipModule

} from '@angular/material';


import {
  WMatTimePickerComponent,
  WTimeDialogComponent,
  WClockComponent
} from '../directives/time-control/index';
import { InlineLabelComponent } from '../directives/inline-label/inline-label.component';
import { MaterialPersianDateAdapter, PERSIAN_DATE_FORMATS } from '../directives/material-persian-date-adapter/material.persian-date.adapter';
import { DraggableDirectiveComponent } from '../directives/draggable-directive/draggable-directive.component';

import { EnvServiceProvider } from '../env/env.service.provider';
import { NgxSpinnerModule } from 'ngx-spinner';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { OnlyNumber } from '../directives/only-number/only-number.component';
import { ControlMessages2Component } from '../directives/control-messages2/control-messages2.component';
//import { ToggleFullscreenDirective } from '../directives/toggle-fullscreen-directive/toggle-fullscreen-directive.component';
import { GridContextMenuComponent } from '../directives/grid-context-menu/grid-context-menu.component';
import { PopupModule } from '@progress/kendo-angular-popup';
import { EnterDetectorDirective } from '../directives/enter.directive/enter.directive';
import { MySpinnerComponent } from '../directives/spinner/spinner.component';
import { MyTruncatePipe } from '../pipes/TruncatePipe';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NotifyService } from '../services/notify.service';
import { SharedService } from '../services/shared.service';
import { AddFullAccessUsersPipe } from '../pipes/add-fullAccess-users.pipe';
import { Ng2Permission } from './ng2-permission/module/ng2-permission.module';

@NgModule({
  declarations: [
   
    ControlMessagesComponent,
    FireFunctionComponent,
    MySpinnerComponent,
    CustomDialogComponent,
    WMatTimePickerComponent,
    WTimeDialogComponent,
    WClockComponent,
    KendoGridPaginationComponent,
    InlineLabelComponent,
    DraggableDirectiveComponent,
    MyTruncatePipe,
    OnlyNumber,
    ControlMessages2Component,
    EnterDetectorDirective,
    GridContextMenuComponent,
    AddFullAccessUsersPipe

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonsModule,
    GridModule,
    ExcelModule,
    TreeViewModule,
    DropDownsModule,
    InputsModule,
    DateInputsModule,
    DialogsModule,
    LayoutModule,
    DpDatePickerModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule, MatButtonToggleModule,
    MatDialogModule, MatIconModule,
    MatSelectModule, MatToolbarModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatRadioModule,
    MatCardModule,
    MatTooltipModule,
    ToastyModule,
    SpinnerModule.forRoot({
      primaryColor: '#FCBE41',
      secondaryColor: '#309488',
    }),
    DragulaModule,
    NgbModule.forRoot(),
    AngularDraggableModule,
    NgxSpinnerModule,
    DragDropModule,
    PopupModule,
    SnotifyModule,
    Ng2Permission

  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ControlMessagesComponent, GridContextMenuComponent,
    FireFunctionComponent,
    ButtonsModule,
    GridModule,
    ExcelModule,
    TreeViewModule,
    DropDownsModule,
    InputsModule,
    DateInputsModule,
    DialogsModule,
    LayoutModule,
    DpDatePickerModule,
    MatInputModule,
    MatCardModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatButtonModule, MatButtonToggleModule,
    MatDialogModule, MatIconModule,
    MatSelectModule, MatToolbarModule,
    MatDatepickerModule,
    MatTableModule,
    MatCheckboxModule,
    MatRadioModule,
    TranslateModule,
    ToastyModule,
    MySpinnerComponent,
    SpinnerModule,
    DragulaModule,
    CustomDialogComponent,
    NgbModule,
    WMatTimePickerComponent,
    WTimeDialogComponent,
    WClockComponent,
    KendoGridPaginationComponent,
    AngularDraggableModule,
    InlineLabelComponent,
    DraggableDirectiveComponent,
    MyTruncatePipe, 
    NgxSpinnerModule,
    DragDropModule,
    ControlMessages2Component,
    PopupModule,
    SnotifyModule,
    AddFullAccessUsersPipe,
    Ng2Permission


  ],
  providers: [
    ValidationService,
    AuthService,
    // Enable Right-to-Left mode for Kendo UI components
    { provide: RTL, useValue: true },
    NgbActiveModal,
    DialogRef,
    DragulaService,
     { provide: DateAdapter, useClass: MaterialPersianDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: PERSIAN_DATE_FORMATS },     
    EnvServiceProvider,
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService,
    NotifyService,
    SharedService,

  ],
  entryComponents: [
    WTimeDialogComponent,
  ]
})
export class SharedModule {



}
