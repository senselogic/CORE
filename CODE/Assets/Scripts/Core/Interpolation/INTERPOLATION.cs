// -- TYPES

namespace CORE
{
    public class INTERPOLATION
    {
        // -- INQUIRIES

        public static float GetLinearRatio(
            float ratio
            )
        {
            return ratio;
        }

        // ~~

        public static float GetEaseInOutRatio(
            float ratio
            )
        {
            return ( 3.0f - 2.0f * ratio ) * ratio * ratio;
        }

        // ~~

        public static float GetQuadraticEaseInRatio(
            float ratio
            )
        {
            return ratio * ratio;
        }

        // ~~

        public static float GetQuadraticEaseOutRatio(
            float ratio
            )
        {
            return ratio * ( 2.0f - ratio );
        }

        // ~~

        public static float GetQuadraticEaseInOutRatio(
            float ratio
            )
        {
            if ( ratio < 0.5f )
            {
                return 2.0f * ratio * ratio;
            }
            else
            {
                return ( 4.0f - 2.0f * ratio ) * ratio - 1.0f;
            }
        }

        // ~~

        public static float GetCubicEaseInRatio(
            float ratio
            )
        {
            return ratio * ratio * ratio;
        }

        // ~~

        public static float GetCubicEaseOutRatio(
            float ratio
            )
        {
            ratio -= 1.0f;

            return ratio * ratio * ratio + 1.0f;
        }

        // ~~

        public static float GetCubicEaseInOutRatio(
            float ratio
            )
        {
            if ( ratio < 0.5f )
            {
                return 4.0f * ratio * ratio * ratio ;
            }
            else
            {
                return ( ratio - 1.0f ) * ( 2.0f * ratio - 2.0f ) * ( 2.0f * ratio - 2.0f ) + 1.0f;
            }
        }

        // ~~

        public static float GetQuarticEaseInRatio(
            float ratio
            )
        {
            return ratio * ratio * ratio * ratio;
        }

        // ~~

        public static float GetQuarticEaseOutRatio(
            float ratio
            )
        {
            ratio -= 1.0f;

            return 1.0f - ratio * ratio * ratio * ratio;
        }

        // ~~

        public static float GetQuarticEaseInOutRatio(
            float ratio
            )
        {
            if ( ratio < 0.5f )
            {
                return 8.0f * ratio * ratio * ratio * ratio;
            }
            else
            {
                ratio -= 1.0f;

                return 1.0f - 8.0f * ratio * ratio * ratio * ratio;
            }
        }

        // ~~

        public static float GetQuinticEaseInRatio(
            float ratio
            )
        {
            return ratio * ratio * ratio * ratio * ratio;
        }

        // ~~

        public static float GetQuinticEaseOutRatio(
            float ratio
            )
        {
            ratio -= 1.0f;

            return ratio * ratio * ratio * ratio * ratio + 1;
        }

        // ~~

        public static float GetQuinticEaseInOutRatio(
            float ratio
            )
        {
            if ( ratio < 0.5f )
            {
                return 16.0f * ratio * ratio * ratio * ratio * ratio;
            }
            else
            {
                ratio -= 1.0f;

                return 16.0f * ratio * ratio * ratio * ratio * ratio + 1;
            }
        }

        // ~~

        public static float GetRatio(
            float ratio,
            INTERPOLATION_TYPE interpolation_type
            )
        {
            switch ( interpolation_type )
            {
                case INTERPOLATION_TYPE.Linear :
                {
                    return ratio;
                }

                case INTERPOLATION_TYPE.EaseInOut :
                {
                    return GetEaseInOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuadraticEaseIn :
                {
                    return GetQuadraticEaseInRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuadraticEaseOut :
                {
                    return GetQuadraticEaseOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuadraticEaseInOut :
                {
                    return GetQuadraticEaseInOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.CubicEaseIn :
                {
                    return GetCubicEaseInRatio( ratio );
                }

                case INTERPOLATION_TYPE.CubicEaseOut :
                {
                    return GetCubicEaseOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.CubicEaseInOut :
                {
                    return GetCubicEaseInOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuarticEaseIn :
                {
                    return GetQuarticEaseInRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuarticEaseOut :
                {
                    return GetQuarticEaseOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuarticEaseInOut :
                {
                    return GetQuarticEaseInOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuinticEaseIn :
                {
                    return GetQuinticEaseInRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuinticEaseOut :
                {
                    return GetQuinticEaseOutRatio( ratio );
                }

                case INTERPOLATION_TYPE.QuinticEaseInOut :
                {
                    return GetQuinticEaseInOutRatio( ratio );
                }
            }

            return ratio;
        }
    }
}
