import { Injectable, EventEmitter } from '@angular/core';
import { FullAccessRoles } from '../../../models/fullAccess-roles.enum';

@Injectable()
export class PermissionService {
    private _permissionStore: Array<string> = [];
    private _permissionStoreChange: EventEmitter<any> = new EventEmitter();
    constructor() {

    }

    public define(permissions: Array<string>): void {
        if (!Array.isArray(permissions)) {
            throw new Error('permissions parameter is not array.');
        }

        this.clearStore();
        for (const permission of permissions) {
            this.add(permission);
        }
    }

    public add(permission: string): void {
        if (typeof permission === 'string' && !this.hasDefined(permission.toLocaleLowerCase())) {
            this._permissionStore.push(permission.toLocaleLowerCase());
            this._permissionStoreChange.emit(null);
      }
      console.log(this._permissionStore);
    }

    public remove(permission: string): void {
        if (typeof permission !== 'string') {
            return;
        }

        const index = this._permissionStore.indexOf(permission.toLowerCase());
        if (index < 0) {
            return;
        }

        this._permissionStore.splice(index, 1);
        this._permissionStoreChange.emit(null);
    }

    public hasDefined(permission: string): boolean {
        if (typeof permission !== 'string') {
            return false;
        }

        const index = this._permissionStore.indexOf(permission.toLowerCase());
        return index > -1;
    }

    public hasOneDefined(permissions: Array<string>): boolean {
        if (!Array.isArray(permissions)) {
            throw new Error('permissions parameter is not array.');
        }

        return permissions.some(v => {
          if (typeof v === 'string') {
            debugger

                return this._permissionStore.indexOf(v.toLowerCase()) >= 0;
            }
        });
    }

    public hasOneDefinedWithFullAccess(permissions: Array<string>): boolean {
        if (!Array.isArray(permissions)) {
            throw new Error('permissions parameter is not array.');
        }

        return permissions.some(v => {

            if (typeof v === 'string') {
                return this._permissionStore.indexOf(v.toLowerCase()) >= 0;
            }
        });
    }

  public clearStore(): void {
    debugger
        this._permissionStore = [];
    }

    get store(): Array<string> {
        return this._permissionStore;
    }

    get permissionStoreChangeEmitter(): EventEmitter<any> {
        return this._permissionStoreChange;
    }
}
