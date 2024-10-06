using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HeadCountBackgroundColorRepository : Repository<HeadCountBackgroundColor>, IHeadCountBackgroundColorRepository
    {
        private readonly AppDbContext _context;

        public HeadCountBackgroundColorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetYellowColorIdAsync()
        {
            // Veritabanında "Yellow" renk ismine sahip olan kaydı arıyoruz
            var yellowColor = await _context.HeadCountBackgroundColors
                .FirstOrDefaultAsync(color => color.ColorHexCode == "#FFFF00");

            // Eğer "Yellow" rengi bulunamazsa bir hata fırlatıyoruz
            if (yellowColor == null)
            {
                throw new Exception("Yellow color not found in the database.");
            }

            // "Yellow" renginin Id'sini döndürüyoruz
            return yellowColor.Id;
        }

        public async Task<int> GetWhiteColorIdAsync()
        {
            // Veritabanında "Yellow" renk ismine sahip olan kaydı arıyoruz
            var yellowColor = await _context.HeadCountBackgroundColors
                .FirstOrDefaultAsync(color => color.ColorHexCode == "#FFFFFF");

            // Eğer "Yellow" rengi bulunamazsa bir hata fırlatıyoruz
            if (yellowColor == null)
            {
                throw new Exception("Yellow color not found in the database.");
            }

            // "Yellow" renginin Id'sini döndürüyoruz
            return yellowColor.Id;
        }

        public async Task<int> GetBlueColorIdAsync()
        {
            var blueColor = await _context.HeadCountBackgroundColors
                .FirstOrDefaultAsync(c => c.ColorHexCode == "#33FF57");
            return blueColor?.Id ?? 0; // Varsayılan değer olarak 0
        }
    }
}
