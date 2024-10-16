using MediatR;
using Microsoft.AspNetCore.Http;
using PositionDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Commands.Request
{
    public class UpdatePositionFileCommandRequest : IRequest<UpdatePositionFileCommandResponse>
    {
        public int Id { get; set; }
        public IFormFile JobDescriptionFile { get; set; }
    }
}
