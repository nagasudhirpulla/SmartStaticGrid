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

## 🛠️ Quick Start (updated)

This quickstart shows the current component parameters and a minimal working example. For fully working examples, see `SmartStaticGrid.Demos/Components/Pages/Home.razor` and `SmartStaticGrid.Demos/Data/UserService.cs` in this repository.

1) Install the package

```bash
dotnet add package Nagasudhir.SmartStaticGrid
```

2) Define your column definitions

```razor
@code {
    private List<ColumnDef> userCols = new() {
        new("Name", "Full Name", Searchable: true),
        new("Email", "Email Address", Searchable: true),
        new("Role", "User Role", Searchable: false)
    };
}
```

3) Implement the data provider

The `DataProvider` must match the signature `Func<GridRequest, Task<GridResponse<TItem>>>`. Return items for the requested page and the total count when appropriate.

```csharp
private async Task<GridResponse<User>> LoadUsers(GridRequest req)
{
    // Forward filters/sort/paging to your data access layer
    var (items, total) = await UserService.GetUsersAsync(req);
    return new GridResponse<User>(items, total);
}
```

4) Use the component in a Razor page

```razor
<SmartStaticGrid Id="UserList"
                 TItem="User"
                 Columns="userCols"
                 DataProvider="LoadUsers"
                 TableClass="table table-striped"
                 PagerButtonClass="btn btn-sm"
                 PageSizeOptions="new[] {10,25,50}">

    <RowTemplate Context="user">
        <td>@user.FullName</td>
        <td>@user.Email</td>
        <td>@user.Role</td>
    </RowTemplate>

    <EmptyTemplate>
        <div>No users found.</div>
    </EmptyTemplate>

</SmartStaticGrid>
```

Notes
- The component persists grid state in hidden form fields and uses Blazor's enhanced form behavior to submit sorting, filtering and paging back to the server.
- `AntiforgeryToken` is included automatically by the component.
- The `Action` parameter is used internally for actions like `sort:<columnKey>` when header buttons are clicked.

Filtering
- Filter inputs are generated when a column is marked `Searchable: true`. The input name maps to `State.Filters["<ColumnKey>"]` where `ColumnKey` is the `Key` you supplied in `ColumnDef`.

Example: reading filters in your `DataProvider`

```csharp
private async Task<GridResponse<User>> LoadUsers(GridRequest req)
{
    // Filters are string values; keys match ColumnDef.Key
    var nameFilter = req.Filters.GetValueOrDefault("Name");
    var emailFilter = req.Filters.GetValueOrDefault("Email");

    // Delegate actual filtering/paging/sorting to your data layer
    // Demo project shows a simple implementation in SmartStaticGrid.Demos/Data/UserService.cs
    var (items, total) = await UserService.GetUsersAsync(req, nameFilter, emailFilter);
    return new GridResponse<User>(items, total);
}
```

Notes
- See the demo project for additional styling examples and advanced usage (`SmartStaticGrid.Demos/Components/Pages` and `SmartStaticGrid.Demos/Data`).

# 🎨 Framework Integration
Want to use Tailwind? Just pass the classes:
```razor
<SmartStaticGrid ... 
                 TableClass="min-w-full divide-y divide-gray-200" 
                 PagerButtonClass="px-4 py-2 border rounded-md" />
```

Another example with Radzen Blazor
```razor
<SmartStaticGrid Id="RadzenStyledUsers" 
                 TItem="User" 
                 Columns="userCols" 
                 DataProvider="LoadUsers"
                 @* Apply Radzen classes to the grid elements *@
                 TableClass="rz-grid-table rz-datatable" 
                 HeaderClass="rz-column-title"
                 RowClass="rz-datatable-even"
                 PagerContainerClass="rz-paginator"
                 PagerButtonClass="rz-paginator-page rz-button rz-button-md rz-variant-flat rz-secondary"
                 PagerActiveClass="rz-paginator-page rz-button rz-button-md rz-variant-flat rz-primary rz-state-active">

    <RowTemplate Context="user">
        <td class="rz-grid-col-text">@user.FullName</td>
        <td class="rz-grid-col-text">@user.Email</td>
        <td class="rz-grid-col-text">@user.Role</td>
    </RowTemplate>

    <EmptyTemplate>
        <div class="rz-p-12 rz-text-align-center">
            <RadzenIcon Icon="find_in_page" Style="font-size: 3rem;" />
            <p>No records found in this view.</p>
        </div>
    </EmptyTemplate>

</SmartStaticGrid>
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

# Notes
## github workflow to publish to nuget
This is in draft
```yaml
name: Publish NuGet Package

on:
  push:
    tags:
      - 'v[0-9]+.[0-9]+.[0-9]+'

jobs:
  build-and-publish:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout
        uses: actions/checkout@v4

      - name: Setup .NET SDK
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '10.0.x'

      - name: Restore
        run: dotnet restore SmartStaticGrid.Blazor/SmartStaticGrid.Blazor.csproj

      - name: Extract version from tag
        # Strip refs/tags/ and optional leading "v" from the tag to produce a valid semver like 1.0.0
        shell: bash
        run: |
          VERSION=${GITHUB_REF#refs/tags/}
          VERSION=${VERSION#v}
          echo "VERSION=$VERSION" >> $GITHUB_ENV

      - name: Build
        run: dotnet build SmartStaticGrid.Blazor/SmartStaticGrid.Blazor.csproj -c Release

      - name: Pack
        run: dotnet pack SmartStaticGrid.Blazor/SmartStaticGrid.Blazor.csproj -c Release -o ./artifacts /p:PackageVersion=$VERSION --no-build

      - name: Publish to nuget.org
        env:
          NUGET_API_KEY: ${{ secrets.NUGET_API_KEY }}
        run: |
          dotnet nuget push ./artifacts/*.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json --skip-duplicate
```

# References
* Create and publish nuget package from dotnet CLI - https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli
