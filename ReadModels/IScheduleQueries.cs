namespace MBACNationals.ReadModels
{
    public interface IScheduleQueries
    {
        ScheduleQueries.Schedule GetSchedule(string division);
    }
}
