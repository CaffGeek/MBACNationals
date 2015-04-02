namespace MBACNationals.ReadModels
{
    public interface IScheduleQueries
    {
        ScheduleQueries.Schedule GetSchedule(int year, string division);
    }
}
