using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Entities;

namespace EmployeeDetails.ExcelImportService
{
    public class EmployeeImportService
    {
        private readonly IResidentalAreaRepository _residentalAreaRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubSectionRepository _subSectionRepository;
        private readonly IEmployeeRepository _employeeRepository; // Employee repository inject ediliyor

        public EmployeeImportService(
            IResidentalAreaRepository residentalAreaRepository,
            IProjectRepository projectRepository,
            IPositionRepository positionRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository,
            IEmployeeRepository employeeRepository) // Employee repository inject ediliyor
        {
            _residentalAreaRepository = residentalAreaRepository;
            _projectRepository = projectRepository;
            _positionRepository = positionRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task ImportAsync(Stream excelStream)
        {
            using var package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            var errors = new List<string>();

            // Satırları kontrol et ve komutları hazırla
            for (int row = 2; row <= rowCount; row++)
            {
                var fullName = worksheet.Cells[row, 1].Text;
                if (string.IsNullOrEmpty(fullName))
                {
                    errors.Add($"Full name is missing at row {row}.");
                    continue;
                }

                var badge = worksheet.Cells[row, 2].Text;
                var fin = worksheet.Cells[row, 3].Text;
                var phoneNumber = worksheet.Cells[row, 4].Text;

                // ResidentalArea kontrolü (isimle karşılaştırma)
                var residentalAreaName = worksheet.Cells[row, 5].Text;
                int? residentalAreaId = null;
                if (!string.IsNullOrEmpty(residentalAreaName))
                {
                    var residentalArea = await _residentalAreaRepository.GetByNameAsync(residentalAreaName);
                    if (residentalArea != null)
                    {
                        residentalAreaId = residentalArea.Id;
                    }
                }

                // Project kontrolü (isimle karşılaştırma)
                var projectName = worksheet.Cells[row, 7].Text;
                var project = await _projectRepository.GetByNameAsync(projectName);
                if (project == null)
                {
                    errors.Add($"Project '{projectName}' not found at row {row}.");
                    continue;
                }
                var projectId = project.Id;

                // Position kontrolü (isimle karşılaştırma)
                var positionName = worksheet.Cells[row, 8].Text;
                var position = await _positionRepository.GetByNameAsync(positionName);
                if (position == null)
                {
                    errors.Add($"Position '{positionName}' not found at row {row}.");
                    continue;
                }
                var positionId = position.Id;

                // Section kontrolü (isimle karşılaştırma)
                var sectionName = worksheet.Cells[row, 9].Text;
                var section = await _sectionRepository.GetByNameAsync(sectionName);
                if (section == null)
                {
                    errors.Add($"Section '{sectionName}' not found at row {row}.");
                    continue;
                }
                var sectionId = section.Id;

                // SubSection kontrolü (isimle karşılaştırma)
                var subSectionName = worksheet.Cells[row, 10].Text;
                int? subSectionId = null;
                if (!string.IsNullOrEmpty(subSectionName))
                {
                    var subSection = await _subSectionRepository.GetByNameAsync(subSectionName);
                    if (subSection != null)
                    {
                        subSectionId = subSection.Id;
                    }
                }

                // StartedDate kontrolü (null olamaz)
                if (!DateTime.TryParse(worksheet.Cells[row, 11].Text, out DateTime startedDate))
                {
                    errors.Add($"Invalid or missing Started Date at row {row}.");
                    continue;
                }

                // ContractEndDate nullable olabilir
                var contractEndDate = worksheet.Cells[row, 12].Text;
                if (string.IsNullOrEmpty(contractEndDate))
                {
                    contractEndDate = null;
                }

                // Yeni Employee entity'si oluşturuluyor
                var employee = new Employee
                {
                    FullName = fullName,
                    Badge = badge,
                    FIN = fin,
                    PhoneNumber = phoneNumber,
                    ResidentalAreaId = residentalAreaId,
                    ProjectId = projectId,
                    PositionId = positionId,
                    SectionId = sectionId,
                    SubSectionId = subSectionId,
                    StartedDate = startedDate.ToUniversalTime(),
                    ContractEndDate = contractEndDate
                };

                // Employee entity'sini ekliyoruz
                await _employeeRepository.AddAsync(employee);
            }

            // Eğer hata yoksa, işlemleri commit ediyoruz
            if (errors.Any())
            {
                throw new ValidationException(errors); // Hatalar varsa işlem durdurulur
            }

            // Tüm işlemleri commit ediyoruz
            await _employeeRepository.CommitAsync();
        }
    }
}
