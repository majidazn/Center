import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { State } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { CenterApiService } from '../../services/center-api.service';
import { EnvService } from '../../../../env/env.service';


@Injectable()
export class DynamicRoleClaimsManagerService {

  constructor(private centerApiService: CenterApiService, private env: EnvService) {
  }

  

  getRoles(): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/DynamicRoleClaimsManager/GetRoles`);
  }
  updateAccess(roleId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/DynamicRoleClaimsManager/UpdateAccess?RoleId=${roleId}`);
  }
  updateAccessSubmit(roleId: number, actionIds: Array<string>): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/DynamicRoleClaimsManager/UpdateAccessSubmit?RoleId=${roleId}`, actionIds);
  }
 
}
