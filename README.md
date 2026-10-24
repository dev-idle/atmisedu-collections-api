# atMISEDU Collections API

Web API quản lý bài viết cho hệ thống atMISEDU, xây dựng bằng ASP.NET Core 8 theo mô hình Clean Architecture. Module **COLLECTIONS** gồm bài viết, tag, tương tác, đánh giá, bình luận/phản hồi và quà tặng; module **HUBS** là phần CRUD mẫu HubsChannel của bài lab.

## Công nghệ

| Thành phần | Phiên bản |
| --- | --- |
| .NET / ASP.NET Core | 8.0 |
| Entity Framework Core (SQL Server) | 8.0.6 |
| MediatR (CQRS) | 12.4.0 |
| Asp.Versioning | 8.1.0 |
| Swashbuckle (Swagger) | 6.5.0 / 6.6.2 |
| SQL Server Express | 2019 trở lên (instance `.\SQLEXPRESS`) |

## Cấu trúc solution

```text
src/
├── atMISEDU.Api             Khởi động ứng dụng, Program.cs, appsettings.json
├── atMISEDU.Presentation    Controllers/v1/<MODULE>, RouteConstant
├── atMISEDU.Application     Requests, Validators, UserCases (handler), Responses
├── atMISEDU.Domain          Entities, Constants, interface repository
├── atMISEDU.Persistence     ApplicationDbContext, Configurations, Repositories
├── atMISEDU.Infrastructure  Tích hợp dịch vụ bên ngoài
└── atMISEDU.Contract        Result, Validator, CustomException, MsgCode, helper
test/
└── atMISEDU.Application.Tests
script/
├── script_db.txt            Tạo database MISEDU và bảng hubsChannel
└── script_collections.sql   Tạo 10 bảng của module COLLECTIONS
```

Chiều tham chiếu: `Api → Presentation → Application → Domain → Contract`, `Presentation → Infrastructure → Persistence → Domain`.

Mỗi tài nguyên được trải ra các tầng theo cùng một quy ước:

| Thành phần | Thư mục |
| --- | --- |
| Entity, hằng số | `Domain/Entities/COLLECTIONS`, `Domain/Constants/COLLECTIONS` |
| Interface repository | `Domain/Abstractions/Repositories/Sql/COLLECTIONS` |
| Cấu hình bảng, repository | `Persistence/Configurations/COLLECTIONS`, `Persistence/Repositories/Sql/COLLECTIONS` |
| Request, Validator, Handler, Response | `Application/{Requests,Validators,UserCases,Responses}/COLLECTIONS/<Entity>` |
| Controller | `Presentation/Controllers/v1/COLLECTIONS` |

## Cài đặt và chạy

1. Mở SSMS, kết nối `.\SQLEXPRESS`, chạy `script/script_db.txt` rồi `script/script_collections.sql`.
2. Kiểm tra chuỗi kết nối `DbSqlServer` trong `src/atMISEDU.Api/appsettings.json`.
3. Chạy API:

```powershell
dotnet build
dotnet run --project src/atMISEDU.Api --launch-profile http
```

4. Mở Swagger tại `http://localhost:5193/swagger` (profile `https`: `https://localhost:7078/swagger`).

## Danh sách API

Mỗi tài nguyên có 5 API: `POST`, `PUT /{id}`, `DELETE /{id}`, `GET /{id}`, `GET` (lọc bằng query).

| Tài nguyên | Route | Khóa | Bộ lọc của GET danh sách |
| --- | --- | --- | --- |
| Bài viết | `/v1/collections/CollectionArticles` | `articleId` | `isActived`, `keyTags` |
| Tag | `/v1/collections/CollectionTags` | `keyTags` | `isActived` |
| Danh mục quà | `/v1/collections/CollectionAwardCatalog` | `awardId` | `isActived` |
| Tương tác bài viết | `/v1/collections/CollectionArticlesInteraction` | `interactionId` | `articleId`, `isActived` |
| Tương tác bình luận | `/v1/collections/CollectionArticlesDiscussionInteraction` | `interactionId` | `discussionId`, `articleId`, `isActived` |
| Đánh giá bài viết | `/v1/collections/CollectionArticlesVote` | `votesId` | `articleId`, `isActived` |
| Bình luận / phản hồi | `/v1/collections/CollectionArticlesDiscussion` | `discussionId` | `articleId`, `discussionPid`, `isActived` |
| Quà tặng bài viết | `/v1/collections/CollectionArticlesAward` | `awId` | `articleId`, `isActived` |
| Quà tặng bình luận | `/v1/collections/CollectionArticlesDiscussionAward` | `daId` | `discussionId`, `isActived` |

