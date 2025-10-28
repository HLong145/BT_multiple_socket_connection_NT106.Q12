# Bài tập lập trình mạng –  ứng dụng quản lý người dùng với tính năng đăng nhập, đăng ký với TCP socket

##  Thông tin nhóm 8

| MSSV | Họ và Tên |
|------|------------|
| 24520943 | **Lâm Tú Lan** |
| 24521005 | **Nguyễn Hoàng Long** |
| 24520903 | **Lục Vĩnh Kiệt** |
| 24520376 | **Huỳnh Thanh Duy** |
| 24520968 | **Phạm Quang Linh** |

##  Mô tả bài tập

Đây là **bài tập 3** trong học phần **Lập trình mạng**, sử dụng **Windows Forms (C#)** kết hợp với **SQL Server** để xây dựng ứng dụng **quản lý người dùng với tính năng đăng nhập, đăng kí** .

Ứng dụng cho phép:
- Người dùng đăng ký tài khoản mới.
- Đăng nhập bằng tài khoản đã có.
- Kiểm tra thông tin hợp lệ và lưu dữ liệu trong cơ sở dữ liệu `USERDB`.
- Nếu người dùng đã có tài khoản nhưng quên mật khẩu thì có thể chọn Forgot password? để được nhận mã OTP về email hay SMS để tạo mật khẩu mới.
- Có tính năng Remember me giúp người dùng có thể đăng nhập nhanh chóng những lần tiếp theo.

##  Công nghệ sử dụng

- Ngôn ngữ lập trình: **C# (.NET 8.0 – WinForms)**
- Cơ sở dữ liệu: **Microsoft SQL Server**
- Môi trường phát triển: **Visual Studio 2022**
- Công cụ làm việc nhóm: GitHub desktop
- Hệ điều hành: **Windows**
- Quản lý session/token đăng nhập JWT **(JSON Web Tokens)**
- Format dữ liệu client-server **JSON serialization**
- Giao tiếp client-server qua TcpClient/TcpListener **TCP Socket Programming**
- Lập trình bất đồng bộ **Task-based Asynchronous Pattern**
- Multithreading **ConcurrentDictionary**

## Hướng dẫn cài đặt và chạy project

Để ứng dụng hoạt động chính xác, bạn cần thực hiện các bước sau:

---

### 1. Đảm bảo đã cài **SQL Server** và **SQL Server Management Studio (SSMS)**

- Nếu chưa cài, tải và cài theo thứ tự:
  - **SQL Server Developer Edition:** [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  - **SQL Server Management Studio (SSMS):** [https://aka.ms/ssmsfullsetup](https://aka.ms/ssmsfullsetup)
- Sau khi cài xong, mở **SSMS** và đảm bảo có thể kết nối được đến:
  - Server name: localhost\SQLEXPRESS
  - Authentication: Windows Authentication
- Nếu kết nối thành công → OK

###  2. Clone repository về máy

- Mở **GitHub Desktop**
- Chọn **File → Clone Repository**
- Dán link GitHub của project :[https://github.com/HLong145/BT_tuan3_laptrinhmang](https://github.com/HLong145/BT_multiple_socket_connection_NT106.Q12)
- Chọn vị trí lưu trên máy → **Clone**

---
### 3. Tạo cơ sở dữ liệu từ file `USERDB.sql`

- Mở **SQL Server Management Studio (SSMS)**
- Kết nối đến **localhost\SQLEXPRESS**
- Chọn menu **File → Open → File...**
- Mở file `USERDB.sql` trong thư mục project vừa clone
- Nhấn **Execute (F5)** để chạy script tạo cơ sở dữ liệu `USERDB`
- Kiểm tra: trong cây **Databases** thấy xuất hiện `USERDB` là thành công

---
### 4. Kết nối Visual Studio với cơ sở dữ liệu

1. Mở **Visual Studio (VS tím)**
2. Vào menu **View → Server Explorer**
3. Trong cửa sổ **Server Explorer**, nhấn biểu tượng **kết nối cơ sở dữ liệu** (hình tròn màu xanh ở góc trên bên trái)
4. Trong hộp thoại hiện ra:
 - **Server name:** nhập tên instance SQL của bạn (thường là `localhost\SQLEXPRESS`)
 - **Authentication:** Windows Authentication *(hoặc nhập username/password nếu dùng SQL Authentication)*
 - Tick chọn **Trust server certificate**
 - **Select or enter a database name:** chọn `USERDB`
5. Nhấn **OK** để kết nối.

Nếu kết nối thành công ta sẽ thấy `USERDB` xuất hiện trong Server Explorer.

---

### 5. Build và chạy chương trình

1. Mở file solution `Socket_LTMCB.sln` trong Visual Studio (Đường dẫn: **Socket_LTMCB -> Socket_LTMCB.sln**)
2. Nhấn **Ctrl + Shift + B** để **Build Solution**
3. Sau khi build thành công, nhấn **F5** hoặc nút **Start Debugging** để chạy chương trình.

Ứng dụng sẽ mở ra form **Dashboard** cho người dùng chọn chạy server/client, sử dụng cơ sở dữ liệu `USERDB` vừa tạo.



### 6. Hướng dẫn sử dụng chương trình

1. Sau khi chạy chương trình thì form **Dashboard** sẽ được hiển thị đầu tiên.
2. Chọn vào **SERVER** và nhấn **Start** để chạy server.
3. Quay lại **Dashboard** và click chọn **CLIENT**
4. Lúc này, Form **Đăng nhập** sẽ được hiện lên và bạn sẽ thực hiện các tác vụ bạn muốn đối với ứng dụng.
5. Để tắt ứng dụng, ta có thể tắt trực tiếp **Dashboard**, như vậy sẽ trực tiếp dừng ứng dụng.

## Chức năng và hình ảnh của các form

| 🧩 Tên Form | 💬 Mô tả chức năng | 🖼️ Ảnh minh họa |
|--------------|--------------------|------------------|
| **Dashboard** | Màn hình chính cho phép người dùng lựa chọn mở client hay server. | <img width="534" height="410" alt="image" src="https://github.com/user-attachments/assets/eb99270e-385a-49e4-8cfd-ac49a51a7569" />|
| **FormDangKy** | Cho phép người dùng tạo tài khoản mới, nhập tên đăng nhập, email hay số điện thoại, mật khẩu, xác nhận mật khẩu. | <img width="866" height="961" alt="Screenshot 2025-10-28 205717" src="https://github.com/user-attachments/assets/708147bd-812d-4eea-84ea-a73f68f436f5" />|
| **FormDangNhap** | Màn hình đăng nhập với tính năng Remember Me, xác thực người dùng và chuyển đến form xác nhận đăng nhập thành công. | <img width="722" height="818" alt="image" src="https://github.com/user-attachments/assets/85baaf54-c4ea-4616-88e9-499f82b92686" />|
| **FormQuenPass** | Cho phép người dùng nhập email hay số điện thoại đã đăng ký để nhận mã OTP khôi phục mật khẩu. | <img width="720" height="716" alt="image" src="https://github.com/user-attachments/assets/cf599b8c-cc9d-4657-ab4f-49cf2f0a670a" />|
| **FormXacThucOTP** | Màn hình xác thực mã OTP được gửi đến email hay SMS người dùng để đặt lại mật khẩu. | <img width="714" height="836" alt="image" src="https://github.com/user-attachments/assets/3bdbbad3-4ffb-4c0a-8aad-8b8e9658eb2b" />|
| **FormResetPass** | Cho phép người dùng nhập và xác nhận mật khẩu mới sau khi xác thực OTP thành công. | <img width="722" height="803" alt="image" src="https://github.com/user-attachments/assets/298cd7ea-215d-4e7c-81b0-0ef811a19271" />|
| **MainForm** | Hiển thị màn hình sau khi đăng nhập thành công. | <img width="1633" height="954" alt="image" src="https://github.com/user-attachments/assets/5addf99f-e739-4022-a71d-6c9690ff1e72" />|
| **UIServer** | Màn hình cho phép chạy/dừng server, đồng thời xem các log đã được thực hiện đối với server. | <img width="717" height="543" alt="image" src="https://github.com/user-attachments/assets/766c838b-bb8f-4974-ba45-fbf731ad3a49" />|
