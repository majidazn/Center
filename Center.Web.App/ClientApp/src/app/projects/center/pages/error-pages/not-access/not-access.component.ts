import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { SharedService } from '../../../../../services/shared.service';

@Component({
  selector: 'app-not-access',
  templateUrl: './not-access.component.html',
  styleUrls: ['./not-access.component.scss']
})
export class NotAccessComponent implements OnInit {
  landingUrl: SafeUrl;
  constructor(private _sharedService: SharedService, private sanitizer: DomSanitizer) {
    this.landingUrl = this.sanitizer.bypassSecurityTrustUrl(`http://${this._sharedService.getLandingAddress()}`);
  }

  ngOnInit(): void {
  }
}
