import { Component, AfterViewInit, NgZone} from '@angular/core';
import { Message } from 'primeng/primeng';

import { NotificationService } from './../main/services/impl/notification.service';
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
    constructor(
        private notificationService: NotificationService,
        private zone: NgZone
    ) { }

    ngAfterViewInit() {
        let that = this;
        this.notificationService.emitter.subscribe((next: Message) => {
            that.zone.run(() => that.msgs.push(next));
        });
    }
}