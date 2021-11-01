import { MessageService } from '@progress/kendo-angular-l10n';

const messages = {
  'kendo.grid.noRecords': 'رکوردی برای نمایش وجود ندارد',
  'kendo.grid.pagerItemsPerPage': 'تعداد رکوردهای صفحه',
  'kendo.grid.pagerItems': 'مورد', 
  'kendo.grid.pagerOf': 'از' 


};

export class PersianMessageService extends MessageService {
  public get(key: string): string {
    return messages[key];
  }
}
