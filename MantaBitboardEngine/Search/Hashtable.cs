using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitboard = System.UInt64;
using MantaCommon;

namespace MantaBitboardEngine
{
	public class HashEntry
    {
		public Bitboard HashLock { get; set; }
		public Square From { get; set; }
		public Square To { get; set; }
		public BitPieceType PromotionPiece { get; set; }
		public int Level { get; set; }
		public int Score { get; set; }
		public HashEntryType HashEntryType { get; set; }
	}

	public enum HashEntryType
    {
		Exact = 0
    }

	public interface IHashtable
    {
		void AddKey(ChessColor color, BitPieceType piece, Square square);
		void AddHash(ChessColor color, int level, int score, HashEntryType type, Square from, Square to, BitPieceType promotionPiece);
		HashEntry LookupPvMove(ChessColor color);
	}

	public class Hashtable : IHashtable
	{
		int HashSize = 2 * 1024 * 1024;
		uint[,,] Hash = new uint[2, 6, 64];
		uint[,,] Lock = new uint[2, 6, 64];

		Random _rand = new Random();

		Bitboard _currentkey;
		Bitboard _currentlock;

		HashEntry[,] _hashtab; // dimension: color, key
		Bitboard[] _hashpositions;
		int _collisions = 0;

		public Hashtable()
		{
			InitializeHash();
			_currentkey = 0;
			_currentlock = 0;

			_hashtab = new HashEntry[2, HashSize];
			_hashpositions = new Bitboard[2];

			for (int i = 0; i < HashSize; i++)
            {
				_hashtab[0, i] = new HashEntry();
				_hashtab[1, i] = new HashEntry();
			}
		}

		public void AddKey(ChessColor color, BitPieceType piece, Square square)
		{
			_currentkey ^= Hash[(int)color, (int)piece, (int)square];
			_currentlock ^= Lock[(int)color, (int)piece, (int)square];
		}

		public void AddHash(ChessColor color, int level, int score, HashEntryType type, Square from, Square to, BitPieceType promotionPiece)
        {
            if (_hashtab[(int)color, _currentkey].HashLock == 0)
            {
                _hashtab[(int)color, _currentkey].HashLock = _currentlock;
				_hashtab[(int)color, _currentkey].From = from;
				_hashtab[(int)color, _currentkey].To = to;
				_hashtab[(int)color, _currentkey].PromotionPiece = promotionPiece;
				_hashtab[(int)color, _currentkey].HashEntryType = type;
				_hashtab[(int)color, _currentkey].Level = level;
				_hashtab[(int)color, _currentkey].Score = score;

				_hashpositions[(int)color]++;

				////Console.WriteLine($"info added move to hashtable {from} {to}");

                return;
            }
			else if (_hashtab[(int)color, _currentkey].HashLock == _currentlock)
            {
				_hashtab[(int)color, _currentkey].From = from;
				_hashtab[(int)color, _currentkey].To = to;
				_hashtab[(int)color, _currentkey].PromotionPiece = promotionPiece;
				_hashtab[(int)color, _currentkey].HashEntryType = type;
				_hashtab[(int)color, _currentkey].Level = level;
				_hashtab[(int)color, _currentkey].Score = score;

				////Console.WriteLine($"info update move.");

				return;
            }
			else
            {
				_hashtab[(int)color, _currentkey].HashLock = _currentlock;
				_hashtab[(int)color, _currentkey].From = from;
                _hashtab[(int)color, _currentkey].To = to;
                _hashtab[(int)color, _currentkey].PromotionPiece = promotionPiece;
                _hashtab[(int)color, _currentkey].HashEntryType = type;
                _hashtab[(int)color, _currentkey].Level = level;
                _hashtab[(int)color, _currentkey].Score = score;

                _collisions++;

                ////Console.WriteLine($"info overwrite move. Collistion number: {_collisions}");

                return;
			}
        }

		public HashEntry LookupPvMove(ChessColor color)
        {
			if (_hashtab[(int)color, _currentkey].HashLock != _currentlock)
			{
				return null;
			}

			return _hashtab[(int)color, _currentkey];
		}

		private void InitializeHash()
		{
			for (int pieceType = (int)BitPieceType.Pawn; pieceType <= (int)BitPieceType.King; pieceType++)
			{
				for (int square = 0; square < 64; square++)
				{
					Hash[0, (int)pieceType, square] = GetRandom(HashSize);
					Hash[1, (int)pieceType, square] = GetRandom(HashSize);
					Lock[0, (int)pieceType, square] = GetRandom(HashSize);
					Lock[1, (int)pieceType, square] = GetRandom(HashSize);
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
