import {
    Pipe,
    PipeTransform,
} from '@angular/core';
import * as momment_ from 'moment';
const moment: any = (<any>momment_)['default'] || momment_;

@Pipe({ name: 'dateformat' })
export class DateFormatPipe implements PipeTransform {
    transform(value: string, formatDate?: string): any {
        if (!value) return '';
        return moment(moment(value)
            .toDate())
            .format(formatDate ? formatDate : 'DD-MM-YYYY HH:mm:ss');
    }
}
