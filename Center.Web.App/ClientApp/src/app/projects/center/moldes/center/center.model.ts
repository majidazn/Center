import { EAddressModel } from './eAddress.model';
import { TelecomModel } from './telecom.model';

export class CenterModel {
  centerId: number;
  name: string;
  enName: string;
  title: number;
  centerGroup: number;
  city: number;
  hostName: string;
  logo?: any;
  nationalCode: string;
  financhialCode: string;
  sepasCode: string;
  address: string;
  zipCode: string;
  tenantId: number;
  validFrom?: Date | string;
  validTo?: Date | string;
  electronicAddresses: EAddressModel[];
  telecoms: TelecomModel[];
}
