using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
				bill.BillInfo.Add(billInfo);
				DataProvider.Ins.DB.BillInfo.Add(billInfo);
			}
            DataProvider.Ins.DB.Bill.Add(bill);
            await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Them thanh cong");
		}
        //Delete bill
        public async Task<(bool, string)> DeleteBill(BillDTO Bill)
		{
			var bill = await DataProvider.Ins.DB.Bill.Where(p=>p.ID==Bill.ID).FirstOrDefaultAsync();
			if(bill.IsDeleted== false) bill.IsDeleted = true;
			foreach(var b in Bill.BillInfo)
			{
				var billInfo = await DataProvider.Ins.DB.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDProduct == b.IDProduct).FirstOrDefaultAsync();
				if(billInfo.IsDeleted== false) billInfo.IsDeleted = true;
            }
			return (true, "Xoa thanh cong");
		}

		//Edit bill
		public async Task<(bool, string)> EditBill(BillDTO newBill)
		{
            var bill = await DataProvider.Ins.DB.Bill.Where(p => p.ID == newBill.ID).FirstOrDefaultAsync();
			bill.IDStaff = newBill.IDStaff;
			bill.Staff=newBill.Staff;
			bill.Customer=newBill.Customer;
			bill.IDCus= newBill.IDCus;
			bill.CreateAt = newBill.CreateAt;
			bill.TotalPrice = newBill.TotalPrice;
			foreach(var b in newBill.BillInfo)
			{
                var billInfo = await DataProvider.Ins.DB.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDProduct == b.IDProduct).FirstOrDefaultAsync();
				billInfo.IDBill = b.IDBill;
				billInfo.IDProduct = b.IDProduct;
				billInfo.PriceItem = b.PriceItem;
				billInfo.IsDeleted = false;
				billInfo.Product = b.Product;
				billInfo.Count = b.Count;
				billInfo.Bill = b.Bill;
                bill.BillInfo.Add(new BillInfo
				{
					IDBill=b.IDBill,
					IDProduct=b.IDProduct,
					PriceItem=b.PriceItem,
					IsDeleted=false,
					Product=b.Product,
					Count=b.Count,
					Bill=b.Bill,
				});
			}
			return (true, "Cap nhat thanh cong");
        }

    }
}
