using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitboard = System.UInt64;

namespace MantaCommon
{
	public class HashEntry
	{
		public Bitboard HashLock { get; set; }
		public Square From { get; set; }
		public Square To { get; set; }
		public PieceType PromotionPiece { get; set; }
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
		Bitboard CurrentKey { get; }
		void AddKey(ChessColor color, PieceType piece, Square square);
		void AddHash(ChessColor color, int level, int score, HashEntryType type, Square from, Square to, PieceType promotionPiece);
		HashEntry LookupPvMove(ChessColor color);
	}
}
