import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CenterApiService } from '../../services/center-api.service';
import { EnvService } from '../../../../env/env.service';

@Injectable()
export class ActivityService {

  constructor(private centerApiService: CenterApiService, private env: EnvService) {
  }

  getCenterVariable(parentId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/CenterVariable/GetCenterVariablesByParentId?parentId=${parentId}`);
  }

  getCenterById(centerId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/Center/GetCenterById?centerId=${centerId}`);
  }

  createActivit(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/Activity/CreateActivity`,model);
  }

  getCenterActivity(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/Activity/CreateActivity`, model);
  }
  getCenterVariablesWithActiveApplications(parentId, tenantId, centerId): Observable<any> {
    return this.centerApiService.get(
      `${this.env.CenterApi}/api/CenterVariable/GetCenterVariablesWithActiveApplications?parentId=${parentId}&tenantId=${tenantId}&centerId=${centerId}`)
  }
}
