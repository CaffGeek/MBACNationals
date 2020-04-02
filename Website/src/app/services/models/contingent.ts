export class Contingent {
    Id: string;
    Tournament: string;
    Province: string;
    Teams: Team[];
    Guests: Participant[];
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
