using CMSAutomationAPI.Model.APIResponseBase;
using CMSAutomationAPI.Services;
using CMSAutomationAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSAutomationAPI.Controllers
{
    public class CodeMasterDeltaController : ControllerBase
    {

        public CptCodesMasterDeltaService _cptCodesMasterDeltaService;
        public CodeMasterDeltaController(CptCodesMasterDeltaService cptCodesMasterDeltaService)
        {
            _cptCodesMasterDeltaService = cptCodesMasterDeltaService;
        }

        [HttpPost("[action]")]
        [Produces(typeof(ListModelResponse<CPTMasterDelta_ViewModel>))]
        public async Task<IActionResult> CPTCodeMasterIndex([FromBody] WorkBenchQueryParams queryParams)
        {
            //Common response model object for list of data
            var response = new ListModelResponse<CPTMasterDelta_ViewModel>() as
                IListModelResponse<CPTMasterDelta_ViewModel>;

            response.PageSize = (Int32)queryParams.PageSize;
            response.PageNumber = (Int32)queryParams.PageNumber;
            //populate data from the table

            var query = _cptCodesMasterDeltaService.GetAll(hcpc_codes: queryParams.HcpcCode).AsQueryable();

            response.TotalRecordCount = query.Count();

            var listdata = query.Skip((response.PageNumber - 1) * response.PageSize).
               Take(response.PageSize).ToList().Select(a => new CPTMasterDelta_ViewModel()
               {
                   CptCode = a.CptCode,
                   Long_Desc = a.Long_Desc,
                   Medium_Desc = a.Medium_Desc,
                   Short_Desc = a.Short_Desc,
                   Code_Type = a.Code_Type,
                   Process_Type = a.Process_Type,
                   EffDate = a.EffDate,
                   TermDate = a.TermDate,
                   Status = a.status,
                   CreatedOn = a.CreatedOn,
                   UpdatedOn = a.UpdatedOn
               }).ToList();

            response.Model = listdata;
            response.CurrentRecordCount = response.Model.Count();

            response.Message = $"Total  records: {response.Model.Count()}";
            return response.ToResponse();

        }

        [HttpPost("[action]")]
        [Produces(typeof(ListModelResponse<CPTMasterDelta_ViewModel>))]
        public async Task<IActionResult> BulkCPTMasterApprovalAsync(WorkBenchQueryParams queryParams)
        {

            var response = new ListModelResponse<CPTMasterDelta_ViewModel>() as
               IListModelResponse<CPTMasterDelta_ViewModel>;

            response.PageSize = (Int32)queryParams.PageSize;
            response.PageNumber = (Int32)queryParams.PageNumber;

            if (queryParams.HcpcCode == null || !queryParams.HcpcCode.Any())
                return response.ToResponse();

            var codes = string.Join(",", queryParams.HcpcCode.Select(x => $"'{x}'"));

            var sql = $@"
UPDATE oce_delta
SET status = 'Approved',
updated_on = NOW()
WHERE code IN ({codes})";

            _cptCodesMasterDeltaService.ExecuteSqlAsync(sql);

            var query = _cptCodesMasterDeltaService.GetAll(hcpc_codes: queryParams.HcpcCode).AsQueryable();

            response.TotalRecordCount = query.Count();

            var listdata = query.Skip((response.PageNumber - 1) * response.PageSize).
               Take(response.PageSize).ToList().Select(a => new CPTMasterDelta_ViewModel()
               {
                   CptCode = a.CptCode,
                   Long_Desc = a.Long_Desc,
                   Medium_Desc = a.Medium_Desc,
                   Short_Desc = a.Short_Desc,
                   Code_Type = a.Code_Type,
                   Process_Type = a.Process_Type,
                   EffDate = a.EffDate,
                   TermDate = a.TermDate,
                   Status = a.status,
                   CreatedOn = a.CreatedOn,
                   UpdatedOn = a.UpdatedOn

               }).ToList();

            response.Model = listdata;
            response.CurrentRecordCount = response.Model.Count();

            response.Message = $"Total  records: {response.Model.Count()}";
            return response.ToResponse();
        }
    }
}
