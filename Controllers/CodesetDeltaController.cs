using CMSAutomationAPI.Model;
using CMSAutomationAPI.Model.APIResponseBase;
using CMSAutomationAPI.Services;
using CMSAutomationAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CMSAutomationAPI.Controllers
{
    public class CodesetDeltaController : ControllerBase
    {
        public CptCodesetDeltaService _CptCodesetDeltaService;
        public IcdCodesetDeltaService _IcdCodesetDeltaService;

        public CodesetDeltaController(CptCodesetDeltaService cptCodesetDeltaService,
            IcdCodesetDeltaService icdCodesetDeltaService)
        {
            _CptCodesetDeltaService = cptCodesetDeltaService;
            _IcdCodesetDeltaService = icdCodesetDeltaService;
        }

        [HttpPost("cpt-policysubset-delta-index")]
        [Produces(typeof(ListModelResponse<CPTCodeSetDelta_ViewModel>))]
        public async Task<IActionResult> CPTCodesetIndex([FromBody] WorkBenchQueryParams queryParams)
        {
            //Common response model object for list of data
            var response = new ListModelResponse<CPTCodeSetDelta_ViewModel>() as
                IListModelResponse<CPTCodeSetDelta_ViewModel>;

            response.PageSize = (Int32)queryParams.PageSize;
            response.PageNumber = (Int32)queryParams.PageNumber;
            //populate data from the table

            var query = _CptCodesetDeltaService.GetAll(hcpc_codes: queryParams.HcpcCode).AsQueryable();

            response.TotalRecordCount = query.Count();

            var listdata = query.Skip((response.PageNumber - 1) * response.PageSize).
               Take(response.PageSize).ToList().Select(a => new CPTCodeSetDelta_ViewModel()
               {
                   CodesetId = a.CodesetId,
                   CodesetName = a.CodeSetName,
                   CptCode = a.CptCode,
                   Action = a.Action,
                   EffDate = a.EffDate,
                   TermDate = a.TermDate,
                   CreatedAt = a.CreatedAt,
                   Status = a.Status,
                   UpdatedAt = a.UpdatedAt

               }).ToList();

            response.Model = listdata;
            response.CurrentRecordCount = response.Model.Count();

            response.Message = $"Total  records: {response.Model.Count()}";
            return response.ToResponse();

        }

        [HttpPost("icd-policysubset-delta-index")]
        [Produces(typeof(ListModelResponse<ICDCodeSetDelta_ViewModel>))]
        public async Task<IActionResult> ICDCodesetIndex([FromBody] WorkBenchQueryParams queryParams)
        {
            //Common response model object for list of data
            var response = new ListModelResponse<ICDCodeSetDelta_ViewModel>() as
                IListModelResponse<ICDCodeSetDelta_ViewModel>;

            response.PageSize = (Int32)queryParams.PageSize;
            response.PageNumber = (Int32)queryParams.PageNumber;

            var query = _IcdCodesetDeltaService.GetAll(icd_codes: queryParams.IcdCode).AsQueryable();

            response.TotalRecordCount = query.Count();

            var listdata = query.Skip((response.PageNumber - 1) * response.PageSize).
               Take(response.PageSize).ToList().Select(a => new ICDCodeSetDelta_ViewModel()
               {
                   CodesetId = a.CodesetId,
                   CodesetName = a.CodeSetName,
                   IcdCode = a.IcdCode,
                   Action = a.Action,
                   EffDate = a.EffDate,
                   TermDate = a.TermDate,
                   CreatedAt = a.CreatedAt,
                   UpdatedAt = a.UpdatedAt
               }).ToList();

            response.Model = listdata;
            response.CurrentRecordCount = response.Model.Count();

            response.Message = $"Total  records: {response.Model.Count()}";
            return response.ToResponse();
        }


        [HttpPost("cpt-policysubset-delta-approval")]
        [Produces(typeof(ListModelResponse<CPTCodeSetDelta_ViewModel>))]
        public async Task<IActionResult> BulkCPTApprovalAsync(WorkBenchQueryParams queryParams)
        {

            var response = new ListModelResponse<CPTCodeSetDelta_ViewModel>() as
               IListModelResponse<CPTCodeSetDelta_ViewModel>;

            response.PageSize = (Int32)queryParams.PageSize;
            response.PageNumber = (Int32)queryParams.PageNumber;

            if (queryParams.HcpcCode == null || !queryParams.HcpcCode.Any())
                return response.ToResponse();

            var codes = string.Join(",", queryParams.HcpcCode.Select(x => $"'{x}'"));

            var sql = $@"
UPDATE cpt_codeset_delta
SET status = 'Approved',
updated_at = NOW()
WHERE cpt_code IN ({codes})";

            _CptCodesetDeltaService.ExecuteSqlAsync(sql);

            var query = _CptCodesetDeltaService.GetAll(hcpc_codes: queryParams.HcpcCode).AsQueryable();

            response.TotalRecordCount = query.Count();

            var listdata = query.Skip((response.PageNumber - 1) * response.PageSize).
               Take(response.PageSize).ToList().Select(a => new CPTCodeSetDelta_ViewModel()
               {
                   CodesetId = a.CodesetId,
                   CodesetName = a.CodeSetName,
                   CptCode = a.CptCode,
                   Action = a.Action,
                   EffDate = a.EffDate,
                   TermDate = a.TermDate,
                   CreatedAt = a.CreatedAt,
                   Status = a.Status,
                   UpdatedAt = a.UpdatedAt
               }).ToList();

            response.Model = listdata;
            response.CurrentRecordCount = response.Model.Count();

            response.Message = $"Total  records: {response.Model.Count()}";
            return response.ToResponse();
        }

        [HttpPost("icd-policysubset-delta-approval")]
        [Produces(typeof(ListModelResponse<ICDCodeSetDelta_ViewModel>))]
        public async Task<IActionResult> BulkICDApprovalAsync(WorkBenchQueryParams queryParams)
        {

            var response = new ListModelResponse<ICDCodeSetDelta_ViewModel>() as
             IListModelResponse<ICDCodeSetDelta_ViewModel>;

            response.PageSize = (Int32)queryParams.PageSize;
            response.PageNumber = (Int32)queryParams.PageNumber;

            if (queryParams.IcdCode == null || !queryParams.IcdCode.Any())
                return response.ToResponse();

            var codes = string.Join(",", queryParams.IcdCode.Select(x => $"'{x}'"));

            var sql = $@"
UPDATE icd_codeset_delta
SET status = 'Approved',
updated_at = NOW()
WHERE icd_code IN ({codes})";

            _IcdCodesetDeltaService.ExecuteSqlAsync(sql);

            var query = _IcdCodesetDeltaService.GetAll(icd_codes: queryParams.IcdCode).AsQueryable();

            response.TotalRecordCount = query.Count();

            var listdata = query.Skip((response.PageNumber - 1) * response.PageSize).
               Take(response.PageSize).ToList().Select(a => new ICDCodeSetDelta_ViewModel()
               {
                   CodesetId = a.CodesetId,
                   CodesetName = a.CodeSetName,
                   IcdCode = a.IcdCode,
                   Action = a.Action,
                   EffDate = a.EffDate,
                   TermDate = a.TermDate,
                   CreatedAt = a.CreatedAt,
                   Status = a.Status,
                   UpdatedAt   = a.UpdatedAt

               }).ToList();

            response.Model = listdata;
            response.CurrentRecordCount = response.Model.Count();

            response.Message = $"Total  records: {response.Model.Count()}";
            return response.ToResponse();


        }
    }
}
