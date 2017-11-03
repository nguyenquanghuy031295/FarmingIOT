import { Component, AfterViewInit, NgZone} from '@angular/core';
import { Message } from 'primeng/primeng';

import { NotificationService } from './../main/services/impl/notification.service';
//AppComponent is used for boostrap to browser. This is root component.
@Component({
    selector: 'my-app',
    template: `
    <div>
        <p-growl [value]="msgs"></p-growl>
    </div>
    <router-outlet></router-outlet>`
})
export class AppComponent implements AfterViewInit {
    public msgs: Message[] = [];

    //constuctor
    constructor(
        private notificationService: NotificationService,
        private zone: NgZone
    ) { }

    //function will be called after view of component rendered
    ngAfterViewInit() {
        let that = this;
        this.notificationService.emitter.subscribe((next: Message) => {
            that.zone.run(() => that.msgs.push(next));
        });
    }
}