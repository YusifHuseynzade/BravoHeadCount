using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Commands.Request
{
    public class UpdateEmployeeLocationCommandRequest : IRequest<UpdateEmployeeLocationCommandResponse>
    {
        public int EmployeeId { get; set; }
        public int? ResidentalAreaId { get; set; }
        public int? BakuDistrictId { get; set; }
        public int? BakuMetroId { get; set; }
        public int? BakuTargetId { get; set; }
    }
}
