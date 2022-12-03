namespace TwentyOne.Day6
{
    public class DaySet
    {
        public DaySet(int numberOfFish, int days)
        {
            NumberOfFish = numberOfFish;
            DaysLeft = days;
        }

        public int NumberOfFish { get; set; }

        public int DaysLeft { get; set; }
    }
}
