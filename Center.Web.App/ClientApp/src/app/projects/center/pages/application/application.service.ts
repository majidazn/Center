
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CenterApiService } from '../../services/center-api.service';
import { EnvService } from '../../../../env/env.service';


@Injectable()
export class ApplicationService {

  constructor(private centerApiService: CenterApiService, private env: EnvService) {
  }


  getCenterVariable(parentId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/CenterVariable/GetCenterVariablesByParentId?parentId=${parentId}`);
  }

  createCenterVariable(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/CreateCenterVariable`, model);
  }

  updateCenterVariable(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/EditCenterVariable`, model);
  }

  sortCenterVariables(sortedModel: Array<object>): Observable<any> {
    let model = {
      sortDtos: sortedModel
    }
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/SortCenterVariables`, model);
  }

  deleteApplication(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/RemoveCenterVariable`, model);
  }

 }
