namespace Infrastructure.Specifications
{
    public class ProductSpecifications
    {
        public int? BrandId{ get; set; }
        public int? TypeId{ get; set; }
        public string? Sort { get; set; }
        private const int  PageMaxSize = 50;
        public int PagIndex { get; set; } = 1;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > PageMaxSize ? PageMaxSize : value);
        }
        private string _search = "";

        public string Search
        {
            get { return _search; }
            set { _search = value.Trim().ToLower(); }
        }


    }
}
