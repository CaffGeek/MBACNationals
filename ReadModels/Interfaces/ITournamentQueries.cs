using MBACNationals.ReadModels;
using System;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface ITournamentQueries
    {
        TournamentQueries.Tournament GetTournament(string year);
        List<TournamentQueries.Tournament> GetTournaments();
        List<TournamentQueries.Sponsor> GetSponsors(string year);
        byte[] GetSponsorImage(Guid sponsorId);
        List<TournamentQueries.News> GetNews(string year);
    }
}
