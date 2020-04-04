import { Tournament } from 'src/app/services/models/tournament';
import { Centre } from 'src/app/services/models/centre';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LaneDrawService } from 'src/app/services/lane-draw.service';
import { LaneDraw, Game } from 'src/app/services/models/lane-draw';
import { TournamentsService } from 'src/app/services/tournaments.service';

@Component({
  selector: 'app-lane-draw-page',
  templateUrl: './lane-draw-page.component.html',
  styleUrls: ['./lane-draw-page.component.scss']
})
export class LaneDrawPageComponent implements OnInit {
  division: string;
  tournament: Tournament;
  groupedDraw: any[];

  provinceColumns: any[];
  displayedColumns: string[];

  constructor(
    private route: ActivatedRoute,
    private tournamentsService: TournamentsService,
    private laneDrawService: LaneDrawService,
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(({ year, division }) => {
      this.division = division;

      this.tournamentsService.getTournament(year)
        .subscribe(tournament => {
          this.tournament = tournament;

          this.provinceColumns = tournament.Contingents.map(x =>  x.ProvinceCode);
              // header: x.ProvinceCode,

              // cell: (element: Game) => {
              //   const match = element[x.ProvinceCode];
              //   return (x.ProvinceCode === match.Home)
              //     ? `vs ${match.Away} \n on ${match.Lane + 1}`
              //     : `@ ${match.Home} \n on ${match.Lane}`;
              // },
          //   }))
          // ];

          this.displayedColumns = [ 'number', ...this.provinceColumns];

          this.laneDrawService.getLaneDraw(year, division)
            .subscribe(draw => {
              const gameNumbers = draw.Games
                .map(x => x.Number)
                .reduce((acc, curr) => {
                  return acc.includes(curr) ? acc : [ ...acc, curr];
                }, []);

              const provinceCodes =  tournament.Contingents.map(x => x.ProvinceCode);
              const byCentre = gameNumbers.reduce((acc, n) => {
                const currentGames = draw.Games.filter(g => g.Number === n);
                const matches = provinceCodes.reduce((macc, pc) => ({
                  ...macc,
                  [pc]: currentGames.find(g => g.Home === pc || g.Away === pc),
                }), {});

                const game = {
                  Number: n,
                  ...matches,
                };

                const lastCentre = acc.length ? acc.slice(-1)[0] : { };
                if (lastCentre.Name === currentGames[0].CentreName) {
                  return [
                    ...acc.slice(0, -1),
                    { ...lastCentre, Games: [ ...lastCentre.Games, game ] },
                  ];
                } else {
                  return [
                    ...acc,
                    { Name: currentGames[0].CentreName, Games: [ game ]},
                  ];
                }
              }, []);

              this.groupedDraw = byCentre;
            });
        });
    });
  }
}
