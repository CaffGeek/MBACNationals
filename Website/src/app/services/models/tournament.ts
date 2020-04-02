export class Tournament {
    Year: number;
    Theme: string;
    TimeZone: string;
    Welcome: string;
    Schedule: {
        Url: string;
        Download: string;
    };
    Logo: string;
    Header: string;
    Banners: {
        default: string;
        results: string;
        statistics: string;
        news: string;
        schedule: string;
        lanedraw: string;
        contingents: string;
        souvenirs: string;
        sponsors: string;
        centres: string;
        location: string;
        history: string;
    };
}
