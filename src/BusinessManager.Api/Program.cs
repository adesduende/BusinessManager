using BusinessManager.Application;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.UseCases.Customer.AddCustomer;
using BusinessManager.Application.UseCases.Customer.GetAllCustomers;
using BusinessManager.Application.UseCases.Order.AssignOrder;
using BusinessManager.Application.UseCases.Order.CreateOrder;
using BusinessManager.Application.UseCases.Permissions.GetAllPermissions;
using BusinessManager.Application.UseCases.Role.AddRole;
using BusinessManager.Application.UseCases.Role.GetAllRoles;
using BusinessManager.Application.UseCases.User.AssignRoles;
using BusinessManager.Application.UseCases.User.CreateUser;
using BusinessManager.Application.UseCases.User.GetAllUsers;
using BusinessManager.Application.UseCases.User.LoginUser;
using BusinessManager.Application.UseCases.User.LogoutUser;
using BusinessManager.Application.UseCases.User.UpdatePassword;
using BusinessManager.Application.UseCases.User.ValidateUser;
using BusinessManager.Infrastructure;
using BusinessManager.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Add secrets depending on the environment
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddEnvironmentVariables();
}

// Add authentication and authorization services
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings!.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.SecretKey))
        };
        options.RequireHttpsMetadata = true;
    });

// Register the dynamic authorization handler
builder.Services.AddScoped<IAuthorizationHandler, DynamicAuthorizationHandler>();

// Add authorization policies
builder.Services.AddAuthorization(
        options =>
        {
            options.DefaultPolicy =  new AuthorizationPolicyBuilder()
                .AddRequirements(new DynamicAuthorizationRequirement(""))
                .Build();
        }
    );

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with JWT authentication
builder.Services.AddSwaggerGen(options =>
{ 
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Business Manager API", Version = "v1" });

    // Define el esquema de seguridad JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

    options.CustomSchemaIds(type => type.FullName);
});

// Configure CORS to allow requests from the frontend application
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllOrigins",
                      policy =>
                      {
                          policy.WithOrigins([
                              "https://localhost:59559",
                              ]);
                          policy.AllowAnyHeader();
                          policy.AllowCredentials();
                          policy.AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllOrigins");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
#region Permissions Endpoints
app.MapGet("/permissions", async(
        [FromQuery] Guid roleId,
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var result = await mediator.SendAsync(new GetAllPermissionsQuery(roleId));
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithOpenApi();

app.MapGet("/endpoints", async (
    ) =>
{
    try
    {
        return await Task.Run(() =>
        {
            var source = app as IEndpointRouteBuilder;
            var endpoints = source.DataSources.SelectMany(ds => ds.Endpoints);
            var result = endpoints.Select(x => x.DisplayName);
            return Results.Ok(result);
        });
    }
    catch (Exception ex)
    {
        return Results.Problem("An error occurred while processing your request.", statusCode: 500);
    }
})
    .RequireAuthorization()
    .WithOpenApi();

#endregion

#region User Endpoints

app.MapGet("/users", 
    async (
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var result = await mediator.SendAsync(new GetAllUsersQuery());
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Users Endpoint")
    .WithOpenApi();

app.MapPost(
    "/user",
    async (
        [FromBody] CreateUserCommand command,
        [FromServices] IMediator mediator
        ) =>
    {
        try
        {
            var result = await mediator.SendAsync(command);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Users Endpoint")
    .WithOpenApi();

app.MapPut("/user/{id}/password", 
    async (
        [FromRoute] Guid id,
        [FromBody] string Password,
        [FromServices] IMediator mediator
        ) =>
        {
            try
            {
                var result = await mediator.SendAsync(new UpdatePasswordCommand(id, Password));
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        })
    .RequireAuthorization()
    .WithTags("Users Endpoint")
    .WithOpenApi();

app.MapPut("/user/roles", 
    async (
        [FromBody] AssignRolesCommand command,
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var result = await mediator.SendAsync(command);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Users Endpoint")
    .WithOpenApi();

app.MapPost("/user/login", async (
        HttpContext context,
        [FromBody] LoginUserCommand login,
        [FromServices] IMediator mediator
    ) =>
{
    try
    {
        var response = await mediator.SendAsync(login);
        if (response is null)
        {
            return Results.Unauthorized();
        }
        // Set a coockie with the JWT token and return the user information with the cookie
        context.Response.Cookies.Append("auth_token", response.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(7) // Set cookie expiration
        });
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.Problem(" An error occurred while processing your request.", statusCode: 500);
    }
})
    .WithTags("Users Endpoint")
    .WithOpenApi();

app.MapPost("/user/me", async (
        HttpContext context,
        [FromServices] IMediator mediator
    ) =>
{
    var jwt = context.Request.Cookies["auth_token"];
    if (jwt == null) return Results.Unauthorized();
    var response = await mediator.SendAsync(new ValidateUserCommand(jwt));
    return Results.Ok(response);
})
    .WithTags("Users Endpoint")
    .WithOpenApi();

app.MapPost("/user/logout", async (
        HttpContext context,
        [FromServices] IMediator mediator
    ) =>
{

    var jwt = context.Request.Cookies["auth_token"];
    if (jwt == null) return Results.Unauthorized();
    // Clear cookie and invalidate jwt
    context.Response.Cookies.Delete("auth_token");
    var response = await mediator.SendAsync(new LogoutUserCommand(jwt));
    return Results.Ok();
})
    .RequireAuthorization()
    .WithTags("Users Endpoint")
    .WithOpenApi();

#endregion

#region Role Endpoints
app.MapPost("/role", async (
        [FromBody] AddRoleCommand command,
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var res = await mediator.SendAsync(command);
            return Results.Ok(res);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Role Endpoint")
    .WithOpenApi();
app.MapGet("/roles", async (
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var result = await mediator.SendAsync(new GetAllRolesQuery());
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Role Endpoint")
    .WithOpenApi();
#endregion

#region Customer Endpoints

app.MapPost("/customer", async (
        [FromBody] AddCustomerCommand command,
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var result = await mediator.SendAsync(command);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem("An error occurred while processing your request.", statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Customer Endpoint")
    .WithOpenApi();

app.MapGet("/customers", async (
        [FromServices] IMediator mediator
    ) =>
{
    try
    {
        var result = await mediator.SendAsync(new GetAllCustomerCommand());
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.Problem("An error occurred while processing your request.", statusCode: 500);
    }
})
    .RequireAuthorization()
    .WithTags("Customer Endpoint")
    .WithOpenApi();

#endregion

#region Order Endpoints

app.MapPost("/order/{id}", async (
        [FromRoute] Guid id,
        [FromBody] string Description,
        [FromServices] IMediator mediator
    ) =>
    {
        try
        {
            var result = await mediator.SendAsync(new CreateOrderCommand(id, Description));
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    })
    .RequireAuthorization()
    .WithTags("Order Endpoint")
    .WithOpenApi();

app.MapPost("/order/{orderId}/assign/", async (
        [FromRoute] Guid orderId,
        [FromBody] Guid technicianId,
        [FromServices] IMediator mediator
    ) =>
{
    try
    {
        var result = await mediator.SendAsync(new AssignOrderCommand(technicianId, orderId));
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message, statusCode: 500);
    }
})
    .RequireAuthorization()
    .WithTags("Order Endpoint")
    .WithOpenApi();

#endregion

app.Run();
