namespace CMSAutomationAPI.Model.APIResponseBase
{


    public interface IListModelResponse<TModel> : IResponse
    {
        Int32 PageSize { get; set; }
        Int32 PageNumber { get; set; }
        int TotalRecordCount { get; set; }
        int CurrentRecordCount { get; set; }
        IEnumerable<TModel> Model { get; set; }
        TModel SubModel { get; set; }
        List<KeyValuePair<string, int>> KeyValues { get; set; }
    }


    public class ListModelResponse<TModel> : IListModelResponse<TModel>
    {
        public String Message { get; set; }

        public Boolean DidError { get; set; }

        public String ErrorMessage { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 PageNumber { get; set; }

        public IEnumerable<TModel> Model { get; set; }
        public TModel SubModel { get; set; }

        public int TotalRecordCount { get; set; }

        public int CurrentRecordCount { get; set; }
        public int? PdflistCount { get; set; }

        public List<KeyValuePair<string, int>> KeyValues { get; set; }

    }
}
