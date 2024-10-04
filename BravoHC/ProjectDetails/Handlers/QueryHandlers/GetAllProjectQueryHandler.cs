using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectDetails.Handlers.QueryHandlers
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQueryRequest, List<GetAllProjectListQueryResponse>>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllProjectQueryHandler(IProjectRepository repository, IMapper mapper, IAppUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetAllProjectListQueryResponse>> Handle(GetAllProjectQueryRequest request, CancellationToken cancellationToken)
        {
            // Oturum açan kullanıcının email bilgisi
            var userEmail = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("Kullanıcı emaili bulunamadı.");
            }

            // Kullanıcı bilgilerini veritabanından alıyoruz
            var loggedInUser = await _userRepository.GetLoggedInUserAsync(userEmail);

            // Eğer kullanıcının rolü 'Store Management' ya da 'Recruiter' ise projeleri filtreliyoruz
            if (loggedInUser.Role.RoleName == "Store Management" || loggedInUser.Role.RoleName == "Recruiter")
            {
                var projects = _repository.GetAll(x =>
                    x.OperationDirectorMail == userEmail ||
                    x.AreaManagerMail == userEmail ||
                    x.StoreManagerMail == userEmail ||
                    x.RecruiterMail == userEmail).ToList();

                // Projeleri response'a map ediyoruz
                var response = _mapper.Map<List<GetAllProjectQueryResponse>>(projects);

                // Sayfalama işlemi
                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = projects.Count();

                // Sonuçları döndürüyoruz
                return new List<GetAllProjectListQueryResponse>
                {
                    new GetAllProjectListQueryResponse
                    {
                        TotalProjectCount = totalCount,
                        Projects = response
                    }
                };
            }
            else
            {
                // Eğer kullanıcının rolü yukarıdaki rollere dahil değilse tüm projeleri getiriyoruz
                var allProjects = _repository.GetAll(x => true).ToList();

                // Projeleri response'a map ediyoruz
                var response = _mapper.Map<List<GetAllProjectQueryResponse>>(allProjects);

                // Sayfalama işlemi
                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = allProjects.Count();

                // Sonuçları döndürüyoruz
                return new List<GetAllProjectListQueryResponse>
                {
                    new GetAllProjectListQueryResponse
                    {
                        TotalProjectCount = totalCount,
                        Projects = response
                    }
                };
            }
        }
    }
}
