import { Component, OnDestroy } from '@angular/core';
import * as mqtt from 'mqtt';
//This Component is stand for buttons of controlling actuators
@Component({
    selector: 'control-sensor',
    templateUrl: './templates/main/components/control-sensor.component.html'
})
export class ControlSensorComponent implements OnDestroy {
    private client: mqtt.Client;
    private topic: string = "kwf/demo/led";
    //constructor
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

    //Publish a message to Broker
    onPublish(message: string) {
        if (this.client.connected) {
            this.client.publish(this.topic, message);
        } else {
            console.log("Not Connect!");
        }
    }

    //This function will be called when user get out of this page (mean change page)
    ngOnDestroy() {
        if (this.client)
            this.client.end(true);
    }
}