
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './services/auth.guard';
import { FullAccessRoles } from './models/fullAccess-roles.enum';
import { AccessDeniedComponent } from './projects/center/pages/error-pages/access-denied/access-denied.component';
import { UnauthorizedComponent } from './projects/center/pages/error-pages/unauthotrized/unauthorized.component';

const fullAccessRoles = [];
Object.keys(FullAccessRoles).filter((key) => {
  fullAccessRoles.push(FullAccessRoles[key]);
});

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', redirectTo: 'center', pathMatch: 'full' },
      {
        path: 'center',
        canActivateChild: [AuthGuard],
        loadChildren: './projects/center/centers-app.module#CentersAppModule'
      },
      {
        component: AccessDeniedComponent,
        path: '403',
      },
      {
        component: UnauthorizedComponent,
        path: '401',
      },
    ], 
    )
  ],
  exports: [
    RouterModule
  ],
  providers: [
  ]
})
export class AppRoutingModule { }
