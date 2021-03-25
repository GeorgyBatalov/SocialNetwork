﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialNetworkSample.Data;

namespace SocialNetworkSample.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialNetworkSample.Data.Entities.ClientEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("SocialNetworkSample.Data.Entities.SubscriptionEntity", b =>
                {
                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PublisherId", "SubscriberId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("SocialNetworkSample.Data.Entities.SubscriptionEntity", b =>
                {
                    b.HasOne("SocialNetworkSample.Data.Entities.ClientEntity", "Subscriber")
                        .WithMany("Subscribers")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialNetworkSample.Data.Entities.ClientEntity", "Publisher")
                        .WithMany("Publishers")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Publisher");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("SocialNetworkSample.Data.Entities.ClientEntity", b =>
                {
                    b.Navigation("Publishers");

                    b.Navigation("Subscribers");
                });
#pragma warning restore 612, 618
        }
    }
}