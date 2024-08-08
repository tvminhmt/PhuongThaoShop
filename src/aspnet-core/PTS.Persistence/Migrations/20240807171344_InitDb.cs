using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PTS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardVGA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThongSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardVGA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeManagePost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CPU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardDrive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThongSo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardDrive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManagePost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagePost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThongSo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichCo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TanSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChatLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: true),
                    LastTimeChangePass = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastTimeLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaVoucher = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenVoucher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GiaTri = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ManufacturerEntityId = table.Column<int>(type: "int", nullable: true),
                    ProductTypeEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Manufacturer_ManufacturerEntityId",
                        column: x => x.ManufacturerEntityId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeEntityId",
                        column: x => x.ProductTypeEntityId,
                        principalTable: "ProductType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    UserEntityId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.UserEntityId);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Payment = table.Column<int>(type: "int", nullable: false),
                    IsPayment = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VoucherEntityId = table.Column<int>(type: "int", nullable: true),
                    UserEntityId = table.Column<int>(type: "int", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdUserId = table.Column<int>(type: "int", nullable: true),
                    UpdDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_Voucher_VoucherEntityId",
                        column: x => x.VoucherEntityId,
                        principalTable: "Voucher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Upgrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductEntityId = table.Column<int>(type: "int", nullable: false),
                    ColorEntityId = table.Column<int>(type: "int", nullable: true),
                    RamEntityId = table.Column<int>(type: "int", nullable: true),
                    CpuEntityId = table.Column<int>(type: "int", nullable: true),
                    HardDriveEntityId = table.Column<int>(type: "int", nullable: true),
                    ScreenEntityId = table.Column<int>(type: "int", nullable: true),
                    CardVGAEntityId = table.Column<int>(type: "int", nullable: true),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdUserId = table.Column<int>(type: "int", nullable: true),
                    UpdDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetail_CPU_CpuEntityId",
                        column: x => x.CpuEntityId,
                        principalTable: "CPU",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetail_CardVGA_CardVGAEntityId",
                        column: x => x.CardVGAEntityId,
                        principalTable: "CardVGA",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetail_Color_ColorEntityId",
                        column: x => x.ColorEntityId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetail_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetail_HardDrive_HardDriveEntityId",
                        column: x => x.HardDriveEntityId,
                        principalTable: "HardDrive",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetail_Product_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Ram_RamEntityId",
                        column: x => x.RamEntityId,
                        principalTable: "Ram",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetail_Screen_ScreenEntityId",
                        column: x => x.ScreenEntityId,
                        principalTable: "Screen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeProductDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillEntityId = table.Column<int>(type: "int", nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdUserId = table.Column<int>(type: "int", nullable: true),
                    UpdDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetail_Bill_BillEntityId",
                        column: x => x.BillEntityId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartEntityId = table.Column<int>(type: "int", nullable: false),
                    ProductDetailEntityId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDetail_Cart_CartEntityId",
                        column: x => x.CartEntityId,
                        principalTable: "Cart",
                        principalColumn: "UserEntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetail_ProductDetail_ProductDetailEntityId",
                        column: x => x.ProductDetailEntityId,
                        principalTable: "ProductDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetailImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDetailId = table.Column<int>(type: "int", nullable: false),
                    IsIndex = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetailImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetailImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetailImage_ProductDetail_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Serial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CrUserId = table.Column<int>(type: "int", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdUserId = table.Column<int>(type: "int", nullable: true),
                    UpdDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProductDetailEntityId = table.Column<int>(type: "int", nullable: true),
                    BillDetailEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serial_BillDetail_BillDetailEntityId",
                        column: x => x.BillDetailEntityId,
                        principalTable: "BillDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Serial_ProductDetail_ProductDetailEntityId",
                        column: x => x.ProductDetailEntityId,
                        principalTable: "ProductDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "phuongthaoshop.vn", "Administrator", "Admin", "ADMIN" },
                    { 2, "phuongthaoshop.vn", "Employee", "Employee", "EMPLOYEE" },
                    { 3, "phuongthaoshop.vn", "Customer", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "CrDateTime", "Email", "EmailConfirmed", "FullName", "IsEnabled", "LastTimeChangePass", "LastTimeLogin", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Notes", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "phuongthaoshop.vn", null, "adphuongthao@gmail.com", false, "Nguyễn Phương Thảo", null, null, null, false, null, "ADPHUONGTHAO@GMAIL.COM", "ADPHUONGTHAO", null, "AQAAAAIAAYagAAAAEE9Pfswin5ojA+GIi5Vl78PGfNNGzwrunnkqKzqlMKtZMoHAQjtRowAhz9qemcxeYw==", null, false, "phuongthaoshop.vn", null, false, "adphuongthao" },
                    { 2, 0, null, "phuongthaoshop.vn", null, "thuhuyen@gmail.com", false, "Vũ Thị Huyền", null, null, null, false, null, "THUHUYEN@GMAIL.COM", "THUHUYEN", null, "AQAAAAIAAYagAAAAENDaY5U4pYlMM5LyIGmQRfYhZ1FcgMNuO7iYPkN4NumNjQGQIoeqONpmYaBH7fxXYw==", null, false, "phuongthaoshop.vn", null, false, "thuhuyen" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserEntityId",
                table: "Address",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserEntityId",
                table: "Bill",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_VoucherEntityId",
                table: "Bill",
                column: "VoucherEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillEntityId",
                table: "BillDetail",
                column: "BillEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_CartEntityId",
                table: "CartDetail",
                column: "CartEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_ProductDetailEntityId",
                table: "CartDetail",
                column: "ProductDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManufacturerEntityId",
                table: "Product",
                column: "ManufacturerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeEntityId",
                table: "Product",
                column: "ProductTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_CardVGAEntityId",
                table: "ProductDetail",
                column: "CardVGAEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ColorEntityId",
                table: "ProductDetail",
                column: "ColorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_CpuEntityId",
                table: "ProductDetail",
                column: "CpuEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_DiscountId",
                table: "ProductDetail",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_HardDriveEntityId",
                table: "ProductDetail",
                column: "HardDriveEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ProductEntityId",
                table: "ProductDetail",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_RamEntityId",
                table: "ProductDetail",
                column: "RamEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ScreenEntityId",
                table: "ProductDetail",
                column: "ScreenEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailImage_ImageId",
                table: "ProductDetailImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailImage_ProductDetailId",
                table: "ProductDetailImage",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Serial_BillDetailEntityId",
                table: "Serial",
                column: "BillDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Serial_ProductDetailEntityId",
                table: "Serial",
                column: "ProductDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CartDetail");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "ManagePost");

            migrationBuilder.DropTable(
                name: "ProductDetailImage");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Serial");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "BillDetail");

            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "CPU");

            migrationBuilder.DropTable(
                name: "CardVGA");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "HardDrive");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Ram");

            migrationBuilder.DropTable(
                name: "Screen");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
