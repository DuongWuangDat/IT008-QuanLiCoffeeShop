# Quản lí coffee shop
## Thành viên nhóm:
- Dương Quang Đạt- 22520208
- Nguyễn Đại Dương- 22520304
- Trần Thành Đạt- 22520237
- Hà Minh Đức- 22520266
## Giảng viên hướng dẫn:
- Nguyễn Tấn Toàn
## Mô tả sản phẩm:
- Coffee Time là ứng dụng quản lý đặc biệt được thiết kế cho quán cafe. Với giao diện thân thiện và đa tính năng, ứng dụng này cung cấp giải pháp toàn diện cho cả admin và nhân viên. Admin có thể dễ dàng quản lý đơn hàng, theo dõi kho hàng, quản lý nhân viên và khách hàng, cũng như xem báo cáo doanh số bán hàng. Trong khi đó, nhân viên có thể sử dụng để nhận và thanh toán đơn hàng, theo dõi hóa đơn và báo cáo sự cố. Với tính năng tương thích đa nền tảng, giao diện thân thiện và bảo mật cao, Coffee Time không chỉ giúp tối ưu hóa quy trình làm việc mà còn tạo ra trải nghiệm người dùng mượt mà, giúp quán cafe nổi bật và tối ưu hóa hoạt động kinh doanh của mình.
## Đối tượng sử dụng:
- Hệ thống quản cafe:
  - Chủ quán cafe: admin app
  - Nhân viên quán cafe
## Công nghệ sử dụng:
- Design UI/UX: Figma
- Front-end và hệ thống API: WPF C#, MVVM, Entity Framework
- Database: Microsoft SQL Server, Azure SQL Database
## Hướng dẫn cài đặt:
**Developer**
- Clone repository này về máy
  `git clone https://github.com/DuongWuangDat/IT008-QuanLiCoffeeShop.git`
- Local:
  - Mở Microsoft SQL Server
  - Chạy query "QuanLiCoffeeShop.sql"
  - Vào App.config, sửa thuộc tính connectionstring từ sau "provider connection string=\&quot;" đến "\&quot;" thành "data source={your server};initial catalog={your database};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
  - Trong đó {your server} sẽ là tên server của bạn, {your database} sẽ là tên database của bạn
  - Bấm F5 để trải nghiệm app (Nếu bạn đang ở Debug sẽ trải nghiệm được app ngay, nếu bạn đang ở Release bạn vào thư mục bin/Release để chạy file .exe)
- Server online:
  - Hiện tại server của chúng tôi đang được online bạn sẽ có thẻ chạy ngay mà không cần các thao tác phức tạp
    
**User**
- Download repository này về
- Local:
  - Mở Microsoft SQL Server
  - Chạy query "QuanLiCoffeeShop.sql"
  - Vào App.config, sửa thuộc tính connectionstring từ sau "provider connection string=\&quot;" đến "\&quot;" thành "data source={your server};initial catalog={your database};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
  - Trong đó {your server} sẽ là tên server của bạn, {your database} sẽ là tên database của bạn
  - Bấm Ctrl + B (Chuyển sang chế độ Release) sau đó bạn vào thư mục bin/Release để chạy file .exe
- Server online:
  - Hiện tại server của chúng tôi đang được online bạn có thể Ctrl + B ngay mà không cần các thao tác phức tạp  
*Server sẽ ngừng hoạt động vào 12/01/2024*
# Tổng kết:
- Sản phẩm là nỗ lực hết mình của các thành viên trong nhóm. Thông qua đó, các thành viên đã học hỏi được nhiều về việc phát triển một dự án thực tế cũng như khó khăn sau này gặp phải.
- Đây là sản phẩm đầu tay của nhóm chúng tôi với lòng nhiệt huyết, đam mê công nghệ nên mong mọi người đón nhận. Mọi sai sót trong quá trình trải nghiêm app, mong mọi người có thể feedback cho đội ngũ phát triển chúng tôi tại coffeetime2510@gmail.com. Chúng tôi vô vùng cảm ơn các bạn
