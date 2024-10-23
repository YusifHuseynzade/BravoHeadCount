using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Commands.Response
{
	public class UpdateScheduledDataCommandResponse
	{
		public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int SuccessCount { get; set; } // Başarılı güncelleme sayısı
        public int FailureCount { get; set; } // Başarısız güncelleme sayısı
        public List<string> FailureMessages { get; set; } // Hata mesajlarının listesi

        public UpdateScheduledDataCommandResponse()
        {
            FailureMessages = new List<string>(); // Hata mesajları için boş liste başlatılıyor
        }
    }
}
