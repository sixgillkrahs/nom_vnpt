using System;
using System.Threading.Tasks;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Data;
using CTIN.Abp.OpenIddict.Applications;
using CSP_NET.Work.LogWorks.Dtos;
using CSP_NET.Work.LogWorks.IServices;
using CTIN.Abp.ObjectMapping;
namespace CSP_NET.Work.LogWorks;


public class LogWorkAppService : CrudAppService<LogWork,LogWorkDto, Guid, LogWorkGetListInput, CreateUpdateLogWorkDto>, ILogWorkAppService
{

    private readonly IDataFilter _dataFilter;
    private readonly ILogWorkRepository _repository;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;

    public LogWorkAppService(ILogWorkRepository repository, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository) : base(repository)
    {
        _dataFilter = dataFilter;
        _repository = repository;
        _openIddictApplicationRepository = openIddictApplicationRepository;
    }

    public override async Task<LogWorkDto> GetAsync(Guid id)
    {
        var logWork = await _repository.FindByIdAsync(id);
        return ObjectMapper.Map<LogWork, LogWorkDto>(logWork);

    }
    public override async Task<LogWorkDto> CreateAsync(CreateUpdateLogWorkDto input)
    {
        var LogWork = await _repository.InsertAsync(new LogWork()
        {
            WorkId = input.WorkId,
            UserId = input.UserId,
            Time = input.Time,
            Date = input.Date,
            Description = input.Description,
            StatusLogWork = input.StatusLogWork
          
        });

        return ObjectMapper.Map<LogWork, LogWorkDto>(LogWork);
    }
    public async override Task<LogWorkDto> UpdateAsync(Guid id, CreateUpdateLogWorkDto input)
    {
        var LogWork = await _repository.FindByIdAsync(id);
        ObjectMapper.Map(input, LogWork);
        await _repository.UpdateAsync(LogWork);
        return ObjectMapper.Map<LogWork, LogWorkDto>(LogWork);
    }
    public async Task<LogWorkDto> FindByWorkIdAsync(Guid WorkId)
    {
        var logWork = await _repository.FindByIdAsync(WorkId);
        return ObjectMapper.Map<LogWork, LogWorkDto>(logWork);
    }
    public async override Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
