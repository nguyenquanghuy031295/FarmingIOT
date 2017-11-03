import {
    Pipe,
    PipeTransform,
} from '@angular/core';
import * as momment_ from 'moment';
const moment: any = (<any>momment_)['default'] || momment_;
//this pipe is used for change date format of data we get from Server
@Pipe({ name: 'dateformat' })
export class DateFormatPipe implements PipeTransform {
    transform(value: string, formatDate?: string): any {
        if (!value) return '';
        return moment(moment(value)
            .toDate())
            .format(formatDate ? formatDate : 'DD-MM-YYYY HH:mm:ss');
    }
}
