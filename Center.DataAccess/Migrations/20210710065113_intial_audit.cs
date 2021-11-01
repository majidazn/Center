using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Center.DataAccess.Migrations
{
    public partial class intial_audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Center");

            migrationBuilder.CreateTable(
                name: "Centers",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<int>(type: "int", nullable: false),
                    CenterGroup = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    HostName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", maxLength: 100000, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinanchialCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SepasCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ValidFrom = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ValidTo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterVariables",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    InternalUsage = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ShortKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterVariables_CenterVariables_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Center",
                        principalTable: "CenterVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterCodes",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Insur = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterCodes_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Center",
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterRefers",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    UrlType = table.Column<byte>(type: "tinyint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterRefers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterRefers_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Center",
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterTelecoms",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<int>(type: "int", nullable: false),
                    TellNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterTelecoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterTelecoms_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Center",
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicAddresses",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    EType = table.Column<int>(type: "int", nullable: false),
                    EAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicAddresses_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Center",
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterVariableId = table.Column<int>(type: "int", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ValidTo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditBase_OperatorId = table.Column<int>(type: "int", nullable: true),
                    AuditBase_CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AuditBase_LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Center",
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_CenterVariables_CenterVariableId",
                        column: x => x.CenterVariableId,
                        principalSchema: "Center",
                        principalTable: "CenterVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Center",
                table: "CenterVariables",
                columns: new[] { "Id", "Address", "AuditId", "Code", "EnName", "Icon", "InternalUsage", "Name", "ParentId", "ShortKey", "Sort", "Status" },
                values: new object[] { 1, null, new Guid("dd1baed9-547e-4233-b3c1-a235fa14744c"), 0, "Root", null, 0, "Root", 1, null, 0, (byte)2 });

            migrationBuilder.InsertData(
                schema: "Center",
                table: "Centers",
                columns: new[] { "Id", "Address", "AuditId", "CenterGroup", "City", "EnName", "FinanchialCode", "HostName", "Logo", "Name", "NationalCode", "SepasCode", "Status", "TenantId", "Title", "ValidFrom", "ValidTo", "ZipCode" },
                values: new object[] { 1, "تهران - ضلع شمال غربی میدان فردوسی ساختمان شهد پلاک 21", new Guid("7f30cb5e-83fa-44b1-8bce-24e83f8bb7bf"), 131, 1406, "EPD2", "411131341944", "localhost:44305", null, "شرکت اطلاع رسانی پیوند داده ها", "461685665", null, (byte)0, 1, 116, new DateTimeOffset(new DateTime(2021, 7, 10, 11, 21, 12, 554, DateTimeKind.Unspecified).AddTicks(5084), new TimeSpan(0, 4, 30, 0, 0)), null, "1599945549" });

            migrationBuilder.InsertData(
                schema: "Center",
                table: "CenterVariables",
                columns: new[] { "Id", "Address", "AuditId", "Code", "EnName", "Icon", "InternalUsage", "Name", "ParentId", "ShortKey", "Sort", "Status" },
                values: new object[,]
                {
                    { 2, null, new Guid("76e49cda-adee-42e7-a0a9-abd97d5f58d0"), 0, "Used in the App", null, 0, "مورد استفاده در برنامه", 1, null, 0, (byte)0 },
                    { 10, null, new Guid("d159422d-ceac-482b-a1e6-257f58ab1014"), 0, "Main Group Application", null, 0, "گروه اصلی برنامه ها", 1, null, 0, (byte)0 },
                    { 20, null, new Guid("32a373e8-1c2d-4b75-827b-d18f8e42d7ae"), 0, "Center Group", null, 0, "گروه مرکز", 1, null, 0, (byte)0 },
                    { 22, null, new Guid("dfb049a2-36ae-4615-8519-d746b589007e"), 0, "Center Title", null, 0, "ماهیت مرکز", 1, null, 0, (byte)0 }
                });

            migrationBuilder.InsertData(
                schema: "Center",
                table: "CenterVariables",
                columns: new[] { "Id", "Address", "AuditId", "Code", "EnName", "Icon", "InternalUsage", "Name", "ParentId", "ShortKey", "Sort", "Status" },
                values: new object[,]
                {
                    { 3, null, new Guid("730fcddd-b6ab-4560-80b8-9ef443abbc8b"), 0, "EPD", null, 0, "پیوند", 2, null, 0, (byte)0 },
                    { 4, null, new Guid("4699cf66-0541-41d7-aef7-7bc87fd3fba6"), 0, "Centers", null, 0, "مراکز", 2, null, 0, (byte)0 },
                    { 5, null, new Guid("79f10363-4e48-4069-bb59-a1f8f303ea12"), 0, "Patient", null, 0, "بیمار", 2, null, 0, (byte)0 },
                    { 6, null, new Guid("3199943e-4eaf-40de-b03a-8b29e7f32005"), 0, "Centers-EPD", null, 0, "پیوند-مراکز", 2, null, 0, (byte)0 },
                    { 7, null, new Guid("48ec9e8e-2ca7-45ae-9070-24a9138a6b33"), 0, "WorkHour", null, 0, "گزارش عملکرد", 2, null, 0, (byte)0 },
                    { 23, null, new Guid("146e41e5-87b2-490b-93da-f3e06dff8358"), 0, "HIS Web", null, 0, "HIS Web", 10, null, 0, (byte)0 },
                    { 24, null, new Guid("4a41b06c-1a1c-40aa-a87a-92d1022e3ef5"), 0, "HIS Cloud", null, 0, "HIS Cloud", 10, null, 0, (byte)0 },
                    { 25, null, new Guid("f4e90167-1ed4-4b2a-ae80-ddfecf06d897"), 0, "Utilities under the Cloud", null, 0, "برنامه های کمکی تحت Cloud", 10, null, 0, (byte)0 },
                    { 26, null, new Guid("f0f47b55-19f8-492a-9eb7-8628ddac64fe"), 0, "HIS Windows Base", null, 0, "HIS Windows Base", 10, null, 0, (byte)0 },
                    { 27, null, new Guid("627fdae4-3f6f-4f99-bcc1-a990fc3a945a"), 0, "MIS", null, 0, "MIS", 10, null, 0, (byte)0 },
                    { 28, null, new Guid("95d6045f-49dc-4b46-8a47-2f0263d57712"), 0, "Pacs", null, 0, "Pacs", 10, null, 0, (byte)0 },
                    { 131, null, new Guid("00a42509-bf52-411a-915d-a849599dd4ac"), 0, "Other centers", null, 0, "سایر مراکز دانشگاهی", 20, null, 0, (byte)0 },
                    { 116, null, new Guid("822fb26b-7656-4f5d-b4ec-f6046ac53fd2"), 0, "Other", null, 0, "سایر", 22, null, 0, (byte)0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CenterId",
                schema: "Center",
                table: "Activities",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CenterVariableId",
                schema: "Center",
                table: "Activities",
                column: "CenterVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Status",
                schema: "Center",
                table: "Activities",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CenterCodes_CenterId",
                schema: "Center",
                table: "CenterCodes",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterCodes_Status",
                schema: "Center",
                table: "CenterCodes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CenterRefers_CenterId",
                schema: "Center",
                table: "CenterRefers",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterRefers_Status",
                schema: "Center",
                table: "CenterRefers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_Status",
                schema: "Center",
                table: "Centers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_TenantId",
                schema: "Center",
                table: "Centers",
                column: "TenantId",
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterTelecoms_CenterId",
                schema: "Center",
                table: "CenterTelecoms",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterTelecoms_Status",
                schema: "Center",
                table: "CenterTelecoms",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CenterVariables_ParentId",
                schema: "Center",
                table: "CenterVariables",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterVariables_Status",
                schema: "Center",
                table: "CenterVariables",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicAddresses_CenterId",
                schema: "Center",
                table: "ElectronicAddresses",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicAddresses_Status",
                schema: "Center",
                table: "ElectronicAddresses",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities",
                schema: "Center");

            migrationBuilder.DropTable(
                name: "CenterCodes",
                schema: "Center");

            migrationBuilder.DropTable(
                name: "CenterRefers",
                schema: "Center");

            migrationBuilder.DropTable(
                name: "CenterTelecoms",
                schema: "Center");

            migrationBuilder.DropTable(
                name: "ElectronicAddresses",
                schema: "Center");

            migrationBuilder.DropTable(
                name: "CenterVariables",
                schema: "Center");

            migrationBuilder.DropTable(
                name: "Centers",
                schema: "Center");
        }
    }
}
