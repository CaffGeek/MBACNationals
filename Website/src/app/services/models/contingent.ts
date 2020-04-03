export class Contingent {
    Id: string;
    Tournament: string;
    Province: string;
    Teams: Team[];
    Guests: Participant[];

    static allParticipants(contingent: Contingent): Participant[] {
        return [
            ...contingent.Teams.filter(x => x.Coach).map(x => x.Coach),
            ...contingent.Teams.flatMap(x => x.Bowlers),
            ...contingent.Guests,
        ];
    }

    static getManager(contingent: Contingent): Participant {
        return Contingent.allParticipants(contingent)
            .find(x => x.IsManager) || new Participant();
    }

    static getDelegates(contingent: Contingent): Participant[] {
        return Contingent.allParticipants(contingent)
            .filter(x => x.IsDelegate) || new Array<Participant>();
    }
}

export class Team {
    Id: string;
    Name: string;
    Contingent: string;
    Bowlers: Participant[];
    CoachId: string;
    Coach: Participant;
    Alternate: string;
    Gender: string;
    SizeLimit: number;
    RequiresShirtSize: boolean;
    RequiresCoach: boolean;
    RequiresAverage: boolean;
    RequiresBio: boolean;
    RequiresGender: boolean;
    IncludesSeniorRep: string;

    static getAverage(team: Team): number {
        return team.Bowlers
            .filter(x => !x.ReplacedBy)
            .map(x => x.Average)
            .reduce((sum, x) => sum + x, 0);
    }
}

export class Participant {
    Id: string;
    ContingentId: string;
    TeamId: string;
    Name: string;
    IsRookie: boolean;
    IsDelegate: boolean;
    IsManager: boolean;
    IsGuest: boolean;
    Average: number;
    ReplacedBy?: string;
    Birthday?: string;
    QualifyingPosition: number;
}
