using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class SeatService
    {
        public SeatService() { }
		private static SeatService _ins;

		public static SeatService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new SeatService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}

		//Get all seat
		public async Task<List<SeatDTO>> GetAllSeat()
		{
			var seatList = (from c in DataProvider.Ins.DB.Seat 
							where c.IsDeleted == false
							select new SeatDTO 
							{ 
								ID = c.ID,
								GenreName= c.GenreSeat.DisplayName,
								IDGenre = c.IDGenre,
								IsDeleted= c.IsDeleted,
								Status=c.Status,
							}).ToListAsync();
			return await seatList;
		}
		
		//Add new seat
		public async Task<(bool, string)> AddNewSeat(Seat newSeat)
		{
			DataProvider.Ins.DB.Seat.Add(newSeat);
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true,"Them thanh cong");

        }

		//Delete seat
		public async Task<(bool, string)> DeleteSeat(int ID)
		{
			var seat = await DataProvider.Ins.DB.Seat.Where(p => p.ID == ID).FirstOrDefaultAsync();
			if(seat.IsDeleted== false) seat.IsDeleted = true;
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Xoa thanh cong");
		}

		//Edit seat
		public async Task<(bool,string)> EditSeat(Seat newSeat)
		{
            var seat = await DataProvider.Ins.DB.Seat.Where(p => p.ID == newSeat.ID).FirstOrDefaultAsync();
			seat.IDGenre = newSeat.IDGenre;
			seat.Status = newSeat.Status;
			seat.GenreSeat = newSeat.GenreSeat;
			await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Cap nhat thanh cong");
		}
	}
}
