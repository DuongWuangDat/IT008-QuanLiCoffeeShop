using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.View.MessageBox
{
    internal class MessageBoxCustom
    {
        public static int Add = 1;
        public static int Error = 2;
        public static void Show(int type, string message)
        {
            if (type == Add)
            {
                new AddedSuccessfully(message).ShowDialog();
            }
            else if (type == Error)
            {
                new Error(message).ShowDialog();
            }
        }
    }
}
