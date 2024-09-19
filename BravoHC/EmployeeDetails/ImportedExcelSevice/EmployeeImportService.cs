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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBakuDistrictRepository _bakuDistrictRepository;
        private readonly IBakuMetroRepository _bakuMetroRepository;
        private readonly IBakuTargetRepository _bakuTargetRepository;

        public EmployeeImportService(
            IResidentalAreaRepository residentalAreaRepository,
            IProjectRepository projectRepository,
            IPositionRepository positionRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository,
            IEmployeeRepository employeeRepository,
            IBakuDistrictRepository bakuDistrictRepository,
            IBakuMetroRepository bakuMetroRepository,
            IBakuTargetRepository bakuTargetRepository)
        {
            _residentalAreaRepository = residentalAreaRepository;
            _projectRepository = projectRepository;
            _positionRepository = positionRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
            _employeeRepository = employeeRepository;
            _bakuDistrictRepository = bakuDistrictRepository;
            _bakuMetroRepository = bakuMetroRepository;
            _bakuTargetRepository = bakuTargetRepository;
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

                // BakuDistrict kontrolü
                var bakuDistrictName = worksheet.Cells[row, 6].Text;
                int? bakuDistrictId = null;
                if (!string.IsNullOrEmpty(bakuDistrictName))
                {
                    var bakuDistrict = await _bakuDistrictRepository.GetByNameAsync(bakuDistrictName);
                    if (bakuDistrict != null)
                    {
                        bakuDistrictId = bakuDistrict.Id;
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

                // BakuMetro kontrolü
                var bakuMetroName = worksheet.Cells[row, 11].Text;
                int? bakuMetroId = null;
                if (!string.IsNullOrEmpty(bakuMetroName))
                {
                    var bakuMetro = await _bakuMetroRepository.GetByNameAsync(bakuMetroName);
                    if (bakuMetro != null)
                    {
                        bakuMetroId = bakuMetro.Id;
                    }
                }

                // BakuTarget kontrolü
                var bakuTargetName = worksheet.Cells[row, 12].Text;
                int? bakuTargetId = null;
                if (!string.IsNullOrEmpty(bakuTargetName))
                {
                    var bakuTarget = await _bakuTargetRepository.GetByNameAsync(bakuTargetName);
                    if (bakuTarget != null)
                    {
                        bakuTargetId = bakuTarget.Id;
                    }
                }

                // RecruiterComment kontrolü
                var recruiterComment = worksheet.Cells[row, 13].Text; // RecruiterComment sütunu ekleniyor

                // StartedDate kontrolü (null olamaz)
                if (!DateTime.TryParse(worksheet.Cells[row, 14].Text, out DateTime startedDate))
                {
                    errors.Add($"Invalid or missing Started Date at row {row}.");
                    continue;
                }

                // ContractEndDate nullable olabilir
                var contractEndDate = worksheet.Cells[row, 15].Text;
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
                    BakuDistrictId = bakuDistrictId,
                    BakuMetroId = bakuMetroId,
                    BakuTargetId = bakuTargetId,
                    RecruiterComment = recruiterComment,  // RecruiterComment alana ekleniyor
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

            // Eğer hata varsa, işlem durdurulur
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            // Tüm işlemleri commit ediyoruz
            await _employeeRepository.CommitAsync();
        }
    }
}
