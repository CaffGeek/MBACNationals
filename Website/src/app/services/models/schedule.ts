export class ScheduleDay {
    key: string;
    date: any; // TODO: CHAD:
    events: ScheduleEvent[];
}

export class ScheduleEvent {
    summary: string;
    location: string;
    description: string;
    start: string;
    end: string;
}

export class GoogleItemDto {
    summary: string;
    location: string;
    description: string;
    start: {
        date: string;
        dateTime: string;
    };
    end: {
        date: string;
        dateTime: string;
    };
}
