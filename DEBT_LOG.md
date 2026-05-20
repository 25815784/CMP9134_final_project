# Technical Debt Log

I identified these "Code Smells" (bad habits) in the legacy logic:

1. **Messy Validation:** The old code used manual checks. I replaced this with automatic [Required] tags in my C# code.
2. **Nested If-Statements:** The old code was like a maze of "if" blocks. I used "Guard Clauses" to check for errors early and exit.
3. **Magic Numbers:** The old code had random numbers like 100 or 50 without explanation. I made the logic clearer.
4. **Duplicated Logic:** The code for capping the score at 100 was repeated. I simplified this using `Math.Min`.