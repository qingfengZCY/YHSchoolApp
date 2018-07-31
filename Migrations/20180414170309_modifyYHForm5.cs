using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace YHSchool.Migrations
{
    public partial class modifyYHForm5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.CreateIndex(
            //    name: "IX_Ent_YHForm_HookID",
            //    table: "Ent_YHForm",
            //    column: "HookID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_HookID",
            //    table: "Ent_YHForm",
            //    column: "HookID",
            //    principalTable: "Ref_Sysconfig",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Ent_YHForm_Ref_Sysconfig_HookID",
            //    table: "Ent_YHForm");

            //migrationBuilder.DropIndex(
            //    name: "IX_Ent_YHForm_HookID",
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
    }
}
