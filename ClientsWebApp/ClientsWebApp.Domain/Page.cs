namespace ClientsWebApp.Domain
{
    public class Page
    {
        public Page(int size, int number)
        {
            Number = number;
            Size = size;
        }

        public int Number { get; set; }
        public int Size { get; set; }

        public PageStatus GetPageStatus(int itemsCount)
        {
            if (itemsCount < Size && Number <= 1)
            {
                return PageStatus.OnlyPage;
            }
            if (Number <= 1)
            {
                return PageStatus.FirstPage;
            }
            if (itemsCount < Size)
            {
                return PageStatus.LastPage;
            }
            return PageStatus.MiddlePage;
        }
    }
}
