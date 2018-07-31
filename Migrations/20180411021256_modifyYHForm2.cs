using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace YHSchool.Migrations
{
    public partial class modifyYHForm2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "SysType",
            //    table: "Ref_Sysconfig",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "DepartmentID",
            //    table: "Ent_YHForm",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Ent_YHForm_DepartmentID",
            //    table: "Ent_YHForm",
            //    column: "DepartmentID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_DepartmentID",
            //    table: "Ent_YHForm",
            //    column: "DepartmentID",
            //    principalTable: "Ref_Sysconfig",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_DepartmentID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropIndex(
            //    name: "IX_Ent_YHForm_DepartmentID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropColumn(
            //    name: "SysType",
            //    table: "Ref_Sysconfig");

            //migrationBuilder.DropColumn(
            //    name: "DepartmentID",
            //    table: "Ent_YHForm");
        }
    }
}
