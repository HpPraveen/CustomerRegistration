using CustomerRegistration.Models.DTO;

namespace CustomerRegistration.Pagination
{
    public static class PaginationHelper
    {
        public static ResponseDto CreatePagedReponse<T>(List<T> data, int pageNumber, int pageSize, int totalRecords, string? enpointUri)
        {
            ResponseDto respose = new();
            respose.Result = data;
            respose.PageNumber = pageNumber;
            respose.PageSize = pageSize;
            var totalPages = totalRecords / (double)pageSize;
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            respose.NextPageNumber = pageNumber >= 1 && pageNumber < roundedTotalPages ? pageNumber + 1 : pageNumber;
            respose.PreviousPageNumber = pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages ? pageNumber - 1 : pageNumber;
            respose.FirstPageNumber = 1;
            respose.LastPageNumber = roundedTotalPages;

            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;

            if (enpointUri != null)
            {
                respose.NextPageUri = pageNumber >= 1 && pageNumber < roundedTotalPages
                    ? GetPageUri(pageNumber + 1, pageSize, enpointUri) : null;

                respose.PreviousPageUri = pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages
                    ? GetPageUri(pageNumber - 1, pageSize, enpointUri) : null;

                respose.FirstPageUri = GetPageUri(1, pageSize, enpointUri);

                respose.LastPageUri = GetPageUri(roundedTotalPages, pageSize, enpointUri);
            }

            return respose;
        }

        public static Uri GetPageUri(int pageNumber, int pageSize, string enpointUri)
        {
            var modifiedUri = enpointUri + "/" + pageNumber + "/" + pageSize;
            return new Uri(modifiedUri);
        }
    }
}
