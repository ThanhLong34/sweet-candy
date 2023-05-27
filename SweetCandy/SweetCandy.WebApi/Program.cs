using FluentValidation;
using SweetCandy.Core.Entities;
using SweetCandy.Data.Contexts;
using SweetCandy.Data.Seeders;
using SweetCandy.Services.Services;
using SweetCandy.WebApi.Extensions;
using SweetCandy.WebApi.Filters;
using SweetCandy.WebApi.Models;
using SweetCandy.WebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace SweetCandy.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                // Add services to the container
                builder.ConfigureCors()
                    .ConfigureServices()
                    .ConfigureSwaggerOpenApi()
                    .ConfigureFluentValidation();
            }

            var app = builder.Build();
            {
                // Configure the HTTP request pipeline
                app.SetupRequestPipeLine();




                //! Categories endpoints

                // GetCategories
                app.MapGet("/categories", async (ICategoryService service, [AsParameters] CategoryFilterModel model) =>
                {
                    var categories = await service.GetCategoriesAsync(
                        model.Name, model.ShowOnMenu);

                    return Results.Ok(categories);
                })
                .WithName("GetCategories")
                .Produces<IList<Category>>()
                .WithOpenApi();

                // AddCategory
                app.MapPost("/categories", async (
                    ICategoryService service, 
                    CategoryEditModel model,
                    IValidator<CategoryEditModel> validator
                ) =>
                {
                    model.Name = model.Name.Trim();

                    var validationResult = await validator.ValidateAsync(model);
                    if (!validationResult.IsValid)
                    {
                        return Results.BadRequest(validationResult.Errors.ToResponse());
                    }

                    Category categoryResult = await service.AddOrUpdateCategoryAsync(model.Name, model.ShowOnMenu);
                    return Results.Ok(categoryResult);
                })
                .WithName("AddCategory")
                .Accepts<Category>("application/json")
                .Produces<Category>()
                .WithOpenApi();

                // UpdateCategory
                app.MapPut("/categories/{id:int}", async (
                    ICategoryService service, 
                    CategoryEditModel model, 
                    int id,
                    IValidator<CategoryEditModel> validator
                ) =>
                {
                    model.Name = model.Name.Trim();

                    var validationResult = await validator.ValidateAsync(model);
                    if (!validationResult.IsValid)
                    {
                        return Results.BadRequest(validationResult.Errors.ToResponse());
                    }

                    Category categoryResult = await service.AddOrUpdateCategoryAsync(model.Name, model.ShowOnMenu, id);
                    return Results.Ok(categoryResult);
                })
                .WithName("UpdateCategory")
                .Accepts<Category>("application/json")
                .Produces<Category>()
                .WithOpenApi();

                // DeleteCategory
                app.MapDelete("/categories/{id:int}", async (ICategoryService service, int id) =>
                {
                    bool isSuccess = await service.DeleteCategoryAsync(id);
                    return Results.Ok(isSuccess ? "Xóa danh mục thành công" : "Không tìm thấy danh mục cần xóa");
                })
                .WithName("DeleteCategory")
                .Produces<string>()
                .WithOpenApi();




                //! Candies endpoints

                // 
                app.MapGet("/candies", async (ICandyService service, [AsParameters] CandyFilterModel model) =>
                {
                    var candies = await service.GetCandiesAsync(
                        model.Name, model.CategoryId, model.CategoryName, model.MinPrice, model.MaxPrice);

                    return Results.Ok(candies);
                })
               .WithName("GetCandies")
               .Produces<IList<Candy>>()
               .WithOpenApi();



                // App run
                app.Run();
            }
        }

        public static void GenerateData()
        {
            var dbContext = new SweetCandyContext();
            var seeder = new DataSeeder(dbContext);
            seeder.Initialize();
        }
    }
}