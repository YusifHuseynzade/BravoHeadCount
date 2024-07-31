using Common.Interfaces;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly IApplicationDbContext _dbContext;

        public SmsService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendOtpCode(string toPhoneNumber, string otpToken)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(u => u.OTPToken == otpToken);

            string baseUrl = "http://api.msm.az/sendsms";
            string username = "demo";
            string apiKey = "uhUkV9fj";
            string senderName = "MSM";
            string text = $"Your OTP code is: {otpToken}";

            using (HttpClient client = new HttpClient())
            {
                string url = $"{baseUrl}?user={username}&password={apiKey}&gsm={toPhoneNumber}&from={senderName}&text={text}";

                try
                {
                    // Send GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"SMS sent successfully! {response.RequestMessage}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send SMS. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public async Task SendGeneratedPassword(string toPhoneNumber, string userName, string password)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(u => u.PhoneNumber == toPhoneNumber && u.UserName == userName && u.Password == password);

            string baseUrl = "http://api.msm.az/sendsms";
            string username = "demo";
            string apiKey = "uhUkV9fj";
            string senderName = "MSM";
            string text = $"Istifadeci adiniz  - {userName}\nMuveqqeti sifreniz - {password}";

            using (HttpClient client = new HttpClient())
            {
                string url = $"{baseUrl}?user={username}&password={apiKey}&gsm={toPhoneNumber}&from={senderName}&text={text}";

                try
                {
                    // Send GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"SMS sent successfully! {response.RequestMessage}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send SMS. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
