﻿using MediatR;
using VacationScheduleDetails.Commands.Response;

namespace VacationScheduleDetails.Commands.Request;

public class CreateVacationScheduleCommandRequest : IRequest<CreateVacationScheduleCommandResponse>
{
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}