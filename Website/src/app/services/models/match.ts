// TODO: CHAD: should probably be split into two classes... as this has duplicated, but differently named, elements
//              or should fix the output of the rest calls
export class Match {
    Id: string;
    MatchId: string;
    ParticipantId: string;
    Year: string;
    Name: string;
    Province: string;
    Opponent: string;
    Gender: string;
    Number: number;
    IsPOA: boolean;
    Scratch: number;
    Score: number;
    POA: number;
    Points: number;
    TotalPoints: number;
    WinLossTie: string;
}

export class MatchResult {
    Id: string;
    Division: string;
    IsPOA: boolean;
    Number: number;
    Away: MatchTeamResult;
    Home: MatchTeamResult;
    Lane: number;
    Centre: string;
    IsComplete: boolean;
}

export class MatchTeamResult {
    Id: string;
    Province: string;
    Bowlers: MatchBowlerResult[];
    Score: number;
    POA: number;
    Points: number;
    TotalPoints: number;
}

export class MatchBowlerResult {
    Id: string;
    Name: string;
    Position: number;
    Score: number;
    POA: number;
    Points: number;
}

export class TeamResult {
    Id: string;
    Name: string;
    Province: string;
    Scores: Match[];
    TotalGames: number;
    TotalWins: number;
    TotalLoss: number;
    TotalScratch: number;
    TotalOpponentScratch: number;
    TotalPOA: number;
    TotalOpponentPOA: number;
    AverageWinsPerGame: number;
    AverageLossPerGame: number;
    AverageScratch: number;
    AveragePOA: number;
    AverageOpponentScratch: number;
    AverageOpponentPOA: number;
}

export class BowlerResult {
    Id: string;
    Name: string;
    Province: string;
    Division: string;
    Average: number;
    NationalGames: number;
    NationalTotal: number;
    NationalTotalPOA: number;
    NationalAverage: number;
    NationalAveragePOA: number;
    OpponentTotal: number;
    OpponentTotalPOA: number;
    OpponentAverage: number;
    OpponentAveragePOA: number;
    NationalWins: number;
    HighScore: number;
    HighPOA: number;
    Scores: {
        MatchId: string;
        Number: number;
        Scratch: number;
        POA: number;
        WinLossTie: string;
        Lane: number;
        Centre: string;
        OpponentId: string;
        OpponentName: string;
        OpponentProvince: string;
        OpponentScratch: number;
        OpponentPOA: number;
        IsPOA: boolean;
    }[];
}
