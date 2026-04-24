Console.InputEncoding = System.Text.Encoding.UTF8; 
Console.OutputEncoding = System.Text.Encoding.UTF8;

void PrintInvoice(string details) => Console.WriteLine($"[HÓA ĐƠN] {details}");
void NotifyKitchen(string details) => Console.WriteLine($"[BẾP] {details}");
void AddLoyaltyPoints(string details) => Console.WriteLine($"[ĐIỂM THÀNH VIÊN] Đã cộng điểm");

// += để thêm hàm vào chuỗi
OrderProcessor allTasks = PrintInvoice;
allTasks += NotifyKitchen;      // thêm hàm thứ hai
allTasks += AddLoyaltyPoints;   // thêm hàm thứ ba

// Gọi một lần — cả ba hàm đều chạy theo thứ tự
allTasks("Latte x2");
// [HÓA ĐƠN] Latte x2
// [BẾP] Latte x2
// [ĐIỂM THÀNH VIÊN] Đã cộng điểm

// -= để gỡ một hàm ra khỏi chuỗi
allTasks -= NotifyKitchen;
allTasks("Espresso x1");
// [HÓA ĐƠN] Espresso x1       ← vẫn chạy
// [ĐIỂM THÀNH VIÊN] Đã cộng điểm  ← vẫn chạy
// (NotifyKitchen đã bị gỡ ra)

delegate void OrderProcessor(string orderDetails);
