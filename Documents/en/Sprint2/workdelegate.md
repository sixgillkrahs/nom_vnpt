# Yêu cầu nghiệp vụ
<br>- Cho phép thêm sửa xóa một công việc cần thực hiện 
<br>- Công việc chưa phân sẽ được nằm ở danh sách công việc chưa phân
<br>- Công việc sẽ được phân cho 1 người xử lý và nhiều người liên quan có thể tham gia bình luận, trao đổi, hỗ trợ
<br>- Tạo công việc con nếu có công việc liên quan tới người khác và có thể gán người xử lý cho công việc con đó
<br>- Tổng các công việc con hoàn thành thì công việc cha sẽ hoàn thành(trường hợp chọn công việc con phụ thuộc vào công việc cha)
<br>- Cho phép phân công công việc cho người khác
<br>- Công việc yêu cầu phê duyệt 2 bước sẽ chọn thêm người phê duyệt(2 người phê duyệt mới tính là done)

## Chức năng danh sách công việc chưa phân
### Mô tả các trường thông tin
| Tên trương | Kiểu dữ liêu | Bắt buộc | Giá trị | Mô tả|
| --- | --- | --- | --- | --- |
| Thông tin công việc|
| Tên công việc | Text | * |
| Độ quan trọng | | *| 
| Hạn hoàn thành | | *| 
| Ngày tạo| | *| 
| Dự án| | *| 
### Giao diện
![ghhh](image/Work%20Delegate.jpg)

## Chức năng thêm mới công việc
### Yêu cầu chung

### Mô tả các trường thông tin
| Tên trương | Kiểu dữ liêu | Bắt buộc | Giá trị | Mô tả|
| --- | --- | --- | --- | --- |
| Thông tin công việc: |
| Tên công việc| Text | * |
| Mã công việc | | *| 
| Mã đầu việc | | *|  | Lấy tại danh mục đầu việc|
| Mô tả | Text | 
|Thông tin giao việc:|
|Người thực hiện| | | | Lấy từ danh sách nhân viên|
|Người kiểm tra| | | | Lấy từ danh sách nhân viên|
|Người giao việc| | | | Tên acc giao việc|
|Ngày bắt đầu|Datetime | | | Mặc định ngày giao việc, có thể sửa |
|Hạn hoàn thành| Datetime | | | |
|Mức độ ưu tiên| Droplist | | | lấy từ danh mục |
|Mức độ quan trọng| Droplist| | | lấy từ danh mục |
|Mục tiêu| checkbox | |  |Thêm mới các mục tiêu trong công việc|
|Bình luận| 
|Công việc con| List| | | Tạo list công việc con gán timne và người xử lý|
|Tài liệu| | |  | Attack tài liệu liên quan tới công việc kèm theo|
|Nhật ký| | | | - Tab nhật ký xử lý công việc: ai phân công, ai xử lý ai phê duyêt.. vào ngày giờ <br> - Tab nhật ký tác đông vào công việc: các tác động như xóa, sửa vào ngày giờ...
|Thời gian thực hiện| | | | -Nếu làm xong sớm có thể log thời gian làm thực tế vào <br>- Trường hợp hoàn thành công việc hệ thống sẽ tự động lấy ngày, giờ kết thúc- giờ băt đầu = thời gian thực hiện|

### Giao diện
![ghhh](Image/creatwork.jpg)
![ghhh](Image/historywork.jpg)
![ghhh](Image/accept.jpg)

## Chức năng bàn giao công việc
### Yêu cầu chung
<br>- Bàn giao công việc đã và đang phụ trách hoặc xử lý cho một người khác 
<br>- Có thể bàn giao công việc của mình hoặc bàn giao công việc của người khác nếu được phân quyền
<br>- Có thể bàn giao công việc theo vai trò của mình đang đảm nhận hoặc trạng thái công việc
<br>- Có thể bàn giao công việc riêng lẻ hoặc theo dự án
<br>- Các công việc được bàn giao sẽ được người mới đảm nhận nhưng lịch sử xử lý công việc vẫn lưu vết quá trình lưu vết của người trước

### Giao diện
![ghhh](image/Work%20Delegate.jpg)

# Yêu cầu kỹ thuật
-

 # Quay lại
 [Sprint SRS](../Index.md#sprint-1)