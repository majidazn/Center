import { Component, OnInit, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { CenterService } from '../../pages/center/center.service';
import { FullAccessRoles } from '../../../../models/fullAccess-roles.enum';
import { Permissions } from '../../../../models/permissions.enum';

@Component({
  selector: "app-aside-center-nav",
  templateUrl: "./aside-nav-center.component.html",
  styleUrls: ['../center-layout.component.scss'],

  encapsulation: ViewEncapsulation.None,
})
export class AsideNavCenterComponent implements OnInit, AfterViewInit {
  token: any;
  hasAccessToSecurity: boolean;
  fullAccessRoles = [];
  public Permissions = Permissions;


  constructor(private _centerService: CenterService) {
    Object.keys(FullAccessRoles).filter((key) => {
      this.fullAccessRoles.push(FullAccessRoles[key]);
    });
  }
  ngOnInit() {


  }
  ngAfterViewInit() {

  }

}
