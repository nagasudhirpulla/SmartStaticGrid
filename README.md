# 🚀 SmartStatic Grid for Blazor SSR
SmartStatic Grid is a high-performance, style-agnostic, and stateful data grid designed specifically for Blazor Static Server Rendering (SSR). It provides the smooth feel of a modern SPA without the overhead of SignalR, WebAssembly, or heavy JavaScript.

# 🌟 Why SmartStatic Grid?
Standard Blazor grids often require Interactive Server (SignalR) or WebAssembly to handle simple tasks like pagination or sorting. This adds latency and server memory overhead.
SmartStatic Grid changes the game by:
Zero Interactivity Required: Works perfectly in pure Static SSR mode.
Enhanced Form Persistence: Uses Blazor's data-enhance to patch the DOM without a full page refresh.
Clean URLs: Keeps your browser address bar free of messy ?page=1&sort=name strings by persisting state in hidden form fields.
Style Agnostic: No hardcoded CSS. Easily "skin" it with Tailwind, Bootstrap, or Radzen using simple class parameters.
Smart Data Fetching: Only requests the total item count when necessary (initial load or filter change), saving expensive database cycles.
