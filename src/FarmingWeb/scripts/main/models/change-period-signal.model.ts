export enum SignalPeriod {
    IsAvailable = 0,
    IsLastPeroid = 1,
    IsNotEnoughDay = 2,
    IsNotAvailable = 3
}

export class ChangePeriodSignal {
    public Signal: SignalPeriod;
}