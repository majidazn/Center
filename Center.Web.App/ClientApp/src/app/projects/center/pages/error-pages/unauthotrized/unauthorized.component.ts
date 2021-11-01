import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { SharedService } from '../../../../../services/shared.service';

@Component({
  selector: 'unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.scss']
})
export class UnauthorizedComponent implements OnInit {
  isTokenInvalid: boolean;
  landingUrl: SafeUrl;
  constructor(private _sharedService: SharedService, private sanitizer: DomSanitizer) {
     
    if (!this._sharedService.getToken()) {
      this.isTokenInvalid = true;
    }
    else if (!this._sharedService.getLandingAddress()) {
      this.isTokenInvalid = true;
    }
    else {
      this.isTokenInvalid = false;
      this.landingUrl = this.sanitizer.bypassSecurityTrustUrl(`http://${this._sharedService.getLandingAddress()}`);
    }
  }

  ngOnInit(): void {

  }
}
