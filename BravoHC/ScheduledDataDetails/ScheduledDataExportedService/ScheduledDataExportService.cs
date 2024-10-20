using Domain.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduledDataDetails.ScheduledDataExportService
{
    public class ScheduledDataExportService
    {
        public async Task<byte[]> ExportScheduledDataToExcelAsync(
            List<ScheduledData> scheduledDataList, int? projectId = null)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ScheduledData");

                // Extract unique dates from scheduled data
                var allDates = scheduledDataList
                    .Select(sd => sd.Date.Date)
                    .Distinct()
                    .OrderBy(date => date)
                    .ToList();

                // Add headers for Employee Info
                worksheet.Cells[1, 1].Value = "Employee Name";
                worksheet.Cells[1, 2].Value = "Badge";
                worksheet.Cells[1, 3].Value = "Section";
                worksheet.Cells[1, 4].Value = "Position";

                // Add headers for shift statistics
                worksheet.Cells[1, 5].Value = "Morning Shift Count";
                worksheet.Cells[1, 6].Value = "Afternoon Shift Count";
                worksheet.Cells[1, 7].Value = "Evening Shift Count";
                worksheet.Cells[1, 8].Value = "Day Off Count";
                worksheet.Cells[1, 9].Value = "Holiday Balance";
                worksheet.Cells[1, 10].Value = "Vacation Balance";

                // Add headers dynamically for each date
                for (int i = 0; i < allDates.Count; i++)
                {
                    worksheet.Cells[1, 11 + i].Value = allDates[i].ToString("yyyy-MM-dd");
                }

                // Group the data by EmployeeId
                var groupedData = scheduledDataList
                    .Where(sd => sd.Employee != null)
                    .GroupBy(sd => sd.EmployeeId)
                    .ToList();

                int row = 2; // Start from the second row for data
                foreach (var group in groupedData)
                {
                    var employee = group.First().Employee;

                    // Calculate shift statistics for the employee
                    int morningShiftCount = group.Count(sd => sd.Plan != null && sd.Plan.Shift == "Səhər");
                    int afternoonShiftCount = group.Count(sd => sd.Plan != null && sd.Plan.Shift == "Günorta");
                    int eveningShiftCount = group.Count(sd => sd.Plan != null && sd.Plan.Shift == "Gecə");
                    int dayOffCount = group.Count(sd => sd.Plan?.Shift == "Day Off");
                    int holidayBalance = group.Count(sd => sd.Plan?.Value == "Bayram");
                    int vacationBalance = group.Count(sd => sd.Plan?.Value == "Məzuniyyət" || sd.Plan?.Value == "Xəstəlik vərəqi");

                    // Fill static data for each employee
                    worksheet.Cells[row, 1].Value = employee.FullName ?? "Unknown";
                    worksheet.Cells[row, 2].Value = employee.Badge ?? "N/A";
                    worksheet.Cells[row, 3].Value = employee.Section?.Name ?? "No Section";
                    worksheet.Cells[row, 4].Value = employee.Position?.Name ?? "No Position";

                    // Fill shift statistics data
                    worksheet.Cells[row, 5].Value = morningShiftCount;
                    worksheet.Cells[row, 6].Value = afternoonShiftCount;
                    worksheet.Cells[row, 7].Value = eveningShiftCount;
                    worksheet.Cells[row, 8].Value = dayOffCount;
                    worksheet.Cells[row, 9].Value = holidayBalance;
                    worksheet.Cells[row, 10].Value = vacationBalance;

                    // Fill dynamic data for each date
                    for (int i = 0; i < allDates.Count; i++)
                    {
                        var date = allDates[i];
                        var scheduledItem = group.FirstOrDefault(sd => sd.Date.Date == date);

                        worksheet.Cells[row, 11 + i].Value = scheduledItem?.Plan?.Value ?? "—";
                    }

                    row++; // Move to the next row for the next employee
                }

                // Auto-fit columns for readability
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Return the Excel file as a byte array
                return await Task.FromResult(package.GetAsByteArray());
            }
        }
    }
}
