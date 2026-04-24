using TEST_APP_;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Output:
        // Đơn hàng #abc-123 đã được tạo
        // [IN] Đang in hóa đơn cho Nguyễn Nam...
        // [BẾP] Chuẩn bị: Latte, Croissant
        // [ĐIỂM THƯỞNG] Cộng 10 điểm cho Nguyễn Nam
        // --- Lắp ghép lại trong Program ---
        var orderService = new OrderService();
        var printService = new PrintingService();
        var kitchenService = new KitchenService();
        var loyaltyService = new LoyaltyService();

        // Mỗi bộ phận tự đăng ký lắng nghe — OrderService không cần biết ai đang nghe
        orderService.OrderCreated += printService.OnOrderCreated;
        orderService.OrderCreated += kitchenService.OnOrderCreated;
        orderService.OrderCreated += loyaltyService.OnOrderCreated;

        // Khi đặt hàng — OrderService chỉ cần raise event, không quan tâm ai xử lý
        orderService.PlaceOrder("Nguyễn Nam", new List<string> { "Latte", "Croissant" });
    }
}

//void PrintInvoice(string details) => Console.WriteLine($"[HÓA ĐƠN] {details}");
//void NotifyKitchen(string details) => Console.WriteLine($"[BẾP] {details}");
//void AddLoyaltyPoints(string details) => Console.WriteLine($"[ĐIỂM THÀNH VIÊN] Đã cộng điểm");

//// += để thêm hàm vào chuỗi
//OrderProcessor allTasks = PrintInvoice;
//allTasks += NotifyKitchen;      // thêm hàm thứ hai
//allTasks += AddLoyaltyPoints;   // thêm hàm thứ ba

//// Gọi một lần — cả ba hàm đều chạy theo thứ tự
//allTasks("Latte x2");
//// [HÓA ĐƠN] Latte x2
//// [BẾP] Latte x2
//// [ĐIỂM THÀNH VIÊN] Đã cộng điểm



//// -= để gỡ một hàm ra khỏi chuỗi
//allTasks -= NotifyKitchen;
//allTasks("Espresso x1");
//// [HÓA ĐƠN] Espresso x1       ← vẫn chạy
//// [ĐIỂM THÀNH VIÊN] Đã cộng điểm  ← vẫn chạy
//// (NotifyKitchen đã bị gỡ ra)

////delegate kiểu void nhận 1 chuỗi làm tham số 
//delegate void OrderProcessor(string orderDetails);


//DELEGATE hiện đại action / func / predicate là 1 phần của thư viện chuẩn .NET,
//giúp đơn giản hóa việc sử dụng delegate mà không cần phải định nghĩa một kiểu delegate riêng biệt.
// có thể có hoặc không có tham số đầu vào và trả về VOID

//// ACTUION dạng không tham số 
//Action _Delegate =() => Console.WriteLine("Sulek ngo"); // tương đương delegate void MyDelegate() { Console.WriteLine("Hello World!"); }

//// Action<T> là một delegate đại diện cho một phương thức có một tham số kiểu T và không trả về giá trị (void).
//Action<string> _Loger = Console.WriteLine; // tương đương delegate void MyDelegate(string s) { Console.WriteLine(s); }
//_Loger.Invoke("Sulek ngo"); // gọi hàm thông qua delegate

//// hoặc sử dụng lamba expression để định nghĩa hàm trực tiếp khi gán cho delegate
//// kiểu này khá là rõ ràng hơn khi sử dụng lambda expression, đặc biệt khi hàm có nhiều tham số hoặc logic phức tạp hơn.
//Action<string> _Loger2 = s => Console.WriteLine(s); // sử dụng biểu thức lambda để định nghĩa hàm
//_Loger2.Invoke("sulek ngo");
//// truyền action vào method khác ví dụ 
//static void ProcessOrder(string _orderID, Action<string> _AffterPro)
//{
//    Console.WriteLine($"Processing order {_orderID}...");
//    // Giả sử có một số logic xử lý đơn hàng ở đây
//    System.Threading.Thread.Sleep(10); // Giả lập thời gian xử lý
//    Console.WriteLine($"Order {_orderID} processed.");

//    // Gọi action sau khi xử lý xong đơn hàng
//    _AffterPro?.Invoke($"Order {_orderID} has been processed successfully.");
//}

//// Sử dụng ProcessOrder với một Action để nhận thông báo sau khi xử lý xong đơn hàng
//ProcessOrder("12345", message => Console.WriteLine($"[NOTIFICATION] {message}"));


////FUNC<t,t,t,...> là một delegate đại diện cho một phương thức có một hoặc nhiều tham số kiểu T và trả về một giá trị kiểu T.
//// tham số cuối cùng là tham số trả về, các tham số trước đó là tham số đầu vào

//Action<string>log = message=> Console.WriteLine($"[LOG] {message}"); // Action để ghi log

//log(DateTime.Now.ToString());

// không nên lạm dụng Delegate , Action, Func, Predicate để làm mọi thứ, đặc biệt là khi logic phức tạp hoặc cần nhiều tham số.
// Trong những trường hợp đó, việc định nghĩa một lớp hoặc interface riêng biệt có thể giúp mã dễ đọc và bảo trì hơn.
// chỉ nên dùng khi cần truyền một hàm như một tham số, hoặc khi muốn tạo ra một chuỗi các hàm để gọi lần lượt
// hoặc khi muốn tận dụng tính năng đa hình của delegate để thay đổi hành vi của chương trình tại runtime.

public class PrintingService
{
    // Phương thức xử lý event phải có đúng chữ ký:
    // void MethodName(object sender, TEventArgs e)
    public void OnOrderCreated(object sender, OrderCreatedEventArgs e)
    {
        Console.WriteLine($"[IN] Đang in hóa đơn cho {e.Order.CustomerName}...");
    }
}

public class KitchenService
{
    public void OnOrderCreated(object sender, OrderCreatedEventArgs e)
    {
        var itemList = string.Join(", ", e.Order.Items);
        Console.WriteLine($"[BẾP] Chuẩn bị: {itemList}");
    }
}

public class LoyaltyService
{
    public void OnOrderCreated(object sender, OrderCreatedEventArgs e)
    {
        Console.WriteLine($"[ĐIỂM THƯỞNG] Cộng 10 điểm cho {e.Order.CustomerName}");
    }
}

