﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitboard = System.UInt64;

namespace HBCommon
{
	public class Hashtable : IHashtable
	{
		private int _hashSize;
		uint[,,] Hash = new uint[2, 6, 64];
		uint[,,] Lock = new uint[2, 6, 64];

		Random _rand = new Random();

		Bitboard _currentKey;
		Bitboard _currentLock;

		HashEntry[,] _hashtab; // dimension: color, key
		Bitboard[] _hashpositions;
		int _collisions = 0;

		public Hashtable(int hashsize)
        {
			_currentKey = 0;
			_currentLock = 0;
			_hashSize = hashsize;

			InitializeHash();

			_hashtab = new HashEntry[2, _hashSize];
			_hashpositions = new Bitboard[2];

			for (int i = 0; i < _hashSize; i++)
            {
				_hashtab[0, i] = new HashEntry();
				_hashtab[1, i] = new HashEntry();
			}
		}

		public Bitboard CurrentKey => _currentKey;

		public void AddKey(ChessColor color, PieceType piece, Square square)
		{
			_currentKey ^= Hash[(int)color, (int)piece, (int)square];
			_currentLock ^= Lock[(int)color, (int)piece, (int)square];
		}

		public void AddHash(ChessColor color, int level, int score, HashEntryType type, Square from, Square to, PieceType promotionPiece)
        {
            if (_hashtab[(int)color, _currentKey].HashLock == 0)
            {
                _hashtab[(int)color, _currentKey].HashLock = _currentLock;
				_hashtab[(int)color, _currentKey].From = from;
				_hashtab[(int)color, _currentKey].To = to;
				_hashtab[(int)color, _currentKey].PromotionPiece = promotionPiece;
				_hashtab[(int)color, _currentKey].HashEntryType = type;
				_hashtab[(int)color, _currentKey].Level = level;
				_hashtab[(int)color, _currentKey].Score = score;

				_hashpositions[(int)color]++;

				////Console.WriteLine($"info added move to hashtable {from} {to}");

                return;
            }
			else if (_hashtab[(int)color, _currentKey].HashLock == _currentLock)
            {
				_hashtab[(int)color, _currentKey].From = from;
				_hashtab[(int)color, _currentKey].To = to;
				_hashtab[(int)color, _currentKey].PromotionPiece = promotionPiece;
				_hashtab[(int)color, _currentKey].HashEntryType = type;
				_hashtab[(int)color, _currentKey].Level = level;
				_hashtab[(int)color, _currentKey].Score = score;

				////Console.WriteLine($"info update move.");

				return;
            }
			else
            {
				_hashtab[(int)color, _currentKey].HashLock = _currentLock;
				_hashtab[(int)color, _currentKey].From = from;
                _hashtab[(int)color, _currentKey].To = to;
                _hashtab[(int)color, _currentKey].PromotionPiece = promotionPiece;
                _hashtab[(int)color, _currentKey].HashEntryType = type;
                _hashtab[(int)color, _currentKey].Level = level;
                _hashtab[(int)color, _currentKey].Score = score;

                _collisions++;

                ////Console.WriteLine($"info overwrite move. Collistion number: {_collisions}");

                return;
			}
        }

		public HashEntry LookupPvMove(ChessColor color)
        {
			if (_hashtab[(int)color, _currentKey].HashLock != _currentLock)
			{
				return null;
			}

			return _hashtab[(int)color, _currentKey];
		}

		private void InitializeHash()
		{
			for (int pieceType = (int)PieceType.Pawn; pieceType <= (int)PieceType.King; pieceType++)
			{
				for (int square = 0; square < 64; square++)
				{
					Hash[0, (int)pieceType, square] = GetRandom(_hashSize);
					Hash[1, (int)pieceType, square] = GetRandom(_hashSize);
					Lock[0, (int)pieceType, square] = GetRandom(_hashSize);
					Lock[1, (int)pieceType, square] = GetRandom(_hashSize);
				}
			}
		}

		uint GetRandom(int max)
        {
			var random = _rand.Next(max);
			return (uint)random;
        }
	}
}
