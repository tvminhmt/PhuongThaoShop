using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Data;

namespace PTS.Infrastructure.Repositories
{
    public class CardVGARepository : ICardVGARepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ResponseDto _responseDto;
        public CardVGARepository(ApplicationDbContext context)
        {
            _context = context;
            _responseDto = new ResponseDto();
        }
        public async Task<bool> Create(CardVGAEntity obj)
        {
            var checkMa = await _context.CardVGAEntity.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.CardVGAEntity.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            var cardVGA = await _context.CardVGAEntity.FindAsync(id);
            if (cardVGA == null)
            {
                return false;
            }
            try
            {
                cardVGA.IsDeleted = true;
                _context.CardVGAEntity.Update(cardVGA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<CardVGAEntity>> GetAllCardVGA()
        {
            var list = await _context.CardVGAEntity.ToListAsync();
            var listCardVGA = list.Where(x => x.IsDeleted == false).ToList();
            return listCardVGA;
        }

        public async Task<CardVGAEntity> GetById(int id)
        {
            var result = await _context.CardVGAEntity.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(CardVGAEntity obj)
        {
            var cardVGA = await _context.CardVGAEntity.FindAsync(obj.Id);
            if (cardVGA == null)
            {
                return false;
            }
            try
            {
                cardVGA.Ten = obj.Ten;
                cardVGA.ThongSo = obj.ThongSo;
                _context.CardVGAEntity.Update(cardVGA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
