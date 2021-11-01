
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { State } from '@progress/kendo-data-query';
import { CenterApiService } from '../../services/center-api.service';
import { EnvService } from '../../../../env/env.service';

@Injectable()
export class VariablesService {

  constructor(private centerApiService: CenterApiService, private env: EnvService) {
  } 

  getParentGvariables(model: State): Observable<any> {

    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/SearchCenterVariable`, model);
  }
  createCenterVariable(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/CreateCenterVariable`, model);
  }
  GetCenterVariablesByParentId(parentId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/CenterVariable/GetCenterVariablesByParentId?parentId=${parentId}`);
  }

  updateCenterVariable(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/EditCenterVariable`, model);
  }
  GetCenterVariables(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/SearchCenterVariable`, model);
  }
  deleteCenterVariable(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/CenterVariable/RemoveCenterVariable`, model);
  }

}
