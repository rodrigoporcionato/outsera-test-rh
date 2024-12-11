namespace GoldenRaspberryAwards.Api.Entities
{
    public class ProducerInterval
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }

    public class ProducersWithIntervalsResult
    {
        public List<ProducerInterval> Min { get; set; }
        public List<ProducerInterval> Max { get; set; }
    }
}
