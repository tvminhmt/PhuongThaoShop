using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVoucher2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "StarDay",
                table: "Voucher",
                newName: "StartDay");

            migrationBuilder.AlterColumn<string>(
                name: "TenVoucher",
                table: "Voucher",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Voucher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaVoucher",
                table: "Voucher",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CrDateTime",
                table: "Voucher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrDateTime",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "StartDay",
                table: "Voucher",
                newName: "StarDay");

            migrationBuilder.AlterColumn<string>(
                name: "TenVoucher",
                table: "Voucher",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Voucher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MaVoucher",
                table: "Voucher",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Voucher",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Voucher",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
