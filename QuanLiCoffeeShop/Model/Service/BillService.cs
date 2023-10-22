using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class BillService
    {
        public BillService() { }
		private static BillService ins;

		public static BillService _ins
		{
			get 
			{
				if (_ins == null)
				{
					_ins = new BillService();
				}
				return ins; 
			}
			private set { ins = value; }
		}
		// Get All Bill
		public async Task<List<BillDTO>> GetAllBill()
		{
			var billList = (from c in DataProvider.Ins.DB.Bill
							select new BillDTO
							{
								ID = c.ID,
								IDCus = c.IDCus,
								IDStaff = c.IDStaff,
								CreateAt = c.CreateAt,
								TotalPrice = c.TotalPrice,
								Customer =	c.Customer,
								Staff = c.Staff,
								BillInfo = (from x in c.BillInfo where x.IsDeleted==false select new BillInfoDTO
											{
												IDBill = x.IDBill,
												PriceItem = x.PriceItem,
												IDProduct= x.IDProduct,
												Count = x.Count,
												Bill = x.Bill,
												Product = x.Product,
											}).ToList(),
								IsDeleted= c.IsDeleted
							}).ToListAsync();
			
            return await billList;
		}
		//Add new bill
		public async Task<(bool, string)> AddNewBill(BillDTO newBill)
		{
			Bill bill = new Bill
			{
				ID = newBill.ID,
				IDCus= newBill.IDCus,
				IDStaff= newBill.IDStaff,
				IsDeleted= newBill.IsDeleted,
				CreateAt= newBill.CreateAt,
				Customer= newBill.Customer,
				Staff= newBill.Staff,
			};
			DataProvider.Ins.DB.Bill.Add(bill);
			foreach(var g in newBill.BillInfo)
			{
				BillInfo billInfo = new BillInfo
				{
					IDBill= g.IDBill,
					IDProduct = g.IDProduct,
					IsDeleted = g.IsDeleted,
					PriceItem= g.PriceItem,
					Count= g.Count,
					Bill = g.Bill,
					Product = g.Product,
				};
				DataProvider.Ins.DB.BillInfo.Add(billInfo);
			}
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Them thanh cong");
		}
		//Edit bill
	}
}
