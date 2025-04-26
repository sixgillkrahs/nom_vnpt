using System;

namespace CSP_NET.Work.ProjectState.Dtos;

[Serializable]
public class CreateUpdateProjectStateDto
{
    public string Code { get; set; }

    public string Name { get; set; }
}
