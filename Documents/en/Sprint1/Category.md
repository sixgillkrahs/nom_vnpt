# Yêu cầu nghiệp vụ
<br>- Cho phép thêm, sửa xóa, danh mục động để dùng chung
<br>- Một số danh mục: 
<br>  + Danh mục giai đoạn dự án
<br>  + Danh mục loại công việc
<br> .......

## Thêm mới danh mục
### Mô tả nghiệp vụ
| | Mô tả|
| --- | --- |
| Người thực hiên: | ADMIN |
| Mô tả ngắn: | - Chức cho phép người dùng thêm mới danh mục trong hệ thống |
| Yêu cầu chung: |- Thêm mới được danh mục <br> - Các danh mục không có danh mục con có thể bỏ qua
| Các yêu cầu đặc biệt: | - Phân trang cho xem được các bản ghi 10, 50,100 bản ghi <br> - Khi xem ở trang 3 và thao tác xem sửa xóa của các đối tượng trên trang 3 sau khi hoàn thành hoặc quay lại sẽ phải ở trang 3 không được về đầu trang |
### Mô tả các trường thông tin
| Tên trương | Kiểu dữ liêu | Bắt buộc | Giá trị | Mô tả|
| --- | --- | --- | --- | --- |
| Mã danh mục| String | * | 
| Tên danh mục | Text | * |
|Khóa|Checkbox| | | |
|Mô tả|
### Giao diện

# Yêu cầu kỹ thuật

 - Phân quyền theo từng danh mục
 - Các danh mục có thể tạo menu riêng
 - Thêm trang CRUD danh mục root (note: Các danh mục mục này được lưu db và có màn hình quản lý riêng tách biệt với các danh mục tạo bằng define)
 > Mục đích dùng cho chức năng 
 > - Dynamic form - Thu thập các dữ liệu của công việc
 > - Dynamic field - Như thuộc tính sản phẩm trong quản lý kho
 
 # Quay lại
 [Sprint SRS](../Index.md#sprint-1)