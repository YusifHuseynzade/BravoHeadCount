using Domain.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadCountDetails.HeadCountExportedService
{
    public class HeadCountExportService
    {
        public async Task<byte[]> ExportHeadCountsToExcelAsync(List<HeadCount> headCounts, int? projectId = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // EPPlus lisansını belirle
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("HeadCounts");

                // Başlık satırlarını ekle
                worksheet.Cells[1, 1].Value = "IsVacant";
                worksheet.Cells[1, 2].Value = "Project Name";
                worksheet.Cells[1, 3].Value = "Functional Area Name";
                worksheet.Cells[1, 4].Value = "Section Name";
                worksheet.Cells[1, 5].Value = "SubSection Name";
                worksheet.Cells[1, 6].Value = "Position Name";
                worksheet.Cells[1, 7].Value = "Employee Name";
                worksheet.Cells[1, 8].Value = "Employee Badge";
                worksheet.Cells[1, 9].Value = "Employee FIN";
                worksheet.Cells[1, 10].Value = "Employee Phone Number";
                worksheet.Cells[1, 11].Value = "Employee Residental Area";
                worksheet.Cells[1, 12].Value = "Recruiter Comment";
                worksheet.Cells[1, 13].Value = "HCNumber";
                worksheet.Cells[1, 14].Value = "Created Date";

                // ProjectId'ye göre filtreleme
                var filteredHeadCounts = projectId.HasValue
                    ? headCounts.Where(hc => hc.ProjectId == projectId.Value).ToList()
                    : headCounts;

                // Veri satırlarını doldur
                var row = 2;
                foreach (var hc in filteredHeadCounts)
                {
                    worksheet.Cells[row, 1].Value = hc.IsVacant.HasValue ? (hc.IsVacant.Value ? "Yes" : "No") : "Unknown";
                    worksheet.Cells[row, 2].Value = hc.Project?.ProjectName ?? "No Project"; // Null kontrolü
                    worksheet.Cells[row, 3].Value = hc.Project?.FunctionalArea ?? "No Functional Area"; // Null kontrolü
                    worksheet.Cells[row, 4].Value = hc.Section?.Name ?? "No Section"; // Null kontrolü
                    worksheet.Cells[row, 5].Value = hc.SubSection?.Name ?? "No SubSection"; // Null kontrolü
                    worksheet.Cells[row, 6].Value = hc.Position?.Name ?? "No Position"; // Null kontrolü
                    worksheet.Cells[row, 7].Value = hc.Employee?.FullName ?? "No Employee"; // Null kontrolü
                    worksheet.Cells[row, 8].Value = hc.Employee?.Badge ?? "No Badge"; // Null kontrolü
                    worksheet.Cells[row, 9].Value = hc.Employee?.FIN ?? "No FIN"; // Null kontrolü
                    worksheet.Cells[row, 10].Value = hc.Employee?.PhoneNumber ?? "No Phone Number"; // Null kontrolü
                    worksheet.Cells[row, 11].Value = hc.Employee?.ResidentalArea?.Name ?? "No Residental Area"; // Null kontrolü
                    worksheet.Cells[row, 12].Value = hc.Employee?.RecruiterComment ?? "N/A"; // Null kontrolü
                    worksheet.Cells[row, 13].Value = hc.HCNumber;
                    worksheet.Cells[row, 14].Value = hc.CreatedDate.ToString("dd/MM/yyyy"); // Tarih formatı

                    row++;
                }

                // Kolon genişliklerini ayarla
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Asenkron olarak byte dizisini döndür
                return await Task.FromResult(package.GetAsByteArray());
            }
        }
    }
}
