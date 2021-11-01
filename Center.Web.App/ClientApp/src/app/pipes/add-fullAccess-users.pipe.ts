import { Pipe, PipeTransform } from '@angular/core';
import { SharedService } from '../services/shared.service';

@Pipe({ name: 'addFullAccessUsers' })
export class AddFullAccessUsersPipe implements PipeTransform {
  fullAccessRoles: string[] = [];

  constructor(private _sharedService: SharedService) {
    this.fullAccessRoles = this._sharedService.fullAccessRoles;
  }

  transform(value: string[], exponent?: number): string[] {
    debugger
    const x = this.fullAccessRoles.concat(value);
    return x;
  }
}
