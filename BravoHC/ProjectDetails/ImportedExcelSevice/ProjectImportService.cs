using Domain.IRepositories;
using MediatR;
using OfficeOpenXml;
using ProjectDetails.Commands.Request;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace ProjectDetails.ExcelImportService
{
    public class ProjectImportService
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _projectRepository;


        public ProjectImportService(
            IMediator mediator,
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            ISectionRepository sectionRepository)
        {
            _mediator = mediator;
            _projectRepository = projectRepository;
        }

        public async Task ImportAsync(Stream excelStream)
        {
            using var package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            var errors = new List<string>();
            var commandList = new List<CreateProjectCommandRequest>();

            for (int row = 2; row <= rowCount; row++)
            {
                // ProjectName -> ProjectId
                var projectCodeFromExcel = worksheet.Cells[row, 1].Text;
                var projectNameFromExcel = worksheet.Cells[row, 2].Text;
                bool? isStoreFromExcel = null;
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 3].Text))
                {
                    if (bool.TryParse(worksheet.Cells[row, 3].Text, out bool parsedIsStore))
                    {
                        isStoreFromExcel = parsedIsStore;
                    }
                    else
                    {
                        errors.Add($"Invalid boolean value for 'IsStore' at row {row}.");
                        continue;
                    }
                }

                bool? isHeadOfficeFromExcel = null;
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 4].Text))
                {
                    if (bool.TryParse(worksheet.Cells[row, 4].Text, out bool parsedIsHeadOffice))
                    {
                        isHeadOfficeFromExcel = parsedIsHeadOffice;
                    }
                    else
                    {
                        errors.Add($"Invalid boolean value for 'IsHeadOffice' at row {row}.");
                        continue;
                    }
                }

                bool? isActiveFromExcel = null;
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 5].Text))
                {
                    if (bool.TryParse(worksheet.Cells[row, 5].Text, out bool parsedIsActive))
                    {
                        isActiveFromExcel = parsedIsActive;
                    }
                    else
                    {
                        errors.Add($"Invalid boolean value for 'IsActive' at row {row}.");
                        continue;
                    }
                }
                var formatFromExcel = worksheet.Cells[row, 6].Text;
                var functionalAreaFromExcel = worksheet.Cells[row, 7].Text;
                var directorFromExcel = worksheet.Cells[row, 8].Text;
                var directorEmailFromExcel = worksheet.Cells[row, 9].Text;
                var areaManagerFromExcel = worksheet.Cells[row, 10].Text;
                var areaManagerBadgeFromExcel = worksheet.Cells[row, 11].Text;
                var areaManagerEmailFromExcel = worksheet.Cells[row, 12].Text;
                var storeManagerEmailFromExcel = worksheet.Cells[row, 13].Text;
                var recruiterFromExcel = worksheet.Cells[row, 14].Text;
                var recruiterEmailFromExcel = worksheet.Cells[row, 15].Text;
                var storeOpeningDateFromExcel = worksheet.Cells[row, 16].Text;
                var storeClosedDateFromExcel = worksheet.Cells[row, 17].Text;

                // Projeyi kontrol et (varsa hata ekle)
                var existingProject = await _projectRepository.GetByProjectCodeAsync(projectCodeFromExcel);
                if (existingProject != null)
                {
                    errors.Add($"Project with code '{projectCodeFromExcel}' already exists at row {row}.");
                    continue;
                }

                // Komut oluştur ve ekle
                var command = new CreateProjectCommandRequest
                {
                    ProjectCode = projectCodeFromExcel,
                    ProjectName = projectNameFromExcel,
                    IsStore = isStoreFromExcel,
                    IsHeadOffice = isHeadOfficeFromExcel,
                    IsActive = isActiveFromExcel,
                    Format = formatFromExcel,
                    FunctionalArea = functionalAreaFromExcel,
                    OperationDirector = directorFromExcel,
                    OperationDirectorMail = directorEmailFromExcel,
                    AreaManager = areaManagerFromExcel,
                    AreaManagerBadge = areaManagerBadgeFromExcel,
                    AreaManagerMail = areaManagerEmailFromExcel,
                    StoreManagerMail = storeManagerEmailFromExcel,
                    Recruiter = recruiterFromExcel,
                    RecruiterMail = recruiterEmailFromExcel,
                    StoreOpeningDate = storeOpeningDateFromExcel,
                    StoreClosedDate = storeClosedDateFromExcel
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
