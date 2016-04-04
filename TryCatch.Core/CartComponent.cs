using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Core
{
    public class CartComponent
    {
        //public void AddItem(Article article,)
        public void Checkout(Cart cart)
        {
            /*var cart = _repository.Carts.FirstOrDefault(c => c.Guid == guid);

            if (cart == null)
            {
                throw 
                ModelState.AddModelError("", "Cart not found.");
                RedirectToAction("Index", "Cart");

                return View(new Order());
            }

            var customer = _repository.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);

            var order = new Order();
            order.Customer = customer;
            order.Items = cart.Items;
            order.Status = OrderStatus.WaitingPayment;
            order.DateTime = DateTime.Now;

            _repository.PutOrder(order);
            _repository.DeleteCart(cart);*/

        }

        public void Assign(Cart cart, Customer customer)
        {

        }
    }
}
