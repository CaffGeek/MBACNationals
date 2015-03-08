using Edument.CQRS;
using MBACNationals.ReadModels;
using MBACNationals.Participant;
using MBACNationals.Contingent;
using System.IO;
using System.Web;
using MBACNationals.Scores;
using MBACNationals;

namespace WebFrontend
{
    public static class Domain
    {
        public static bool IsRebuilding { get; private set; }
        public static string ReadModelFolder { get; private set; }

        public static MessageDispatcher Dispatcher;
        public static IParticipantQueries ParticipantQueries;
        public static IParticipantProfileQueries ParticipantProfileQueries;
        public static IContingentQueries ContingentQueries;
        public static IContingentViewQueries ContingentViewQueries;
        public static IContingentTravelPlanQueries ContingentTravelPlanQueries;
        public static IContingentPracticePlanQueries ContingentPracticePlanQueries;
        public static IContingentEventHistoryQueries ContingentEventHistoryQueries;
        public static IReservationQueries ReservationQueries;
        public static IScheduleQueries ScheduleQueries;
        public static IMatchQueries MatchQueries;
        public static IStandingQueries StandingQueries;
        public static IHighScoreQueries HighScoreQueries;
        public static IParticipantScoreQueries ParticipantScoreQueries;
        public static ITeamScoreQueries TeamScoreQueries;

        public static void Setup()
        {
            ReadModelFolder = HttpContext.Current.Server.MapPath("~/App_Data/ReadModels/");
            
            Dispatcher = new MessageDispatcher(new SqlEventStore(Properties.Settings.Default.DefaultConnection));

            Dispatcher.ScanInstance(new ParticipantCommandHandlers(Dispatcher));
            Dispatcher.ScanInstance(new ContingentCommandHandlers());
            Dispatcher.ScanInstance(new ScoresCommandHandlers(Dispatcher));

            ParticipantQueries = new ParticipantQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ParticipantQueries);

            ParticipantProfileQueries = new ParticipantProfileQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ParticipantProfileQueries);

            ContingentQueries = new ContingentQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ContingentQueries);

            ContingentViewQueries = new ContingentViewQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ContingentViewQueries);

            ContingentTravelPlanQueries = new ContingentTravelPlanQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ContingentTravelPlanQueries);

            ContingentPracticePlanQueries = new ContingentPracticePlanQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ContingentPracticePlanQueries);

            ContingentEventHistoryQueries = new ContingentEventHistoryQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ContingentEventHistoryQueries);

            ReservationQueries = new ReservationQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ReservationQueries);

            ScheduleQueries = new ScheduleQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ScheduleQueries);

            MatchQueries = new MatchQueries(ReadModelFolder);
            Dispatcher.ScanInstance(MatchQueries);

            StandingQueries = new StandingQueries(ReadModelFolder);
            Dispatcher.ScanInstance(StandingQueries);

            HighScoreQueries = new HighScoreQueries(ReadModelFolder);
            Dispatcher.ScanInstance(HighScoreQueries);

            ParticipantScoreQueries = new ParticipantScoreQueries(ReadModelFolder);
            Dispatcher.ScanInstance(ParticipantScoreQueries);

            TeamScoreQueries = new TeamScoreQueries(ReadModelFolder);
            Dispatcher.ScanInstance(TeamScoreQueries);

            if (!Directory.Exists(ReadModelFolder))
            {
                RebuildReadModels();
            }
        }
        
        public static void RebuildReadModels()
        {
            GoOffline();

            if (Directory.Exists(ReadModelFolder))
                Directory.Delete(ReadModelFolder, true);

            RebuildSchedule();
            Dispatcher.RepublishEvents();

            GoOnline();
        }

        public static void RebuildReadModel(string readmodel)
        {
            GoOffline();

            var readModel = Path.Combine(ReadModelFolder, readmodel);
            if (File.Exists(readModel))
                File.Delete(readModel);

            Dispatcher.RepublishEvents(readmodel);

            GoOnline();
        }

        public static void RebuildSchedule()
        {
            ScheduleBuilder.TournamentMenSingle(Domain.Dispatcher);
            ScheduleBuilder.TournamentLadiesSingle(Domain.Dispatcher);
            ScheduleBuilder.TournamentLadies(Domain.Dispatcher);
            ScheduleBuilder.TournamentMen(Domain.Dispatcher);
            ScheduleBuilder.TeachingLadies(Domain.Dispatcher);
            ScheduleBuilder.TeachingMen(Domain.Dispatcher);
            ScheduleBuilder.Seniors(Domain.Dispatcher);
        }

        private static void GoOffline()
        {
            var bakFile = HttpContext.Current.Server.MapPath("~/app_offline.bak");
            var htmFile = HttpContext.Current.Server.MapPath("~/app_offline.htm");
            File.Copy(bakFile, htmFile, true);
        }

        private static void GoOnline()
        {
            var htmFile = HttpContext.Current.Server.MapPath("~/app_offline.htm");
            File.Delete(htmFile);
        }
    }
}