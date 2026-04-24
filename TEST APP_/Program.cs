Console.OutputEncoding=System.Text.Encoding.UTF8; // để hiển thị emoji
Console.WriteLine(GetStatusLabel("Pending"));


// switch expression — mỗi arm là "pattern => giá trị"
// Compiler cảnh báo nếu bạn quên xử lý một case nào đó (exhaustiveness)
string GetStatusLabel(string status) => status switch
{
    "Pending" => " Đang chờ xác nhận",
    "Preparing" => " Đang pha chế",
    "Ready" => " Sn sàng lấy",
    "Delivered" => " Đã giao",
    "Cancelled" => " Đã hủy",
    _ => "❓ Trạng thái không xác định" // _ là wildcard — bắt mọi trường hợp còn lại
};