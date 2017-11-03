import { Injectable, EventEmitter } from "@angular/core";
import { Message } from "primeng/primeng";

//this service is used for showing notifications
@Injectable()
export class NotificationService {
    public emitter: EventEmitter<Message> = new EventEmitter<Message>();
}