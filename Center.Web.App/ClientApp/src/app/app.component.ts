import { Component } from '@angular/core';
//import { TranslateService } from '@ngx-translate/core';
import { Router, Event, NavigationStart, NavigationEnd, NavigationError } from '@angular/router';

//import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
import { BaseUrls } from './models/base-urls';

import { BaseApiService } from './services/base-api.service';
import { trigger, style, transition, animate, group }
  from '@angular/animations';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { NavigationCancel } from '@angular/router';
import { NgxLoadingModule } from 'ngx-loading';
import { SharedService } from './services/shared.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { DefinePermissionsAndRolesService } from './services/define-permissions-and-roles.service';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [
    trigger('itemAnim', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)' }),
        animate(350)
      ]),
      transition(':leave', [
        group([
          animate('0.2s ease', style({
            transform: 'translate(150px,25px)'
          })),
          animate('0.5s 0.2s ease', style({
            opacity: 0
          }))
        ])
      ])
    ])
  ]
})
export class AppComponent {
  title = 'app';
  public loading = false;
  fullImagePath = '/assets/images/logo.png';

  isAuthenticated = true;
  eventRouting: any;
  currentUrl: any;

  constructor(
    private router: Router,
    private baseApiService: BaseApiService,
    private titleService: Title,
    private _spinner: NgxSpinnerService,
    private _sharedService: SharedService)
  {
  }



  public setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }


}

