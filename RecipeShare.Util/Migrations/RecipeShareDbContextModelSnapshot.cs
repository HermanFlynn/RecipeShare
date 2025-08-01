﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeShare.Util.Data;

#nullable disable

namespace RecipeShare.Util.Migrations
{
    [DbContext(typeof(RecipeShareDbContext))]
    partial class RecipeShareDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeShare.Util.Models.Database.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CookingTimeMinutes")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.PrimitiveCollection<string>("DietaryTags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = "c612eaed-6258-489b-80c1-3103bb33d628",
                            CookingTimeMinutes = 20,
                            CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            DietaryTags = "[0]",
                            Ingredients = "[\"Spaghetti\",\"Bacon\",\"Eggs\",\"Parmesan cheese\",\"Black pepper\"]",
                            IsDeleted = false,
                            ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            Title = "Spaghetti Carbonara"
                        },
                        new
                        {
                            Id = "2de1e26e-3575-4727-85cf-b7ca6af6d07f",
                            CookingTimeMinutes = 15,
                            CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            DietaryTags = "[2,4]",
                            Ingredients = "[\"Mixed vegetables\",\"Soy sauce\",\"Garlic\",\"Ginger\",\"Sesame oil\"]",
                            IsDeleted = false,
                            ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            Title = "Vegetable Stir Fry"
                        },
                        new
                        {
                            Id = "bd9c31b2-d794-4603-8023-750263ca5405",
                            CookingTimeMinutes = 30,
                            CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            DietaryTags = "[0]",
                            Ingredients = "[\"Chicken\",\"Yogurt\",\"Tikka masala sauce\",\"Rice\",\"Cilantro\"]",
                            IsDeleted = false,
                            ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            Title = "Chicken Tikka Masala"
                        },
                        new
                        {
                            Id = "26a9d2b6-733f-4d7c-9fa0-74da7fc480f7",
                            CookingTimeMinutes = 10,
                            CreatedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            DietaryTags = "[2,4,14]",
                            Ingredients = "[\"Quinoa\",\"Cucumber\",\"Tomato\",\"Feta cheese\",\"Lemon dressing\"]",
                            IsDeleted = false,
                            ModifiedDateTime = new DateTime(2023, 1, 2, 11, 30, 0, 0, DateTimeKind.Utc),
                            Title = "Quinoa Salad"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
