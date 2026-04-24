using System;
using System.Collections.Generic;
using System.Text;

namespace TEST_APP_
{
    //Event : Delegate đặc biệt được sử dụng để xử lý các sự kiện, cho phép các đối tượng đăng ký và phản hồi khi một sự kiện xảy ra.
    public class OrderService
    {


        // Khai báo event — dùng từ khóa 'event' phía trước delegate
        // EventHandler<T> là delegate chuẩn của .NET cho các sự kiện
        // T là kiểu dữ liệu đi kèm với sự kiện (EventArgs)
        public event EventHandler<OrderCreatedEventArgs> OrderCreated;
        // EventArgs là lớp chứa dữ liệu đi kèm với sự kiện
        // Quy ước đặt tên: [TênSựKiện]EventArgs
       
        public void PlaceOrder(string customerName, List<string> items)
        {
            // Xử lý logic tạo đơn hàng
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = customerName,
                Items = items,
                CreatedAt = DateTime.Now
            };

            Console.WriteLine($"Đơn hàng #{order.Id} đã được tạo");

            // Kích hoạt event — dùng ?. để an toàn khi chưa có ai đăng ký
            // 'this' là nguồn phát sinh sự kiện (sender)
            // EventArgs chứa dữ liệu gửi kèm theo sự kiện
            OrderCreated?.Invoke(this, new OrderCreatedEventArgs { Order = order });
        }
    }
   

}
