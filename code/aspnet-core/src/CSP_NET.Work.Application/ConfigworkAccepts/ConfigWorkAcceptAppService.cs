using System;
using System.Threading.Tasks;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Data;
using CTIN.Abp.OpenIddict.Applications;
using CSP_NET.Work.ConfigworkAccepts.Dtos;
using CSP_NET.Work.ConfigworkAccepts.IServices;
using CTIN.Abp.ObjectMapping;
namespace CSP_NET.Work.ConfigworkAccepts;


public class ConfigWorkAcceptAppService : CrudAppService<ConfigWorkAccept, ConfigWorkAcceptDto, Guid,ConfigWorkAcceptGetListInput, CreateUpdateConfigWorkAcceptDto>, IConfigWorkAcceptAppService
{

    private readonly IDataFilter _dataFilter;
    private readonly IConfigWorkAcceptRepository _repository;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;

    public ConfigWorkAcceptAppService(IConfigWorkAcceptRepository repository, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository) : base(repository)
    {
        _dataFilter = dataFilter;
        _repository = repository;
        _openIddictApplicationRepository = openIddictApplicationRepository;
    }

    public override async Task<ConfigWorkAcceptDto> GetAsync(Guid id)
    {
        var config = await _repository.FindByIdAsync(id);
        return ObjectMapper.Map<ConfigWorkAccept, ConfigWorkAcceptDto>(config);

    }
    public override async Task<ConfigWorkAcceptDto> CreateAsync(CreateUpdateConfigWorkAcceptDto input)
    {
        var ConfigworkAccept = await _repository.InsertAsync(new ConfigWorkAccept()
        {
            WorkId = input.WorkID,
            UserAccept1 = input.UserAccept1,
            DateAccept1 = DateTime.Now,
            UserAccept2 = input.UserAccept2,
            DateAccept2 = DateTime.Now,
            StatusAccept1 = input.StatusAccept1,
            StatusAccept2 = input.StatusAccept2,
            Accept2 = input.Accept2
        });

        return ObjectMapper.Map<ConfigWorkAccept, ConfigWorkAcceptDto>(ConfigworkAccept);
    }
    public async override Task<ConfigWorkAcceptDto> UpdateAsync(Guid id, CreateUpdateConfigWorkAcceptDto input)
    {
        var ConfigworkAccept = await _repository.FindByIdAsync(id);
        ObjectMapper.Map(input, ConfigworkAccept);
        await _repository.UpdateAsync(ConfigworkAccept);
        return ObjectMapper.Map<ConfigWorkAccept, ConfigWorkAcceptDto>(ConfigworkAccept);
    }
    public async Task<ConfigWorkAcceptDto> FindByWorkIdAsync(Guid WorkId)
    {
        var config = await _repository.FindByIdAsync(WorkId);
        return ObjectMapper.Map<ConfigWorkAccept, ConfigWorkAcceptDto>(config);
    }
    public async override Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
