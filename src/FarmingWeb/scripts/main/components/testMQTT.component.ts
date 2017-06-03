import { Component, OnDestroy } from '@angular/core';
import * as mqtt from 'mqtt';
@Component({
    selector: 'testMQTT',
    template: `
    <p>test MQTT</p>
    <button (click)="onPublish()">Publish</button>
`
})
export class TestMQTTComponent implements OnDestroy {
    private client: mqtt.Client;
    constructor() {
        const options: mqtt.IClientOptions = {
            host: "broker.hivemq.com",
            port: 8000,
            keepalive: 60,
            reconnectPeriod: 10000,
            clientId: "clientId-Farming-" + Math.floor(Math.random() * 65535),
            username: "huy",
            password: "huy",
            path: "/mqtt"
        };
        this.client = mqtt.connect(options);
    }

    onPublish() {
        if (this.client.connected) {
            this.client.publish("kwf/demo/led", "led1off");
            console.log("Connected");
        }
        else
            console.log("Not Connect!");
    }

    ngOnDestroy() {
        this.client.end(true);
    }
}