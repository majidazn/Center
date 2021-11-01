import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../../modules/shared.module';
import { AddVariablesComponent } from './add-variables/add-variables.component';
import { VariableSubGridComponent } from './variable-sub-grid/variable-sub-grid.component';
import { Routes, RouterModule } from '@angular/router';
import { DeleteVariableConfirmationDialogComponent } from './delete-variable-confirmation-dialog/delete-variable-confirmation-dialog.component';
import { VariablesService } from './variables.service';
import { ToastrModule } from 'ngx-toastr';

const routes: Routes = [
  { 
    path: '',
    component: AddVariablesComponent,

  }]
@NgModule({
  declarations: [
    AddVariablesComponent,
    VariableSubGridComponent,
    DeleteVariableConfirmationDialogComponent,

  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    ToastrModule.forRoot()

  ],
  entryComponents: [
    DeleteVariableConfirmationDialogComponent,
  ],
  providers: [
    VariablesService
  ]
})
export class VariablesModule { }
