# Manta Chess Engine

A strong chess engine or at least it is supposed to beat me.

## Current Status

Manta Chess Engine is reasonably playing. It beats Nelson from chess.com.

### Implemented features:
- Limited implementation of UCI. 
- Evaluation of the current position material and position of the pieces.
- Alpha Beta Pruning
- Aspiration Window
- PVS search
- Keep searching after max depth for capture moves to selective depth

### Missing features:
- draw after 50 moves without capture of pawn move
- draw after position repetition

### Features for a stronger game
- Opening book
- Bitboard
- Endgame tables
- Transposition Table

### Next Tasks
- Bitboard
- UCI asynchronous
