import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NotifyService } from '../../../../services/notify.service';
import { DynamicRoleClaimsManagerService } from './dynamic-role-claims-manager.service';
//import sha256, { Hash, HMAC } from "fast-sha256";
//import * as sha256 from "fast-sha256";

declare var $: any;
@Component({
  selector: 'app-dynamic-role-claims-manager',
  templateUrl: './dynamic-role-claims-manager.component.html',
  styleUrls: ['./dynamic-role-claims-manager.component.scss']
})
/** DynamicRoleClaimsManager component*/
export class DynamicRoleClaimsManagerComponent {
  RoleList: any;
  controllers: any;
  role: any;
  selectedRole: any;
  formAccess: FormGroup;
  RoleListFilter: any;
  /** DynamicRoleClaimsManager ctor */
  constructor(
    private fb: FormBuilder,
    private _dynamicRoleClaimsManagerService: DynamicRoleClaimsManagerService,
    private _route: ActivatedRoute,
    private notifyService: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.getRoles();
    this.CreateForm();

  }

  getRoles() {
     
    return this._dynamicRoleClaimsManagerService.getRoles().subscribe(
      res => {
         
        this.RoleList = res;
        this.RoleListFilter = res;
      });
  }

  onchangeRole(value: number) {

    if (value == undefined) {
      this.controllers = null;
      this.role = null;
      return;
    }
      

    return this._dynamicRoleClaimsManagerService.updateAccess(value).subscribe(
      res => {
        this.controllers = res.securedControllerActions;
        this.role = res.role;
      });
  }

  isChecked(value: any) {
    
    if (this.role.claims == null)
      return false;

    return this.role.claims.some(e => e.claimValue === value);
    //  return false;
  }
  string2Bin(str) {
    var result = [];
    for (var i = 0; i < str.length; i++) {
      result.push(str.charCodeAt(i).toString(2));
    }
    return result;
  }
  bin2String(array) {
    var result = "";
    for (var i = 0; i < array.length; i++) {
      result += String.fromCharCode(parseInt(array[i], 2));
    }
    return result;
  }
  CreateForm() {
    this.formAccess = this.fb.group({

      Role: [null],


    });

  }

  save() {
    this.spinner.show();
     
    var actionIds: Array<string> = [];
    var checkedList = $('.accessdiv input[type="checkbox"]:checked');
    checkedList.each(function () {
      actionIds.push(this.id);
    });
    
    return this._dynamicRoleClaimsManagerService.updateAccessSubmit(this.formAccess.value.Role, actionIds).subscribe(
      res => {
        this.spinner.hide();
        if (res) {
          this.notifyService.success("", "تغییرات با موفقیت انجام شد");
        }
        else {
          this.notifyService.error("", "خطا در اعمال تغییرات!");
        }
      },
      response => {
        this.spinner.hide();
      }
    );
  }


  handleFilterRoleList(value: any) {
    if (value !== "")
      this.RoleList = this.RoleListFilter.filter((s) => s.text.toLowerCase().indexOf(value.toLowerCase()) !== -1);
    else
      this.RoleList = this.RoleListFilter;
  }

}
