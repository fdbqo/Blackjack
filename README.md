# Blackjack

Some info --------------
- you can currently always deposit, just in case the user loses,
  although there is a system to limit deposit (cannot deposit 20% above wallet,
  unless wallet is below 1 which allows deposits of up to 500)
- dealer is not "smart", won't hit above 17 even if player has above, unless on a soft 17 (ace + etc)
- game uses 6 decks like real life casinos
- game uses 1.5x multiplier for blackjack win, and 0.1x multiplier for draw in the unlikely case
- 1k starting balance, can be changed easily
- used cards are removed from the deck, so dupes cannot be generated if used up
