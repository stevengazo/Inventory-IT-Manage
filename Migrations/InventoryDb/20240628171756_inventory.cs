using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryIT.Migrations.InventoryDb
{
    public partial class inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Departament",
                columns: table => new
                {
                    DepartamentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departament", x => x.DepartamentID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    PhoneNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMEIs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.PhoneNumberId);
                });

            migrationBuilder.CreateTable(
                name: "SmartPhone",
                columns: table => new
                {
                    SmartPhoneModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneSerial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartPhone", x => x.SmartPhoneModelId);
                    table.ForeignKey(
                        name: "FK_SmartPhone_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Fired = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartamentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Departament_DepartamentID",
                        column: x => x.DepartamentID,
                        principalTable: "Departament",
                        principalColumn: "DepartamentID");
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    ComputerModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaveSSD = table.Column<bool>(type: "bit", nullable: false),
                    SizeDisk = table.Column<int>(type: "int", nullable: false),
                    SizeRAM = table.Column<int>(type: "int", nullable: false),
                    RAMType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyboardLayout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasNumericKeyboard = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.ComputerModelID);
                    table.ForeignKey(
                        name: "FK_Computer_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_Computer_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Peripheral",
                columns: table => new
                {
                    PeripheralModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripheral", x => x.PeripheralModelId);
                    table.ForeignKey(
                        name: "FK_Peripheral_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_Peripheral_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Phone_Number_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumberId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SmartPhoneModelId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone_Number_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Number_User_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Phone_Number_User_PhoneNumber_PhoneNumberId",
                        column: x => x.PhoneNumberId,
                        principalTable: "PhoneNumber",
                        principalColumn: "PhoneNumberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phone_Number_User_SmartPhone_SmartPhoneModelId",
                        column: x => x.SmartPhoneModelId,
                        principalTable: "SmartPhone",
                        principalColumn: "SmartPhoneModelId");
                });

            migrationBuilder.CreateTable(
                name: "PhoneExtension",
                columns: table => new
                {
                    PhoneExtensionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneExtensionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forwardings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberPBX = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneExtension", x => x.PhoneExtensionId);
                    table.ForeignKey(
                        name: "FK_PhoneExtension_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ComputerModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_Computer_ComputerModelID",
                        column: x => x.ComputerModelID,
                        principalTable: "Computer",
                        principalColumn: "ComputerModelID");
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    HistoryModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComputerModelID = table.Column<int>(type: "int", nullable: true),
                    SmartPhoneModelId = table.Column<int>(type: "int", nullable: true),
                    PeripheralModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.HistoryModelID);
                    table.ForeignKey(
                        name: "FK_History_Computer_ComputerModelID",
                        column: x => x.ComputerModelID,
                        principalTable: "Computer",
                        principalColumn: "ComputerModelID");
                    table.ForeignKey(
                        name: "FK_History_Peripheral_PeripheralModelId",
                        column: x => x.PeripheralModelId,
                        principalTable: "Peripheral",
                        principalColumn: "PeripheralModelId");
                    table.ForeignKey(
                        name: "FK_History_SmartPhone_SmartPhoneModelId",
                        column: x => x.SmartPhoneModelId,
                        principalTable: "SmartPhone",
                        principalColumn: "SmartPhoneModelId");
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "BrandId", "Name" },
                values: new object[,]
                {
                    { 1, "Dell" },
                    { 2, "Lenovo" },
                    { 3, "HP" },
                    { 4, "Epson" },
                    { 5, "Ubiquiti" },
                    { 7, "Cisco" },
                    { 8, "Samsung" },
                    { 9, "Xiaomi" },
                    { 10, "Apple" },
                    { 11, "Huawei" }
                });

            migrationBuilder.InsertData(
                table: "Departament",
                columns: new[] { "DepartamentID", "Name" },
                values: new object[,]
                {
                    { 1, "Gerencia" },
                    { 2, "Administracion" },
                    { 3, "Financiero" },
                    { 4, "Proyectos" },
                    { 5, "Operativo" },
                    { 6, "Arquitectura" },
                    { 7, "Ventas" },
                    { 8, "IT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computer_BrandId",
                table: "Computer",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_EmployeeId",
                table: "Computer",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartamentID",
                table: "Employee",
                column: "DepartamentID");

            migrationBuilder.CreateIndex(
                name: "IX_File_ComputerModelID",
                table: "File",
                column: "ComputerModelID");

            migrationBuilder.CreateIndex(
                name: "IX_History_ComputerModelID",
                table: "History",
                column: "ComputerModelID");

            migrationBuilder.CreateIndex(
                name: "IX_History_PeripheralModelId",
                table: "History",
                column: "PeripheralModelId");

            migrationBuilder.CreateIndex(
                name: "IX_History_SmartPhoneModelId",
                table: "History",
                column: "SmartPhoneModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Peripheral_BrandId",
                table: "Peripheral",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Peripheral_EmployeeId",
                table: "Peripheral",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_Number_User_EmployeeId",
                table: "Phone_Number_User",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_Number_User_PhoneNumberId",
                table: "Phone_Number_User",
                column: "PhoneNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_Number_User_SmartPhoneModelId",
                table: "Phone_Number_User",
                column: "SmartPhoneModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneExtension_EmployeeId",
                table: "PhoneExtension",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartPhone_BrandId",
                table: "SmartPhone",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Phone_Number_User");

            migrationBuilder.DropTable(
                name: "PhoneExtension");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Peripheral");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "SmartPhone");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Departament");
        }
    }
}
