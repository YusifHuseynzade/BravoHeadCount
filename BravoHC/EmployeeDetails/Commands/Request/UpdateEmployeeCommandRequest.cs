﻿using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Commands.Request;

public class UpdateEmployeeCommandRequest : IRequest<UpdateEmployeeCommandResponse>
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Badge { get; set; }
    public string? Director { get; set; }
    public string? Recruiter { get; set; }
    public string? AreaManager { get; set; }
    public string? StoreManager { get; set; }
    public int FunctionalAreaId { get; set; }
    public int ProjectId { get; set; }
    public int PositionId { get; set; }
    public int SectionId { get; set; }
    public int? SubSectionId { get; set; }
}
