using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changes5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_Role_Id",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_TB_M_Account_Nik",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "TB_M_Role");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "TB_M_AccountRole");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_Role_Id",
                table: "TB_M_AccountRole",
                newName: "IX_TB_M_AccountRole_Role_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_Nik",
                table: "TB_M_AccountRole",
                newName: "IX_TB_M_AccountRole_Nik");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Role",
                table: "TB_M_Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_Nik",
                table: "TB_M_AccountRole",
                column: "Nik",
                principalTable: "TB_M_Account",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Role_Role_Id",
                table: "TB_M_AccountRole",
                column: "Role_Id",
                principalTable: "TB_M_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Account_Nik",
                table: "TB_M_AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_AccountRole_TB_M_Role_Role_Id",
                table: "TB_M_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Role",
                table: "TB_M_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_AccountRole",
                table: "TB_M_AccountRole");

            migrationBuilder.RenameTable(
                name: "TB_M_Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "TB_M_AccountRole",
                newName: "AccountRoles");

            migrationBuilder.RenameIndex(
                name: "IX_TB_M_AccountRole_Role_Id",
                table: "AccountRoles",
                newName: "IX_AccountRoles_Role_Id");

            migrationBuilder.RenameIndex(
                name: "IX_TB_M_AccountRole_Nik",
                table: "AccountRoles",
                newName: "IX_AccountRoles_Nik");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_Role_Id",
                table: "AccountRoles",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_TB_M_Account_Nik",
                table: "AccountRoles",
                column: "Nik",
                principalTable: "TB_M_Account",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
