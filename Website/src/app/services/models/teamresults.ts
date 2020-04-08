import { Match } from './match';

export class TeamResults {
    Id: string;
    ContingentId: string;
    TeamId: string;
    Division: string;
    Province: string;
    RunningPoints: number;
    Matches: Match[];
}
