import { Component, OnInit, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { EnvService } from '../../../../env/env.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SharedService } from '../../../../services/shared.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
declare var jquery: any;
declare var $: any;


declare let mLayout: any;
@Component({
    selector: "app-header-center",
  templateUrl: "./app-header-center.component.html",
  styleUrls: ['../center-layout.component.scss'],

    encapsulation: ViewEncapsulation.None,
})
export class AppHeaderCenterComponent implements OnInit, AfterViewInit {
    UserNameTitle: string | null;


  constructor(private env: EnvService, private _sharedService: SharedService, private sanitizer: DomSanitizer) {
    
  }
  
  LoginUrl: string;
  HomeUrl: SafeUrl; 

  ngOnInit() {
    
    const helper = new JwtHelperService();
    var token = this.getCookie("token");
    const decodedToken = helper.decodeToken(token);

    this.UserNameTitle = decodedToken.UserDisplayName;

    this.HomeUrl = this.sanitizer.bypassSecurityTrustUrl(`http://${this._sharedService.getLandingAddress()}`);;
    }
    ngAfterViewInit() {
       
      
    }
    
    public toggleMenu() {
      $("#wrapper").toggleClass("toggled");
  }
  getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
  }

}
