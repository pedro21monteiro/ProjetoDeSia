﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoDeSia.Data;

namespace ProjetoDeSia.Migrations
{
    [DbContext(typeof(ProjetoDeSiaContext))]
    partial class ProjetoDeSiaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoDeSia.Models.Item", b =>
                {
                    b.Property<int>("IdItem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Importancia")
                        .HasColumnType("int");

                    b.Property<double>("Pontucao")
                        .HasColumnType("float");

                    b.Property<int>("PosicaoId")
                        .HasColumnType("int");

                    b.Property<int>("TecnicaId")
                        .HasColumnType("int");

                    b.Property<string>("classificacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdItem");

                    b.HasIndex("TecnicaId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("ProjetoDeSia.Models.Tecnica", b =>
                {
                    b.Property<int>("IdTecnica")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UtilizadorId")
                        .HasColumnType("int");

                    b.Property<string>("nomeQuadrante1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nomeQuadrante2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nomeQuadrante3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nomeQuadrante4")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTecnica");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Tecnica");
                });

            modelBuilder.Entity("ProjetoDeSia.Models.Utilizador", b =>
                {
                    b.Property<int>("IdUtilizador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUtilizador");

                    b.ToTable("Utilizador");
                });

            modelBuilder.Entity("ProjetoDeSia.Models.Item", b =>
                {
                    b.HasOne("ProjetoDeSia.Models.Tecnica", "Tecnica")
                        .WithMany("Item")
                        .HasForeignKey("TecnicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tecnica");
                });

            modelBuilder.Entity("ProjetoDeSia.Models.Tecnica", b =>
                {
                    b.HasOne("ProjetoDeSia.Models.Utilizador", "Utilizador")
                        .WithMany("Tecnica")
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("ProjetoDeSia.Models.Tecnica", b =>
                {
                    b.Navigation("Item");
                });

            modelBuilder.Entity("ProjetoDeSia.Models.Utilizador", b =>
                {
                    b.Navigation("Tecnica");
                });
#pragma warning restore 612, 618
        }
    }
}
