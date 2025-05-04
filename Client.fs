namespace MotivationalQuotes

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating
open WebSharper.UI.Notation

[<JavaScript>]
module Templates =

    type MainTemplate = Templating.Template<"Main.html", ClientLoad.FromDocument, ServerLoad.WhenChanged>

[<JavaScript>]
module Client =

    let quotes = [
        "Push yourself, because no one else is going to do it for you."
        "Great things never come from comfort zones."
        "Don’t stop when you’re tired. Stop when you’re done."
        "Wake up with determination. Go to bed with satisfaction."
    ]

    let rand = System.Random()

    let getRandomQuote () =
        quotes.[rand.Next(quotes.Length)]

    let Main () =
        let currentQuote = Var.Create "Click the button to get a quote!"
        Templates.MainTemplate.MotivationPage()
            .OnGetQuote(fun _ ->
                async {
                    let! quote = getQuoteFromAPI ()
                    currentQuote.Value <- quote
                } |> Async.Start
            )
            .QuoteText(currentQuote.View)
            .Doc()