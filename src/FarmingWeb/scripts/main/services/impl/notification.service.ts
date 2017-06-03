import { Injectable, EventEmitter } from "@angular/core";
import { Message } from "primeng/primeng";

@Injectable()
export class NotificationService {
    public emitter: EventEmitter<Message> = new EventEmitter<Message>();
}