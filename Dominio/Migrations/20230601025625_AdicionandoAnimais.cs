﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoAnimais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Animais",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animais_UsuarioId",
                table: "Animais",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animais_Usuario_UsuarioId",
                table: "Animais",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_Usuario_UsuarioId",
                table: "Animais");

            migrationBuilder.DropIndex(
                name: "IX_Animais_UsuarioId",
                table: "Animais");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Animais");
        }
    }
}
