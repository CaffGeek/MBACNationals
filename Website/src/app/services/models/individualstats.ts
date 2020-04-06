import { Match } from './match';

export class IndividualStats {
    ParticipantId: string;
    Gender: string;
    Name: string;
    Total: number;
    Average: number;
    Wins: number;
    Scores: Match[];
}
