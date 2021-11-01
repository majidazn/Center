import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { CenterApiService } from '../../services/center-api.service';
import { EnvService } from '../../../../env/env.service';
import { FilterModel } from './center.component';
import { GeneralVariablesParentIdEnum, GeneralVariablesSystemCodeEnum } from '../../moldes/general-variables.enum';




@Injectable()
export class CenterService {

  constructor(private centerApiService: CenterApiService, private env: EnvService) {
  }

  getAllCities(): Observable<any> {

    let data = [GeneralVariablesSystemCodeEnum.City];
    let url = `${this.env.CenterApi}/api/GeneralVariable/StandardVariables`;
    return this.centerApiService.post(url, data);

  }
  getAllStates(): Observable<any> {

    let statesParentId = GeneralVariablesParentIdEnum.State;
    let url = `${this.env.CenterApi}/api/GeneralVariable/StandardVariablesByParentId?parentId=${statesParentId}`;
    return this.centerApiService.get(url);

  }
  getCityByState(stateId: number): Observable<any> {

    let url = `${this.env.CenterApi}/api/GeneralVariable/StandardVariablesByParentId?parentId=${stateId}`;
    return this.centerApiService.get(url);

  }

  getCenterVariable(parentId: number): Observable<any> {

    let url = `${this.env.CenterApi}/api/CenterVariable/GetCenterVariablesByParentId?parentId=${parentId}`;
    return this.centerApiService.get(url);

  }

  getCenterInformation(filterModel: FilterModel): Observable<any> {

    let url = `${this.env.CenterApi}/api/Center/SearchCenter`;
    return this.centerApiService.post(url, filterModel);

  }

  createCenter(model): Observable<any> {

    return this.centerApiService.post(`${this.env.CenterApi}/api/center/CreateCenter`, model);

  }
  getCenterById(centerId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/Center/GetCenterById?centerId=${centerId}`);
  }

  getCenterActivities(centerId: number): Observable<any> {
    return this.centerApiService.get(`${this.env.CenterApi}/api/Activity/GetActivitiesByCenterId?centerId=${centerId}`);
  }
  removeActivity(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/Activity/RemoveActivity`, model);
  }
  updateCenter(model): Observable<any> {

    return this.centerApiService.post(`${this.env.CenterApi}/api/Center/EditCenter`, model);
  }

  deleteCenter(model): Observable<any> {
    return this.centerApiService.post(`${this.env.CenterApi}/api/Center/RemoveCenter`,model);
  }

}
