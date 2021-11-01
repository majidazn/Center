import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';

import { FullAccessRoleIds } from '../models/fullAccess-roleIds.enum';
import { FullAccessRoles } from '../models/fullAccess-roles.enum';
import { PermissionService } from '../modules/ng2-permission/services/permission.service';
import { EnvService } from '../env/env.service';
import { AuthGuard } from '../services/auth.guard'
import { forEach } from '@angular/router/src/utils/collection';

@Injectable()
export class DefinePermissionsAndRolesService {
  debugger
  userPermissions: string[] = [];
  tenantId: number;
  roleIds = [];
  constructor(
    private _baseApi: BaseApiService,
    public authGuard: AuthGuard,
    private _env: EnvService
  ) { }

  async init(_permissionService) {
    // get user RoleIds from token
    this.roleIds = this.authGuard.getUserRoleId();
    this.tenantId = this.authGuard.getUserTenantId();
    // check if user has fullAccess role or not
    const userFullAccessRoles = [];
    if (this.roleIds && this.roleIds.includes(FullAccessRoleIds.Admin)) {
      userFullAccessRoles.push(FullAccessRoles.Admin);
    } else {
      if (this.roleIds && this.roleIds.length !== undefined) {
        this.roleIds.forEach(function (item) {
          if (Object.keys(FullAccessRoleIds).includes(item))
            userFullAccessRoles.push(FullAccessRoleIds[item]);
        });
        //this.roleIds.find((uId) => {
        //  Object.keys(FullAccessRoleIds).filter((key) => {
        //    if (uId === key) {
        //      userFullAccessRoles.push(FullAccessRoleIds[key]);
        //    }
        //  });
        //});
      } else {
        if (this.roleIds) {
          const isRole = Object.keys(FullAccessRoleIds).includes(

            this.roleIds.toString()
          );
          if (isRole) {
            Object.keys(FullAccessRoleIds).filter((key) => {
              if (this.roleIds && this.roleIds.includes(key)) {
                userFullAccessRoles.push(FullAccessRoleIds[key]);
              }
            });
          }
        }
      }
    }
    // if (userFullAccessRoles.length !== 0) {
    //   userFullAccessRoles.forEach(role => {
    //     this.userPermissions.push(FullAccessRoles[role]);
    //   });
    //   this._permissionService.define(this.userPermissions);
    // }
    // if {
    this.userPermissions = [];

    const permissionList = await this._baseApi.getDynamicPermissions(
      this.roleIds
    )
    if (permissionList) {
      permissionList.forEach((x) => {
        if (x !== null) {
          this.userPermissions.push(x);
        }
      });
    }
    const roleNames = this.authGuard.getUserRoleNames();
    roleNames.forEach((roleName) => {
      this.userPermissions.push(roleName);
    });
    // console.log(this.userPermissions);
    _permissionService.define(this.userPermissions);
  }
}
