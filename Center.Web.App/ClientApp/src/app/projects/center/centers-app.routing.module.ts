import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FullAccessRoles } from '../../models/fullAccess-roles.enum';
import { IPermissionGuardModel } from '../../modules/ng2-permission/model/permission-guard.model';
import { PermissionGuard } from '../../modules/ng2-permission/services/permission.guard';
// components
import { CenterLayoutComponent } from './center-layout/center-layout.component';
import { Permissions } from '../../models/permissions.enum';

const fullAccessRoles = [];
Object.keys(FullAccessRoles).filter((key) => {
  fullAccessRoles.push(FullAccessRoles[key]);
});

const routes: Routes = [
  {
    'path': '',
    'component': CenterLayoutComponent,
    'children': [
      {
        path: '',
       canActivate: [PermissionGuard],
        loadChildren: './pages/center/center.module#CenterModule',
        data: {
          Permission: {
            Only: fullAccessRoles.concat([
              Permissions.SearchCenter,
            ]),
            RedirectTo: '403',
          } as IPermissionGuardModel,
          breadcrumb: 'تعریف مرکز',
          hasRibbon: true,
          title: "home",
        }
      },
      {
        'path': 'activity/:CenterId',
        canActivate: [PermissionGuard],
        loadChildren: './pages/activity/activity.module#ActivityModule',
        data: {
          breadcrumb: 'تخصیص برنامه به مرکز',
          hasRibbon: true,
          Permission: {
            Only: fullAccessRoles.concat([
              Permissions.GetActivitiesByCenterId,
            ]),
            RedirectTo: '403',
          } as IPermissionGuardModel,
        }
      },
      {
        'path': 'application',
        canActivate: [PermissionGuard],
        data: {
          breadcrumb: 'تعریف برنامه',
          Permission: {
            Only: fullAccessRoles.concat([
              Permissions.GetCenterVariables,
            ]),
            RedirectTo: '403',
          } as IPermissionGuardModel,
        },
        loadChildren: './pages/application/application.module#ApplicationModule',

      },
      {
        path: 'rolemanager',
        canActivate: [PermissionGuard],
        data: {
          breadcrumb: 'امنیت',
          hasRibbon: true,
          Permission: {
            Only: fullAccessRoles.concat([
              Permissions.UpdateAccess
            ]),
            RedirectTo: '403',
          } as IPermissionGuardModel,
        },
        loadChildren: './pages/dynamic-role-claims-manager/dynamic-role-claims-manager.module#DynamicRoleClaimsManagerModule',
      },
      {
        path: 'variables',
        canActivate: [PermissionGuard],
        loadChildren: './pages/variables/variables.module#VariablesModule',
        data: {
          breadcrumb: 'مدیریت متغیرهای پایه',
          hasRibbon: true,
          Permission: {
            Only: {
              Only: fullAccessRoles.concat([
                Permissions.SearchCenterVariable,
                Permissions.GetCenterVariables,
              ]),
              RedirectTo: '403',
            } as IPermissionGuardModel,
          }
        },
      }
    ],
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CentersAppRoutingModule { }
