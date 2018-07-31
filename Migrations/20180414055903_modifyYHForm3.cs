using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace YHSchool.Migrations
{
    public partial class modifyYHForm3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_DepartmentID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropIndex(
            //    name: "IX_Ent_YHForm_DepartmentID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropColumn(
            //    name: "DepartmentID",
            //    table: "Ent_YHForm");

            //migrationBuilder.AddColumn<int>(
            //    name: "WebHookID",
            //    table: "Ent_YHForm",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Ent_YHForm_WebHookID",
            //    table: "Ent_YHForm",
            //    column: "WebHookID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_WebHookID",
            //    table: "Ent_YHForm",
            //    column: "WebHookID",
            //    principalTable: "Ref_Sysconfig",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_WebHookID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropIndex(
            //    name: "IX_Ent_YHForm_WebHookID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropColumn(
            //    name: "WebHookID",
            //    table: "Ent_YHForm");

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
    }
}
