using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataTypes
{
    public class Either<TL, TR>
    {
        private TL _left;
        private TR _right;
        public bool IsRight = true;

        private Either() { }
        public static Either<TL,TR> Left(TL value)
        {
            var either = new Either<TL, TR>
            {
                _left = value,
                IsRight = false
            };
            return either;
        }
        public static Either<TL,TR> Right(TR value)
        {
            var either = new Either<TL, TR>
            {
                _right = value,
                IsRight = true
            };
            return either;
        }
        public TL LeftOrThrow {
            get
            {
                if (IsRight)
                {
                    throw new Exception("Cannot access Left of Either with a Right.  Please check before accessing");
                }
                return _left;
            } }
        public TR RightOrThrow
        {
            get
            {
                if (IsRight)
                {
                    throw new Exception("Cannot access Right of Either with a Left.  Please check before accessing");
                }
                return _right;
            }
        }
    }
}
