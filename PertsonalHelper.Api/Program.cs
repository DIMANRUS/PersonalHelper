var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next();
});

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();