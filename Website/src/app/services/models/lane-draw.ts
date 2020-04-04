export class LaneDraw {
    Division: string;
    Games: Game[];
}

export class Game {
    Id: string;
    Division: string;
    IsPOA: boolean;
    Number: number;
    Away: string;
    Home: string;
    Lane: number;
    Centre: number;
    CentreName: string;
    IsComplete: boolean;
}
