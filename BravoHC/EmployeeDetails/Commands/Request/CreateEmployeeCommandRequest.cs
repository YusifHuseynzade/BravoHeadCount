﻿using EmployeeDetails.Commands.Response;
using MediatR;


namespace EmployeeDetails.Commands.Request;

public class CreateEmployeeCommandRequest : IRequest<CreateEmployeeCommandResponse>
{
    public string FullName { get; set; }
    public string Badge { get; set; }
    public string FIN { get; set; }
    public string PhoneNumber { get; set; }
    public int? ResidentalAreaId { get; set; }
    public int FunctionalAreaId { get; set; }
    public int ProjectId { get; set; }
    public int PositionId { get; set; }
    public int SectionId { get; set; }
    public int? SubSectionId { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? ContractEndDate { get; set; }
}
