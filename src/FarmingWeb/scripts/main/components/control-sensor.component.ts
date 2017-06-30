import { Component, OnDestroy } from '@angular/core';
import * as mqtt from 'mqtt';
@Component({
    selector: 'control-sensor',
    templateUrl: './templates/main/components/control-sensor.component.html'
})
export class ControlSensorComponent implements OnDestroy {
    private client: mqtt.Client;
    private topic: string = "kwf/demo/led";
    constructor() {
        const options: mqtt.IClientOptions = {
            host: "broker.hivemq.com",
            port: 8000,
            keepalive: 60,
            reconnectPeriod: 10000,
            clientId: "clientId-Farming-" + Math.floor(Math.random() * 65535),
            path: "/mqtt"
        };
        this.client = mqtt.connect(options);
    }

    onPublish(message: string) {
        if (this.client.connected) {
            this.client.publish(this.topic, message);
        } else {
            console.log("Not Connect!");
        }
    }

    ngOnDestroy() {
        if (this.client)
            this.client.end(true);
    }
}