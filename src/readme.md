# 🎄 Advent of Code Project
*A .NET 10 Shared Library for Fetching AoC Puzzle Input, Puzzle HTML, and Caching Results*

This project provides:

- Automatic puzzle input & HTML downloading  
- Raw HTML preservation (`<p>`, `<em>`, `<code>`, `<ul>`, etc.)  
- Local caching per puzzle  
- A DI-ready `IAdventService`  
- A standardized MSTest template for solving each puzzle  
- A clean folder structure per **year → day**  

---

## Requirements

- **.NET 10** SDK  
- Advent of Code account  
- Environment variable `AOCSession` containing your session cookie  

---

## Getting Your Advent of Code Session Token

To fetch *your* puzzle input, AoC requires authentication via a cookie named **`session`**.

### How to Get It (Chrome / Edge)

1. Log in at: https://adventofcode.com/

2. Open Developer Tools  
   ```
   F12 → Application → Cookies
   ```

3. Select:  
   ```
   https://adventofcode.com
   ```

5. Copy the value of the `session` cookie

### Set the Environment Variable

**Windows**
```cmd
setx AOCSession "<your-token>"
```

**macOS / Linux**
```bash
export AOCSession="<your-token>"
```

---

## Shared Library: `Advent.Shared`

### Puzzle Model

```csharp
public class Puzzle
{
    public int Year { get; set; }
    public int Day { get; set; }
    public string Title { get; set; }   // Parsed <h2> title
    public string Body { get; set; }    // Raw HTML (<p>, <em>, <ul>, etc.)
    public string Input { get; set; }   // Puzzle input text
}
```

### Service Interface

```csharp
public interface IAdventService
{
    Task<Puzzle> GetPuzzleAsync(int year = 2015, int day = 1);
}
```

### Service Responsibilities

✔ Download puzzle input  
✔ Download puzzle HTML page  
✔ Extract `<article class="day-desc">` block  
✔ Preserve all HTML tags  
✔ Save to:  

```
%TEMP%/AOC{year}{day}.json
```

✔ Use cached version on subsequent runs  

---

## Recommended Project Structure

```
Advent/
 ├─ Advent.Shared/
 │   ├─ Models/
 │   │   └─ Puzzle.cs
 │   ├─ Services/
 │   │   └─ AdventService.cs
 │   └─ Providers/
 │       └─ AdventServiceProvider.cs
 │
 └─ Advent.Solutions/
     ├─ 2015/
     │   ├─ Day01.cs
     │   ├─ Day02.cs
     │   └─ ...
     ├─ 2016/
     │   └─ Day01s.cs
     ├─ 2024/
     │   ├─ Day01.cs
     │   └─ Day02.cs
```
