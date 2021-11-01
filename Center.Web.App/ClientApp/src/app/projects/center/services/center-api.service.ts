import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { BaseApiService } from '../../../services/base-api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { EnvService } from '../../../env/env.service';




@Injectable()
export class CenterApiService extends BaseApiService {

  constructor(

    http: HttpClient, public notifyService: ToastrService, spinner: NgxSpinnerService, router: Router, env: EnvService
  ) {
    super(http, notifyService, spinner ,env,router);
  }
}
