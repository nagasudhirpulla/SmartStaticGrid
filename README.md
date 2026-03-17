# 🚀 SmartStatic Grid for Blazor SSR
SmartStatic Grid is a high-performance, style-agnostic, and stateful data grid designed specifically for Blazor Static Server Rendering (SSR). It provides the smooth feel of a modern SPA without the overhead of SignalR, WebAssembly, or heavy JavaScript.

# 🌟 Why SmartStatic Grid?
Standard Blazor grids often require Interactive Server (SignalR) or WebAssembly to handle simple tasks like pagination or sorting. This adds latency and server memory overhead.

SmartStatic Grid changes the game by:
* Zero Interactivity Required: Works perfectly in pure Static SSR mode.
* Enhanced Form Persistence: Uses Blazor's data-enhance to patch the DOM without a full page refresh.
* Clean URLs: Keeps your browser address bar free of messy ?page=1&sort=name strings by persisting state in hidden form fields.
* Style Agnostic: No hardcoded CSS. Easily "skin" it with Tailwind, Bootstrap, or Radzen using simple class parameters.
* Smart Data Fetching: Only requests the total item count when necessary (initial load or filter change), saving expensive database cycles.

# 📦 Installation
```bash
dotnet add package SmartStaticGrid.Lib
```

# 🛠️ Quick Start
1. Define your Column Definitions
```razor
@code {
    private List<ColumnDef> userCols = new() {
        new("Name", "Full Name", Searchable: true),
        new("Email", "Email Address", Searchable: true),
        new("Role", "User Role", Searchable: false)
    };
}
```

2. Implement the Data Provider
The grid doesn't care where your data comes from. Just provide a delegate that follows the GridRequest contract.
```razor
private async Task<GridResponse<User>> LoadUsers(GridRequest req)
{
    // Pass filters and sort directly to your EF Core / Dapper service
    var (items, total) = await UserService.GetUsersAsync(req);
    return new GridResponse<User>(items, total);
}
```

3. Drop in the Grid
```razor
<SmartStaticGrid Id="UserList" 
                 TItem="User" 
                 Columns="userCols" 
                 DataProvider="LoadUsers"
                 TableClass="table table-striped">
    <RowTemplate Context="user">
        <td>@user.Name</td>
        <td>@user.Email</td>
        <td>@user.Role</td>
    </RowTemplate>
</SmartStaticGrid>
```

# 🎨 Framework Integration
Want to use Tailwind? Just pass the classes:
```razor
<SmartStaticGrid ... 
                 TableClass="min-w-full divide-y divide-gray-200" 
                 PagerButtonClass="px-4 py-2 border rounded-md" />
```

# 🛡️ Security & Reliability
* CSRF Protection: Automatically includes AntiforgeryToken.
* Isolation: Unique Id parameter ensures multiple grids on the same page never clash.
* Error Handling: Built-in ErrorTemplate and EmptyTemplate for professional UX.

# Pro-Tip for Contributors
Since this grid relies on Enhanced Form Handling, ensure your App.razor includes the standard Blazor script:
```html
<script src="_framework/blazor.web.js"></script>
```
