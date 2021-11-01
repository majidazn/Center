import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class MyTruncatePipe implements PipeTransform {
  transform(value: string, limit = 25, completeWords = false, ellipsis = '...') {
    if (value) {
      if (value.length < limit)
        return `${value.substr(0, limit)}`;
   

    if (completeWords) {
      limit = value.substr(0, limit).lastIndexOf(' ');
    }
      return `${value.substr(0, limit)}${ellipsis}`;
    }
  }
}
