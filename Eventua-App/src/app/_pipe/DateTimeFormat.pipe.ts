import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Constant } from '../util/Constant';

@Pipe({
  name: 'DateTimeFormatPipe'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, Constant.DATE_TIME_FMT);
  }

}
