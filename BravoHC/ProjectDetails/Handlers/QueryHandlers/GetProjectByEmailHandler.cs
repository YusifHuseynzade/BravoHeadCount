using AutoMapper;
using Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Handlers.QueryHandlers
{
    public class GetProjectByEmailHandler : IRequestHandler<GetProjectByEmailRequest, GetProjectByEmailResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectByEmailHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProjectByEmailResponse> Handle(GetProjectByEmailRequest request, CancellationToken cancellationToken)
        {
            // E-posta adresine göre projeyi bul
            var project = await _context.Projects
                .Include(p => p.ProjectSections)
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p =>
                    p.StoreManagerMail == request.Email ||
                    p.StoreUserMail == request.Email, cancellationToken);

            if (project == null)
            {
                return new GetProjectByEmailResponse
                {
                    IsSuccess = false,
                    Message = "No project found associated with this email."
                };
            }

            // Kullanıcının hangi tip mail adresi olduğunu belirle
            string settingType = project.StoreManagerMail == request.Email ? "StoreManagerMail" : "StoreUserMail";

            // Projeye bağlı SettingFinanceOperation kayıtlarını getir
            var financeOperations = await _context.SettingFinanceOperations
                .Where(s => s.ProjectId == project.Id)
                .ToListAsync(cancellationToken);

            var financeOperationResponses = _mapper.Map<List<FinanceOperationResponse>>(financeOperations);
            // Genel ayarları getir
            var generalSettings = await _context.GeneralSettings.FirstOrDefaultAsync();

            return new GetProjectByEmailResponse
            {
                IsSuccess = true,
                Message = "Project found successfully.",
                ProjectCode = project.ProjectCode,
                SettingType = settingType,
                GeneralSettings = generalSettings,
                FinanceOperations = financeOperationResponses
            };
        }
    }
}
