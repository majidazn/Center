export interface QueryModel {

  customFilterMetDataDescriptors: any[];
  skip: number;
  pageSize: number;

}


export class CustomFilterMetDataDescriptor {

  Member: string;
  Name: string;
  Value: string;
  MemberType: string;
  Operator: string;

}
