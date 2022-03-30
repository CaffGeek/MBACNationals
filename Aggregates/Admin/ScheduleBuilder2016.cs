using Edument.CQRS;
using MBACNationals.ReadModels;
using MBACNationals.Scores.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals
{
    public static class ScheduleBuilder2016
    {
        const string year = "2016";

        public static Func<int, string, string, int, string, SaveMatch> MatchBuilder(ICommandQueries commandQueries, string division, BowlingCentre at, bool isPoa)
        {
            var tournament = commandQueries.GetTournaments().Single(x => x.Year == year);
            return (game, away, home, lane, slot) => 
            {
                var match = commandQueries.GetMatch(tournament.Year, division, game, slot)
                    ?? new CommandQueries.Match { Id = Guid.NewGuid() };

                return new SaveMatch(match.Id, tournament.Id, tournament.Year, division, game, away, home, lane, slot, at, isPoa);
            };
        }
        
        public static void TournamentMenSingle(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Tournament Men Single";
            var isPoa = false;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SO", "MB", lane = 17, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "NO", "BC", lane += 2, "C"), build(game, "SK", "NL", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 17, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "MB", "SK", lane += 2, "C"), build(game, "AB", "NO", lane += 2, "D"),
                build(++game, "NL", "NO", lane = 17, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "AB", "SO", lane += 2, "C"), build(game, "QC", "MB", lane += 2, "D"),
                build(++game, "AB", "SK", lane = 17, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "QC", "NL", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "MB", "BC", lane = 17, "A"), build(game, "AB", "NL", lane += 2, "B"), build(game, "SO", "NO", lane += 2, "C"), build(game, "QC", "SK", lane += 2, "D"),
                build(++game, "SK", "SO", lane = 17, "A"), build(game, "NO", "QC", lane += 2, "B"), build(game, "NL", "MB", lane += 2, "C"), build(game, "BC", "AB", lane += 2, "D"),
                build(++game, "AB", "MB", lane = 17, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "NO", "SK", lane += 2, "C"), build(game, "SO", "QC", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Cloverdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "NO", "NL", lane = 03, "A"), build(game, "BC", "SK", lane += 2, "B"), build(game, "SO", "AB", lane += 2, "C"), build(game, "MB", "QC", lane += 2, "D"),
                build(++game, "MB", "SO", lane = 03, "A"), build(game, "AB", "QC", lane += 2, "B"), build(game, "BC", "NO", lane += 2, "C"), build(game, "NL", "SK", lane += 2, "D"),
                build(++game, "QC", "BC", lane = 03, "A"), build(game, "SO", "NL", lane += 2, "B"), build(game, "SK", "MB", lane += 2, "C"), build(game, "NO", "AB", lane += 2, "D"),
                build(++game, "SK", "AB", lane = 03, "A"), build(game, "MB", "NO", lane += 2, "B"), build(game, "NL", "QC", lane += 2, "C"), build(game, "BC", "SO", lane += 2, "D"),
                build(++game, "QC", "SO", lane = 03, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "NO", "SK", lane += 2, "C"), build(game, "AB", "MB", lane += 2, "D"),
                build(++game, "MB", "BC", lane = 03, "A"), build(game, "QC", "SK", lane += 2, "B"), build(game, "AB", "NL", lane += 2, "C"), build(game, "SO", "NO", lane += 2, "D"),
                build(++game, "NO", "QC", lane = 03, "A"), build(game, "BC", "AB", lane += 2, "B"), build(game, "SK", "SO", lane += 2, "C"), build(game, "MB", "NL", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "SK", lane = 13, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "QC", "NL", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "NL", "NO", lane = 13, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "AB", "SO", lane += 2, "C"), build(game, "QC", "MB", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 13, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "MB", "SK", lane += 2, "C"), build(game, "AB", "NO", lane += 2, "D"),
                build(++game, "SO", "MB", lane = 13, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "NO", "BC", lane += 2, "C"), build(game, "SK", "NL", lane += 2, "D"),
                build(++game, "AB", "BC", lane = 13, "A"), build(game, "MB", "NL", lane += 2, "B"), build(game, "SO", "SK", lane += 2, "C"), build(game, "QC", "NO", lane += 2, "D"),
                build(++game, "SK", "QC", lane = 13, "A"), build(game, "NO", "SO", lane += 2, "B"), build(game, "BC", "MB", lane += 2, "C"), build(game, "NL", "AB", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 13, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "SO", "QC", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }

        public static void TournamentLadiesSingle(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Tournament Ladies Single";
            var isPoa = false;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SO", "MB", lane = 17, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "NO", "BC", lane += 2, "C"), build(game, "SK", "NL", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 17, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "MB", "SK", lane += 2, "C"), build(game, "AB", "NO", lane += 2, "D"),
                build(++game, "NL", "NO", lane = 17, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "AB", "SO", lane += 2, "C"), build(game, "QC", "MB", lane += 2, "D"),
                build(++game, "AB", "SK", lane = 17, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "QC", "NL", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "MB", "BC", lane = 17, "A"), build(game, "AB", "NL", lane += 2, "B"), build(game, "SO", "NO", lane += 2, "C"), build(game, "QC", "SK", lane += 2, "D"),
                build(++game, "SK", "SO", lane = 17, "A"), build(game, "NO", "QC", lane += 2, "B"), build(game, "NL", "MB", lane += 2, "C"), build(game, "BC", "AB", lane += 2, "D"),
                build(++game, "AB", "MB", lane = 17, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "NO", "SK", lane += 2, "C"), build(game, "SO", "QC", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Cloverdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "NO", "NL", lane = 03, "A"), build(game, "BC", "SK", lane += 2, "B"), build(game, "SO", "AB", lane += 2, "C"), build(game, "MB", "QC", lane += 2, "D"),
                build(++game, "MB", "SO", lane = 03, "A"), build(game, "AB", "QC", lane += 2, "B"), build(game, "BC", "NO", lane += 2, "C"), build(game, "NL", "SK", lane += 2, "D"),
                build(++game, "QC", "BC", lane = 03, "A"), build(game, "SO", "NL", lane += 2, "B"), build(game, "SK", "MB", lane += 2, "C"), build(game, "NO", "AB", lane += 2, "D"),
                build(++game, "SK", "AB", lane = 03, "A"), build(game, "MB", "NO", lane += 2, "B"), build(game, "NL", "QC", lane += 2, "C"), build(game, "BC", "SO", lane += 2, "D"),
                build(++game, "QC", "SO", lane = 03, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "NO", "SK", lane += 2, "C"), build(game, "AB", "MB", lane += 2, "D"),
                build(++game, "MB", "BC", lane = 03, "A"), build(game, "QC", "SK", lane += 2, "B"), build(game, "AB", "NL", lane += 2, "C"), build(game, "SO", "NO", lane += 2, "D"),
                build(++game, "NO", "QC", lane = 03, "A"), build(game, "BC", "AB", lane += 2, "B"), build(game, "SK", "SO", lane += 2, "C"), build(game, "MB", "NL", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "SK", lane = 13, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "QC", "NL", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "NL", "NO", lane = 13, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "AB", "SO", lane += 2, "C"), build(game, "QC", "MB", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 13, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "MB", "SK", lane += 2, "C"), build(game, "AB", "NO", lane += 2, "D"),
                build(++game, "SO", "MB", lane = 13, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "NO", "BC", lane += 2, "C"), build(game, "SK", "NL", lane += 2, "D"),
                build(++game, "AB", "BC", lane = 13, "A"), build(game, "MB", "NL", lane += 2, "B"), build(game, "SO", "SK", lane += 2, "C"), build(game, "QC", "NO", lane += 2, "D"),
                build(++game, "SK", "QC", lane = 13, "A"), build(game, "NO", "SO", lane += 2, "B"), build(game, "BC", "MB", lane += 2, "C"), build(game, "NL", "AB", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 13, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "SO", "QC", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }

        public static void TournamentMen(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Tournament Men";
            var isPoa = false;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SK", "NO", lane = 03, "A"), build(game, "NL", "AB", lane += 2, "B"), build(game, "MB", "QC", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "QC", "NL", lane = 03, "A"), build(game, "NO", "BC", lane += 2, "B"), build(game, "SK", "SO", lane += 2, "C"), build(game, "AB", "MB", lane += 2, "D"),
                build(++game, "BC", "MB", lane = 03, "A"), build(game, "SO", "QC", lane += 2, "B"), build(game, "NO", "AB", lane += 2, "C"), build(game, "NL", "SK", lane += 2, "D"),
                build(++game, "AB", "SO", lane = 03, "A"), build(game, "MB", "SK", lane += 2, "B"), build(game, "BC", "NL", lane += 2, "C"), build(game, "QC", "NO", lane += 2, "D"),
                build(++game, "QC", "SK", lane = 03, "A"), build(game, "BC", "AB", lane += 2, "B"), build(game, "MB", "NO", lane += 2, "C"), build(game, "SO", "NL", lane += 2, "D"),
                build(++game, "NO", "SO", lane = 03, "A"), build(game, "NL", "MB", lane += 2, "B"), build(game, "AB", "QC", lane += 2, "C"), build(game, "SK", "BC", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SK", "AB", lane = 01, "A"), build(game, "QC", "BC", lane += 2, "B"), build(game, "SO", "MB", lane += 2, "C"), build(game, "NO", "NL", lane += 2, "D"),
                build(++game, "QC", "SO", lane = 01, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "SK", "NL", lane += 2, "C"), build(game, "MB", "BC", lane += 2, "D"),
                build(++game, "AB", "NL", lane = 01, "A"), build(game, "MB", "QC", lane += 2, "B"), build(game, "BC", "SO", lane += 2, "C"), build(game, "NO", "SK", lane += 2, "D"),
                build(++game, "SK", "MB", lane = 01, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "NO", "QC", lane += 2, "C"), build(game, "AB", "SO", lane += 2, "D"),
                build(++game, "BC", "NO", lane = 01, "A"), build(game, "SO", "SK", lane += 2, "B"), build(game, "MB", "AB", lane += 2, "C"), build(game, "NL", "QC", lane += 2, "D"),
                build(++game, "NL", "MB", lane = 01, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "SO", "NO", lane += 2, "C"), build(game, "BC", "SK", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Cloverdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SK", "QC", lane = 03, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "AB", "BC", lane += 2, "C"), build(game, "NO", "MB", lane += 2, "D"),
                build(++game, "NO", "NL", lane = 03, "A"), build(game, "SK", "AB", lane += 2, "B"), build(game, "MB", "SO", lane += 2, "C"), build(game, "BC", "QC", lane += 2, "D"),
                build(++game, "MB", "SK", lane = 03, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "QC", "NO", lane += 2, "C"), build(game, "SO", "AB", lane += 2, "D"),
                build(++game, "SO", "QC", lane = 03, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "NL", "SK", lane += 2, "C"), build(game, "BC", "MB", lane += 2, "D"),
                build(++game, "BC", "NO", lane = 03, "A"), build(game, "SK", "SO", lane += 2, "B"), build(game, "MB", "AB", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "AB", "NL", lane = 03, "A"), build(game, "QC", "MB", lane += 2, "B"), build(game, "SO", "BC", lane += 2, "C"), build(game, "NO", "SK", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "SK", lane = 15, "A"), build(game, "NL", "NO", lane += 2, "B"), build(game, "QC", "BC", lane += 2, "C"), build(game, "SO", "MB", lane += 2, "D"),
                build(++game, "MB", "NL", lane = 15, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "NO", "SO", lane += 2, "C"), build(game, "AB", "QC", lane += 2, "D"),
                build(++game, "QC", "SK", lane = 15, "A"), build(game, "MB", "NO", lane += 2, "B"), build(game, "BC", "AB", lane += 2, "C"), build(game, "SO", "NL", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }
        
        public static void TournamentLadies(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Tournament Ladies";
            var isPoa = false;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SK", "NO", lane = 09, "A"), build(game, "NL", "AB", lane += 2, "B"), build(game, "MB", "QC", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "QC", "NL", lane = 09, "A"), build(game, "NO", "BC", lane += 2, "B"), build(game, "SK", "SO", lane += 2, "C"), build(game, "AB", "MB", lane += 2, "D"),
                build(++game, "BC", "MB", lane = 09, "A"), build(game, "SO", "QC", lane += 2, "B"), build(game, "NO", "AB", lane += 2, "C"), build(game, "NL", "SK", lane += 2, "D"),
                build(++game, "AB", "SO", lane = 09, "A"), build(game, "MB", "SK", lane += 2, "B"), build(game, "BC", "NL", lane += 2, "C"), build(game, "QC", "NO", lane += 2, "D"),
                build(++game, "QC", "SK", lane = 09, "A"), build(game, "BC", "AB", lane += 2, "B"), build(game, "MB", "NO", lane += 2, "C"), build(game, "SO", "NL", lane += 2, "D"),
                build(++game, "NO", "SO", lane = 09, "A"), build(game, "NL", "MB", lane += 2, "B"), build(game, "AB", "QC", lane += 2, "C"), build(game, "SK", "BC", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SK", "AB", lane = 13, "A"), build(game, "QC", "BC", lane += 2, "B"), build(game, "SO", "MB", lane += 2, "C"), build(game, "NO", "NL", lane += 2, "D"),
                build(++game, "QC", "SO", lane = 13, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "SK", "NL", lane += 2, "C"), build(game, "MB", "BC", lane += 2, "D"),
                build(++game, "AB", "NL", lane = 13, "A"), build(game, "MB", "QC", lane += 2, "B"), build(game, "BC", "SO", lane += 2, "C"), build(game, "NO", "SK", lane += 2, "D"),
                build(++game, "SK", "MB", lane = 13, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "NO", "QC", lane += 2, "C"), build(game, "AB", "SO", lane += 2, "D"),
                build(++game, "BC", "NO", lane = 13, "A"), build(game, "SO", "SK", lane += 2, "B"), build(game, "MB", "AB", lane += 2, "C"), build(game, "NL", "QC", lane += 2, "D"),
                build(++game, "NL", "MB", lane = 13, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "SO", "NO", lane += 2, "C"), build(game, "BC", "SK", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SK", "QC", lane = 17, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "AB", "BC", lane += 2, "C"), build(game, "NO", "MB", lane += 2, "D"),
                build(++game, "NO", "NL", lane = 17, "A"), build(game, "SK", "AB", lane += 2, "B"), build(game, "MB", "SO", lane += 2, "C"), build(game, "BC", "QC", lane += 2, "D"),
                build(++game, "MB", "SK", lane = 17, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "QC", "NO", lane += 2, "C"), build(game, "SO", "AB", lane += 2, "D"),
                build(++game, "SO", "QC", lane = 17, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "NL", "SK", lane += 2, "C"), build(game, "BC", "MB", lane += 2, "D"),
                build(++game, "BC", "NO", lane = 17, "A"), build(game, "SK", "SO", lane += 2, "B"), build(game, "MB", "AB", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "AB", "NL", lane = 17, "A"), build(game, "QC", "MB", lane += 2, "B"), build(game, "SO", "BC", lane += 2, "C"), build(game, "NO", "SK", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "SK", lane = 03, "A"), build(game, "NL", "NO", lane += 2, "B"), build(game, "QC", "BC", lane += 2, "C"), build(game, "SO", "MB", lane += 2, "D"),
                build(++game, "MB", "NL", lane = 03, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "NO", "SO", lane += 2, "C"), build(game, "AB", "QC", lane += 2, "D"),
                build(++game, "QC", "SK", lane = 03, "A"), build(game, "MB", "NO", lane += 2, "B"), build(game, "BC", "AB", lane += 2, "C"), build(game, "SO", "NL", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }

        public static void TeachingMen(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Teaching Men";
            var isPoa = true;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "NO", "QC", lane = 01, "A"), build(game, "SO", "SK", lane += 2, "B"), build(game, "MB", "NL", lane += 2, "C"), build(game, "AB", "BC", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 01, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "QC", "SO", lane += 2, "D"),
                build(++game, "NL", "SO", lane = 01, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "BC", "QC", lane += 2, "C"), build(game, "SK", "MB", lane += 2, "D"),
                build(++game, "BC", "SK", lane = 01, "A"), build(game, "QC", "MB", lane += 2, "B"), build(game, "SO", "AB", lane += 2, "C"), build(game, "NO", "NL", lane += 2, "D"),
                build(++game, "AB", "QC", lane = 01, "A"), build(game, "SK", "NL", lane += 2, "B"), build(game, "NO", "BC", lane += 2, "C"), build(game, "MB", "SO", lane += 2, "D"),
                build(++game, "SO", "NO", lane = 01, "A"), build(game, "BC", "MB", lane += 2, "B"), build(game, "NL", "AB", lane += 2, "C"), build(game, "QC", "SK", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "BC", "SO", lane = 03, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "SK", "AB", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "NL", "NO", lane = 03, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "MB", "QC", lane += 2, "C"), build(game, "AB", "SO", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 03, "A"), build(game, "SO", "QC", lane += 2, "B"), build(game, "BC", "NL", lane += 2, "C"), build(game, "NO", "SK", lane += 2, "D"),
                build(++game, "SK", "QC", lane = 03, "A"), build(game, "AB", "NL", lane += 2, "B"), build(game, "NO", "SO", lane += 2, "C"), build(game, "MB", "BC", lane += 2, "D"),
                build(++game, "NL", "MB", lane = 03, "A"), build(game, "QC", "NO", lane += 2, "B"), build(game, "AB", "BC", lane += 2, "C"), build(game, "SO", "SK", lane += 2, "D"),
                build(++game, "BC", "NO", lane = 03, "A"), build(game, "SO", "MB", lane += 2, "B"), build(game, "NL", "SK", lane += 2, "C"), build(game, "QC", "AB", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SO", "AB", lane = 09, "A"), build(game, "NL", "NO", lane += 2, "B"), build(game, "BC", "SK", lane += 2, "C"), build(game, "QC", "MB", lane += 2, "D"),
                build(++game, "MB", "SK", lane = 09, "A"), build(game, "QC", "BC", lane += 2, "B"), build(game, "SO", "NL", lane += 2, "C"), build(game, "NO", "AB", lane += 2, "D"),
                build(++game, "NL", "QC", lane = 09, "A"), build(game, "AB", "SK", lane += 2, "B"), build(game, "MB", "NO", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "NO", "BC", lane = 09, "A"), build(game, "MB", "SO", lane += 2, "B"), build(game, "AB", "QC", lane += 2, "C"), build(game, "SK", "NL", lane += 2, "D"),
                build(++game, "AB", "MB", lane = 09, "A"), build(game, "BC", "NL", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "QC", "SO", lane += 2, "D"),
                build(++game, "SK", "SO", lane = 09, "A"), build(game, "NO", "QC", lane += 2, "B"), build(game, "NL", "MB", lane += 2, "C"), build(game, "BC", "AB", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "NL", "AB", lane = 13, "A"), build(game, "MB", "BC", lane += 2, "B"), build(game, "QC", "SK", lane += 2, "C"), build(game, "SO", "NO", lane += 2, "D"),
                build(++game, "NO", "MB", lane = 13, "A"), build(game, "SK", "AB", lane += 2, "B"), build(game, "BC", "SO", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 13, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "AB", "NO", lane += 2, "C"), build(game, "SK", "MB", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }

        public static void TeachingLadies(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Teaching Ladies";
            var isPoa = true;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Cloverdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "NO", "QC", lane = 03, "A"), build(game, "SO", "SK", lane += 2, "B"), build(game, "MB", "NL", lane += 2, "C"), build(game, "AB", "BC", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 03, "A"), build(game, "NL", "BC", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "QC", "SO", lane += 2, "D"),
                build(++game, "NL", "SO", lane = 03, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "BC", "QC", lane += 2, "C"), build(game, "SK", "MB", lane += 2, "D"),
                build(++game, "BC", "SK", lane = 03, "A"), build(game, "QC", "MB", lane += 2, "B"), build(game, "SO", "AB", lane += 2, "C"), build(game, "NO", "NL", lane += 2, "D"),
                build(++game, "AB", "QC", lane = 03, "A"), build(game, "SK", "NL", lane += 2, "B"), build(game, "NO", "BC", lane += 2, "C"), build(game, "MB", "SO", lane += 2, "D"),
                build(++game, "SO", "NO", lane = 03, "A"), build(game, "BC", "MB", lane += 2, "B"), build(game, "NL", "AB", lane += 2, "C"), build(game, "QC", "SK", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "BC", "SO", lane = 17, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "SK", "AB", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "NL", "NO", lane = 17, "A"), build(game, "SK", "BC", lane += 2, "B"), build(game, "MB", "QC", lane += 2, "C"), build(game, "AB", "SO", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 17, "A"), build(game, "SO", "QC", lane += 2, "B"), build(game, "BC", "NL", lane += 2, "C"), build(game, "NO", "SK", lane += 2, "D"),
                build(++game, "SK", "QC", lane = 17, "A"), build(game, "AB", "NL", lane += 2, "B"), build(game, "NO", "SO", lane += 2, "C"), build(game, "MB", "BC", lane += 2, "D"),
                build(++game, "NL", "MB", lane = 17, "A"), build(game, "QC", "NO", lane += 2, "B"), build(game, "AB", "BC", lane += 2, "C"), build(game, "SO", "SK", lane += 2, "D"),
                build(++game, "BC", "NO", lane = 17, "A"), build(game, "SO", "MB", lane += 2, "B"), build(game, "NL", "SK", lane += 2, "C"), build(game, "QC", "AB", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SO", "AB", lane = 03, "A"), build(game, "NL", "NO", lane += 2, "B"), build(game, "BC", "SK", lane += 2, "C"), build(game, "QC", "MB", lane += 2, "D"),
                build(++game, "MB", "SK", lane = 03, "A"), build(game, "QC", "BC", lane += 2, "B"), build(game, "SO", "NL", lane += 2, "C"), build(game, "NO", "AB", lane += 2, "D"),
                build(++game, "NL", "QC", lane = 03, "A"), build(game, "AB", "SK", lane += 2, "B"), build(game, "MB", "NO", lane += 2, "C"), build(game, "SO", "BC", lane += 2, "D"),
                build(++game, "NO", "BC", lane = 03, "A"), build(game, "MB", "SO", lane += 2, "B"), build(game, "AB", "QC", lane += 2, "C"), build(game, "SK", "NL", lane += 2, "D"),
                build(++game, "AB", "MB", lane = 03, "A"), build(game, "BC", "NL", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "QC", "SO", lane += 2, "D"),
                build(++game, "SK", "SO", lane = 03, "A"), build(game, "NO", "QC", lane += 2, "B"), build(game, "NL", "MB", lane += 2, "C"), build(game, "BC", "AB", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "NL", "AB", lane = 03, "A"), build(game, "MB", "BC", lane += 2, "B"), build(game, "QC", "SK", lane += 2, "C"), build(game, "SO", "NO", lane += 2, "D"),
                build(++game, "NO", "MB", lane = 03, "A"), build(game, "SK", "AB", lane += 2, "B"), build(game, "BC", "SO", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 03, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "AB", "NO", lane += 2, "C"), build(game, "SK", "MB", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }

        public static void Seniors(ICommandQueries commandQueries, MessageDispatcher dispatcher)
        {
            var division = "Seniors";
            var isPoa = true;
            var game = 0;
            var lane = 0;
            Func<int, string, string, int, string, SaveMatch> build;
            var commands = new List<SaveMatch>();

            build = MatchBuilder(commandQueries, division, BowlingCentre.Willowbrook, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "SK", lane = 13, "A"), build(game, "BC", "SO", lane += 2, "B"), build(game, "QC", "MB", lane += 2, "C"), build(game, "NO", "NL", lane += 2, "D"),
                build(++game, "MB", "BC", lane = 13, "A"), build(game, "SK", "NL", lane += 2, "B"), build(game, "AB", "NO", lane += 2, "C"), build(game, "SO", "QC", lane += 2, "D"),
                build(++game, "NL", "QC", lane = 13, "A"), build(game, "NO", "MB", lane += 2, "B"), build(game, "SK", "SO", lane += 2, "C"), build(game, "BC", "AB", lane += 2, "D"),
                build(++game, "SO", "NO", lane = 13, "A"), build(game, "QC", "AB", lane += 2, "B"), build(game, "NL", "BC", lane += 2, "C"), build(game, "MB", "SK", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 13, "A"), build(game, "NL", "SO", lane += 2, "B"), build(game, "QC", "SK", lane += 2, "C"), build(game, "NO", "BC", lane += 2, "D"),
                build(++game, "SK", "NO", lane = 13, "A"), build(game, "BC", "QC", lane += 2, "B"), build(game, "SO", "MB", lane += 2, "C"), build(game, "AB", "NL", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "SO", lane = 09, "A"), build(game, "MB", "NL", lane += 2, "B"), build(game, "NO", "QC", lane += 2, "C"), build(game, "SK", "BC", lane += 2, "D"),
                build(++game, "MB", "NO", lane = 09, "A"), build(game, "SO", "SK", lane += 2, "B"), build(game, "AB", "BC", lane += 2, "C"), build(game, "QC", "NL", lane += 2, "D"),
                build(++game, "SO", "BC", lane = 09, "A"), build(game, "QC", "MB", lane += 2, "B"), build(game, "NL", "NO", lane += 2, "C"), build(game, "SK", "AB", lane += 2, "D"),
                build(++game, "AB", "QC", lane = 09, "A"), build(game, "BC", "NL", lane += 2, "B"), build(game, "SK", "MB", lane += 2, "C"), build(game, "SO", "NO", lane += 2, "D"),
                build(++game, "NL", "SK", lane = 09, "A"), build(game, "NO", "AB", lane += 2, "B"), build(game, "QC", "SO", lane += 2, "C"), build(game, "BC", "MB", lane += 2, "D"),
                build(++game, "BC", "QC", lane = 09, "A"), build(game, "MB", "SO", lane += 2, "B"), build(game, "NO", "SK", lane += 2, "C"), build(game, "NL", "AB", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Scottsdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "AB", "MB", lane = 01, "A"), build(game, "BC", "NO", lane += 2, "B"), build(game, "SO", "NL", lane += 2, "C"), build(game, "SK", "QC", lane += 2, "D"),
                build(++game, "SK", "BC", lane = 01, "A"), build(game, "AB", "SO", lane += 2, "B"), build(game, "QC", "NO", lane += 2, "C"), build(game, "NL", "MB", lane += 2, "D"),
                build(++game, "QC", "AB", lane = 01, "A"), build(game, "BC", "NL", lane += 2, "B"), build(game, "MB", "SK", lane += 2, "C"), build(game, "NO", "SO", lane += 2, "D"),
                build(++game, "NO", "MB", lane = 01, "A"), build(game, "SO", "SK", lane += 2, "B"), build(game, "BC", "AB", lane += 2, "C"), build(game, "NL", "QC", lane += 2, "D"),
                build(++game, "NL", "SK", lane = 01, "A"), build(game, "AB", "NO", lane += 2, "B"), build(game, "QC", "SO", lane += 2, "C"), build(game, "MB", "BC", lane += 2, "D"),
                build(++game, "SO", "BC", lane = 01, "A"), build(game, "MB", "QC", lane += 2, "B"), build(game, "NO", "NL", lane += 2, "C"), build(game, "SK", "AB", lane += 2, "D"),
            });

            build = MatchBuilder(commandQueries, division, BowlingCentre.Cloverdale, isPoa);
            commands.AddRange(new List<SaveMatch>
            {
                build(++game, "SO", "AB", lane = 03, "A"), build(game, "BC", "SK", lane += 2, "B"), build(game, "MB", "NL", lane += 2, "C"), build(game, "NO", "QC", lane += 2, "D"),
                build(++game, "QC", "BC", lane = 03, "A"), build(game, "AB", "NL", lane += 2, "B"), build(game, "SK", "NO", lane += 2, "C"), build(game, "SO", "MB", lane += 2, "D"),
                build(++game, "MB", "AB", lane = 03, "A"), build(game, "QC", "SK", lane += 2, "B"), build(game, "NL", "SO", lane += 2, "C"), build(game, "NO", "BC", lane += 2, "D"),
            });

            foreach (var command in commands)
                try
                {
                    dispatcher.SendCommand(command);
                }
                catch (MBACNationals.Scores.MatchAlreadyCreated e)
                {
                }
        }
    }
}
