using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changes6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_Nik",
                table: "TB_M_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_AccountRole_Nik",
                table: "TB_M_AccountRole");

            migrationBuilder.DropColumn(
                name: "id",
                table: "TB_M_AccountRole");

            migrationBuilder.DropColumn(
                name: "Nik",
                table: "TB_M_AccountRole");

            migrationBuilder.AddColumn<string>(
                name: "Account_Nik",
                table: "TB_M_AccountRole",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole",
                columns: new[] { "Account_Nik", "Role_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_Account_Nik",
                table: "TB_M_AccountRole",
                column: "Account_Nik",
                principalTable: "TB_M_Account",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_Account_Nik",
                table: "TB_M_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole");

            migrationBuilder.DropColumn(
                name: "Account_Nik",
                table: "TB_M_AccountRole");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "TB_M_AccountRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Nik",
                table: "TB_M_AccountRole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_AccountRole_Nik",
                table: "TB_M_AccountRole",
                column: "Nik");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_Nik",
                table: "TB_M_AccountRole",
                column: "Nik",
                principalTable: "TB_M_Account",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