## Quy tắc nghiệp vụ chính

- `isActived`: 0 chưa duyệt, 1 đã duyệt, 2 đã xóa. POST mặc định 1, POST/PUT chỉ nhận 0 hoặc 1, DELETE là xóa mềm (gán 2). GET danh sách mặc định trả trạng thái 0 và 1; GET chi tiết không trả dòng đã xóa.
- `createdBy` do client gửi khi tạo (> 0); `createdDate` do backend gán theo Unix UTC (giây). PUT chỉ cập nhật trường được gửi có giá trị, không đổi khóa chính, người tạo, ngày tạo.
- Chuỗi được trim; độ dài và NULL theo script. ID phải > 0; ID liên kết phải tồn tại và chưa bị xóa.
- `intnType` thuộc `HAHA`, `LOVE`, `SAD`, `LIKE`; `votesPoint` từ 1 đến 5; `articleTimeReading`, `articleViews`, `awPoint` không âm.
- Tương tác, đánh giá, quà tặng: mỗi người một bản ghi trên mỗi đối tượng. POST lại cập nhật bản ghi cũ, giữ ID và ngày tạo; bản ghi đã xóa được dùng lại và chuyển về 1.
- `tagIds` của bài viết khi PUT: không gửi hoặc `null` giữ nguyên, `[]` gỡ hết, danh sách mới thay thế; ID trùng chỉ lưu một lần.
- Chi tiết bài viết trả thêm `tags`, `discussionCount`, `interactionCount`, `awardCount`; chi tiết bình luận trả thêm `interactionCount`, `awardCount`. Số đếm chỉ tính trạng thái 1.

## Định dạng phản hồi

```json
{ "statusCode": 200, "isSuccess": true, "data": { "articleId": 1 } }
```

| Trường hợp | HTTP | `data` |
| --- | --- | --- |
| POST thành công | 200 | `{ <khóa>: id }` |
| PUT, DELETE thành công | 200 | không có (null) |
| GET chi tiết / danh sách | 200 | object / mảng (rỗng là `[]`) |
| Dữ liệu đầu vào sai | 400 | `messageCode` dạng `ERR_..._INVALID` |
| ID không tồn tại hoặc đã xóa | 404 | `messageCode` dạng `ERR_..._ID_NOT_FOUND` |

## Ghi chú so với tài liệu lab

- Cột `tinyint` (`isActived`, `votesPoint`) được ánh xạ sang `int` bằng `HasConversion<byte>()`, áp dụng cả cho `HubsChannel`.
- Request không đọc được (JSON sai định dạng, sai kiểu dữ liệu) trả 400 theo định dạng `Result`.
- Swagger gọi đúng host đang chạy, dùng được với cả profile `http` và `https`.
- Dữ liệu trả về dùng DTO trong `Application/Responses` để tên trường trùng tên cột (ví dụ `articleId`).

## gRPC

`atMISEDU.Grpc` là cổng vào thứ hai bên cạnh REST, dùng lại toàn bộ handler của tầng Application qua MediatR.

| Project | Vai trò |
| --- | --- |
| `atMISEDU.Protos` | File `.proto` (`common.proto`, `COLLECTIONS/collection_tags.proto`), sinh code cho cả server và client |
| `atMISEDU.Grpc` | Host gRPC (HTTP/2): service `Services/v1/COLLECTIONS`, `ExceptionInterceptor` đổi lỗi sang gRPC status |

Hiện có `CollectionTagsService` với `Create`, `Update`, `Delete`, `GetDetail`, `GetAll`, cùng quy tắc nghiệp vụ như REST.

```powershell
dotnet run --project src/atMISEDU.Grpc --launch-profile http
```

Gọi thử bằng Postman (New → gRPC) tại `localhost:5185`, chọn **Using server reflection** để tự nạp danh sách method.

| Lỗi | gRPC status | Trailers |
| --- | --- | --- |
| Dữ liệu đầu vào sai | `InvalidArgument` | `status-code: 400`, `message-code: ERR_..._INVALID` |
| ID không tồn tại hoặc đã xóa | `NotFound` | `status-code: 404`, `message-code: ERR_..._ID_NOT_FOUND` |
| Lỗi hệ thống | `Internal` | `status-code: 500`, `message-code: ERR_INTERNAL_SERVER` |
