using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using OfficeOpenXml;
using HeadCountDetails.Commands.Request;
using Domain.IRepositories;

namespace HeadCountDetails.ExcelImportService
{
    public class HeadCountImportService
    {
        private readonly IMediator _mediator;
        private readonly IHeadCountRepository _headCountRepository;
        private readonly ISubSectionRepository _subSectionRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly ISectionRepository _sectionRepository;

        public HeadCountImportService(
            IMediator mediator,
            ISubSectionRepository subSectionRepository,
            IPositionRepository positionRepository,
            IEmployeeRepository employeeRepository,
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            ISectionRepository sectionRepository)
        {
            _mediator = mediator;
            _subSectionRepository = subSectionRepository;
            _positionRepository = positionRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _sectionRepository = sectionRepository;
        }

        public async Task ImportAsync(Stream excelStream)
        {
            using var package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            var errors = new List<string>();
            var commandList = new List<UpdateHeadCountCommandRequest>();

            for (int row = 2; row <= rowCount; row++)
            {
                // ProjectName -> ProjectId
                var projectNameFromExcel = worksheet.Cells[row, 2].Text;
                var projectId = await _projectRepository.GetIdByNameAsync(projectNameFromExcel);

                if (projectId == null)
                {
                    errors.Add($"Project '{projectNameFromExcel}' not found at row {row}.");
                    continue;
                }

                // FunctionalAreaName -> FunctionalAreaId
                var functionalAreaNameFromExcel = worksheet.Cells[row, 3].Text;
                var functionalAreaId = await _functionalAreaRepository.GetIdByNameAsync(functionalAreaNameFromExcel);

                if (functionalAreaId == null)
                {
                    errors.Add($"Functional Area '{functionalAreaNameFromExcel}' not found at row {row}.");
                    continue;
                }

                // Mevcut headcount verisini getir
                var headCountId = int.Parse(worksheet.Cells[row, 1].Text);
                var existingHeadCount = await _headCountRepository.GetByIdAsync(headCountId);

                if (existingHeadCount == null)
                {
                    errors.Add($"HeadCount with ID '{headCountId}' not found at row {row}.");
                    continue;
                }

                // ProjectName ve FunctionalAreaName karşılaştırması
                if (existingHeadCount.ProjectId != projectId.Value)
                {
                    errors.Add($"ProjectId mismatch for HeadCount ID {headCountId}. Database value: {existingHeadCount.ProjectId}, Excel value: {projectId.Value}");
                    continue;
                }

                if (existingHeadCount.FunctionalAreaId != functionalAreaId.Value)
                {
                    errors.Add($"FunctionalAreaId mismatch for HeadCount ID {headCountId}. Database value: {existingHeadCount.FunctionalAreaId}, Excel value: {functionalAreaId.Value}");
                    continue;
                }

                // SectionName -> SectionId
                var sectionNameFromExcel = worksheet.Cells[row, 4].Text;
                var sectionId = string.IsNullOrWhiteSpace(sectionNameFromExcel) ? (int?)null : await _sectionRepository.GetIdByNameAsync(sectionNameFromExcel);

                if (!string.IsNullOrWhiteSpace(sectionNameFromExcel) && sectionId == null)
                {
                    errors.Add($"Section '{sectionNameFromExcel}' not found at row {row}.");
                    continue;
                }

                // SubSectionName -> SubSectionId
                var subSectionNameFromExcel = worksheet.Cells[row, 5].Text;
                var subSectionId = string.IsNullOrWhiteSpace(subSectionNameFromExcel) ? (int?)null : await _subSectionRepository.GetIdByNameAsync(subSectionNameFromExcel);

                if (!string.IsNullOrWhiteSpace(subSectionNameFromExcel) && subSectionId == null)
                {
                    errors.Add($"SubSection '{subSectionNameFromExcel}' not found at row {row}.");
                    continue;
                }

                // PositionName -> PositionId
                var positionNameFromExcel = worksheet.Cells[row, 6].Text;
                var positionId = string.IsNullOrWhiteSpace(positionNameFromExcel) ? (int?)null : await _positionRepository.GetIdByNameAsync(positionNameFromExcel);

                if (!string.IsNullOrWhiteSpace(positionNameFromExcel) && positionId == null)
                {
                    errors.Add($"Position '{positionNameFromExcel}' not found at row {row}.");
                    continue;
                }

                // EmployeeName -> EmployeeId
                var employeeNameFromExcel = worksheet.Cells[row, 7].Text;
                var employeeId = string.IsNullOrWhiteSpace(employeeNameFromExcel) ? (int?)null : await _employeeRepository.GetIdByNameAsync(employeeNameFromExcel);

                if (!string.IsNullOrWhiteSpace(employeeNameFromExcel) && employeeId == null)
                {
                    errors.Add($"Employee '{employeeNameFromExcel}' not found at row {row}.");
                    continue;
                }

                // ParentId işlemi
                int? parentHeadCountId = null;
                if (employeeId.HasValue)
                {
                    var parentHeadCount = await _headCountRepository.GetAsync(d => d.EmployeeId == employeeId.Value);
                    if (parentHeadCount != null)
                    {
                        parentHeadCountId = parentHeadCount.Id;
                    }
                }

                // Karşılaştırma: Diğer alanlar (SubSectionId, PositionId, EmployeeId)
                if (existingHeadCount.SubSectionId != subSectionId ||
                    existingHeadCount.PositionId != positionId ||
                    existingHeadCount.EmployeeId != employeeId)
                {
                    errors.Add($"HeadCount ID {headCountId} has different values in Excel and database. No update performed.");
                    continue;
                }

                // Güncelleme işlemi
                var command = new UpdateHeadCountCommandRequest
                {
                    Id = headCountId,
                    ProjectId = existingHeadCount.ProjectId,
                    FunctionalAreaId = existingHeadCount.FunctionalAreaId,
                    SectionId = sectionId,
                    SubSectionId = subSectionId,
                    PositionId = positionId,
                    EmployeeId = employeeId,
                    HCNumber = existingHeadCount.HCNumber, // HCNumber güncellenmeyecek
                    IsVacant = bool.Parse(worksheet.Cells[row, 9].Text),
                    RecruiterComment = worksheet.Cells[row, 10].Text,
                    ParentId = parentHeadCountId // ParentId veritabanındaki değere göre ayarlanacak
                };

                commandList.Add(command);
            }

            // Hataları yönetme
            if (errors.Count > 0)
            {
                throw new Exception($"Validation errors: {string.Join(", ", errors)}");
            }

            // Komutları işleme
            foreach (var command in commandList)
            {
                await _mediator.Send(command);
            }
        }
    }
}
