Console.InputEncoding = System.Text.Encoding.UTF8; 
Console.OutputEncoding = System.Text.Encoding.UTF8;

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
static void ProcessOrder(string _orderID,Action<string> _AffterPro) 
{
    Console.WriteLine($"Processing order {_orderID}...");
    // Giả sử có một số logic xử lý đơn hàng ở đây
    System.Threading.Thread.Sleep(10); // Giả lập thời gian xử lý
    Console.WriteLine($"Order {_orderID} processed.");
    
    // Gọi action sau khi xử lý xong đơn hàng
    _AffterPro?.Invoke($"Order {_orderID} has been processed successfully.");
}

// Sử dụng ProcessOrder với một Action để nhận thông báo sau khi xử lý xong đơn hàng
ProcessOrder("12345", message => Console.WriteLine($"[NOTIFICATION] {message}"));


//FUNC<t,t,t,...> là một delegate đại diện cho một phương thức có một hoặc nhiều tham số kiểu T và trả về một giá trị kiểu T.
// tham số cuối cùng là tham số trả về, các tham số trước đó là tham số đầu vào