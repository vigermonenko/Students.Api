namespace Students.Core.Common
{
    public class Paging
    {
        public int Offset { get; set; } = 0;

        public int Size { get; set; } = 50;

        public int Page => Offset / Size;
    }
}