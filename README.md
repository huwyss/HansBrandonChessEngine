# Manta Chess Engine

A strong chess engine or at least it is supposed to beat me.

## Current Status

Manta Chess Engine is reasonably playing. It beats Nelson from chess.com.

### Implemented features:
- Limited implementation of UCI. 
- Minimax search performance allows for 3 or 4 half moves (4 takes about 20 seconds thinking time, 3 takes less than a second)
- Evaluation of the current position via points for each piece. Additionally pawn and knight get points for position on board and doulbe bishop gives extra points.
- Alpha Beta Pruning

### Missing features:
- draw after 50 moves without capture of pawn move
- draw after position repetition

### Features for a stronger game
- Opening book
- Keep searching after max depth for selected moves
- Move ordering for alpha beta pruning
- Faster move generator with better board representation
- How to improve endgame?

### Next Tasks
- Improve search
- UCI asynchronous

