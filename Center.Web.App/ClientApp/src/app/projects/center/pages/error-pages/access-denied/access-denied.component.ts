import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { SharedService } from '../../../../../services/shared.service';

@Component({
  selector: 'access-denied',
  templateUrl: './access-denied.component.html',
  styleUrls: ['./access-denied.component.scss']
})
export class AccessDeniedComponent implements OnInit {
  landingUrl: SafeUrl;
  constructor(private _sharedService: SharedService, private sanitizer: DomSanitizer) {
    this.landingUrl = this.sanitizer.bypassSecurityTrustUrl(`http://${this._sharedService.getLandingAddress()}`);
  }

  ngOnInit(): void {
  }
}
